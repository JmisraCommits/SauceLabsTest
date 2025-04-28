using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using SauceDemoTests.Drivers;
using SauceDemoTests.Pages;
using SauceDemoTests.Utilities;

namespace SauceDemoTests.Tests
{
    public class BaseTest : IDisposable
    {
        protected IWebDriver Driver;
        protected LoginPage LoginPage;
        protected ProductsPage ProductsPage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Initialize configuration
            ConfigurationManager.GetSettings();

            // Create Screenshots directory if it doesn't exist
            string screenshotDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
            if (!Directory.Exists(screenshotDir))
            {
                Directory.CreateDirectory(screenshotDir);
            }
        }

      

        [TearDown]
        public void TearDown()
        {
            try
            {
                // Take screenshot on test failure
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    TakeScreenshot($"Failed_{TestContext.CurrentContext.Test.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error taking failure screenshot: {ex.Message}");
            }

            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
        }

        public void Dispose()
        {
            TearDown();
        }

        protected void TakeScreenshot(string screenshotName)
        {
            try
            {
                if (Driver == null)
                    return;

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

                string fileName = $"{screenshotName}_{timestamp}.png";
                string fullPath = Path.Combine(screenshotPath, fileName);

                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                screenshot.SaveAsFile(fullPath, ScreenshotImageFormat.Png);

                // Add to test context for reporting
                TestContext.AddTestAttachment(fullPath, "Screenshot");

                Console.WriteLine($"Screenshot saved: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error taking screenshot: {ex.Message}");
            }
        }
    }
}