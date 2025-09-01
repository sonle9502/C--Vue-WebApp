using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.Api.Models.Entities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        [MaxLength(50)]
        public string Gender { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int HospitalId { get; set; }
    }
}
