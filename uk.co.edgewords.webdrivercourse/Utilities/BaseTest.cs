using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Interactions;

[assembly: Parallelizable(ParallelScope.Fixtures)] //Can only parallelise Features
[assembly: LevelOfParallelism(8)] //Worker thread i.e. max amount of Features to run in Parallel
namespace uk.co.edgewords.webdrivercourse.Utilities
{
    

    public class BaseTest
    {
        //Field to hold a WebDriver instance
        protected IWebDriver driver;


        [SetUp]
        public void SetUp2()
        {
            string baseUrl = TestContext.Parameters["baseURL"];
            string browser = Environment.GetEnvironmentVariable("BROWSER");

            //Should be doing some null checks in case the env var isnt set.

            switch (browser)
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;

                default:
                    driver = new EdgeDriver();
                    break;
            }

            driver.Url = baseUrl;
        }



        public void Setup()
        {
            /*
             * Launching browsers in various configurations
             */
            //Launch Chrome(Driver)
            //driver = new ChromeDriver();

            //Launch Chrome(Driver) with options
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--start-maximized"); //There are better ways to maximise that are x browser friendly
            //options.AddArgument("--headless"); //Don't show browser on screen
            //options.BrowserVersion = "beta"; //Selenium-Manager can fetch specific browser version numbers or channels (stable/beta/dev/canary/esr (firefox) ) for testing.

            driver = new ChromeDriver(options);

            //Launch Chrome in Mobile Emulation mode
            //Note this is more of a simulation than actual emulation
            //https://developer.chrome.com/docs/chromedriver/mobile-emulation
            //https://www.youtube.com/watch?v=Mo6LmFGrtxY - 
            //ChromeOptions options = new ChromeOptions();
            //options.EnableMobileEmulation("Pixel 7"); //Use a built in device description

            //or make your own device
            //ChromiumMobileEmulationDeviceSettings mobOpts = new() 
            //{
            //    EnableTouchEvents = true,
            //    Height = 300,
            //    Width = 300,
            //    PixelRatio = 1.5,
            //    UserAgent = "Steves weird mobile web browser 1.0 (like Gecko)"
            //};
            //options.EnableMobileEmulation(mobOpts);

            //driver = new ChromeDriver(options);



            //Launch Firefox
            //driver = new FirefoxDriver();

            //Launch Firefox with options
            //FirefoxOptions options = new FirefoxOptions();
            //options.AddArgument("--headless"); //Don't show browser on screen
            //driver = new FirefoxDriver(options);

            //Launching Edge(Chromium) - use EdgeOptions object to pass options. See ChromeDriver for details
            //driver = new EdgeDriver(); 

            //Attempting to launch IE - no wait dont
            //driver = new InternetExplorerDriver(); //Can't be done on Win11 24H2 🙌😁 as protected mode checkbox is missing
            //On older Windows IE11 is still problematic but possible. 

            //Remote WebDriver - The Selenium Grid. BrowserStack, Saucelabs, Perfecto Mobile etc.
            //ChromeOptions options = new ChromeOptions(); //To choose the webbrowser in Sel4 pass an options object of the appropriate type to RemoteWebDriver when instantiating.
            //FirefoxOptions options = new FirefoxOptions();
            //EdgeOptions options = new EdgeOptions();
            //driver = new RemoteWebDriver(new Uri("http://192.168.178.175:4444/wd/hub"), options); //Runs on Steve's Linux VM

            //Selenium3 - you would need to pass an appropriately configured DesiredCapabilities object. Because you "desire" to run on some configuration. You're not actually *guarenteed* to get all that you "desire"...





            /*
             * Enable implicit waits for the launched browser
             */


            //Turn on implicit waits - OK as a 'crutch' to get started, but look to eliminate these in production tests.
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //Good for getting up and running - but not a long term solution (can be buggy, other drawbacks too

            //See warning in docs: https://www.selenium.dev/documentation/webdriver/waits/

            //This is the type of thing that future WebDriver BiDi may make reliable.
        }

        [TearDown]
        public void Teardown()
        {
            Thread.Sleep(5000); //Just there so we can see the browser before it quits
            driver.Quit();
            
        }
    }
}
