using HealthcareApp.Api.Data;
using HealthcareApp.Api.Models.Entities;
using HealthcareApp.Api.Repositories.Interfaces;
using ClosedXML.Excel;

namespace HealthcareApp.Api.Repositories.Implementations
{
    public class ExcelRepository : IExcelRepository
    {
        private readonly HospitalDbContext _context;

        public ExcelRepository(HospitalDbContext dbContext)
        {
            _context = dbContext;
        }

        public List<ExcelData> ParseExcel(string filePath)
        {
            var results = new List<ExcelData>();

            // Mở workbook bằng ClosedXML
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // sheet đầu tiên
                var lastRow = worksheet.LastRowUsed();
                if (lastRow == null) return results; // Excel trống

                int rowCount = lastRow.RowNumber();

                for (int i = 2; i <= rowCount; i++) // giả sử row 1 là header
                {
                    var name = worksheet.Cell(i, 1).GetValue<string>() ?? string.Empty;
                    var value = worksheet.Cell(i, 2).GetValue<string>() ?? string.Empty;

                    var data = new ExcelData
                    {
                        Name = name,
                        Value = value
                    };
                    results.Add(data);
                }
            }

            return results;
        }

        public async Task SaveExcelDataAsync(List<ExcelData> datas)
        {
            if (datas == null || !datas.Any()) return;

            _context.ExcelDatas.AddRange(datas);
            await _context.SaveChangesAsync();
        }
    }
}
