using System.Configuration;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MechDancer.Framework.Net.Modules.Multicast;
using MechDancer.Framework.Net.Presets;

namespace MapMonitor {
	public partial class MainForm : Form {
		public MainForm() {
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
			var hub = new RemoteHub
				(name: "MapMonitor",
				 group: new IPEndPoint(IPAddress.Parse(sIP), nPort),
				 additions: new MulticastListener
					 (pack => mapBox1.UpdateRaw(pack.Payload), 123)
				);

			// 开启接收线程
			new Thread(() => {
				           while (true) hub.Invoke();
			           }) {IsBackground = true}.Start();
		}
	}
}