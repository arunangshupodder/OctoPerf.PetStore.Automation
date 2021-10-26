using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using static OctoPerf.PetStore.Automation.Framework.Data.Data;

namespace OctoPerf.PetStore.Automation.Framework.Base
{
    public partial class BasePage
    {
        protected IWebDriver Driver;
        private WebDriverWait _wait;
        private DefaultWait<IWebDriver> _fluentWait;
        private IJavaScriptExecutor _jsExecutor;
        private static List<string> _assertList;

        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;

            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME));
            _wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            _wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            _fluentWait = new DefaultWait<IWebDriver>(driver);
            _fluentWait.Timeout = TimeSpan.FromSeconds(Constants.DEFAULT_WAIT_TIME);
            _fluentWait.PollingInterval = TimeSpan.FromSeconds(Constants.WAIT_LOW);

            _jsExecutor = (IJavaScriptExecutor)driver;
        }
    }
}
