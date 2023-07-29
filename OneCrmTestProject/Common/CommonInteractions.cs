using OpenQA.Selenium;

namespace OneCrmTestProject.Common
{
    public static class CommonInteractions
    {
        public static void ClickWebElement(IWebElement webElement)
        {
            webElement.Click();
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
    }
}
