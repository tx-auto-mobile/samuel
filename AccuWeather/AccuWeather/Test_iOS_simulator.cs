using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuWeather
{
    class Test_iOS_simulator
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void Class1()
        {
            //Setting Capabilities
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("platformName", "iOS");
            capabilities.SetCapability("platformVersion", "9.3");
            capabilities.SetCapability("platform", "Mac");
            capabilities.SetCapability("deviceName", "iPhone 6");
            capabilities.SetCapability("app", "/users/hinda-qa/desktop/Edhita.app");
            
            //capabilities.SetCapability("NO_RESET", true);
            //Connecting to Appium Server
            driver = new RemoteWebDriver(new Uri("http://192.168.120.48:4723/wd/hub"), capabilities, TimeSpan.FromMinutes(10)); 
        }

        [Test]
        public void TCIOS_simulator01_NewFile()
        {
            //Test to login into app
            driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIANavigationBar[1]/UIAButton[2]")).Click();
            driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIAActionSheet[1]/UIACollectionView[1]/UIACollectionCell[1]")).Click();
            driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIAAlert[1]/UIAScrollView[1]/UIACollectionView[1]/UIACollectionCell[1]/UIATextField[1]")).SendKeys("Sam");
            driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIAAlert[1]/UIACollectionView[1]/UIACollectionCell[2]/UIAButton[1]")).Click();
            Assert.AreEqual("Sam", driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIATableView[1]/UIATableCell[1]/UIAStaticText[1]")).Text);
            // driver.FindElement(By.XPath("//UIATextField[1]")).SendKeys("username");
            // driver.FindElement(By.XPath("///UIASecureTextField[1]")).SendKeys("password");
            //driver.FindElement(By.XPath("///UIAButton[1]")).Click();
        }
        [Test]
        public void TCIOS_simulator02_openExampleFolder()
        {
            //Test to login into app
            go_home();
            driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIATableView[1]/UIATableCell[@name='example']")).Click();
            Assert.AreEqual("example", driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIANavigationBar[1]/UIAStaticText[1]")).Text);
            driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIANavigationBar[1]/UIAButton[2]")).Click();
            // driver.FindElement(By.XPath("//UIATextField[1]")).SendKeys("username");
            // driver.FindElement(By.XPath("///UIASecureTextField[1]")).SendKeys("password");
            //driver.FindElement(By.XPath("///UIAButton[1]")).Click();
        }
        [Test]
        public void TCIOS_simulator03_goBack()
        {
            //Test to open exaple folder and then go back
            go_home();
            driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIATableView[1]/UIATableCell[1]")).Click();
            driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIANavigationBar[1]/UIAButton[2]")).Click();
            Assert.AreEqual("Documents", driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIANavigationBar[1]/UIAStaticText[1]")).Text);
         
        }
        private void go_home()
        {
            while (driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIANavigationBar[1]/UIAStaticText[1]")).Text != "Documents")
            {
                driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIANavigationBar[1]/UIAButton[@name='Back']")).Click();
            }
        }

        [OneTimeTearDown]
        public void End()
        {
            driver.Quit();
        }
    }
}
