using NUnit.Framework;
using OpenQA.Selenium;

namespace OneCrmTestProject.Common
{
    /// <summary>
    /// It's good to have built-in methods wrapped in custom methods in case we want to modify behaviour of any of these globally
    /// </summary>
    public static class CommonAssertions
    {
        public static void AssertElementValue(IWebElement inputElement, string expectedValue)
        {
            Assert.AreEqual(expectedValue, inputElement.GetDomProperty("value"));
        }

        public static void AssertElementInnerText(IWebElement inputElement, string expectedValue)
        {
            Assert.AreEqual(expectedValue, inputElement.Text.Trim());
        }

        public static void AssertDisplayed(IWebElement webElement)
        {
            Assert.True(webElement.Displayed);
        }

        public static void AssertMessageContent(IWebElement webElement, string expectedMessageContent)
        {
            Assert.AreEqual(expectedMessageContent, webElement.Text);
        }

        public static void AssertCheckboxState(IWebElement checkbox, bool expectedState)
        {
            Assert.AreEqual(expectedState, CommonInteractions.GetCheckboxState(checkbox));
        }
    }
}
