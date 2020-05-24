
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts
{

    /// <summary>
    /// Tu wkładamy konfigurację
    /// </summary>
    public class AppSettings {

        public string Secret { get; set; }

        public Mailing Mailing { get; set; }
    }

}
