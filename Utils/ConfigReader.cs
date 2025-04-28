using Newtonsoft.Json.Linq;


namespace ReqnrollFirstTestProject.Utils
{
    public static class ConfigReader
    {
        private static readonly JObject? config;

        static ConfigReader()
        {

            // Calculate project root (go up from bin/Debug/net8.0/ to project folder)
            var baseDirectory = AppContext.BaseDirectory;  // Better than Directory.GetCurrentDirectory()
            var projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, "..", "..", ".."));
            var configPath = Path.Combine(projectRoot, "UserDetails.json");


            if (File.Exists(configPath))
            {
                Console.WriteLine("UserDetails.json found.");
                var json = File.ReadAllText(configPath);
                config = JObject.Parse(json);
            }
            else
            {
                Console.WriteLine("UserDetails.json NOT FOUND.");
                config = null;
            }
        }

        public static string GetEmail()
        {
            // 1. Priority: Environment Variable (for GitHub Actions)
            var emailFromEnv = Environment.GetEnvironmentVariable("TEST_EMAIL");
            if (!string.IsNullOrEmpty(emailFromEnv))
                return emailFromEnv;

            // 2. Priority: Local config file (for local testing)
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
