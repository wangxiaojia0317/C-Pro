using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_Generate
{
    class Program
    {
        static void Main(string[] args)
        {
            rational a1 = new rational(2);
            rational a2 = new rational(3);
            Console.WriteLine((a1 + a2)); ;
            Console.Read();

        }
        private static object lockobj = new object(); // pangpeng


        private static string GetChar(int ppar)
        {
            if (ppar <= 9 && ppar >= 0)
            {
                return ppar.ToString();
            }
            else if (ppar >= 10 && ppar < 36)
            {
                return Convert.ToChar(ppar - 10 + 65).ToString();
            }
            else if (ppar >= 36 && ppar <= 60)
            {
                return Convert.ToChar(ppar - 36 + 97).ToString();
            }
            else
            {
                //  Compact.Assert(false);
                return null;
            }
        }

        static string correntTimeStr = null;


        static int ID = 0;
        public static string GetID()
        {
            // 多任务操作涉及id重复
            // 如果可以改为原子操作
            // 2018-05-11
            lock (lockobj)
            {
                string partID = null;
                partID += GetChar(DateTime.Now.Year - 2017);
                partID += GetChar(DateTime.Now.Month);
                partID += GetChar(DateTime.Now.Day);
                partID += GetChar(DateTime.Now.Hour);
                partID += GetChar(DateTime.Now.Minute);
                partID += GetChar(DateTime.Now.Second);
                partID += string.Format("{0:000}", DateTime.Now.Millisecond);
                if (partID.Equals(correntTimeStr))
                {
                    ID++;
                    if (ID > 999)
                    {
                        // Compact.Assert(false);
                    }
                }
                else
                {
                    ID = 0;
                    correntTimeStr = partID;
                }
                partID += string.Format("{0:000}", ID); ;
                return partID;
            }
        }





    }


    public sealed class rational
    {
        private int _value = 0;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public rational(int value)
        {
            this._value = value;
        }

        public rational()
        {

        }
        public static int operator +(rational num1, rational num2)
        {
            rational result = new rational(num1.Value + num2.Value);
            return result.Value;
        }

    }


}
