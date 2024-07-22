using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManagementApplication.Models;
using TaskManagementApplication.Data;

namespace TaskManagementApplication.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskManagementApplicationContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        public TasksController(TaskManagementApplicationContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            
            
            return View(await _context.Tasks.Include(t => t.AssignedTo).ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.AssignedTo)
                .FirstOrDefaultAsync(m => m.Id == id);
            tasks.Notes = await _context.Note.Where(x => x.TaskId == tasks.Id).ToListAsync();
            tasks.Documents = await _context.Document.Where(x => x.TaskId == tasks.Id).ToListAsync();
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_context.User.ToList(), "Id", "Name");
            ViewData["AssignedToId"] = new SelectList(_context.Set<User>(), "Id", "Name");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DueDate,IsCompleted,AssignedToId,Notes,Documents,NewNoteContent")] Tasks tasks, IFormFile? documentFile)
        {
            if (ModelState.IsValid)
            {
                Note note = new Note();
                note.Content = tasks.NewNoteContent;
                note.TaskId = tasks.Id; if (tasks.Notes is null)
                {
                    tasks.Notes = new List<Note>();
                    tasks.Notes.Add(note);
                }
                tasks.Notes.Add(note);
                using (var memoryStream = new MemoryStream())
                {
                    if (documentFile != null)
                    {
                        await documentFile.CopyToAsync(memoryStream);

                        if (tasks.Documents == null)
                        {
                            tasks.Documents = new List<Document>();
                        }
                        tasks.Documents.Add(new Document
                        {
                            FilePath = Path.GetFullPath(documentFile.FileName),

                            //Content = memoryStream.ToArray()
                        });
                    }
                    _context.Add(tasks);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(tasks);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            tasks.Notes = await _context.Note.Where(x => x.TaskId == tasks.Id).ToListAsync();
            tasks.Documents = await _context.Document.Where(x => x.TaskId == tasks.Id).ToListAsync();
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["AssignedToId"] = new SelectList(_context.Set<User>(), "Id", "Name", tasks.AssignedToId);
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DueDate,IsCompleted,AssignedToId,Notes,Documents,NewNoteContent")] Tasks tasks, IFormFile? documentFile)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }
            Note note=new Note();
            note.Content = tasks.NewNoteContent;
            note.TaskId=tasks.Id;
            if (tasks.Notes is null)
            {
                tasks.Notes = new List<Note>();
                tasks.Notes.Add(note);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        if (documentFile != null)
                        {
                            await documentFile.CopyToAsync(memoryStream);

                            if (tasks.Documents == null)
                            {
                                tasks.Documents = new List<Document>();
                            }
                            tasks.Documents.Add(new Document
                            {
                                FilePath = Path.GetFullPath(documentFile.FileName),

                                //Content = memoryStream.ToArray()
                            });
                        }
                    }
                    _context.Update(tasks);
                  
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //if (documentFile != null && documentFile.Length > 0)
                //{
                //    using (var memoryStream = new MemoryStream())
                //    {
                //        await documentFile.CopyToAsync(memoryStream);
                //        if (tasks.Documents == null)
                //        {
                //            tasks.Documents = new List<Document>();
                //        }
                //        tasks.Documents.Add(new Document
                //        {
                //            FilePath = Path.GetFullPath(documentFile.FileName),
                //            //Content = memoryStream.ToArray()
                //        });
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignedToId"] = new SelectList(_context.Set<User>(), "Id", "Id", tasks.AssignedToId);
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.AssignedTo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks != null)
            {
                _context.Tasks.Remove(tasks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
