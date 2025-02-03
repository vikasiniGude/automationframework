using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_CoreLayer.Utils;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using static System.Net.Mime.MediaTypeNames;

namespace Automation_CoreLayer.DriverUtils
{
    public static class waitOnElementActions
    {
        public static void WaitForPageLoad(this IWebDriver driver, int waitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
                wait.Until(d =>
                {
                    string state = ((IJavaScriptExecutor)driver).ExecuteScript(@"return document.readystate").ToString();
                    return (state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase));
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Log4NetLogger.Error($"Error: Page has exceeded the time given to load - WaitForPageLoad method, {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at WaitForPageLoad method, {ex.Message}");
                throw;
            }

        }
        public static void SetImplicitWait(this IWebDriver driver, int timeoutInSeconds)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
        }
        public static void Wait(this IWebDriver driver, int timeoutInSeconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(timeoutInSeconds));
        }
        public static void SetPageLoadTimeOut(this IWebDriver driver, int waitTime)
        {
            try
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(waitTime);
            }
            catch (WebDriverTimeoutException ex)
            {
                Log4NetLogger.Error($"Error: Page has exceeded the time given to load - SetPageLoadTimeOut method, {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at SetPageLoadTimeOut method, {ex.Message}");
                throw;
            }

        }
        public static IWebElement WaitUntilElementVisibile(this IWebDriver driver, By locator, int waitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return element;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: Element is not visible within the time, at WaitUntilElementVisibile method for {locator.ToString()}, {ex.Message}");
                return null;
            }

        }
        public static IWebElement WaitUntilElementClickable(this IWebDriver driver, By locator, int waitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
                IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                return element;
            }
            catch (WebDriverTimeoutException ex)
            {
                Log4NetLogger.Error($"Error: Element is not clickable within the time, at WaitUntilElementVisibile method for {locator.ToString()}, {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at WaitUntilElementVisibile method for {locator.ToString()}, {ex.Message}");
                throw;
                return null;
            }

        }
        public static bool WaitForTextToBePresent(this IWebDriver driver, IWebElement element, string text, int timeoutInSeconds)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(ExpectedConditions.TextToBePresentInElement(element, text));
            }
            catch (WebDriverTimeoutException ex)
            {
                Log4NetLogger.Error($"Error: Text in element is not visible within the time, at WaitForTextToBePresent method for {text}, {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at WaitForTextToBePresent method for {text}, {ex.Message}");
                throw;
                return false;
            }
        }
        public static IWebElement? WaitForElementWithFluentWait(this IWebDriver driver, By locator, int timeoutInSeconds, int pollingIntervalInMillis)
        {
            try
            {
                DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver)
                {
                    Timeout = TimeSpan.FromSeconds(timeoutInSeconds),
                    PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalInMillis)
                };
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

                return fluentWait.Until(drv =>
                {
                    IWebElement element = drv.FindElement(locator);
                    return element != null && element.Displayed ? element : null;
                });
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at WaitForElementWithFluentWait method for , {ex.Message}");
                throw;
                return null;
            }
        }
        public static IWebElement GetElement(this IWebDriver driver, By by_selector, int timeoutInSeconds = 10)
        {
            IWebElement element = null;
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                element = wait.Until(ExpectedConditions.ElementExists(by_selector));
            }
            catch (StaleElementReferenceException stl)
            {
                try
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    element = wait.Until(ExpectedConditions.ElementExists(by_selector));
                }
                catch (Exception)
                {
                    throw new Exception($"Failed to find the element = {by_selector}");
                }
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at GetElement method for , {ex.Message}");
                throw new Exception($"Failed to find the element = {by_selector}");
            }
            finally
            {
                //can be used for logging the info of the exception.
            }
            return element;
        }
    }
}
