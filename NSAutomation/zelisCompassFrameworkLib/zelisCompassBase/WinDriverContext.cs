using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.Diagnostics;


namespace zelisCompassFrameWorkLib.zelisCompassBase
{

    public static class  WinDriverContext 
    {
        public static WindowsDriver<WindowsElement> CompassCMS;
        public static WindowsDriver<WindowsElement> CompassCMSSecondWindow = null;

        public static WindowsDriver<WindowsElement> initializePriZem()
        {
            var compassLocation = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Local\CompassConsoleQA\Zelis.Compass.Desktop.Console.exe";
            AppiumOptions compassCapabilities = new AppiumOptions();
            compassCapabilities.AddAdditionalCapability("app", compassLocation);
            CompassCMS = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), compassCapabilities);
          //  CompassCMS.Manage().Window.Maximize();
            return CompassCMS;
          System.Threading.Thread.Sleep(3000);
        }

      
    }


}
