using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqClass
{
    public  delegate bool Dele();

    class Program
    {
        static void Main(string[] args)
        {
            LogRecord log = new LogRecord();

            log.AddLog("afhdoi");
            log.AddLog("afhdoi");
            log.AddLog("afhdoi");
            log.AddLog("afhdoi");
            log.AddLog("afhdoi");
            Console.WriteLine(log.s);
            Console.Read();
        }




        public class LogRecord
        {
            public string s = string.Empty;
            public void AddLog(string o)
            {
                s += "\n" + o;

            }

        }









        static void ResultCon_fun()
        {
            System.Diagnostics.Stopwatch oTime = new System.Diagnostics.Stopwatch();   //定义一个计时对象    
            oTime.Start();

            //开始计时   
            Random ran = new Random();
            string path = @"C:\Users\yuanzhu\Desktop\新建文本文档.txt";
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs);

                for (long i = 0; i < 1000000; i++)
                {

                    sw.WriteLine(ran.Next(1, 500000));
                }
            }

            oTime.Stop();


            Console.WriteLine(oTime.ElapsedMilliseconds);
            // Test();


            Console.WriteLine("结束");
        }


        private static void Test()
        {
            List<AA> lstA = new List<AA>(){
               new AA { book = "科目1", name = "张三", price = "100" },
               new AA { book = "科目2", name = "李素", price = "300" },
                     new AA { book = "科目3", name = "王五", price = "3400" }};

            List<CC> lstC = new List<CC>() {
               new CC{name="张三",old="24"}, new CC{name="王五",old="26"} ,
               new CC{name="王五五",old="27"}
               };
           // 利用Contains来比较来着
            var jj = from p in lstA where !(from pp in lstC select pp.name).Contains(p.name) select p.name;
            foreach (var item in jj)
            {
                Console.WriteLine(item.ToString());

            }
          //  利用Except来比较来着
            IEnumerable<string> dd = (from p in lstA select p.name).Except(from p in lstC select p.name);
            foreach (var item in dd)
            {
                Console.WriteLine(item.ToString());
            }
            //输出李素不在CC里面
        }

        public class AA
        {
            public string book { get; set; }
            public string name { get; set; }
            public string price { get; set; }
        }
        public class CC
        {
            public string name { get; set; }
            public string old { get; set; }
        }
    }
  
  
}
