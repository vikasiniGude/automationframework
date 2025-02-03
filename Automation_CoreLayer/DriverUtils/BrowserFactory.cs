using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace Automation_CoreLayer.DriverUtils
{
    public class BrowserFactory
    {
        public static IWebDriver GetWebDriver(string browserType)
        {
            IwebDriverFactory driverFactory;

            switch (browserType.ToLower())
            {
                case "chrome":
                    driverFactory = new ChromeDriverFactory();
                    break;
                case "edge":
                    driverFactory = new EdgeDriverFactory();
                    break;
                default:
                    throw new ArgumentException($"Browser type '{browserType}' is not supported.");
            }
            return driverFactory.CreateDriver();
        }
    }
}
