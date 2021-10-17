using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace OctoPerf.PetStore.Automation.Framework.Utilities
{
    public class ScriptExecutor
    {
        public static void KillDriver(BrowserType browserType)
        {
            /*
            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + @"\Resources";
            proc.StartInfo.FileName = "KillTask_ChromeDriver.bat";
            proc.StartInfo.CreateNoWindow = false;
            Console.WriteLine("File to be executed: " + proc.StartInfo.WorkingDirectory + @"\" + proc.StartInfo.FileName);
            bool result = proc.Start(proc.StartInfo.WorkingDirectory + @"\" + proc.StartInfo.FileName);
            proc.WaitForExit();
            if (result) Console.WriteLine("Kill Driver executed!");
            else Console.WriteLine("Kill Driver not executed!");
            */
        }
    }
}
