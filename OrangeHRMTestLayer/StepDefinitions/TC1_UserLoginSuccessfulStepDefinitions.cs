using System;
using Automation_CoreLayer.Utils;
using OrangeHRMTestLayer.StepDefinitions;
using TechTalk.SpecFlow;
using OrangeHRM_BusinessLayer.Pages;
using System.Security.AccessControl;
using NUnit.Framework;

namespace OrangeHRM_TestLayer.Steps
{
    [Binding]
    public class TC1_UserLoginSuccessfulStepDefinitions : BaseSteps
    {
        private UserLoginPage LoginPage;
        public TC1_UserLoginSuccessfulStepDefinitions(FeatureContext fc, ScenarioContext sc) : base(fc, sc)
        {
            LoginPage = new UserLoginPage(Driver);
        }

        [Given(@"I am in the orange HRM login page")]
        public void GivenIAmInTheOrangeHRMLoginPage()
        {
            try
            {
                Log4NetLogger.Info("Navigating to the URL");
                LoginPage.NavigateToURL(url);
                Log4NetLogger.Info("Navigation successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log4NetLogger.Error($"Failed: at GivenIAmInTheOrangeHRMLoginPage- {ex.Message}");
                Assert.Fail($"Failed: at GivenIAmInTheOrangeHRMLoginPage- {ex.Message}");
            }
        }

        [When(@"I input the username, password and click on login")]
        public void WhenIInputTheUsernamePasswordAndClickOnLogin()
        {
            try
            {
                Log4NetLogger.Info($"Login with username {username} and password");
                LoginPage.Login(username, password);
                Log4NetLogger.Info("Login successful");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log4NetLogger.Error($"Failed: at WhenIInputTheUsernamePasswordAndClickOnLogin- {ex.Message}");
                Assert.Fail($"Failed: at WhenIInputTheUsernamePasswordAndClickOnLogin- {ex.Message}");
            }
        }

        [Then(@"I should see the dashboard page")]
        public void ThenIShouldSeeTheDashboardPage()
        {
            try
            {
                Log4NetLogger.Info("Login validation in progress");
                Assert.That(LoginPage.VerifyLoginIsSuccessful(), "Login Validation failed");
                Log4NetLogger.Info("Login validation complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log4NetLogger.Error($"Failed: at WhenIInputTheUsernamePasswordAndClickOnLogin- {ex.Message}");
                Assert.Fail($"Failed: at WhenIInputTheUsernamePasswordAndClickOnLogin- {ex.Message}");
            }
        }
    }
}
