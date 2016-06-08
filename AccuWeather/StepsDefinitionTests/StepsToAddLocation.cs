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
    public class StepsToAddLocation
    {
        AppiumDriver<IWebElement> driver;

        static string add_location_screen_id = "com.accuweather.android:id/overlay";

        static string button_back_id = "com.accuweather.android:id/back_arrow";
        static string button_search_id = "android:id/search_mag_icon";
        static string text_field_search_id = "android:id/search_src_text";
        static string search_list_id= "com.accuweather.android:id/locations_search_results";
        static string button_close_id = "android:id/search_close_btn";
        //need to complete the index
        static string element_searched_path = "//android.widget.ListView[@resource-id='com.accuweather.android:id/locations_search_results']//android.widget.RelativeLayout";//[@index=0]";
        static string element_searched_class = "android.widget.RelativeLayout";

        static string element_searched_location_name_id = "com.accuweather.android:id/location_search_name";
        static string element_searched_location_country_id = "com.accuweather.android:id/location_search_country";

      
        public StepsToAddLocation(AppiumDriver<IWebElement> driver)
        {
            this.driver = driver;
        }

        public bool verify_add_location_screen_displayed()
        {
            try
            {
                return driver.FindElementById(add_location_screen_id).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public bool verify_element_displayed_add_location_screen_by_id(string element)
        {
            string id_element = element.ToLower().Equals("backbutton") ? button_back_id
                             : element.ToLower().Equals("searchbutton") ? button_search_id
                             : element.ToLower().Equals("searchtextfield") ? text_field_search_id
                             : element.ToLower().Equals("searchlist") ? search_list_id
                             : element.ToLower().Equals("closebutton") ? button_close_id
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
        public string get_element_add_ocation_screen_text_element_by_id(string element)
        {
            string id_element = element.ToLower().Equals("backbutton") ? button_back_id
                             : element.ToLower().Equals("searchbutton") ? button_search_id
                             : element.ToLower().Equals("searchtextfield") ? text_field_search_id
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

        public bool fill_up_serch_text_field(string text)
        {
            try
            {
                if (verify_element_displayed_add_location_screen_by_id("searchtextfield"))
                {
                    driver.FindElement(By.Id(text_field_search_id)).SendKeys(text);
                    
                    return true;
                }
                
                else
                    return false;


            }
            catch
            {
                return false;
            }


        }

        public bool click_search_result_by_xpath(string index)
        {
            try
            {

                if (verify_element_displayed_add_location_screen_by_id("searchList"))
                {
                    driver.FindElementByXPath(element_searched_path + "[@index=" + index + "]").Click();
                    return true;
                }

                else
                    return false;


            }
            catch
            {
                return false;
            }
        }

        public string get_string_location_selected_xpath(string index)
        {
            try
            {

                if (verify_element_displayed_add_location_screen_by_id("searchList"))
                {
                    return driver.FindElementByXPath(element_searched_path + "[@index=" + index + "]").FindElement(By.Id("com.accuweather.android:id/location_search_name")).GetAttribute("text").ToString()+", "+ driver.FindElementByXPath(element_searched_path + "[@index=" + "0" + "]").FindElement(By.Id("com.accuweather.android:id/location_search_country")).GetAttribute("text").ToString();
                }

                else
                    return "";


            }
            catch
            {
                return "";
            }
        }

        public bool click_back_button_by_id()
        {
            try
            {
                if (verify_element_displayed_add_location_screen_by_id("backbutton"))
                {
                    driver.FindElementById(button_back_id).Click();

                    return true;
                }

                else
                    return false;


            }
            catch
            {
                return false;
            }


        }

        public bool click_close_search_button_by_id()
        {
            try
            {
                if (verify_element_displayed_add_location_screen_by_id("closebutton"))
                {
                    driver.FindElementById(button_close_id).Click();

                    return true;
                }

                else
                    return false;


            }
            catch
            {
                return false;
            }


        }
        public int get_amount_location_suggestions()
        {
            try
            {
                if (verify_element_displayed_add_location_screen_by_id("searchlist"))
                {
                    return driver.FindElementById(search_list_id).FindElements(By.ClassName(element_searched_class)).Count();
                }
                else
                    return 0;
            }
            catch
            {
                return 0;
            }

        }

        public bool add_location(string location)
        {
            bool resp = false;

            resp = fill_up_serch_text_field(location);
            resp = click_search_result_by_index(0);

            return resp;
        }

        public string get_string_location_selected_by_index(int index)
        {
            try
            {

                if (verify_element_displayed_add_location_screen_by_id("searchList"))
                {
                    return driver.FindElementById(search_list_id).FindElements(By.ClassName(element_searched_class))[index].FindElement(By.Id(element_searched_location_name_id)).Text + driver.FindElementById(search_list_id).FindElements(By.ClassName(element_searched_class))[index].FindElement(By.Id(element_searched_location_country_id)).Text;
                   // return driver.FindElementByXPath(element_searched_path + "[@index=" + index + "]").FindElement(By.Id("com.accuweather.android:id/location_search_name")).GetAttribute("text").ToString() + ", " + driver.FindElementByXPath(element_searched_path + "[@index=" + "0" + "]").FindElement(By.Id("com.accuweather.android:id/location_search_country")).GetAttribute("text").ToString();
                }

                else
                    return "";


            }
            catch
            {
                return "";
            }
        }

        public bool click_search_result_by_index(int index)
        {
            try
            {

                if (verify_element_displayed_add_location_screen_by_id("searchList"))
                {
                    driver.FindElementById(search_list_id).FindElements(By.ClassName(element_searched_class))[index].Click();
                    return true;
                }

                else
                    return false;


            }
            catch
            {
                return false;
            }
        }

        


    }
    
}


