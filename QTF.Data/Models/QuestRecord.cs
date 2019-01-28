using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Data.Models
{
    /// <summary>
    /// User record of the quest. Created when user starting a new quest.
    /// </summary>
    public class QuestRecord
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public QuestState State { get; set; }

        [ForeignKey("Quest")]
        public int QuestId { get; set; }
        public Quest Quest { get; set; }

        public int CurrentTask { get; set; }
    }

    public enum QuestState
    {
        Started,
        Finished
    }
}