using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ReqnrollFirstTestProject.Utils
{
    public static class ConfigReader
    {
        private static readonly JObject? config;

        static ConfigReader()
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "UserDetails.json");

            if (File.Exists(configPath))
            {
                var json = File.ReadAllText(configPath);
                config = JObject.Parse(json);
            }
            else
            {
                config = null; // No local file, will use environment variables
            }
        }

        public static string GetEmail()
        {
            // 1. Priority: Environment Variable (for GitHub Actions)
            var emailFromEnv = Environment.GetEnvironmentVariable("TEST_EMAIL");
            if (!string.IsNullOrEmpty(emailFromEnv))
                return emailFromEnv;

            // 2. Priority: Local config file
            if (config?["email"] != null)
                return config["email"]!.ToString();

            throw new Exception("Email not found in environment variables or local config file.");
        }

        public static string GetPassword()
        {
            var passwordFromEnv = Environment.GetEnvironmentVariable("TEST_PASSWORD");
            if (!string.IsNullOrEmpty(passwordFromEnv))
                return passwordFromEnv;

            if (config?["password"] != null)
                return config["password"]!.ToString();

            throw new Exception("Password not found in environment variables or local config file.");
        }
    }
}
