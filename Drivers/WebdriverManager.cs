using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReqnrollFirstTestProject.Drivers
{

    public class WebDriverManager : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public WebDriverManager()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        public void Dispose()
        {
            Driver.Dispose();
        }

        public void Quit()
        {
            Driver.Quit();
        }
    }
}

