using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_CoreLayer.DriverUtils;
using OpenQA.Selenium;

namespace OrangeHRM_BusinessLayer.Pages
{
    public class UserInfoPage : CommonPage
    {
        private readonly By EmployeeNamePath = By.Name("firstName");
        private readonly By SaveButton = By.XPath("//div[@class='orangehrm-horizontal-padding orangehrm-vertical-padding']//button[@type='submit']");
        private readonly By ProfileNameCheckPath = By.XPath("//span[@class='oxd-userdropdown-tab']/p");
        private readonly By EmployeeIdPath = By.XPath("//label[text()='Employee Id']/../following-sibling::div");
        public UserInfoPage(IWebDriver driver) : base(driver)
        {
        }
        public void NavigateToLeftPaneTab(string tabName)
        {
            try
            {
                By tabPath = By.XPath($"//span[text()='{tabName}']");
                BLDriver.Click(tabPath);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void EditProfileEmployee(string employeeName)
        {
            try
            {
                BLDriver.ClearAndEnterText(employeeName, EmployeeNamePath);
                Random random = new Random();
                int randomNumber = random.Next(1,10);
                BLDriver.ClearAndEnterText(randomNumber.ToString(), EmployeeIdPath);
                BLDriver.Click(SaveButton);
                BLDriver.RefreshPage();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool ValidateProfileUpdate(string employeeName, ref string actualName)
        {
            try
            {
                actualName = BLDriver.GetText(ProfileNameCheckPath);
                if (actualName.Contains(employeeName))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
