using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QTF.Data.Models;
using QTF.Web.Data;
using QTF.Data.Models.HomeViewModels;
using QTF.Web.Helpers;

namespace QTF.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IActionResult Index()
        {
            var quizes = _context.Quizes.Include(q => q.Questions);
            return View(quizes);
        }

        public IActionResult Question(int quizId)
        {
            var quiz = _context.Quizes
                .Include(q => q.Questions)
                .ThenInclude(qw => qw.Answers)
                .SingleOrDefault(row => row.Id == quizId);
            if (quiz.Questions == null || quiz.Questions.Count() == 0)
            {
                return RedirectToAction(nameof(Error));
            }
            var firstQuestion = quiz.Questions.First();
            var answers =
                firstQuestion.Answers.Select(a => a.Value).ToArray();
            answers.Shuffle();
            var vm = new QuestionViewModel
            {
                Title = firstQuestion.Content,
                IsLastQuestion = quiz.Questions.Count() == 1,
                Answers = answers,
                QuizId = quizId,
                CorrectAnswers = 0,
                CurrentQuestion = 0
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Question(QuestionViewModel model)
        {
            Quiz quiz = null;
            if (!ModelState.IsValid)
            {
                quiz = _context.Quizes
                                .Include(q => q.Questions)
                                .ThenInclude(qw => qw.Answers)
                                .SingleOrDefault(row => row.Id == model.QuizId);
                if (quiz.Questions == null || quiz.Questions.Count() == 0)
                {
                    return RedirectToAction(nameof(Error));
                }
                var question = quiz.Questions.ElementAt(model.CurrentQuestion);
                var lAnswers = question.Answers.Select(a => a.Value).ToArray();
                lAnswers.Shuffle();
                var lVm = new QuestionViewModel
                {
                    Title = question.Content,
                    IsLastQuestion = quiz.Questions.Count() == 1,
                    Answers = lAnswers,
                    QuizId = model.QuizId,
                    CorrectAnswers = model.CorrectAnswers,
                    CurrentQuestion = model.CurrentQuestion
                };
                return View(lVm);
            }
            quiz = _context.Quizes
                            .Include(q => q.Questions)
                            .ThenInclude(qw => qw.Answers)
                            .SingleOrDefault(row => row.Id == model.QuizId);
            if (quiz.Questions == null || quiz.Questions.Count() == 0)
            {
                return RedirectToAction(nameof(Error));
            }
            var submittedQuestion = quiz.Questions.ElementAt(model.CurrentQuestion);
            model.CorrectAnswers += RateAnswer(model.SubmitedAnswers, submittedQuestion.Answers);

            if (quiz.Questions.Count() <= model.CurrentQuestion + 1)
            {
                return RedirectToAction(nameof(QuizResult),
                    new { score = $"{model.CorrectAnswers:F2} / {quiz.Questions.Count()}" });
            }
            var nextQuestion = quiz.Questions.ElementAt(model.CurrentQuestion + 1);
            var answers = nextQuestion.Answers.Select(a => a.Value ?? "").ToArray();
            answers.Shuffle();
            var vm = new QuestionViewModel
            {
                Title = nextQuestion.Content,
                IsLastQuestion = quiz.Questions.Count() == model.CurrentQuestion + 2,
                Answers = answers,
                CorrectAnswers = model.CorrectAnswers,
                CurrentQuestion = model.CurrentQuestion + 1,
                QuizId = model.QuizId
            };

            return View(vm);
        }
        
        public IActionResult Quiz(int id)
        {
            var quiz = _context.Quizes
                .Include(q => q.Questions)
                .ThenInclude(qw => qw.Answers)
                .SingleOrDefault(row => row.Id == id);
            if (quiz == null)
            {
                return RedirectToAction(nameof(Error));
            }

            QuizViewModel model = new QuizViewModel
            {
                Id = quiz.Id,
                Title = quiz.Description,
                HasQuestions = quiz.Questions == null ? false : quiz.Questions.Count() > 0
            };
            return View(model);
        }

        public IActionResult QuizResult(string score)
        {
            return View((object)score);
        }

        [Authorize]
        public IActionResult CreateQuiz()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateQuiz(CreateQuizViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Quiz newQuiz = new Quiz
            {
                Name = model.Title,
                Description = model.Description
            };
            _context.Quizes.Add(newQuiz);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult AddQuestion(int quizId)
        {
            var quiz = _context.Quizes
                        .Include(q => q.Questions)
                        .ThenInclude(qw => qw.Answers)
                        .SingleOrDefault(row => row.Id == quizId);
            if (quiz == null)
            {
                return RedirectToAction(nameof(Error));
            }
            string quizName = quiz.Name;
            var newModel = new CreateQuestionViewModel
            {
                QuizId = quizId,
                QuizName = quizName,
                Answers = new List<AnswerViewModel>
                {
                    new AnswerViewModel(),
                    new AnswerViewModel(),
                    new AnswerViewModel()
                }
            };
            return View(newModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddQuestion(CreateQuestionViewModel question)
        {
            if (!ModelState.IsValid)
            {
                return View(question);
            }

            if (question.Answers.Where(ans => ans.Correct == true).Count() == 0)
            {
                ModelState.AddModelError("", "At least one answer should be coorect!");
                return View(question);
            }

            var answers = new List<QuestionAnswer>();
            foreach (var answer in question.Answers)
            {
                answers.Add(new QuestionAnswer { Value = answer.Text, IsCorrect = answer.Correct });
            };
            var newQuestion = new Question
            {
                Content = question.Title,
                QuizId = question.QuizId,
                Answers = answers
            };
            var quiz = _context.Quizes
                    .Include(q => q.Questions)
                    .ThenInclude(qw => qw.Answers)
                    .SingleOrDefault(row => row.Id == question.QuizId);
            if (quiz.Questions == null)
            {
                quiz.Questions = new List<Question>();
            }
            _context.Questions.Add(newQuestion);
            foreach (var answer in newQuestion.Answers)
            {
                _context.Answers.Add(answer);
                newQuestion.Answers.Append(answer);
            }
            quiz.Questions.Append(newQuestion);
            _context.SaveChanges();

            return RedirectToAction(nameof(AddQuestion), new { quizId = question.QuizId });
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private float RateAnswer(IEnumerable<string> submittedAnswers, ICollection<QuestionAnswer> realAnswers)
        {
            float correctAnswersCount = 0;
            foreach (var sa in submittedAnswers)
            {
                if (realAnswers.FirstOrDefault(ans => ans.Value == sa && ans.IsCorrect == true) != null)
                {
                    correctAnswersCount += 1;
                }
            };
            float correctAnswersOverall = realAnswers.Where(a => a.IsCorrect).ToArray().Count();
            float correctness = 0;
            if (correctAnswersOverall != 0)
            {
                correctness = correctAnswersCount / correctAnswersOverall;
            }
            float chance = 0;
            int submittedAnsversCount = submittedAnswers.Count();
            if (submittedAnsversCount != 0)
            {
                chance = correctAnswersCount / submittedAnsversCount;
            }
            return correctness * chance;
        }
    }
}
