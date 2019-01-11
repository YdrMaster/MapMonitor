using System.Windows.Forms;
using System.Configuration;

namespace MapMonitor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            string sIP = "230.1.1.100";
            int nPort = 60100;
            // 从配置文件读取参数
            if (ConfigurationManager.AppSettings["Address"] != null)
            {
                string sConfig = ConfigurationManager.AppSettings["Address"].ToString();
                string[] items = sConfig.Split(':');
                if (items.Length == 2)
                {
                    sIP = items[0].Trim();
                    int port = 0;
                    if (int.TryParse(items[1], out port))
                    {
                        nPort = port;
                    }
                }
            }
            // 启动网络连接
            Udp udp = new Udp(sIP, nPort);
            udp.DataUpdated += mapBox1.UpdateRaw;
        }
    }
}
