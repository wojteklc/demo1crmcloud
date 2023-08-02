using NUnit.Framework;
using OneCrmTestProject.Common;
using OneCrmTestProject.PageObjects.Dashboard;
using OpenQA.Selenium;

namespace OneCrmTestProject.PageObjects.Login
{
    /// <summary>
    /// This PO is only small part of 'Login view' page, just to fulfill example scenatios requirements
    /// </summary>
    public class OneCrmLoginViewPO
    {
        private readonly string? _url = $"{TestContext.Parameters["oneCrmBaseUrl"]}/login.php?login_module=Home&login_action=index";
        private readonly IWebDriver _driver;

        // I really liked to use PageFactory, but because it's deprecated below way of initializing page object web elemens seems optimal to me
        // I personally prefer XPaths as they are very flexible (allow to use all unique html attributes and XPath Axes to locate web elements)
        // Holding locators in below fields allow us to use them in any class method we want and as many times we want
        // They are easy to maintain also (only one occurrence of locator)
        private readonly By _userNameInputLocator = By.Id("login_user");
        private readonly By _passwordInputLocator = By.Id("login_pass");
        private readonly By _loginButtonLocator = By.Id("login_button");

        // Lazy initialization of properties holding references to web elements using expression-bodied member
        private IWebElement UserNameInput => _driver.FindElement(_userNameInputLocator);
        private IWebElement PasswordInput => _driver.FindElement(_passwordInputLocator);
        private IWebElement LoginButton => _driver.FindElement(_loginButtonLocator);

        public OneCrmLoginViewPO(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GoTo()
        {
            _driver.Navigate().GoToUrl(_url);
            VerifyPageIsOpened();
        }

        public void VerifyPageIsOpened()
        {
            CommonWaits.WaitForUrlToBe(_driver, _url);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _userNameInputLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _passwordInputLocator);
            CommonWaits.WaitForElementToBecomeVisible(_driver, _loginButtonLocator);
            CommonAssertions.AssertDisplayed(UserNameInput);
            CommonAssertions.AssertDisplayed(PasswordInput);
            CommonAssertions.AssertDisplayed(LoginButton);
        }

        public OneCrmHomeDashboardViewPO LogIn(string userName, string password)
        {
            CommonInteractions.SetInputValue(UserNameInput, userName);
            CommonInteractions.SetInputValue(PasswordInput, password);

            CommonWaits.WaitForElementToBecomeClickable(_driver, _loginButtonLocator);
            CommonInteractions.ClickWebElement(LoginButton);

            return new OneCrmHomeDashboardViewPO(_driver);
        }

        public void SetUserName(string userName)
        {
            CommonInteractions.SetInputValue(UserNameInput, userName);
        }

        public void SetPassword(string password)
        {
            CommonInteractions.SetInputValue(PasswordInput, password);
        }

        public void PressLoginButton()
        {
            CommonWaits.WaitForElementToBecomeClickable(_driver, _loginButtonLocator);
            CommonInteractions.ClickWebElement(LoginButton);
        }
    }
}
