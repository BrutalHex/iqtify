using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Resources = QTF.Resource;

namespace QTF.Dtos.UserBundle
{
    public class AddressDto
    {
        public int Key { get; set; }

        /// <summary>
        /// postal code of user address
        /// </summary>
        /// <remarks>
        /// its consist of one part defined like : 4 digits as ####
        /// </remarks>
        /// <example>
        /// 1452
        /// </example>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Messages))]
        [RegularExpression(@"^[0-9]{4}", ErrorMessageResourceName = "ValidationRegularExpression", ErrorMessageResourceType = typeof(Resources.Messages))]
        [Display(Name = "PostalCode", ResourceType = typeof(Resources.Entity))]
        public string PostalCode { get; set; }

        /// <summary>
        ///   the text that includes the address it self
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Messages))]
        [RegularExpression(@"^[a-zA-Z0-9\s\,\(\)\-]{1,500}", ErrorMessageResourceName = "ValidationRegularExpression", ErrorMessageResourceType = typeof(Resources.Messages))]
        [Display(Name = "Path", ResourceType = typeof(Resources.Entity))]
        public string Path { get; set; }

        public string UserKey { get; set; }
    }

    public class GetAddressDto: AddressDto
    {
        public string  Username { get; set; }
    }

       


}
