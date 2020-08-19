using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Data.EF.Entities
{
    public class Movie : BaseEntity
    {
        public string Url { get; set; }
        public virtual String Title { get; set; }
        public string Image { get; set; }
    }
}
