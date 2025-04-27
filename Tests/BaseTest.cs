using System;
using NUnit.Framework;
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
        }

        [SetUp]
        public void Setup()
        {
            // Initialize WebDriver
            Driver = new WebDriverFactory().CreateDriver();

            // Initialize Pages
            LoginPage = new LoginPage(Driver);
            ProductsPage = new ProductsPage(Driver);
        }

        [TearDown]
        public void TearDown()
        {
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
    }
}