using QTF.Domain.Entity.QuestBundle;
using QTF.Domain.Entity.UserBundle;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Domain.Entity.AnswereBundle
{
    /// <summary>
    /// Stores the answer submitted by user to specific question or task
    /// </summary>
    public class UserAnswer
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("QuestTask")]
        public int? QuestTaskId { get; set; }
        public virtual QuestTask QuestTask { get; set; }

        public string Value { get; set; }
        public DateTime DateTime { get; set; }
        public bool? IsCorrect { get; set; }
    }
}