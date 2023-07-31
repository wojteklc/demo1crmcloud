using OneCrmTestProject.Common;
using OneCrmTestProject.Helpers;
using OpenQA.Selenium;

namespace OneCrmTestProject.PageObjects.Common
{
    /// <summary>
    /// This PO is only small part of 'Main menu' module, just to fulfill example scenatios requirements
    /// </summary>
    public class OneCrmMainMenuBarPO
    {
        private readonly IWebDriver _driver;

        // I really liked to use PageFactory, but because it's deprecated below way of initializing page object web elemens seems optimal to me
        // I personally prefer XPaths as they are very flexible (allow to use all unique html attributes and XPath Axes to locate web elements)
        // Holding locators in below fields allow us to use them in any class method we want and as many times we want
        // They are easy to maintain also (only one occurrence of locator)
        private readonly By _salesAndMarketingTabLocator = By.XPath("//a[@title='Sales & Marketing']");
        private readonly By _reportsAndSettingsTabLocator = By.XPath("//a[@title='Reports & Settings']");
        private readonly By _contactsSubMenuElementLocator = By.XPath("//a[@class='menu-tab-sub-list' and text()=' Contacts']");
        private readonly By _reportsSubMenuElementLocator = By.XPath("//a[@class='menu-tab-sub-list' and text()=' Reports']");
        private readonly By _activityLogsSubMenuElementLocator = By.XPath("//a[@class='menu-tab-sub-list' and text()=' Activity Log']");

        // Lazy initialization of properties holding references to web elements using expression-bodied member
        private IWebElement SalesAndMarketingTab => _driver.FindElement(_salesAndMarketingTabLocator);
        private IWebElement ReportsAndSettingsTab => _driver.FindElement(_reportsAndSettingsTabLocator);
        private IWebElement ContactsSubMenuElement => _driver.FindElement(_contactsSubMenuElementLocator);
        private IWebElement ReportsSubMenuElement => _driver.FindElement(_reportsSubMenuElementLocator);
        private IWebElement ActivityLogsSubMenuElement => _driver.FindElement(_activityLogsSubMenuElementLocator);

        public OneCrmMainMenuBarPO(IWebDriver driver)
        {
            _driver = driver;
        }

        public void VerifyPageIsOpened()
        {
            CommonWaits.WaitForElementToBecomeVisible(_driver, _salesAndMarketingTabLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _reportsAndSettingsTabLocator);
            CommonAssertions.AssertDisplayed(SalesAndMarketingTab);
            CommonAssertions.AssertDisplayed(ReportsAndSettingsTab);
        }

        public void NavigateTo(MenuPathDto menuPath)
        {
            switch (menuPath.MainMenuTab)
            {
                case MainMenuTabs.SalesAndMarketing:
                    CommonWaits.WaitForElementToBecomeClickable(_driver, _salesAndMarketingTabLocator);
                    CommonInteractions.ClickWebElement(SalesAndMarketingTab);

                    if (menuPath.SubmenuOption != null)
                    {
                        switch (menuPath.SubmenuOption)
                        {
                            case SubmenuOptions.Contacts:
                                CommonWaits.WaitForElementToBecomeVisible(_driver, _contactsSubMenuElementLocator);
                                CommonWaits.WaitForElementToBecomeClickable(_driver, _contactsSubMenuElementLocator);
                                CommonInteractions.ClickWebElement(ContactsSubMenuElement);
                                break;
                            // Add more cases if needed

                            default:
                                throw new NotImplementedException($"'{menuPath.SubmenuOption}' option not supported");
                        }
                    }
                    break;

                case MainMenuTabs.ReportsAndSettings:
                    CommonWaits.WaitForElementToBecomeClickable(_driver, _reportsAndSettingsTabLocator);
                    CommonInteractions.ClickWebElement(ReportsAndSettingsTab);

                    if (menuPath.SubmenuOption != null)
                    {
                        switch (menuPath.SubmenuOption)
                        {
                            case SubmenuOptions.Reports:
                                CommonWaits.WaitForElementToBecomeVisible(_driver, _reportsSubMenuElementLocator);
                                CommonWaits.WaitForElementToBecomeClickable(_driver, _reportsSubMenuElementLocator);
                                CommonInteractions.ClickWebElement(ReportsSubMenuElement);
                                break;

                            case SubmenuOptions.ActivityLogs:
                                CommonWaits.WaitForElementToBecomeVisible(_driver, _activityLogsSubMenuElementLocator);
                                CommonWaits.WaitForElementToBecomeClickable(_driver, _activityLogsSubMenuElementLocator);
                                CommonInteractions.ClickWebElement(ActivityLogsSubMenuElement);
                                break;
                            // Add more cases if needed

                            default:
                                throw new NotImplementedException($"'{menuPath.SubmenuOption}' option not supported");
                        }
                    }
                    break;
                // Add more cases if needed

                default:
                    throw new NotImplementedException($"'{menuPath.SubmenuOption}' option not supported");
            }
        }
    }
}
