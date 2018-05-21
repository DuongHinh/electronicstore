using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Entities
{
    public class Brand
    {
        [Key]
        public int Id { set; get; }

        public string Name { set; get; }

        public string Alias { set; get; }

        public bool Status { set; get; }

        public string Description { set; get; }

        public string Logo { set; get; }

        public virtual IEnumerable<Product> Products { set; get; }
    }
}
