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
            return View(await _context.QtfTasks.ToListAsync());
        }

        public async Task<IActionResult> Index()
        {
            VerifyRole();
            return View(await _context.QtfTasks.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qtfTask = await _context.QtfTasks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (qtfTask == null)
            {
                return NotFound();
            }

            return View(qtfTask);
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
        public async Task<IActionResult> Create([Bind("Id,Name,InternalName,Description,TaskType")] QtfTask qtfTask)
        {
            VerifyRole();
            if (ModelState.IsValid)
            {
                _context.Add(qtfTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(qtfTask);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            VerifyRole();
            if (id == null)
            {
                return NotFound();
            }

            var qtfTask = await _context.QtfTasks.SingleOrDefaultAsync(m => m.Id == id);
            if (qtfTask == null)
            {
                return NotFound();
            }

            ViewData["TaskTypes"] = new SelectList(Enum.GetValues(typeof(TaskType)).Cast<TaskType>());
            return View(qtfTask);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InternalName,Description,TaskType")] QtfTask qtfTask)
        {
            VerifyRole();
            if (id != qtfTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qtfTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QtfTaskExists(qtfTask.Id))
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
            return View(qtfTask);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            VerifyRole();
            if (id == null)
            {
                return NotFound();
            }

            var qtfTask = await _context.QtfTasks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (qtfTask == null)
            {
                return NotFound();
            }

            return View(qtfTask);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            VerifyRole();
            var qtfTask = await _context.QtfTasks.SingleOrDefaultAsync(m => m.Id == id);
            _context.QtfTasks.Remove(qtfTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QtfTaskExists(int id)
        {
            return _context.QtfTasks.Any(e => e.Id == id);
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
