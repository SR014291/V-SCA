using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using zelisCompassFrameWorkLib.zelisCompassBase;
using zelisCompassFrameWorkLib.zelisCompassExtensions;
using zelisCompassFrameWorkLib.zelisHelpers;
using zelisCompassPages.zelisCompassPages.ClaimsTab;
using zelisCompassPages.zelisCompassPages.FlyoutMenu;
using zelisCompassPages.zelisCompassPages.PayorTab;

namespace zelisCompassPages
{
    public class MainScreen : CompassLocators
    {

        ExtentReports rlog = ReportHelper.getReportInstance();
        ExtentTest report;

        public ClaimLookupPage ClickOnClaimLookUp()
        {

            miClaim.Click();
            foreach (IWebElement option in miClaim.FindElements(By.TagName("MenuItem")))
            {
                if (option.Text.Equals("Lookup"))
                {
                    option.Click();
                    break;
                }
            }

          
            return GetInstance<ClaimLookupPage>();
         
        }



        public ClaimLookupPage ClickOnValidationQueue()
        {

            miClaim.Click();
            foreach (IWebElement option in miClaim.FindElements(By.TagName("MenuItem")))
            {
                if (option.Text.Equals("Validation Queue"))
                {
                    option.Click();
                    break;
                }
            }

            return GetInstance<ClaimLookupPage>();

        }


        public ClaimLookupPage ClickOnBulkClaimsRouting()
        {

            miClaim.Click();
            foreach (IWebElement option in miClaim.FindElements(By.TagName("MenuItem")))
            {
                if (option.Text.Equals("Bulk Claims Routing"))
                {
                    option.Click();
                    break;
                }
            }

            return GetInstance<ClaimLookupPage>();

        }







        public PayorLookupPage ClickOnPayorLookUp()
        {

            miPayers.Hover();
            miClaim.Click();
            foreach (IWebElement option in miClaim.FindElements(By.TagName("MenuItem")))
            {
                if (option.Text.Equals("Lookup"))
                {
                    option.Click();
                    break;
                }
            }
            return GetInstance<PayorLookupPage>();

        }



        public Provider clickedONProviderFlyoutMenu(ExtentTest report)
        {
            btnProviderTab.Click();
            report.Log(Status.Info, "Provider Flyout Menu is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<Provider>();
        }

        public TINLookup clickedONTINLookupTabFlyoutMenu(ExtentTest report)
        {
            btnTINLookupTab.Click();
            report.Log(Status.Info, "TIN LookupTab Flyout Menu is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<TINLookup>();
        }


    }
}
