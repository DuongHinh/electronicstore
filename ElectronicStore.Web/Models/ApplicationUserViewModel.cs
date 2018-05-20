using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class ApplicationUserViewModel
    {
        public string Id { set; get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }   

        public string MiddleName { get; set; }

        public string FullName
        {
            get {
                return !string.IsNullOrWhiteSpace(MiddleName) ?

                string.Format("{0} {1} {2}", LastName, MiddleName, FirstName) :

                string.Format("{0} {1}", LastName, FirstName);

            }
        }

        public DateTime? BirthDay { set; get; }

        public bool Active { get; set; }

        public string Email { set; get; }

        public string Password { set; get; }

        public string UserName { set; get; }

        public string PhoneNumber { set; get; }

        public string Address { set; get; }

        public IEnumerable<GroupViewModel> Groups { set; get; }
    }
}