using System.ComponentModel.DataAnnotations;

namespace Store.Contracts.ViewModel
{
    public class DeleteAccountViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "PasswordFormField")]
        public string Password { get; set; }
    }
}
