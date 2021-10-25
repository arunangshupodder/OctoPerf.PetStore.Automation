using OctoPerf.PetStore.Automation.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPerf.PetStore.Automation.Framework.Base
{
    public partial class BasePage
    {
        public void LogPass(string message)
        {
            ReportManager.LogStepDetails(message);
        }

        public void LogFail(string message)
        {
            ReportManager.LogStepDetails(message, AventStack.ExtentReports.Status.Fail);
        }

        public void LogSkip(string message)
        {
            ReportManager.LogStepDetails(message, AventStack.ExtentReports.Status.Skip);
        }

        public void LogInfo(string message)
        {
            ReportManager.LogStepDetails(message, AventStack.ExtentReports.Status.Info);
        }
    }
}
