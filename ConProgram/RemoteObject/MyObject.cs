using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RemoteObject
{
    public class MyObject:MarshalByRefObject
    {
        public int Add(int a, int b) => a + b;

    }
}
