using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using BoDi;
using NUnit.Framework;
using RestSharp;
using OneCrmTestProject.Helpers;
using System.Text.Json;
using System.Net;

namespace OneCrmTestProject.Hooks
{
    [Binding]
    public class Hooks
    {
        // Using the DI framework which is a part of SpecFlow
        private readonly IObjectContainer _container;
        private ScenarioContext _scenarioContext;

        public Hooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario("UI")]
        public void CreateWebDriver()
        {
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

        [AfterScenario("UI")]
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