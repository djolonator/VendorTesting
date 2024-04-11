using ClosedXML.Excel;
using System.ComponentModel;
using System.Reflection;
using static VendorTesting.Models.Models;

namespace VendorTesting.Service
{
    public static class XLSXFactoryClosedXML
    {
        public static void CreateDocument(TestResult testResult, TimeSpan ts)
        {
            var dateTimeCurrent = DateTime.Now.ToString("dd-MMM-HH_mm_ss_tt");
            var filePath = $"C:\\Users\\Djordje\\Desktop\\Rezultat-Testiranja-{dateTimeCurrent}.xlsx";
            SaveFile(filePath, testResult, ts);
        }

        private static void SaveFile(string filePath, TestResult testResult, TimeSpan ts)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var workbook = new XLWorkbook())
            {
                var passedCount = testResult.TestPassed.Count;
                var failedCount = testResult.TestFailed.Count;

                // Summary worksheet
                var workSheetSummary = workbook.Worksheets.Add("Rezime");
                workSheetSummary.Cell("E2").Value = "Vreme izvrsenja";
                workSheetSummary.Column(5).Style.Font.Bold = true;
                workSheetSummary.Cell("F2").Value = ts.ToString();
                workSheetSummary.Cell("E3").Value = "Prosli";
                workSheetSummary.Cell("F3").Value = passedCount.ToString();
                workSheetSummary.Cell("E4").Value = "Pali";
                workSheetSummary.Cell("F4").Value = failedCount.ToString();
                workSheetSummary.Columns().AdjustToContents();

                // Passed worksheet
                var workSheetPass = workbook.Worksheets.Add("Prosli");
                CreateTestSheet(workSheetPass);
                LoadDataIntoWorksheet(workSheetPass, testResult.TestPassed);

                // Failed worksheet
                var workSheetFailed = workbook.Worksheets.Add("Pali");
                CreateTestSheet(workSheetFailed);
                LoadDataIntoWorksheet(workSheetFailed, testResult.TestFailed);

                workbook.SaveAs(filePath);
            }
        }

        public static IXLWorksheet CreateTestSheet(IXLWorksheet worksheet)
        {
            worksheet.Cell("A3").Value = "Vendor info";
            worksheet.Range("A3:C3").Merge();
            worksheet.Range("A3:C3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range("A3:C3").Style.Font.Bold = true;
            worksheet.Range("A3:C3").Style.Font.FontSize = 16;
            worksheet.Range("A4:J4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range("A4:J4").Style.Font.Bold = true;
            worksheet.Range("A4:J4").Style.Font.FontSize = 13;

            return worksheet;
        }

        public static void LoadDataIntoWorksheet(IXLWorksheet workSheet, List<ExcelModel> testResults)
        {
            var headers = new List<string>();
            var properties = typeof(ExcelModel).GetProperties();
            foreach (var property in properties)
            {
                var descriptionAttribute = property.GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttribute != null)
                {
                    headers.Add(descriptionAttribute.Description);
                }
            }

            for (int i = 0; i < headers.Count; i++)
            {
                workSheet.Cell(4, i + 1).Value = headers[i];
            }

            for (int i = 0; i < testResults.Count; i++)
            {
                var rowIndex = i + 5; 
                var testResult = testResults[i];
                for (int j = 0; j < headers.Count; j++)
                {
                    var property = properties[j];
                    var value = property.GetValue(testResult);
                    workSheet.Cell(rowIndex, j + 1).Value = value?.ToString() ?? string.Empty;
                }
            }

            workSheet.Columns().AdjustToContents();
        }
    }
}
