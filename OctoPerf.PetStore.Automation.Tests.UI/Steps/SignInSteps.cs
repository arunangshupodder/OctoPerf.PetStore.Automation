using OctoPerf.PetStore.Automation.Framework.Objects;
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
    public sealed class SignInSteps
    {

        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public SignInSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this._featureContext = featureContext;
            this._scenarioContext = scenarioContext;
        }

        [Given("I login to application as (.*) user")]
        [When("I login to application as (.*) user")]
        public void GivenILoginAsANewOrExistingUser(string userType)
        {
            SignInPage signInPage = new SignInPage(DriverManager.Driver);
            signInPage.ValidateUserNavigatedToSignInPage();

            User user = null;
            if (userType.ToLower().Equals("new"))
            {
                signInPage.NavigateToUserRegistrationPage();
                UserRegistrationPage regPage = new UserRegistrationPage(DriverManager.Driver);
                user = regPage.RegisterNewUser();

                ((HomePage)_scenarioContext["homePage"]).ClickOnSignIn();
                signInPage.ValidateUserNavigatedToSignInPage();
                
                user = signInPage.Login(userType, user);
            }
            else
            {
                user = signInPage.Login(userType);
            }
        }

        [When("I register a new user")]
        public void WhenIRegisterANewUser()
        {
            SignInPage signInPage = new SignInPage(DriverManager.Driver);
            signInPage.ValidateUserNavigatedToSignInPage();
            signInPage.NavigateToUserRegistrationPage();
            
            UserRegistrationPage regPage = new UserRegistrationPage(DriverManager.Driver);
            User user = regPage.RegisterNewUser();
        }
    }
}
