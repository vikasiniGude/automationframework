using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;

namespace Automation_CoreLayer.Utils
{
    public class SeleniumDriver
    {
        private IWebDriver Driver;
        private bool _disposed;
        private object DriverLock = new object();
        public IWebDriver GetDriver(string browserType)
        {
            if (Driver == null)
            {
                lock (DriverLock)
                {
                    if (Driver == null)
                    {
                        Driver = CreateDriver(browserType);
                    }
                }
            }
            return Driver;
        }
        public IWebDriver CreateDriver(string browserType)
        {
            switch (browserType)
            {
                case "Chrome":
                    {
                        Driver = new ChromeDriver();
                        break;
                    }
                case "Edge":
                    {
                        Driver = new EdgeDriver();
                        break;
                    }
                case "IE":
                    {
                        Driver = new InternetExplorerDriver();
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Unsupported browser type");
                    }
            }
            return Driver;
        }

        public void QuitDriver()
        {
            lock (DriverLock)
            {
                if (Driver != null)
                {
                    Driver.Quit();
                    Driver = null;
                }
            }
        }
        protected virtual void DisposeDriver(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Driver?.Quit();
                    Driver?.Dispose();
                }

                // Free unmanaged resources (if any) and set large fields to null.
                Driver = null;
                _disposed = true;
                DisposeDriver(false);
            }
        }

        public void Dispose()
        {
            // Prevent finalizer from running.
            DisposeDriver(true);
            GC.SuppressFinalize(this);
        }
    }
}
