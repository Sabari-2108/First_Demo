using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium_Practice
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver;
        WebDriverWait wait;

        [TestMethod]
        public void TestMethod1()
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--start-maximized");
            option.AddExcludedArgument("enable-automation");
            option.AddArgument("--headless");
            option.AddArgument("--window-size=1920,1080");
            driver = new ChromeDriver(option);
            driver.Url = "https://secure.samil.in/SecureLogin/OasysLogin";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            //Clicking the Verify Button
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("ContentPlaceHolder1_btnSignIn")));
            driver.FindElement(By.Id("ContentPlaceHolder1_btnSignIn")).Click(); 

            alertReplacer("Top", "fail");

            Thread.Sleep(1000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 10000);");

            Screenshot();

            Thread.Sleep(500);
            driver.Quit();
        }

        public void Screenshot()
        {
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(@"C:\Users\Vivekananda\OneDrive\Desktop\Screenshots\Sample" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".png", ScreenshotImageFormat.Png);
        }

        public void alertReplacer(string ID_Value, string Pass_Fail)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            alert.Accept();

            if (Pass_Fail.ToLower().Contains("pass"))
            {
                Thread.Sleep(1000);
                string script = "var p = document.createElement('input');var ele = document.getElementById('" + ID_Value + "');p.setAttribute('style', 'width: 600px !important; height: 50px !important; padding: 10px !important;border: 10px outset green; text-align: center;color: #000000; background-color: #aed581; font-weight: bold;');p.setAttribute('value', '" + alertText + "');ele.appendChild(p);";
                ((IJavaScriptExecutor)driver).ExecuteScript(script);
            }
            else if (Pass_Fail.ToLower().Contains("fail"))
            {
                Thread.Sleep(1000);
                string script = "var p = document.createElement('input');var ele = document.getElementById('" + ID_Value + "');p.setAttribute('style', 'width: 600px !important; height: 50px !important; padding: 10px !important;border: 10px outset red; text-align: center;color: #000000; background-color: #FFCCCB; font-weight: bold;');p.setAttribute('value', '" + alertText + "');ele.appendChild(p);";
                ((IJavaScriptExecutor)driver).ExecuteScript(script);
            }
        }
    }
}