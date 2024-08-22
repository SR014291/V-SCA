
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zelisCompassFrameWorkLib.zelisHelpers
{
    public static class ReportHelper
    {
        public static ExtentHtmlReporter htmlReporter;
        public static ExtentReports extent;

        public static ExtentReports getReportInstance()
        {
            if (extent == null)
            {
                string reportPath = @"E:\AutomationTestResult\Compass\Report\" + DateTime.Now.ToString("dd_MM_yyyy");
                if (!Directory.Exists(reportPath))
                {
                    Directory.CreateDirectory(reportPath);
                }
                string reportFullPath = reportPath + "\\report.html";
                htmlReporter = new ExtentHtmlReporter(reportFullPath);
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
                extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
                extent.AddSystemInfo("Machine Name", Environment.MachineName);
                extent.AddSystemInfo("User Name", Environment.UserName);
                extent.AddSystemInfo("Host Name", Environment.UserDomainName);
                string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
                String extentPath = projectPath + "zelisCompassFrameworkLib\\extent-config.xml";
                htmlReporter.LoadConfig(extentPath);
            }
            return extent;
        }

        public static ExtentReports getReportInstanceMedEngine()
        {
            if (extent == null)
            {
                string reportPath = @"E:\AutomationTestResult\MedEngine\Report\" + DateTime.Now.ToString("dd_MM_yyyy");
                if (!Directory.Exists(reportPath))
                {
                    Directory.CreateDirectory(reportPath);
                }
                string reportFullPath = reportPath + "\\report.html";
                htmlReporter = new ExtentHtmlReporter(reportFullPath);
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
                extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
                extent.AddSystemInfo("Machine Name", Environment.MachineName);
                extent.AddSystemInfo("User Name", Environment.UserName);
                extent.AddSystemInfo("Host Name", Environment.UserDomainName);
                string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
                String extentPath = projectPath + "zelisCompassFrameWorkLib\\extent-config.xml";
                htmlReporter.LoadConfig(extentPath);
            }
            return extent;
        }
    }
}
