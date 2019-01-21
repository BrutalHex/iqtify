using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QTF.Data;
using QTF.Data.Models;

namespace QTF.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly QtfDbContext _context;

        public TasksController(QtfDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> My()
        {
            //TODO: filter - do not show alraedy done
            return View(await _context.QuestTasks.ToListAsync());
        }

        public async Task<IActionResult> Index()
        {
            VerifyRole();
            return View(await _context.QuestTasks.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var QuestTask = await _context.QuestTasks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (QuestTask == null)
            {
                return NotFound();
            }

            return View(QuestTask);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            VerifyRole();
            ViewData["TaskTypes"] = new SelectList(Enum.GetValues(typeof(TaskType)).Cast<TaskType>());

            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InternalName,Description,TaskType")] QuestTask QuestTask)
        {
            VerifyRole();
            if (ModelState.IsValid)
            {
                _context.Add(QuestTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(QuestTask);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            VerifyRole();
            if (id == null)
            {
                return NotFound();
            }

            var QuestTask = await _context.QuestTasks.SingleOrDefaultAsync(m => m.Id == id);
            if (QuestTask == null)
            {
                return NotFound();
            }

            ViewData["TaskTypes"] = new SelectList(Enum.GetValues(typeof(TaskType)).Cast<TaskType>());
            return View(QuestTask);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InternalName,Description,TaskType")] QuestTask QuestTask)
        {
            VerifyRole();
            if (id != QuestTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(QuestTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestTaskExists(QuestTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(QuestTask);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            VerifyRole();
            if (id == null)
            {
                return NotFound();
            }

            var QuestTask = await _context.QuestTasks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (QuestTask == null)
            {
                return NotFound();
            }

            return View(QuestTask);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            VerifyRole();
            var QuestTask = await _context.QuestTasks.SingleOrDefaultAsync(m => m.Id == id);
            _context.QuestTasks.Remove(QuestTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestTaskExists(int id)
        {
            return _context.QuestTasks.Any(e => e.Id == id);
        }

        private void VerifyRole()
        {
            // temporary hack
            //TODO: change to roles
#if !DEBUG
            throw new Exception("Not allowed");
#endif
        }
    }
}
