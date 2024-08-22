using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace zelisCompassFrameWorkLib.zelisHelpers
{
    public static class DatabaseHelperExtensions
    {
        //Open the connection
        public static SqlConnection DBConnect(string dataconnectionString)
        {
            //string ConnectionString = "///";
            try
            {
                
                SqlConnection sqlConnect = new SqlConnection();
                sqlConnect.ConnectionString = dataconnectionString;
                sqlConnect.Open();
                return sqlConnect;
            }
            catch (Exception e)
            {
                LogHelper.Write("ERROR :: " + e.Message);
            }

            return null;
        }



        //Closing the connection 
        public static void DBClose(this SqlConnection sqlConnection)
        {
            try
            {
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                LogHelper.Write("ERROR :: " + e.Message);
            }

        }


        //Execution
        public static SqlDataReader ExecuteQuery(string connectionString, string queryString)
        {
            SqlConnection sqlCnn;
            SqlCommand sqlCmd=null;
            SqlDataReader sqlReader = null;
            //string connetionString = "Data Source=...; Initial Catalog= CMS_QA;Integrated Security=True";
            
            sqlCnn = new SqlConnection(connectionString);
            try
            {
                sqlCnn.Open();
                if (sqlCnn.State == System.Data.ConnectionState.Open)
                {
                    sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = queryString;
                    sqlReader = sqlCmd.ExecuteReader(CommandBehavior.Default);
                    
                }
                return sqlReader;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return sqlReader;
            }
            
            




        }
    }
}
