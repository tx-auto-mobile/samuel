using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsDefinitionTests
{
    public class StepsToEditLocation
    {
       
        AppiumDriver<IWebElement> driver;
        static string label_title_id = "com.accuweather.android:id/title";
        static string label_subtitle_id = "com.accuweather.android:id/subtitle";
        static string button_toogle_id = "com.accuweather.android:id/toggle";
        static string edit_list_id = "com.accuweather.android:id/locations_edit_list";
        static string location_class = "android.widget.RelativeLayout";

        static string button_back_tool_bar_xpath = "//android.view.View[@resource-id='com.accuweather.android:id/locations_edit_top_toolbar']//android.widget.ImageButton[@index=0]";
        static string label_title_tool_bar_xpath = "//android.view.View[@resource-id='com.accuweather.android:id/locations_edit_top_toolbar']//android.widget.TextView[@index=1]";

        static string tool_bar_edit_id = "com.accuweather.android:id/locations_edit_top_toolbar";
        static string button_back_tool_bar_class = "android.widget.ImageButton";
        static string label_title_tool_bar_class = "android.widget.TextView";

        public StepsToEditLocation(AppiumDriver<IWebElement> driver)
        {
            this.driver = driver;
        }

        public bool verify_element_displayed_edit_location_screen_by_id(string element)
            
        {
            
            string id_element = element.ToLower().Equals("titlelabel") ? label_title_id
                             : element.ToLower().Equals("subtitlelabel") ? label_subtitle_id
                             : element.ToLower().Equals("tooglebutton") ? button_toogle_id
                             : element.ToLower().Equals("editlist") ? edit_list_id
                             : "";

            try
            {
                return driver.FindElementById(id_element).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public bool verify_element_displayed_edit_location_screen_by_xpath(string element)

        {

            string id_element = element.ToLower().Equals("backbuttontoolbar") ? button_back_tool_bar_xpath
                             : element.ToLower().Equals("titletoolbar") ? label_title_tool_bar_xpath
                             : "";

            try
            {
                return driver.FindElementByXPath(id_element).Displayed;
            }
            catch
            {
                return false;
            }
        }
        public bool verify_element_displayed_tool_bar_edit_location_screen_by_class(string element)

        {

            string id_element = element.ToLower().Equals("backbuttontoolbar") ? button_back_tool_bar_class
                                : element.ToLower().Equals("titletoolbar") ? label_title_tool_bar_class
                                : "";

            try
            {
                return driver.FindElementById(tool_bar_edit_id).FindElement(By.ClassName(id_element)).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public string get_text_element_displayed_tool_bar_edit_location_screen_by_class(string element)

        {

            string id_element = element.ToLower().Equals("backbuttontoolbar") ? button_back_tool_bar_class
                                : element.ToLower().Equals("titletoolbar") ? label_title_tool_bar_class
                                : "";

            try
            {
                return driver.FindElementById(tool_bar_edit_id).FindElement(By.ClassName(id_element)).Text;
            }
            catch
            {
                return "";
            }
        }



        public string get_element_edit_location_screen_text_element_by_id(string element)
        {
            string id_element = element.ToLower().Equals("titlelabel") ? label_title_id
                             : element.ToLower().Equals("subtitlelabel") ? label_subtitle_id
                             : element.ToLower().Equals("tooglebutton") ? button_toogle_id
                             : element.ToLower().Equals("editlist") ? edit_list_id
                             : "";

            try
            {
                return driver.FindElementById(id_element).GetAttribute("text").ToString();
            }
            catch
            {
                return "";
            }
        }

        public string get_element_edit_location_screen_text_element_by_xpath(string element)
        {
            string id_element = element.ToLower().Equals("backbuttontoolbar") ? button_back_tool_bar_xpath
                            : element.ToLower().Equals("titletoolbar") ? label_title_tool_bar_xpath
                            : "";

            try
            {
                return driver.FindElementByXPath(id_element).GetAttribute("text").ToString();
            }
            catch
            {
                return "";
            }
        }

        public int get_amount_of_elements_location_list_by_class()
        {
            try
            {
                IList<IWebElement> list = driver.FindElementById(edit_list_id).FindElements(By.ClassName(location_class));

                return list.Count;
            }
            catch
            {
                return 0;
            }
        }

        public bool swipe_location_to_delete_by_class(int element)
        {
            try
            {
                
                IList<IWebElement> list = driver.FindElementById(edit_list_id).FindElements(By.ClassName(location_class));


                driver.Swipe(driver.Manage().Window.Size.Width-4, list[element].Location.Y, 1, list[element].Location.Y, 5000);
                ITouchAction touch = new TouchAction(driver);
                
                return true;


            }
            catch
            {
                return false;
            }


        }

        public void click_back_android_button()
        {
            driver.Navigate().Back();
        }
    }
}

