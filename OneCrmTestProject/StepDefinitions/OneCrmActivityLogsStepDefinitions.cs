using OneCrmTestProject.Common;
using OneCrmTestProject.PageObjects.Contacts;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace OneCrmTestProject.StepDefinitions
{
    [Binding]
    public class OneCrmActivityLogsStepDefinitions
    {
        private readonly IWebDriver _driver;
        private ScenarioContext _scenarioContext;

        private OneCrmActivityLogViewPO? _oneCrmActivityLogViewPO;

        /// <summary>
        /// 'driver' and 'scenarioContext' are received thanks to Specflow's build-in Dependency Injection
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="scenarioContext"></param>
        public OneCrmActivityLogsStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
        }

        [Then(@"User should see list of all acivity logs")]
        public void ThenUserShouldSeeListOfAllAcivityLogs()
        {
            _oneCrmActivityLogViewPO = new OneCrmActivityLogViewPO(_driver);
            _oneCrmActivityLogViewPO.VerifyPageIsOpened();
        }

        [When(@"User selects first '(\d+)' rows in the activity table")]
        public void WhenUserSelectsFirstRowsInTheActivityTable(int rowsAmount)
        {
            _oneCrmActivityLogViewPO = new OneCrmActivityLogViewPO(_driver);
            var selectedActivityLogRowsDescriptions = _oneCrmActivityLogViewPO.MarkUnmarkFirstRowsAmount(rowsAmount, true);
            _scenarioContext["SelectedActivityLogRowsDescriptions"] = selectedActivityLogRowsDescriptions;
        }

        [When(@"User selects '([^']*)' option from 'Actions' dropdown")]
        public void WhenUserSelectsOptionFromActionsDropdown(string actionsDropdownOption)
        {
            _oneCrmActivityLogViewPO = new OneCrmActivityLogViewPO(_driver);
            _oneCrmActivityLogViewPO.SelectOptionFromActionsDropdown(actionsDropdownOption);
        }

        [Then(@"Selected activity rows have been deleted")]
        public void ThenSelectedActivityRowsHaveBeenDeleted()
        {
            _oneCrmActivityLogViewPO = new OneCrmActivityLogViewPO(_driver);
            var selectedActivityLogRowsDescriptions = CommonInteractions.TryGetValueFromScenarioContext(_scenarioContext, "SelectedActivityLogRowsDescriptions");
            _oneCrmActivityLogViewPO.VerifyActivityLogRowsDoesNotExistByActivityDescription((List<string>)selectedActivityLogRowsDescriptions);
        }
    }
}
