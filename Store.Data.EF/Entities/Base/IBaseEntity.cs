using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.Entities
{
  public  interface IBaseEntity
    {
        long Id { get; set; }
        DateTime CreateDate { get; set; }
        DateTime ModifyDate { get; set; }
    }
}
