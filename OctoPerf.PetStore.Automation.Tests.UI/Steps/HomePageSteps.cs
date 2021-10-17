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
    public sealed class HomePageSteps
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public HomePageSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this._featureContext = featureContext;
            this._scenarioContext = scenarioContext;
        }

        [Then("I land on the home page of the application")]
        public void ThenILandOnTheHomePage()
        {
            HomePage homePage = new HomePage(DriverManager.Driver);
            homePage.VerifyNavigationToHomePage();
            _scenarioContext["homePage"] = homePage;
        }

        [Given("I click on Sign In")]
        public void GivenIClickOnSignIn()
        {
            ((HomePage)_scenarioContext["homePage"]).ClickOnSignIn();
        }
    }
}
