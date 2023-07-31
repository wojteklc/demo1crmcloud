using OneCrmTestProject.PageObjects.Contacts;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace OneCrmTestProject.StepDefinitions
{
    [Binding]
    public class OneCrmReportsStepDefinitions
    {
        private readonly IWebDriver _driver;
        private ScenarioContext _scenarioContext;

        private OneCrmReportsViewPO? _oneCrmReportsViewPO;
        private OneCrmReportViewPO? _oneCrmReportViewPO;

        /// <summary>
        /// 'driver' and 'scenarioContext' are received thanks to Specflow's build-in Dependency Injection
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="scenarioContext"></param>
        public OneCrmReportsStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
        }

        [Then(@"User should see list of all reports")]
        public void ThenUserShouldSeeListOfAllReports()
        {
            _oneCrmReportsViewPO = new OneCrmReportsViewPO(_driver);
            _oneCrmReportsViewPO.VerifyPageIsOpened();
        }

        [When(@"User opens '([^']*)' report")]
        public void WhenUserOpensReport(string reportName)
        {
            _oneCrmReportsViewPO = new OneCrmReportsViewPO(_driver);
            _oneCrmReportViewPO = _oneCrmReportsViewPO.OpenReportByName(reportName);
            _oneCrmReportViewPO.VerifyPageIsOpened();
        }

        [Then(@"User should see '([^']*)' report")]
        public void ThenUserShouldSeeReport(string reportName)
        {
            _oneCrmReportViewPO = new OneCrmReportViewPO(_driver);
            _oneCrmReportViewPO.VerifyOpenedReportName(reportName);
        }

        [When(@"User runs opened report")]
        public void WhenUserRunsOpenedReport()
        {
            _oneCrmReportViewPO = new OneCrmReportViewPO(_driver);
            _oneCrmReportViewPO.RunOpenedReport();
        }

        [Then(@"User should see '(\d+)' result rows")]
        public void ThenUserShouldSeeAtLeastResultRows(int expectedAmount)
        {
            _oneCrmReportViewPO = new OneCrmReportViewPO(_driver);
            _oneCrmReportViewPO.VerifyResultRowsAmount(expectedAmount);
        }
    }
}
