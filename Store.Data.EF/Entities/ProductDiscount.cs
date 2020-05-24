using System.Net.Sockets;

namespace Store.Data.EF.Entities
{
    public class ProductDiscount: BaseEntity
    {
        public long ProductId { get; set; }

        public long DiscountId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Discount Discount { get; set; }
    }
}