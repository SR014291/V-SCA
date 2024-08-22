using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using zelisCompassFrameWorkLib.zelisHelpers;
using System.Configuration;
using NUnit.Framework;
using System.Data.SqlClient;
using AventStack.ExtentReports;
using System.Linq;
using ZelisMedEngine.ComparisonFileObject;


namespace ZelisMedEngine.FileComparison
{
    [TestFixture]
    public class set4FileComparison
    {
        
        ExtentReports rlog = ReportHelper.getReportInstanceMedEngine();
        ExtentTest report;
        

        [SetUp]
        public void GetZipFile()
        {
            //extractZip file
            try
            {
                FileHelper.extractFile("SET 4", "ZipFileName");
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
        [Description("Verify the number of records in the SET4 file match the BenchmarkStage Database")]
        public void CompareSet4FileCountToStageDB()
        {
            try
            {
                report = rlog.CreateTest("Compare SET4 file count to stagedb");
                
                //Count line in the extracted files
                var lineCounter = FileHelper.countlines(ConfigurationManager.AppSettings["extractFilePath"] + "SET4_ALL.TXT");

                //Reads the database to get the number of records that gets imported
                var dbCount = 0L;
                string recordCountQuery = "Select Count(*) from MedicareClinicalLabFeeSchedules";
                string connectionString = ConfigurationManager.AppSettings["RDPBenchmarkStageConnstring"];
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
        [Description("Verify First line of File match Benchmarkstage Database")]
        public void CompareSet4FirstInFiletoStageDB()
        {
            try
            {
                report = rlog.CreateTest($"Compare 1st line to stageDB");

                // Read the first line of the Import file into string then create a list collection
                string currentLine = File.ReadLines(ConfigurationManager.AppSettings["extractFilePath"] + "SET4_ALL.TXT").First();

                
                var fileData = Set4FileDefinition.GetSet4FileData(currentLine);
                var dbData = Set4FileDefinition.GetSet4StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);
                             

                Assert.AreEqual(fileData[0].modifier, dbData[0].modifier);
                Assert.AreEqual(fileData[0].description, dbData[0].description);
                Assert.AreEqual(fileData[0].limitationAmount, dbData[0].limitationAmount);
                Assert.AreEqual(fileData[0].payment, dbData[0].payment);

            }
            catch (Exception e)
            {
                report.Log(Status.Fail, e);
                Console.WriteLine(e);
            }
        }

        [Test]
        [Description("Verify Last Line File Matches BenchmarkStage Database")]
        public void CompareSet4LastInFiletoStageDB()
        {
            try
            {
                report = rlog.CreateTest($"Compare last line to stagedb");
                string currentLine = File.ReadLines(ConfigurationManager.AppSettings["extractFilePath"] + "SET4_ALL.TXT").Last();
                var fileData = Set4FileDefinition.GetSet4FileData(currentLine);

                var dbData = Set4FileDefinition.GetSet4StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);


                Assert.AreEqual(fileData[0].modifier, dbData[0].modifier);
                Assert.AreEqual(fileData[0].description, dbData[0].description);
                Assert.AreEqual(fileData[0].limitationAmount, dbData[0].limitationAmount);
                Assert.AreEqual(fileData[0].payment, dbData[0].payment);

            }
            catch (Exception e)
            {
                report.Log(Status.Fail, e);
                Console.WriteLine(e);
            }
        }

        [Test]
        [Description("Verify Random lines of File match BenchmarkStage Database")]
        public void CompareSet4RandomyFromFiletoStagekDB()
        {
            try
            {
                report = rlog.CreateTest($"Compare Random lines stagedb");

                int lineCount = (int)FileHelper.countlines(ConfigurationManager.AppSettings["extractFilePath"] + "SET4_ALL.TXT");
                int[] randomLineNum = FileHelper.generateRandomArray(lineCount, 10);
                Array.Sort(randomLineNum);
                string[] results = randomLineNum.Select(x => x.ToString()).ToArray();
                int indicator = 0;
                string currentLine = null;

                using (var reader = File.OpenText(ConfigurationManager.AppSettings["extractFilePath"] + "SET4_ALL.TXT"))
                {
                    while ((currentLine = reader.ReadLine()) != null)
                    {

                       foreach(int i in randomLineNum)
                       {
                            if (indicator == i)
                            {
                                var fileData = Set4FileDefinition.GetSet4FileData(currentLine);

                                if (fileData[0].modifier != "  ")
                                {

                                    var dbData = Set4FileDefinition.GetSet4StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);

                                    Assert.AreEqual("  ", dbData[0].modifier);
                                    Assert.AreEqual(fileData[0].description, dbData[0].description);
                                    Assert.AreEqual(fileData[0].limitationAmount, dbData[0].limitationAmount);
                                    Assert.AreEqual(fileData[0].payment, dbData[0].payment);

                                    Assert.AreEqual(fileData[0].modifier, dbData[1].modifier);
                                    Assert.AreEqual(fileData[0].description, dbData[1].description);
                                    Assert.AreEqual(fileData[0].limitationAmount, dbData[1].limitationAmount);
                                    Assert.AreEqual(fileData[0].payment, dbData[1].payment);

                                }
                                else
                                {
                                    var dbData = Set4FileDefinition.GetSet4StageDBData(fileData[0].contractor, fileData[0].locality, fileData[0].CPTCode);

                                    Assert.AreEqual(fileData[0].modifier, dbData[0].modifier);
                                    Assert.AreEqual(fileData[0].description, dbData[0].description);
                                    Assert.AreEqual(fileData[0].limitationAmount, dbData[0].limitationAmount);
                                    Assert.AreEqual(fileData[0].payment, dbData[0].payment);

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
    }
}
