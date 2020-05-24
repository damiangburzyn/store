using Store.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.Entities
{
    public class Acceptance : BaseEntity
    {
        /// <summary>
        /// Constructor for creating new acceptance with all reqiured properties
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <param name="acceptanceFormulaId"></param>
        /// <param name="state"></param>
        /// <param name="ip"></param>
        public Acceptance(long applicationUserId, long acceptanceFormulaId, bool state, string ip)
        {
            ApplicationUserId = applicationUserId;
            AcceptanceFormulaId = acceptanceFormulaId;
            State = state;
            Ip = ip;
        }

        public bool State { get; set; }

        public string Ip { get; set; }

        public long ApplicationUserId { get; set; }

        public long AcceptanceFormulaId { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual AcceptanceFormula AcceptanceFormula { get; set; }
      
    }
}