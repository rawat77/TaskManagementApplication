using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } // Admin, Manager, Employee
        [ForeignKey("TeamId")]
        public int TeamId { get; set; }
        public Team? Team { get; set; }
    }
}
