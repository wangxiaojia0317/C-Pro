using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 信号量
{
    public class Program
    {
        static Semaphore sema = new Semaphore(5, 5);
        const int cycleNum = 9;
        static void Main(string[] args)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }
            Console.WriteLine(list.First());
            Console.ReadKey();
        }
        public static void testFun(object obj)
        {
            sema.WaitOne();
            Console.WriteLine(obj.ToString() + "进洗手间：" + DateTime.Now.ToString());
            Thread.Sleep(2000);
            Console.WriteLine(obj.ToString() + "出洗手间：" + DateTime.Now.ToString());
            sema.Release();
        }
    }
}
