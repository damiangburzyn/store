using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.Entities
{
    public class AcceptanceFormula : BaseEntity
    {
        public string JsonText { get; set; }

        public long AcceptanceTypeId { get; set; }


        public virtual AcceptanceType AcceptanceType { get; set; }

        public virtual ICollection<Acceptance> Acceptances { get; private set; } = new HashSet<Acceptance>();
    }
}
