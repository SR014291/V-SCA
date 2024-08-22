using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zelisCompassFrameWorkLib.zelisHelpers;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace zelisCompassPages
{
    [TestFixture]
    public class Covid19API
    {



        ExtentReports rlog = ReportHelper.getReportInstance();
        ExtentTest report;

        [Test]
        [TestCategory("Black-Box Testing")]
        [Description("Patch Testing - HBR - BRA Distribution and Tracking Screen ")]

        public void API()
        {

            var client = new RestClient("https://schema.getpostman.com/json/collection/v2.1.0/collection.json");
            var request = new RestRequest("posts/{postid}", Method.GET);
            request.AddUrlSegment("{postid}", 1);
            var content = client.Execute(request).Content;
            Console.WriteLine($"{content}");
        }
    }
        
}
