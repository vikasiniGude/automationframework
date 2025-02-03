using TechTalk.SpecFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
namespace OrangeHRMTestLayer.StepDefinitions
{
    public class BaseSteps
    {
        protected IWebDriver Driver;
        protected string url;
        protected string username;
        protected string password;
        private ScenarioContext ScenarioContext;
        private FeatureContext featureContext;
        public BaseSteps(FeatureContext fc, ScenarioContext sc)
        {
            this.featureContext = fc;
            url = fc.Get<string>("url");
            username = fc.Get<string>("username");
            password = fc.Get<string>("password");
            this.ScenarioContext = sc;
            Driver = sc.Get<IWebDriver>("Driver");

        }
    }
}
