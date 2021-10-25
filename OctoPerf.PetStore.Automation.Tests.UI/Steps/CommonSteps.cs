using OctoPerf.PetStore.Automation.Framework.Page.Objects;
using OctoPerf.PetStore.Automation.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using static OctoPerf.PetStore.Automation.Framework.Page.Objects.Login;

namespace OctoPerf.PetStore.Automation.Tests.UI.Steps
{
    [Binding]
    public abstract class CommonSteps
    {
        protected readonly FeatureContext _featureContext;
        protected readonly ScenarioContext _scenarioContext;
        protected readonly LandingPage _landingPage;
        protected readonly SignInPage _signInPage;
        protected readonly UserRegistrationPage _regnPage;
        protected readonly HomePage _homePage;
        protected readonly CheckoutPage _checkoutPage;

        public CommonSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this._featureContext = featureContext;
            this._scenarioContext = scenarioContext;
            this._landingPage = new LandingPage(DriverManager.Driver);
            this._signInPage = new SignInPage(DriverManager.Driver);
            this._homePage = new HomePage(DriverManager.Driver);
            this._regnPage = new UserRegistrationPage(DriverManager.Driver);
            this._checkoutPage = new CheckoutPage(DriverManager.Driver);
        }
    }
}
