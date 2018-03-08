using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QTF.Data.Models.HomeViewModels
{
    public class QuestionViewModel
    {
        public int QuizId { get; set; }
        public float CorrectAnswers { get; set; }
        public int CurrentQuestion { get; set; }
        public string Title { get; set; }
        public ICollection<string> Answers { get; set; }
        public bool IsLastQuestion { get; set; }

        [Required(ErrorMessage = "Please chose the answer!")]
        public ICollection<string> SubmitedAnswers { get; set; }
    }
}
