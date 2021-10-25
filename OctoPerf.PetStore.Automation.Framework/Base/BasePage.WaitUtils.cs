using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using static OctoPerf.PetStore.Automation.Framework.Data.Data;

namespace OctoPerf.PetStore.Automation.Framework.Base
{
    public partial class BasePage
    {
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
    }
}
