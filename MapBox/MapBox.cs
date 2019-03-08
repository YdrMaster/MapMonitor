using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

/// <summary>
/// 地图与定位显示
/// </summary>
namespace MapBox {
	public partial class MapBox : UserControl {
		// 地图
		private readonly List<MapNode> mapList = new List<MapNode>();

		// 移动节点
		private readonly List<PosNode> posList = new List<PosNode>();
		private          bool          leftDown;

		/// <summary>
		///     录点相邻点最小显示间隔(mm)
		/// </summary>
		public int MinSpacing; //50;

		private Point mouseLocation;

		// 鼠标拖拽与缩放
		private Point  mouseTranslate;
		private Point  mouseZoomB;
		private Point  mouseZoomBLast;
		private double mouseZoomK     = 1;
		private double mouseZoomKLast = 1;

		// 自动偏移与缩放
		private Point offset;

		// 轨迹录点
		private bool   record;
		private double scale = -1;

        // 状态栏高度
        private int statusHeight = 44;

		// 构造
		public MapBox() {
			InitializeComponent();
			// 双缓冲
			SetStyle(ControlStyles.ResizeRedraw,          true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint,  true);
			// ListView
			listView1.BorderStyle = BorderStyle.None;
			listView2.BorderStyle = BorderStyle.None;
			// 消息
			MouseMove            += MapBox_MouseMove;
			MouseWheel           += MapBox_MouseWheel;
			MouseDoubleClick     += MapBox_MouseDoubleClick;
			MouseDown            += MapBox_MouseDown;
			MouseUp              += MapBox_MouseUp;
			listView1.MouseWheel += ListView1_MouseWheel;
		}

		/// <summary>
		///     原始数据更新
		/// </summary>
		public void UpdateRaw(byte[] data) {
			try {
				var type = data[0];
				if (type == 202) // 地图
				{
					var list       = new List<Node>();
					var onePackLen = 20;
					var count      = (data.Length - 1) / onePackLen;
					for (var i = 0; i < count; i++) {
						var id  = BitConverter.ToInt32(data, onePackLen * i + 1);
						var tag = BitConverter.ToInt32(data, onePackLen * i + 5);
						var x   = BitConverter.ToInt32(data, onePackLen * i + 9);
						var y   = BitConverter.ToInt32(data, onePackLen * i + 13);
						var z   = BitConverter.ToInt32(data, onePackLen * i + 17);
						list.Add(new Node(id, tag, x, y));
					}

					UpdateMap(list);
				} else if (type == 201) // 定位
				{
					var list       = new List<Node>();
					var onePackLen = 24;
					var count      = (data.Length - 1) / onePackLen;
					for (var i = 0; i < count; i++) {
						var id  = BitConverter.ToInt32(data, onePackLen  * i + 1);
                        if (id > 99 || id < 0)
                        {
                            continue;
                        }
						var tag = BitConverter.ToInt32(data, onePackLen  * i + 5);
						var x   = BitConverter.ToInt32(data, onePackLen  * i + 9);
						var y   = BitConverter.ToInt32(data, onePackLen  * i + 13);
						var z   = BitConverter.ToInt32(data, onePackLen  * i + 17);
						var yaw = BitConverter.ToSingle(data, onePackLen * i + 21);
                        if (id == 60)
                        {
                            long time = 1548200000000 + z;
                            DateTime dt = new DateTime(1970, 1, 1);
                            Console.WriteLine(dt.AddMilliseconds(time).AddHours(8));
                        }

                        list.Add(new Node(id, tag, x, y, yaw));
					}

					UpdatePos(list);
				}
			} catch { }
		}

		/// <summary>
		///     更新地图
		/// </summary>
		public void UpdateMap(List<Node> nodeList) {
			Invoke(new UpdateData(nodes => {
				                      mapList.Clear();
				                      foreach (var node in nodes) mapList.Add(new MapNode(node));
			                      }), nodeList);
			Invalidate();
		}

		/// <summary>
		///     更新定位
		/// </summary>
		public void UpdatePos(List<Node> nodeList) {
			Invoke(new UpdateData(nodes => {
				foreach (var node in nodes) {
					PosNode posNode = null;
					foreach (var pos in posList)
						if (pos.Id == node.Id) {
							posNode = pos;
							break;
						}

					if (posNode == null) // 无则新建
					{
						posNode = new PosNode(node);
						posList.Add(posNode);
						ListView_AddItem(posNode);
					} else // 有则更新
					{
						// 轨迹录点
						if (record) {
							posNode.PosList.Add(new Node(posNode));
							for (var i = posNode.PosList.Count - 2; i >= 0; i--)
								if (posNode.PosList[i].Tag >= 0) {
									if (posNode.PosList[i].distance(posNode) < MinSpacing)
										posNode.PosList.Last().Tag -= 101;
									break;
								}
						}

						// 更新
						posNode.Set(node.X, node.Y, node.Tag, node.Yaw);
					}
				}
			}), nodeList);
			Invalidate();
		}

		// 添加ListView项
		private void ListView_AddItem(PosNode node) {
			var item = new ListViewItem(new[] {""});
			item.Tag       = node.Id;
			item.BackColor = node.Color;
			item.Checked   = true;
			listView1.Items.Insert(0, item);
			var sId                                        = node.Id.ToString();
			if (sId.Length > 2 && sId.Length % 2 == 1) sId = "0" + sId;
			item     = new ListViewItem(new[] {sId});
			item.Tag = node.Id;
			listView2.Items.Insert(0, item);
			MapBox_Resize(null, null);
		}

		// 坐标变换
		private Point CalcWinCoord(Point point) {
			// 自动变换适应窗口
			var x = (point.X - offset.X) * scale * 0.8 + 0.1 * Width;
			var y = (Height - statusHeight) - ((point.Y - offset.Y) * 
                scale * 0.8 + 0.1 * (Height - statusHeight));
			// 鼠标拖动与缩放
			x = (int) (mouseZoomK * x + mouseZoomB.X + mouseTranslate.X);
			y = (int) (mouseZoomK * y + mouseZoomB.Y + mouseTranslate.Y);
			return new Point((int) x, (int) y);
		}

		// 计算完整显示偏移及缩放
		private double CalcScale() {
			// 计算地图、定位、轨迹的坐标极大极小值
			Point min, max;
			if (mapList.Count > 0) {
				min = new Point(mapList[0].X, mapList[0].Y);
				max = new Point(mapList[0].X, mapList[0].Y);
			} else if (posList.Count > 0) {
				min = new Point(posList[0].X, posList[0].Y);
				max = new Point(posList[0].X, posList[0].Y);
			} else {
				scale = -1;
				return scale;
			}

			CalcMinMax(mapList.Cast<Node>().ToList(), ref min, ref max);
			foreach (var posNode in posList)
				if (posNode.Visible) {
					CalcMinMax(posNode,         ref min, ref max);
					CalcMinMax(posNode.PosList, ref min, ref max);
				}

			offset.X = min.X;
			offset.Y = min.Y;
			// 计算缩放比例
			scale = Math.Min((double) (Width - 60) / (max.X - min.X),
			                 (double) (Height - statusHeight) / (max.Y - min.Y));
			if (double.IsInfinity(scale)) scale = -1;
			return scale;
		}

		// 计算坐标最大最小值
		private void CalcMinMax(Node node, ref Point min, ref Point max) {
			if (min.X > node.X) min.X = node.X;
			if (max.X < node.X) max.X = node.X;
			if (min.Y > node.Y) min.Y = node.Y;
			if (max.Y < node.Y) max.Y = node.Y;
		}

		private void CalcMinMax(List<Node> dataList, ref Point min, ref Point max) {
			for (var i = 0; i < dataList.Count; i++) {
				if (min.X > dataList[i].X) min.X = dataList[i].X;
				if (max.X < dataList[i].X) max.X = dataList[i].X;
				if (min.Y > dataList[i].Y) min.Y = dataList[i].Y;
				if (max.Y < dataList[i].Y) max.Y = dataList[i].Y;
			}
		}

		// 绘图
		private void MapBox_Paint(object sender, PaintEventArgs e) {
			var graphics = e.Graphics;
			var sfCenter = new StringFormat();
			sfCenter.Alignment     = StringAlignment.Center;
			sfCenter.LineAlignment = StringAlignment.Center;
			if (CalcScale() > 0) // 计算偏移缩放
			{
				// 计算地图及定位窗口坐标(提前一次计算减小计算量)
				var winMapList = new List<Point>();
				foreach (var node in mapList) winMapList.Add(CalcWinCoord(node.Coordinate));
				var winPosList = new List<Point>();
				foreach (var node in posList) winPosList.Add(CalcWinCoord(node.Coordinate));
				// 绘制地图与移动连线
				if (winPosList.Count > 0)
					for (var i = 0; i < mapList.Count; i++)
						if (mapList[i].IsLink)
							graphics.DrawLine(Pens.Gray,
							                  winPosList.Last(), winMapList[i]);
				// 绘制地图
				for (var i = 1; i < winMapList.Count; i++)
					// 画线
					graphics.DrawLine(Pens.Black, winMapList[i - 1], winMapList[i]);
				for (var i = 0; i < winMapList.Count; i++) {
					// 画点
					var pt = winMapList[i];
					graphics.FillEllipse(mapList[i].Brush, pt.X - 10, pt.Y - 10, 20, 20);
					// 显示id
					graphics.DrawString(mapList[i].Id.ToString(), new Font("Arial", 10),
					                    Brushes.Black, new RectangleF(pt.X - 15, pt.Y - 15, 30, 30), sfCenter);
				}

				// 绘制移动点和偏航角
				for (var i = 0; i < posList.Count; i++) {
					if (!cbPoint.Checked && !posList[i].Visible) continue;
					var pt = winPosList[i];
					// 点
					graphics.FillEllipse(
					                     posList[i].Brush, pt.X - 10, pt.Y - 10, 20, 20);
					// 偏航角
					var yaw = posList[i].Yaw;
					if (!float.IsNaN(yaw)) {
						var x = (int) (50 * Math.Cos(yaw));
						var y = -(int) (50 * Math.Sin(yaw));
						graphics.DrawLine(Pens.Blue, pt.X, pt.Y, pt.X + x, pt.Y + y);
					}
				}

				// 绘制录点
				foreach (var node in posList)
					if (node.Visible) {
						var lastPt = new Point(-1, -1);
						for (var i = 0; i < node.PosList.Count; i++) {
							if (node.PosList[i].Tag < 0) continue;
							var pt = CalcWinCoord(node.PosList[i].Coordinate);
							graphics.FillEllipse(node.ReliableBrush(node.PosList[i].Tag),
							                     pt.X - 4, pt.Y - 4, 8, 8);
							// 可信度
							if (cbReliable.Checked)
								graphics.DrawString(node.PosList[i].Tag.ToString(),
								                    new Font("Arial", 10), Brushes.Black,
								                    new RectangleF(pt.X - 15, pt.Y - 15, 30, 30), sfCenter);
							// 连线
							if (cbLine.Checked && i > 0 &&
							    node.PosList[i].Tag > trackBar1.Value) {
								if (lastPt.X != -1 || lastPt.Y != -1) graphics.DrawLine(Pens.Purple, pt, lastPt);
								lastPt = pt;
							}
						}
					}

				// 录点指示
				if (record)
					if (((Environment.TickCount >> 8) & 3) > 0)
						graphics.FillEllipse(Brushes.Red, listView1.Location.X - 15, 5, 10, 10);
				// 比例尺
				var rulerWidth = 50; // 比例尺显示长度(像素)
				var rulerText  = "";
				var ruler      = rulerWidth / (scale * 0.8 * mouseZoomK);
				var nRuler     = 1;
				while (ruler >= 10) {
					ruler  /= 10;
					nRuler *= 10;
				}

				if (ruler > 1.7321 && ruler < 5.4772) // sqrt(3)~sqrt(30)
					nRuler                       *= 3;
				else if (ruler >= 5.4772) nRuler *= 10;
				rulerWidth = (int) (nRuler * scale * 0.8 * mouseZoomK);
				if (nRuler >= 1000)
					rulerText = nRuler / 1000 + "m";
				else if (nRuler >= 10)
					rulerText = nRuler / 10 + "cm";
				else
					rulerText = nRuler + "mm";
                int height = Height - statusHeight;
                graphics.DrawLine(Pens.Black, Width - 20 - rulerWidth, height - 20, Width - 20, height - 20);
				graphics.DrawLine(Pens.Black, Width - 20 - rulerWidth, height - 20, Width - 20 - rulerWidth,
				                  height                 - 23);
				graphics.DrawLine(Pens.Black, Width - 20, height - 20, Width - 20, height - 23);
				graphics.DrawString(rulerText, new Font("Arial", 10), Brushes.Black,
				                    new RectangleF(Width - 40 - rulerWidth, height - 20, 40 + rulerWidth, 20),
				                    sfCenter);
			}
		}

		// 菜单“开始/停止录点”
		private void MiRecord_Click(object sender, EventArgs e) {
			if (miRecord.Text == "开始录点") {
				record = true;
				// 清除历史数据
				foreach (var node in posList) node.PosList.Clear();
				miRecord.Text = "停止录点";
			} else {
				record        = false;
				miRecord.Text = "开始录点";
			}

			Invalidate();
		}

		// 菜单“隐藏/显示所有”
		private void MiHideAll_Click(object sender, EventArgs e) {
			if (miHideAll.Text == "隐藏所有") {
				foreach (ListViewItem item in listView1.Items) item.Checked = false;
				miHideAll.Text = "显示所有";
			} else {
				foreach (ListViewItem item in listView1.Items) item.Checked = true;
				miHideAll.Text = "隐藏所有";
			}

			Invalidate();
		}

		// 菜单“清空”
		private void MiClear_Click(object sender, EventArgs e) {
			foreach (var node in posList) node.PosList.Clear();
			Invalidate();
		}

		// 调整ListView
		private void MapBox_Resize(object sender, EventArgs e) {
			listView1.Height        = listView1.Items.Count * 16 + 4;
			listView2.Height        = listView2.Items.Count * 16 + 4;
			listView2.Location      = new Point(Width                - 50,              2);
			listView1.Location      = new Point(listView2.Location.X - listView1.Width, 2);
			flowLayoutPanel1.Width  = listView1.Location.X - 30 > 400 ? 400 : listView1.Location.X - 30;
			flowLayoutPanel1.Height = 40;
		}

		// 双击修改颜色
		private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e) {
			var hinfo = listView1.HitTest(e.Location);
			if (hinfo != null && hinfo.Item != null) {
				hinfo.Item.Checked = !hinfo.Item.Checked;
				var colorDlg = new ColorDialog();
				if (colorDlg.ShowDialog() == DialogResult.OK) {
					hinfo.Item.BackColor = colorDlg.Color;
					var id = (int) hinfo.Item.Tag;
					foreach (var node in posList)
						if (node.Id == id) {
							node.Color = colorDlg.Color;
							Invalidate();
							break;
						}
				}
			}
		}

		// checkbox
		private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e) {
			var id = (int) e.Item.Tag;
			foreach (var node in posList)
				if (node.Id == id) {
					node.Visible = e.Item.Checked;
					Invalidate();
					// 所有选中状态一致则修改“隐藏/显示所有”菜单
					var same = true;
					foreach (ListViewItem item in listView1.Items)
						if (item.Checked != e.Item.Checked)
							same = false;
					if (same) miHideAll.Text = e.Item.Checked ? "隐藏所有" : "显示所有";
					break;
				}
		}

		// listView1焦点自动转移到listView2，防止选中时遮盖颜色块
		private void ListView1_Enter(object sender, EventArgs e) => listView2.Focus();

		// ListView1选中时联动选中ListView2对应行
		private void ListView1_SelectedIndexChanged(object sender, EventArgs e) {
			if (listView1.SelectedIndices.Count > 0) listView2.Items[listView1.SelectedIndices[0]].Selected = true;
		}

		// ListView2选中时对应曲线置于最上层
		private void ListView2_SelectedIndexChanged(object sender, EventArgs e) {
			if (listView2.SelectedItems.Count > 0) {
				var id = (int) listView2.SelectedItems[0].Tag;
				for (var i = posList.Count - 1; i >= 0; i--)
					if (posList[i].Id == id) {
						// 将选中的id移至列表最后(绘图时在最上层)
						var node = posList[i];
						posList.Remove(node);
						posList.Add(node);
						Invalidate();
						break;
					}
			}
		}

		// ListView1鼠标悬停时显示录点最小间距
		private void ListView1_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e) =>
			new ToolTip().SetToolTip(
			                         e.Item.ListView, MinSpacing + "mm");

		// ListView2鼠标悬停时显示录点数
		private void ListView2_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e) {
			var id = (int) e.Item.Tag;
			foreach (var node in posList)
				if (node.Id == id) {
					if (node.PosList.Count > 0)
						new ToolTip().SetToolTip(
						                         e.Item.ListView, node.PosList.Count + "点");
					break;
				}
		}

		private void MapBox_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				leftDown      = true;
				mouseLocation = e.Location;
			}
		}

		private void MapBox_MouseUp(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) leftDown = false;
		}

		// 按下鼠标左键拖拽
		private void MapBox_MouseMove(object sender, MouseEventArgs e) {
			if (leftDown && e.Button == MouseButtons.Left) {
				mouseTranslate.X += e.Location.X - mouseLocation.X;
				mouseTranslate.Y += e.Location.Y - mouseLocation.Y;
				Invalidate();
			}

			mouseLocation = e.Location;
		}

		// 滚轮缩放
		private void MapBox_MouseWheel(object sender, MouseEventArgs e) {
			// 记录上次缩放K、B值
			mouseZoomKLast = mouseZoomK;
			mouseZoomBLast = mouseZoomB;
			// 更新K值
			//mouseZoomK += e.Delta / 1000.0;
			if (e.Delta > 0)
				mouseZoomK *= 1.1;
			else
				mouseZoomK /= 1.1;
			if (mouseZoomK <= 1) mouseZoomK = 1;
			// 更新B值
			var k = mouseZoomK / mouseZoomKLast;
			mouseZoomB.X = (int) (k * mouseZoomBLast.X + (1 - k) * (mouseLocation.X - mouseTranslate.X));
			mouseZoomB.Y = (int) (k * mouseZoomBLast.Y + (1 - k) * (mouseLocation.Y - mouseTranslate.Y));
			Invalidate();
		}

		// 双击恢复默认显示
		private void MapBox_MouseDoubleClick(object sender, MouseEventArgs e) {
			mouseTranslate = new Point();
			mouseZoomK     = 1;
			mouseZoomKLast = 1;
			mouseZoomB     = new Point();
			mouseZoomBLast = new Point();
			Invalidate();
		}

		// 滚轮调整录点相邻点最小间隔
		private void ListView1_MouseWheel(object sender, MouseEventArgs e) {
			MinSpacing += e.Delta * 50 / 120;
			if (MinSpacing < 0) MinSpacing = 0;
			new ToolTip().SetToolTip(listView1, MinSpacing + "mm");
			// 根据最小间隔更新显示标志
			foreach (var node in posList) {
				Node last = null;
				foreach (var point in node.PosList)
					if (last != null && last.distance(point) < MinSpacing) {
						point.Tag -= point.Tag >= 0 ? 101 : 0;
					} else {
						point.Tag += point.Tag < 0 ? 101 : 0;
						last      =  point;
					}
			}

			Invalidate();
		}

		// 滑块
		private void trackBar1_ValueChanged(object sender, EventArgs e) => lblThr.Text = trackBar1.Value.ToString();

		// 更新数据委托
		private delegate void UpdateData(List<Node> nodes);
	}
}