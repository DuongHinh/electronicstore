using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<RoleViewModel> Roles { set; get; }
    }
}