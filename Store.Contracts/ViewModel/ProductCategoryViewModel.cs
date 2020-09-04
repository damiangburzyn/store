using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace Store.Contracts.ViewModel
{
    public class ProductCategoryViewModel : BaseViewModel
    {
        public long CategoryId { get; set; }
        public long ProductId { get; set; }
        public virtual CategoryViewModel Category { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ProductViewModel Product { get; set; }
    }
}