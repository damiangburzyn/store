using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
