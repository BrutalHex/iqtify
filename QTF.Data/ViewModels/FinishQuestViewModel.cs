using QTF.Data.Models;

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