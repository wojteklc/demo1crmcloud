using OpenQA.Selenium;
using OneCrmTestProject.Helpers;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace OneCrmTestProject.Common
{
    public static class CommonWaits
    {
        public static void WaitForUrlToBe(IWebDriver driver, string url)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Five));
            wait.Until(ExpectedConditions.UrlToBe(url));
        }

        public static void WaitForUrlToContain(IWebDriver driver, string url)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Five));
            wait.Until(ExpectedConditions.UrlContains(url));
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
