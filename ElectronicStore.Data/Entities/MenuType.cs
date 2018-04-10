using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Entities
{
    public class MenuType
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { set; get; }

        [MaxLength(256)]
        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        [MaxLength(256)]
        public string UpdatedBy { set; get; }

        public bool Status { set; get; }

        public virtual IEnumerable<Menu> Menus { set; get; }

    }
}
