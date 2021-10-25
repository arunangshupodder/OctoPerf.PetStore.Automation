using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OctoPerf.PetStore.Automation.Tests.UI.Steps
{
    [Binding]
    public class LandingPageSteps : CommonSteps
    {
        public LandingPageSteps(FeatureContext featureContext, ScenarioContext scenarioContext) 
            : base(featureContext, scenarioContext) { }

        [Given("Application is launched")]
        public void GivenApplicationIsLaunched()
        {
            _landingPage.VerifyLandingPageIsLaunched();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        [When("I click on Enter the Store link")]
        public void WhenIClickOnEnterTheStoreLink()
        {
            _landingPage.ClickEnterStoreLink();
        }
    }
}
