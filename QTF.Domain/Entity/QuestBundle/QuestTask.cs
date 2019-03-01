using QTF.Domain.Entity.AnswereBundle;
using QTF.Domain.Entity.UserBundle;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Domain.Entity.QuestBundle
{
    /// <summary>
    /// Specific task of the quest. Each quest could have few tasks.
    /// </summary>
    public class QuestTask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string InternalName { get; set; }

        public string Description { get; set; }

        public TaskType TaskType { get; set; }

        [ForeignKey("Quest")]
        public int? QuestId { get; set; }
        public virtual Quest Quest { get; set; }

        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }

        /// <summary>
        /// If the quest has few tasks - they can be ordered. 1->2->3->..
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Possible answers to this question. Icluding correct and incorrect.
        /// </summary>
        public virtual ICollection<TaskAnswer> Answers { get; set; }

        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }

    public enum TaskType
    {
        /// <summary>
        /// Task has only question inside.
        /// </summary>
        TextQuestion,

        /// <summary>
        /// There is a specific code to check the result of the task
        /// </summary>
        CustomCode,

        SingleChoiceAnswer,

        MultipleChoiceAnswer
    }
}