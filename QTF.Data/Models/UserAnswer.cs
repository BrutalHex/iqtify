using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Data.Models
{
    /// <summary>
    /// Stores the answer submitted by user to specific question or task
    /// </summary>
    public class UserAnswer
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("QuestTask")]
        public int? QuestTaskId { get; set; }
        public QuestTask QuestTask { get; set; }

        public string Value { get; set; }
        public DateTime DateTime { get; set; }
        public bool? IsCorrect { get; set; }
    }
}