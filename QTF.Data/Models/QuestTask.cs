using System.Collections.Generic;

namespace QTF.Data.Models
{
    /// <summary>
    /// Specific task of the quest. Each quest coudl have few tasks.
    /// </summary>
    public class QuestTask
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public TaskType TaskType { get; set; }

        public virtual TaskQuizRecord TaskQuizRecord { get; set; }
    }

    public enum TaskType
    {
        /// <summary>
        /// Task has only questions inside.
        /// </summary>
        Quiz
    }
}