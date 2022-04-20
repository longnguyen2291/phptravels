using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace phptravel_test
{
    [TestFixture()]
    public class Dashboard_Page
    {
        // Create a setup process to access into phptravels page
        IWebDriver driver;
        [SetUp]
        public void startBrowser()
        {
            driver = new SafariDriver();
        }

        // This is the main testing processes where we write methods for automation testing
        [Test()]
        public void Dashboard()
        {
            // Go to the phptravels page
            driver.Url = ("https://phptravels.net/api/admin");
            doLogin();
            verifyItems();
            settingDropdownMenu();
            bookingPage();
        }

        void doLogin()
        {
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
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Func<IWebDriver, bool> waitForElement = new Func<IWebDriver, bool>((IWebDriver Web) =>
            {
                return Web.Title == "Dashboard";
            });
            wait.Until(waitForElement);

            // Check whether the Dashboad page is displayed
            string currentWindowTitle = "Dashboard";
            Assert.AreEqual(currentWindowTitle, driver.Title);
        }

        void verifyItems()
        {
            // This method is verifying the required titles and icons in Dashboard page
            Thread.Sleep(5000);
            IWebElement vDashboard = driver.FindElement(By.XPath("//div[@id='layoutDrawer_content']//div[@class='container-xl p-4']/div[1]//h1[@class='display-4 mb-0']"));
            Assert.True(vDashboard.Displayed);

            IWebElement vConfirmed = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[1]/a/div/div/div/div[1]/div[2]"));
            Assert.True(vConfirmed.Displayed);
            IWebElement iconConfirmed = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[1]/a/div/div/div/div[2]"));
            Assert.True(iconConfirmed.Displayed);

            IWebElement vPending = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[2]/a/div/div/div/div[1]/div[2]"));
            Assert.True(vPending.Displayed);
            IWebElement iconPending = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[2]/a/div/div/div/div[2]"));
            Assert.True(iconPending.Displayed);

            IWebElement vCancelled = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[3]/a/div/div/div/div[1]/div[2]"));
            Assert.True(vCancelled.Displayed);
            IWebElement iconCancelled = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[3]/a/div/div/div/div[2]"));
            Assert.True(iconCancelled.Displayed);

            IWebElement vPaid = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[4]/a/div/div/div/div[1]/div[2]"));
            Assert.True(vPaid.Displayed);
            IWebElement iconPaid = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[4]/a/div/div/div/div[2]"));
            Assert.True(iconPaid.Displayed);

            IWebElement vUnPaid = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[5]/a/div/div/div/div[1]/div[2]"));
            Assert.True(vUnPaid.Displayed);
            IWebElement iconUnPaid = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[5]/a/div/div/div/div[2]"));
            Assert.True(iconUnPaid.Displayed);

            IWebElement vRefunded = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[6]/a/div/div/div/div[1]/div[2]"));
            Assert.True(vRefunded.Displayed);
            IWebElement iconRefunded = driver.FindElement(By.XPath("//*[@id='layoutDrawer_content']/main/div/div[2]/div[6]/a/div/div/div/div[2]"));
            Assert.True(iconRefunded.Displayed);
        }

        void settingDropdownMenu()
        {
            // Increase the window size
            driver.Manage().Window.Maximize();
            // Find the setting section with its descendant and store as a collection
            ReadOnlyCollection<IWebElement> findSettings = driver.FindElements(By.XPath("//*[@id='collapseDashboards']//descendant::a"));


            // Check whether we can get all the options inside settings
            foreach (var item in findSettings)
            {
                // Check whether the option is null or empty. Then whether it's clickable
                var url = item.GetAttribute("href");
                Assert.True(!String.IsNullOrEmpty(url));
                Assert.True(item.Enabled);
            }
        }

        void bookingPage()
        {
            // Find booking section and click to fo to booking page
            IWebElement clickBooking = driver.FindElement(By.XPath("/html/body/nav/div/div/ul/li[2]"));
            clickBooking.Click();

            // Create a wait method to wait the page move to booking page successful after clicking
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Func<IWebDriver, bool> waitForElement = new Func<IWebDriver, bool>((IWebDriver Web) =>
            {
                return Web.Title == "All Bookings View";
            });
            wait.Until(waitForElement);

            // Check whether the Booking page is displayed
            string currentWindowTitle = "All Bookings View";
            Assert.AreEqual(currentWindowTitle, driver.Title);

            // Find and verify Show entries menu has default value is 10
            IWebElement showEntries = driver.FindElement(By.XPath("//select[@name='data_length']"));
            String showDefaultValue = showEntries.GetAttribute("value");
            Assert.True(!String.IsNullOrEmpty(showDefaultValue));
            Assert.AreEqual("10", showDefaultValue);

            // Find and verify seach textbox has the default value is empty
            IWebElement searchBar = driver.FindElement(By.TagName("input"));
            String searchDefaultValue = showEntries.GetAttribute("placeholder");
            Assert.True(String.IsNullOrEmpty(searchDefaultValue));

            // Find the data table in booking page and verify it contains the columns
            driver.Manage().Window.Maximize();
            IWebElement bookingTable = driver.FindElement(By.XPath("//table[@id='data']"));
            List<IWebElement> listThElem = new List<IWebElement>(bookingTable.FindElements(By.TagName("th")));

            foreach (var elemTh in listThElem)
            {
                var thHeader = elemTh.Text;
                Assert.True(elemTh.Enabled);
            }

        }

        // Create a exit process to close the browser whenever all the testing processes is finished
        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}