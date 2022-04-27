using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Safari;
using NUnit.Framework;

namespace phptravel_test
{
    public class Auth
    {
 
        public void doLogin(IWebDriver driver)
        {
            driver.Url = ("https://phptravels.net/api/admin");
            // Find the email field and assign the value to it
            IWebElement email = driver.FindElement(By.Name("email"));
            email.SendKeys("admin@phptravels.com");

            // Find the password field and assign the value to it
            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys("demoadmin");

            // Find the submit button and click after entered the email and password to log in
            IWebElement pressLogin = driver.FindElement(By.XPath("//button[@type = 'submit']"));
            pressLogin.Click();

            // Create a wait method to wait the logging in process finishes successfully and move to Dashboard page
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            Func<IWebDriver, bool> waitForElement = new Func<IWebDriver, bool>((IWebDriver Web) =>
            {
                return Web.Title == "Dashboard";
            });
            wait.Until(waitForElement);

            // Check whether the Dashboad page is displayed
            string currentWindowTitle = "Dashboard";
            Assert.AreEqual(currentWindowTitle, driver.Title);
        }
    }
}
