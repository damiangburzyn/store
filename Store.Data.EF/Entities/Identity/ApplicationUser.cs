using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Store.Contracts;
using Store.Data.EF.Entities;

namespace Store.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser<long>, IBaseEntity
    {
        /// <summary>
        ///     Basic constructor
        /// </summary>
        public ApplicationUser()
        {
        }

        public ApplicationUser(string userName, string email,  EUserStatus status, bool emailConfirmed)
        {
            base.UserName = userName;
            base.Email = email;
            base.EmailConfirmed = emailConfirmed;

          
            Status = status;
        }


        public EUserStatus Status { get; set; }

        public DateTime? RemovalDateUtc { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public virtual ICollection<Acceptance> Acceptances { get; private set; } = new HashSet<Acceptance>();

        public virtual void Operate()
        {
        }

        public virtual void Validate()
        {
        }
    }
}