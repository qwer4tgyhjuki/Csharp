
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.V3.Models;

namespace WebApplication2.V3.Services
{
    public class TestVersionServiceV3 : ITestVersionServiceV3
    {
        public Task<ExcelModel> Get()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");
                worksheet.Cell("A1").Value = "Hello World!";
                worksheet.Cell("A2").Value = new Random().Next(1, 100);

                string valueA1 = worksheet.Cell("A1").Value.ToString();
                int valueA2 = worksheet.Cell("A2").GetValue<int>();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return Task.FromResult(new ExcelModel
                    {
                        Cell1 = valueA1,
                        Cell2 = valueA2
                    });
                }
            }
        }
    }
}
