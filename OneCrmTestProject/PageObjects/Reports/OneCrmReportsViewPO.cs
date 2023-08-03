using NUnit.Framework;
using OneCrmTestProject.Common;
using OpenQA.Selenium;

namespace OneCrmTestProject.PageObjects.Contacts
{
    /// <summary>
    /// This PO is only small part of 'Reports list' page, just to fulfill example scenatios requirements
    /// </summary>
    public class OneCrmReportsViewPO
    {
        private readonly IWebDriver _driver;

        // I really liked to use PageFactory, but because it's deprecated below way of initializing page object web elemens seems optimal to me
        // I personally prefer XPaths as they are very flexible (allow to use all unique html attributes and XPath Axes to locate web elements)
        // Holding locators in below fields allow us to use them in any class method we want and as many times we want
        // They are easy to maintain also (only one occurrence of locator)
        private readonly By _reportsTitleLabelLocator = By.XPath("//div[@id='main-title']//span[text()='Reports']");
        private readonly By _reportsSearchbarLocator = By.XPath("//input[contains(@autosave, 'Reports')]");
        private readonly By _reportsListRowLocator = By.XPath("//tr[contains(@class, 'listViewRow')]");

        // Lazy initialization of properties holding references to web elements using expression-bodied member
        private IWebElement ReportsTitleLabel => _driver.FindElement(_reportsTitleLabelLocator);
        private IWebElement ReportsSearchbar => _driver.FindElement(_reportsSearchbarLocator);
        private List<IWebElement> ReportsListRows => _driver.FindElements(_reportsListRowLocator).ToList();

        public OneCrmReportsViewPO(IWebDriver driver)
        {
            _driver = driver;
        }

        public void VerifyPageIsOpened()
        {
            CommonWaits.WaitForLoadingIndicatorToDisappear(_driver);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _reportsTitleLabelLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _reportsSearchbarLocator);
            CommonAssertions.AssertDisplayed(ReportsTitleLabel);
            CommonAssertions.AssertDisplayed(ReportsSearchbar);
        }

        public void VerifyReportRowsAmount(int expectedAmount)
        {
            Assert.AreEqual(expectedAmount, ReportsListRows.Count);
        }

        public OneCrmReportViewPO OpenReportByName(string reportName)
        {
            var reportRow = GetReportRowByName(reportName);
            var rowCells = CommonInteractions.GetTableRowCells(_driver, reportRow);
            var reportRowNameLink = CommonInteractions.FindChildElement(_driver, rowCells[2], By.TagName("a"));

            CommonInteractions.ClickWebElement(reportRowNameLink);

            return new OneCrmReportViewPO(_driver);
        }

        private IWebElement GetReportRowByName(string reportName, bool riseException = true)
        {
            IWebElement? searchedRow = null;

            CommonWaits.WaitForLoadingIndicatorToDisappear(_driver);
            CommonInteractions.PerformSearchOnList(_driver, ReportsSearchbar, reportName);

            if (ReportsListRows.Any())
            {
                var rowCells = CommonInteractions.GetTableRowCells(_driver, ReportsListRows[0]);

                if (rowCells[2].Text == reportName)
                {
                    searchedRow = ReportsListRows[0];
                }
            }

            if (riseException && searchedRow == null)
            {
                throw new NotFoundException($"Report row with name: '{reportName}' was not found");
            }

            return searchedRow;
        }
    }
}
