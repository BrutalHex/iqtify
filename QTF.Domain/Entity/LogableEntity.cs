using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QTF.Domain.Entity
{
    public class LogableEntity<T>: BaseEntity<T>
    {

        [Column("CreateDate")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Column("UpdateDate")]
        public DateTime? UpdateDate { get; set; }


    }
}
