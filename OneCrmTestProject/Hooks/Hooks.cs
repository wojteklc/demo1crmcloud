using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using BoDi;

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
            chromeOptions.AddArgument("--start-maximized");
            //--disable-notifications

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