namespace TaskManagementApplication.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int TaskId { get; set; }
        public Tasks Task { get; set; }
    }
}
