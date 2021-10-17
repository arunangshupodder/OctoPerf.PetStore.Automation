using FluentAssertions;
using OctoPerf.PetStore.Automation.Framework.Base;
using OctoPerf.PetStore.Automation.Framework.Objects;
using OctoPerf.PetStore.Automation.Framework.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPerf.PetStore.Automation.Framework.Page.Objects
{
    public partial class Login
    {
        public class UserRegistrationPage : BasePage
        {
            private By _userId = By.Name("username");
            private By _newPassword = By.Name("password");
            private By _repeatPassword = By.Name("repeatedPassword");
            private By _firstName = By.Name("account.firstName");
            private By _lastName = By.Name("account.lastName");
            private By _email = By.Name("account.email");
            private By _phone = By.Name("account.phone");
            private By _address1 = By.Name("account.address1");
            private By _address2 = By.Name("account.address2");
            private By _city = By.Name("account.city");
            private By _state = By.Name("account.state");
            private By _zip = By.Name("account.zip");
            private By _country = By.Name("account.country");
            private By _saveInfoButton = By.Name("newAccount");

            public UserRegistrationPage(IWebDriver driver) : base(driver) { }

            public User CreateNewUser()
            {
                User user = new User();
                user.FirstName = TestHelper.GetRandomString();
                user.LastName = TestHelper.GetRandomString();
                user.UserId = user.FirstName + "." + user.LastName;
                user.Password = TestHelper.GetRandomAlphaNumericString();
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

            public User RegisterNewUser()
            {
                User user = CreateNewUser();

                IsElementDisplayed(_userId).Should().BeTrue("User Id field should be displayed.");
                EnterText(_userId, user.UserId);
                IsElementDisplayed(_newPassword).Should().BeTrue("New Password field should be displayed.");
                EnterText(_newPassword, user.Password);
                IsElementDisplayed(_repeatPassword).Should().BeTrue("Repeat Password field should be displayed.");
                EnterText(_repeatPassword, user.Password);
                IsElementDisplayed(_firstName).Should().BeTrue("First Name field should be displayed.");
                EnterText(_firstName, user.FirstName);
                IsElementDisplayed(_lastName).Should().BeTrue("Last Name field should be displayed.");
                EnterText(_lastName, user.LastName);
                IsElementDisplayed(_email).Should().BeTrue("Email field should be displayed.");
                EnterText(_email, user.Email);
                IsElementDisplayed(_phone).Should().BeTrue("Phone Number field should be displayed.");
                EnterText(_phone, user.Phone);
                IsElementDisplayed(_address1).Should().BeTrue("Address 1 field should be displayed.");
                EnterText(_address1, user.Address1);
                IsElementDisplayed(_address2).Should().BeTrue("Address 2 field should be displayed.");
                EnterText(_address2, user.Address2);
                IsElementDisplayed(_city).Should().BeTrue("City field should be displayed.");
                EnterText(_city, user.City);
                IsElementDisplayed(_phone).Should().BeTrue("Phone Number field should be displayed.");
                EnterText(_phone, user.Phone);
                IsElementDisplayed(_state).Should().BeTrue("State field should be displayed.");
                EnterText(_state, user.State);
                IsElementDisplayed(_zip).Should().BeTrue("Zip Code field should be displayed.");
                EnterText(_zip, user.Zip);
                IsElementDisplayed(_country).Should().BeTrue("Country field should be displayed.");
                EnterText(_country, user.Country);

                IsElementClickable(_saveInfoButton).Should().BeTrue("Save Information button should be displayed.");
                Click(_saveInfoButton);

                return user;
            }
        }
    }

    
}
