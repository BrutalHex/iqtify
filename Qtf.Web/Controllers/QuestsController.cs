using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QTF.Data;
using QTF.Data.Models;
using QTF.Data.ViewModels;
using QTF.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QTF.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestsController : Controller
    {
        private readonly QtfDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestsController(QtfDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Quests
        public async Task<IActionResult> Index()
        {
            var qtfDbContext = _context.Quests.Include(q => q.Creator);
            return View(await qtfDbContext.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Start(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests
                .FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }

            var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            if (userId == null)
            {
                return Redirect("~/identity/account/username");
            }

            var questRecord = await _context.QuestRecords
                .SingleOrDefaultAsync(r => r.QuestId == id && r.UserId == userId);

            if (questRecord == null)
            {
                var firstTask = _context.QuestTasks
                    .Include(_ => _.Answers)
                    .SingleOrDefault(_ => _.Order == 1);
                if (firstTask != null)
                {
                    questRecord = new QuestRecord()
                    {
                        CurrentTask = firstTask.Id,
                        UserId = userId,
                        QuestId = quest.Id,
                        State = QuestState.Started
                    };

                    await _context.QuestRecords.AddAsync(questRecord);
                    await _context.SaveChangesAsync();
                    return View(nameof(Play), new TaskViewModel(firstTask));
                }
            }
            else if (questRecord.State == QuestState.Finished)
            {
                return RedirectToAction(nameof(Finish), new { id = quest.Id });
            }
            else
            {
                var task = await _context.QuestTasks
                    .Include(_ => _.Answers)
                    .SingleOrDefaultAsync(t => t.Id == questRecord.CurrentTask);
                if (task != null)
                {
                    return View(nameof(Play), new TaskViewModel(task));
                }
            }

            //todo: define behavior if no first task
            return View(nameof(Details), quest);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Play(int? taskId, string[] answers)
        {
            //string[] answers = (string[])Request.Form("answers");
            if (taskId == null)
            {
                return NotFound();
            }

            var task = await _context.QuestTasks
                .Include(t => t.Answers)
                .SingleOrDefaultAsync(t => t.Id == taskId);
            if (task == null)
            {
                return NotFound();
            }

            var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            if (userId == null)
            {
                return Redirect("~/identity/account/username");
            }

            var questRecord = await _context.QuestRecords
                .SingleOrDefaultAsync(r => r.QuestId == task.QuestId && r.UserId == userId);

            if (questRecord == null)
            {
                return NotFound();
            }

            var userAnswers = answers.ToList();
            var taskAnswers = task.Answers.Select(a => a.Value).ToList();

            foreach (var item in answers)
            {
                var userAnswer = new UserAnswer
                {
                    UserId = userId,
                    DateTime = DateTime.UtcNow,
                    QuestTaskId = taskId,
                    Value = item,
                    IsCorrect = taskAnswers.Contains(item)
                };

                _context.Add(userAnswer);
                await _context.SaveChangesAsync();
            }

            var tasksLeft = _context.QuestTasks
                .Where(t => t.Order > task.Order && t.QuestId == task.QuestId);
            if (!tasksLeft.Any())
            {
                return RedirectToAction(nameof(Finish), new { id = task.QuestId });
            }

            var minOrder = tasksLeft.Min(t => t.Order);
            var nextTask = tasksLeft.Single(t => t.Order == minOrder);
            var nextTaskInstance = await _context.QuestTasks
                .Include(t => t.Answers)
                .SingleOrDefaultAsync(t => t.Id == nextTask.Id);

            return View(new TaskViewModel(nextTaskInstance));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Finish(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests
                .Include(q => q.Tasks)
                .SingleOrDefaultAsync(q => q.Id == id);
            if (quest == null)
            {
                return NotFound();
            }

            var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            if (userId == null)
            {
                return Redirect("~/identity/account/username");
            }

            var questRecord = await _context.QuestRecords
                .SingleOrDefaultAsync(r => r.QuestId == id && r.UserId == userId);

            if (questRecord == null)
            {
                return NotFound();
            }

            questRecord.State = QuestState.Finished;
            await _context.SaveChangesAsync();

            return View(new FinishQuestViewModel(quest));
        }

        // GET: Quests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests
                .Include(q => q.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // GET: Quests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,InternalName,Description")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                quest.CreatorId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
                _context.Add(quest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(quest);
        }

        // GET: Quests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // POST: Quests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,InternalName,Description")] Quest quest)
        {
            if (id != quest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestExists(quest.Id))
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

            return View(quest);
        }

        // GET: Quests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests
                .Include(q => q.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // POST: Quests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quest = await _context.Quests.FindAsync(id);
            _context.Quests.Remove(quest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestExists(int id)
        {
            return _context.Quests.Any(e => e.Id == id);
        }
    }
}
