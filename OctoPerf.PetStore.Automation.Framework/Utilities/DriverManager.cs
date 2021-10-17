using OpenQA.Selenium.Chrome;
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

        public static RemoteWebDriver InitiateDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    Driver = CreateChromeDriver();
                    break;
                case BrowserType.Firefox:
                    Driver = CreateFirefoxDriver();
                    break;
                default:
                    throw new InvalidOperationException($"Browser {browserType} is not currently supported by the framework.");
            }
            
            return Driver;
        }

        public static void CloseDriver()
        {
            if (Driver != null)
            {
                Driver.Close();
                Driver.Quit();
                Driver = null;
            }
        }

        public static void CloseBrowser()
        {
            if (Driver != null)
            {
                Driver.Close();
            }
        }

        private static RemoteWebDriver CreateChromeDriver()
        {
            RemoteWebDriver webDriver = null;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--incognito");

            webDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options);

            return webDriver;
        }

        private static RemoteWebDriver CreateFirefoxDriver()
        {
            return null;
        }
    }
}
