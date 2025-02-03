using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_CoreLayer.Utils;
using OpenQA.Selenium;
using static System.Net.Mime.MediaTypeNames;

namespace Automation_CoreLayer.DriverUtils
{
    public static class JavaScriptActions
    {
        public static void EnterTextUsingJS(this IWebDriver driver,string text, By locator)
        {
            try
            {
                IWebElement element = driver.FindElement(locator);
                element.Clear();
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView()", element);
                jse.ExecuteScript("arguments[0].setAttribute(\"value\", arguments[1])",element, "");

            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at EnterTextUsingJS method for text -{text}, {locator.ToString()}, {ex.Message}");
                throw;
            }

        }
        public static void ScrollIntoViewUsingJS(this IWebDriver driver, By locator)
        {
            try
            {
                IWebElement element = driver.FindElement(locator);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView()", element);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at ScrollIntoViewUsingJS method for {locator.ToString()}, {ex.Message}");
                throw;
            }
        }
        public static void ClickUsingJS(this IWebDriver driver, By locator)
        {
            try
            {
                IWebElement element = driver.FindElement(locator);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView()", element);
                jse.ExecuteScript("arguments[0].click()", element, "");

            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at ClickUsingJS method for {locator.ToString()}, {ex.Message}");
                throw;
            }

        }
    }
}
