﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CPU内存监控工具
{
    class Program
    {
        private static void Say(string txt)
        {
            Console.WriteLine(txt);
        } // auxiliary print methods
        private static void Say()
        {
            Say("");
        } // The main method. Command line arguments are ignored.

        public static void Main()
        {
            Say("$Id: CpuLoadInfo.cs,v 1.2 2002/08/17 17:45:48 rz65 Exp $");
            Say();
            Say("Attempt to create a PerformanceCounter instance:");
            Say("Category name = " + CategoryName);
            Say("Counter name = " + CounterName);
            Say("Instance name = " + InstanceName); PerformanceCounter pc = new PerformanceCounter(CategoryName, CounterName, InstanceName);
            Say("Performance counter was created.");
            Say("Property CounterType: " + pc.CounterType);
            Say();
            Say("Property CounterHelp: " + pc.CounterHelp);
            Say();
            Say("Entering measurement loop.");

            while (true)
            {
                Thread.Sleep(1000); // wait for 1 second
                float cpuLoad = pc.NextValue();
                Say("CPU load = " + cpuLoad + " %.");
            }
        } // constants used to select the performance counter.
        private const string CategoryName = "Processor";
        private const string CounterName = "% Processor Time";
        private const string InstanceName = "_Total";
    }

    
}
