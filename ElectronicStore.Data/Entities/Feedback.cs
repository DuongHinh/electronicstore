using System;
using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Data.Entities
{
    public class Feedback
    {
        [Key]
        public int ID { set; get; }

        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(500)]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        public bool IsReaded { set; get; }
    }
}