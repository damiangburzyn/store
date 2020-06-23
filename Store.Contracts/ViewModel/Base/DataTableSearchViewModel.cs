using System.Collections.Generic;
using Newtonsoft.Json;

namespace Store.Contracts.ViewModel
{
    public class DataTableSearchViewModel<T>
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("perPage")]
        public int PerPage { get; set; }

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("lastPage")]
        public int LastPage { get; set; }
      
        [JsonProperty("from")]
        public int From { get; set; }
      
        [JsonProperty("to")]
        public int To { get; set; }

        [JsonProperty("nextPageUrl")]
        public string NextPageUrl { get; set; }

        [JsonProperty("prevPageUrl")]
        public string PrevPageUrl { get; set; }
        [JsonProperty("data")]
        public ICollection<T> Data { get; set; }
    }
}
