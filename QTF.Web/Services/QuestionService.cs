using System.Collections.Generic;
using QTF.Data;
using QTF.Data.ViewModels.HomeViewModels;

namespace QTF.Web.Services
{
    public class QuestionService : IQuestionService
    {
        private QtfDbContext _db;
        private static Dictionary<string, int> progress = new Dictionary<string, int>();

        private static Dictionary<int, string> questions = new Dictionary<int, string>()
        {
            {0, "hey, how are you?"},
            {1, "okaaaay"},
            {2, "good bye"}
        };

        public QuestionService(QtfDbContext dbContext)
        {
            _db = dbContext;
        }
        public QuestionViewModel Get(string userId)
        {
            if(!progress.TryGetValue(userId, out var number))
            {
                progress.Add(userId, 0);
            };

            return new QuestionViewModel
            {
                Title = questions[number>2?2:number],
                Content = "Something sdfds kfhasdkhf 8fp o 43hf",
                Progress = number * 100 / 3
            };
        }

        public void Save(string userId, string answer)
        {
            progress[userId]++;
        }
    }
}