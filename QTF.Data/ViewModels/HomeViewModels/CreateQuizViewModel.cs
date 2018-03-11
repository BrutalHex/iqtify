using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QTF.Data.Models.HomeViewModels
{
    public class CreateQuizViewModel
    {
        [Required]
        public string Title { get; set; }

        public IEnumerable<CreateQuestionViewModel> Questions { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
