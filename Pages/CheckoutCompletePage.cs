using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    public class CheckoutCompletePage : BasePage
    {
        // Locators
        private readonly By _pageTitle = By.ClassName("title");
        private readonly By _completeHeader = By.ClassName("complete-header");
        private readonly By _completeText = By.ClassName("complete-text");
        private readonly By _backHomeButton = By.Id("back-to-products");

        public CheckoutCompletePage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsCheckoutCompletePageLoaded()
        {
            Wait.WaitForElementVisible(_pageTitle);
            Wait.WaitForUrlContains("checkout-complete.html");
            return GetPageTitle() == "Checkout: Complete!";
        }

        public string GetPageTitle()
        {
            return GetText(_pageTitle);
        }

        public string GetCompleteHeader()
        {
            return GetText(_completeHeader);
        }

        public string GetCompleteText()
        {
            return GetText(_completeText);
        }

        public ProductsPage BackToHome()
        {
            Click(_backHomeButton);
            return new ProductsPage(Driver);
        }
    }
}
