using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SkyTest.Drivers;
using System;
using System.Threading;
using TechTalk.SpecFlow;


namespace SkyTest.Features
{
    [Binding]
    public sealed class SkyTestStepDefinition
    {
        private readonly ScenarioContext _scenarioContext;
        public static string ScreenshotPath = Skytest.ScreenShotPath;

        IWebDriver driver;
        public SkyTestStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the home page")]
        public void GivenIAmOnTheHomePage()
        {
            try
            {
                //for Local
                driver = _scenarioContext.Get<Driver>("Driver").Setup();
                driver.Url = "https://www.sky.com/";
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                //By ele = By.Id("sp_message_iframe_474555");
                //wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(ele));
                //Switch to privacy option
                driver.SwitchTo().Frame(driver.FindElement(By.Id("sp_message_iframe_474555")));
                //To click Agree button in popup
                //driver.FindElement(By.CssSelector("button.message-component:nth-child(2)")).Click();

                //To click Manage preferences
                driver.FindElement(By.CssSelector("button.message-component:nth-child(1)")).Click();
                driver.SwitchTo().DefaultContent();
                driver.SwitchTo().Frame(driver.FindElement(By.Id("sp_message_iframe_204887")));

                driver.FindElement(By.CssSelector("html body div.message-container.global-font div.message.type-modal div.message-component.message-row div.message-component.message-column.unstack button.message-component.message-button.no-children.focusable.sp_choice_type_ACCEPT_ALL")).Click();

            }
            catch(NoSuchElementException ex)
            {
                TestContext.WriteLine("Test Failed : " + ex.Message);
                Assert.Fail("Element not found");
            }



        }

        [When(@"I navigate to ‘Deals’")]
        public void WhenINavigateToDeals()
        {
            try
            {
                driver.SwitchTo().ParentFrame();
                driver.FindElement(By.LinkText("Deals")).Click();
            }
            catch(Exception e)
            {
                Assert.Fail("Test Failed : "+e.Message);
            }

        }

        [Then(@"the user should be on the https://www\.sky\.com/deals page")]
        public void ThenTheUserShouldBeOnTheHttpsWww_Sky_ComDealsPage()
        {
            try
            {
                //Verify the url
                String URL = driver.Url;
                Assert.AreEqual("https://www.sky.com/deals", URL);

                //Verify the header
                String htext = driver.FindElement(By.Id("deals-primary-heading")).Text;
                Assert.AreEqual("Sky Deals", htext);
                TestContext.WriteLine("Test Passed");
                // Screenshot
                String img = CommonMethods.SaveScreenshot(_scenarioContext.Get<IWebDriver>("Webdriver"), "Deals Page");
                TestContext.WriteLine("Screenshot Saved in " + img);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                TestContext.WriteLine(ex);
            }

        }


        [When(@"I try to sign in with invalid (.*)")]
        public void WhenITryToSignInWithInvalid(string Email)
        {
            try
            {
                driver.FindElement(By.LinkText("Sign in")).Click();
                driver.SwitchTo().Frame(driver.FindElement(By.Id("sp_message_iframe_474555")));
                driver.FindElement(By.CssSelector("button.message-component:nth-child(2)")).Click();
                driver.SwitchTo().DefaultContent();


                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='iFrame containing Sky Sign-In application']")));

                driver.FindElement(By.Id("username")).SendKeys(Email);
                driver.FindElement(By.CssSelector(".Buttonstyles__ButtonLabel-sc-1baq2ha-1")).Click();
                driver.SwitchTo().DefaultContent();
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
                TestContext.WriteLine(ex);
                // Screenshot
                String img = CommonMethods.SaveScreenshot(_scenarioContext.Get<IWebDriver>("Webdriver"), "Failed Login");
                TestContext.WriteLine("Screenshot Saved in " + img);
            }

        }

        [Then(@"I should see a box with the text ‘Create your My Sky password’")]
        public void ThenIShouldSeeABoxWithTheTextCreateYourMySkyPassword()
        {
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='iFrame containing Sky Sign-In application']")));
                string msgtext = driver.FindElement(By.XPath("/html/body/div/div/div/main/div/form/div[3]]")).Text;
                Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/div/div/main/div/form/div[3]]")).Displayed, "Create your password text not visible");
                Assert.AreEqual("Create your My Sky password", msgtext);
                TestContext.WriteLine("Test Passed");
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
                TestContext.WriteLine(ex);
            }


        }

        [Given(@"I am on the ‘https://www\.sky\.com/deals‘ page")]
        public void GivenIAmOnTheHttpsWww_Sky_ComDealsPage()
        {
            GivenIAmOnTheHomePage();
            WhenINavigateToDeals();
        }

        [Then(@"I see a list of deals with a price to it")]
        public void ThenISeeAListOfDealsWithAPriceToIt()
        {
            try
            {
                //Manage Privacy options
                driver.SwitchTo().Frame(driver.FindElement(By.Id("sp_message_iframe_474555")));
                driver.FindElement(By.CssSelector("button.message-component:nth-child(2)")).Click();
                driver.SwitchTo().DefaultContent();
                //Check deals
                String deal1 = driver.FindElement(By.Id("price.offer.blt842c920bfe4270ca")).Text;
                String deal2 = driver.FindElement(By.Id("price.offer.blt6fd199dca4c4855d")).Text;
                String deal3 = driver.FindElement(By.Id("price.offer.blt399c437e47d58311")).Text;


                Assert.AreEqual("£26", deal1.Substring(deal1.Length-3));
                Assert.AreEqual("£46", deal2.Substring(6, 3));
                Assert.AreEqual("£38", deal3.Substring(6, 3));
                TestContext.WriteLine("Test Passed");

                // Screenshot
                String img = CommonMethods.SaveScreenshot(_scenarioContext.Get<IWebDriver>("Webdriver"), "Deals Pricing Page");
                TestContext.WriteLine("Screenshot Saved in " + img);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                TestContext.WriteLine("Test Failed : "+ ex.Message);
            }



        }

        [When(@"I search (.*) in the search bar")]
        public void WhenISearchInTheSearchBar(string data)
        {
            try
            {
                driver.FindElement(By.Id("masthead-search-toggle")).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                driver.FindElement(By.XPath("//input[@type='search']")).SendKeys(data);
                TestContext.WriteLine("Test Passed : Able to input in search text");
                Assert.Pass("Search text visible");
            }
            catch(NoSuchElementException e)
            {
                Assert.Fail(e.Message);
                TestContext.WriteLine("Test Failed : " + e.Message);
            }


        }

        [Then(@"I should see (.*) an editorial section\.")]
        public void ThenIShouldSeeAnEditorialSection_(string p0)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                Assert.IsTrue(driver.FindElement(By.Id("search-results-wrapper")).Displayed);

                driver.FindElement(By.XPath("//button[@aria-label='Search']")).Click();

                //Navigate to search to search result
                //Verify the url
                String URL = driver.Url;
                Assert.AreEqual("https://www.sky.com/new-search?q=" + p0, URL);
                TestContext.WriteLine("Test Passed : Able to search data");
                // Screenshot
                String img = CommonMethods.SaveScreenshot(_scenarioContext.Get<IWebDriver>("Webdriver"), "Search result page");
                TestContext.WriteLine("Screenshot Saved in " + img);

            }
            catch (NoSuchElementException e)
            {
                Assert.Fail(e.Message);
                TestContext.WriteLine("Test Failed : " + e.Message);
            }
        }


    }
}
