using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_CoreLayer.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.Support.UI;

namespace Automation_CoreLayer.DriverUtils
{
    public static class AllActions
    {
        public static void Click(this IWebDriver driver, By locator)
        {
            try
            {
                IWebElement element = driver.WaitUntilElementClickable(locator, 30);
                element.Click();
                driver.SetImplicitWait(10);
            }
            catch (ElementClickInterceptedException)
            {
                Console.WriteLine("Click intercepted. Attempting to scroll to the element and retry.");
                driver.ScrollIntoViewUsingJS(locator);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at click method for {locator.ToString()}, {ex.Message}");
                throw;
            }

        }
        public static void Click(this IWebDriver driver, By locator, int waitTime)
        {
            try
            {
                IWebElement element = driver.WaitUntilElementClickable(locator, waitTime);
                element.Click();
                driver.SetImplicitWait(10);
            }
            catch (ElementClickInterceptedException)
            {
                Console.WriteLine("Click intercepted. Attempting to scroll to the element and retry.");
                driver.ScrollIntoViewUsingJS(locator);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at click method for {locator.ToString()}, {ex.Message}");
                throw;
            }

        }
        public static void EnterText(this IWebDriver driver, By locator, string text)
        {
            try
            {
                IWebElement element = driver.WaitUntilElementVisibile(locator, 30);
                element.Clear();
                element.SendKeys(text);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at enterText method for {locator.ToString()}, {ex.Message}");
                throw;
            }

        }
        public static void TakeScreenShot(this IWebDriver driver, string fileName)
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(fileName);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at TakeScreenShot method, {ex.Message}");
                throw;
            }
        }

        public static string GetText(this IWebDriver driver, By locator, string attribute=null)
        {
            try
            {
                IWebElement element = driver.WaitUntilElementVisibile(locator, 30);
                string elementText = element.Text;
                if (attribute != null)
                     elementText = element.GetAttribute(attribute);
                return elementText;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at enterText method for {locator.ToString()}, {ex.Message}");
                throw;
            }

        }

        public static void SelectFromDropdown(this IWebDriver driver, By Mainlocator, By SubLocator)
        {
            try
            {
                Click(driver, Mainlocator);
                Click(driver, SubLocator);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at SelectFromDropdown method for {SubLocator.ToString()}, {ex.Message}");
                throw;
            }
        }
        public static void SelectFromDropdown(this IWebDriver driver, By locator, string text)
        {
            try
            {
                SelectElement element = new SelectElement(driver.FindElement(locator));
                element.SelectByText(text);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at SelectFromDropdown method for {text}, {ex.Message}");
                throw;
            }
        }
    }
}
