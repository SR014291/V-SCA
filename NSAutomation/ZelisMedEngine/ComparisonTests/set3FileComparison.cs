using System;
using System.IO;
using zelisCompassFrameWorkLib.zelisHelpers;
using System.Configuration;
using NUnit.Framework;
using AventStack.ExtentReports;
using System.Linq;
using ZelisMedEngine.ComparisonFileObject;


namespace ZelisMedEngine.FileComparison
{
    [TestFixture]
    public class set3FileComparison
    {
        
        ExtentReports rlog = ReportHelper.getReportInstanceMedEngine();
        ExtentTest report;
        

        [SetUp]
        public void GetZipFile()
        {
            //extractZip file
            try
            {
                FileHelper.extractFile("SET 3", "ZipFileName");
                Set3FileDefinition.CreateSet3TempFile();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
                
        [TearDown]
        public void RemoveFiles()
        {
           rlog.Flush();
           FileHelper.deleteAllFile(ConfigurationManager.AppSettings["extractFilePath"]);
        }

        [Test]
        [Description("Compare SET3 file count to stagedb")]
        public void CompareSet3FileCountToStageDB()
        {
            try
            {
                report = rlog.CreateTest("Compare SET3 file count to stagedb");

                //Count line in the extracted files
                var lineCounter = Set3FileDefinition.CountSet3Lines();
                var dbCount = Set3FileDefinition.GetSet3StageDBCount();
                
                // Validate Lines in file match lines in DB
                Assert.AreEqual(lineCounter, dbCount);

            }
            catch (Exception e)
            {
                report.Log(Status.Fail, e);
                Console.WriteLine(e);
            }
        }
        
        [Test]
        [Description("Compare SET3 first record to stagedb")]
        public void CompareSet3FirstInFiletoStageDB()
        {
            try
            {
                report = rlog.CreateTest($"Compare SET3 1st record to stagedb");
                string currentLine = null;
                string[] statusCodes = { "R", "A", "T" };

                using (var reader = Set3FileDefinition.ReadSet3File())
                {
                    while ((currentLine = reader.ReadLine()) != null)
                    {

                        var fileData = Set3FileDefinition.GetSet3FileData(currentLine);

                        if (statusCodes.Contains(fileData[0].statusCode))
                        {
                            var dbData = Set3FileDefinition.GetSet3StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);

                            Assert.AreEqual(fileData[0].description, dbData[0].description);
                            Assert.AreEqual(fileData[0].parFeeNonFacility, dbData[0].parFeeNonFacility);
                            Assert.AreEqual(fileData[0].parFeeFacility, dbData[0].parFeeFacility);
                            Assert.AreEqual(fileData[0].nonParFeeNonFacility, dbData[0].nonParFeeNonFacility);
                            Assert.AreEqual(fileData[0].nonParFeeFacility, dbData[0].nonParFeeFacility);
                            Assert.AreEqual(fileData[0].oppsParFeeNonFacility, dbData[0].oppsParFeeNonFacility);
                            Assert.AreEqual(fileData[0].oppsParFeeFacility, dbData[0].oppsParFeeFacility);
                            Assert.AreEqual(fileData[0].oppsNonParFeeNonFacility, dbData[0].oppsNonParFeeNonFacility);
                            Assert.AreEqual(fileData[0].oppsNonParFeeFacility, dbData[0].oppsNonParFeeFacility);
                            Assert.AreEqual(fileData[0].globalSurgery, dbData[0].globalSurgery);
                            Assert.AreEqual(fileData[0].calcFlag, dbData[0].calcFlag);
                            Assert.AreEqual(fileData[0].parFeeFacility.TrimStart(new char[] { '0' }), dbData[0].PricingAmount1.TrimStart(new char[] { '0' }));
                            Assert.AreEqual(fileData[0].parFeeNonFacility.TrimStart(new char[] { '0' }), dbData[0].PricingAmount2.TrimStart(new char[] { '0' }));

                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                report.Log(Status.Fail, e);
                Console.WriteLine(e);
            }
        }
        
        [Test]
        [Description("Compare SET3 file last record to stagedb")]
        public void CompareSet3LastInFiletoStageDB()
        {
            try
            {

                report = rlog.CreateTest($"Compare SET3 last record to stagedb");
                string currentLine;
                
                var beforeCounter = Set3FileDefinition.CountSet3Lines();
                var afterCounter = Set3FileDefinition.CountSet3TempLines();

                currentLine = Set3FileDefinition.ReadSet3TempFileLast();
                var fileData = Set3FileDefinition.GetSet3FileData(currentLine);
                var dbData = Set3FileDefinition.GetSet3StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);


                Assert.AreEqual(fileData[0].description, dbData[0].description);
                Assert.AreEqual(fileData[0].parFeeNonFacility, dbData[0].parFeeNonFacility);
                Assert.AreEqual(fileData[0].parFeeFacility, dbData[0].parFeeFacility);
                Assert.AreEqual(fileData[0].nonParFeeNonFacility, dbData[0].nonParFeeNonFacility);
                Assert.AreEqual(fileData[0].nonParFeeFacility, dbData[0].nonParFeeFacility);
                Assert.AreEqual(fileData[0].oppsParFeeNonFacility, dbData[0].oppsParFeeNonFacility);
                Assert.AreEqual(fileData[0].oppsParFeeFacility, dbData[0].oppsParFeeFacility);
                Assert.AreEqual(fileData[0].oppsNonParFeeNonFacility, dbData[0].oppsNonParFeeNonFacility);
                Assert.AreEqual(fileData[0].oppsNonParFeeFacility, dbData[0].oppsNonParFeeFacility);
                Assert.AreEqual(fileData[0].globalSurgery, dbData[0].globalSurgery);
                Assert.AreEqual(fileData[0].calcFlag, dbData[0].calcFlag);
                Assert.AreEqual(fileData[0].parFeeFacility.TrimStart(new char[] { '0' }), dbData[0].PricingAmount1.TrimStart(new char[] { '0' }));
                Assert.AreEqual(fileData[0].parFeeNonFacility.TrimStart(new char[] { '0' }), dbData[0].PricingAmount2.TrimStart(new char[] { '0' }));

            }
            catch (Exception e)
            {
                report.Log(Status.Fail, e);
                Console.WriteLine(e);
            }
        }
       
        [Test]
        [Description("Compare Random lines of File match stagedb")]
        public void CompareSet4RandomyFromFiletoStagekDB()
        {
            try
            {
                report = rlog.CreateTest($"Compare Random lines stagedb");

                int lineCount = (int)Set3FileDefinition.CountSet3TempLines();
                int[] randomLineNum = FileHelper.generateRandomArray(lineCount, 10);
                Array.Sort(randomLineNum);
                string[] results = randomLineNum.Select(x => x.ToString()).ToArray();
                int indicator = 0;
                string currentLine = null;

                using (var reader = Set3FileDefinition.ReadSet3TempFile())
                {
                    while ((currentLine = reader.ReadLine()) != null)
                    {

                       foreach(int i in randomLineNum)
                       {
                            if (indicator == i)
                            {
                                var fileData = Set3FileDefinition.GetSet3FileData(currentLine);

                                if (fileData[0].modifier != "26")
                                {

                                    var dbData = Set3FileDefinition.GetSet3StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);
                                    Assert.AreEqual(fileData[0].description, dbData[1].description);
                                    Assert.AreEqual(fileData[0].parFeeNonFacility, dbData[1].parFeeNonFacility);
                                    Assert.AreEqual(fileData[0].parFeeFacility, dbData[1].parFeeFacility);
                                    Assert.AreEqual(fileData[0].nonParFeeNonFacility, dbData[1].nonParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].nonParFeeFacility, dbData[1].nonParFeeFacility);
                                    Assert.AreEqual(fileData[0].oppsParFeeNonFacility, dbData[1].oppsParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].oppsParFeeFacility, dbData[1].oppsParFeeFacility);
                                    Assert.AreEqual(fileData[0].oppsNonParFeeNonFacility, dbData[1].oppsNonParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].oppsNonParFeeFacility, dbData[1].oppsNonParFeeFacility);
                                    Assert.AreEqual(fileData[0].globalSurgery, dbData[1].globalSurgery);
                                    Assert.AreEqual(fileData[0].calcFlag, dbData[1].calcFlag);
                                    Assert.AreEqual(fileData[0].parFeeFacility.TrimStart(new char[] { '0' }), dbData[1].PricingAmount1.TrimStart(new char[] { '0' }));
                                    Assert.AreEqual(fileData[0].parFeeNonFacility.TrimStart(new char[] { '0' }), dbData[1].PricingAmount2.TrimStart(new char[] { '0' }));

                                }
                                if (fileData[0].modifier != "53")
                                {

                                    var dbData = Set3FileDefinition.GetSet3StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);
                                    Assert.AreEqual(fileData[0].description, dbData[1].description);
                                    Assert.AreEqual(fileData[0].parFeeNonFacility, dbData[1].parFeeNonFacility);
                                    Assert.AreEqual(fileData[0].parFeeFacility, dbData[1].parFeeFacility);
                                    Assert.AreEqual(fileData[0].nonParFeeNonFacility, dbData[1].nonParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].nonParFeeFacility, dbData[1].nonParFeeFacility);
                                    Assert.AreEqual(fileData[0].oppsParFeeNonFacility, dbData[1].oppsParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].oppsParFeeFacility, dbData[1].oppsParFeeFacility);
                                    Assert.AreEqual(fileData[0].oppsNonParFeeNonFacility, dbData[1].oppsNonParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].oppsNonParFeeFacility, dbData[1].oppsNonParFeeFacility);
                                    Assert.AreEqual(fileData[0].globalSurgery, dbData[1].globalSurgery);
                                    Assert.AreEqual(fileData[0].calcFlag, dbData[1].calcFlag);
                                    Assert.AreEqual(fileData[0].parFeeFacility.TrimStart(new char[] { '0' }), dbData[1].PricingAmount1.TrimStart(new char[] { '0' }));
                                    Assert.AreEqual(fileData[0].parFeeNonFacility.TrimStart(new char[] { '0' }), dbData[1].PricingAmount2.TrimStart(new char[] { '0' }));

                                }
                                if (fileData[0].modifier != "TC")
                                {

                                    var dbData = Set3FileDefinition.GetSet3StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);
                                    Assert.AreEqual(fileData[0].description, dbData[2].description);
                                    Assert.AreEqual(fileData[0].parFeeNonFacility, dbData[2].parFeeNonFacility);
                                    Assert.AreEqual(fileData[0].parFeeFacility, dbData[2].parFeeFacility);
                                    Assert.AreEqual(fileData[0].nonParFeeNonFacility, dbData[2].nonParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].nonParFeeFacility, dbData[2].nonParFeeFacility);
                                    Assert.AreEqual(fileData[0].oppsParFeeNonFacility, dbData[2].oppsParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].oppsParFeeFacility, dbData[2].oppsParFeeFacility);
                                    Assert.AreEqual(fileData[0].oppsNonParFeeNonFacility, dbData[2].oppsNonParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].oppsNonParFeeFacility, dbData[2].oppsNonParFeeFacility);
                                    Assert.AreEqual(fileData[0].globalSurgery, dbData[2].globalSurgery);
                                    Assert.AreEqual(fileData[0].calcFlag, dbData[2].calcFlag);
                                    Assert.AreEqual(fileData[0].parFeeFacility.TrimStart(new char[] { '0' }), dbData[2].PricingAmount1.TrimStart(new char[] { '0' }));
                                    Assert.AreEqual(fileData[0].parFeeNonFacility.TrimStart(new char[] { '0' }), dbData[2].PricingAmount2.TrimStart(new char[] { '0' }));

                                }
                                else
                                {

                                    var dbData = Set3FileDefinition.GetSet3StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);
                                    Assert.AreEqual(fileData[0].description, dbData[0].description);
                                    Assert.AreEqual(fileData[0].parFeeNonFacility, dbData[0].parFeeNonFacility);
                                    Assert.AreEqual(fileData[0].parFeeFacility, dbData[0].parFeeFacility);
                                    Assert.AreEqual(fileData[0].nonParFeeNonFacility, dbData[0].nonParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].nonParFeeFacility, dbData[0].nonParFeeFacility);
                                    Assert.AreEqual(fileData[0].oppsParFeeNonFacility, dbData[0].oppsParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].oppsParFeeFacility, dbData[0].oppsParFeeFacility);
                                    Assert.AreEqual(fileData[0].oppsNonParFeeNonFacility, dbData[0].oppsNonParFeeNonFacility);
                                    Assert.AreEqual(fileData[0].oppsNonParFeeFacility, dbData[0].oppsNonParFeeFacility);
                                    Assert.AreEqual(fileData[0].globalSurgery, dbData[0].globalSurgery);
                                    Assert.AreEqual(fileData[0].calcFlag, dbData[0].calcFlag);
                                    Assert.AreEqual(fileData[0].parFeeFacility.TrimStart(new char[] { '0' }), dbData[0].PricingAmount1.TrimStart(new char[] { '0' }));
                                    Assert.AreEqual(fileData[0].parFeeNonFacility.TrimStart(new char[] { '0' }), dbData[0].PricingAmount2.TrimStart(new char[] { '0' }));

                                }
                            }
                       }
                        indicator++;
                    }
                }
            }
            catch (Exception e)
            {
                report.Log(Status.Fail, e);
                Console.WriteLine(e);
            }
        }
       /*
        [Test]
        [Description("Verify number of records in BenchmarkStageDB match BenchmarkDB")]
        public void CompareStageDBCountToBenckmarkDB() 
        {
            try
            {
                report = rlog.CreateTest($"Compare file count to benchmarkdb");
                //Count line in the extracted files
                var lineCounter = FileHelper.countlines(ConfigurationManager.AppSettings["extractFilePath"] + "SET4_ALL.TXT");

                //Reads the database to get the number of records that gets imported
                var dbCount = 0L;
                string recordCountQuery = "Select count(*) from Benchmarks where method=7 and DateStart='2021-04-01' and DateEnd IS NULL";
                string connectionString = ConfigurationManager.AppSettings["RDPBenchmarkConnstring"];
                SqlDataReader sqlReader = DatabaseHelperExtensions.ExecuteQuery(connectionString, recordCountQuery);
                while (sqlReader.Read())
                {
                    dbCount = (long)sqlReader.GetInt32(0);
                }

                Assert.AreEqual(lineCounter, dbCount);
            }
            catch (Exception e)
            {
                report.Log(Status.Fail, e);
                Console.WriteLine(e);
            }
        }

        [Test]
        [Description("Verify First line of File match Benchmark Database")]
        public void CompareStageDBFirsttoBenchmarkDB()
        {
            try
            {
                report = rlog.CreateTest($"Compare 1st line to benchmarkdb");

                string currentLine = File.ReadLines(ConfigurationManager.AppSettings["extractFilePath"] + "SET4_ALL.TXT").First();
                var fileData = Set4FileDefinition.GetSet4FileData(currentLine);

                string dateStart = ExcelHelper.readTestData("SET 4", "DateStart");
                string dateEnd = ExcelHelper.readTestData("SET 4", "DateEnd");

                var dbData = BenchmarkDBQueries.GetBenchmarksTableData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode, 2);

                Assert.AreEqual("Null", dbData[0].dateEnd);
                Assert.AreEqual(dateStart, dbData[0].dateStart);
                Assert.AreEqual(fileData[0].locality, dbData[0].locationCode2);
                Assert.AreEqual(fileData[0].contractor, dbData[0].locationCode1);
                Assert.AreEqual(fileData[0].payment.TrimStart(new char[] { '0' }), dbData[0].pricingamt1.TrimStart(new char[] { '0' }));
                Assert.AreEqual(dateEnd, dbData[1].dateEnd);



            }
            catch (Exception e)
            {
                report.Log(Status.Fail, e);
                Console.WriteLine(e);
            }
        }

        [Test]
        [Description("Verify Last line of File match Benchmark Database")]
        public void CompareStageDBLasttoBenchmarkDB()
        {
            try
            {
                report = rlog.CreateTest($"Compare last line to benchmarkdb");

                string currentLine = File.ReadLines(ConfigurationManager.AppSettings["extractFilePath"] + "SET4_ALL.TXT").Last();
                var fileData = Set4FileDefinition.GetSet4FileData(currentLine);

                string dateStart = ExcelHelper.readTestData("SET 4", "DateStart");
                string dateEnd = ExcelHelper.readTestData("SET 4", "DateEnd");

                var dbData = BenchmarkDBQueries.GetBenchmarksTableData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode, 2);

                Assert.AreEqual("Null", dbData[0].dateEnd);
                Assert.AreEqual(dateStart, dbData[0].dateStart);
                Assert.AreEqual(fileData[0].locality, dbData[0].locationCode2);
                Assert.AreEqual(fileData[0].contractor, dbData[0].locationCode1);
                Assert.AreEqual(fileData[0].payment.TrimStart(new char[] { '0' }), dbData[0].pricingamt1.TrimStart(new char[] { '0' }));
                Assert.AreEqual(dateEnd, dbData[1].dateEnd);


            }
            catch (Exception e)
            {
                report.Log(Status.Fail, e);
                Console.WriteLine(e);
            }
        }

        [Test]
        [Description("Verify Random lines of File match BenchmarkStage Database")]
        public void CompareStageDBRandomytoBenchmarkDB()
        {
            try
            {
                report = rlog.CreateTest($"Compare random lines benchmarkdb");

                int lineCount = (int)FileHelper.countlines(ConfigurationManager.AppSettings["extractFilePath"] + "SET4_ALL.TXT");
                int[] randomLineNum = FileHelper.generateRandomArray(lineCount, 5);
                Array.Sort(randomLineNum);
                string[] results = randomLineNum.Select(x => x.ToString()).ToArray();
                int indicator = 0;
                string currentLine = null;


                string dateStart = ExcelHelper.readTestData("SET 4", "DateStart");
                string dateEnd = ExcelHelper.readTestData("SET 4", "DateEnd");


                using (var reader = File.OpenText(ConfigurationManager.AppSettings["extractFilePath"] + "SET4_ALL.TXT"))
                {
                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        foreach (int i in randomLineNum)
                        {
                            if (indicator==i)
                            {
                                var fileData = Set4FileDefinition.GetSet4FileData(currentLine);
                                Console.WriteLine("Currentline: " + fileData[0].contractor+ "|" + fileData[0].locality + "|" + fileData[0].CPTCode + "|" + fileData[0].modifier + "|" + fileData[0].payment.TrimStart(new char[] { '0' }));

                                int dbCount = Set4FileDefinition.GetSet4StageDBCount(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);
                                Console.WriteLine("DBCount: " + dbCount);

                                var stageData = Set4FileDefinition.GetSet4StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);
                                if(dbCount == 1)
                                {
                                    Console.Write("Stage Data 1: " + stageData[0].contractor + "|" + stageData[0].locality + "|" + stageData[0].CPTCode + "|" + stageData[0].modifier + "|" + stageData[0].description + "|" + stageData[0].limitationAmount + "|" + stageData[0].payment + "|" + stageData[0].pricingAmt.TrimStart(new char[] { '0' }));
                                }
                                else
                                {
                                    Console.Write("Stage Data 1: " + stageData[0].contractor + "|" + stageData[0].locality + "|" + stageData[0].CPTCode + "|" + stageData[0].modifier + "|" + stageData[0].description + "|" + stageData[0].limitationAmount + "|" + stageData[0].payment + "|" + stageData[0].pricingAmt.TrimStart(new char[] { '0' }));
                                    Console.Write("Stage Data 2: " + stageData[1].contractor + "|" + stageData[1].locality + "|" + stageData[1].CPTCode + "|" + stageData[1].modifier + "|" + stageData[1].description + "|" + stageData[1].limitationAmount + "|" + stageData[1].payment + "|" + stageData[0].pricingAmt.TrimStart(new char[] { '0' }));

                                }

                                var benchmarkData = BenchmarkDBQueries.GetBenchmarksTableData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode, 4);
                                Console.Write("Benchmark Data 1: " + benchmarkData[0].dateEnd + "|" + benchmarkData[0].dateStart + "|" + benchmarkData[0].locationCode1 + "|" + benchmarkData[0].locationCode2 + "|" + benchmarkData[0].serviceDescription1 + "|" + benchmarkData[0].serviceDescription2 + "|" + benchmarkData[0].pricingamt1.TrimStart(new char[] { '0' }));
                                Console.Write("Benchmark Data 2: " + benchmarkData[1].dateEnd + "|" + benchmarkData[1].dateStart + "|" + benchmarkData[1].locationCode1 + "|" + benchmarkData[1].locationCode2 + "|" + benchmarkData[1].serviceDescription1 + "|" + benchmarkData[1].serviceDescription2 + "|" + benchmarkData[1].pricingamt1.TrimStart(new char[] { '0' }));
                                Console.Write("Benchmark Data 3: " + benchmarkData[2].dateEnd + "|" + benchmarkData[2].dateStart + "|" + benchmarkData[2].locationCode1 + "|" + benchmarkData[2].locationCode2 + "|" + benchmarkData[2].serviceDescription1 + "|" + benchmarkData[2].serviceDescription2 + "|" + benchmarkData[2].pricingamt1.TrimStart(new char[] { '0' }));
                                Console.Write("Benchmark Data 4: " + benchmarkData[3].dateEnd + "|" + benchmarkData[3].dateStart + "|" + benchmarkData[3].locationCode1 + "|" + benchmarkData[3].locationCode2 + "|" + benchmarkData[3].serviceDescription1 + "|" + benchmarkData[3].serviceDescription2 + "|" + benchmarkData[3].pricingamt1.TrimStart(new char[] { '0' }));

                                if (dbCount > 1)
                                {
                                    //Check first row of DB Query                                   
                                    Assert.AreEqual("Null", benchmarkData[0].dateEnd);
                                    Assert.AreEqual(dateStart, benchmarkData[0].dateStart);
                                    Assert.AreEqual(stageData[0].locality, benchmarkData[0].locationCode2);
                                    Assert.AreEqual(stageData[0].contractor, benchmarkData[0].locationCode1);
                                    Assert.AreEqual("Null", benchmarkData[0].serviceDescription2);
                                    Assert.AreEqual(stageData[0].payment.TrimStart(new char[] { '0' }), benchmarkData[0].pricingamt1.TrimStart(new char[] { '0' }));

                                    //Check second row of DB Query
                                    Assert.AreEqual("Null", benchmarkData[1].dateEnd);
                                    Assert.AreEqual(dateStart, benchmarkData[1].dateStart);
                                    Assert.AreEqual(stageData[0].locality, benchmarkData[1].locationCode2);
                                    Assert.AreEqual(stageData[0].contractor, benchmarkData[1].locationCode1);
                                    Assert.AreEqual(stageData[1].modifier, benchmarkData[1].serviceDescription2);
                                    Assert.AreEqual(stageData[0].payment.TrimStart(new char[] { '0' }), benchmarkData[0].pricingamt1.TrimStart(new char[] { '0' }));

                                    //Check third row of DB Query
                                    Assert.AreEqual(dateEnd, benchmarkData[2].dateEnd);
                                    Assert.AreEqual("Null", benchmarkData[2].serviceDescription2);

                                    //Check fourth row of DB Query
                                    Assert.AreEqual(dateEnd, benchmarkData[3].dateEnd);
                                    Assert.AreEqual(stageData[1].modifier, benchmarkData[3].serviceDescription2);

                                }
                                else
                                {
                                    //Check first row
                                    Assert.AreEqual("Null", benchmarkData[0].dateEnd);
                                    Assert.AreEqual(dateStart, benchmarkData[0].dateStart);
                                    Assert.AreEqual(stageData[0].locality, benchmarkData[0].locationCode2);
                                    Assert.AreEqual(stageData[0].contractor, benchmarkData[0].locationCode1);
                                    Assert.AreEqual("Null", benchmarkData[0].serviceDescription2);
                                    Assert.AreEqual(stageData[0].payment.TrimStart(new char[] { '0' }), benchmarkData[0].pricingamt1.TrimStart(new char[] { '0' }));

                                    Assert.AreEqual(dateEnd, benchmarkData[1].dateEnd);
                                    Assert.AreEqual("Null", benchmarkData[1].serviceDescription2);
                                }
                            }
                        }
                        indicator++;
                    }
                }
            }
            catch (Exception e)
            {
                report.Log(Status.Fail, e);
                Console.WriteLine(e);
            }
        }
        */
    }
}
