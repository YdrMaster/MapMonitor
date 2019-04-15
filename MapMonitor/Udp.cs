using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace MapMonitor
{
    class Udp
    {
        /// <summary>
        /// 数据更新回调
        /// </summary>
        public Action<byte, byte[]> DataUpdated;
        // socket
        private Socket socket;

        // 构造
        public Udp(string ip, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //IPEndPoint iep = new IPEndPoint(IPAddress.Parse("192.168.1.101"), port);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, port);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            socket.Bind(iep);
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                new MulticastOption(IPAddress.Parse(ip)));

            //BindNetworkCard();

            // 开启接收线程
            Thread thread = new Thread(WorkThreadCast);
            thread.IsBackground = true;
            thread.Start();

            
        }

        public void BindNetworkCard()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.Description.StartsWith("ASIX"))
                {
                    Console.WriteLine(adapter.GetIPProperties().UnicastAddresses[1].Address);
                    int index = adapter.GetIPProperties().GetIPv4Properties().Index;
                    socket.SetSocketOption(SocketOptionLevel.IP, 
                        SocketOptionName.MulticastInterface,
                        IPAddress.HostToNetworkOrder(index));
                }
            }
        }

        // 接收组播线程
        private void WorkThreadCast()
        {
            byte[] buffer = new byte[1024];
            EndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    int len = socket.ReceiveFrom(buffer, ref remotePoint);
                    if (len > 0 && DataUpdated != null)
                    {
                        byte type = buffer[0];
                        byte[] data = new byte[len - 1];
                        Array.Copy(buffer, 1, data, 0, len - 1);
                        DataUpdated.Invoke(type, data);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
