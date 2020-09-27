using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteObject;
namespace RemoteClient
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            RemoteObject.MyObject app = (RemoteObject.MyObject)Activator.GetObject(typeof(RemoteObject.MyObject), System.Configuration.ConfigurationSettings.AppSettings["ServiceURL"]);
            Console.WriteLine(app.Add(1, 2));
            Console.ReadLine();
        }
    }
}
