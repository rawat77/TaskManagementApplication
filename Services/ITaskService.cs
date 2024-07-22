using TaskManagementApplication.Models;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Services
{
    public interface ITaskService
    {
        IEnumerable<Tasks> GetTasks(TaskFilterCriteria criteria);
        // Other methods...
    }
}
