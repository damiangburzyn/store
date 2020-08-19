using System.Collections.Generic;


namespace Store.Contracts.ViewModel
{
    public class AcceptanceFormulaViewModel: BaseViewModel
    {
        public AcceptanceFormulaViewModel()
        {

        }
        public string JsonText { get; set; }
        public long AcceptanceTypeId { get; set; }
        public ICollection<AcceptanceViewModel> Acceptances { get; private set; } = new HashSet<AcceptanceViewModel>();

     
    }
}