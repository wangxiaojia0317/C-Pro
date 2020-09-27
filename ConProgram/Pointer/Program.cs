using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pointer
{
    class Program
    {
        struct Location
        {
            public int X;
            public int Y;
        }
         static void Main(string[] args)
        {
            unsafe
           {
                Location location;
                location.X = 10;
                location.Y = 5;
                Location* lptr = &location;
                Console.WriteLine(string.Format("location 地址{0},lptr地址{1},lptr值{2}", (int)&location, (int)lptr, *lptr));
                Console.WriteLine(string.Format("location.x的地址{0}，location.x的值{1}", (int)&(lptr->X), lptr->X));
                Console.WriteLine(string.Format("location.y的地址{0}，location.y的值{1}", (int)&(lptr->Y), lptr->Y));

            }
            Console.Read();
        }
    }
}
