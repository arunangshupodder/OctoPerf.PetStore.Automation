using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static OctoPerf.PetStore.Automation.Framework.Data.Data;

namespace OctoPerf.PetStore.Automation.Framework.Base
{
    public partial class BasePage
    {
        public IWebElement GetElement(By locator)
        {
            return GetElement(locator, TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME));
        }

        public IWebElement GetElement(By locator, TimeSpan timeOut)
        {
            _wait.Timeout = timeOut;
            return _wait.Until(driver => driver.FindElement(locator));
        }

        public ReadOnlyCollection<IWebElement> GetElements(By locator)
        {
            return GetElements(locator, TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME));
        }

        public ReadOnlyCollection<IWebElement> GetElements(By locator, TimeSpan timeOut)
        {
            _wait.Timeout = timeOut;
            return _wait.Until(driver => driver.FindElements(locator));
        }

        public void Click(By locator)
        {
            try
            {
                GetElement(locator).Click();
            }
            catch (ElementClickInterceptedException)
            {
                JavascriptClick(locator);
            }
        }

        public void JavascriptClick(By locator)
        {
            String javascript = "arguments[0]. click()";
            _jsExecutor.ExecuteScript(javascript, GetElement(locator));
        }

        public void EnterText(By locator, string text)
        {
            GetElement(locator).Clear();
            GetElement(locator).SendKeys(text);
        }
    }
}
