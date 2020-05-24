using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts.Authentication
{
    public class ProfileViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("roles")]
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
