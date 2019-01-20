using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Data.Models
{
    /// <summary>
    /// Pre-defined answers to the tasks in the system
    /// </summary>
    public class QuestTaskAnswer
    {
        public int Id { get; set; }
        public string Value { get; set; }

        [ForeignKey("QuestTask")]
        public int QuestTaskId { get; set; }
        public QuestTask QuestTask { get; set; }

        public bool? IsCorrect { get; set; }

        public string CustomReaction { get; set; }
    }
}