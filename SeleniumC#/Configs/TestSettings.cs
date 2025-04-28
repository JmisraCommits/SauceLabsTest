namespace SauceDemoTests.Configuration
{
    public class TestSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public Credentials Credentials { get; set; } = new Credentials();
        public Timeouts Timeouts { get; set; } = new Timeouts();
        public BrowserSettings BrowserSettings { get; set; } = new BrowserSettings();
    }

    public class Credentials
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Wrongpassword { get; set; } = string.Empty;
    }

    public class Timeouts
    {
        public int ImplicitWait { get; set; }
        public int ExplicitWait { get; set; }
    }

    public class BrowserSettings
    {
        public string DefaultBrowser { get; set; } = string.Empty;
        public bool Headless { get; set; } = false;
    }
}