using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Store.Contracts
{
    public enum EUserStatus
    {
        [Description("Blocked")]
        Blocked = 1,

        [Description("Active")]
        Active = 2,

        [Description("Deleted")]
        Deleted = 3
    }
}
