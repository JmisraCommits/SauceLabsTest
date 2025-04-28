using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    public class CheckoutOverviewPage : BasePage
    {
        // Locators
        private readonly By _cartItems = By.ClassName("cart_item");
        private readonly By _cartItemNames = By.ClassName("inventory_item_name");
        private readonly By _subtotalLabel = By.ClassName("summary_subtotal_label");
        private readonly By _taxLabel = By.ClassName("summary_tax_label");
        private readonly By _totalLabel = By.ClassName("summary_total_label");
        private readonly By _finishButton = By.Id("finish");
        private readonly By _cancelButton = By.Id("cancel");
        private readonly By _pageTitle = By.ClassName("title");

        public CheckoutOverviewPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsCheckoutOverviewPageLoaded()
        {
            Wait.WaitForElementVisible(_pageTitle);
            Wait.WaitForUrlContains("checkout-step-two.html");
            return GetPageTitle() == "Checkout: Overview";
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

        public string GetSubtotal()
        {
            return GetText(_subtotalLabel);
        }

        public string GetTax()
        {
            return GetText(_taxLabel);
        }

        public string GetTotal()
        {
            return GetText(_totalLabel);
        }

        public CheckoutCompletePage FinishCheckout()
        {
            Click(_finishButton);
            return new CheckoutCompletePage(Driver);
        }

        public ProductsPage CancelCheckout()
        {
            Click(_cancelButton);
            return new ProductsPage(Driver);
        }
    }
}

