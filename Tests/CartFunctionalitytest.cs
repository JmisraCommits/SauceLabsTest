using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SauceDemoTests.Pages;
using SauceDemoTests.Tests;

namespace SeleniumC_.Tests
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
                //pm Assert.IsTrue(ProductsPage.IsProductsPageLoaded(), "Products page was not loaded after login");
                //pm Assert.AreEqual("Products", ProductsPage.GetHeaderText(), "Header text does not match expected value");
            int initialCount = ProductsPage.GetCartItemCount();
            string productName = ProductsPage.GetProductNames()[initialCount];

            ProductsPage.AddProductToCart(productName);
            int laterCount = ProductsPage.GetCartItemCount();
            // Log test success
            Console.WriteLine("Login test passed successfully!");
        }
    }
    
    }
