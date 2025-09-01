using HealthcareApp.Api.Models.Entities;
using HealthcareApp.Api.Models.Entities.lab;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Api.Data
{
    // Kế thừa IdentityDbContext với ApplicationUser
    public class HospitalDbContext : IdentityDbContext<User>
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Hospital> Hospitals { get; set; } = null!;
        public DbSet<ExcelData> ExcelDatas { get; set; }
        public DbSet<LabResult> LabResults { get; set; }

        // Nếu muốn, có thể override OnModelCreating để cấu hình thêm
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Ví dụ: cấu hình tên bảng riêng cho Identity
            builder.Entity<User>(b =>
            {
                b.ToTable("Users");
            });
        }
    }
}
