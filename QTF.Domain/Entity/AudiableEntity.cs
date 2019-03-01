using System;
using System.Collections.Generic;
using System.Text;

namespace QTF.Domain.Entity
{
    public class AuditableEntity<T,Y>: LogableEntity<T>
    {
        public Y CreatedUserKey { get; set; }

    }
}
