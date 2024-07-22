using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Models;
using TaskManagementApplication.Data;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskManagementApplicationContext _context;

        public TaskService(TaskManagementApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Tasks> GetTasks(TaskFilterCriteria criteria)
        {
            var query = _context.Tasks.ToList() ;

            if (criteria.FromDate.HasValue)
            {
                query = query.Where(t => t.DueDate >= criteria.FromDate.Value).ToList();
            }

            if (criteria.ToDate.HasValue)
            {
                query = query.Where(t => t.DueDate <= criteria.ToDate.Value).ToList();
            }

            if (criteria.EmployeeId.HasValue)
            {
                query = query.Where(t => t.AssignedToId == criteria.EmployeeId.Value).ToList();
            }

            query = query.Where(t => t.IsCompleted==criteria.IsCompleted).ToList();

            return query;
        }

       
    }

}
