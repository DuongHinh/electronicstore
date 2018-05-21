using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? BirthDay { set; get; }

        [MaxLength(256)]
        public string Address { set; get; }

        public bool Active { get; set; }

        [MaxLength(128)]
        public string FirstName { get; set; }

        [MaxLength(128)]
        public string LastName { get; set; }

        [MaxLength(128)]
        public string MiddleName { get; set; }

        public string GetFullName()
        {
            return string.IsNullOrWhiteSpace(MiddleName) ?
                string.Format("{0} {1}", LastName, FirstName) :
                string.Format("{0} {1} {2}", LastName, MiddleName, FirstName);
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual IEnumerable<Order> Orders { set; get; }
    }
}