using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Entities
{
    public class NewsTag
    {
        [Key]
        [Column(Order = 1)]
        public int NewsId { set; get; }

        [Key]
        [Column(Order = 2)]
        public int TagId { set; get; }

        [ForeignKey("NewsId")]
        public virtual News News { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}
