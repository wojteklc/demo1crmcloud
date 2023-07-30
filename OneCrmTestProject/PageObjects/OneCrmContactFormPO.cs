using OneCrmTestProject.Common;
using OneCrmTestProject.Helpers;
using OpenQA.Selenium;

namespace OneCrmTestProject.PageObjects
{
    public class OneCrmContactFormPO
    {
        private readonly IWebDriver _driver;

        // I really liked to use PageFactory, but because it's deprecated below way of initializing page object web elemens seems optimal to me
        // I personally prefer XPaths as they are very flexible (allow to use all unique html attributes and XPath Axes to locate web elements)
        // However below I used IDs to locate web elements
        // Holding locators in below fields allow us to use them in any class method we want and as many times we want. They are easy to maintain also (only one occurrence of locator)
        private readonly By _newRecordTitleLabelLocator = By.Id("main-title-text");
        private readonly By _saveButtonLocator = By.Id("DetailForm_save");
        private readonly By _firstNameInputLocator = By.Id("DetailFormfirst_name-input");
        private readonly By _lastNameInputLocator = By.Id("DetailFormlast_name-input");
        private readonly By _categoryFieldLocator = By.Id("DetailFormcategories-input");
        private readonly By _businessRoleFieldLocator = By.Id("DetailFormbusiness_role-input");

        // Lazy initialization of properties holding references to web elements using expression-bodied member
        private IWebElement NewRecordTitleLabel => _driver.FindElement(_newRecordTitleLabelLocator);
        private IWebElement SaveButton => _driver.FindElement(_saveButtonLocator);
        private IWebElement FirstNameInput => _driver.FindElement(_firstNameInputLocator);
        private IWebElement LastNameInput => _driver.FindElement(_lastNameInputLocator);
        private IWebElement CategoryFieldInput => _driver.FindElement(_categoryFieldLocator);
        private IWebElement BusinessRoleField => _driver.FindElement(_businessRoleFieldLocator);

        public OneCrmContactFormPO(IWebDriver driver)
        {
            _driver = driver;
        }

        public void VerifyPageIsOpened()
        {
            CommonWaits.WaitForElementToBecomeVisible(_driver, _newRecordTitleLabelLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _saveButtonLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _firstNameInputLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _lastNameInputLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _categoryFieldLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _businessRoleFieldLocator);
            CommonAssertions.AssertDisplayed(NewRecordTitleLabel);
            CommonAssertions.AssertDisplayed(SaveButton);
            CommonAssertions.AssertDisplayed(FirstNameInput);
            CommonAssertions.AssertDisplayed(LastNameInput);
            CommonAssertions.AssertDisplayed(CategoryFieldInput);
            CommonAssertions.AssertDisplayed(BusinessRoleField);
        }

        public OneCrmContactViewPO PressSaveButton()
        {
            CommonWaits.WaitForElementToBecomeClickable(_driver, _saveButtonLocator);
            CommonInteractions.ClickWebElement(SaveButton);

            return new OneCrmContactViewPO(_driver);
        }

        public void SetContactDetails(ContactDetailsDto contactDetails)
        {
            SetFirstName(contactDetails.FirstName);
            SetLastName(contactDetails.LastName);
            SelectBusinessRole(contactDetails.BusinessRole);
            AddMultipleCategories(contactDetails.Categories.Split(',').ToList());
        }

        public void VerifyContactDetails(ContactDetailsDto contactDetails)
        {
            VerifyFirstName(contactDetails.FirstName);
            VerifyLastName(contactDetails.LastName);
            VerifyBusinessRole(contactDetails.BusinessRole);
            VerifyCategory(contactDetails.Categories.Replace(",", "\r\n"));
        }

        public void SetFirstName(string firstName)
        {
            CommonInteractions.SetInputValue(FirstNameInput, firstName);
        }

        public void VerifyFirstName(string expectedFirstName)
        {
            CommonAssertions.AssertElementValue(FirstNameInput, expectedFirstName);
        }

        public void SetLastName(string lastName)
        {
            CommonInteractions.SetInputValue(LastNameInput, lastName);
        }

        public void VerifyLastName(string expectedLastName)
        {
            CommonAssertions.AssertElementValue(LastNameInput, expectedLastName);
        }

        public void AddCategory(string categoryName)
        {
            CommonInteractions.SelectItemFromInputSearchPopup(_driver, CategoryFieldInput, categoryName);
        }

        public void VerifyCategory(string expectedCategoryName)
        {
            CommonAssertions.AssertElementInnerText(CategoryFieldInput, expectedCategoryName);
        }

        public void AddMultipleCategories(List<string> categoriesNames)
        {
            foreach (var categoryName in categoriesNames)
            {
                CommonInteractions.SelectItemFromInputSearchPopup(_driver, CategoryFieldInput, categoryName);
            }
        }
        public void SelectBusinessRole(string businessRoleName)
        {
            CommonInteractions.SelectItemFromInputPopup(_driver, BusinessRoleField, businessRoleName);
        }

        public void VerifyBusinessRole(string expectedBusinessRole)
        {
            CommonAssertions.AssertElementInnerText(BusinessRoleField, expectedBusinessRole);
        }
    }
}
