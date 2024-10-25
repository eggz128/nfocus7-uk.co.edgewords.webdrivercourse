using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.webdrivercourse.POMs
{
    
    public class HomePagePOM
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions

        //Constructor gets the driver from the calling test
        public HomePagePOM(IWebDriver driver) 
        {
            this._driver = driver; //puts the driver in the field above
        }

        //Locators
        private IWebElement _loginLink => _driver.FindElement(By.PartialLinkText("Login"));

        //Service Method
        public void GoLogin()
        {
            _loginLink.Click();
        }
    }
}
