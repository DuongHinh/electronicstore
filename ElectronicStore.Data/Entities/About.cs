using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Data.Entities
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { set; get; }
    }
}