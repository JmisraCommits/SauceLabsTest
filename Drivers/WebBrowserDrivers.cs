using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using SauceDemoTests.Configuration;
using SauceDemoTests.Utilities;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Interactions;

namespace SauceDemoTests.Drivers
{
    public class WebDriverFactory
    {
        private readonly TestSettings _settings;

        public WebDriverFactory()
        {
            _settings = SauceDemoTests.Utilities.ConfigurationManager.GetSettings();
        }

        public IWebDriver CreateDriver()
        {
            IWebDriver driver;

            switch (_settings.BrowserSettings.DefaultBrowser.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    if (_settings.BrowserSettings.Headless)
                        chromeOptions.AddArgument("--headless");

                    chromeOptions.AddUserProfilePreference("profile.password_manager_leak_detection", "false");
                    chromeOptions.AddArgument("--suppress-message-center-popups");
                    chromeOptions.AddUserProfilePreference("profile.default_content_settings.popups", 0); // 0 - block popups
                    chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2); // 2 - Deny notifications
                                                                                                                       //  Additional ChromeOptions to handle password breach and automation detection:
                    chromeOptions.AddExcludedArguments(new List<string>() { "disable-infobars", "ignore-certificate-errors", "disable-prompt-on-repost" });
                    chromeOptions.AddAdditionalOption("useAutomationExtension", false);
                    chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                    chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                    chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
                    chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
                    chromeOptions.AddUserProfilePreference("download.default_directory", "/tmp");
                    chromeOptions.AddUserProfilePreference("safebrowsing.enabled", false);
                    chromeOptions.AddUserProfilePreference("profile.default_password_setting", "0");
                    chromeOptions.AddUserProfilePreference("profile.gaia_prompt_for_password", false);
                    chromeOptions.AddArguments("--password-store=basic");
                    chromeOptions.AddArgument("--incognito");
                    chromeOptions.AddArguments("--disable-password-manager-dialog");
                    chromeOptions.AddArguments("--disable-features=PasswordManager");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeDetectionUI,PasswordPrompt");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeDetectionUI,PasswordPrompt,PasswordUpdate");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeChangeDetectionUI,PasswordPrompt,PasswordUpdate,PasswordStrength,PasswordGeneration");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeChangeDetectionUI,PasswordPrompt,PasswordUpdate,PasswordStrength,PasswordGeneration,PasswordProtectedInterstitial");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeChangeDetectionUI,PasswordPrompt,PasswordUpdate,PasswordStrength,PasswordGeneration,PasswordProtectedInterstitial,OriginChip,ForceOfferStoreUnmasked,PasswordManagerOnboarding");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeChangeDetectionUI,PasswordPrompt,PasswordUpdate,PasswordStrength,PasswordGeneration,PasswordProtectedInterstitial,OriginChip,ForceOfferStoreUnmasked,PasswordManagerOnboarding,PasswordManager,PasswordCredentialLeakPrompt");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeChangeDetectionUI,PasswordPrompt,PasswordUpdate,PasswordStrength,PasswordGeneration,PasswordProtectedInterstitial,OriginChip,ForceOfferStoreUnmasked,PasswordManagerOnboarding,PasswordManager,PasswordCredentialLeakPrompt,PasswordReuse");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeChangeDetectionUI,PasswordPrompt,PasswordUpdate,PasswordStrength,PasswordGeneration,PasswordProtectedInterstitial,OriginChip,ForceOfferStoreUnmasked,PasswordManagerOnboarding,PasswordManager,PasswordCredentialLeakPrompt,PasswordReuse,PasswordAutosignin");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeChangeDetectionUI,PasswordPrompt,PasswordUpdate,PasswordStrength,PasswordGeneration,PasswordProtectedInterstitial,OriginChip,ForceOfferStoreUnmasked,PasswordManagerOnboarding,PasswordManager,PasswordCredentialLeakPrompt,PasswordReuse,PasswordAutosignin,PasswordManagerOnboardingV2");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeChangeDetectionUI,PasswordPrompt,PasswordUpdate,PasswordStrength,PasswordGeneration,PasswordProtectedInterstitial,OriginChip,ForceOfferStoreUnmasked,PasswordManagerOnboarding,PasswordManager,PasswordCredentialLeakPrompt,PasswordReuse,PasswordAutosignin,PasswordManagerOnboardingV2,PasswordUpdateBubble");
                    chromeOptions.AddArguments("--disable-features=PasswordManager,PasswordLeakDetection,PasswordChangeDetection,PasswordChangeChangeDetectionUI,PasswordPrompt,PasswordUpdate,PasswordStrength,PasswordGeneration,PasswordProtectedInterstitial,OriginChip,ForceOfferStoreUnmasked,PasswordManagerOnboarding,PasswordManager,PasswordCredentialLeakPrompt,PasswordReuse,PasswordAutosignin,PasswordManagerOnboardingV2,PasswordUpdateBubble,PasswordManagedByAdministrator,PasswordStore,PasswordPromptPrimaryAction,PasswordPromptPrimaryActionWithSimplePasswordPrompt");
                    chromeOptions.AddArguments("--suppress-message-center-popups");
                    chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                    chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                    


                    driver = new ChromeDriver(chromeOptions);
                    break;
                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    if (_settings.BrowserSettings.Headless)
                        firefoxOptions.AddArgument("--headless");

                    driver = new FirefoxDriver(firefoxOptions);
                    break;
                case "edge":
                    var edgeOptions = new EdgeOptions();
                    if (_settings.BrowserSettings.Headless)
                        edgeOptions.AddArgument("--headless");

                    driver = new EdgeDriver(edgeOptions);
                    break;
                default:
                    throw new ArgumentException($"Unsupported browser type: {_settings.BrowserSettings.DefaultBrowser}");
            }

            // Configure driver settings
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_settings.Timeouts.ImplicitWait);
            driver.Manage().Window.Maximize();

            return driver;
        }
    }
}