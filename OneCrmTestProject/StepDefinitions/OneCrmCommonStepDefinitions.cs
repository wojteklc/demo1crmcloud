using OneCrmTestProject.Helpers;
using OneCrmTestProject.PageObjects.Common;
using OneCrmTestProject.PageObjects.Dashboard;
using OneCrmTestProject.PageObjects.Login;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace OneCrmTestProject.StepDefinitions
{
    [Binding]
    public class OneCrmCommonStepDefinitions
    {
        private readonly IWebDriver _driver;
        private ScenarioContext _scenarioContext;

        private OneCrmLoginViewPO? _oneCrmLoginViewPO;
        private OneCrmHomeDashboardViewPO? _oneCrmHomeDashboardViewPO;
        private OneCrmMainMenuBarPO? _oneCrmMainMenuBarPO;

        /// <summary>
        /// 'driver' and 'scenarioContext' are received thanks to Specflow's build-in Dependency Injection
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="scenarioContext"></param>
        public OneCrmCommonStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
        }

        [Given(@"Random number between '(\d+)' and '(\d+)' is generated")]
        public void GivenRandomNumberBetweenAndIsGenerated(int min, int max)
        {
            _scenarioContext["RandomNumber"] = new Random().Next(min, max);
        }

        [Given(@"User logs in using '([^']*)' user name and '([^']*)' password")]
        public void GivenUserLogsInUsingUserNameAndPassword(string userName, string password)
        {
            _oneCrmLoginViewPO = new OneCrmLoginViewPO(_driver);
            _oneCrmLoginViewPO.GoTo();
            _oneCrmLoginViewPO.VerifyPageIsOpened();
            _oneCrmHomeDashboardViewPO = _oneCrmLoginViewPO.LogIn(userName, password);
            _oneCrmHomeDashboardViewPO.VerifyPageIsOpened();
            _oneCrmMainMenuBarPO = new OneCrmMainMenuBarPO(_driver);
            _oneCrmMainMenuBarPO.VerifyPageIsOpened();
        }

        [When(@"User navigates to '(.*)' menu item")]
        public void WhenUserNavigatesToMenuItem(MenuPathDto path)
        {
            _oneCrmMainMenuBarPO = new OneCrmMainMenuBarPO(_driver);
            _oneCrmMainMenuBarPO.NavigateTo(path);
        }
    }
}
