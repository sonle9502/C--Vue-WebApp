using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Api.Models.Entities
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
