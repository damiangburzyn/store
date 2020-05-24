using System.ComponentModel.DataAnnotations;

namespace Store.Contracts.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "EmailFormField")]
        public string Email { get; set; }
    }
}
