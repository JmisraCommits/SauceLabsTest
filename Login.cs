using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SeleniumC_.Configs;

namespace Logintest
{

    public class LoginTests : TestBase
    {
        private LoginPage loginPage;

        [SetUp]
        // Navigate to the Sauce Demo website using the base URL from configuration
        driver.Navigate().GoToUrl(LoginAndProductTests.baseUrl);

        // Perform login before each test
        Login_WithValidCredentials_RedirectsToInventory();
}

[Test]
public void Login_WithValidCredentials_RedirectsToInventory()
{
    // Locate username and password fields and the login button
    IWebElement usernameField = driver.FindElement(By.Id("user-name"));
    IWebElement passwordField = driver.FindElement(By.Id("password"));
    IWebElement loginButton = driver.FindElement(By.Id("login-button"));

    // Enter credentials from configuration
    usernameField.SendKeys(username);
    passwordField.SendKeys(password);

    // Click the login button
    loginButton.Click();

    // Verify successful login by checking for an element that exists on the products page
    wait.Until(ExpectedConditions.ElementExists(By.ClassName("inventory_item")));

    // Verify the page title or header contains expected text
    IWebElement productsHeader = driver.FindElement(By.ClassName("title"));
    Assert.AreEqual("Products", productsHeader.Text, "Login failed: Products header not found");

    Assert.IsTrue(driver.Url.Contains("inventory.html"), "Login failed: Not redirected to inventory page");
    Console.WriteLine("Login test passed successfully!");
}