using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace CPU_Usage_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                using (PerformanceCounter pcProcess = new PerformanceCounter("Process", "% Processor Time", proc.ProcessName))
                {
                    pcProcess.NextValue();
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("Process:{0} CPU% {1}", proc.ProcessName, pcProcess.NextValue());
                }
            }
        }
    }
}
