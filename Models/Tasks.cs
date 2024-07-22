using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace TaskManagementApplication.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int AssignedToId { get; set; }
        public User? AssignedTo { get; set; }
        [NotMapped]
        public string NewNoteContent { get; set; }
        public List<Note>? Notes { get; set; }
        public List<Document>? Documents { get; set; }
    }
}
