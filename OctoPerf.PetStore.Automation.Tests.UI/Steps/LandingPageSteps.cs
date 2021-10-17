using Octoperf.PetStore.Automation.UI.Tests.Hooks;
using OctoPerf.PetStore.Automation.Framework.Base;
using OctoPerf.PetStore.Automation.Framework.Page.Objects;
using OctoPerf.PetStore.Automation.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OctoPerf.PetStore.Automation.Tests.UI.Steps
{
    [Binding]
    public sealed class LandingPageSteps
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public LandingPageSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this._featureContext = featureContext;
            this._scenarioContext = scenarioContext;
        }

        [Given("Application is launched")]
        public void GivenApplicationIsLaunched()
        {
            LandingPage landingPage = new LandingPage(DriverManager.Driver);
            landingPage.VerifyLandingPageIsLaunched();
            _scenarioContext["landingPage"] = landingPage;
        }

        [When("I click on Enter the Store link")]
        public void WhenIClickOnEnterTheStoreLink()
        {
            LandingPage landingPage = (LandingPage)_scenarioContext["landingPage"];
            landingPage.ClickEnterStoreLink();
        }
    }
}
