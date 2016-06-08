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
using System.Linq;
using NUnit.Framework.Interfaces;

namespace AccuWeather
{
    [TestFixture]
    class TestEditLocation
    {
        static string allowChars = "a,e,i,o,u,c,s,d,u,b"; 
        AppiumDriver<IWebElement> driver;
        ReadCsvs readCsv;
        StepsToSetUpDevice stepSetUpDevice;
        StepsToTermsAndConditions stepTermConditions;
        StepsToMenuOptions stepMenuOptions;
        StepsToAddLocation stepAddLocation;
        StepsToEditLocation stepEditLocation;
        static string appActivityMainScreen = "com.accuweather.app.MainActivity";

        int createLocationNumber=3;


        [SetUp]
        public void setUp()
        {
            String testCaseName = TestContext.CurrentContext.Test.Name;


            stepSetUpDevice = new StepsToSetUpDevice();
            driver = stepSetUpDevice.run_driver_with_install(testCaseName, appActivityMainScreen);
            stepTermConditions = new StepsToTermsAndConditions(driver);
            //stepTermConditions.accept_termns_and_conditions();
            stepMenuOptions = new StepsToMenuOptions(driver);
            stepAddLocation = new StepsToAddLocation(driver);
            stepEditLocation = new StepsToEditLocation(driver);

            bool setUpEnviroment = add_several_locations(createLocationNumber);
            Assert.That(setUpEnviroment, Is.True, "The Test case cannot be executed because the enviroment cannot be created");
        }

        [Test]
        [TestCase("Edit")]
        [TestCase("Menu")]
        public void TC015_Edit_Location_Delete_Location_When_It_Is_Swipped_Left(string screen)
        {

            if (stepMenuOptions.verify_menu_displayed() == false)
            {
                Assert.That(stepMenuOptions.click_menu_button_by_class(), Is.True, "Menu Button is not found");
            }

            int num_locations_added_in_menu = stepMenuOptions.get_amount_of_elements_location_list_by_id();
            Assert.That(stepMenuOptions.click_edit_location_button_by_id(), Is.True, "Edit Location button is not found");
            Assert.That(stepEditLocation.verify_element_displayed_edit_location_screen_by_id("editlist"), Is.True, "Location list is not displyaed");

            int num_locations_added_in_edit = stepEditLocation.get_amount_of_elements_location_list_by_class();
            Assert.That(num_locations_added_in_edit, Is.GreaterThan(1), "There is no enough locations to test a delete operation");

            Assert.That(stepEditLocation.swipe_location_to_delete_by_class(num_locations_added_in_edit - 1), Is.True, "The element cannot be swipped");

            if (screen.ToLower().Equals("Menu".ToLower()))
            {
                stepEditLocation.click_back_android_button();
                Assert.That(num_locations_added_in_menu-1, Is.EqualTo(stepMenuOptions.get_amount_of_elements_location_list_by_id()), "The element is not deleted in " +screen);
            }
            else if (screen.ToLower().Equals("Edit".ToLower()))
            {
                Assert.That(num_locations_added_in_edit-1, Is.EqualTo(stepEditLocation.get_amount_of_elements_location_list_by_class()), "The element is not deleted in " + screen);
            }
            else
            {
                Assert.False(false,"The screen to verify that the element is deleted is not found");
            }
            
        }

        [TearDown]
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

        //Methods to help to set up eenvironemnt

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
