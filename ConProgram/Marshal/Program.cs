﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marshal
{
    public struct Point
    {
        public Int32 x, y;
    }


    public sealed class App
    {
        static void Main()
        {
            // Demonstrate the use of public static fields of the Marshal class.
            Console.WriteLine("SystemDefaultCharSize={0}, SystemMaxDBCSCharSize={1}",
                Marshal.SystemDefaultCharSize, Marshal.SystemMaxDBCSCharSize);

            // Demonstrate the use of the SizeOf method of the Marshal class.
            Console.WriteLine("Number of bytes needed by a Point object: {0}",
                Marshal.SizeOf(typeof(Point)));
            Point p = new Point();
            Console.WriteLine("Number of bytes needed by a Point object: {0}",
                Marshal.SizeOf(p));

            // Demonstrate how to call GlobalAlloc and 
            // GlobalFree using the Marshal class.
            IntPtr hglobal = Marshal.AllocHGlobal(100);
            Marshal.FreeHGlobal(hglobal);

            // Demonstrate how to use the Marshal class to get the Win32 error 
            // code when a Win32 method fails.
            Boolean f = CloseHandle(new IntPtr(-1));
            if (!f)
            {
                Console.WriteLine("CloseHandle call failed with an error code of: {0}",
                    Marshal.GetLastWin32Error());
            }
        }

        // This is a platform invoke prototype. SetLastError is true, which allows 
        // the GetLastWin32Error method of the Marshal class to work correctly.    
        [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
        static extern Boolean CloseHandle(IntPtr h);

    }


}
