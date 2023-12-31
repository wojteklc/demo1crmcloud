﻿using OpenQA.Selenium;
using OneCrmTestProject.Helpers;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace OneCrmTestProject.Common
{
    /// <summary>
    /// It's good to have built-in methods wrapped in custom methods in case we want to modify behaviour of any of these globally
    /// </summary>
    public static class CommonWaits
    {
        public static void WaitForLoadingIndicatorToDisappear(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Ten));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='statusDiv' and text()='Loading ...']")));
        }

        public static void WaitForUrlToBe(IWebDriver driver, string url)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Five));
            wait.Until(ExpectedConditions.UrlToBe(url));
        }

        public static void WaitForElementToBecomeVisible(IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Five));
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        public static void WaitForElementToDisappear(IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Five));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        public static void WaitForElementToDisappearFromDom(IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Five));
            wait.Until(driver => driver.FindElements(by).Count == 0);
        }

        public static void WaitForElementToBecomeClickable(IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Five));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        public static void WaitForElementInnerTextChange(IWebDriver driver, IWebElement element, string expectedText)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Five));
            wait.Until(ExpectedConditions.TextToBePresentInElement(element, expectedText));
        }
    }
}
