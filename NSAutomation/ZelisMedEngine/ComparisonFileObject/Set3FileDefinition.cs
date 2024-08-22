using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using zelisCompassFrameWorkLib.zelisHelpers;

namespace ZelisMedEngine.ComparisonFileObject
{
    class Set3FileDefinition
    {
        public string contractor { get; set; }
        public string locality { get; set; }
        public string CPTCode { get; set; }
        public string modifier { get; set; }
        public string description { get; set; }
        public string statusCode { get; set; }
        public string parFeeNonFacility { get; set; }
        public string parFeeFacility { get; set; }
        public string nonParFeeNonFacility { get; set; }
        public string nonParFeeFacility { get; set; }
        public string oppsParFeeNonFacility { get; set; }
        public string oppsParFeeFacility { get; set; }
        public string oppsNonParFeeNonFacility { get; set; }
        public string oppsNonParFeeFacility { get; set; }
        public string globalSurgery { get; set; }
        public string calcFlag { get; set; }
        public string PricingAmount1  { get; set; }
        public string PricingAmount2 { get; set; }

        public static StreamReader ReadSet3File()
        {
            var reader = File.OpenText(ConfigurationManager.AppSettings["extractFilePath"] + "SET3_ALL.TXT");
            return reader;
        }

        public static long CountSet3Lines()
        {
            //Read a file and counts the number of line in that file
            string[] statusCodes = { "R", "A", "T" };
            var lineCounter = 0L;
            string currentLine = null;
            
            //using (var reader = File.OpenText(ConfigurationManager.AppSettings["extractFilePath"] + "SET3_ALL.TXT"))
            using (var reader = ReadSet3File())
            {
                while ((currentLine = reader.ReadLine()) != null)
                {
                    var fileData = Set3FileDefinition.GetSet3FileData(currentLine);

                    if (statusCodes.Contains(fileData[0].statusCode))
                    {
                        lineCounter++;
                    }
                }
            }
            return lineCounter;
        }

        public static List<Set3FileDefinition> GetSet3FileData(string currentLine)
        {
            /*uses the string returned when reading a single line of the import file then divides the string into individual
            data values and builds a List collection that is returned to be used in test
            */

            List<Set3FileDefinition> fileData = new List<Set3FileDefinition>();
            Set3FileDefinition row = new Set3FileDefinition();
            row.contractor = currentLine.Substring(0, 5);
            row.locality = currentLine.Substring(5, 2);
            row.CPTCode = currentLine.Substring(7, 5);
            row.modifier = currentLine.Substring(12, 2);
            row.description = currentLine.Substring(14, 30);
            row.statusCode = currentLine.Substring(44, 1);
            row.parFeeNonFacility = currentLine.Substring(45, 8);
            row.parFeeFacility = currentLine.Substring(53, 8);
            row.nonParFeeNonFacility = currentLine.Substring(61, 8);
            row.nonParFeeFacility = currentLine.Substring(69, 8);
            row.oppsParFeeNonFacility = currentLine.Substring(77, 8);
            row.oppsParFeeFacility = currentLine.Substring(85, 8);
            row.oppsNonParFeeNonFacility = currentLine.Substring(93, 8);
            row.oppsNonParFeeFacility = currentLine.Substring(101, 8);
            row.globalSurgery = currentLine.Substring(109, 3);
            row.calcFlag = currentLine.Substring(112, 1);


            fileData.Add(row);

            return fileData;

        }

        public static List<Set3FileDefinition> GetSet3StageDBData(string recContractor, string recLocality, string recCPTCode)
        {
            /* Executes a query against the database and adds the results to a List collection
            */


            List<Set3FileDefinition> dbData = new List<Set3FileDefinition>();
            string recordQuery = "Select * from MedicareLocalitySpecificPhysicianFeeSchedules where contractor='" + recContractor + "' and Locality=" + recLocality + " and Code='" + recCPTCode + "'";
            string connectionString = ConfigurationManager.AppSettings["RDPBenchmarkStageConnstring"];
            SqlDataReader sqlReader = DatabaseHelperExtensions.ExecuteQuery(connectionString, recordQuery);

            try
            {
                while (sqlReader.Read())
                {

                    Set3FileDefinition row = new Set3FileDefinition();

                    row.contractor = sqlReader.GetString(0);
                    row.locality = sqlReader.GetString(1);
                    row.CPTCode = sqlReader.GetString(2);
                    row.modifier = sqlReader.GetString(3);
                    row.description = sqlReader.GetString(4);
                    row.statusCode = sqlReader.GetString(5);
                    row.parFeeNonFacility = sqlReader.GetString(6);
                    row.parFeeFacility = sqlReader.GetString(7);
                    row.nonParFeeNonFacility = sqlReader.GetString(8);
                    row.nonParFeeFacility = sqlReader.GetString(9);
                    row.oppsParFeeNonFacility = sqlReader.GetString(10);
                    row.oppsParFeeFacility = sqlReader.GetString(11);
                    row.oppsNonParFeeNonFacility = sqlReader.GetString(12);
                    row.oppsNonParFeeFacility = sqlReader.GetString(13);
                    row.globalSurgery = sqlReader.GetString(14);
                    row.calcFlag = sqlReader.GetString(15);
                    row.PricingAmount1 = sqlReader.GetDecimal(16).ToString();
                    row.PricingAmount2 = sqlReader.GetDecimal(17).ToString();

                    dbData.Add(row);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            sqlReader.Close();
            return dbData;
        }

        public static int GetSet3StageDBCount()
        {
        //Executes a query against the database to get number of database 

            int dbCount = 0;
            string recordQuery = "Select Count(contractor) from MedicareLocalitySpecificPhysicianFeeSchedules where StatusCode IN ('R','A','T')";
            string connectionString = ConfigurationManager.AppSettings["RDPBenchmarkStageConnstring"];
            SqlDataReader sqlReader = DatabaseHelperExtensions.ExecuteQuery(connectionString, recordQuery);
            while (sqlReader.Read())
            {

                dbCount = sqlReader.GetInt32(0);
            }
            sqlReader.Close();
            return dbCount;
        }

        public static int GetSet3StageDBCount(string recContractor, string recLocality, string recCPTCode)
        {
            /* Executes a query against the database to get number of database for a specific Contractor, Locality, CPTCode
            */

            int dbCount = 0;
            string recordQuery = "Select Count(*) from MedicareLocalitySpecificPhysicianFeeSchedules where contractor='" + recContractor + "' and Locality=" + recLocality + " and Code='" + recCPTCode + "' and StatusCode IN ('R','A','T')";
            string connectionString = ConfigurationManager.AppSettings["RDPBenchmarkStageConnstring"];
            SqlDataReader sqlReader = DatabaseHelperExtensions.ExecuteQuery(connectionString, recordQuery);
            while (sqlReader.Read())
            {

                dbCount = sqlReader.GetInt32(0);
            }
            sqlReader.Close();

            return dbCount;

        }

        //Summary
        //Creates a temporary working file with the non-relavent Status Codes removed
        //Summary
        public static void CreateSet3TempFile()
        {
            
            string currentLine;
            string[] statusCodes = { "R", "A", "T" };
            
            using (var reader = Set3FileDefinition.ReadSet3File())
            using (var writer = new StreamWriter(ConfigurationManager.AppSettings["extractFilePath"] + "SET3TEMP.TXT"))
            {
                while ((currentLine = reader.ReadLine()) != null)
                {
                    if (statusCodes.Contains(currentLine.Substring(44, 1)))
                    {
                        writer.WriteLine(currentLine);
                    }
                }
            }

        }

        //Summary
        //Counts the number of records in the Temporary file created to remove the non-relavent Status Codes
        //Summary
        public static long CountSet3TempLines()
        {
            var lineCounter = 0L;
            string currentLine;

            using (var reader = ReadSet3TempFile())
            {
                while ((currentLine = reader.ReadLine()) != null)
                {
                    lineCounter++;

                }
            }
            return lineCounter;
        }

        //Summary
        //Opens the Temporary file created to remove the non-relevant Status Codes and return a stream so that when the file is read it can be broken down into
        //a collection
        //Summary
        public static StreamReader ReadSet3TempFile()
        {
            var reader = File.OpenText(ConfigurationManager.AppSettings["extractFilePath"] + "SET3TEMP.TXT");
            return reader;
        }

        public static string ReadSet3TempFileFirst()
        {
            var lastLine = File.ReadLines(ConfigurationManager.AppSettings["extractFilePath"] + "SET3TEMP.TXT").First();
            return lastLine;
        }

        public static string ReadSet3TempFileLast()
        {
            var lastLine = File.ReadLines(ConfigurationManager.AppSettings["extractFilePath"] + "SET3TEMP.TXT").Last();
            return lastLine;
        }

        
    }

}
