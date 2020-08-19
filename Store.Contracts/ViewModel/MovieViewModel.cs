using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Contracts.ViewModel
{
    public class MovieViewModel : BaseViewModel
    {
        public string Url { get; set; }
        public virtual String Title { get; set; }
        public string Image { get; set; }
    }
}
