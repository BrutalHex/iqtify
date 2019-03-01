using QTF.Domain.Entity.UserBundle;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Domain.Entity.QuestBundle
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
        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<QuestRecord> QuestRecords { get; set; }

        public virtual ICollection<QuestTask> Tasks { get; set; }
    }
}