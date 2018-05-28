using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class NewsViewModel
    {
        public int Id { set; get; }

        public string Title { set; get; }

        public string Alias { set; get; }

        public int CategoryId { set; get; }

        public string Image { set; get; }

        public string Description { set; get; }

        public int? ViewCount { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public bool Status { set; get; }

        public virtual NewsCategoryViewModel NewsCategory { set; get; }
    }
}