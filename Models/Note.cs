namespace TaskManagementApplication.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int TaskId { get; set; }
        public Tasks? Task { get; set; }
    }
}
