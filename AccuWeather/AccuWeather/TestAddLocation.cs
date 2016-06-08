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

namespace AccuWeather
{
    [TestFixture]
    class TestAddLocation
    {
        AppiumDriver<IWebElement> driver;
        ReadCsvs readCsv;
        StepsToSetUpDevice stepSetUpDevice;
        StepsToTermsAndConditions stepTermConditions;
        StepsToMenuOptions stepMenuOptions;
        StepsToAddLocation stepAddLocation;
        static string appActivityMainScreen = "com.accuweather.app.MainActivity";

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
        }

        [Test]
        public void TC009_Add_Location_Back_button_Display_Previous_Screen()
        {
            
            if (stepMenuOptions.verify_menu_displayed() == false)
            {
                Assert.That(stepMenuOptions.click_menu_button_by_class(), Is.True, "Menu Button is not found");
            }
            
            
            Assert.That(stepMenuOptions.click_add_location_button_by_id(), Is.True, "Add Location button is not found");
            Assert.That(stepAddLocation.click_back_button_by_id, Is.True, "Back button is not found");
            Assert.That(stepAddLocation.verify_add_location_screen_displayed, Is.False, "Add location screen is still displayed");

            Assert.That(stepMenuOptions.verify_menu_displayed(), Is.True, "Previous screen is not displayed after touching Back button from Add Location screen");

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

        [Test]
        [TestCase("Santa")]
        public void TC011_Add_Location_Search_Close_Button_Clean_Search_Text_Field_And_Suggestion_List(string location)
        {

            if (stepMenuOptions.verify_menu_displayed() == false)
            {
                Assert.That(stepMenuOptions.click_menu_button_by_class(), Is.True, "Menu Button is not found");
            }


            Assert.That(stepMenuOptions.click_add_location_button_by_id(), Is.True, "Add Location button is not found");
            string text_before_search = stepAddLocation.get_element_add_ocation_screen_text_element_by_id("searchtextfield");

            Assert.That(stepAddLocation.fill_up_serch_text_field(location), Is.True, "The text field cannot be found");
            Assert.That(stepAddLocation.click_close_search_button_by_id(), Is.True, "Search close button is not displayed");
            Assert.That(stepAddLocation.get_element_add_ocation_screen_text_element_by_id("searchtextfield"),Is.EqualTo(text_before_search),"Text field is not correctly cleaned");
            

        }

        [Test]
        [TestCase("Santa")]
        public void TC012_Add_Location_Suggestion_Disappears_After_Touching_Close_Search_Button(string location)
        {

            if (stepMenuOptions.verify_menu_displayed() == false)
            {
                Assert.That(stepMenuOptions.click_menu_button_by_class(), Is.True, "Menu Button is not found");
            }


            Assert.That(stepMenuOptions.click_add_location_button_by_id(), Is.True, "Add Location button is not found");
           
            Assert.That(stepAddLocation.fill_up_serch_text_field(location), Is.True, "The text field cannot be found");
            Assert.That(stepAddLocation.verify_element_displayed_add_location_screen_by_id("searchlist"), Is.True, "Suggestion List is not displayed");
            Assert.That(stepAddLocation.click_close_search_button_by_id(), Is.True, "Search close button is not displayed");

            Assert.That(stepAddLocation.verify_element_displayed_add_location_screen_by_id("searchlist"), Is.False, "Suggestion List is still displayed after touching close button");

        }

        [Test]
        [TestCase("Santa")]
        public void TC013_Add_Location_Close_Search_Button_Dissapears_After_Touching_It(string location)
        {

            if (stepMenuOptions.verify_menu_displayed() == false)
            {
                Assert.That(stepMenuOptions.click_menu_button_by_class(), Is.True, "Menu Button is not found");
            }


            Assert.That(stepMenuOptions.click_add_location_button_by_id(), Is.True, "Add Location button is not found");

            Assert.That(stepAddLocation.fill_up_serch_text_field(location), Is.True, "The text field cannot be found");
            Assert.That(stepAddLocation.click_close_search_button_by_id(), Is.True, "Search close button is not displayed");

            Assert.That(stepAddLocation.verify_element_displayed_add_location_screen_by_id("closeButton"), Is.False, "Close button does not dissapears after touching it");

        }

        [TestCase("Santa Cruz de la sierra")]
        public void TC014_Add_Location_Add_New_Location(string location)
        {

            if (stepMenuOptions.verify_menu_displayed() == false)
            {
                Assert.That(stepMenuOptions.click_menu_button_by_class(), Is.True, "Menu Button is not found");
            }

            int cant_locations_before = stepMenuOptions.get_amount_of_elements_location_list_by_id();

            Assert.That(stepMenuOptions.click_add_location_button_by_id(), Is.True, "Add Location button is not found");

            Assert.That(stepAddLocation.fill_up_serch_text_field(location), Is.True, "The text field cannot be found");

            
            Assert.That(stepAddLocation.click_search_result_by_index(0), Is.True, "Results are not found");

            Assert.That(stepMenuOptions.get_amount_of_elements_location_list_by_id(), Is.EqualTo(cant_locations_before+1), "The location is not created");
            
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

    }
}
