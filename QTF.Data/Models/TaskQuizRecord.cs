using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Data.Models
{
    /// <summary>
    /// For the tasks of type Quiz - link to the specific Quiz.
    /// </summary>
    public class TaskQuizRecord
    {
        public int Id { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        [ForeignKey("QuestTask")]
        public int QuestTaskId { get; set; }
        public QtfTask QtfTask { get; set; }
    }
}