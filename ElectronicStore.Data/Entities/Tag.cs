using ElectronicStore.Data.Entities.Enum;
using ElectronicStore.Fulcrum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Entities
{
    public class Tag
    {
        [Key]
        public int Id { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string Alias { set; get; }

        [MaxLength(50)]
        [Required]
        public string Name { set; get; }

        [Required]
        public TagType Type { set; get; }
    }
}
