using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using ReadCsvFiles;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Android;
using System.Diagnostics;
using StepsDefinitionTests;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using NUnit.Framework.Interfaces;
using System.Linq;

namespace AccuWeather
{
    [TestFixture]
    class TestUICatalog
    {
        AppiumDriver<IWebElement> driver;
        StepsToSetUpDevice stepSetUpDevice;
        StepsToTermsAndConditions stepTermConditions;
        StepsToMenuOptions stepMenuOptions;
        StepsToAddLocation stepAddLocation;
        StepsToEditLocation StepEditLocation;
        static int runtimes = 0;  
        static string appActivityMainScreen = "com.accuweather.app.MainActivity";
        static string allowChars = "Coc,Santa,i,o,u,c,s,d,un,b";

        [OneTimeSetUp]
        public void oneTimeSetUp()
        {
            String testCaseName = TestContext.CurrentContext.Test.Name;

            runtimes++;
            stepSetUpDevice = new StepsToSetUpDevice();
            driver = stepSetUpDevice.run_driver_Browser_on_mac_agent(testCaseName, appActivityMainScreen);
            stepTermConditions = new StepsToTermsAndConditions(driver);
            //stepTermConditions.accept_termns_and_conditions();
            stepMenuOptions = new StepsToMenuOptions(driver);
            stepAddLocation = new StepsToAddLocation(driver);
            StepEditLocation = new StepsToEditLocation(driver);
            bool setUpEnviroment = add_several_locations(2);
            Assert.That(setUpEnviroment, Is.True, "The Test case cannot be executed because the enviroment cannot be created");
            
        }
        [SetUp]
        public void setUp()
        {
            if (runtimes>1)
            {
                String testCaseName = TestContext.CurrentContext.Test.Name;

                stepSetUpDevice = new StepsToSetUpDevice();
                driver = stepSetUpDevice.run_driver_without_install(testCaseName, appActivityMainScreen);
                stepTermConditions = new StepsToTermsAndConditions(driver);
                //stepTermConditions.accept_termns_and_conditions();
                stepMenuOptions = new StepsToMenuOptions(driver);
                stepAddLocation = new StepsToAddLocation(driver);
                StepEditLocation = new StepsToEditLocation(driver);
                runtimes++;
                

                            }
            runtimes++;
        }

        [Test]
        public void TCIOS01_chagne_to_metrics_system()
        {
            Assert.AreEqual(" KM/H", "KM/H");
                        
        }
        
        
        

        [OneTimeTearDown]
        public void cleanUp()
        {
            bool passed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            try
            {
                // Logs the result to Sauce Labs
                ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            catch
            {
                driver.Quit();
            }
            finally
            {
                // Terminates the remote webdriver session
                driver.Quit();
            }


        }
        public bool add_several_locations(int number)
        {
            try
            {
                bool precondition_add_locations = true;
                List<string> characters_allowed_this_set_up = allowChars.Split(',').OfType<string>().ToList();
                for (int i = 0; i < number; i++)
                {

                    if (stepMenuOptions.menu_add_location() == true)
                    {
                        string location = characters_allowed_this_set_up[0].ToString();


                        if (stepAddLocation.add_location(location) == true)
                        {
                            characters_allowed_this_set_up.RemoveAt(0);
                        }
                        else
                        {
                            precondition_add_locations = false;
                            break;
                        }
                    }
                    else
                    {
                        precondition_add_locations = false;
                        break;
                    }
                }
                return precondition_add_locations;
            }
            catch
            {
                return false;
            }

        }

    }
}
