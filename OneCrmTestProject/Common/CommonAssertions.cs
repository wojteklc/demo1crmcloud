﻿using NUnit.Framework;
using OpenQA.Selenium;

namespace OneCrmTestProject.Common
{
    public static class CommonAssertions
    {
        public static void AssertInputVaue(IWebElement inputElement, string expectedValue)
        {
            Assert.AreEqual(expectedValue, inputElement.GetDomProperty("value"));
        }

        public static void AssertDisplayed(IWebElement webElement)
        {
            Assert.True(webElement.Displayed);
        }

        public static void AssertMessageContent(IWebElement webElement, string expectedMessageContent)
        {
            Assert.AreEqual(expectedMessageContent, webElement.Text);
        }
    }
}