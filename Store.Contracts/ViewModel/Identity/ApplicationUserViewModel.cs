using System;
using System.Collections.Generic;


namespace Store.Contracts.ViewModel
{
    public class ApplicationUserViewModel : BaseViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string Position { get; set; }

        public string Category { get; set; }

        public string Region { get; set; }

        public bool AcceptTermsOfUse { get; set; }
        public bool AcceptEmailAdd { get; set; }
        public bool AcceptDataProcess { get; set; }
        public bool AcceptDataProcess2 { get; set; }
        public bool AcceptEmailReceiving { get; set; }
        public bool AcceptEmailMarketing { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

       // public ECulture Culture { get; set; }

        public EUserStatus Status { get; set; }

        public bool EmailConfirmed { get; set; }

        public string StatusDescription => Status.GetDescription();

        public void SetRoles(List<string> roles)
        {
            roles.ForEach(r =>
            {
                var role = (ERole)Enum.Parse(typeof(ERole), r);
                Roles.Add(new KeyValuePair<ERole, string>(role, r));
                RoleNames.Add(r);
            });
        }

       // public string CreationDateUtcDescription => CreationDateUtc.ToUniversalTime().GetStandardDateTimeFormat();

        public DateTime? RemovalDateUtc { get; set; }

        public IDictionary<ERole, string> Roles { get; private set; } = new Dictionary<ERole, string>();

        public ICollection<string> RoleNames { get; } = new List<string>();

        public ICollection<ApplicationRoleViewModel> RoleObjects { get; set; }

        public string RolesDescription => string.Join(", ", Roles.Values);

        public string LastLoginDateUtcDescription { get; set; }
    }
}