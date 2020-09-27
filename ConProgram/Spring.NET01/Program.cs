using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context.Support;
namespace Spring.NET01
{
    class Program
    {
        static void Main(string[] args)

        {

            //普通对象创建

          
            Console.Read();

        }
    }

    public class Person
    {
        public Person()
        { }

        ~Person()
        { }

        public void print()
        {
            Console.WriteLine("我是一个Person对象");

        }

    }

}
