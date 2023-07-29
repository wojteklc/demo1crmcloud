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

        // Lazy initialization of properties holding references to web elements using expression-bodied member
        private IWebElement ContactsTitleLabel => _driver.FindElement(_contactsTitleLabelLocator);

        public OneCrmContactsViewPO(IWebDriver driver)
        {
            _driver = driver;
        }

        public void VerifyPageIsOpened()
        {
            CommonWaits.WaitForElementToBecomeVisible(_driver, _contactsTitleLabelLocator);
            CommonAssertions.AssertDisplayed(ContactsTitleLabel);
        }
    }
}
