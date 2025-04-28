using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    public class CartPage : BasePage
    {
        // Locators
        private readonly By _cartItems = By.ClassName("cart_item");
        private readonly By _cartItemNames = By.ClassName("inventory_item_name");
        private readonly By _cartItemPrices = By.ClassName("inventory_item_price");
        private readonly By _removeButtons = By.CssSelector(".cart_button");
        private readonly By _checkoutButton = By.Id("checkout");
        private readonly By _continueShoppingButton = By.Id("continue-shopping");
        private readonly By _pageTitle = By.ClassName("title");

        public CartPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsCartPageLoaded()
        {
            Wait.WaitForElementVisible(_pageTitle);
            Wait.WaitForUrlContains("cart.html");
            return GetPageTitle() == "Your Cart";
        }

        public string GetPageTitle()
        {
            return GetText(_pageTitle);
        }

        public int GetCartItemCount()
        {
            return Driver.FindElements(_cartItems).Count;
        }

        public List<string> GetCartItemNames()
        {
            return Driver.FindElements(_cartItemNames)
                .Select(element => element.Text)
                .ToList();
        }

        public bool IsItemInCart(string itemName)
        {
            return GetCartItemNames().Contains(itemName);
        }

        public void RemoveItemFromCart(string itemName)
        {
            var cartItems = Driver.FindElements(_cartItems);
            foreach (var item in cartItems)
            {
                var nameElement = item.FindElement(By.ClassName("inventory_item_name"));
                if (nameElement.Text == itemName)
                {
                    var removeButton = item.FindElement(By.CssSelector(".cart_button"));
                    removeButton.Click();
                    break;
                }
            }
        }

        public CheckoutInfoPage ProceedToCheckout()
        {
            Click(_checkoutButton);
            return new CheckoutInfoPage(Driver);
        }

        public ProductsPage ContinueShopping()
        {
            Click(_continueShoppingButton);
            return new ProductsPage(Driver);
        }
    }
}
