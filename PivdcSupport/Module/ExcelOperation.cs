using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PivdcSupportModule
{
    public static class ExcelOperation
    {
        public static string WriteGenralExcel(this DataTable dataTable, string fullFilePath)
        {
            try
            {
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }

                using (ExcelPackage objExcelPackage = new ExcelPackage())
                {
                    ExcelWorksheet objWorkSheet = objExcelPackage.Workbook.Worksheets.Add(dataTable.TableName);
                    objWorkSheet.Cells["A1"].LoadFromDataTable(dataTable, true, TableStyles.Custom);
                    objWorkSheet.Cells.AutoFitColumns();

                    FileStream objFileStream = File.Create(fullFilePath);
                    objFileStream.Close();
                    File.WriteAllBytes(fullFilePath, objExcelPackage.GetAsByteArray());
                }

                return fullFilePath;
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        public static DataTable ReadFromExcel(string filename, string sheetName)
        {
            DataTable dt = new DataTable();
            FileInfo existingFile = new FileInfo(filename);
            using (ExcelPackage xlPackage = new ExcelPackage(existingFile))
            {
                try
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName);
                    for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        // Adding Header For DataTable
                        if (row == 1)
                        {
                            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                            {
                                dt.Columns.Add("Column" + col.ToString());
                            }
                        }
                        DataRow dr = dt.NewRow();
                        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                        {
                            dr[col - 1] = worksheet.Cells[row, col].Value;
                        }
                        dt.Rows.Add(dr);
                    }
                }
                catch (Exception ex)
                {
                    ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
            return dt;
        }
    }
}