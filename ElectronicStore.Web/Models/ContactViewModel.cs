using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class ContactViewModel
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string PhoneNumber { set; get; }

        public string Fax { set; get; }

        public string Email { set; get; }

        public string Address { set; get; }

        public string Other { set; get; }

        public bool Status { set; get; }
    }
}