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
            AssertTrue(IsElementDisplayed(welcomeBanner), "Welcome Banner should be displayed.",
                "Landing Page is launched successfully.");
        }

        public void ClickEnterStoreLink()
        {
            AssertTrue(IsElementClickable(enterStoreLink), "Enter Store link should be displayed and clickable.");
            Click(enterStoreLink);
        }
    }
}
