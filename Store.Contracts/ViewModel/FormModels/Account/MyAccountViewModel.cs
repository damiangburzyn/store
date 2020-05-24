using System.ComponentModel.DataAnnotations;

namespace Store.Contracts.ViewModel
{
    public class MyAccountViewModel
    {
        public MyAccountViewModel()
        {

        }

        [Required]
        [Display(Name = "NameRegisterFormField")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "SurnameFormField")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "CompanyFormField")]
        public string Company { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "EmailFormField")]
        public virtual string Email { get; set; }

        [Display(Name = "PhoneFormField")]
        public long? Phone { get; set; }

        [Display(Name = "CompanyAddressRegisterFormField")]
        public string CompanyAddress { get; set; }

        [Required]
        [Display(Name = "RoleFormField")]
        public virtual string Role { get; set; }

        [Display(Name = "JobTitleRegisterFormField")]
        public string JobTitle { get; set; }
    }
}
