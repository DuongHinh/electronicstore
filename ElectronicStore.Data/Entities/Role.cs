using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Entities
{
    public class Role : IdentityRole
    {
        public Role() : base()
        {

        }
        [StringLength(256)]
        public string Description { set; get; }
    }
}
