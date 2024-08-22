using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zelisCompassFrameWorkLib.zelisHelpers
{
    public static class FileHelper
    {
        public static Boolean moveFile (string sourcePath, string filename)
        {            
            /*string fileName = "test.txt";
            string sourcePath = @"C:\Source Folder";
            string targetPath = @"D:\Destination Folder";*/
            string destinationPath = @"C:\AutomationTestResult\NSSolution\Result\" + DateTime.Now.ToString("dd_MM_yyyy");
            string sourceFile = Path.Combine(sourcePath, filename);
            string destFile = Path.Combine(destinationPath, filename);
            if (!File.Exists(sourceFile))
            {
                return false;
            }

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            File.Move(sourceFile, destFile);
            return true;
        }

        public static void extractFile(string testCaseName, string columnNanme)
        {
            try
            {
                List<string> getLoadTestData = ExcelHelper.loadTestData(testCaseName, columnNanme);
                foreach (string getZipFileName in getLoadTestData)
                {
                    string downloadFile = ConfigurationManager.AppSettings["DowloadedFilePath"] + getZipFileName;
                    string extractFilePath = ConfigurationManager.AppSettings["extractFilePath"];
                    ZipFile.ExtractToDirectory(downloadFile, extractFilePath);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static long countlines(string filePath)
        {
            //Read a file and counts the number of line in that file
            var lineCounter = 0L;
            using (var reader = new StreamReader(filePath))
            {
                while (reader.ReadLine() != null)
                {
                    lineCounter++;
                }
            }
            return lineCounter;

        }

        public static void deleteAllFile(string fileDirectory)
        {
            string[] files = Directory.GetFiles(fileDirectory);
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

       public static int[] generateRandomArray(int lineCount, int arraySize)
        {
            int min = 0;
            int max = lineCount;
            int size = arraySize;
            Random randNum = new Random();

            int[] numberRecords = Enumerable
                .Repeat(0, arraySize)
                .Select(i => randNum.Next(min,max))
                .ToArray();

            return numberRecords;

        }
            

    }
}
