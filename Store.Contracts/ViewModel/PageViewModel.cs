using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts.ViewModel
{
    public class PageViewModel : BaseViewModel
    {
        public string PageName { get; set; }
        public  string Title { get; set; }
        public  string Description { get; set; }
        public  string Keys { get; set; }
    }
}
