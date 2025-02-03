using TechTalk.SpecFlow;
using OpenQA.Selenium;
using log4net;
using Automation_CoreLayer.Utils;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Automation_CoreLayer.DriverUtils;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;

namespace OrangeHRM_TestLayer
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver? _driver = null;
        private readonly SeleniumDriver _seleniumDriver = new SeleniumDriver();
        private static string ScreenshotPath = string.Empty;
        private static string ReportPath = string.Empty;
        private static AsyncLocal<ExtentTest>? feature = new AsyncLocal<ExtentTest>();
        private static AsyncLocal<ExtentTest>? scenario = new AsyncLocal<ExtentTest>();
        private static readonly Lazy<ExtentReports> _extent = new Lazy<ExtentReports>(SetupExtentReport);

        public static ExtentReports extentReports => _extent.Value;
        //private static ExtentReports? extentReports;
        private static string TestLayerPath = string.Empty;
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        private static ExtentReports SetupExtentReport()
        {
            ReportPath = GetReportDirectoryPath();
            var extent = new ExtentReports();
            extent.AttachReporter(new ExtentSparkReporter($@"{ReportPath}\\Report_{DateTime.Now.ToString("hh_mm_ss_tt")}.html"));
            return extent;
        }
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            TestLayerPath = Directory.GetCurrentDirectory().Split("TestLayer")[0] + "TestLayer";
            DirectoryInfo directoryInfo = new DirectoryInfo(TestLayerPath);
            TestLayerPath = directoryInfo.Parent.FullName;
            Log4NetLogger.SetFilePath(GetLogFilePath());

            //ReportPath = GetReportDirectoryPath();
            //extentReports = new ExtentReports();
            //extentReports.AttachReporter(new ExtentSparkReporter($@"{ReportPath}\\Report_{DateTime.Now.ToString("hh_mm_ss_tt")}.html"));
        }
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            string username = AppSettings.GetAppSettingData("username");
            string password = AppSettings.GetAppSettingData("password");
            string url = AppSettings.GetAppSettingData("url");
            string browser = AppSettings.GetAppSettingData("browser");
            featureContext.Set<string>(username, "username");
            featureContext.Set<string>(password, "password");
            featureContext.Set<string>(url, "url");
            if (featureContext != null)
            {
                feature.Value = extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title, $"{featureContext.FeatureInfo.Description} on '{browser}' browser");
            }
        }
        [BeforeScenario("@ui")]
        public void BeforeScenario()
        {
            scenario.Value = feature.Value.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title, _scenarioContext.ScenarioInfo.Description);
            //_driver = BrowserFactory.GetWebDriver(AppSettings.GetAppSettingData("browser"));
            _driver = _seleniumDriver.GetDriver(AppSettings.GetAppSettingData("browser"));
            _driver.Manage().Window.Maximize();
            _scenarioContext.Set(_driver, "Driver");

        }
        [AfterStep]
        public void AfterStep()
        {
            ScenarioBlock scenarioBlock = _scenarioContext.CurrentScenarioBlock;
            switch (scenarioBlock)
            {
                case ScenarioBlock.Given:
                    CreateNode<Given>();
                    break;
                case ScenarioBlock.When:
                    CreateNode<When>();
                    break;
                case ScenarioBlock.Then:
                    CreateNode<Then>();
                    break;
                default:
                    CreateNode<And>();
                    break;
            }
        }
        [AfterScenario]
        public void AfterScenario()
        {
            //_driver.Quit();
            //_driver = null;
            _seleniumDriver.Dispose();
        }
        [AfterTestRun]
        public static void TearDownReport()
        {
            extentReports?.Flush();
        }
        public void CreateNode<T>() where T : IGherkinFormatterModel
        {
            if (_scenarioContext.TestError != null)
            {
                if (!Directory.Exists($"{TestLayerPath}\\Screenshots"))
                    Directory.CreateDirectory($"{TestLayerPath}\\Screenshots");
                ScreenshotPath = $"{TestLayerPath}\\Screenshots";
                string fileName = $"{ScreenshotPath}\\" + _scenarioContext.ScenarioInfo.Title.ToString() + "_" + DateTime.Now.ToString("hh_mm_ss_tt") + ".png";
                AllActions.TakeScreenShot(_driver, fileName);
                scenario.Value.CreateNode<T>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message + "\n" + _scenarioContext.TestError.StackTrace).AddScreenCaptureFromPath(fileName);
            }
            else
            {
                scenario.Value.CreateNode<T>(_scenarioContext.StepContext.StepInfo.Text);
            }
        }

        public static string GetReportDirectoryPath()
        {
            if (!Directory.Exists($"{TestLayerPath}\\Reports"))
                Directory.CreateDirectory($"{TestLayerPath}\\Reports");
            return $"{TestLayerPath}\\Reports";
        }
        public static string GetLogFilePath()
        {
            if (!Directory.Exists($"{TestLayerPath}\\Log"))
                Directory.CreateDirectory($"{TestLayerPath}\\Log");
            return $"{TestLayerPath}\\Log\\log-file{DateTime.Now.ToString("hh_mm_ss tt")}.log";
        }
    }
}