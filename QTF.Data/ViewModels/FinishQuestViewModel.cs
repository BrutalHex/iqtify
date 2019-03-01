using QTF.Data.Models;
using QTF.Domain.Entity.QuestBundle;

namespace QTF.ViewModels
{
    public class FinishQuestViewModel
    {
        public string Title { get; set; }

        public FinishQuestViewModel(Quest quest)
        {
            this.Title = quest.Title;
        }
    }
}