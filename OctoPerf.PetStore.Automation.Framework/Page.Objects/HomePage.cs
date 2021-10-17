using FluentAssertions;
using OctoPerf.PetStore.Automation.Framework.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPerf.PetStore.Automation.Framework.Page.Objects
{
    public class HomePage : BasePage
    {
        private By _signInLink = By.XPath("//a[text()='Sign In']");

        private readonly string URL = "/actions/Catalog.action";
        public HomePage(IWebDriver driver) : base(driver) { }

        public void VerifyNavigationToHomePage()
        {
            (DoesURLContainPath(URL) && IsElementDisplayed(_signInLink))
                .Should().BeTrue("User should have been navigated to the Home Page.");
        }

        public void ClickOnSignIn()
        {
            IsElementDisplayed(_signInLink).Should().BeTrue("Sign In link should be displayed.");
            GetElement(_signInLink).Click();
        }
    }
}
