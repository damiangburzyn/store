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

        [Required]
        [Editable(true)]
        [Display(Name = "RoleFormField")]
        public  override string Role { get; set; }
       
        [Display(Name = "RegionFormField")]
        public string Region { get; set; }

        [Range(typeof(bool), "true", "true")]
        public bool AcceptTermsOfUse { get; set; }
        [Range(typeof(bool), "true", "true")]
        public bool AcceptEmailAdd { get; set; }
        public bool AcceptDataProcess { get; set; }
        public bool AcceptDataProcess2 { get; set; }
        public bool AcceptEmailReceiving { get; set; }
        public bool AcceptEmailMarketing { get; set; }

        public string AcceptTermsOfUseText { get; set; }
        public string AcceptEmailAddText { get; set; }
        public string AcceptDataProcessText { get; set; }
        public string AcceptDataProcess2Text { get; set; }
        public string AcceptEmailReceivingText { get; set; }
        public string AcceptEmailMarketingText { get; set; }

       
    }
}
