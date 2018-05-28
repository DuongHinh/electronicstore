using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class NewsCategoryViewModel
    {
        public int Id { set; get; }

     
        public string Name { set; get; }

   
        public string Alias { set; get; }

    
        public string Description { set; get; }

        public int? ParentId { set; get; }

        
        public string Image { set; get; }

        public DateTime? CreatedDate { set; get; }

    
        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

    
        public string UpdatedBy { set; get; }

        public bool Status { set; get; }

        public virtual IEnumerable<NewsViewModel> News { set; get; }
    }
}