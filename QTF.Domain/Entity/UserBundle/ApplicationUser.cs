using Microsoft.AspNetCore.Identity;
using QTF.Domain.Entity.AnswereBundle;
using QTF.Domain.Entity.QuestBundle;
using System;
using System.Collections.Generic;
 

namespace QTF.Domain.Entity.UserBundle
{
    public class ApplicationUser : IdentityUser
    {
        public long Experience { get; set; }
        public int Level { get; set; }

        public virtual ICollection<QuestRecord> QuestRecords { get; set; }
        public virtual ICollection<UserAnswer> QuestionAnswers { get; set; }

        public virtual ICollection<AddressEntity> AddressEntities { get; set; }

    }
}
