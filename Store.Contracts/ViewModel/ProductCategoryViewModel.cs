namespace Store.Contracts.ViewModel
{
    public class ProductCategoryViewModel : BaseViewModel
    {
        public long CategoryId { get; set; }

        public long ProductId { get; set; }

        public virtual CategoryViewModel Category { get; set; }

        public virtual ProductViewModel Product { get; set; }
    }
}