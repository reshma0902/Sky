using OpenQA.Selenium;
using SkyTest.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SkyTest.Hooks
{
    [Binding]
    public sealed class HooksInitialisation
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private readonly ScenarioContext _scenarioContext;
        public HooksInitialisation(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            //TODO: implement logic that has to run before executing each scenario
            Driver driver = new Driver(_scenarioContext);
            _scenarioContext.Set(driver, "Driver");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Screenshot
            String img = CommonMethods.SaveScreenshot(_scenarioContext.Get<IWebDriver>("Webdriver"), "Report");
            Console.WriteLine("Screenshot Saved in " + img);
            _scenarioContext.Get<IWebDriver>("Webdriver").Quit();
            Console.WriteLine("Driver quit");

        }
    }
}
