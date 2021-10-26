using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPerf.PetStore.Automation.Framework.Utilities
{
    public class DriverManager
    {
        [ThreadStatic]
        private static RemoteWebDriver _driver;
        
        public static RemoteWebDriver Driver
        {
            get
            {
                return _driver;
            }
            set
            {
                _driver = value;
            }
        }

        public static RemoteWebDriver InitiateDriver(BrowserType browserType, bool isHeadlessEnabled=false)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    Driver = CreateChromeDriver(isHeadlessEnabled);
                    break;
                case BrowserType.Firefox:
                    Driver = CreateFirefoxDriver(isHeadlessEnabled);
                    break;
                case BrowserType.IE:
                    Driver = CreateIEDriver();
                    break;
                default:
                    throw new InvalidOperationException($"Browser {browserType} is not currently supported by the framework.");
            }
            
            return Driver;
        }

        public static RemoteWebDriver CloseDriver()
        {
            if (Driver != null)
            {
                Driver.Close();
                Driver.Quit();
                Driver = null;
            }
            return Driver;
        }

        public static RemoteWebDriver CloseBrowser()
        {
            if (Driver != null)
            {
                Driver.Close();
            }
            return Driver;
        }

        private static RemoteWebDriver CreateChromeDriver(bool isHeadlessEnabled)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--incognito");
            if (isHeadlessEnabled) options.AddArgument("--headless");

            RemoteWebDriver webDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options);

            return webDriver;
        }

        private static RemoteWebDriver CreateFirefoxDriver(bool isHeadlessEnabled)
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--incognito");
            if (isHeadlessEnabled)  options.AddArguments("--headless");

            RemoteWebDriver webDriver = new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), options);
            return webDriver;
        }

        private static RemoteWebDriver CreateIEDriver()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.IgnoreZoomLevel = true;

            RemoteWebDriver webDriver = new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(), options);
            return webDriver;
        }
    }
}
