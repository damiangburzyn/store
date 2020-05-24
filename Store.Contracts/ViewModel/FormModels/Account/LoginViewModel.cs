using System.ComponentModel.DataAnnotations;

namespace Store.Contracts.ViewModel

{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "EmailFormField")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordFormField")]
        public string Password { get; set; }

        [Display(Name = "RememberMeFormField")]
        public bool RememberMe { get; set; }
    }
}
