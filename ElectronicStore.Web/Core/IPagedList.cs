using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Web.Core
{
    public interface IPagedList<T>
    {
        int PageSize { get; set; }

        int Skip { get; set; }

        int TotalResults { get; set; }

        int CurrentPage { get;}

        int TotalPages { get;}

        IEnumerable<T> Results { set; get; }
    }

    public class PagedList<T> : IPagedList<T>
    {
       
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int TotalResults { get; set; }

        public int CurrentPage
        {
            get { return (int)Math.Ceiling((Skip + 1) / (decimal)PageSize); }
        }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalResults / PageSize); }
        }

        public IEnumerable<T> Results { get; set; }
    }
}
