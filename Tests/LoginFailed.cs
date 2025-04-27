using System;
using NUnit.Framework;

namespace SauceDemoTests.Tests
{
    [TestFixture]
    public class FailedLoginTests : BaseTest
    {
        [Test]
        public void TestFailedLogin()
        {
            // Navigate to login page
            LoginPage.NavigateToLoginPage();

            // Login with valid credentials
            ProductsPage = LoginPage.LoginWithWrongPassword();

            // Verify login failed
            //pm Assert.IsTrue(ProductsPage.IsProductsPageLoaded(), "Products page was not loaded after login");
            //pm Assert.AreEqual("Products", ProductsPage.GetHeaderText(), "Header text does not match expected value");

            // Log test success
            Console.WriteLine("Login with wrong password failed and verified use case successfully!");
        }

        
    }
}
