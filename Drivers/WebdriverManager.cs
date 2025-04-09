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

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                options.AddArgument("--headless=new"); // Required for newer Chrome
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--window-size=1920,1080"); // Important
                options.AddArgument("--single-process");         // Recommended
                options.AddArgument("--disable-extensions");     // Optional
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
