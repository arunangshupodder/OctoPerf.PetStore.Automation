using BoDi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoPerf.PetStore.Automation.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using static OctoPerf.PetStore.Automation.Framework.Data.Data;

namespace Octoperf.PetStore.Automation.UI.Tests.Hooks
{
    [TestClass]
    [Binding]
    public sealed class Hooks
    {
        [ThreadStatic]
        private readonly IObjectContainer _objectContainer;

        [ThreadStatic]
        private ScenarioContext _scenarioContext;

        [ThreadStatic]
        private static TestContext _testContext;

        public TestContext CurrentTestContext
        {
            get
            {
                return _testContext;
            }

            set
            {
                _testContext = value;
            }
        }

        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext, TestContext testContext)
        {
            this._objectContainer = objectContainer;
            this._scenarioContext = scenarioContext;
            CurrentTestContext = testContext;
            ReportManager.CurrentTestContext = testContext;
        }

        [BeforeTestRun]
        public static void SuiteSetup()
        {
            ReportManager.InitializeReport();
        }

        [BeforeFeature]
        public static void FeatureSetup(FeatureContext featureContext)
        {
            ReportManager.CreateCurrentFeature(featureContext);
        }

        [BeforeScenario("UITests")]
        public void TestSetup()
        {
            TestHelper.CurrentTags = this._scenarioContext.ScenarioInfo.Tags;

            if (TestHelper.CurrentTags.Contains("Ignore"))
            {
                Assert.Inconclusive($"Scenario: '{this._scenarioContext.ScenarioInfo.Title}' is ignored.");
            }

            if (Config.GetConfigData("Browser").ToLower().Contains("chrome"))
            {
                DriverManager.InitiateDriver(BrowserType.Chrome);
            } 
            else if (Config.GetConfigData("Browser").ToLower().Contains("firefox"))
            {
                DriverManager.InitiateDriver(BrowserType.Firefox);
            }

            _objectContainer.RegisterInstanceAs(DriverManager.Driver);
            DriverManager.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME);
            DriverManager.Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME);
            DriverManager.Driver.Navigate().GoToUrl(Config.GetConfigData("JPetStore.URL"));
            
            ReportManager.CreateCurrentScenario(this._scenarioContext);
        }

        [BeforeStep]
        public void StepSetup()
        {
            ReportManager.CreateCurrentScenarioStep(this._scenarioContext);
        }

        [AfterStep]
        public void StepTeardown()
        {
            
        }

        [AfterScenario("UITests")]
        public void TestTearDown()
        {
            var resultFiles = ReportManager.UpdateResult(this._scenarioContext);
            foreach (var resultFile in resultFiles) CurrentTestContext.AddResultFile(resultFile.Value);
            DriverManager.CloseDriver();
            if (Config.GetConfigData("Browser").ToLower().Contains("chrome")) ScriptExecutor.KillDriver(BrowserType.Chrome);
        }

        [AfterFeature]
        public static void FeatureTeardown(FeatureContext featureContext)
        {
            
        }

        [AfterTestRun]
        public static void SuiteTearDown()
        {
            ReportManager.PublishReport();
        }
    }
}
