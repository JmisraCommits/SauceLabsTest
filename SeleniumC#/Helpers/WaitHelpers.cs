using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SauceDemoTests.Configuration;
using SeleniumExtras.WaitHelpers;

namespace SauceDemoTests.Helpers
{
    public class WaitHelpers
    {
        private readonly IWebDriver _driver;
        private readonly TestSettings _settings;
        private readonly WebDriverWait _wait;

        public WaitHelpers(IWebDriver driver)
        {
            _driver = driver;
            _settings = SauceDemoTests.Utilities.ConfigurationManager.GetSettings();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_settings.Timeouts.ExplicitWait));
        }

        public void WaitForElementVisible(By locator)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public void WaitForElementClickable(By locator)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public void WaitForElementPresent(By locator)
        {
            _wait.Until(ExpectedConditions.ElementExists(locator));
        }

        public void WaitForTextPresent(By locator, string text)
        {
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }

        public void WaitForUrlContains(string partialUrl)
        {
            _wait.Until(ExpectedConditions.UrlContains(partialUrl));
        }
    }
}