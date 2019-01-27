using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QTF.Data;
using QTF.Data.Models;

namespace QTF.Web.Obsolete.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestTasksController : Controller
    {
        private readonly QtfDbContext _context;

        public QuestTasksController(QtfDbContext context)
        {
            _context = context;
        }

        // GET: QuestTasks
        public async Task<IActionResult> Index()
        {
            var qtfDbContext = _context.QuestTasks.Include(q => q.Creator).Include(q => q.Quest);
            return View(await qtfDbContext.ToListAsync());
        }

        // GET: QuestTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questTask = await _context.QuestTasks
                .Include(q => q.Creator)
                .Include(q => q.Quest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questTask == null)
            {
                return NotFound();
            }

            return View(questTask);
        }

        // GET: QuestTasks/Create
        public IActionResult Create()
        {
            ViewData["QuestId"] = new SelectList(_context.Quests, "Id", "InternalName");
            ViewData["TaskType"] = new SelectList(Enum.GetValues(typeof(TaskType)), TaskType.MultipleChoiceAnswer);

            return View();
        }

        // POST: QuestTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,InternalName,Description,TaskType,QuestId,Order")] QuestTask questTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //User.Identity.
            ViewData["QuestId"] = new SelectList(_context.Quests, "Id", "InternalName", questTask.QuestId);
            ViewData["TaskType"] = new SelectList(Enum.GetValues(typeof(TaskType)), TaskType.MultipleChoiceAnswer);
            return View(questTask);
        }

        // GET: QuestTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questTask = await _context.QuestTasks.FindAsync(id);
            if (questTask == null)
            {
                return NotFound();
            }
            ViewData["QuestId"] = new SelectList(_context.Quests, "Id", "InternalName", questTask.QuestId);
            ViewData["TaskType"] = new SelectList(Enum.GetValues(typeof(TaskType)), TaskType.MultipleChoiceAnswer);
            return View(questTask);
        }

        // POST: QuestTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,InternalName,Description,TaskType,QuestId,CreatorId,Order")] QuestTask questTask)
        {
            if (id != questTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestTaskExists(questTask.Id))
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
            ViewData["QuestId"] = new SelectList(_context.Quests, "Id", "InternalName", questTask.QuestId);
            ViewData["TaskType"] = new SelectList(Enum.GetValues(typeof(TaskType)), TaskType.MultipleChoiceAnswer);
            return View(questTask);
        }

        // GET: QuestTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questTask = await _context.QuestTasks
                .Include(q => q.Creator)
                .Include(q => q.Quest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questTask == null)
            {
                return NotFound();
            }

            return View(questTask);
        }

        // POST: QuestTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questTask = await _context.QuestTasks.FindAsync(id);
            _context.QuestTasks.Remove(questTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestTaskExists(int id)
        {
            return _context.QuestTasks.Any(e => e.Id == id);
        }
    }
}
