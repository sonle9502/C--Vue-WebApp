using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareApp.Api.Models.Entities.lab
{
    [Table("LabResults")]
    public class LabResult
    {
        [Key]
        public int Id { get; set; }

        // liên kết với bệnh nhân
        [Required]
        public int PatientId { get; set; }

        // giá trị kết quả (có thể text hoặc numeric)
        [Required]
        [MaxLength(500)]
        public string Result { get; set; }

        // tên xét nghiệm (nếu cần)
        [MaxLength(200)]
        public string TestName { get; set; }

        // ngày tạo / ngày nhập
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // navigation property
        public Patient Patient { get; set; }
    }
}
