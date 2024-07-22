using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagementApplication.Data;
using TaskManagementApplication.Models;
using TaskManagementApplication.Services;

namespace TaskManagementApplication.Controllers
{
    public class TaskSummaryController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly TaskManagementApplicationContext _context;

        public TaskSummaryController(ITaskService taskService, TaskManagementApplicationContext context)
        {
            _taskService = taskService;
            _context = context;
        }
        public IActionResult Index()
        {
            //ViewBag.Employees = new SelectList(_context.User.ToList(), "Id", "Name");
            ViewBag.Teams=new SelectList(_context.Team.ToList(), "Id", "Name");
            return View("TaskSummary");
        }
        [HttpPost]
        public IActionResult FilterTasks(TaskFilterCriteria criteria)
        {
            // ViewBag.Employees = new SelectList(_context.User.ToList(), "Id", "Name");
            ViewBag.Teams = new SelectList(_context.Team.ToList(), "Id", "Name");
            var tasks = _taskService.GetTasks(criteria);
            return View("FilterTasks", tasks);
        }

        public  IActionResult GetUsersByTeam(int teamId)
        {
            var users = _context.User.Where(x=>x.Id==teamId);
            return Json(users);

        }
    }
        
}
