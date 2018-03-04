using System.Collections.Generic;

namespace QTF.Data.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public QuizType QuizType { get; set; }

        public virtual TaskQuizRecord TaskQuizRecord { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }

    public enum QuizType
    {
        /// <summary>
        /// Quiz exists independently, not connected to other entities
        /// </summary>
        Independent,

        /// <summary>
        /// Quiz is part of the task of some quest
        /// </summary>
        QuestTaskQuiz,
        
    }
}
