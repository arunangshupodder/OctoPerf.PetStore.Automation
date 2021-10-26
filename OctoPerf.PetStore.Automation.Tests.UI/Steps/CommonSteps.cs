using OctoPerf.PetStore.Automation.Framework.Page.Objects;
using OctoPerf.PetStore.Automation.Framework.Utilities;
using OpenQA.Selenium;
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
        protected readonly IWebDriver Driver;

        public CommonSteps(FeatureContext featureContext, ScenarioContext scenarioContext, IWebDriver driver)
        {
            this.Driver = driver;
            this._featureContext = featureContext;
            this._scenarioContext = scenarioContext;
            this._landingPage = new LandingPage(driver);
            this._signInPage = new SignInPage(driver);
            this._homePage = new HomePage(driver);
            this._regnPage = new UserRegistrationPage(driver);
            this._checkoutPage = new CheckoutPage(driver);
        }
    }
}
