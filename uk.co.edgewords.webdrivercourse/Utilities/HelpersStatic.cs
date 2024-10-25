using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.webdrivercourse.Utilities
{
    public static class HelpersStatic
    {

        public static IWebElement WaitForElement(IWebDriver driver, By locator, int timeToWait = 3)
        {
            WebDriverWait shortWait = new(driver, TimeSpan.FromSeconds(timeToWait));
            IWebElement element = shortWait.Until(drv => drv.FindElement(locator));
            return element;
        }

        //Put other helpers here

        public static IWebElement WaitForElementByExtendingDriver (this IWebDriver driver, By locator, int timeToWait = 3)
        {
            WebDriverWait shortWait = new(driver, TimeSpan.FromSeconds(timeToWait));
            IWebElement element = shortWait.Until(drv => drv.FindElement(locator));
            return element;
        } //availble on driver in test i.e. driver.WaitForElementByExtendingDriver(By.Css(""));

    }
}
