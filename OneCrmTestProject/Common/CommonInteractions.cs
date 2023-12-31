﻿using OneCrmTestProject.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace OneCrmTestProject.Common
{
    /// <summary>
    /// It's good to have built-in methods wrapped in custom methods in case we want to modify behaviour of any of these globally
    /// </summary>
    public static class CommonInteractions
    {
        public static void ClickWebElement(IWebElement webElement)
        {
            webElement.Click();
        }

        public static void SubmitWebElement(IWebElement webElement)
        {
            webElement.Submit();
        }

        public static void SetInputValue(IWebElement inputElement, string value)
        {
            ClearInputValue(inputElement);
            inputElement.SendKeys(value);
        }

        public static void ClearInputValue(IWebElement inputElement)
        {
            inputElement.Clear();
        }

        public static List<IWebElement> GetTableRowCells(IWebDriver driver, IWebElement tabelRow)
        {
            return FindChildElements(driver, tabelRow, By.TagName("td"));
        }

        public static void SetCheckboxState(IWebElement checkbox, bool state)
        {
            if (GetCheckboxState(checkbox) != state)
            {
                ClickWebElement(checkbox);
            }
        }

        public static bool GetCheckboxState(IWebElement checkbox)
        {
            return checkbox.GetDomProperty("checked") == "true";
        }

        public static void SelectItemFromInputSearchPopup(IWebDriver driver, IWebElement elementTriggeringPopup, string itemName)
        {
            var popupLocator = By.XPath("//div[contains(@id, 'input-search') and contains(@class, 'popup-default')]");
            var popupSearchbarLocator = By.XPath(".//div[@id='DetailFormcategories-input-search-text']//input");
            var popupListItemsLocator = By.XPath(".//div[contains(@class, 'option-cell')]");

            ClickWebElement(elementTriggeringPopup);
            CommonWaits.WaitForElementToBecomeVisible(driver, popupLocator);
            var popupElement = FindElement(driver, popupLocator);

            var popupSearchbarInput = FindChildElement(driver, popupElement, popupSearchbarLocator);
            SetInputValue(popupSearchbarInput, itemName);

            var foundItem = FindChildElements(driver, popupElement, popupListItemsLocator).Find(x => x.Text == itemName);
            ClickWebElement(foundItem);
            CommonWaits.WaitForElementToDisappearFromDom(driver, popupLocator);
        }

        public static void SelectItemFromInputPopup(IWebDriver driver, IWebElement elementTriggeringPopup, string itemName)
        {
            var popupLocator = By.XPath("//div[contains(@id, 'input-popup') and contains(@class, 'panel-outer')]");
            var popupListItemsLocator = By.XPath(".//div[contains(@class, 'option-cell')]");

            ClickWebElement(elementTriggeringPopup);
            CommonWaits.WaitForElementToBecomeVisible(driver, popupLocator);
            var popupElement = FindElement(driver, popupLocator);
            var foundItem = FindChildElements(driver, popupElement, popupListItemsLocator).Find(x => x.Text == itemName);

            ClickWebElement(foundItem);
            CommonWaits.WaitForElementToDisappearFromDom(driver, popupLocator);
        }

        public static void SelectOptionFromButtonHeadPopup(IWebDriver driver, IWebElement elementTriggeringPopup, string itemName)
        {
            var popupLocator = By.XPath("//div[contains(@id, 'ActionButtonHead-popup') and contains(@class, 'panel-outer')]");
            var popupListItemsLocator = By.XPath(".//div[contains(@class, 'menu-option')]");

            ClickWebElement(elementTriggeringPopup);
            CommonWaits.WaitForElementToBecomeVisible(driver, popupLocator);
            var popupElement = FindElement(driver, popupLocator);
            var foundItem = FindChildElements(driver, popupElement, popupListItemsLocator).Find(x => x.Text == itemName);

            ClickWebElement(foundItem);

            if (itemName == "Delete")
            {
                driver.SwitchTo().Alert().Accept();
            }

            CommonWaits.WaitForElementToDisappearFromDom(driver, popupLocator);
        }

        public static IWebElement FindElement(IWebDriver driver, By by, bool riseException = true, int waitTimeout = Timeouts.Five)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeout));
            IWebElement? element = null;

            try
            {
                element = wait.Until(x => x.FindElement(by));
            }
            catch (NotFoundException ex)
            {
                if (riseException) 
                { 
                    throw ex;
                }
            }

            return element;
        }

        public static IWebElement FindChildElement(IWebDriver driver, IWebElement parentElement, By by, bool riseException = true, int waitTimeout = Timeouts.Five)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeout));
            IWebElement? element = null;

            try
            {
                element = wait.Until(x => parentElement.FindElement(by));
            }
            catch (NotFoundException ex)
            {
                if (riseException)
                {
                    throw ex;
                }
            }

            return element;
        }

        public static List<IWebElement> FindChildElements(IWebDriver driver, IWebElement parentElement, By by, bool riseException = true, int waitTimeout = Timeouts.Five)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeout));
            List<IWebElement>? elements = null;

            try
            {
                elements = wait.Until(x => parentElement.FindElements(by)).ToList();
            }
            catch (NotFoundException ex)
            {
                if (riseException)
                {
                    throw ex;
                }
            }

            return elements;
        }

        public static bool GetElementDisplayedStatus(IWebDriver driver, By by)
        {
            var elementToFind = FindElement(driver, by, false);
            
            if (elementToFind == null)
            {
                return false;
            }

            return elementToFind.Displayed;
        }

        public static void PerformSearchOnList(IWebDriver driver, IWebElement searchbar, string searchString)
        {
            SetInputValue(searchbar, searchString);
            SubmitWebElement(searchbar);
            CommonWaits.WaitForLoadingIndicatorToDisappear(driver);
        }

        public static object? TryGetValueFromScenarioContext(ScenarioContext scenarioContext, string key)
        {
            try
            {
                return scenarioContext[key];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}
