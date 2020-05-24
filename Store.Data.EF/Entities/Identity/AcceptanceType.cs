using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.Entities
{
    public class AcceptanceType : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<AcceptanceFormula> AcceptanceFormulas { get; private set; } = new HashSet<AcceptanceFormula>();
    }
}
