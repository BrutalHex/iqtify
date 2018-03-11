using System;
using System.Collections.Generic;
using System.Text;

namespace QTF.Data.Models.HomeViewModels
{
    public class QuizViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool HasQuestions { get; set; }
    }
}
