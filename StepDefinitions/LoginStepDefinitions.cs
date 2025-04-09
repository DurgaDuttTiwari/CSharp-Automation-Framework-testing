using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using ReqnrollFirstTestProject.Drivers;
using ReqnrollFirstTestProject.Selectors;

namespace ReqnrollFirstTestProject.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {

        private readonly IWebDriver driver;
        private readonly LoginSelectors loginSelectors;
        //private readonly WebDriverWait wait;


        public LoginStepDefinitions(ScenarioContext scenarioContext)
        {
            var driverManager = scenarioContext.Get<WebDriverManager>(nameof(WebDriverManager));
            driver = driverManager.Driver;
            loginSelectors = new LoginSelectors(driver);
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        


        [Given("I am on th login page")]
        public void GivenIAmOnThLoginPage()
        {
            driver.Navigate().GoToUrl("https://www.w3schools.com/cs/cs_exercises.php");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
        }

        [When("I click on the login link")]
        public void WhenIClickOnTheLoginLink()
        {
            Console.WriteLine(loginSelectors.loginlink.Text);
            loginSelectors.loginlink.Click();
            
        }

        [When("I enter username as {string} and password as {string}")]
        public void WhenIEnterUsernameAsAndPasswordAs(string admin, string password)
        {
            loginSelectors.EnterEmail.SendKeys(admin);
            loginSelectors.EnterPassword.SendKeys(password);
        }

        [When("I clicked on login button")]
        public void WhenIClickedOnLoginButton()
        {
           loginSelectors.LoginButton.Click();
        }

        [Then("I should be looged in")]
        public void ThenIShouldBeLoogedIn()
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            bool isUrlCorrect = wait.Until(driver =>
                driver.Url == "https://www.w3schools.com/cs/cs_exercises.php"
            );

            string currentUrl = driver.Url;
            Console.WriteLine("Current URL: " + currentUrl);
            Assert.That(isUrlCorrect, Is.True, $"Expected URL not reached. Current URL: {currentUrl}");

            //string currentUrl = driver.Url;
            //Console.WriteLine("Current URL: " + currentUrl);
            //Assert.That(currentUrl, Is.EqualTo("https://www.w3schools.com/cs/cs_exercises.php"));
        }

        [Then("I clicked on logout")]
        public void ThenIClickedOnLogout()
        {
            loginSelectors.profileButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            loginSelectors.logout.Click();
        }

    }
}
