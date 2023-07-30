using OneCrmTestProject.Common;
using OpenQA.Selenium;

namespace OneCrmTestProject.PageObjects
{
    public class OneCrmContactViewPO
    {
        private readonly IWebDriver _driver;

        // I really liked to use PageFactory, but because it's deprecated below way of initializing page object web elemens seems optimal to me
        // I personally prefer XPaths as they are very flexible (allow to use all unique html attributes and XPath Axes to locate web elements)
        // However below I used IDs to locate web elements
        // Holding locators in below fields allow us to use them in any class method we want and as many times we want. They are easy to maintain also (only one occurrence of locator)
        private readonly By _existingContactTitleLabelLocator = By.Id("main-title-text");
        private readonly By _editButtonLocator = By.Id("DetailForm_edit");
        private readonly By _contactFormHeaderLocator = By.XPath("//div[@id='_form_header']//h3");

        // Lazy initialization of properties holding references to web elements using expression-bodied member
        private IWebElement ExistingContactTitleLabel => _driver.FindElement(_existingContactTitleLabelLocator);
        private IWebElement EditButton => _driver.FindElement(_editButtonLocator);
        private IWebElement ContactFormHeader => _driver.FindElement(_contactFormHeaderLocator);

        public OneCrmContactViewPO(IWebDriver driver)
        {
            _driver = driver;
        }

        public void VerifyPageIsOpened()
        {
            CommonWaits.WaitForElementToBecomeVisible(_driver, _existingContactTitleLabelLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _editButtonLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _contactFormHeaderLocator);
            CommonAssertions.AssertDisplayed(ExistingContactTitleLabel);
            CommonAssertions.AssertDisplayed(EditButton);
            CommonAssertions.AssertDisplayed(ContactFormHeader);
        }

        public OneCrmContactFormPO PressEditButton()
        {
            CommonWaits.WaitForElementToBecomeClickable(_driver, _editButtonLocator);
            CommonInteractions.ClickWebElement(EditButton);

            return new OneCrmContactFormPO(_driver);
        }

        public void VerifyContactNameInPageMainTitle(string expectedContactName)
        {
            var expectedTitle = $"CONTACTS: {expectedContactName.ToUpper()}";

            CommonWaits.WaitForElementInnerTextChange(_driver, ExistingContactTitleLabel, expectedTitle);
            CommonAssertions.AssertElementInnerText(ExistingContactTitleLabel, expectedTitle);
        }

        public void VerifyContactNameExistsInContactFormHeader(string expectedContactName)
        {
            CommonWaits.WaitForElementInnerTextChange(_driver, ContactFormHeader, expectedContactName);
            CommonAssertions.AssertElementInnerText(ContactFormHeader, expectedContactName);
        }
    }
}
