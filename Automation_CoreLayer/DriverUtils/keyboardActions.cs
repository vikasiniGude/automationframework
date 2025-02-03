using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_CoreLayer.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Automation_CoreLayer.DriverUtils
{
    public static  class keyboardActions
    {
        private static Actions action;
        public static void EnterSimulate(this IWebDriver driver)
        {
            try
            {
                action = new Actions(driver);
                action.SendKeys(Keys.Enter).Perform();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at EnterSimulate method, {ex.Message}");
                throw;
            }

        }
        public static void ArrowDownSimulate(this IWebDriver driver)
        {
            try
            {
                action = new Actions(driver);
                action.SendKeys(Keys.ArrowDown).Perform();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at ArrowDownSimulate method, {ex.Message}");
                throw;
            }

        }
        public static void TabSimulate(this IWebDriver driver)
        {
            try
            {
                action = new Actions(driver);
                action.SendKeys(Keys.Tab).Perform();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at TabSimulate method, {ex.Message}");
                throw;
            }

        }
        public static void ArrowUpSimulate(this IWebDriver driver)
        {
            try
            {
                action = new Actions(driver);
                action.SendKeys(Keys.ArrowUp).Perform();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at ArrowUpSimulate method, {ex.Message}");
                throw;
            }
        }
        public static void MouseHover(this IWebDriver driver, By locator)
        {
            try
            {
                IWebElement element = driver.WaitUntilElementVisibile(locator, 30);
                Actions action = new Actions(driver);
                action.MoveToElement(element).Build().Perform();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at MouseHover method, {ex.Message}");
                throw;
            }
        }
        public static void RightClick(this IWebDriver driver, By locator)
        {
            try
            {
                IWebElement element = driver.WaitUntilElementVisibile(locator, 30);
                Actions action = new Actions(driver);
                action.ContextClick(element).Build().Perform();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at RightClick method, {ex.Message}");
                throw;
            }
        }
        public static void DoubleClick(this IWebDriver driver, By locator)
        {
            try
            {
                IWebElement element = driver.WaitUntilElementVisibile(locator, 30);
                Actions action = new Actions(driver);
                action.DoubleClick(element).Build().Perform();
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at DoubleClick method, {ex.Message}");
                throw;
            }
        }
        public static void DoubleClickAndEnterText(this IWebDriver driver, string text, By toplocator, By bottomLocator)
        {
            try
            {
                IWebElement element = driver.WaitUntilElementVisibile(toplocator, 30);
                var builder = new Actions(driver);
                IWebElement element1 = driver.WaitUntilElementVisibile(bottomLocator, 30);
                builder.DoubleClick(element).KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control).SendKeys(Keys.Delete).SendKeys(element1, text).Build().Perform();

            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at DoubleClickAndEnterText method, {ex.Message}");
                throw;
            }
        }
        public static void ClearAndEnterText(this IWebDriver driver, string text, By locator)
        {
            try
            {
                IWebElement element = driver.WaitUntilElementVisibile(locator, 30);
                var builder = new Actions(driver);
                driver.Wait(5);
                builder.Click(element).KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control).SendKeys(Keys.Delete).SendKeys(element, text).Build().Perform();                
                driver.Wait(2);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at DoubleClickAndEnterText method, {ex.Message}");
                throw;
            }
        }
        public static void DragAndDrop(this IWebDriver driver, By toplocator, By bottomLocator)
        {
            try
            {
                IWebElement element1 = driver.WaitUntilElementVisibile(toplocator, 30);
                action = new Actions(driver);
                IWebElement element2 = driver.WaitUntilElementVisibile(bottomLocator, 30);
                action.ClickAndHold(element1).MoveToElement(element2).Release(element2).Build().Perform();

            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Error: at DragAndDrop method, {ex.Message}");
                throw;
            }
        }
    }
}
