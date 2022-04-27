using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Safari;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace phptravel_test
{
    public class Bookings
    {
        public void mainPage(IWebDriver driver)
        {
            // Find booking section and click to fo to booking page
            IWebElement clickBookings = driver.FindElement(By.XPath("/html/body/nav/div/div/ul/li[2]"));
            clickBookings.Click();

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
        }

        public void verifyElem(IWebDriver driver)
        {
            // Find and verify Show entries menu has default value is 10
            int entrDefaultValue = 10;
            IWebElement showEntries = driver.FindElement(By.XPath("//div[@class='dataTables_length']//child::select"));
            String actualDefault = showEntries.GetAttribute("value");
            Assert.True(!String.IsNullOrEmpty(actualDefault));

            int value = Int32.Parse(actualDefault);
            Assert.AreEqual(entrDefaultValue, value);

            // Find and verify seach textbox has the default value is empty
            IWebElement searchBar = driver.FindElement(By.XPath("//div[@class='dataTables_filter']//child::input"));
            String searchValue = showEntries.GetAttribute("placeholder");
            Assert.True(String.IsNullOrEmpty(searchValue));

            // Find the data table in booking page and verify it contains the required columns
            var listColumns = new string[] {"#","ID","Ref","Customer","Module","Supplier","From","Date","Price","Pnr","Booking Status","Payment Status","Execute Booking"};
            ReadOnlyCollection<IWebElement> bookingTable = driver.FindElements(By.XPath("//table[@id='data']//child::tr//th"));
            List<string>actualListColumns = bookingTable.Select(o => o.Text.Trim()).Where(o => !String.IsNullOrEmpty(o)).ToList();
            Assert.True(listColumns.SequenceEqual(actualListColumns));
        }
    }
}
