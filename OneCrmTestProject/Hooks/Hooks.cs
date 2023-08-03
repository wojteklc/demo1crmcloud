using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using BoDi;
using NUnit.Framework;
using RestSharp;
using OneCrmTestProject.Helpers;
using System.Text.Json;
using System.Net;
using AventStack.ExtentReports;
using System.Reflection;
using AventStack.ExtentReports.Reporter;

namespace OneCrmTestProject.Hooks
{
    /// <summary>
    /// Driver is handled using the Specflow's build-in Dependency Injection
    /// Connected 'ExtentReports' HTML reporter
    ///
    /// [BeforeTestRun] - for 'ExtentReports' purpose
    /// [BeforeFeature] - for 'ExtentReports' purpose
    /// [BeforeStep] - for 'ExtentReports' purpose
    /// [AfterStep] - for 'ExtentReports' purpose
    /// [AfterFeature] - for 'ExtentReports' purpose
    ///
    /// [BeforeScenario("UI")]
    /// [BeforeScenario("API")]
    /// [AfterScenario("UI", "API")]
    /// </summary>
    [Binding]
    public class Hooks
    {
        // Using the DI framework which is a part of SpecFlow
        private readonly IObjectContainer _container;
        private ScenarioContext _scenarioContext;

        private static string? _extentReportFolderPath;
        private static ExtentReports? _extentReports;
        private static ExtentTest? _feature;
        private ExtentTest? _scenario;
        private ExtentTest? _step;

        public Hooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var path = Assembly.GetCallingAssembly().Location;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            var timestamp = DateTime.Now.ToString("dd-MM-yyyy_HH;mm;ss");
            var reportFolderPath = projectPath + $"ExtentTestReports\\TestReport_{timestamp}\\";

            if (!Directory.Exists(reportFolderPath))
            {
                Directory.CreateDirectory(reportFolderPath);
            }

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(new ExtentHtmlReporter(reportFolderPath));
            _extentReportFolderPath = reportFolderPath;
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extentReports.CreateTest(featureContext.FeatureInfo.Title);
        }

        [BeforeStep]
        public void BeforeStep()
        {
            _step = _scenario;
        }

        [AfterStep]
        public void AfterStep()
        {
            if (_scenarioContext.TestError == null)
            {
                _step.Log(Status.Pass, _scenarioContext.StepContext.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                // Taking screenshot on test failure, saving it and adding to test report
                var driver = _container.Resolve<IWebDriver>();
                var screenshotPath = $"{_extentReportFolderPath}{_scenarioContext.ScenarioInfo.Title}.png";
                var stackTrace = $"{_scenarioContext.StepContext.StepInfo.Text}<br><br>{_scenarioContext.TestError.StackTrace.Replace(Environment.NewLine, "<br>")}<br>";

                Screenshot file = ((ITakesScreenshot)driver).GetScreenshot();
                file.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

                // Step failure report will consist of stack trace and screenshot
                _step.Log(Status.Fail, stackTrace, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            _extentReports?.Flush();
        }

        [BeforeScenario("UI")]
        public void CreateWebDriver()
        {
            _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title);

            var chromeOptions = new ChromeOptions();

            if (TestContext.Parameters.Exists("disableNotifications"))
            {
                if (bool.Parse(TestContext.Parameters["disableNotifications"].ToLower()))
                {
                    chromeOptions.AddArgument("--disable-notifications");
                }
            }

            if (TestContext.Parameters.Exists("headlessMode"))
            {
                if (bool.Parse(TestContext.Parameters["headlessMode"].ToLower()))
                {
                    chromeOptions.AddArgument("--headless");
                }
            }

            if (TestContext.Parameters.Exists("maximizeBrowser"))
            {
                if (bool.Parse(TestContext.Parameters["maximizeBrowser"].ToLower()))
                {
                    chromeOptions.AddArgument("--start-maximized");
                }
            }

            var driver = new ChromeDriver(chromeOptions);

            // Registering WebDriver with DI container
            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [BeforeScenario("API")]
        public void LogInUsingApi()
        {
            _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title);

            var client = new RestClient(TestContext.Parameters["oneCrmBaseUrl"]);
            var request = new RestRequest(TestContext.Parameters["loginApiEndpoint"], Method.Post);

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-type", "application/json");

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            request.AddBody(JsonSerializer.Serialize(new ApiLoginRequestDto(), jsonSerializerOptions));

            var response = client.Execute(request);
            var responseDeserialized = JsonSerializer.Deserialize<ApiLoginResponseDto>(response.Content, jsonSerializerOptions);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), responseDeserialized.Result);

            // Adding session cookie to ScenarioContext to be reused in tests
            _scenarioContext["ApiSessionIdCookie"] = response.Cookies["PHPSESSID"];
        }

        [AfterScenario("UI", "API")]
        public void DisposeWebDriver()
        {
            var driver = _container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}