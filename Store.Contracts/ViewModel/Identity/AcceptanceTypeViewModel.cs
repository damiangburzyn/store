using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Contracts.ViewModel
{ 
   public class AcceptanceTypeViewModel: BaseViewModel
    {
        public string Name { get; set; }
        public virtual ICollection<AcceptanceFormulaViewModel> AcceptanceFormulas { get; private set; } = new HashSet<AcceptanceFormulaViewModel>();
    }
}
