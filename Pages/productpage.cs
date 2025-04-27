using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceDemoTests.Pages
{
    public class ProductsPage : BasePage
    {
        // Locators
        private readonly By _productsHeader = By.ClassName("title");
        private readonly By _inventoryItems = By.ClassName("inventory_item");
        private readonly By _sortDropdown = By.ClassName("product_sort_container");
        private readonly By _productNames = By.ClassName("inventory_item_name");
        private readonly By _productPrices = By.ClassName("inventory_item_price");
        private readonly By _addToCartButtons = By.CssSelector(".pricebar button");
        private readonly By _shoppingCartBadge = By.ClassName("shopping_cart_badge");

        public ProductsPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsProductsPageLoaded()
        {
            Wait.WaitForElementVisible(_productsHeader);
            Wait.WaitForUrlContains("inventory.html");
            return GetHeaderText() == "Products";
        }

        public string GetHeaderText()
        {
            return GetText(_productsHeader);
        }

        public int GetProductCount()
        {
            return Driver.FindElements(_inventoryItems).Count;
        }

        public void SortProductsByPriceLowToHigh()
        {
            SelectDropdownByValue(_sortDropdown, "lohi");
        }

        public void SortProductsByPriceHighToLow()
        {
            SelectDropdownByValue(_sortDropdown, "hilo");
        }

        public void SortProductsByNameAToZ()
        {
            SelectDropdownByValue(_sortDropdown, "az");
        }

        public void SortProductsByNameZToA()
        {
            SelectDropdownByValue(_sortDropdown, "za");
        }

        public List<string> GetProductNames()
        {
            return Driver.FindElements(_productNames)
                .Select(element => element.Text)
                .ToList();
        }

        public List<decimal> GetProductPrices()
        {
            return Driver.FindElements(_productPrices)
                .Select(element => decimal.Parse(element.Text.Replace("$", "")))
                .ToList();
        }

        public void AddProductToCart(string productName)
        {
            try
            {
               
                var productElements = Driver.FindElements(_inventoryItems);
                foreach (var product in productElements)
                {
                    var nameElement = product.FindElement(By.ClassName("inventory_item_name"));
                    if (nameElement.Text == productName)
                    {
                        var addToCartButton = product.FindElement(By.CssSelector(".pricebar button"));
                        addToCartButton.Click();
                        break;
                    }
                }
            }
            catch (NoSuchElementException)
            {
                // Pop-up not found, so continue normally
                Console.WriteLine("No pop-up appeared.");
            }

        }

        public int GetCartItemCount()
        {
            try
            {
                return int.Parse(Driver.FindElement(_shoppingCartBadge).Text);
            }
            catch (NoSuchElementException)
            {
                return 0;
            }
        }
    }
}