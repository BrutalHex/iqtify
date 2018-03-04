using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Data.Models
{
    public class TaskQuizRecord
    {
        public int Id { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        [ForeignKey("QuestTask")]
        public int QuestTaskId { get; set; }
        public QuestTask QuestTask { get; set; }
    }
}