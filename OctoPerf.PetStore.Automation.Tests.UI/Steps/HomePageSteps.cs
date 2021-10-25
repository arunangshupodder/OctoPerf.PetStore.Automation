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
    public sealed class HomePageSteps : CommonSteps
    {
        public HomePageSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
            : base(featureContext, scenarioContext) { }

        [Then("I land on the home page of the application")]
        public void ThenILandOnTheHomePage()
        {
            _homePage.VerifyNavigationToHomePage();
        }

        [Given("I click on Sign In")]
        public void GivenIClickOnSignIn()
        {
            _homePage.ClickOnSignIn();
        }
    }
}
