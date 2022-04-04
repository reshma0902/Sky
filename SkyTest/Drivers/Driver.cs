using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;

namespace SkyTest.Drivers
{
    public class Driver
    {

        private IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        public Driver(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
            
        public IWebDriver Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver(path + @"\drivers\");
            //driver.Navigate().GoToUrl("https://www.sky.com/");


            //Set the driver
            _scenarioContext.Set(driver, "Webdriver");

            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
