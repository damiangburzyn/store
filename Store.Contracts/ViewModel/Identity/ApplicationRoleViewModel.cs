

namespace Store.Contracts.ViewModel
{
    public class ApplicationRoleViewModel : BaseViewModel
    {
        public string Name { get; set; }    
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
  
    }
}