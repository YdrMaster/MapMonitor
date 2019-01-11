using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace MapBox {
	/// <summary>
	///     地图或移动节点
	/// </summary>
	public class PosNode : Node {
		/// <summary>
		///     色相索引
		/// </summary>
		private static int hueIndex;

		private readonly Hashtable brushTable = new Hashtable();
		private          Color     color;

		/// <summary>
		///     HSL色相
		/// </summary>
		private int hue;

		/// <summary>
		///     轨迹位置列表
		/// </summary>
		public List<Node> PosList = new List<Node>();

		/// <summary>
		///     轨迹是否可见
		/// </summary>
		public bool Visible = true;

		// 构造
		public PosNode(Node node)
			: base(node) {
			hue   = 20 + 40 * (hueIndex++ % 6);
			color = HslToRgb(hue, 240, 120);
		}

		/// <summary>
		///     颜色
		/// </summary>
		public Color Color {
			get => color;
			set {
				hue   = RgbToHsl(value).R;
				color = HslToRgb(hue, 240, 120);
				brushTable.Clear();
			}
		}


		/// <summary>
		///     画刷
		/// </summary>
		public Brush Brush => ReliableBrush(Tag);

		/// <summary>
		///     可信度
		/// </summary>
		public int Reliable => Tag;

		/// <summary>
		///     更新定位
		/// </summary>
		public void Set(int x, int y, int tag, float yaw) {
			X   = x;
			Y   = y;
			Tag = tag;
			Yaw = yaw;
		}

		/// <summary>
		///     根据可信度获取画刷
		/// </summary>
		public Brush ReliableBrush(int reliable) {
			if (!brushTable.ContainsKey(reliable))
				brushTable.Add(reliable, new SolidBrush(
				                                        HslToRgb(hue, reliable * 2 + 40, 120)));
			return (Brush) brushTable[reliable];
		}

		/// <summary>
		///     HSL转RGB
		/// </summary>
		private Color HslToRgb(int h, int s, int l) {
			double R, G, B;
			var    H = h / 239.0;
			var    S = s / 240.0;
			var    L = l / 240.0;
			if (s == 0) {
				R = L * 255;
				G = R;
				B = R;
			} else {
				double var_1, var_2;
				if (L < 0.5)
					var_2 = L * (1 + S);
				else
					var_2 = L + S - S * L;
				var_1 = 2.0 * L - var_2;
				R     = 255.0 * Hue2RGB(var_1, var_2, H + 1.0 / 3.0);
				G     = 255.0 * Hue2RGB(var_1, var_2, H);
				B     = 255.0 * Hue2RGB(var_1, var_2, H - 1.0 / 3.0);
			}

			return Color.FromArgb(RGB(R), RGB(G), RGB(B));
		}

		private double Hue2RGB(double v1, double v2, double vH) {
			if (vH       < 0) vH += 1;
			if (vH       > 1) vH -= 1;
			if (6.0 * vH < 1) return v1 + (v2 - v1) * 6.0 * vH;
			if (2.0 * vH < 1) return v2;
			if (3.0 * vH < 2) return v1 + (v2 - v1) * (2.0 / 3.0 - vH) * 6.0;
			return v1;
		}

		private int RGB(double value) {
			var val          = (int) value % 256;
			if (val < 0) val += 256;
			return val;
		}

		/// <summary>
		///     RGB转HSL
		/// </summary>
		private Color RgbToHsl(Color rgb) {
			double H, S, L;
			double R, G, B, Max, Min, del_R, del_G, del_B, del_Max;
			R = rgb.R / 255.0;
			G = rgb.G / 255.0;
			B = rgb.B / 255.0;

			Min     = Math.Min(R, Math.Min(G, B)); //Min. value of RGB
			Max     = Math.Max(R, Math.Max(G, B)); //Max. value of RGB
			del_Max = Max - Min;                   //Delta RGB value

			L = (Max + Min) / 2.0;

			if (del_Max == 0) //This is a gray, no chroma...
			{
				//H = 2.0/3.0;          //Windows下S值为0时，H值始终为160（2/3*240）
				H = 0; //HSL results = 0 ÷ 1
				S = 0;
			} else //Chromatic data...
			{
				if (L < 0.5) S = del_Max / (Max     + Min);
				else S         = del_Max / (2 - Max - Min);

				del_R = ((Max - R) / 6.0 + del_Max / 2.0) / del_Max;
				del_G = ((Max - G) / 6.0 + del_Max / 2.0) / del_Max;
				del_B = ((Max - B) / 6.0 + del_Max / 2.0) / del_Max;

				if (R      == Max) H     = del_B             - del_G;
				else if (G == Max) H     = 1.0 / 3.0 + del_R - del_B;
				else /*if (B == Max)*/ H = 2.0 / 3.0 + del_G - del_R;

				if (H < 0) H += 1;
				if (H > 1) H -= 1;
			}

			return Color.FromArgb((int) (H * 239), (int) (S * 240), (int) (L * 240));
		}
	}
}