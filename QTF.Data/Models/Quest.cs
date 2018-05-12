using System.Collections.Generic;

namespace QTF.Data.Models
{
    public class Quest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<QuestRecord> QuestRecords { get; set; }

        public ICollection<QtfTask> Tasks { get; set; }
    }
}