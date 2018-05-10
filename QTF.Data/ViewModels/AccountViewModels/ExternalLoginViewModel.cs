using System.ComponentModel.DataAnnotations;

namespace QTF.Data.ViewModels.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
