using System;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MapMonitor.Properties;
using MechDancer.Framework.Dependency;
using MechDancer.Framework.Net.Modules.Multicast;
using MechDancer.Framework.Net.Presets;
using MechDancer.Framework.Net.Resources;

namespace MapMonitor
{
    public partial class MainForm : Form
    {
        Udp udp = null;
        public MainForm()
        {
            InitializeComponent();
            tsmiNetworkNew.Checked = true;

            var sIP = "230.1.1.100";
            var nPort = 60100;
            // 从配置文件读取参数
            if (ConfigurationManager.AppSettings["Address"] != null)
            {
                var sConfig = ConfigurationManager.AppSettings["Address"];
                var items = sConfig.Split(':');
                if (items.Length == 2)
                {
                    sIP = items[0].Trim();
                    nPort = int.TryParse(items[1], out var port) ? port : nPort;
                }
            }

            // 开启接收线程
            new Thread(() =>
            {
                var hub = new RemoteHub("MapMonitor",
                    group: new IPEndPoint(IPAddress.Parse(sIP), nPort),
                    additions: new MulticastListener(
                        pack => { if (tsmiNetworkNew.Checked)
                                mapBox1.UpdateRaw(pack.Command, pack.Payload); }, 
                        201, 202));
                while (true) hub.Invoke();
            })
            { IsBackground = true }.Start();

            // 旧网络
            udp = new Udp(sIP, nPort);
        }

        // TopMost功能
        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            if (TopMost)
            {
                toolStripDropDownButton3.Image = Resources._lock;
            }
            else
            {
                toolStripDropDownButton3.Image = Resources.unlock;
            }
            toolStripStatusLabel2.Text = (TopMost ? "置于顶层；" : "") +
                (tsmiNetworkNew.Checked ? "新网络协议；" : "") +
                (tsmiNetworkOld.Checked ? "旧网络协议；" : "").TrimEnd('；');
        }

        private void TsmiNetworkNew_Click(object sender, EventArgs e)
        {
            tsmiNetworkNew.Checked = !tsmiNetworkNew.Checked;
            toolStripStatusLabel2.Text = (TopMost ? "置于顶层；" : "") +
                (tsmiNetworkNew.Checked ? "新网络协议；" : "") +
                (tsmiNetworkOld.Checked ? "旧网络协议；" : "").TrimEnd('；');
        }

        private void TsmiNetworkOld_Click(object sender, EventArgs e)
        {
            tsmiNetworkOld.Checked = !tsmiNetworkOld.Checked;
            if (tsmiNetworkOld.Checked)
            {
                udp.DataUpdated += mapBox1.UpdateRaw;
            }
            else
            {
                udp.DataUpdated -= mapBox1.UpdateRaw;
            }
            toolStripStatusLabel2.Text = (TopMost ? "置于顶层；" : "") +
                (tsmiNetworkNew.Checked ? "新网络协议；" : "") +
                (tsmiNetworkOld.Checked ? "旧网络协议；" : "").TrimEnd('；');
        }
    }
}
