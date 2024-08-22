using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using zelisCompassFrameWorkLib.zelisHelpers;

namespace ZelisMedEngine.ComparisonFileObject
{
    public class Set4FileDefinition
    {   
        //Set 4 File Data Elements
        public string contractor { get; set; }
        public string locality { get; set; }
        public string CPTCode { get; set; }
        public string modifier { get; set; }
        public string description { get; set; }
        public string limitationAmount { get; set; }
        public string payment { get; set; }
        public string pricingAmt { get; set; }
        public string pricingIndicator { get; set; }
        

        
        public static List<Set4FileDefinition> GetSet4FileData(string currentLine)
        {
            /*uses the string returned when reading a single line of the import file then divides the string into individual
            data values and builds a List collection that is returned to be used in test
            */
            
            List<Set4FileDefinition> fileData = new List<Set4FileDefinition>();
            Set4FileDefinition row = new Set4FileDefinition();
            row.contractor=currentLine.Substring(0, 5);
            row.locality=currentLine.Substring(5, 2);
            row.CPTCode=currentLine.Substring(7, 5);
            row.modifier=currentLine.Substring(12, 2);
            row.description=currentLine.Substring(14, 30);
            row.limitationAmount=currentLine.Substring(44, 8);
            row.payment=currentLine.Substring(52, 8);
            row.pricingIndicator=currentLine.Substring(60, 1);

            fileData.Add(row);

            return fileData;

        }

        public static List<Set4FileDefinition> GetSet4StageDBData(string recContractor, string recLocality, string recCPTCode)
        {
            /* Executes a query against the database and adds the results to a List collection
            */


            List<Set4FileDefinition> dbData = new List<Set4FileDefinition>();
            string recordQuery = "Select * from MedicareClinicalLabFeeSchedules where contractor='" + recContractor + "' and Locality=" + recLocality + " and Code='" + recCPTCode + "'";
            string connectionString = ConfigurationManager.AppSettings["RDPBenchmarkStageConnstring"];
            SqlDataReader sqlReader = DatabaseHelperExtensions.ExecuteQuery(connectionString, recordQuery);

            try
            {
               while (sqlReader.Read())
               {

                   Set4FileDefinition row = new Set4FileDefinition();

                   row.contractor = sqlReader.GetString(0);
                   row.locality = sqlReader.GetString(1);
                   row.CPTCode = sqlReader.GetString(2);
                   row.modifier = sqlReader.GetString(3);
                   row.description = sqlReader.GetString(4);
                   row.limitationAmount = sqlReader.GetString(5);
                   row.payment = sqlReader.GetString(6);
                   row.pricingAmt = sqlReader.GetDecimal(7).ToString();

                   dbData.Add(row);
               }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            sqlReader.Close();
            return dbData;
        }
       
        public static int GetSet4StageDBCount(string recContractor, string recLocality, string recCPTCode)
        {
            /* Executes a query against the database to get number of database for a specific Contractor, Locality, CPTCode
            */

            int dbCount=0;
            string recordQuery = "Select Count(*) from MedicareClinicalLabFeeSchedules where contractor='" + recContractor + "' and Locality=" + recLocality + " and Code='" + recCPTCode + "'";
            string connectionString = ConfigurationManager.AppSettings["RDPBenchmarkStageConnstring"];
            SqlDataReader sqlReader = DatabaseHelperExtensions.ExecuteQuery(connectionString, recordQuery);
            while (sqlReader.Read())
            {

               dbCount = sqlReader.GetInt32(0);
            }
            sqlReader.Close();

            return dbCount;

        }


    }
}
