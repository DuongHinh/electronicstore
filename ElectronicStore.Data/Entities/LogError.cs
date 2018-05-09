using System;
using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Data.Entities
{
    public class LogError
    {
        [Key]
        public int Id { set; get; }

        public string Message { set; get; }

        public string StackTrace { set; get; }

        public DateTime Date { set; get; }
    }
}