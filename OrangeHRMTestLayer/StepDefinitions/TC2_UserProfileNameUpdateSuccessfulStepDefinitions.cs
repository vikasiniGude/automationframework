using System;
using TechTalk.SpecFlow;
using OrangeHRM_BusinessLayer.Pages;
using Automation_CoreLayer.Utils;
using NUnit.Framework;

namespace OrangeHRMTestLayer.StepDefinitions
{
    [Binding]
    public class TC2_UserProfileNameUpdateSuccessfulStepDefinitions : BaseSteps
    {
        private UserInfoPage InfoPage;
        public TC2_UserProfileNameUpdateSuccessfulStepDefinitions(FeatureContext fc, ScenarioContext sc) : base(fc, sc)
        {
            InfoPage = new UserInfoPage(Driver);
        }

        [When(@"I select tab (.*) from left pane")]
        public void WhenISelectMyTabFromLeftPane(string tabName)
        {
            try
            {
                Log4NetLogger.Info($"Select tab {tabName}");
                InfoPage.NavigateToLeftPaneTab(tabName);
                Log4NetLogger.Info("Navigation successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log4NetLogger.Error($"Failed: at WhenISelectMyTabFromLeftPane- {ex.StackTrace}");
                Assert.Fail($"Failed: at WhenISelectMyTabFromLeftPane- {ex.StackTrace}");
            }
        }

        [When(@"update Employee name (.*) and click save")]
        public void WhenUpdateEmployeeNameAndClickSave(string employeeName)
        {
            try
            {
                Log4NetLogger.Info("updating name in Info page");
                InfoPage.EditProfileEmployee(employeeName);               
                Log4NetLogger.Info("update saved");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log4NetLogger.Error($"Failed: at WhenUpdateEmployeeNameAndClickSave- {ex.StackTrace}");
                Assert.Fail($"Failed: at GivenIAmInTheOranWhenUpdateEmployeeNameAndClickSavegeHRMLoginPage- {ex.StackTrace}");
            }
        }

        [Then(@"I validate employee name (.*) update is successful")]
        public void ThenIValidateEmployeeNameEmployeeNameUpdateIsSuccessful(string employeeName)
        {
            try
            {
                Log4NetLogger.Info("validate name in profile page");
                string actualName = string.Empty;
                Assert.IsTrue(InfoPage.ValidateProfileUpdate(employeeName, ref actualName), $"profile name not matches Expected: {employeeName}, Actual: {actualName}");
                Log4NetLogger.Info("Validation successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log4NetLogger.Error($"Failed: at GivenIAmInTheOrangeHRMLoginPage- {ex.StackTrace}");
                Assert.Fail($"Failed: at GivenIAmInTheOrangeHRMLoginPage- {ex.StackTrace}");
            }
        }
    }
}
