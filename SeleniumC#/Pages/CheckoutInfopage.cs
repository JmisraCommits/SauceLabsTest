using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    public class CheckoutInfoPage : BasePage
    {
        // Locators
        private readonly By _firstNameField = By.Id("first-name");
        private readonly By _lastNameField = By.Id("last-name");
        private readonly By _postalCodeField = By.Id("postal-code");
        private readonly By _continueButton = By.Id("continue");
        private readonly By _cancelButton = By.Id("cancel");
        private readonly By _errorMessage = By.CssSelector("[data-test='error']");
        private readonly By _pageTitle = By.ClassName("title");

        public CheckoutInfoPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsCheckoutInfoPageLoaded()
        {
            Wait.WaitForElementVisible(_pageTitle);
            Wait.WaitForUrlContains("checkout-step-one.html");
            return GetPageTitle() == "Checkout: Your Information";
        }

        public string GetPageTitle()
        {
            return GetText(_pageTitle);
        }

        public CheckoutOverviewPage FillInfoAndContinue(string firstName, string lastName, string postalCode)
        {
            SendKeys(_firstNameField, firstName);
            SendKeys(_lastNameField, lastName);
            SendKeys(_postalCodeField, postalCode);
            Click(_continueButton);
            return new CheckoutOverviewPage(Driver);
        }

        public CartPage Cancel()
        {
            Click(_cancelButton);
            return new CartPage(Driver);
        }

        public string GetErrorMessage()
        {
            return IsElementDisplayed(_errorMessage) ? GetText(_errorMessage) : string.Empty;
        }
    }
}

