﻿//using OpenQA.Selenium;
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
                // Linux CI options
                options.AddArgument("--headless=new");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--remote-debugging-port=9222");
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
