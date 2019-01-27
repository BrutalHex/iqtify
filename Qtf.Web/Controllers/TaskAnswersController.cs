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
    public class TaskAnswersController : Controller
    {
        private readonly QtfDbContext _context;

        public TaskAnswersController(QtfDbContext context)
        {
            _context = context;
        }

        // GET: TaskAnswers
        public async Task<IActionResult> Index()
        {
            var qtfDbContext = _context.Answers.Include(t => t.QuestTask);
            return View(await qtfDbContext.ToListAsync());
        }

        // GET: TaskAnswers
        public async Task<IActionResult> ForQuestTask(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = _context.QuestTasks.Find(id);
            if(task == null)
            {
                return NotFound();
            }

            var qtfDbContext = _context.Answers
                .Include(t => t.QuestTask)
                .Where(t => t.QuestTaskId == id);

            return View(await qtfDbContext.ToListAsync());
        }

        // GET: TaskAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskAnswer = await _context.Answers
                .Include(t => t.QuestTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskAnswer == null)
            {
                return NotFound();
            }

            return View(taskAnswer);
        }

        // GET: TaskAnswers/Create
        public IActionResult Create()
        {
            ViewData["QuestTaskId"] = new SelectList(_context.QuestTasks, "Id", "InternalName");
            return View();
        }

        // POST: TaskAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,QuestTaskId,IsCorrect,CustomReaction")] TaskAnswer taskAnswer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestTaskId"] = new SelectList(_context.QuestTasks, "Id", "InternalName", taskAnswer.QuestTaskId);
            return View(taskAnswer);
        }

        // GET: TaskAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskAnswer = await _context.Answers.FindAsync(id);
            if (taskAnswer == null)
            {
                return NotFound();
            }
            ViewData["QuestTaskId"] = new SelectList(_context.QuestTasks, "Id", "InternalName", taskAnswer.QuestTaskId);
            return View(taskAnswer);
        }

        // POST: TaskAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,QuestTaskId,IsCorrect,CustomReaction")] TaskAnswer taskAnswer)
        {
            if (id != taskAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskAnswerExists(taskAnswer.Id))
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
            ViewData["QuestTaskId"] = new SelectList(_context.QuestTasks, "Id", "InternalName", taskAnswer.QuestTaskId);
            return View(taskAnswer);
        }

        // GET: TaskAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskAnswer = await _context.Answers
                .Include(t => t.QuestTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskAnswer == null)
            {
                return NotFound();
            }

            return View(taskAnswer);
        }

        // POST: TaskAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskAnswer = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(taskAnswer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskAnswerExists(int id)
        {
            return _context.Answers.Any(e => e.Id == id);
        }
    }
}
