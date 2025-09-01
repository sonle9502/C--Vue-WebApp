namespace HealthcareApp.Api.Models.Entities
{
    public class ExcelData
    {
        public int Id { get; set; }   // Khóa chính

        public string Name { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        // Nếu bạn có quan hệ với bảng khác (ví dụ Xét nghiệm, Ảnh...) có thể thêm:
        // public int LabTestId { get; set; }
        // public LabTest LabTest { get; set; }
    }
}
