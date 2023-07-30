using OneCrmTestProject.Common;
using OpenQA.Selenium;

namespace OneCrmTestProject.PageObjects
{
    public class OneCrmContactsViewPO
    {
        private readonly IWebDriver _driver;

        // I really liked to use PageFactory, but because it's deprecated below way of initializing page object web elemens seems optimal to me
        // I personally prefer XPaths as they are very flexible (allow to use all unique html attributes and XPath Axes to locate web elements)
        // However below I used IDs to locate web elements
        // Holding locators in below fields allow us to use them in any class method we want and as many times we want. They are easy to maintain also (only one occurrence of locator)
        private readonly By _contactsTitleLabelLocator = By.XPath("//div[@id='main-title']//span[text()='Contacts']");
        private readonly By _contactsSearchbarLocator = By.Id("filter_text");
        private readonly By _contactsCreateButtonLocator = By.XPath("//span[text()='Create']//ancestor::button[@name='SubPanel_create']");

        // Lazy initialization of properties holding references to web elements using expression-bodied member
        private IWebElement ContactsTitleLabel => _driver.FindElement(_contactsTitleLabelLocator);
        private IWebElement ContactsSearchbar => _driver.FindElement(_contactsSearchbarLocator);
        private IWebElement CreateButton => _driver.FindElement(_contactsCreateButtonLocator);

        public OneCrmContactsViewPO(IWebDriver driver)
        {
            _driver = driver;
        }

        public OneCrmContactFormPO PressCreateButton()
        {
            CommonWaits.WaitForElementToBecomeClickable(_driver, _contactsCreateButtonLocator);
            CommonInteractions.ClickWebElement(CreateButton);

            return new OneCrmContactFormPO(_driver);
        }

        public void VerifyPageIsOpened()
        {
            CommonWaits.WaitForElementToBecomeVisible(_driver, _contactsTitleLabelLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _contactsSearchbarLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _contactsCreateButtonLocator);
            CommonAssertions.AssertDisplayed(ContactsTitleLabel);
            CommonAssertions.AssertDisplayed(ContactsSearchbar);
            CommonAssertions.AssertDisplayed(CreateButton);
        }
    }
}
