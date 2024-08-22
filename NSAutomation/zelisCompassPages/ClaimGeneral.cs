using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zelisCompassFrameWorkLib.zelisCompassBase;
using zelisCompassFrameWorkLib.zelisCompassExtensions;
using zelisCompassFrameWorkLib.zelisHelpers;
using zelisCompassPages.zelisCompassPages.ClaimsTab;
using zelisCompassPages.zelisCompassPages.FlyoutMenu;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace zelisCompassPages
{
    [TestFixture]
    class ClaimGeneral : CompassTestInitialize
    {
        ExtentReports rlog = ReportHelper.getReportInstance();
        ExtentTest report;

        [TearDown]
        public void closeReport()
        {
            rlog.Flush();
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23721 - verify provider notes & Create notes and verify it is saved")]

        public void verifyCreateProviderNotes()
        {
            try
            {
                report = rlog.CreateTest($"verifyCreateProviderNotes");
                CurrentPage = GetInstance<MainScreen>();
                CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
                CurrentPage = GetInstance<ClaimLookupPage>();
                CurrentPage.As<ClaimLookupPage>().enterClaimID("151007567");
                CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
                CurrentPage = CurrentPage.As<ClaimLookupPage>().clickedONProviderNotesTab(report);
                CurrentPage.As<ProviderNotesTab>().CreateProviderNotes("QA Header", "Notes", report);

            }
            catch(Exception e)
            {
                report.Log(Status.Fail, $"{e}");
                report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            }
           
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23718 - Click RBP Repricing tab and check repricing data is populated.")]
        public void VerifyRBPRepricing()
        {
            report = rlog.CreateTest($"VerifyRBPRepricing");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
           // CurrentPage.As<ClaimLookupPage>().enterClaimID("150906861");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
            CurrentPage = CurrentPage.As<ClaimLookupPage>().clickedONRBPRepricingsTab(report);
            CurrentPage.As<RBPRepricingsTab>().VerifyRBPRepricingDisplayed(report);
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("24264 - Go the claims menu search for  claim , then Navigate to the Provider lookup flyout menu The tax id and  PM ID  that claim should show up ")]
        public void VerifyProviderlookupFlyOutMenu()
        {
            report = rlog.CreateTest($"VerifyProviderlookupFlyOutMenu");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
            // CurrentPage.As<ClaimLookupPage>().enterClaimID("150906861");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage = CurrentPage.As<MainScreen>().clickedONProviderFlyoutMenu(report);
            CurrentPage.As<Provider>().VerifProviderDetails(report);
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("24527 - Go the claims menu search for  claim , then Navigate to the TIN lookup flyout menu The tax id lookup should display the taxid number ")]
        public void VerifyTINLookupFlyOutMenu()
        {
            report = rlog.CreateTest($"VerifyTINLookupFlyOutMenu");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
            // CurrentPage.As<ClaimLookupPage>().enterClaimID("150906861");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage = CurrentPage.As<MainScreen>().clickedONTINLookupTabFlyoutMenu(report);
            CurrentPage.As<TINLookup>().VerifTINLookupDetails(report);
        }


        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23742 - Reject a claim")]
        public void VerifyRejectClaim()
        {
            report = rlog.CreateTest($"VerifyRejectClaim");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
            // CurrentPage.As<ClaimLookupPage>().enterClaimID("150906861");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23716 - Reprice Claim")]
        public void VerifyRepriceClaim()
        {
            report = rlog.CreateTest($"VerifyRepriceClaim");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
            CurrentPage.As<ClaimLookupPage>().SelectClaimStatus(1,report);
            //CurrentPage.As<ClaimLookupPage>().enterClaimID("150906861");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
            CurrentPage = CurrentPage.As<ClaimLookupPage>().clickedONLineItemsTab(report);
            CurrentPage.As<LineItemsTab>().VerifyEnterRepriceClaim(report);
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23655 - Revise ----> ( In-Net/No Disc )")]
        public void VerifyReviseInNetNoDisc()
        {
            report = rlog.CreateTest($"VerifyReviseInNetNoDisc");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
            CurrentPage.As<ClaimLookupPage>().enterPatientName("CL");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
            CurrentPage = CurrentPage.As<ClaimLookupPage>().clickedONLineItemsTab(report);
            CurrentPage.As<LineItemsTab>().VerifySetReviseInNetNoDisc(report);
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23715 - Revise ----> (Auto Route)")]
        public void VerifyReviseAutoRoute()
        {
            report = rlog.CreateTest($"VerifyReviseAutoRoute");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
            CurrentPage.As<ClaimLookupPage>().enterPatientName("CL");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
            CurrentPage = CurrentPage.As<ClaimLookupPage>().clickedONLineItemsTab(report);
            CurrentPage.As<LineItemsTab>().VerifySetReviseAutoRoute(report);
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23713 - Revise ----> (Do not Flip)")]
        public void VerifyReviseDoNotFlip()
        {
            report = rlog.CreateTest($"VerifyReviseDoNotFlip");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
            CurrentPage.As<ClaimLookupPage>().enterPatientName("CL");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
            CurrentPage = CurrentPage.As<ClaimLookupPage>().clickedONLineItemsTab(report);
            CurrentPage.As<LineItemsTab>().VerifySetReviseDoNotFlip(report);
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23712 - Revise ----> (Missing data)")]
        public void VerifyReviseMissingData()
        {
            report = rlog.CreateTest($"VerifyReviseMissingData");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
            CurrentPage.As<ClaimLookupPage>().enterPatientName("CL");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
            CurrentPage = CurrentPage.As<ClaimLookupPage>().clickedONLineItemsTab(report);
            CurrentPage.As<LineItemsTab>().VerifySetReviseMissingData(report);
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23711 - Revise ----> (Non-Network)")]
        public void VerifyReviseNonNetwork()
        {
            report = rlog.CreateTest($"VerifyReviseNonNetwork");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
            CurrentPage.As<ClaimLookupPage>().enterPatientName("CL");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
            CurrentPage = CurrentPage.As<ClaimLookupPage>().clickedONLineItemsTab(report);
            CurrentPage.As<LineItemsTab>().VerifySetReviseNonNetwork(report);
        }

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("23714 - Revise ----> (Re-Route)")]
        public void VerifyReviseReRoute()
        {
            report = rlog.CreateTest($"VerifyReviseReRoute");
            CurrentPage = GetInstance<MainScreen>();
            CurrentPage.As<MainScreen>().ClickOnClaimLookUp();
            CurrentPage = GetInstance<ClaimLookupPage>();
            CurrentPage.As<ClaimLookupPage>().enterPatientName("CL");
            CurrentPage.As<ClaimLookupPage>().SelectClaimID(report);
            CurrentPage = CurrentPage.As<ClaimLookupPage>().clickedONLineItemsTab(report);
            CurrentPage.As<LineItemsTab>().VerifySetReviseReRoute(report);
        }
    }
}
