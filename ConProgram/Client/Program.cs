using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static void Main(string[] args)
        {
            Console.WriteLine("开启客户端");
            Init();
            while (true)
            {
                string str = Console.ReadLine();
                client.Send(Encoding.UTF8.GetBytes(str));

            }
        }
        public static void Init()
        {

            //连接到的目标IP

            IPAddress ip = IPAddress.Parse("10.12.10.166");

            //IPAddress ip = IPAddress.Any;

            //连接到目标IP的哪个应用(端口号！)

            IPEndPoint point = new IPEndPoint(ip, 2021);

            try

            {

                //连接到服务器

                client.Connect(point);
                //连接成功后，就可以接收服务器发送的信息了

                Thread th = new Thread(ReceiveMsg);

                th.IsBackground = true;

                th.Start();

            }

            catch
            {


            }

        }

        static void ReceiveMsg()
        {
            while (true)
            {
                try
                {

                    byte[] buffer = new byte[1024 * 1024];

                    int n = client.Receive(buffer);

                    string s = Encoding.UTF8.GetString(buffer, 0, n);
                }

                catch
                {

                }

            }



        }

    }
}
