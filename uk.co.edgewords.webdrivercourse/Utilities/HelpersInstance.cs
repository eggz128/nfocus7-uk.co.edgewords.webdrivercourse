using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.webdrivercourse.Utilities
{
    public class HelpersInstance
    {
        private IWebDriver _driver;

        public HelpersInstance(IWebDriver driver)
        {
            this._driver = driver;
        }

        public IWebElement WaitForElement(By locator, int timeToWait = 3)
        {
            WebDriverWait shortWait = new(_driver, TimeSpan.FromSeconds(timeToWait));
            IWebElement element = shortWait.Until(drv => drv.FindElement(locator));
            return element;
        }

        //Other instance method hepers can be added here as required

    }
}
