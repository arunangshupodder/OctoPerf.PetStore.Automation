using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OctoPerf.PetStore.Automation.Framework.Utilities
{
    public class ReportManager
    {
        private static readonly Lazy<AventStack.ExtentReports.ExtentReports> _extentReport = new Lazy<AventStack.ExtentReports.ExtentReports>();
        private static readonly object lockObj = new object();
        private static readonly object resultLockObj = new object();
        
        private static readonly string ReportingDirectory = $@"{Directory.GetCurrentDirectory()}\Reports";
        private static readonly string ScreenshotsFolder = $@"{ReportingDirectory}\Screenshots";
        private static readonly string IndexReportFilePath = $@"{ReportingDirectory}\AutomationReport.html";
        private static readonly string DashboardReportFilePath = $@"{ReportingDirectory}\Dashboard.html";

        private static AventStack.ExtentReports.ExtentReports Global_Extent => _extentReport.Value;

        [ThreadStatic]
        public static Screenshot ScreenCapture;

        [ThreadStatic]
        public static TestContext CurrentTestContext;

        [ThreadStatic]
        public static FeatureContext CurrentFeatureContext;

        [ThreadStatic]
        public static ScenarioContext CurrentScenarioContext;

        public static void InitializeReport()
        {
            if (!Directory.Exists(ReportingDirectory)) Directory.CreateDirectory(ReportingDirectory);
            if (Directory.Exists(ScreenshotsFolder)) Directory.Delete(ScreenshotsFolder, true);
            Directory.CreateDirectory(ScreenshotsFolder);
            var htmlReporter = new ExtentV3HtmlReporter(IndexReportFilePath);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            Global_Extent.AttachReporter(htmlReporter);
        }

        public static void CreateCurrentFeature(FeatureContext featureContext)
        {
            lock (lockObj)
            {
                featureContext["Feature"] = Global_Extent.CreateTest<Feature>("Feature: " + featureContext.FeatureInfo.Title);
                CurrentFeatureContext = featureContext;
            }
        }

        public static void CreateCurrentScenario(ScenarioContext scenarioContext)
        {
            lock (lockObj)
            {
                scenarioContext["Scenario"] = CurrentFeatureContext.Get<ExtentTest>("Feature").CreateNode<Scenario>($"Scenario :: {scenarioContext.ScenarioInfo.Title}");
                CurrentScenarioContext = scenarioContext;
                ReportManager.ScreenCapture = null;
            }
        }

        public static void CreateCurrentScenarioStep(ScenarioContext scenarioContext)
        {
            // create dynamic step name
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            switch(stepType)
            {
                case "Given":
                    scenarioContext["CurrentStep"] = scenarioContext.Get<ExtentTest>("Scenario")
                            .CreateNode<Given>($"Given :: {scenarioContext.StepContext.StepInfo.Text}");
                    break;
                case "When":
                    scenarioContext["CurrentStep"] = scenarioContext.Get<ExtentTest>("Scenario")
                            .CreateNode<When>($"When :: {scenarioContext.StepContext.StepInfo.Text}");
                    break;
                case "Then":
                    scenarioContext["CurrentStep"] = scenarioContext.Get<ExtentTest>("Scenario")
                            .CreateNode<Then>($"Then :: {scenarioContext.StepContext.StepInfo.Text}");
                    break;
                case "And":
                    scenarioContext["CurrentStep"] = scenarioContext.Get<ExtentTest>("Scenario")
                            .CreateNode<And>($"And :: {scenarioContext.StepContext.StepInfo.Text}");
                    break;
                case "But":
                    scenarioContext["CurrentStep"] = scenarioContext.Get<ExtentTest>("Scenario")
                            .CreateNode<But>($"But :: {scenarioContext.StepContext.StepInfo.Text}");
                    break;
            }
            CurrentScenarioContext = scenarioContext;
            if (scenarioContext.TestError != null) UpdateResult(scenarioContext);
        }

        public static Dictionary<string, string> UpdateResult(ScenarioContext scenarioContext)
        {
            lock (resultLockObj)
            {
                var status = scenarioContext.TestError != null ? Status.Fail : Status.Pass;
                var resultFiles = new Dictionary<string, string>();
                string[] excludeExceptions = { "Unable to connect to the remote server", 
                                               "No connection could be made because the target machine actively refused it",
                                               "An existing connection was forcibly closed by the remote host",
                                               "An error occurred while sending the request" };
                switch (status)
                {
                    case Status.Fail:
                        var errorMessage = 
                            $"Test execution completed with status as {Status.Fail} :: {scenarioContext.TestError.Message} </br> </br> StackTrace: </br> {scenarioContext.TestError.StackTrace}";
                        if (excludeExceptions.Where(ex => errorMessage.Contains(ex)).Count().Equals(0))
                        {
                            if (ScreenCapture == null) ScreenCapture = CaptureScreenshot();
                            CurrentScenarioContext.Get<ExtentTest>("CurrentStep")
                                .Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(ReportManager.ScreenCapture.AsBase64EncodedString).Build());
                            resultFiles.Add("ErrorScreenshot", SaveScreenshot(ScreenCapture));
                        }
                        else
                        {
                            CurrentScenarioContext.Get<ExtentTest>("CurrentStep").Fail(errorMessage);
                        }
                        break;
                    default:
                        LogStepDetails("Test Execution completed successfully");
                        break;
                }
                return resultFiles;
            }
        }

        public static void LogStepDetails(string details, Status status = Status.Pass)
        {
            LogStepDetails(details, status, CurrentScenarioContext.Get<ExtentTest>("CurrentStep"));
        }

        private static void LogStepDetails(string details, Status status = Status.Pass, params ExtentTest[] currentSteps)
        {
            lock (lockObj)
            {
                foreach (var currentStep in currentSteps)
                {
                    currentStep.Log(status, $"[{DateTime.Now.ToString("F")}] ==> {details}");
                    CurrentTestContext.WriteLine($"[{DateTime.Now.ToString("F")}] ==> {details}");
                }
            }
        }

        private static string CaptureScreenshotAsBase64EncodedString()
        {
            ITakesScreenshot ts = (ITakesScreenshot)DriverManager.Driver;
            Screenshot screenshot = ts.GetScreenshot();
            return screenshot.AsBase64EncodedString;
        }

        private static Screenshot CaptureScreenshot()
        {
            ITakesScreenshot ts = (ITakesScreenshot)DriverManager.Driver;
            return ts.GetScreenshot();
        }

        private static string SaveScreenshot(Screenshot screenshot, string screenshotName = "ScreenCapture")
        {
            if (!Directory.Exists(ScreenshotsFolder)) Directory.CreateDirectory(ScreenshotsFolder);
            string finalPath = $@"{ScreenshotsFolder}\{screenshotName}{DateTime.Now.Ticks}.png";
            string screenshotPath = new Uri(finalPath).LocalPath;
            screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }

        public static void PublishReport()
        {
            Global_Extent.Flush();
        }
    }
}
