using System;
using OpenQA.Selenium;
using System.Web;
using System.Text;
using System.Net;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using ReadCsvFiles;
using System.Collections.Generic;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;

namespace StepsDefinitionTests
{
    public class StepsToSetUpDevice
    {
        private string real;
        private string plataform;
        private string deviceName;
        private string plataformVersion;
        private string udid;
        private string app;
        private string sauceLabs;
        private string appiumVersion;

        private string username;
        private string key;

        private string server;
        private string port;



        public StepsToSetUpDevice()
        {
            ReadCsvs readCsv = new ReadCsvs();
            List<string> documentCapabilities = readCsv.readCapabilities();
            List<string> appiumConfiguration = readCsv.readAppiumConfiguration();
            List<string> sauceLabsConfiguration = readCsv.readSauceLabsConfiguration();

            this.real = documentCapabilities[0].ToLower();
            this.plataform = documentCapabilities[1];
            this.deviceName = documentCapabilities[2];
            this.udid = documentCapabilities[3];
            this.plataformVersion = documentCapabilities[4];

            this.app = documentCapabilities[5];
            this.sauceLabs = documentCapabilities[6].ToLower();
            this.appiumVersion = documentCapabilities[7];

            this.username = sauceLabsConfiguration[0];
            this.key = sauceLabsConfiguration[1];

            this.server = appiumConfiguration[0];
            this.port = appiumConfiguration[1];

        }

        public AppiumDriver<IWebElement> run_driver_with_install(string testName, string appActivity)
        {
            AppiumDriver<IWebElement> driver;
            DesiredCapabilities cap = new DesiredCapabilities();
            cap.SetCapability("noReset", "false");
            cap.SetCapability("fullReset", "true");
            cap.SetCapability("name", testName);

            if (real.Equals("yes"))
            {
                cap.SetCapability("platformName", plataform);
                cap.SetCapability("deviceName", deviceName);
                cap.SetCapability("platformVersion", plataformVersion);
                cap.SetCapability("app", app);
            }
            else if (real.Equals("no"))
            {
                if (sauceLabs.Equals("no"))
                {
                    cap.SetCapability("platformName", plataform);
                    cap.SetCapability("deviceName", deviceName);
                    cap.SetCapability("udid", udid);
                    cap.SetCapability("platformVersion", plataformVersion);
                    cap.SetCapability("app", app);
                }
                else
                {
                    cap.SetCapability("deviceName", deviceName);
                    cap.SetCapability("platformVersion", plataformVersion);
                    cap.SetCapability("platformName", plataform);
                    cap.SetCapability("app", app);
                    cap.SetCapability("username", username);
                    cap.SetCapability("accessKey", key);
                    cap.SetCapability("appiumVersion", appiumVersion);
                }
            }

            cap.SetCapability("appPackage", "com.accuweather.android");
            cap.SetCapability("appActivity", appActivity);
            driver = new AndroidDriver<IWebElement>(new Uri("http://" + server + ":" + port + "/wd/hub"), cap, TimeSpan.FromSeconds(600));


            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));

           
            return driver;
        }
    }
}
