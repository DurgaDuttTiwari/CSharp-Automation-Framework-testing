using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollFirstTestProject.Selectors
{
    public class LoginSelectors
    {
        private readonly IWebDriver driver;

        public LoginSelectors(IWebDriver driver) {
            this.driver = driver;
        
        }

        public IWebElement loginlink => driver.FindElement(By.PartialLinkText("Log"));

        public IWebElement EnterEmail => driver.FindElement(By.XPath("//input[@placeholder='email']"));

        public IWebElement EnterPassword => driver.FindElement(By.XPath("//input[@placeholder='password']"));

        public IWebElement LoginButton => driver.FindElement(By.XPath("//button[@type='submit']"));

        public IWebElement profileButton => driver.FindElement(By.XPath("/html[1]/body[1]/div[2]/div[1]/div[3]/a[3]/*[name()='svg'][2]"));

        public IWebElement logout => driver.FindElement(By.PartialLinkText("Loghhhh"));



    }
}
