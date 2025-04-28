using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using NU = NUnit.Framework;

namespace SauceDemoTests.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        [Test, Order(1)]
        public void TestSuccessfulLogin()
        {
            // Navigate to login page
            LoginPage.NavigateToLoginPage();

            // Login with valid credentials
            ProductsPage = LoginPage.LoginWithValidCredentials();

            // Verify successful login
            MSTest.Assert.IsTrue(ProductsPage.IsProductsPageLoaded(), "Products page was not loaded after login");
            MSTest.Assert.AreEqual("Products", ProductsPage.GetHeaderText(), "Header text does not match expected value");

            // Log test success
            Console.WriteLine("Login test passed successfully!");
        }

        [Test, Order(3)]
        public void TestViewProductList()
        {
            // Navigate to login page and login
            LoginPage.NavigateToLoginPage();
            ProductsPage = LoginPage.LoginWithValidCredentials();

            // Verify products are loaded
            MSTest.Assert.IsTrue(ProductsPage.IsProductsPageLoaded(), "Products page was not loaded");

            // Verify products exist
            int productCount = ProductsPage.GetProductCount();
            NU.Assert.That(productCount != 0);
            Console.WriteLine($"Found {productCount} products on the page");

            // Test sorting
            TestProductSorting();

            Console.WriteLine("Product list test passed successfully!");
        }

        private void TestProductSorting()
        {
            // Sort products by price - low to high
            ProductsPage.SortProductsByPriceLowToHigh();

            // Get product prices
            var prices = ProductsPage.GetProductPrices();

            // Verify products are sorted by price (low to high)
            for (int i = 0; i < prices.Count - 1; i++)
            {
                MSTest.Assert.IsTrue(prices[i] <= prices[i + 1], "Products are not sorted by price (low to high)");
            }

            Console.WriteLine("Product sorting test passed successfully!");
        }
    }
}