using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QTF.Data.Models.HomeViewModels
{
    public class CreateQuestionViewModel
    {
        [Required]
        public int QuizId { get; set; }

        public string QuizName { get; set; }

        [Required]
        public string Title { get; set; }

        public List<AnswerViewModel> Answers { get; set; }
    }
}
