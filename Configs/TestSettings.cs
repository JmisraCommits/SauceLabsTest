using System;

namespace SauceDemoTests.Configuration
{
    public class TestSettings
    {
        public string BaseUrl { get; set; }
        public Credentials Credentials { get; set; }
        public Timeouts Timeouts { get; set; }
        public BrowserSettings BrowserSettings { get; set; }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Wrongpassword { get; set; }
    }

    public class Timeouts
    {
        public int ImplicitWait { get; set; }
        public int ExplicitWait { get; set; }
    }

    public class BrowserSettings
    {
        public string DefaultBrowser { get; set; }
        public bool Headless { get; set; }
    }
}