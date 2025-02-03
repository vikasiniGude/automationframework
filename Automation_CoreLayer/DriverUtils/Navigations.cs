using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Automation_CoreLayer.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;

namespace Automation_CoreLayer.DriverUtils
{
    public static class Navigations
    {
        public static void NavigateTo(this IWebDriver driver, string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at NavigateTo method for {url}, {ex.Message}");
                throw;
            }
        }

        public static void NavigateBack(this IWebDriver driver)
        {
            try
            {
                driver.Navigate().Back();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at NavigateBack method, {ex.Message}");
                throw;
            }
        }

        public static void RefreshPage(this IWebDriver driver)
        {
            try
            {
                driver.Navigate().Refresh();
                driver.Wait(5);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at RefreshPage method, {ex.Message}");
                throw;
            }
        }
        public static bool SwitchToTab(this IWebDriver driver, string title)
        {
            try
            {
                foreach (var handle in driver.WindowHandles)
                {
                    driver.SwitchTo().Window(handle);
                    if (driver.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at SwitchToTab method, {ex.Message}");
                throw;
            }
        }
        public static void SwitchToFrame(this IWebDriver driver, By locator)
        {
            try
            {
                IWebElement element = driver.WaitUntilElementVisibile(locator, 30);
                driver.SwitchTo().Frame(element);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at SwitchToFrame method, {ex.Message}");
                throw;
            }
        }
        public static void SwitchToFrame(this IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().ParentFrame();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at SwitchToFrame method, {ex.Message}");
                throw;
            }
        }
        public static void SwitchToDefaultContent(this IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at SwitchToDefaultContent method, {ex.Message}");
                throw;
            }
        }
        public static void EnterTextIntoAlert(this IWebDriver driver, string text)
        {
            try
            {
                string alertText = driver.SwitchTo().Alert().Text;
                driver.SwitchTo().Alert().SendKeys(text);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at EnterTextIntoAlert method, {ex.Message}");
                throw;
            }
        }
        public static void AcceptAlert(this IWebDriver driver, string text)
        {
            try
            {
                driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at EnterTextIntoAlert method, {ex.Message}");
                throw;
            }
        }
        public static void DismissAlert(this IWebDriver driver, string text)
        {
            try
            {
                driver.SwitchTo().Alert().Dismiss();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at EnterTextIntoAlert method, {ex.Message}");
                throw;
            }
        }
    }
}
