﻿using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Data.Entities
{
    public class Contact
    {
        [Key]
        public int Id { set; get; }

        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(50)]
        public string PhoneNumber { set; get; }

        [StringLength(50)]
        public string Fax { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(250)]
        public string Address { set; get; }

        public string Other { set; get; }

        public bool Status { set; get; }
    }
}