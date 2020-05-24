

namespace Store.Contracts.ViewModel
{
    public class AcceptanceViewModel : BaseViewModel
    {
        public bool State { get; set; }
        public string Ip { get; set; }
        public long ApplicationUserId { get; set; }
        public long AcceptanceFormulaId { get; set; }
        public AcceptanceFormulaViewModel AcceptanceFormula{ get; set; }
    }
}