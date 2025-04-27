using System;
using OpenQA.Selenium;
using SauceDemoTests.Configuration;

namespace SauceDemoTests.Pages
{
    public class LoginPage : BasePage
    {
        // Locators
        private readonly By _usernameField = By.Id("user-name");
        private readonly By _passwordField = By.Id("password");
        private readonly By _loginButton = By.Id("login-button");
        private readonly By _errorMessage = By.CssSelector("[data-test='error']");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateToLoginPage()
        {
            Driver.Navigate().GoToUrl(Settings.BaseUrl);
        }

        public ProductsPage LoginWithValidCredentials()
        {
            return Login(Settings.Credentials.Username, Settings.Credentials.Password);
        }

        public ProductsPage LoginWithWrongPassword()
        {
            return Login(Settings.Credentials.Username, Settings.Credentials.Wrongpassword);
        }

        public ProductsPage Login(string username, string password)
        {
            SendKeys(_usernameField, username);
            SendKeys(_passwordField, password);
            Click(_loginButton);
            return new ProductsPage(Driver);
        }

        public bool IsErrorMessageDisplayed()
        {
            return IsElementDisplayed(_errorMessage);
        }

        public string GetErrorMessage()
        {
            return GetText(_errorMessage);
        }
    }
}