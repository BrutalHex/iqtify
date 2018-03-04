using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace QTF.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public long Experience { get; set; }
        public int Level { get; set; }

        public ICollection<QuestRecord> QuestRecords { get; set; }
        public ICollection<UserAnswer> QuestionAnswers { get; set; }
    }
}
