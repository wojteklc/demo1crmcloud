using OpenQA.Selenium;
using OneCrmTestProject.Helpers;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace OneCrmTestProject.Common
{
    public static class CommonWaits
    {
        public static void WaitForUrlToMatch(IWebDriver driver, string url)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Five));
            wait.Until(ExpectedConditions.UrlMatches(url));
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

        public static void WaitForElementToBecomeClickable(IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeouts.Five));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
    }
}
