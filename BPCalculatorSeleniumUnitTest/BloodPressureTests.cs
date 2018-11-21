using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;

namespace BPCalculatorSeleniumUnitTest
{
    /// <summary>
    /// Summary description for MySeleniumTests
    /// </summary>
    [TestClass]
    public class BloodPressureTests
    {
        private TestContext testContextInstance;
        private IWebDriver driver;
        private string appURL;


        public BloodPressureTests()
        {
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void BasicPageLoadHeaderCheck_True()
        {
            driver.Navigate().GoToUrl(appURL + "/");
            driver.FindElement(By.XPath("/html/body/div/h4")).Click();
            Assert.IsTrue(driver.Title.Contains("BP Category Calculator"), "Verified title of the page");
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void BasicPageLoadHeaderCheck_False()
        {
            driver.Navigate().GoToUrl(appURL + "/");
            driver.FindElement(By.XPath("//*[@id='form1']/div[1]/label")).Click();
            Assert.IsFalse(driver.Title.Contains("BPI Category Calculators"), "Verified title of the page");
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void ReturnNormalBloodPressure_True()
        {
            driver.Navigate().GoToUrl(appURL + "/");
            driver.FindElement(By.Id("BP_Systolic")).Clear();
            driver.FindElement(By.Id("BP_Systolic")).SendKeys("110");
            driver.FindElement(By.Id("BP_Diastolic")).Click();
            driver.FindElement(By.XPath("//*[@id='form1']/div[3]"));
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("Normal Blood Pressure"));
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void ReturnNormalBloodPressure_False()
        {
            driver.Navigate().GoToUrl(appURL + "/");
            driver.FindElement(By.Id("BP_Systolic")).Clear();
            driver.FindElement(By.Id("BP_Systolic")).SendKeys("130");
            driver.FindElement(By.Id("BP_Diastolic")).Click();
            driver.FindElement(By.XPath("//*[@id='form1']/div[3]"));
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains("Normal Blood Pressure"));
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void ReturnHighBloodPressure_True()
        {
            driver.Navigate().GoToUrl(appURL + "/");
            driver.FindElement(By.Id("BP_Systolic")).Clear();
            driver.FindElement(By.Id("BP_Systolic")).SendKeys("170");
            driver.FindElement(By.Id("BP_Diastolic")).Clear();
            driver.FindElement(By.Id("BP_Diastolic")).SendKeys("95");
            driver.FindElement(By.Id("BP_Systolic")).Click();
            driver.FindElement(By.XPath("//*[@id='form1']/div[3]"));
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("High Blood Pressure"));
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void ReturnHighBloodPressure_False()
        {
            driver.Navigate().GoToUrl(appURL + "/");
            driver.FindElement(By.Id("BP_Systolic")).Clear();
            driver.FindElement(By.Id("BP_Systolic")).SendKeys("100");
            driver.FindElement(By.Id("BP_Diastolic")).Clear();
            driver.FindElement(By.Id("BP_Diastolic")).SendKeys("60");
            driver.FindElement(By.Id("BP_Systolic")).Click();
            driver.FindElement(By.XPath("//*[@id='form1']/div[3]"));
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains("High Blood Pressure"));
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void SystolicGreaterThanDiastolic_True()
        {
            driver.Navigate().GoToUrl(appURL + "/");
            driver.FindElement(By.Id("BP_Systolic")).Clear();
            driver.FindElement(By.Id("BP_Systolic")).SendKeys("89");
            driver.FindElement(By.Id("BP_Diastolic")).Clear();
            driver.FindElement(By.Id("BP_Diastolic")).SendKeys("95");
            driver.FindElement(By.Id("BP_Systolic")).Click();
            driver.FindElement(By.XPath("//*[@id='form1']/div[3]"));
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("Systolic must be greater than Diastolic"));
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void SystolicGreaterThanDiastolic_False()
        {
            driver.Navigate().GoToUrl(appURL + "/");
            driver.FindElement(By.Id("BP_Systolic")).Clear();
            driver.FindElement(By.Id("BP_Systolic")).SendKeys("89");
            driver.FindElement(By.Id("BP_Diastolic")).Clear();
            driver.FindElement(By.Id("BP_Diastolic")).SendKeys("63");
            driver.FindElement(By.Id("BP_Systolic")).Click();
            driver.FindElement(By.XPath("//*[@id='form1']/div[3]"));
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains("Systolic must be greater than Diastolic"));
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void SetupTest()
        {
            appURL = "http://localhost:40327/bloodpressure";

            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"));
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }
}