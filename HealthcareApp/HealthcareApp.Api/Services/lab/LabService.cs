using HealthcareApp.Api.Models.Entities.lab;
using HealthcareApp.Api.Repositories.lab;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.IO.Compression;
using OfficeOpenXml;

namespace HealthcareApp.Api.Services.lab
{
    public class LabService : ILabService
    {
        private readonly ILabRepository _repo;

        public LabService(ILabRepository repo)
        {
            _repo = repo;
        }

        public async Task ProcessZipAsync(IFormFile zipFile)
        {
            // 1. Lưu file zip vào thư mục uploads
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, zipFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await zipFile.CopyToAsync(stream);
            }

            // 2. Giải nén vào thư mục tạm, bỏ qua folder gốc trong ZIP
            var extractPath = Path.Combine(uploadPath, Path.GetFileNameWithoutExtension(zipFile.FileName));
            if (Directory.Exists(extractPath))
                Directory.Delete(extractPath, true); // Xóa nếu đã tồn tại
            Directory.CreateDirectory(extractPath);

            using (var archive = ZipFile.OpenRead(filePath))
            {
                foreach (var entry in archive.Entries)
                {
                    // Bỏ folder gốc, chỉ lấy tên file
                    var fileName = Path.GetFileName(entry.FullName);
                    if (string.IsNullOrEmpty(fileName)) continue; // skip folder

                    var destinationPath = Path.Combine(extractPath, fileName);
                    entry.ExtractToFile(destinationPath, true);
                }
            }

            // 3. Đọc tất cả file Excel (.xlsx) trong thư mục vừa giải nén
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // EPPlus license
            var files = Directory.GetFiles(extractPath, "*.xlsx", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                using var package = new ExcelPackage(new FileInfo(file));
                var worksheet = package.Workbook.Worksheets[0]; // sheet đầu tiên
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // bỏ header
                {
                    var patientIdText = worksheet.Cells[row, 1].Text;
                    var resultText = worksheet.Cells[row, 2].Text;
                    var testNameText = worksheet.Cells[row, 3].Text;

                    if (string.IsNullOrWhiteSpace(patientIdText) || string.IsNullOrWhiteSpace(resultText))
                        continue;

                    var labResult = new LabResult
                    {
                        PatientId = int.Parse(patientIdText),
                        Result = resultText,
                        TestName = testNameText,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _repo.SaveLabResultAsync(labResult);
                }
            }
        }
        public async Task<List<LabResult>> GetLabResultsAsync()
        {
            return await _repo.GetResultsAsync();
        }
    }

    public interface ILabService
    {
        Task ProcessZipAsync(IFormFile zipFile);
        Task<List<LabResult>> GetLabResultsAsync();
    }
}
