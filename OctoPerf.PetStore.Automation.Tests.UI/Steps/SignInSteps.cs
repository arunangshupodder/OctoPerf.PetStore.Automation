using OctoPerf.PetStore.Automation.Framework.Objects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OctoPerf.PetStore.Automation.Tests.UI.Steps
{
    [Binding]
    public sealed class SignInSteps : CommonSteps
    {
        public SignInSteps(FeatureContext featureContext, ScenarioContext scenarioContext, IWebDriver driver)
            : base(featureContext, scenarioContext, driver) { }

        [Given("I login to application as (.*) user")]
        [When("I login to application as (.*) user")]
        public void GivenILoginAsANewOrExistingUser(string userType)
        {
            _signInPage.ValidateUserNavigatedToSignInPage();
            User user = null;
            if (userType.ToLower().Equals("new"))
            {
                _signInPage.NavigateToUserRegistrationPage();
                user = _regnPage.RegisterNewUser();
                _homePage.ClickOnSignIn();
                _signInPage.ValidateUserNavigatedToSignInPage();
                user = _signInPage.Login(userType, user);
            }
            else
            {
                user = _signInPage.Login(userType);
            }
            _scenarioContext["user"] = user;
        }

        [When("I register a new user")]
        public void WhenIRegisterANewUser()
        {
            _signInPage.ValidateUserNavigatedToSignInPage();
            _signInPage.NavigateToUserRegistrationPage();
            User user = _regnPage.RegisterNewUser();
            _scenarioContext["user"] = user;
        }

        [Then("I should be navigated to Home Page")]
        public void ThenINavigatedToHomePage()
        {
            _homePage.VerifyNavigationToHomePage();
        }

        [Then("I should be able to login to application")]
        public void ThenILoginInToApplication()
        {
            _homePage.ClickOnSignIn();
            _signInPage.ValidateUserNavigatedToSignInPage();
            _signInPage.Login("existing", (User)_scenarioContext["user"]);
        }
    }
}
