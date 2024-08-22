using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using zelisCompassFrameWorkLib.zelisCompassBase;
using zelisCompassFrameWorkLib.zelisCompassExtensions;
using zelisCompassFrameWorkLib.zelisHelpers;

namespace zelisCompassPages.zelisCompassPages.ClaimsTab
{
    public class ClaimLookupPage : CompassLocators
    {

  
        public void LookUpClaim()
        { }

        public void SearchType()
        {
            throw new NotImplementedException();
        }

        public BenchmarksTab clickedONBenchmarksTab(ExtentTest report)
        {
            tabBenchmarks.Click();
            report.Log(Status.Info, "Benchmarks Tab is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<BenchmarksTab>();
        }

        public ClaimNotesTab clickedONClaimNotesTab(ExtentTest report)
        {
            tabClaimNotes.Click();
            report.Log(Status.Info, "Claim Notes Tab is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<ClaimNotesTab>();
        }

        public GroupTab clickedONGroupTab(ExtentTest report)
        {
            tabGroup.Click();
            report.Log(Status.Info, "Group Tab is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<GroupTab>();
        }

        public HistoryTab clickedONHistoryTab(ExtentTest report)
        {
            tabHistory.Click();
            report.Log(Status.Info, "History Tab is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<HistoryTab>();
        }

        public InvoiceTab clickedONInvoiceTab(ExtentTest report)
        {
            tabInvoice.Click();
            report.Log(Status.Info, "Invoice Tab is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<InvoiceTab>();
        }

        public ProviderInquiryTab clickedONProviderInquiryTab(ExtentTest report)
        {
            tabProviderInquiry.Click();
            report.Log(Status.Info, "Provider Inquiry Tab is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<ProviderInquiryTab>();
        }

        public LineItemsTab clickedONLineItemsTab(ExtentTest report)
        {
            tabLineItems.Click();
            report.Log(Status.Info, "Line Items Tab is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<LineItemsTab>();
        }

        public ProviderNotesTab clickedONProviderNotesTab(ExtentTest report)
        {
            tabProviderNotes.Click();
            report.Log(Status.Info, "Provider Notes Tab is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<ProviderNotesTab>();
        }

        public RBPRepricingsTab clickedONRBPRepricingsTab(ExtentTest report)
        {
            tabRBPRepricings.Click();
            report.Log(Status.Info, "RBP Repricings Tab is clicked");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            return GetInstance<RBPRepricingsTab>();
        }

        public void FindTxtBox(ExtentTest report)
        {
            /* txtClaimID.SendKeys("11");
             txtPayerClaimID1.SendKeys("22");
             txtPayerClaimID2.SendKeys("33");
             txtConfirmation.SendKeys("44");
             txtPatientName.SendKeys("55");
             txtProviderTIN.SendKeys("77");
             txtInvoiceNumber.SendKeys("99");
             txtDOSDateRangeFrom.SendKeys("7/1/2021");
             txtDOSDateRangeTo.SendKeys("7/23/2021");
             txtBilledAmountRangeFrom.SendKeys("111");
             txtBilledAmountRangeTo.SendKeys("113");
             SelectListItem(ddClaimStatus,3, report);
             SelectListItem(ddClaimAge, 3, report);
             SelectSearchPayersNetworkGridItems(txtSearchPayers, dgSearchPayers, "Health", "Advantage Health Plans (Kempton)", report);
             SelectSearchPayersNetworkGridItems(txtSearchNetworks, dgSearchNetworks, "Most", "4Most", report);
             btnClearAll.Click();
             btnSearch.Click();*/
            //tabClaimNotes.Click();
            // tabLineItems.Click();
            // tabProviderNotes.Click(); 
            // tabHistory.Click();
            // tabInvoice.Click();
            // tabRBPRepricings.Click();
            // tabProviderInquiry.Click();
            //tabBenchmarks.Click();
            tabGroup.Click();
        }

        public void enterClaimID(string value)
        {
            txtClaimID.SendKeys(value);
        }

        public void enterPayerClaimID1(string value)
        {
            txtPayerClaimID1.SendKeys(value);
        }

        public void enterPayerClaimID2(string value)
        {
            txtPayerClaimID2.SendKeys(value);
        }

        public void enterConfirmation(string value)
        {
            txtConfirmation.SendKeys(value);
        }

        public void enterPatientName(string value)
        {
            txtPatientName.SendKeys(value);
        }


        public void enterProviderTIN(string value)
        {
            txtProviderTIN.SendKeys(value);
        }

        public void enterInvoiceNumber(string value)
        {
            txtInvoiceNumber.SendKeys(value);
        }

        public void enterDOSDateRangeFrom(string value)
        {
            txtDOSDateRangeFrom.SendKeys(value);
        }

        public void enterDOSDateRangeTo(string value)
        {
            txtDOSDateRangeTo.SendKeys(value);
        }


        public void enterBilledAmountRangeFrom(string value)
        {
            txtBilledAmountRangeFrom.SendKeys(value);
        }

        public void enterBilledAmountRangeTo(string value)
        {
            txtBilledAmountRangeTo.SendKeys(value);
        }

        public void SelectClaimStatus(int index, ExtentTest report)
        {
            SelectListItem(ddClaimStatus, index, report);
        }

        public void SelectClaimID(ExtentTest report)
        {
            btnSearch.Click();
            Thread.Sleep(5000);
            Boolean flag = false;
            int ClaimsDetailCounts = dgClaimsDetails.Count;
            for (int i = 1; i <= ClaimsDetailCounts; i++)
            {
                IList<IWebElement> elements = dgClaimsRowDetails(i).FindElements(By.TagName("Text"));
                foreach (IWebElement element in elements)
                {
                    element.Click();
                    Thread.Sleep(5000);
                    report.Log(Status.Info, $"Select Claim Id {element.Text}");
                    report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
                    flag = true;
                    break;
                }
                if (flag)
                {
                    break;
                }
            }
        }

        public void SelectClaimID(string claimID, ExtentTest report)
        {
            Boolean flag = false;
            int ClaimsDetailCounts = dgClaimsDetails.Count;
            for (int i = 1; i < ClaimsDetailCounts; i++)
            {
                IList<IWebElement> elements = dgClaimsRowDetails(i).FindElements(By.TagName("Text"));
                foreach (IWebElement element in elements)
                {
                    if (element.Text.Equals(claimID))
                    {
                        flag = true;
                        Thread.Sleep(5000);
                        report.Log(Status.Info, $"Select Claim Id {claimID}");
                        report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
                        break;
                    }
                }
            }

            if (!flag)
            {
                report.Log(Status.Fail, $"Unable to find Claim Id {claimID}");
            }

        }

        public void SelectSearchPayersNetworkGridItems(IWebElement txtElement, IList<IWebElement> gridElement, string txtValue, string gridValue, ExtentTest report)
        {
            Boolean flag = false;
            txtElement.SendKeys(txtValue);
            foreach (IWebElement element in gridElement)
            {
                string elementValue = element.Text.Trim();
                if (elementValue.Equals(gridValue))
                {
                    element.Click();
                    report.Log(Status.Info, $"{gridValue} Selected");
                    report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                report.Log(Status.Fail, $"element -  {gridElement} is unable to find {gridValue} value");
            }
        }

       


        public void SelectListItem(IWebElement element, int value, ExtentTest report)
        {
            Boolean flag = false;
            element.Click();
            int i = 0;
            foreach (IWebElement option in WinDriverContext.CompassCMS.FindElementsByTagName("ListItem"))
            {
                if (i.Equals(value))
                {
                    option.Click();
                    flag = true;
                    break;
                }
                i++;
            }
            if (!flag)
            {
                report.Log(Status.Fail, $"element -  {element} is unable to find index {i}");
            }
        }

        
    }
}
