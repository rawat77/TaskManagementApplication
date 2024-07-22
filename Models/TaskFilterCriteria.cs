namespace TaskManagementApplication.Models
{
    public class TaskFilterCriteria
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? EmployeeId { get; set; }

        public int? TeamId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
