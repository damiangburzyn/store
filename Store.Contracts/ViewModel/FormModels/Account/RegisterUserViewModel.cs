using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Store.Contracts.ViewModel
{
    public class RegisterUserViewModel :MyAccountViewModel
    {
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        [Display(Name = "PasswordFormField")]
        public string Password { get; set; }

        [Display(Name = "RepeatPasswordRegisterFormField")]
        public string RepeatPassword { get; set; }

        [Required]
        [EmailAddress]
        [Editable(true)]
        [Display(Name = "EmailFormField")]
        public override string Email { get; set; }


        [Range(typeof(bool), "true", "true")]
        public bool AcceptTermsOfUse { get; set; }

       
    }
}
