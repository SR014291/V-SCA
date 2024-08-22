using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelDataReader;
using System.Configuration;

namespace zelisCompassFrameWorkLib.zelisHelpers
{/*Testing*/
    public class ExcelHelper
    {
        private static List<DataCollection> _dataCol = new List<DataCollection>();
        /// <summary>
        /// Storing all the excel values in to the in-memory collections
        /// </summary>
        /// <param name="fileName"></param>
        public static void PopulateInCollection(string fileName)
        {
            DataTable table = ExcelToDataTable(fileName);
            //Iterate through the rows and columns of the Table
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    DataCollection dtTable = new DataCollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    //Add all the details for each row
                    _dataCol.Add(dtTable);
                }
            }
        }
        private static DataTable ExcelToDataTable(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var Result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    /*Get All tables*/
                    DataTableCollection table = Result.Tables;
                    /*Store it in DataTable*/
                    DataTable resultTable = table["TestData"];
                    /*return*/
                    return resultTable;

                }
            }
        }
        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations
                string data = (from colData in _dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();
                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
                return data.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static void cleanContentsFromExcelHelper()
        {
            _dataCol.Clear();
        }

        public static List<string> loadTestData(string testCaseName, string ColumnName)
        {
            List<string> getData = new List<string>();
            string data = readTestData(testCaseName, ColumnName);
            if (!string.IsNullOrEmpty(data))
            {
                getData = splitTestData(data);
            }
            return getData;
        }

        public static string readTestData(string testCaseName, string ColumnName)
        {
            string testData = null;
            //string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            // Update needed
            String excelPath = ConfigurationManager.AppSettings["FileCompareTestDataPath"];

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(excelPath);
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets["TestData"];
            int iColumn = 0;
            for (iColumn = 1; iColumn <= xlWorkSheet.UsedRange.Columns.Count; iColumn++)
            {
                string getColumnName = xlWorkSheet.Cells[1, iColumn].value;
                if (ColumnName.Equals(getColumnName))
                {
                    break;
                }
            }

            for (int iRow = 2; iRow <= xlWorkSheet.UsedRange.Rows.Count; iRow++)
            {
                string getTestcaseName = xlWorkSheet.Cells[iRow, 1].value;
                if (testCaseName.Equals(getTestcaseName))
                {
                    if (!string.IsNullOrEmpty(xlWorkSheet.Cells[iRow, iColumn].value))
                    {
                        testData = xlWorkSheet.Cells[iRow, iColumn].value;
                        break;
                    }
                }
            }
            xlWorkBook.Close();
            xlApp.Quit();
            return testData;
        }

        public static List<string> readSheetTestData(string SheetName, string ColumnName)
        {
            List<string> testData = new List<string>();
            string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            String excelPath = projectPath + "zelisPriZem\\xlsxTestDate\\priZemTestData.xlsx";
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(excelPath);
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets[SheetName];
            int iColumn = 0;
            for (iColumn = 1; iColumn <= xlWorkSheet.UsedRange.Columns.Count; iColumn++)
            {
                string getColumnName = xlWorkSheet.Cells[1, iColumn].value;
                if (ColumnName.Equals(getColumnName))
                {
                    break;
                }
            }

            for (int iRow = 2; iRow <= xlWorkSheet.UsedRange.Rows.Count; iRow++)
            {
                if (!string.IsNullOrEmpty(xlWorkSheet.Cells[iRow, iColumn].value))
                {
                    testData.Add(xlWorkSheet.Cells[iRow, iColumn].value).ToString();
                    Console.WriteLine(iRow);
                }
            }
            xlWorkBook.Close();
            xlApp.Quit();
            return testData;
        }

        public static List<string> splitTestData(string data)
        {
             List<string> getDataList = new List<string>();
            if(data.IndexOf(",") != -1)
            {
                string[] splitDatas = data.Split(',');
                foreach(string getData in splitDatas)
                {
                    getDataList.Add(getData);
                }
            }
            else
            {
                getDataList.Add(data);
            }
            return getDataList;
        }

        public static void updateTergetClaimID(string testCaseName, string tergetClaimID)
        {
            string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            String excelPath = projectPath + "zelisPriZem\\xlsxTestDate\\priZemTestData.xlsx";
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(excelPath);
            try
            {
                string tergetClaimIDColName = "TergetClaimID";
                Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets["TestData"];
                int TCIDColumn = 0;
                for (int iColumn = 1; iColumn <= xlWorkSheet.UsedRange.Columns.Count; iColumn++)
                {
                    string getColumnName = xlWorkSheet.Cells[1, iColumn].value;
                    if (getColumnName.Contains(tergetClaimIDColName))
                    {
                        TCIDColumn = iColumn;
                        break;
                    }
                }

                for (int iRow = 2; iRow <= xlWorkSheet.UsedRange.Rows.Count; iRow++)
                {
                    string getTestcaseName = xlWorkSheet.Cells[iRow, 1].value;
                    if (testCaseName.Equals(getTestcaseName))
                    {
                        if (string.IsNullOrEmpty(xlWorkSheet.Cells[iRow, TCIDColumn].value) && !string.IsNullOrEmpty(tergetClaimID))
                        {
                            xlWorkSheet.Cells[iRow, TCIDColumn].value = "'" + tergetClaimID;
                            break;
                        }
                        else if (!string.IsNullOrEmpty(xlWorkSheet.Cells[iRow, TCIDColumn].value) && !string.IsNullOrEmpty(tergetClaimID))
                        {
                            string getPreviousCID = xlWorkSheet.Cells[iRow, TCIDColumn].value;
                            if (!getPreviousCID.Contains(tergetClaimID))
                            {
                                xlWorkSheet.Cells[iRow, TCIDColumn].value = getPreviousCID + ", " + tergetClaimID;
                                break;
                            }
                        }
                    }
                }
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
            }
            catch (Exception e)
            {
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
                Console.WriteLine(e.Message);
                Debug.Write(e.Message);
            }
        }

    }

}
    public class DataCollection
    {
        public int rowNumber { get; set; }
        public string colName { get; set; }
        public string colValue { get; set; }
    }


