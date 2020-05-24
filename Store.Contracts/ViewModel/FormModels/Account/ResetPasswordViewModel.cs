using System.ComponentModel.DataAnnotations;

namespace GC5.Application.ViewModels.FormModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "EmailFormField")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "PasswordFormField")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "ConfirmPasswordFormField")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
