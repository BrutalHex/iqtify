using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Data.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Content { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Possible answers to this question. Icluding correct and incorrect.
        /// </summary>
        public ICollection<QuestionAnswer> Answers { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }
    }

    public enum QuestionType
    {
        Text,
        SingleChoice,
        MultipleChoice
    }
}