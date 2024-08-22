using OpenQA.Selenium.Appium.Windows;
using SeleniumExtras.PageObjects;
using zelisCompassFrameWorkLib.zelisCompassBase;

namespace zelisCompassFrameWorkLib.zelisCompassBase
{
    /*this class is going to be base for our many different classes that we create like ChromeDriver Utilities,
     WinDriver Utilities etc. this class will hold some of our moving parts (the variable) that we will define
    efewsdf
     */
    public  class CompassBase
    {
        public WindowsDriver<WindowsElement> CompassContext;
        public CompassBasePage CurrentPage { get; set; }
        public WindowsDriver<WindowsElement> CompassCMSContexts { get; set; }

        protected zPage GetInstance<zPage>() where zPage : CompassBasePage, new()
        {
            zPage pageInstance = new zPage()
            {
                CompassCMSContexts = WinDriverContext.CompassCMS
            };
            PageFactory.InitElements(WinDriverContext.CompassCMS, pageInstance);
            return pageInstance;
        }

        public zPage As<zPage>() where zPage : CompassBasePage
        {
            return (zPage)this;
        }
    }

}
