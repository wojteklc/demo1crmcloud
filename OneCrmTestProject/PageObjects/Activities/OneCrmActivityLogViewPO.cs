using NUnit.Framework;
using OneCrmTestProject.Common;
using OpenQA.Selenium;

namespace OneCrmTestProject.PageObjects.Contacts
{
    /// <summary>
    /// This PO is only small part of 'Contacts view' page, just to fulfill example scenatios requirements
    /// </summary>
    public class OneCrmActivityLogViewPO
    {
        private readonly IWebDriver _driver;

        // I really liked to use PageFactory, but because it's deprecated below way of initializing page object web elemens seems optimal to me
        // I personally prefer XPaths as they are very flexible (allow to use all unique html attributes and XPath Axes to locate web elements)
        // Holding locators in below fields allow us to use them in any class method we want and as many times we want
        // They are easy to maintain also (only one occurrence of locator)
        private readonly By _activityLogTitleLabelLocator = By.XPath("//div[@id='main-title']//span[text()='Activity Log']");
        private readonly By _activityLogListRowLocator = By.XPath("//tr[contains(@class, 'listViewRow')]");

        // Lazy initialization of properties holding references to web elements using expression-bodied member
        private IWebElement ActivityLogTitleLabel => _driver.FindElement(_activityLogTitleLabelLocator);
        private List<IWebElement> ActivityLogListRows => _driver.FindElements(_activityLogListRowLocator).ToList();

        public OneCrmActivityLogViewPO(IWebDriver driver)
        {
            _driver = driver;
        }

        public void VerifyPageIsOpened()
        {
            CommonWaits.WaitForLoadingIndicatorToDisappear(_driver);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _activityLogTitleLabelLocator);
            CommonAssertions.AssertDisplayed(ActivityLogTitleLabel);
        }

        public void VerifyActivityRowsAmountGreaterThan(int greateThanAmount)
        {
            Assert.GreaterOrEqual(ActivityLogListRows.Count, greateThanAmount);
        }

        public void VerifyActivityLogRowsDoesNotExistByActivityDescription(List<string> activityDescriptions)
        {
            foreach (var activityDescription in activityDescriptions)
            {
                VerifyActivityLogRowDoesNotExistByActivityDescription(activityDescription);
            }
        }

        public void VerifyActivityLogRowDoesNotExistByActivityDescription(string activityDescription)
        {
            Assert.IsNull(GetActivityLogRowByActivityDescription(activityDescription, false));
        }

        public List<string> MarkUnmarkFirstRowsAmount(int rowsAmount, bool markUnmark)
        {
            var listOfActirityRowsDescriptions = new List<string>();

            CommonWaits.WaitForLoadingIndicatorToDisappear(_driver);

            if (ActivityLogListRows.Any())
            {
                for (int i = 0; i < rowsAmount; i++)
                {
                    var activityLogRow = GetActivityLogRowByIndex(i);
                    var activityRowCells = CommonInteractions.GetTableRowCells(_driver, activityLogRow);
                    var activityRowCheckbox = CommonInteractions.FindChildElement(_driver, activityRowCells[0], By.XPath("//input[@class='checkbox']"));
                    listOfActirityRowsDescriptions.Add(activityRowCells[1].Text.Trim());

                    CommonInteractions.SetCheckboxState(activityRowCheckbox, markUnmark);
                }
            }

            return listOfActirityRowsDescriptions;
        }

        private IWebElement GetActivityLogRowByIndex(int index, bool riseException = true)
        {
            IWebElement? searchedRow = null;

            CommonWaits.WaitForLoadingIndicatorToDisappear(_driver);
            
            if (ActivityLogListRows.Any())
            {
                searchedRow = ActivityLogListRows[index];
            }

            if (riseException && searchedRow == null)
            {
                throw new NotFoundException($"Activity log row with index: '{index}' was not found");
            }

            return searchedRow;
        }

        private IWebElement GetActivityLogRowByActivityDescription(string activityDescription, bool riseException = true)
        {
            IWebElement? searchedRow = null;

            CommonWaits.WaitForLoadingIndicatorToDisappear(_driver);

            if (ActivityLogListRows.Any())
            {
                foreach (var activityLogListRow in ActivityLogListRows)
                {
                    var activityRowCells = CommonInteractions.GetTableRowCells(_driver, activityLogListRow);

                    if (activityRowCells[1].Text.Trim() == activityDescription)
                    {
                        searchedRow = activityLogListRow;
                    }
                }
            }

            if (riseException && searchedRow == null)
            {
                throw new NotFoundException($"Activity log row with activity description: '{activityDescription}' was not found");
            }

            return searchedRow;
        }
    }
}
