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

using System.Net;
using NUnit.Framework.Interfaces;

namespace AccuWeather
{

    [TestFixture]
    public class TestMenuButtonAndMenuOptions
    {
        AppiumDriver<IWebElement> driver;
        ReadCsvs readCsv;
        StepsToSetUpDevice stepSetUpDevice;
        StepsToTermsAndConditions stepTermConditions;
        StepsToMenuOptions stepMenuOptions;
        StepsToAddLocation stepAddLocation;
        StepsToEditLocation stepEditLocation;
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
            stepEditLocation = new StepsToEditLocation(driver);
        }
        

        [Test]
        [TestCase("list", "")]
        [TestCase("addIcon", "")]
        [TestCase("addLabel", "Add Location")]
        [TestCase("editIcon", "")]
        [TestCase("editLabel", "Edit Locations")]
        public void TC005_Menu_Button_Open_Menu_With_Correct_Content(string element, string expectedText)
        {
            if (stepMenuOptions.verify_menu_displayed() == true)
            {
                stepMenuOptions.click_back_android_button();
            }
           // driver.FindElementById("com.accuweather.android:id/tool_bar").FindElement(By.ClassName("android.widget.ImageButton")).Click();
            Assert.That(stepMenuOptions.click_menu_button_by_class(),Is.True,"Menu button is not found");
            Assert.That(stepMenuOptions.verify_element_displayed_menu_by_id(element), Is.True, element + " is not displayed in the menu");
            Assert.That(stepMenuOptions.get_element_text_menu_by_id(element), Is.EqualTo(expectedText), element + " is displayed with the incorrect text");



        }
        [Test]
        public void TC006_Menu_Closed_When_User_Touch_Outside_menu()
        {
            if (stepMenuOptions.verify_menu_displayed() == false)
            {
                Assert.That(stepMenuOptions.click_menu_button_by_class(),Is.True,"Menu Button is not found");
            }
            Assert.That(stepMenuOptions.touch_outside_element_by_id(),Is.True,"Problems to touch outside of the Menu");
            Assert.That(stepMenuOptions.verify_menu_displayed(), Is.False, "Menu is not closed after touching outside of menu");


        }
        [Test]
        [TestCase("backButton", "")]
        [TestCase("searchbutton", "")]
        [TestCase("searchtextfield", "Search for city or location name")]
        public void TC007_Menu_Add_Location_Screen_Open_with_CorrectContent_User_touch_Add_Location_Menu_Option(string element,string expectedText)
        {
            if (stepMenuOptions.verify_menu_displayed() == false)
            {
                Assert.That(stepMenuOptions.click_menu_button_by_class(), Is.True, "Menu Button is not found");
            }
            Assert.That(stepMenuOptions.click_add_location_button_by_id(), Is.True, "Add Location button cannot be found");
            Assert.That(stepAddLocation.verify_element_displayed_add_location_screen_by_id(element), Is.True, element + " is not displayed in the add screen");
            Assert.That(stepAddLocation.get_element_add_ocation_screen_text_element_by_id(element), Is.EqualTo(expectedText), element + " is displayed with the incorrect text");
        }

        [Test]
        [TestCase("titleLabel", "Current", "id")]
        [TestCase("subtitleLabel", "Searching for your current location", "id")]
        [TestCase("toogleButton", "ON", "id")]
        [TestCase("editList", "", "id")]
        [TestCase("backbuttontoolbar", "", "class")]
        [TestCase("titletoolbar", "Edit Locations", "class")]

        public void TC008_Menu_Edit_Location_Screen_Open_with_CorrectContent_User_touch_Edit_Location_Menu_Option(string element, string expectedText, string by)
        {
            if (stepMenuOptions.verify_menu_displayed() == false)
            {
                Assert.That(stepMenuOptions.click_menu_button_by_class(), Is.True, "Menu Button is not found");
            }

            Assert.That(stepMenuOptions.click_edit_location_button_by_id(), Is.True, "Edit Location button cannot be found");

            if (by.ToLower().Equals("id"))
            {
                Assert.That(stepEditLocation.verify_element_displayed_edit_location_screen_by_id(element), Is.True, element + " is not displayed in the edit screen");
                Assert.That(stepEditLocation.get_element_edit_location_screen_text_element_by_id(element), Is.EqualTo(expectedText), element + " is displayed with the incorrect text");
            }

            else if (by.ToLower().Equals("xpath"))
            {
                Assert.That(stepEditLocation.verify_element_displayed_edit_location_screen_by_xpath(element), Is.True, element + " is not displayed in the edit screen");
                Assert.That(stepEditLocation.get_element_edit_location_screen_text_element_by_xpath(element), Is.EqualTo(expectedText), element + " is displayed with the incorrect text");
            }
            else if (by.ToLower().Equals("class"))
            {
                Assert.That(stepEditLocation.verify_element_displayed_tool_bar_edit_location_screen_by_class(element), Is.True, element + " is not displayed in the edit screen");
                Assert.That(stepEditLocation.get_text_element_displayed_tool_bar_edit_location_screen_by_class(element), Is.EqualTo(expectedText), element + " is displayed with the incorrect text");
            }
            else
            {
                Assert.False(false,"Type of search element provided is incorrect. Test was not completed");
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

    }
}
