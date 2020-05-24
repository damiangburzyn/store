using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.Entities
{
    public class Discount : BaseEntity
    {
        public int PercentageDiscount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<ProductDiscount> ProductDiscounts {get;set;}
    }
}
