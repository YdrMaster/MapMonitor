namespace MapMonitor
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mapBox1 = new MapBox.MapBox();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.新网络协议ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.旧网络协议ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNetworkOld = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNetworkNew = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripDropDownButton3,
            this.toolStripDropDownButton4});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 666);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(936, 30);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(100, 25);
            this.toolStripStatusLabel2.Text = "新网络协议";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(195, 25);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // mapBox1
            // 
            this.mapBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapBox1.IdDescDict = null;
            this.mapBox1.Location = new System.Drawing.Point(0, 0);
            this.mapBox1.MinSpacing = 0;
            this.mapBox1.Name = "mapBox1";
            this.mapBox1.Size = new System.Drawing.Size(936, 696);
            this.mapBox1.TabIndex = 0;
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton3.Image = global::MapMonitor.Properties.Resources.unlock;
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(42, 28);
            this.toolStripDropDownButton3.Text = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Click += new System.EventHandler(this.toolStripDropDownButton3_Click);
            // 
            // toolStripDropDownButton4
            // 
            this.toolStripDropDownButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNetworkNew,
            this.tsmiNetworkOld});
            this.toolStripDropDownButton4.Image = global::MapMonitor.Properties.Resources.network;
            this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            this.toolStripDropDownButton4.Size = new System.Drawing.Size(42, 28);
            this.toolStripDropDownButton4.Text = "toolStripDropDownButton4";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(42, 28);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.Image = global::MapMonitor.Properties.Resources.unlock;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(42, 28);
            this.toolStripDropDownButton2.Text = "toolStripDropDownButton2";
            // 
            // 新网络协议ToolStripMenuItem
            // 
            this.新网络协议ToolStripMenuItem.Name = "新网络协议ToolStripMenuItem";
            this.新网络协议ToolStripMenuItem.Size = new System.Drawing.Size(182, 30);
            this.新网络协议ToolStripMenuItem.Text = "新网络协议";
            // 
            // 旧网络协议ToolStripMenuItem
            // 
            this.旧网络协议ToolStripMenuItem.Name = "旧网络协议ToolStripMenuItem";
            this.旧网络协议ToolStripMenuItem.Size = new System.Drawing.Size(182, 30);
            this.旧网络协议ToolStripMenuItem.Text = "旧网络协议";
            // 
            // tsmiNetworkOld
            // 
            this.tsmiNetworkOld.Name = "tsmiNetworkOld";
            this.tsmiNetworkOld.Size = new System.Drawing.Size(252, 30);
            this.tsmiNetworkOld.Text = "旧网络协议";
            this.tsmiNetworkOld.Click += new System.EventHandler(this.TsmiNetworkOld_Click);
            // 
            // tsmiNetworkNew
            // 
            this.tsmiNetworkNew.Name = "tsmiNetworkNew";
            this.tsmiNetworkNew.Size = new System.Drawing.Size(252, 30);
            this.tsmiNetworkNew.Text = "新网络协议";
            this.tsmiNetworkNew.Click += new System.EventHandler(this.TsmiNetworkNew_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 696);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mapBox1);
            this.Name = "MainForm";
            this.Text = "可视化工具";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MapBox.MapBox mapBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
        private System.Windows.Forms.ToolStripMenuItem tsmiNetworkNew;
        private System.Windows.Forms.ToolStripMenuItem tsmiNetworkOld;
        private System.Windows.Forms.ToolStripMenuItem 新网络协议ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 旧网络协议ToolStripMenuItem;
    }
}

