using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace GC5.Application.ViewModels.FormModels
{
    public class ContactFormViewModel
    {

        public ContactFormViewModel()
        {

        }



        [Required]
        [Display(Name = "NameAndSurnameFormField")]
        public string NameAndSurname { get; set; }

        [Display(Name = "CompanyFormField")]
        public string Company { get; set; }

        [Display(Name = "RegionFormField")]
        public string Region { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "EmailFormField")]
        public string Email { get; set; }

        [Display(Name = "SubjectFormField")]
        public string Subject { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        [Display(Name = "MessageFormField")]
        public string Message { get; set; }

        [Range(typeof(bool), "true", "true")]
        [Display(Name = "DataInfoFormField")]
        public bool IsDataInfo { get; set; }

        public IFormFile Attachment { set; get; }



    }
}
