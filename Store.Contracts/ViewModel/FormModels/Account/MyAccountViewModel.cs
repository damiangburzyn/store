using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Store.Contracts.ViewModel
{
    public class MyAccountViewModel
    {
        public MyAccountViewModel()
        {

        }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }

        public string Phone { get; set; }

    }
}
