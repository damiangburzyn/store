using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts.ViewModel
{
    public class ContentViewModel
    {
        public ContentViewModel()
        {

        }
        [MediaFile(relatedProperty: "Data")]
        public string Name { get; set; }
        public string Url { get; set; }
        [Media]
        public string Data { get; set; }
    }
}
