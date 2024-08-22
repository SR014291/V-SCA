using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using zelisCompassFrameWorkLibrary.zelisCompassBase;
using zelisCompassFrameWorkLibrary.zelisCompassHelpers;
using zelisCompassPages;
using zelisCompassPages.zelisCompassPages.ClaimsTab;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace ClaimLookUp
{
    [TestFixture]
   public  class ClaimLookUp : CompassTestInitialize
    {
        ExtentReports rlog = ReportHelper.getReportInstance();
        ExtentTest report;
        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23642: Search by Claim Status from Claim Lookup")]

        public void StatusClaimLook()
        {

            report = rlog.CreateTest("StatusClaimLook");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage = CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage.As<ClaimLookupPage>().SearchType();
        }




        [TearDown]
        public void closePriZem()
        {

            CompassContext.Quit();
           
            //CompassContext.Close();
            rlog.Flush();

        }
    }
}
