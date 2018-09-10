namespace QTF.Data.Models
{
    /// <summary>
    /// Specific task of the quest. Each quest could have few tasks.
    /// </summary>
    public class QtfTask
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string InternalName { get; set; }

        public string Description { get; set; }

        public TaskType TaskType { get; set; }

        /// <summary>
        /// Link to the quiz, if the task is Quiz type
        /// </summary>
        public virtual TaskQuizRecord TaskQuizRecord { get; set; }
    }

    public enum TaskType
    {
        /// <summary>
        /// Task has only questions inside.
        /// </summary>
        Quiz,

        /// <summary>
        /// There is a specific code to check the result of the task
        /// </summary>
        CustomCode,

        /// <summary>
        /// Any answer to this task is correct - just need to submit something
        /// </summary>
        AnswerNotEmpty
    }
}