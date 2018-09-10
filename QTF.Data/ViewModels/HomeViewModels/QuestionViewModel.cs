using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QTF.Data.ViewModels.HomeViewModels
{
    public class QuestionViewModel
    {
        public int QuizId { get; set; }
        public float CorrectAnswers { get; set; }
        public int CurrentQuestion { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<string> Answers { get; set; }
        public bool IsLastQuestion { get; set; }
        public int Progress { get; set; }

        [Required(ErrorMessage = "Please chose the answer!")]
        public ICollection<string> SubmitedAnswers { get; set; }
    }
}
