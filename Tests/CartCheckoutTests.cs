using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using SauceDemoTests.Pages;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SauceDemoTests.Tests
{
    [TestFixture]
    public class CartCheckoutTests : BaseTest
    {
        private CartPage? CartPage = null;
        private CheckoutInfoPage? CheckoutInfoPage = null;
        private CheckoutOverviewPage? CheckoutOverviewPage = null;
        private CheckoutCompletePage? CheckoutCompletePage = null;
        private static ExtentTest? test = null;
        public static ExtentReports? extent = null;

        [Test]
        public void TestAddItemToCart()
        {
            extent = new ExtentReports();
            var htmlreporter = new ExtentSparkReporter(@"C:\ReportResults\Report" + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".html");
            extent.AttachReporter(htmlreporter);
            test = extent.CreateTest("T").Info("Cart test");

            // Navigate to login page and login
            LoginPage.NavigateToLoginPage();
            ProductsPage = LoginPage.LoginWithValidCredentials();

            // Create page instances
            CartPage = new CartPage(Driver);
            CheckoutInfoPage = new CheckoutInfoPage(Driver);
            CheckoutOverviewPage = new CheckoutOverviewPage(Driver);
            CheckoutCompletePage = new CheckoutCompletePage(Driver);
            // Verify products page is loaded
            MSTest.Assert.IsTrue(ProductsPage.IsProductsPageLoaded(), "Products page was not loaded");

            // Select first product
            string productName = ProductsPage.GetProductNames()[0];

            // Add product to cart
            ProductsPage.AddProductToCart(productName);

            // Take screenshot after adding to cart
            TakeScreenshot("AddToCart");

            // Verify cart badge shows 1 item
            MSTest.Assert.AreEqual(1, ProductsPage.GetCartItemCount(), "Cart badge doesn't show correct item count");

            // Go to cart
            CartPage = ProductsPage.GoToCart();

            // Verify cart page is loaded
            MSTest.Assert.IsTrue(CartPage.IsCartPageLoaded(), "Cart page was not loaded");

            // Verify item is in cart
            MSTest.Assert.IsTrue(CartPage.IsItemInCart(productName), $"Product '{productName}' not found in cart");

            Console.WriteLine($"Successfully added product '{productName}' to cart");
            test.Log(Status.Pass, "TestAddItemToCart passed");
            extent.Flush();
        }

        [Test]
        public void TestRemoveItemFromCart()
        {
            //base.Setup();

            // Navigate to login page and login
            LoginPage.NavigateToLoginPage();
            ProductsPage = LoginPage.LoginWithValidCredentials();

            // Create page instances
            CartPage = new CartPage(Driver);
            CheckoutInfoPage = new CheckoutInfoPage(Driver);
            CheckoutOverviewPage = new CheckoutOverviewPage(Driver);
            CheckoutCompletePage = new CheckoutCompletePage(Driver);
            // Add item to cart first
            string productName = ProductsPage.GetProductNames()[0];
            ProductsPage.AddProductToCart(productName);

            // Go to cart
            CartPage = ProductsPage.GoToCart();

            // Verify cart page is loaded
            MSTest.Assert.IsTrue(CartPage.IsCartPageLoaded(), "Cart page was not loaded");

            // Verify item is in cart
            MSTest.Assert.IsTrue(CartPage.IsItemInCart(productName), $"Product '{productName}' not found in cart");

            // Take screenshot before removal
            TakeScreenshot("BeforeRemoveFromCart");

            // Remove item from cart
            CartPage.RemoveItemFromCart(productName);

            // Take screenshot after removal
            TakeScreenshot("AfterRemoveFromCart");

            // Verify item is removed from cart
            MSTest.Assert.AreEqual(0, CartPage.GetCartItemCount(), "Item was not removed from cart");

            Console.WriteLine($"Successfully removed product '{productName}' from cart");
        }

        [Test]
        public void TestCompleteCheckoutProcess()
        {
            //base.Setup();

            // Navigate to login page and login
            LoginPage.NavigateToLoginPage();
            ProductsPage = LoginPage.LoginWithValidCredentials();

            // Create page instances
            CartPage = new CartPage(Driver);
            CheckoutInfoPage = new CheckoutInfoPage(Driver);
            CheckoutOverviewPage = new CheckoutOverviewPage(Driver);
            CheckoutCompletePage = new CheckoutCompletePage(Driver);
            // Add item to cart
            string productName = ProductsPage.GetProductNames()[0];
            ProductsPage.AddProductToCart(productName);

            // Go to cart
            CartPage = ProductsPage.GoToCart();

            // Verify cart page is loaded and item is in cart
            MSTest.Assert.IsTrue(CartPage.IsCartPageLoaded(), "Cart page was not loaded");
            MSTest.Assert.IsTrue(CartPage.IsItemInCart(productName), $"Product '{productName}' not found in cart");

            // Take screenshot of cart
            TakeScreenshot("CartBeforeCheckout");

            // Proceed to checkout
            CheckoutInfoPage = CartPage.ProceedToCheckout();

            // Verify checkout info page is loaded
            MSTest.Assert.IsTrue(CheckoutInfoPage.IsCheckoutInfoPageLoaded(), "Checkout information page was not loaded");

            // Fill checkout information
            CheckoutOverviewPage = CheckoutInfoPage.FillInfoAndContinue("John", "Doe", "12345");

            // Take screenshot of checkout overview
            TakeScreenshot("CheckoutOverview");

            // Verify checkout overview page is loaded
            MSTest.Assert.IsTrue(CheckoutOverviewPage.IsCheckoutOverviewPageLoaded(), "Checkout overview page was not loaded");

            // Verify item is in checkout
            MSTest.Assert.IsTrue(CheckoutOverviewPage.GetCartItemNames().Contains(productName),
                $"Product '{productName}' not found in checkout overview");

            // Complete order
            CheckoutCompletePage = CheckoutOverviewPage.FinishCheckout();

            // Verify checkout complete page is loaded
            MSTest.Assert.IsTrue(CheckoutCompletePage.IsCheckoutCompletePageLoaded(), "Checkout complete page was not loaded");

            // Take screenshot of complete page
            TakeScreenshot("CheckoutComplete");

            // Verify success message
            MSTest.Assert.AreEqual("Thank you for your order!", CheckoutCompletePage.GetCompleteHeader(),
                "Success message not displayed");

            Console.WriteLine("Checkout process completed successfully");
        }

        private void TakeScreenshot(string testName)
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
    }
}

