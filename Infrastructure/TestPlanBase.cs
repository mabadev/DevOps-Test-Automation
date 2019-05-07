using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTestExample.Infrastructure
{
    public class TestPlanBase
    {
        public TimeSpan WaitTime
        {
            get
            {
                return TimeSpan.FromSeconds(3);
            }
        }

        public TimeSpan WaitTime1Second
        {
            get
            {
                return TimeSpan.FromSeconds(1);
            }
        }

        public TimeSpan WaitTime3Seconds
        {
            get
            {
                return TimeSpan.FromSeconds(3);
            }
        }

        public TimeSpan WaitTime5Seconds
        {
            get
            {
                return TimeSpan.FromSeconds(5);
            }
        }

        public IWebDriver Driver;
       
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            bool usePhantomJs = false;
            if (usePhantomJs)
            {
                //// Wie extrahiere ich aus einem p12 Zertifikat den private key
                //// https://stackoverflow.com/questions/21001374/what-is-the-correct-way-to-feed-an-ssl-certificate-into-phantomjs
                //PhantomJSDriverService phantomJsDriverService = PhantomJSDriverService.CreateDefaultService();
                //phantomJsDriverService.IgnoreSslErrors = true;
                //phantomJsDriverService.AddArgument("--ssl-client-certificate-file=clientcert.cer");
                //phantomJsDriverService.AddArgument("--ssl-client-key-file=clientcert.key");
                //phantomJsDriverService.AddArgument("--ssl-client-key-passphrase=A1stO2015!");
                //this.Driver = new PhantomJSDriver(phantomJsDriverService);
            }
            else
            {
                //string src = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
                //var versionString = System.Diagnostics.FileVersionInfo.GetVersionInfo(src).FileVersion;
                //var version = Convert.ToInt32(versionString.Split('.').First());
                //if (version < 74)
                //{
                //    Assert.IsTrue(version < 74,
                //        $"The installed chrome version {versionString}  is outdated. Update your browser. At least 74.xxx should be installed.");
                //}

                this.Driver = new ChromeDriver();
            }

            this.Driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 70);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }

        public void ExplicitWait(IWebDriver driver, string id, int seconds, string errorMessage)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            try
            {
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id(id)));
            }
            catch (Exception e)
            {
                throw new Exception(errorMessage);
            }
        }

        public void ExplicitWaitUntilElementExists(IWebDriver driver, string id, int seconds, string errorMessage)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
            }
            catch (Exception e)
            {
                throw new Exception(errorMessage);
            }
        }
    }
}
