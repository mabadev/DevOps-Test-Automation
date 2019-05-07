using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTestExample.Infrastructure
{
    public class TestPlanBase
    {
        public IWebDriver Driver;
       
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            this.Driver = new ChromeDriver();
            this.Driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 70);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }
    }
}
