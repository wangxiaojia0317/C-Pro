using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFTest
{
    class App 
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            MainWindow win = new MainWindow();
            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            app.Run(win);


            app.Activated += app_Activated;

            app.Exit += app_Exit;



        }

        private static void app_Exit(object sender, ExitEventArgs e)
        {
            
        }

        private static void app_Activated(object sender, EventArgs e)
        {
            
        }
    }
}
