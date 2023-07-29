using OneCrmTestProject.Common;
using OpenQA.Selenium;

namespace OneCrmTestProject.PageObjects
{
    public class OneCrmNewContactViewPO
    {
        private readonly IWebDriver _driver;

        // I really liked to use PageFactory, but because it's deprecated below way of initializing page object web elemens seems optimal to me
        // I personally prefer XPaths as they are very flexible (allow to use all unique html attributes and XPath Axes to locate web elements)
        // However below I used IDs to locate web elements
        // Holding locators in below fields allow us to use them in any class method we want and as many times we want. They are easy to maintain also (only one occurrence of locator)
        private readonly By _newRecordTitleLabelLocator = By.XPath("//div[@id='main-title']//h1[text()='Contacts: (new record)']");

        // Lazy initialization of properties holding references to web elements using expression-bodied member
        private IWebElement NewRecordTitleLabel => _driver.FindElement(_newRecordTitleLabelLocator);

        public OneCrmNewContactViewPO(IWebDriver driver)
        {
            _driver = driver;
        }

        public void VerifyPageIsOpened()
        {
            CommonWaits.WaitForElementToBecomeVisible(_driver, _newRecordTitleLabelLocator);
            CommonAssertions.AssertDisplayed(NewRecordTitleLabel);
        }
    }
}
