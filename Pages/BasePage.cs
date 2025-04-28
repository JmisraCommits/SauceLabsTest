// Pages/BasePage.cs
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SauceDemoTests.Configuration;
using SauceDemoTests.Helpers;
using SauceDemoTests.Utilities;

namespace SauceDemoTests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WaitHelpers Wait;
        protected readonly TestSettings Settings;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WaitHelpers(driver);
            Settings = ConfigurationManager.GetSettings();
        }

        protected void Click(By locator)
        {
            Wait.WaitForElementClickable(locator);
            Driver.FindElement(locator).Click();
        }

        protected void SendKeys(By locator, string text)
        {
            Wait.WaitForElementVisible(locator);
            Driver.FindElement(locator).Clear();
            Driver.FindElement(locator).SendKeys(text);
        }

        protected string GetText(By locator)
        {
            Wait.WaitForElementVisible(locator);
            return Driver.FindElement(locator).Text;
        }

        protected bool IsElementDisplayed(By locator)
        {
            try
            {
                Wait.WaitForElementVisible(locator);
                return Driver.FindElement(locator).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        protected void SelectDropdownByValue(By locator, string value)
        {
            Wait.WaitForElementVisible(locator);
            var dropdown = Driver.FindElement(locator);
            var select = new SelectElement(dropdown);
            select.SelectByValue(value);
        }

        protected void SelectDropdownByText(By locator, string text)
        {
            Wait.WaitForElementVisible(locator);
            var dropdown = Driver.FindElement(locator);
            var select = new SelectElement(dropdown);
            select.SelectByText(text);
        }

        /*protected void TakeScreenshot(string testName)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

                // Create directory if it doesn't exist
                if (!Directory.Exists(screenshotPath))
                    Directory.CreateDirectory(screenshotPath);

                string fileName = $"{testName}_{timestamp}.png";
                string fullPath = Path.Combine(screenshotPath, fileName);

                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                screenshot.SaveAsFile(fullPath);

                Console.WriteLine($"Screenshot saved: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error taking screenshot: {ex.Message}");
            }
        }
        */

    }
}