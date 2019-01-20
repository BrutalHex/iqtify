using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Data.Models
{
    /// <summary>
    /// Container of Tasks, Questions or other steps of user's interaction.
    /// </summary>
    public class Quest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string InternalName { get; set; }

        public string Description { get; set; }

        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public ICollection<QuestRecord> QuestRecords { get; set; }

        public ICollection<QuestTask> Tasks { get; set; }
    }
}