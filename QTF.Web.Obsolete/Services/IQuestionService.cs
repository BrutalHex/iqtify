using QTF.Data.ViewModels.HomeViewModels;

namespace QTF.Web.Services
{
    public interface IQuestionService
    {
        QuestionViewModel Get(string userId);
        void Save(string userId, string answer);
    }
}