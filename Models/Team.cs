using System.ComponentModel.DataAnnotations;

namespace TaskManagementApplication.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<User>? Members { get; set; }
    }
}
