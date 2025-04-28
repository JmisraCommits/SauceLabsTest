using SauceDemoTests.Pages;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SauceDemoTests.Tests
{
    public class CartFunctionalitytest : BaseTest
    {

        [Test]
        public void AddItemtoCart()
        {
            // Navigate to login page
            LoginPage.NavigateToLoginPage();

            // Login with valid credentials
            ProductsPage = LoginPage.LoginWithValidCredentials();

            // Verify successful login
            MSTest.Assert.IsTrue(ProductsPage.IsProductsPageLoaded(), "Products page was not loaded after login");
            MSTest.Assert.AreEqual("Products", ProductsPage.GetHeaderText(), "Header text does not match expected value");
            int initialCount = ProductsPage.GetCartItemCount();
            string productName = ProductsPage.GetProductNames()[initialCount];

            ProductsPage.AddProductToCart(productName);
            int laterCount = ProductsPage.GetCartItemCount();
            Assert.That(laterCount != 0);
            // Log test success
            Console.WriteLine("The value of item in the cart is: " + laterCount);
            Console.WriteLine("Login test passed successfully!");
        }
    }

}
