//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;

//namespace ReqnrollFirstTestProject.Drivers
//{

//    public class WebDriverManager : IDisposable
//    {
//        public IWebDriver Driver { get; private set; }

//        public WebDriverManager()
//        {
//            Driver = new ChromeDriver();
//            Driver.Manage().Window.Maximize();
//        }

//        public void Dispose()
//        {
//            Driver.Dispose();
//        }

//        public void Quit()
//        {
//            Driver.Quit();
//        }
//    }
//}

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Runtime.InteropServices;

namespace ReqnrollFirstTestProject.Drivers
{
    public class WebDriverManager : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public WebDriverManager()
        {
            var options = new ChromeOptions();
            //options.BinaryLocation = "/usr/bin/google-chrome";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                options.AddArgument("--headless=new"); // Required for newer Chrome
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--remote-debugging-port=9222");
                options.AddArgument("--window-size=1920,1080"); // 🆕 Prevents rendering crashes
                options.AddArgument("--disable-software-rasterizer"); // 🆕
                Console.WriteLine("🟢 Running in Linux with headless Chrome.");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                options.AddArgument("--start-maximized");
                Console.WriteLine("🔵 Running in Windows.");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                options.AddArgument("--headless=new");
                Console.WriteLine("🟣 Running in macOS with headless Chrome.");
            }

            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // 🚀 Sanity check to ensure Chrome actually launched in CI
            try
            {
                Console.WriteLine("🚀 Launching sanity URL...");
                Driver.Navigate().GoToUrl("https://www.myget.org/F/reqnroll/api/v3/index.json");
                Console.WriteLine("✅ Chrome launched and navigated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Chrome launch failed: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            Driver?.Quit();
            Driver?.Dispose();
        }

        public void Quit()
        {
            Driver?.Quit();
        }
    }
}
