//#define DEBUG
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 特性
{
    //    [Author("123",1.1,false,name ="adhfi",version =1.0,ver = false)]


    class Program
    {
      
        static void Main(string[] args)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < 20000000; i++)
            {
                dic.Add(i,i);
            }
            Console.WriteLine("输入完成");
            Console.Read();
            
        }
        
}
    public class HelpAttribute : Attribute
    {
        string url;
        string topic;
        public HelpAttribute(string url)
        {
            this.url = url;
        }

        public string Url => url;

        public string Topic
        {
            get { return topic; }
            set { topic = value; }
        }
    }
    [Help("https://docs.microsoft.com/dotnet/csharp/tour-of-csharp/attributes")]
    public class Widget
    {
        [Help("https://docs.microsoft.com/dotnet/csharp/tour-of-csharp/attributes",
        Topic = "Display")]
        public void Display(string text) { }
    }






    [AttributeUsage(AttributeTargets.All,AllowMultiple =true,Inherited =true)]
    public class AuthorAttribute : Attribute
    {
        public string name;
        public double version;
        public bool ver;
        public AuthorAttribute(string name,double version,bool ver)
        {
            this.name = name;



            this.version = 1.0;
            this.ver = false;
        }
    }



    //class Test
    //{
    //    static void function1()
    //    {
    //        Myclass.Message("In Function 1.");
    //        function2();
    //    }
    //    static void function2()
    //    {
    //        Myclass.Message("In Function 2.");
    //    }
    //    public static void Main()
    //    {
    //        Myclass.Message("adfhio");
    //        function1();
    //        Console.ReadKey();
    //    }
    //}

    //public class A
    //{
    //    [Obsolete("该方法已经被启用，使用Method代替", false)]
    //    public void old()
    //    {
    //        Console.WriteLine( "老方法");
    //    }

    //}

}

