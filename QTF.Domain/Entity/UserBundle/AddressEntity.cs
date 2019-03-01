using System;
using System.Collections.Generic;
using System.Text;

namespace QTF.Domain.Entity.UserBundle
{
    /// <summary>
    /// holds information about user address.
    /// </summary>
    public class AddressEntity: LogableEntity<int>
    {
        /// <summary>
        /// postal code of user address
        /// </summary>
        /// <remarks>
        /// its consist of one part defined like : 4 digits as ####
        /// </remarks>
        /// <example>
        /// 1452
        /// </example>
        public string PostalCode { get; set; }

        /// <summary>
        ///   the text that includes the address it self
        /// </summary>
        public string Path { get; set; }

      


        public string UserKey { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
