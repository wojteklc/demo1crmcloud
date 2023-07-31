using NUnit.Framework;
using OneCrmTestProject.Common;
using OpenQA.Selenium;

namespace OneCrmTestProject.PageObjects.Contacts
{
    /// <summary>
    /// This PO is only small part of 'Contacts view' page, just to fulfill example scenatios requirements
    /// </summary>
    public class OneCrmReportViewPO
    {
        private readonly IWebDriver _driver;

        // I really liked to use PageFactory, but because it's deprecated below way of initializing page object web elemens seems optimal to me
        // I personally prefer XPaths as they are very flexible (allow to use all unique html attributes and XPath Axes to locate web elements)
        // Holding locators in below fields allow us to use them in any class method we want and as many times we want
        // They are easy to maintain also (only one occurrence of locator)
        private readonly By _reportNameLabelLocator = By.XPath("//h4[contains(@class, 'form-title search-title')]");
        private readonly By _runReportButtonLocator = By.XPath("//span[text()='Run Report']//ancestor::button[@type='submit']");
        private readonly By _resultsListItemsLocator = By.XPath("//tr[contains(@class, 'listViewRow')]");

        // Lazy initialization of properties holding references to web elements using expression-bodied member
        private IWebElement ProjectNameLabel => _driver.FindElement(_reportNameLabelLocator);
        private IWebElement RunReportButton => _driver.FindElement(_runReportButtonLocator);
        private List<IWebElement> ResultsListItems => _driver.FindElements(_resultsListItemsLocator).ToList();

        public OneCrmReportViewPO(IWebDriver driver)
        {
            _driver = driver;
        }

        public void VerifyPageIsOpened()
        {
            CommonWaits.WaitForLoadingIndicatorToDisappear(_driver);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _reportNameLabelLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _runReportButtonLocator);
            CommonAssertions.AssertDisplayed(ProjectNameLabel);
            CommonAssertions.AssertDisplayed(RunReportButton);
        }

        public void VerifyResultRowsAmount(int expectedAmount)
        {
            CommonWaits.WaitForLoadingIndicatorToDisappear(_driver);
            Assert.AreEqual(expectedAmount, ResultsListItems.Count);
        }

        public void VerifyOpenedReportName(string reportName)
        {
            CommonAssertions.AssertElementInnerText(ProjectNameLabel, reportName);
        }

        public void RunOpenedReport()
        {
            CommonInteractions.ClickWebElement(RunReportButton);
            CommonWaits.WaitForLoadingIndicatorToDisappear(_driver);
        }
    }
}
