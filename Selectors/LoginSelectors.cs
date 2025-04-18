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

        public IWebElement loginlink => driver.FindElement(By.XPath("//header//a[contains(text(),'Sign In')]"));

        public IWebElement EnterEmail => driver.FindElement(By.XPath("//input[@title='Email']"));

        public IWebElement EnterPassword => driver.FindElement(By.XPath("//input[@title='Password']"));

        public IWebElement LoginButton => driver.FindElement(By.XPath("//span[normalize-space()='Sign In']"));

        public IWebElement WecomeBanner => driver.FindElement(By.XPath("//header//span[contains(@class,'logged-in')]"));

        public IWebElement profileButton => driver.FindElement(By.XPath("//header//ul//button[@type='button']"));

        public IWebElement logout => driver.FindElement(By.PartialLinkText("Sign Out"));



    }
}
