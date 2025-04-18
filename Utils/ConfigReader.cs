using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollFirstTestProject.Utils
{
    public static class ConfigReader
    {
        private static readonly JObject config;

        static ConfigReader()
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "UserDetails.json");
            var json = File.ReadAllText(configPath);
            config = JObject.Parse(json);
        }

        public static string GetEmail()
        {
            return config["email"]?.ToString();
        }

        public static string GetPassword()
        {
            return config["password"]?.ToString();
        }
    }
}
