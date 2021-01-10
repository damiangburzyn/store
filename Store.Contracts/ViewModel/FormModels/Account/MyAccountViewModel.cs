using System.ComponentModel.DataAnnotations;

namespace Store.Contracts.ViewModel
{
    public class MyAccountViewModel
    {
        public MyAccountViewModel()
        {

        }

        [Required] 
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }

        public string Phone { get; set; }

    }
}
