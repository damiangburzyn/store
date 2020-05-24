using System.ComponentModel.DataAnnotations;

namespace Store.Contracts.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "PasswordFormField")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "NewPasswordFormField")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "ConfirmPasswordFormField")]
        public string ConfirmNewPassword { get; set; }
    }
}
