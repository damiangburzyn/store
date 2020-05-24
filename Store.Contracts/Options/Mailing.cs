using System.Collections.Generic;

namespace Store.Contracts
{ 
    public class Mailing
    {
        public string DevelopmentRecipient { get; set; }

        public string SenderDomain { get; set; }

        public Dictionary<string, string> Senders { get; set; }


/// <summary>
/// smpt
/// </summary>
        public string MailHost { get; set; }

        public int MailPort { get; set; }

        public bool UseSsl { get; set; }

        public bool IsDisabled { get; set; }
        public bool IsAuthenticationDisabled { get; set; }
    }
}