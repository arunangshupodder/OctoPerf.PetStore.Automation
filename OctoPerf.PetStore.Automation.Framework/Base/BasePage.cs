using OctoPerf.PetStore.Automation.Framework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static OctoPerf.PetStore.Automation.Framework.Data.Data;

namespace OctoPerf.PetStore.Automation.Framework.Base
{
    public class BasePage
    {
        protected IWebDriver Driver;
        private WebDriverWait _wait;
        private DefaultWait<IWebDriver> _fluentWait;
        private IJavaScriptExecutor jsExecutor;

        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;

            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME));
            _wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            _wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            _fluentWait = new DefaultWait<IWebDriver>(driver);
            _fluentWait.Timeout = TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME);
            _fluentWait.PollingInterval = TimeSpan.FromSeconds(Constants.WAIT_LOW);

            jsExecutor = (IJavaScriptExecutor)driver;
        }

        /* Wait Utils */
        public bool IsElementDisplayed(By locator, TimeSpan? timeOut = null)
        {
            return CheckoForElementVisibility(locator, timeOut);
        }

        public bool CheckoForElementVisibility(By locator, TimeSpan? timeOut = null)
        {
            bool isElementFound;
            try
            {
                if (timeOut != null) _wait.Timeout = timeOut ?? TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME);
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
                isElementFound = true;
            }
            catch
            {
                isElementFound = false;
            }
            return isElementFound;
        }

        public bool IsElementClickable(By locator, TimeSpan? timeOut = null)
        {
            return CheckoForElementVisibility(locator, timeOut) && 
                CheckoForElementClickability(locator, timeOut);
        }

        public bool CheckoForElementClickability(By locator, TimeSpan? timeOut = null)
        {
            bool isElementFound;
            try
            {
                if (timeOut != null) _wait.Timeout = timeOut ?? TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME);
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                isElementFound = true;
            }
            catch
            {
                isElementFound = false;
            }
            return isElementFound;
        }

        public bool DoesURLContainPath(string urlPath)
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(urlPath));
        }



        /* Element Utils */
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
            jsExecutor.ExecuteScript(javascript, GetElement(locator));
        }

        public void EnterText(By locator, string text)
        {
            GetElement(locator).Clear();
            GetElement(locator).SendKeys(text);
        }

        public void LogPass(string message)
        {
            ReportManager.LogStepDetails(message);
        }

        public void LogFail(string message)
        {
            ReportManager.LogStepDetails(message, AventStack.ExtentReports.Status.Fail);
        }

        public void LogSkip(string message)
        {
            ReportManager.LogStepDetails(message, AventStack.ExtentReports.Status.Skip);
        }
    }
}
