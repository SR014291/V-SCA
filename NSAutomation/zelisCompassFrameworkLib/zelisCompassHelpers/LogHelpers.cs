using System;
using System.IO;

namespace zelisCompassFrameWorkLib.zelisHelpers
{
    public class LogHelper
    {
        //log file name (Global Declaration)
       // private static string _logFileName = string.Format("{0:yyyymmddhhmmss}", DateTime.Now);
        private static StreamWriter _steamWriter = null;
        //create a file to store the test informations
        public static void CreateTestEvidenceFile()
        {
            string _logFileName = string.Format("{0:yyyymmdd}", DateTime.Now);
            string FileDirectory = @"C:\AutomationTestResult\Compass\";

            
            try
            { 
                    if (Directory.Exists(FileDirectory))
                {

                    _steamWriter = File.AppendText($"{FileDirectory}{_logFileName}-{Guid.NewGuid().ToString()}.log");
                }
                else
                {
                    Directory.CreateDirectory(FileDirectory);
                    _steamWriter = File.AppendText($"{FileDirectory}{_logFileName}-{Guid.NewGuid().ToString()}.log");
                   
                }
            }
            catch (IOException e)
            {
                
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }


        }

        //method which will write the text in the log file
        public static void Write(string LogMessage)
        {

         _steamWriter.Write("{0} {1}", DateTime.Now.ToLongTimeString(),
              
                DateTime.Now.ToLongDateString());
            _steamWriter.WriteLine(" {0}", LogMessage);
            _steamWriter.Flush();
        }

    }
}
