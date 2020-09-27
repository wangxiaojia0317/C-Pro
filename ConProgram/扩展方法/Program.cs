
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 扩展方法
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    /// <summary>
    /// 扩展方法必须是引用类型
    /// </summary>
    public static class ExtensionHelper
    {
        //collection extension
        public static void AddIfNotExist<T>(this List<T> list, T obj) where T : class
        {
            if (!list.Contains(obj))
            {
                list.Add(obj);
            }
        }
    }

}
