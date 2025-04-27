using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using SauceDemoTests.Configuration;

namespace SauceDemoTests.Utilities
{
    public static class ConfigurationManager
    {
        private static TestSettings _settings;

        public static TestSettings GetSettings()
        {
            if (_settings != null)
                return _settings;

            // Build configuration
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            // Bind configuration to settings object
            _settings = new TestSettings();
            configuration.GetSection("TestSettings").Bind(_settings);

            return _settings;
        }
    }
}