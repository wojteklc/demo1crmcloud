using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using BoDi;
using NUnit.Framework;

namespace OneCrmTestProject.Hooks
{
    [Binding]
    public class Hooks
    {
        // Using the DI framework which is a part of SpecFlow
        private readonly IObjectContainer _container;

        public Hooks(IObjectContainer container)
        {
            _container = container;
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

            if (TestContext.Parameters.Exists("browserResolution"))
            {
                chromeOptions.AddArgument($"--window-size={TestContext.Parameters["browserResolution"]}");
            }

            var driver = new ChromeDriver(chromeOptions);

            // Registering WebDriver with DI container
            _container.RegisterInstanceAs<IWebDriver>(driver);
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

        public void Login()
        {
        // https://demo.1crmcloud.com/json.php?action=login

        }
    }
}