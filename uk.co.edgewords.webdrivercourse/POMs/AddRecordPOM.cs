using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.webdrivercourse.POMs
{
    internal class AddRecordPOM
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions

        //Constructor gets the driver from the calling test
        public AddRecordPOM(IWebDriver driver)
        {
            this._driver = driver; //puts the driver in the field above
        }

        //Locators
        public string bodyText 
        {
            get => _driver.FindElement(By.TagName("body")).Text;
        }


    }
}
