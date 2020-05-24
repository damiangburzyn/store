using System;
using System.Collections.Generic;
using System.Text;

namespace GC5.Application.ViewModels.Identity
{
    public class TokenViewModel
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
