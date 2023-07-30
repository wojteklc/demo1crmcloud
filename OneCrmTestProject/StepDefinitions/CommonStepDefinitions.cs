using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace OneCrmTestProject.StepDefinitions
{
    [Binding]
    public class CommonStepDefinitions
    {
        private readonly IWebDriver _driver;
        private ScenarioContext _scenarioContext;

        /// <summary>
        /// 'driver' and 'scenarioContext' are received thanks to Specflow's build-in Dependency Injection
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="scenarioContext"></param>
        public CommonStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
        }

        [Given(@"Random number between '(\d+)' and '(\d+)' is generated")]
        public void GivenRandomNumberBetweenAndIsGenerated(int min, int max)
        {
            _scenarioContext["RandomNumber"] = new Random().Next(min, max);
        }
    }
}
