using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNet
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloCollection helloCollection = new HelloCollection();
            foreach (var item in helloCollection)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }

    #region 指针

    class PointerClass
    {
       unsafe static void Pointer(string[] args)
        {
            int a = 32;
            int b = 33;
            int* p1 = &a;
            int** p2 = &p1;
            Console.WriteLine($"a的值是{(int)*p1}");
            Console.WriteLine($"a的地址==p是{(int)&a}");
            Console.WriteLine($"p1的值是{(int)*p2}");
            Console.WriteLine($"p2的地址是{(int)p2}");


            Console.WriteLine($"a的值时{*p1}");

            Console.WriteLine($"a的地址是{(int)p1}");



            Console.Read();
        }
    }

    #endregion

    #region 迭代器


    public class HelloCollection : IEnumerable
    {

        int[] array = new int[10];


        public HelloCollection()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
        }


        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < array.Length; i++)
            {
                yield return array[i];

            }

        }
    }

    //public class HelloCollection : IEnumerable
    //{
    //    public IEnumerator GetEnumerator()
    //    {
    //        Enumerator enumerator = new Enumerator(0);

    //        return enumerator;
    //    }


    //    public class Enumerator : IEnumerator, IDisposable
    //    {
    //        private int State;
    //        private object current;
    //        public object Current
    //        {
    //            get
    //            {
    //                return current;
    //            }
    //        }

    //        public void Dispose()
    //        {
    //            //throw new NotImplementedException();
    //        }

    //        public bool MoveNext()
    //        {
    //            switch (State)
    //            {
    //                case 0:
    //                    current = "Hello";
    //                    State = 1;
    //                    return true;

    //                case 1:
    //                    current = "World";
    //                    State = 2;
    //                    return true;

    //                case 2:
    //                    break;
    //            }

    //            return false;
    //        }

    //        public void Reset()
    //        {
    //            throw new NotImplementedException();
    //        }





    //        public Enumerator(int state)
    //        {
    //            this.State = state;


    //        }
    //    }




    //}


    public class A : IEnumerable
    {
        private int[] array = new int[10];

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
                yield return array[i];
            }
            
        }
    }


    #endregion

}
