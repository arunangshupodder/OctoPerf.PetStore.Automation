using FluentAssertions;
using OctoPerf.PetStore.Automation.Framework.Base;
using OctoPerf.PetStore.Automation.Framework.Objects;
using OctoPerf.PetStore.Automation.Framework.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using static OctoPerf.PetStore.Automation.Framework.Data.Data;

namespace OctoPerf.PetStore.Automation.Framework.Page.Objects
{
    public partial class Login
    {
        public class SignInPage : BasePage
        {
            private By _registerNowLink = By.XPath("//a[contains(text(), 'Register Now')]");
            private By _signInPageMessage = By.XPath("//p[text()='Please enter your username and password.']");
            private By _username = By.Name("username");
            private By _password = By.Name("password");
            private By _signInButton = By.Name("signon");
            private readonly string URL = "/actions/Account.action";

            public SignInPage(IWebDriver driver) : base(driver) { }

            public User SetDefaultUser()
            {
                User user = new User();
                user.FirstName = Config.GetUserFirstName();
                user.LastName = Config.GetUserLastName();
                user.UserId = Config.GetUserId();
                user.Password = Config.GetUserPassword();
                user.Email = user.UserId + "@gmail.com";
                user.Phone = "+91 1234567890";
                user.Address1 = "2nd floor, Shining Residency";
                user.Address2 = "123, Andheri Street";
                user.City = "Mumbai";
                user.State = "Maharashtra";
                user.Zip = "456345";
                user.Country = "India";

                return user;
            }

            public void ValidateUserNavigatedToSignInPage()
            {
                (DoesURLContainPath(URL) && IsElementDisplayed(_signInPageMessage))
                    .Should().BeTrue("User should be navigated to Sign In page.");
            }

            public void NavigateToUserRegistrationPage()
            {
                IsElementClickable(_registerNowLink).Should().BeTrue("Register Now link should be displayed.");
                Click(_registerNowLink);
            }

            public User Login(string userType, User user=null)
            {
                if (userType.Equals("existing") && user==null) user = SetDefaultUser();

                IsElementDisplayed(_username).Should().BeTrue("Username field should be displayed.");
                EnterText(_username, user.UserId);
                IsElementDisplayed(_password).Should().BeTrue("Password field should be displayed.");
                EnterText(_password, user.Password);
                IsElementClickable(_signInButton).Should().BeTrue("Sign In button should be displayed.");
                Click(_signInButton);

                return user;
            }
        }
    }
}
