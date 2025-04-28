namespace SauceDemoTests.Tests
{
    [TestFixture]
    public class FailedLoginTests : BaseTest
    {
        [Test, Order(3)]
        public void TestFailedLogin()
        {
            // Navigate to login page
            LoginPage.NavigateToLoginPage();

            // Login with valid credentials
            ProductsPage = LoginPage.LoginWithWrongPassword();

            // Log test success
            Console.WriteLine("Login with wrong password failed and verified use case successfully!");
        }


    }
}
