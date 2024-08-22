using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using zelisCompassFrameWorkLib.zelisCompassExtensions;

namespace zelisCompassPages.zelisCompassPages.ClaimsTab
{
    public class ProviderNotesTab : CompassLocators
    {
        public void CreateProviderNotes(string header, string notes, ExtentTest report)
        {
            Thread.Sleep(20000);
            txtProviderNotesHeader.SendKeys(header);
            txtProviderNotes.SendKeys(notes);
            btnProviderNoteCreateNew.Click();
            int oldCount = int.Parse(lblProviderNotesCount.Text);
            txtProviderNotesHeader.SendKeys(header);
            txtProviderNotes.SendKeys(notes);
            report.Log(Status.Info, $"Enter the New  Provider Notes");
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());
            btnProviderNoteSave.Click();
            int newCount = int.Parse(lblProviderNotesCount.Text);
            if(newCount > oldCount)
            {
                report.Log(Status.Pass, $"New  Provider Notes creared");
            }
            else{
                report.Log(Status.Fail, $"Unable to create Provider Notes");
            }
            report.AddScreenCaptureFromPath(GetScreenShot.windowsScreenShot());

        }
    }
}
