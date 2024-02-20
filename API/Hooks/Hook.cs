using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using RestSharp;
using TechTalk.SpecFlow;

namespace WebAutomation.Hooks
{
    [Binding]
    public class Hook
    {
        public static RestClient restClient;
        public static HttpClient httpClient;
        public static AventStack.ExtentReports.ExtentReports extent;
        public static ExtentTest feature;
        public ExtentTest _scenario, _step;
        static string reportPath =Directory.GetParent(@"../../../").FullName
           + Path.DirectorySeparatorChar + "Test Results"
           + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("yyyyMMddHHmmss");

        [BeforeTestRun]
        public static void beforeTestRun()
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://reqres.in/api/")
            };

            //Rest Client
             restClient = new RestClient(restClientOptions);

            httpClient = new HttpClient();

            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath + ".html");
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void beforeFeature(FeatureContext featureContext)
        {
            feature = extent.CreateTest(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void beforeScenario(ScenarioContext scenarioContext)
        {
            scenarioContext.Add("RestClient", restClient);
            scenarioContext.Add("HttpClient", httpClient);
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

            }
            else if (scenarioContext.TestError != null)
            {
                _step.Log(Status.Fail, scenarioContext.StepContext.StepInfo.Text);
            }
        }

        [AfterFeature]
        public static void afterFeature()
        {
            extent.Flush();
        }

        [AfterTestRun]
        public static void afterTestRun()
        {
            restClient.Dispose();
            httpClient.Dispose();
        }
    }
}
