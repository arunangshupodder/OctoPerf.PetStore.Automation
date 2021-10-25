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
        private readonly LandingPage _landingPage;

        public LandingPageSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this._featureContext = featureContext;
            this._scenarioContext = scenarioContext;
            this._landingPage = new LandingPage(DriverManager.Driver);
        }

        [Given("Application is launched")]
        public void GivenApplicationIsLaunched()
        {
            _landingPage.VerifyLandingPageIsLaunched();
        }

        [When("I click on Enter the Store link")]
        public void WhenIClickOnEnterTheStoreLink()
        {
            _landingPage.ClickEnterStoreLink();
        }
    }
}
