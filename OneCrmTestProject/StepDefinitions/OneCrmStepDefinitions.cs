using OneCrmTestProject.Common;
using OneCrmTestProject.Helpers;
using OneCrmTestProject.PageObjects;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace OneCrmTestProject.StepDefinitions
{
    [Binding]
    public class OneCrmStepDefinitions
    {
        private readonly IWebDriver _driver;
        private ScenarioContext _scenarioContext;
        private object? _randomContactNumber;

        private OneCrmLoginViewPO? _oneCrmLoginViewPO;
        private OneCrmHomeDashboardViewPO? _oneCrmHomeDashboardViewPO;
        private OneCrmMainMenuBarPO? _oneCrmMainMenuBarPO;
        private OneCrmContactsViewPO? _oneCrmContactsViewPO;
        private OneCrmContactFormPO? _oneCrmContactFormPO;
        private OneCrmContactViewPO? _oneCrmContactViewPO;

        public OneCrmStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
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
        public void WhenUserNavigatesToMenuItem(MenuPath path)
        {
            _oneCrmMainMenuBarPO = new OneCrmMainMenuBarPO(_driver);
            _oneCrmMainMenuBarPO.NavigateTo(path);
        }

        [Then(@"User should see list of all contacts")]
        public void ThenUserShouldSeeListOfAllContacts()
        {
            _oneCrmContactsViewPO = new OneCrmContactsViewPO(_driver);
            _oneCrmContactsViewPO.VerifyPageIsOpened();
        }

        [When(@"User opens 'New contact' form using 'Create' button")]
        public void WhenUserOpensNewContactFormUsingCreateButton()
        {
            _oneCrmContactsViewPO = new OneCrmContactsViewPO(_driver);
            _oneCrmContactFormPO = _oneCrmContactsViewPO.PressCreateButton();
            _oneCrmContactFormPO.VerifyPageIsOpened();
        }

        [When(@"User enters new contact's details with random number in 'First Name' to avoid contact duplicates")]
        public void WhenUserEntersNewRandomContactDetails(Table table)
        {
            var contactDetails = table.CreateInstance<ContactDetailsDto>();
            _randomContactNumber = CommonInteractions.TryGetValueFromScenarioContext(_scenarioContext, "RandomNumber");

            contactDetails.FirstName = $"{contactDetails.FirstName}{_randomContactNumber}";
            
            _oneCrmContactFormPO = new OneCrmContactFormPO(_driver);
            _oneCrmContactFormPO.SetContactDetails(contactDetails);
        }

        [When(@"User saves new contact")]
        public void WhenUserSavesNewContact()
        {
            _oneCrmContactFormPO = new OneCrmContactFormPO(_driver);
            _oneCrmContactViewPO = _oneCrmContactFormPO.PressSaveButton();
            _oneCrmContactViewPO.VerifyPageIsOpened();
        }

        [Then(@"User should see saved '([^']*) ([^']*)' contact")]
        public void ThenUserShouldSeeSavedContact(string firstName, string lastName)
        {
            _oneCrmContactViewPO = new OneCrmContactViewPO(_driver);
            _oneCrmContactViewPO.VerifyContactNameInPageMainTitle($"{firstName}{_randomContactNumber} {lastName}");
            _oneCrmContactViewPO.VerifyContactNameExistsInContactFormHeader($"{firstName}{_randomContactNumber} {lastName}");
        }

        [When(@"User opens '([^']*) ([^']*)' contact")]
        public void WhenUserOpensContact(string firstName, string lastName)
        {
            _oneCrmContactViewPO = new OneCrmContactViewPO(_driver);
            _oneCrmContactViewPO.VerifyContactNameInPageMainTitle($"{firstName}{_randomContactNumber} {lastName}");
            _oneCrmContactViewPO.VerifyContactNameExistsInContactFormHeader($"{firstName}{_randomContactNumber} {lastName}");
            _oneCrmContactFormPO = _oneCrmContactViewPO.PressEditButton();
            _oneCrmContactFormPO.VerifyPageIsOpened();
        }

        [Then(@"Contact details are correct")]
        public void ThenContactDetailsAreCorrect(Table table)
        {
            var contactDetails = table.CreateInstance<ContactDetailsDto>();
            contactDetails.FirstName = $"{contactDetails.FirstName}{_randomContactNumber}";

            _oneCrmContactFormPO = new OneCrmContactFormPO(_driver);
            _oneCrmContactFormPO.VerifyContactDetails(contactDetails);
        }
    }
}
