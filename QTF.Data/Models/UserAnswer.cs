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

        public AnswerType AnswerType { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Question")]
        public int? QuestionId { get; set; }
        public Question Question { get; set; }

        [ForeignKey("QtfTask")]
        public int? QtfTaskId { get; set; }
        public QtfTask QtfTask { get; set; }

        public string Value { get; set; }
        public DateTime DateTime { get; set; }
    }

    public enum AnswerType
    {
        Question,
        Task
    }
}