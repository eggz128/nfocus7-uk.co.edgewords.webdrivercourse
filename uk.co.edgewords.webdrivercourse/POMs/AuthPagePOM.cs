using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace uk.co.edgewords.webdrivercourse.POMs
{
    public class AuthPagePOM
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions

        //Constructor gets the driver from the calling test
        public AuthPagePOM(IWebDriver driver)
        {
            this._driver = driver; //puts the driver in the field above
            
            string pageUrl = _driver.Url;
            Assert.That(pageUrl, Does.Match("$add_record.php"), "Not on correct page?");
        }

        //Locators
        public IWebElement _usernameField => _driver.FindElement(By.Id("username"));
        private IWebElement _passwordField => Utilities.HelpersStatic.WaitForElement(_driver, By.Id("username"));
        private IWebElement _submitFormButton => new WebDriverWait(_driver, TimeSpan.FromSeconds(3))
                                                        .Until(drv => drv.FindElement(By.LinkText("Submit")));

        //Service Methods
        public AuthPagePOM SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);
            return this;
        }

        public AuthPagePOM SetPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);
            return this;
        }

        public void SubmitForm()
        {
            _submitFormButton.Click();
            //return new AddrecordPOM(_driver); //You could fluently move to the next page by returning it's instantiated page class (I don't like this)
        }

        //Higher level "Helper" methods
        public bool LoginExpectSuccess(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
            SubmitForm();
            try
            {
                _driver.SwitchTo().Alert();
                return false;
            } 
            catch (NoAlertPresentException e)
            {
                Console.WriteLine("No Alert found. Assume login worked");
                return true;
            }
        }

        public bool InvalidLoginExpectFail(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
            SubmitForm();
            try
            {
                _driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException e)
            {
                Console.WriteLine("Expected failiure message - none found");
                return false;
            }
        }


    }
}
