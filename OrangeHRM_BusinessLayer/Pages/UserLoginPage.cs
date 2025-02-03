using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_CoreLayer.DriverUtils;
using OpenQA.Selenium;

namespace OrangeHRM_BusinessLayer.Pages
{
    public class UserLoginPage : CommonPage
    {
        private readonly By UsernamePath = By.XPath("//input[@placeholder=\"Username\"]");
        private readonly By PasswordPath = By.Name("password");
        private readonly By LoginButton = By.XPath("//button[@type='submit']");
        private readonly By DashboardCheck = By.XPath("//span[text()='Dashboard']");
        public UserLoginPage(IWebDriver driver) : base(driver)
        {
        }
        public void NavigateToURL(string url)
        {
            try
            {
                BLDriver.NavigateTo(url);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Login(string username, string password)
        {
            try
            {
                BLDriver.EnterText(UsernamePath, username);
                BLDriver.EnterText(PasswordPath, password);
                BLDriver.Click(LoginButton);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool VerifyLoginIsSuccessful()
        {
            try
            {
                if (BLDriver.WaitUntilElementVisibile(DashboardCheck, 30) == null) 
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
