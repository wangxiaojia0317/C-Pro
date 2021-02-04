using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace SocketNet
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketUtility.GetCurrentNetInfo(x=>x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);
            Console.Read();
        }
    }


    public class SocketUtility
    {


        private Dictionary<string, Socket> dic = new Dictionary<string, Socket>();

        #region NetInfo
        public string GetAllIPAddressInfoes()
        {
            string info = "";
            //2、获取本址IP
            IPAddress[] ips = Dns.GetHostAddresses(""); //当参数为""时返回本机所有IP
            //通过Dns.GetHostAddresses(\"\")获取本机所有IP信息:\r\n
            for (int i = 0; i < ips.Length; i++)
            {
                info += string.Format("{0}) [ip:]{1}，  [ip类型:]{2}\r\n", i.ToString(), ips[i].ToString(), ips[i].AddressFamily);
            }
            Console.WriteLine(info);
            return info;

        }

        public void GetNetInfoes()
        {
            string info = "";
            int count = 0;
            foreach (NetworkInterface netInt in NetworkInterface.GetAllNetworkInterfaces())
            {
                count++;
                info += string.Format("{0})接口名:{1}\r\n    接口类型:{2}\r\n    接口MAC:{3}\r\n    接口速度:{4}\r\n    接口描述信息:{5}\r\n", count, netInt.Name, netInt.NetworkInterfaceType, netInt.GetPhysicalAddress().ToString(), netInt.Speed / 1000 / 1000, netInt.Description);
                info += "    接口配置的IP地址:\r\n";
                foreach (UnicastIPAddressInformation ipIntProp in netInt.GetIPProperties().UnicastAddresses.ToArray<UnicastIPAddressInformation>())
                {
                    info += string.Format("    接口名:{0}，  ip:{1}，  ip类型:{2}\r\n", netInt.Name, ipIntProp.Address.ToString(), ipIntProp.Address.AddressFamily);
                }
            }

        }

        public static UnicastIPAddressInformation GetCurrentNetInfo(Func<UnicastIPAddressInformation,bool> predict)
        {
            //netInfoes.Name, ipInfo.Address
            var netInfoes = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(x => x.OperationalStatus == OperationalStatus.Up && x.NetworkInterfaceType == NetworkInterfaceType.Ethernet);
            return netInfoes.GetIPProperties().UnicastAddresses.FirstOrDefault(predict);
            
            
        }

        public void GetCurrentIP()
        {
            string infos = "";
            IPAddress[] dnsIps = Dns.GetHostAddresses(Dns.GetHostName());
            for (int i = 0; i < dnsIps.Length; i++)
            {
                if (dnsIps[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    infos += "\r\nDns.GetHostAddresses()得到本机正在使用的IP为：" + dnsIps[i].ToString();
                }
            }

        }
        #endregion


        public enum IPType
        {
            IPV4,
            IPV6
        }
        private void InitSocket(IPType ipType=IPType.IPV4)
        {
            AddressFamily addressFamily = ipType == IPType.IPV4 ? AddressFamily.InterNetwork : AddressFamily.InterNetworkV6;
            string ipAddress = GetCurrentNetInfo(x => x.Address.AddressFamily == addressFamily).Address.ToString();

            IPAddress iP = IPAddress.Parse(ipAddress);

            int port = 2021;

            IPEndPoint point = new IPEndPoint(iP,port);

            Socket socket = new Socket(addressFamily, SocketType.Stream,ProtocolType.Tcp);

            try
            {
                socket.Bind(point);

                socket.Listen(10);//同时监听的客户端的数量

                Thread th = new Thread(AcceptInfo);

                th.IsBackground = true;

                th.Start(socket);
            }
            catch
            {
                Console.WriteLine("Socket Error");
            }

        }


        private void AcceptInfo(object o)
        {
            Socket socket = o as Socket;
            while (true)
            {
                try
                {
                    Socket subSocket = socket.Accept();
                    string point = subSocket.RemoteEndPoint.ToString();
                    dic.Add(point, subSocket);
                    Thread thread = new Thread(ReceiveMsg);
                    thread.IsBackground = true;
                    thread.Start(subSocket);
                }
                catch 
                {

                }
            }
        }
        private void ReceiveMsg(object o)
        {
            Socket client = o as Socket;
            byte[] buffer = new byte[1024*2];
            while (true)
            {
                try
                {
                    int n = client.Receive(buffer);
                    //接收到数据之后开始去解包进行处理
                }
                catch
                {
                    
                }
            }
        }

















    }




}
