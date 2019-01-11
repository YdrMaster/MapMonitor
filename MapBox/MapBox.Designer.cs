namespace MapBox
{
    partial class MapBox
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.miHideAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbLine = new System.Windows.Forms.CheckBox();
            this.cbPoint = new System.Windows.Forms.CheckBox();
            this.cbReliable = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lblThr = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRecord,
            this.miHideAll,
            this.miClear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 88);
            // 
            // miRecord
            // 
            this.miRecord.Name = "miRecord";
            this.miRecord.Size = new System.Drawing.Size(152, 28);
            this.miRecord.Text = "开始录点";
            this.miRecord.Click += new System.EventHandler(this.MiRecord_Click);
            // 
            // miHideAll
            // 
            this.miHideAll.Name = "miHideAll";
            this.miHideAll.Size = new System.Drawing.Size(152, 28);
            this.miHideAll.Text = "隐藏所有";
            this.miHideAll.Click += new System.EventHandler(this.MiHideAll_Click);
            // 
            // miClear
            // 
            this.miClear.Name = "miClear";
            this.miClear.Size = new System.Drawing.Size(152, 28);
            this.miClear.Text = "清空";
            this.miClear.Click += new System.EventHandler(this.MiClear_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Control;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(326, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(62, 97);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            this.listView1.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.ListView1_ItemMouseHover);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
            this.listView1.Enter += new System.EventHandler(this.ListView1_Enter);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 37;
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.SystemColors.Control;
            this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView2.Location = new System.Drawing.Point(394, 3);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(80, 97);
            this.listView2.TabIndex = 2;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.ListView2_ItemMouseHover);
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.ListView2_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 50;
            // 
            // cbLine
            // 
            this.cbLine.AutoSize = true;
            this.cbLine.Checked = true;
            this.cbLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLine.Location = new System.Drawing.Point(133, 6);
            this.cbLine.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cbLine.Name = "cbLine";
            this.cbLine.Size = new System.Drawing.Size(70, 22);
            this.cbLine.TabIndex = 0;
            this.cbLine.Text = "连线";
            this.cbLine.UseVisualStyleBackColor = true;
            // 
            // cbPoint
            // 
            this.cbPoint.AutoSize = true;
            this.cbPoint.Checked = true;
            this.cbPoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPoint.Location = new System.Drawing.Point(3, 6);
            this.cbPoint.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cbPoint.Name = "cbPoint";
            this.cbPoint.Size = new System.Drawing.Size(124, 22);
            this.cbPoint.TabIndex = 1;
            this.cbPoint.Text = "其他定位点";
            this.cbPoint.UseVisualStyleBackColor = true;
            // 
            // cbReliable
            // 
            this.cbReliable.AutoSize = true;
            this.cbReliable.Location = new System.Drawing.Point(209, 6);
            this.cbReliable.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cbReliable.Name = "cbReliable";
            this.cbReliable.Size = new System.Drawing.Size(88, 22);
            this.cbReliable.TabIndex = 2;
            this.cbReliable.Text = "可信度";
            this.cbReliable.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.cbPoint);
            this.flowLayoutPanel1.Controls.Add(this.cbLine);
            this.flowLayoutPanel1.Controls.Add(this.cbReliable);
            this.flowLayoutPanel1.Controls.Add(this.trackBar1);
            this.flowLayoutPanel1.Controls.Add(this.lblThr);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(315, 125);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(3, 34);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(210, 38);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.Value = 50;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // lblThr
            // 
            this.lblThr.AutoSize = true;
            this.lblThr.Location = new System.Drawing.Point(219, 39);
            this.lblThr.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lblThr.Name = "lblThr";
            this.lblThr.Size = new System.Drawing.Size(26, 18);
            this.lblThr.TabIndex = 4;
            this.lblThr.Text = "50";
            // 
            // MapBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Name = "MapBox";
            this.Size = new System.Drawing.Size(477, 322);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MapBox_Paint);
            this.Resize += new System.EventHandler(this.MapBox_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miRecord;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem miHideAll;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.CheckBox cbLine;
        private System.Windows.Forms.CheckBox cbPoint;
        private System.Windows.Forms.CheckBox cbReliable;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lblThr;
    }
}
