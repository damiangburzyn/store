using System.Collections.Generic;
using Newtonsoft.Json;

namespace Store.Contracts.ViewModel
{
    public class DataTableSearchViewModel<T>
    {
        public int Total { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("last_page")]
        public int LastPage { get; set; }

        public int From { get; set; }

        public int To { get; set; }

        [JsonProperty("next_page_url")]
        public string NextPageUrl { get; set; }

        [JsonProperty("prev_page_url")]
        public string PrevPageUrl { get; set; }

        public ICollection<T> Data { get; set; }
    }
}
