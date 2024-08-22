using FlaUI.Core.Capturing;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zelisCompassFrameWorkLib.zelisCompassExtensions
{
    public static class GetScreenShot
    {
        public static string webScreenShot(IWebDriver driver)
        {
            string screenShotName = DateTime.Now.ToString("ddMMyyyyHHmmssffff");
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string screenShotPath = @"E:\AutomationTestResult\Compass\Report\" + DateTime.Now.ToString("dd_MM_yyyy") + "\\screenshot";
            if (!Directory.Exists(screenShotPath))
            {
                Directory.CreateDirectory(screenShotPath);
            }
            screenShotPath = screenShotPath + "/" + screenShotName + ".png";
            string localpath = new Uri(screenShotPath).LocalPath;
            screenshot.SaveAsFile(screenShotPath, ScreenshotImageFormat.Png);
            return localpath;
        }

        public static string windowsScreenShot()
        {
            string screenShotName = DateTime.Now.ToString("ddMMyyyyHHmmssffff");
            var appScreenshot = Capture.Screen();
            string screenShotPath = @"E:\AutomationTestResult\Compass\Report\" + DateTime.Now.ToString("dd_MM_yyyy") + "\\screenshot";
            if (!Directory.Exists(screenShotPath))
            {
                Directory.CreateDirectory(screenShotPath);
            }
            screenShotPath = screenShotPath + "/" + screenShotName + ".png";
            string localpath = new Uri(screenShotPath).LocalPath;
            appScreenshot.ToFile(screenShotPath);
            return localpath;
        }
    }
}
