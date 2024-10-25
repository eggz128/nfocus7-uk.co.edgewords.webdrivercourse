using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.edgewords.webdrivercourse.Utilities;
using static uk.co.edgewords.webdrivercourse.Utilities.HelpersStatic;


namespace uk.co.edgewords.webdrivercourse.Test_Cases
{
    [TestFixture]
    internal class HelloWebDriverTest : Utilities.BaseTest
    {
        

        [Test, Order(1)]
        [Category("Testing")]
        public void FirstTest()
        {
            Console.WriteLine("Test start");
            //We have access to the driver field
            //The driver should now have an instance of ChromeDriver in it frpom [Setup]
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/";
            //driver.Navigate().GoToUrl("https://www.edgewordstraining.co.uk/webdriver2/"); //Exactly the same but with extra typing.

            //Find and click the Login link
            //"Chrome" please "find an element" - "how?" by looking for a link which includes the text "Login". What then? "Click it".
            driver.FindElement(By.PartialLinkText("Login")).Click();

            //Remember to log important events
            Console.WriteLine("Should now be on login page");

            //Find the username element By using CSS, then send some keys (type) in to it.
            driver.FindElement(By.CssSelector("#username")).SendKeys("edgewords");

            //Find and store reference to password field for later use
            //Note if the element "goes away" for any reason and then comes back - you shoud re-find the element before interacting
            //Or you will get a StaleElementException. Particularly problematic with AJAX/Asynchronous content 
            IWebElement passwordField = driver.FindElement(By.CssSelector("#password"));
            passwordField.SendKeys("edge");
            passwordField.SendKeys("words123"); //sendKeys doesn't clear already present text - just appends to it
            //passwordField.Clear(); //"Magically" clears the field - ok 99.999% of the time
            passwordField.SendKeys(Keys.Control + "a"); //But you can do it "Like a user" would
            passwordField.SendKeys(Keys.Backspace);
            //Put the password back in
            passwordField.SendKeys("edgewords123");


            //Getting text from the page examples
            string headingText = driver.FindElement(By.CssSelector("#right-column > h1")).Text;
            Console.WriteLine("The heading Text is: " + headingText);

            //string passwordText = passwordField.Text; //input (text boxes) don't have inner text to capture
            string passwordText = passwordField.GetAttribute("value");
            Console.WriteLine("The typed password text is: " + passwordText);


            //Click submit "button" (it's actually a link)
            driver.FindElement(By.LinkText("Submit")).Click(); //Remember C# - and webdriver - is CaSe SeNsItIve - "submit" won't work - NoSuchElementExcepton

            Console.WriteLine("Test Finished");
        }

        [Test]
        public void DragDropDemo()
        {
            //Navigate to apple slider page
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/docs/cssXPath.html";

            IWebElement gripper = driver.FindElement(By.CssSelector("#slider > a"));
            IWebElement footer = driver.FindElement(By.Id("footer")); //Element at bottom of page
            //XBrowser friendly scroll using injected Javascript
            var js = (IJavaScriptExecutor)driver; //Not all drivers can run JS. To gain that ability cast to JavaScriptExecutor
            //var js2 = driver as IJavaScriptExecutor; //Alternative cast syntax
            js.ExecuteScript("arguments[0].scrollIntoView()", footer);
            
            
            Actions actions = new Actions(driver);
            IAction action = actions
                //.ScrollToElement(footer) //Chrome only scroll
                .ClickAndHold(gripper)
                .MoveByOffset(10, 0) //Do lots of 'little' steps to workaround site problem in Chrome
                .MoveByOffset(10, 0)
                .MoveByOffset(10, 0)
                .MoveByOffset(10, 0)
                .MoveByOffset(10, 0)
                .MoveByOffset(10, 0)
                .MoveByOffset(10, 0)
                .Release() //If you 'held' a button down - make sure you release it
                .Build();
            action.Perform();

        }

        [Test]
        public void VisualRelationshipTest()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/sdocs/auth.php";
            IWebElement usernameField = driver.FindElement(By.Id("username"));
            IWebElement passwordField = driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Below(usernameField));
            passwordField.SendKeys("Works");
        }

        [Test, Order(2)]
        [Category("Smoke")]
        public void SynchronisationTest()
        {
            Console.WriteLine("Test start");
            //We have access to the driver field
            //The driver should now have an instance of ChromeDriver in it frpom [Setup]
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/";
            //driver.Navigate().GoToUrl("https://www.edgewordstraining.co.uk/webdriver2/"); //Exactly the same but with extra typing.

            //Find and click the Login link
            //"Chrome" please "find an element" - "how?" by looking for a link which includes the text "Login". What then? "Click it".
            driver.FindElement(By.PartialLinkText("Login")).Click();

            //Some sync needed?
            //Thread.Sleep(3000);
            WebDriverWait shortWait = new(driver, TimeSpan.FromSeconds(2));
            shortWait.Until(drv => drv.FindElement(By.CssSelector("#username")));

            //Remember to log important events
            Console.WriteLine("Should now be on login page");

            //Find the username element By using CSS, then send some keys (type) in to it.
            driver.FindElement(By.CssSelector("#username")).SendKeys("edgewords");

            //Find and store reference to password field for later use
            //Note if the element "goes away" for any reason and then comes back - you shoud re-find the element before interacting
            //Or you will get a StaleElementException. Particularly problematic with AJAX/Asynchronous content 
            IWebElement passwordField = driver.FindElement(By.CssSelector("#password"));
            passwordField.SendKeys("edge");
            passwordField.SendKeys("words123"); //sendKeys doesn't clear already present text - just appends to it
            //passwordField.Clear(); //"Magically" clears the field - ok 99.999% of the time
            passwordField.SendKeys(Keys.Control + "a"); //But you can do it "Like a user" would
            passwordField.SendKeys(Keys.Backspace);
            //Put the password back in
            passwordField.SendKeys("edgewords123");


            //Getting text from the page examples
            string headingText = driver.FindElement(By.CssSelector("#right-column > h1")).Text;
            Console.WriteLine("The heading Text is: " + headingText);
            Console.WriteLine("Hot Reload");
            //string passwordText = passwordField.Text; //input (text boxes) don't have inner text to capture
            string passwordText = passwordField.GetAttribute("value");
            Console.WriteLine("The typed password text is: " + passwordText);


            //Click submit "button" (it's actually a link)
            driver.FindElement(By.LinkText("Submit")).Click(); //Remember C# - and webdriver - is CaSe SeNsItIve - "submit" won't work - NoSuchElementExcepton

            //Now log out
            //Thread.Sleep(3000); //Explicit Unconditional wait - Problem: stuck in this long wait
            
            //Best solution - wait for "somthing" up to a given period of time
            //If it happens move on straight away
            //If it hasnt happened by timeout - then die (we would have done this anyway)

            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //myWait.PollingInterval = TimeSpan.FromMilliseconds(750d);
            //IWebElement logoutLink = myWait.Until(drv => drv.FindElement(By.LinkText("Log Out")));
            //logoutLink.Click();

            myWait.Until(drv => drv.FindElement(By.LinkText("Log Out")).Displayed);

            driver.FindElement(By.LinkText("Log Out")).Click();

            //Click OK on alert
            driver.SwitchTo().Alert().Accept();


            WebDriverWait longWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            longWait.Until(drv => drv.FindElement(By.CssSelector("#username"))).SendKeys("Finished");
            
            Console.WriteLine("Test Finished");
        }

        [Test]
        public void ActuallyALogoutTest()
        {
            //Arrange - get ready to check log out works


            Console.WriteLine("Test start");
            //We have access to the driver field
            //The driver should now have an instance of ChromeDriver in it frpom [Setup]
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/";
            //driver.Navigate().GoToUrl("https://www.edgewordstraining.co.uk/webdriver2/"); //Exactly the same but with extra typing.

            //Find and click the Login link
            //"Chrome" please "find an element" - "how?" by looking for a link which includes the text "Login". What then? "Click it".
            driver.FindElement(By.PartialLinkText("Login")).Click();

            //Some sync needed?
            //Thread.Sleep(3000);
            //Using Static helper in HelpersStatic.cs
            //WaitForElement(driver, By.CssSelector("#username"), 4);

            //Using an instance of HelpersInstance
            var helpLib = new HelpersInstance(driver);
            helpLib.WaitForElement(By.CssSelector("#username"), 4);

            string title = driver.Title; //Capture the <title> elements text. .Text wont work as it is not visible on the page

            //"Sanity check" before we continue with the test - are our basic assumptions met:
            //"We should not already be logged in"
            //But - if we are we - maybe we can continue anyway

            //Nunit 2.x / early nunit 3 - use try/catch for validation (continue after fail)
            //try
            //{
            //    Assert.That(driver.FindElement(By.TagName("body")).Text, Does.Contain("User is not logged in XXXX").IgnoreCase, "Already logged in");
            //}
            //catch
            //{
            //    //Do nothing - test continues but NUnit will log the fail
            //      //Future Warning: Specflow LivingDoc will *not* see this as a fail
            //}

            //Nunit 4 - Warn if
            //Generates a warning if the constraint *passes*, but continues with test
            //Warn.If(driver.FindElement(By.TagName("body")).Text,
            //    Does.Not.Contain("User is not logged inXXX").IgnoreCase,
            //    "Already logged in - continuing test anyway");

            //Similar to Assert -- but marks the test as "Inconclusive" if the constraint fails instead of outraight *fail*
            //Assume.That(driver.FindElement(By.TagName("body")).Text,
            //    Does.Contain("User is not logged inXXX").IgnoreCase,
            //    "Already logged in - stopping test");




            //Remember to log important events
            Console.WriteLine("Should now be on login page");

            //Find the username element By using CSS, then send some keys (type) in to it.
            driver.FindElement(By.CssSelector("#username")).SendKeys("edgewords");

            //Find and store reference to password field for later use
            //Note if the element "goes away" for any reason and then comes back - you shoud re-find the element before interacting
            //Or you will get a StaleElementException. Particularly problematic with AJAX/Asynchronous content 
            IWebElement passwordField = driver.FindElement(By.CssSelector("#password"));
            passwordField.SendKeys("edge");
            passwordField.SendKeys("words123"); //sendKeys doesn't clear already present text - just appends to it
            //passwordField.Clear(); //"Magically" clears the field - ok 99.999% of the time
            passwordField.SendKeys(Keys.Control + "a"); //But you can do it "Like a user" would
            passwordField.SendKeys(Keys.Backspace);
            //Put the password back in
            passwordField.SendKeys("edgewords123");


            //Getting text from the page examples
            string headingText = driver.FindElement(By.CssSelector("#right-column > h1")).Text;
            Console.WriteLine("The heading Text is: " + headingText);
            Console.WriteLine("Hot Reload");
            //string passwordText = passwordField.Text; //input (text boxes) don't have inner text to capture
            string passwordText = passwordField.GetAttribute("value");
            Console.WriteLine("The typed password text is: " + passwordText);


            //Click submit "button" (it's actually a link)
            driver.FindElement(By.LinkText("Submit")).Click(); //Remember C# - and webdriver - is CaSe SeNsItIve - "submit" won't work - NoSuchElementExcepton

            //Now log out
            //Thread.Sleep(3000); //Explicit Unconditional wait - Problem: stuck in this long wait

            //Best solution - wait for "somthing" up to a given period of time
            //If it happens move on straight away
            //If it hasnt happened by timeout - then die (we would have done this anyway)

            //WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //myWait.PollingInterval = TimeSpan.FromMilliseconds(750d);
            //IWebElement logoutLink = myWait.Until(drv => drv.FindElement(By.LinkText("Log Out")));
            //logoutLink.Click();

            //myWait.Until(drv => drv.FindElement(By.LinkText("Log Out")).Displayed);

            var logoutButton = WaitForElement(driver, By.LinkText("Log Out")); //Default of 3s wait will be fine


            //Act - click logout
            //driver.FindElement(By.LinkText("Log Out")).Click();
            logoutButton.Click();

            //Click OK on alert
            driver.SwitchTo().Alert().Accept();


            //WebDriverWait longWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //longWait.Until(drv => drv.FindElement(By.CssSelector("#username"))).SendKeys("Finished");

            //Reuse helpLib object created earlier
            helpLib.WaitForElement(By.CssSelector("#username"), 10).SendKeys("Finished");
            

            //Assert - Check that clicking logout does result in us being logged out
            string loggedInStatus = driver.FindElement(By.TagName("body")).Text;
            Assert.That(loggedInStatus,
                Does.Contain("User is NOT logged in").IgnoreCase,
                "Somehow, we were still logged in");

            //A failing assert stops the test. No following asserts will run.
            //bool truthy = true;
            //int one = 1;
            //Assert.That(truthy, Is.False, "True is not False"); //Fail and stop
            //Assert.That(one, Is.EqualTo(0), "One is not 0"); //Will not be checked

            //If you want to run multiple assertions do this:
            //Assert.Multiple(() =>
            //{
            //    Assert.That(truthy, Is.False, "True is not False"); //This will run
            //    Assert.That(one, Is.EqualTo(0), "One is not 0"); //As will this
            //}); //Then the test will fail


            Console.WriteLine("Test Finished");

        }


        [Test]
        public void ScreenshotDemo()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/docs/forms.html";

            var ssDriver = driver as ITakesScreenshot;
            var screenshot = ssDriver.GetScreenshot();
            screenshot.SaveAsFile(@"D:\Screenshots\fullpage.png");

            var ss = driver.TakeScreenshot();
            ss.SaveAsFile(@"D:\Screenshots\fullpage2.png");

            IWebElement form = driver.FindElement(By.CssSelector("div#right-column"));
            var ssForm = form as ITakesScreenshot;
            var formScreenshot = ssForm.GetScreenshot();
            formScreenshot.SaveAsFile(@"D:\Screenshots\form.png");

            TestContext.AddTestAttachment(@"D:\Screenshots\form.png");
        }


    }
}
