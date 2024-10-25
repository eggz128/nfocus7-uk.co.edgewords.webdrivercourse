using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.edgewords.webdrivercourse.POMs;

namespace uk.co.edgewords.webdrivercourse.Test_Cases
{
    internal class POMTests : Utilities.BaseTest
    {

        [Test]
        public void LogoutTest()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/";
            HomePagePOM homePage = new HomePagePOM(driver);
            homePage.GoLogin(); //Test doesnt how to find the login link, or how to click it. It just asks the POM page to do an action.
        
            //Should now be on Auth page
            AuthPagePOM authPage = new(driver);
            //authPage.SetUsername("edgewords");
            //authPage.SetPassword("edgewords123");
            //authPage.SubmitForm();

            //authPage.LoginExpectSuccess("edgewords", "edgewords123");

            //When a service method returns "this" you can use fleunt method chaining
            //authPage.SetUsername("edgewords").SetPassword("edgewords123").SubmitForm();

            //Check we are logged in based on status text
            AddRecordPOM addRecordPage = new AddRecordPOM(driver);
            //bool loggedIn = addRecordPage.bodyText.Contains("User is Logged in ");
            //Assert.That(loggedIn, Is.True); //Dont do this - here's a better way:

            Assert.That(addRecordPage.bodyText, Does.Contain("User is Logged in"), "Didn't find login text");


        }

        [Test]
        public void AttemptLoginWithInvalidData()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/";
            HomePagePOM homePage = new HomePagePOM(driver);
            homePage.GoLogin();

            AuthPagePOM authPage = new(driver);
            authPage._usernameField.SendKeys("edgewords");
            //authPage._usernameField.FindElement(By.XPath("../../..")).Click(); //If WebELements are returned from the Page Class, then further find ops could be chained on (leaking the web driver instance)

            bool didNotLogin = authPage.InvalidLoginExpectFail("invalid", "invalid");
            Assert.That(didNotLogin, Is.True, "No alert - we somehow logged in");
        }

    }
}
