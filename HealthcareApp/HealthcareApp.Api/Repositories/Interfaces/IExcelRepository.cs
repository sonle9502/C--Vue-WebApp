using HealthcareApp.Api.Models.Entities;

namespace HealthcareApp.Api.Repositories.Interfaces
{
    public interface IExcelRepository
    {
        List<ExcelData> ParseExcel(string filePath);
        Task SaveExcelDataAsync(List<ExcelData> datas);
    }
}
