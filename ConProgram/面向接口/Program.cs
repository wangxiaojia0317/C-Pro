
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 面向接口
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();

            for (int i = 0; i < 10; i++)
            {
                List<int> l = new List<int>();
                for (int j = 0; j < 10; j++)
                {
                    l.Add(j);
                }

                dic.Add(i,l);
            }



            IEnumerable<Dictionary<int, List<int>>> s = GetInfo_Fun(dic);
            Console.WriteLine(s.Max());
            
            Console.Read();
        }




        public static IEnumerable<Dictionary<int, List<int>>> GetInfo_Fun(Dictionary<int, List<int>> dic)
        {
            var woodInfos = from item in dic
                            where item.Value.Count>5
                            select item;
            return woodInfos as IEnumerable<Dictionary<int, List<int>>>;
        }

    }
  
}
