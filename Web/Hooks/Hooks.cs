using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebAutomation.Common;
using WebAutomation.DriverFactory;
using WebAutomation.Support.Reporting;

namespace WebAutomation.Hooks
{
    [Binding]
    public class Hook 
    {
        public static IWebDriver driver;
        static CreateBrowsers createBrowser;
        public static AventStack.ExtentReports.ExtentReports extent;
        public static ExtentTest feature;
        public ExtentTest _scenario, _step;
        static string reportPath = Directory.GetParent(@"../../../").FullName
           + Path.DirectorySeparatorChar + "Test Results"
           + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("yyyyMMddHHmmss");

        [BeforeTestRun]
        public static void setUp()
        {

            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath+".html");
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);

            var appSettingsData = DataReader.ReadDataFromJson<AppSettingParser>(Directory.GetParent(@"../../../").FullName + Path.DirectorySeparatorChar+"AppSettings.json");
            
            switch (appSettingsData.browser.ToLower())
            {
                case "chrome":
                    //driver = new ChromeDriver(appSettingsData.driverPath);
                    IBrowserFactory chromeFactory = new ChromeFactory();
                     createBrowser = new CreateBrowsers(chromeFactory);
                    driver=createBrowser.Initialize();
                    createBrowser.Navigate(appSettingsData.appUrl);
                    break;
            }
        }

        [BeforeFeature]
        public static void beforeFeature(FeatureContext featureContext)
        {
            feature = extent.CreateTest(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void beforeScenario(ScenarioContext scenarioContext)
        {
            _scenario = feature.CreateNode(scenarioContext.ScenarioInfo.Title);
        }

        [BeforeStep]
        public void beforeStep()
        {
            _step = _scenario;
        }

        [AfterStep]
        public void afterStep(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError == null)
            {
                _step.Log(Status.Pass, scenarioContext.StepContext.StepInfo.Text);
                
            } else if (scenarioContext.TestError != null)
            {
                _step.Log(Status.Fail, scenarioContext.StepContext.StepInfo.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(CaptureScreenshots.screenshotCpature(driver)).Build());
            }
        }

        [AfterFeature]
        public static void afterFeature()
        {
            extent.Flush();
        }

        [AfterTestRun]
        public static void tearDown()
        {
            createBrowser.Cleanup();
        }
    }
}
