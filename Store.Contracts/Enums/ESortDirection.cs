using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Store.Data.EF.Enums
{
    public enum ESortDirection
    {
        [Description("asc")]
        Asc = 1,

        [Description("desc")]
        Desc = 2
    }
}
