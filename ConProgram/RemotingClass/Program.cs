using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingClass
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }





    public class RemotableType : MarshalByRefObject
    {
        private string _internalString = "This is the RemotableType";

        public string StringMethod()
        {
            return _internalString;
        }
    }


}
