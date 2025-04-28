namespace SauceDemoTests.Tests
{
    public class ProductTest : BaseTest
    {
        private const string V = "Products";

        [Test]
        public void ProductPageTest()
        {
            // Navigate to login page
            LoginPage.NavigateToLoginPage();

            // Login with valid credentials and get product page
            ProductsPage = LoginPage.LoginWithValidCredentials();

            // Check the number of products on the page and confirm that it is non-zero

            int prodCount = ProductsPage.GetProductCount();
            Assert.That(prodCount != 0);

            Console.WriteLine("Product page  test passed successfully!");

        }

    }
}
