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
    class TestChangeSettings
    {
        AppiumDriver<IWebElement> driver;
        StepsToSetUpDevice stepSetUpDevice;
        StepsToTermsAndConditions stepTermConditions;
        StepsToMenuOptions stepMenuOptions;
        StepsToAddLocation stepAddLocation;
        StepsToEditLocation StepEditLocation;
        static string appActivityMainScreen = "com.accuweather.app.MainActivity";
        static string allowChars = "Coc,Santa,i,o,u,c,s,d,un,b";

        [OneTimeSetUp]
        public void setUpOnce()
        {
            String testCaseName = TestContext.CurrentContext.Test.Name;


            stepSetUpDevice = new StepsToSetUpDevice();
            driver = stepSetUpDevice.run_driver_with_install(testCaseName, appActivityMainScreen);
            stepTermConditions = new StepsToTermsAndConditions(driver);
            //stepTermConditions.accept_termns_and_conditions();
            stepMenuOptions = new StepsToMenuOptions(driver);
            stepAddLocation = new StepsToAddLocation(driver);
            StepEditLocation = new StepsToEditLocation(driver);
            bool setUpEnviroment = add_several_locations(2);
            Assert.That(setUpEnviroment, Is.True, "The Test case cannot be executed because the enviroment cannot be created");
        }
        [OneTimeSetUp]
        public void setUp()
        {
            String testCaseName = TestContext.CurrentContext.Test.Name;


            stepSetUpDevice = new StepsToSetUpDevice();
            driver = stepSetUpDevice.run_driver_with_install(testCaseName, appActivityMainScreen);
            stepTermConditions = new StepsToTermsAndConditions(driver);
            //stepTermConditions.accept_termns_and_conditions();
            stepMenuOptions = new StepsToMenuOptions(driver);
            stepAddLocation = new StepsToAddLocation(driver);
            StepEditLocation = new StepsToEditLocation(driver);
            bool setUpEnviroment = add_several_locations(2);
            Assert.That(setUpEnviroment, Is.True, "The Test case cannot be executed because the enviroment cannot be created");
        }



        [Test]
        public void TC016_chagne_to_metrics_system()
        {
            if (stepMenuOptions.verify_menu_displayed() == true)
            {
                Assert.That(stepMenuOptions.touch_outside_element_by_id(), Is.True, "Problems to touch outside of the Menu");//close menu option 
            }
            Assert.That(stepMenuOptions.click_more_options(), Is.True, "More options button is not found");
            Assert.That(stepMenuOptions.click_settings_option(), Is.True, "Settings button is not found");
            Assert.That(stepMenuOptions.click_units_option(), Is.True, "Units option is not found");
            Assert.That(stepMenuOptions.click_metric_option(), Is.True, "Metric option is not found");
            stepMenuOptions.click_back_android_button();
            stepMenuOptions.click_back_android_button();
            Assert.AreEqual(" KM/H", stepMenuOptions.get_units_text());
                        
        }
        [Test]
        public void TC017_chagne_to_imperial_system()
        {
            if (stepMenuOptions.verify_menu_displayed() == true)
            {
                Assert.That(stepMenuOptions.touch_outside_element_by_id(), Is.True, "Problems to touch outside of the Menu");//close menu option 
            }
            Assert.That(stepMenuOptions.click_more_options(), Is.True, "More options button is not found");
            Assert.That(stepMenuOptions.click_settings_option(), Is.True, "Settings button is not found");
            Assert.That(stepMenuOptions.click_units_option(), Is.True, "Units option is not found");
            Assert.That(stepMenuOptions.click_imperial_option(), Is.True, "Metric option is not found");
            stepMenuOptions.click_back_android_button();
            stepMenuOptions.click_back_android_button();
            Assert.AreEqual(" MPH", stepMenuOptions.get_units_text());

        }
        [Test]
        public void TC018_chagne_to_hybrid_system()
        {
            if (stepMenuOptions.verify_menu_displayed() == true)
            {
                Assert.That(stepMenuOptions.touch_outside_element_by_id(), Is.True, "Problems to touch outside of the Menu");//close menu option 
            }
            Assert.That(stepMenuOptions.click_more_options(), Is.True, "More options button is not found");
            Assert.That(stepMenuOptions.click_settings_option(), Is.True, "Settings button is not found");
            Assert.That(stepMenuOptions.click_units_option(), Is.True, "Units option is not found");
            Assert.That(stepMenuOptions.click_hybrid_option(), Is.True, "Metric option is not found");
            stepMenuOptions.click_back_android_button();
            stepMenuOptions.click_back_android_button();
            Assert.AreEqual(" KM/H", stepMenuOptions.get_units_text());

        }
        [Test]
        [TestCase("Santa")]
        public void TC010_Add_Location_Suggestions_Location_Displayed_Matches_Text_Entered(string location)
        {

            if (stepMenuOptions.verify_menu_displayed() == false)
            {
                Assert.That(stepMenuOptions.click_menu_button_by_class(), Is.True, "Menu Button is not found");
            }

            
            Assert.That(stepMenuOptions.click_add_location_button_by_id(), Is.True, "Add Location button is not found");
            Assert.That(stepAddLocation.fill_up_serch_text_field(location), Is.True, "The text field cannot be found");

            Assert.That(stepAddLocation.verify_element_displayed_add_location_screen_by_id("searchlist"),Is.True,"Suggestion search list is not displayed");
            Assert.That(stepAddLocation.get_amount_location_suggestions(), Is.GreaterThan(0), "There is no suggestions displayed in Suggestion list");

            for(int i=0;i< stepAddLocation.get_amount_location_suggestions(); i++)
            {
                Assert.That(stepAddLocation.get_string_location_selected_by_index(i).Substring(0,location.Length).ToLower(), Is.EqualTo(location.ToLower()), "Suggestion does not match with the string entered in the text field");

            }

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
