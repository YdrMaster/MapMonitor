using System;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MechDancer.Framework.Dependency;
using MechDancer.Framework.Net.Modules.Multicast;
using MechDancer.Framework.Net.Resources;

namespace MapMonitor
{
	public partial class MainForm : Form
    {
		public MainForm()
        {
			InitializeComponent();

			var sIP   = "230.1.1.100";
			var nPort = 60100;
			// 从配置文件读取参数
			if (ConfigurationManager.AppSettings["Address"] != null) {
				var sConfig = ConfigurationManager.AppSettings["Address"];
				var items   = sConfig.Split(':');
				if (items.Length == 2) {
					sIP   = items[0].Trim();
					nPort = int.TryParse(items[1], out var port) ? port : nPort;
				}
			}

            // 启动网络连接
            var hub = new MulticastReceiver();

            var _scope = new DynamicScope();
            _scope.Setup(new Name("MapMonitor"));


            _scope.Setup(new Networks());
            _scope.Setup(new MulticastSockets(new IPEndPoint(IPAddress.Parse(sIP), nPort)));

            var monitor = new MulticastMonitor();
            _scope.Setup(monitor);
            monitor.BindAll();
            _scope.Setup(new MulticastBroadcaster());
            _scope.Setup(hub);
            _scope.Setup(new MulticastListener
                     (pack => mapBox1.UpdateRaw(pack.Payload), 123));


            // 开启接收线程
            new Thread(() => {
                while (true) hub.Invoke();
            }) {IsBackground = true}.Start();
        }

        // TopMost功能
        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            if (TopMost)
            {
                toolStripDropDownButton3.Image = Properties.Resources._lock;
                toolStripStatusLabel2.Text = "topmost";
            }
            else
            {
                toolStripDropDownButton3.Image = Properties.Resources.unlock;
                toolStripStatusLabel2.Text = "normal";
            }
        }
    }
}