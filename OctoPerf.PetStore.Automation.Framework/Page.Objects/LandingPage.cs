using FluentAssertions;
using OctoPerf.PetStore.Automation.Framework.Base;
using OctoPerf.PetStore.Automation.Framework.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPerf.PetStore.Automation.Framework.Page.Objects
{
    public class LandingPage : BasePage
    {
        By welcomeBanner = By.XPath("//h2[text()='Welcome to JPetStore 6']");
        By enterStoreLink = By.XPath("//a[text()='Enter the Store']");

        public LandingPage(IWebDriver driver) : base(driver) {}

        public void VerifyLandingPageIsLaunched()
        {
            IsElementDisplayed(welcomeBanner).Should().BeTrue("Welcome Banner should be displayed.");
        }

        public void ClickEnterStoreLink()
        {
            IsElementClickable(enterStoreLink).Should().BeTrue("Enter Store link should be displayed and clickable.");
            Click(enterStoreLink);
        }
    }
}
