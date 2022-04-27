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
        private Auth auth = new Auth();
        private Bookings booking = new Bookings();

        [SetUp]
        public void startBrowser()
        {
            driver = new SafariDriver();
        }

        [Test()]
        public void Dashboard()
        {
            auth.doLogin(driver);
            verifyItems();
            menuSettings();
            booking.mainPage(driver);
            booking.verifyElem(driver);
        }

        void verifyItems()
        {
            // This method is verifying the required titles and icons in Dashboard page
            Thread.Sleep(5000);
            IWebElement vDashboard = driver.FindElement(By.XPath("//div[@id='layoutDrawer_content']//div[@class='container-xl p-4']/div[1]//h1[@class='display-4 mb-0']"));
            Assert.True(vDashboard.Displayed);

            var expListTexts = new string[] { "Confrimed Bookings", "Pending Bookings", "Cancelled Bookings", "Paid Bookings","Unpaid Bookings","Refunded Bookings"}; 
            ReadOnlyCollection<IWebElement> dashBListText = driver.FindElements(By.XPath("//div[@class='row gx-3']//child::div[@class='card-text']"));
            List<string> dashText = dashBListText.Select(o => o.Text).ToList();
            Assert.True(expListTexts.SequenceEqual(dashText));

            var expListIcons = new string[] { "task_alt", "event", "clear", "credit_score", "production_quantity_limits", "money_off" };
            ReadOnlyCollection<IWebElement> dashBListIcons = driver.FindElements(By.XPath("//div[@class='row gx-3']//child::i[@class='material-icons']"));
            List<string> dashIcon = dashBListIcons.Select(o => o.Text).ToList();
            Assert.True(expListIcons.SequenceEqual(dashIcon));
        }

        void menuSettings()
        {
            // Increase the window size
            driver.Manage().Window.Maximize();

            // Create expectation list of menu settings dropdown
            var expectSettingsList = new string[] { "General Settings", "Updates", "Currencies", "Payment Gateways", "Social Connections", "Homepage Sliders", "Email Templates", "SMS API Settings", "BackUp", "Ban IP" };
            // Find the setting section and get all the options inside the menu to verify those are available
            ReadOnlyCollection<IWebElement> menuSettings = driver.FindElements(By.XPath("//div[@id='collapseDashboards']//child::a"));
            List<string> actualSettingsList = menuSettings.Select(o => o.Text.Trim()).Where(o => !String.IsNullOrEmpty(o)).ToList();
            Assert.True(expectSettingsList.SequenceEqual(actualSettingsList));

            // Verify to ensure there is no null or empty options. Then whether it's clickable
            foreach (var item in menuSettings)
            {
                var url = item.GetAttribute("href");
                Assert.True(!String.IsNullOrEmpty(url));
                Assert.True(item.Enabled);
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