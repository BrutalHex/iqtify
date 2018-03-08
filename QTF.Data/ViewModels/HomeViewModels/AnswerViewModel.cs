using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QTF.Data.Models.HomeViewModels
{
    public class AnswerViewModel
    {
        [Required(ErrorMessage = "Answer text is required")]
        public string Text { get; set; }

        public bool Correct { get; set; }
    }
}
