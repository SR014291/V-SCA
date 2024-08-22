using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using zelisCompassFrameWorkLib.zelisHelpers;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZelisMedEngine.ComparisonFileObject
{
    class BenchmarkDBQueries
    {
        //Benchmark Benchmarks Table Definition
        public string dateEnd { get; set; }
        public string dateStart { get; set; }
        public string locationCode1 { get; set; }
        public string locationCode2 { get; set; }
        public string serviceDescription1 { get; set; }
        public string serviceDescription2 { get; set; }
        public string pricingamt1 { get; set; }
        public string pricingamt2 { get; set; }


        public static List<BenchmarkDBQueries> GetBenchmarksTableData(string recContractor, string recLocality, string recCPTCode, int returnCount)
        {

                List<BenchmarkDBQueries> dbData = new List<BenchmarkDBQueries>();
                string recordQuery = "Select Top(" + returnCount + ") DateEnd, DateStart,  LocationCode1, LocationCode2, ServiceDescription1, ServiceDescription2, PricingAmount1 From Benchmarks where Method=7 and LocationCode1='" + recContractor + "' and LocationCode2=" + recLocality + " and ServiceDescription1='" + recCPTCode + "' Order by DateStart Desc";
                string connectionString = ConfigurationManager.AppSettings["RDPBenchmarkConnstring"];
                SqlDataReader sqlReader = DatabaseHelperExtensions.ExecuteQuery(connectionString, recordQuery);
                try
                {
                    while (sqlReader.Read())
                    {

                        BenchmarkDBQueries row = new BenchmarkDBQueries();

                        if (sqlReader.IsDBNull(0))
                        {
                            row.dateEnd = "Null";
                        }
                        else
                        {
                            row.dateEnd = sqlReader.GetDateTime(0).ToString("yyyy-MM-dd");
                        }

                    if (sqlReader.IsDBNull(4))
                    {
                        row.serviceDescription1 = "Null";
                    }
                    else
                    {
                        row.serviceDescription1 = sqlReader.GetString(4);
                    }
                    if (sqlReader.IsDBNull(5))
                        {
                            row.serviceDescription2 = "Null";
                        }
                        else
                        {
                            row.serviceDescription2 = sqlReader.GetString(5);
                        }
                        row.dateStart = sqlReader.GetDateTime(1).ToString("yyyy-MM-dd");
                        row.locationCode1 = sqlReader.GetString(2);
                        row.locationCode2 = sqlReader.GetString(3);
                        row.pricingamt1 = sqlReader.GetDecimal(6).ToString();

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
    }
}
