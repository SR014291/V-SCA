using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using zelisCompassFrameWorkLib.zelisCompassBase;
using zelisCompassFrameWorkLib.zelisHelpers;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace zelisCompassPages
{
    [TestFixture]
    public class TestDebug : CompassTestInitialize
    {

        ExtentReports rlog = ReportHelper.getReportInstance();
        ExtentTest report;

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("Patch Testing - HBR - BRA Distribution and Tracking Screen ")]

        public void GoOutCLaimInfo()
        {

            report = rlog.CreateTest($"GoOutCLaimInfo");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
           // CurrentPage.As<MainScreen>().FindTxtBox(report);
        }



        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("Patch Testing - HBR - BRA Distribution and Tracking Screen ")]
        public void PayorCLaimInfo()
        {
       
            report = rlog.CreateTest("PayorCLaimInfo");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnPayorLookUp();
        }


        [TearDown]
        public void closePriZem()
        {
       
          // CompassContext.Quit();
            //CompassContext.Close();
          //  rlog.Flush();
      
        }

    }
}
