using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Store.Contracts.ViewModel
{
    public class RegisterUserViewModel :MyAccountViewModel
    {
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        [JsonProperty("password")]
        public string Password { get; set; }

        //[Display(Name = "RepeatPasswordRegisterFormField")]
        public string RepeatPassword { get; set; }

        [Required]
        [EmailAddress]
        [Editable(true)]
        [JsonProperty("email")]
        public override string Email { get; set; }


        [Range(typeof(bool), "true", "true")]

        [JsonProperty("acceptTermsOfUse")]
        public bool AcceptTermsOfUse { get; set; }

       
    }
}
