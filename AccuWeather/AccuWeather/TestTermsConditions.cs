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
using NUnit.Framework.Interfaces;

namespace AccuWeather
{
    
    [TestFixture]
    public class TestTermsConditions
    {
        AppiumDriver<IWebElement> driver;
        ReadCsvs readCsv;
        StepsToSetUpDevice stepSetUpDevice;
        StepsToTermsAndConditions stepTermConditions;
        static string appActivityTermsConditions = "com.accuweather.app.SplashScreen";


        [SetUp]
        public void setUp()
        {
            String testCaseName = TestContext.CurrentContext.Test.Name;


            stepSetUpDevice = new StepsToSetUpDevice();
            driver = stepSetUpDevice.run_driver_with_install(testCaseName, appActivityTermsConditions);
            stepTermConditions = new StepsToTermsAndConditions(driver);
            
        }
        
        [Test]
        [TestCase("Title", "Terms of Use")]
        [TestCase("Message", "Please agree so we can get you your weather!\r\n\r\nBy using this application, you agree to AccuWeather's Terms & Conditions (in English) which can be found at \r\nhttp://m.accuweather.com/en/legal \r\n\r\nand AccuWeather's Privacy Statement (in English) which can be found at \r\nhttp://m.accuweather.com/en/privacy")]
        [TestCase("I Agree", "I Agree")]
        [TestCase("No Thanks","No Thanks")]
        
        public void TC001_Terms_Conditions_Displayed_With_The_Correct_Content(string element,string expectedText)
        {

            Assert.That(stepTermConditions.verify_element_displayed_term_conditions_by_id(element),Is.True, element + " is not displayed in Terms and condition");
            Assert.That(stepTermConditions.get_element_text_term_conditions_by_id(element), Is.EqualTo(expectedText), element + " is displayed with the incorrect text");

        }

        [Test]
        public void TC002_Terms_Conditions_No_Accepted()
        {
            Assert.That(stepTermConditions.click_reject_button_terms_conditions_by_id(), Is.True, " The test case is blocked because \"No Thanks\" button is not displayed in terms and condition");
            Assert.That(stepTermConditions.verify_app_is_closed(), Is.False, "The application was not closed after touching \"No Thanks\" button");
        }

        [Test]
        [TestCase("Title", "Welcome to the new AccuWeather for Android!")]
        [TestCase("Message", "We’ve streamlined our app to provide a more modern experience in Material Design with improved speed, ease-of-use, and reliability. \r\n\r\nAccuWeather offers unique features such as MinuteCast™ with the Superior Accuracy™ you’ve come to trust, providing a great experience across all Android smartphones and tablets, and Android Wear™.")]
        [TestCase("Let's Go!", "Let's Go!")]

        public void TC003_Wellcome_Displayed_With_The_Correct_Content(string element, string expectedText)
        {

            Assert.That(stepTermConditions.click_agree_button_terms_conditions_by_id(), Is.True, "The test case is blocked because \"I Agree\" button is not displayed in terms and condition");
            stepTermConditions.click_allow_permission_location_message();
            Assert.That(stepTermConditions.verify_element_displayed_wellcome_by_id(element), Is.True, element + " is not displayed in Wellcome");
            Assert.That(stepTermConditions.get_element_text_Wellcome_by_id(element), Is.EqualTo(expectedText), element + " is displayed with the incorrect text");
            
            
        }

        [Test]
        public void TC004_Application_Opened_Menu_Displayed_After_Wellcome_Screen()
        {

            Assert.That(stepTermConditions.click_agree_button_terms_conditions_by_id(), Is.True, "The test case is blocked because \"I Agree\" button is not displayed in terms and condition");
            Assert.That(stepTermConditions.click_lets_go_button_wellcome_by_id(), Is.True, "The test case is blocked because \"Let's Go!\" button is not displayed in wellcome");

            Assert.That(stepTermConditions.verify_lets_go_button_displays_application_opened_with_menu_displayed(), Is.True, "Application is not opened with the menu displayed after touching on \"Let's Go\" button from \"Wellcome\" Alert");

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
