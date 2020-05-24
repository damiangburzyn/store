using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Store.Contracts
{
    public enum ERole
    {
        [Description("User")]
        User = 1,

        [Description("Administrator")]
        Administrator = 2,
    }
}
