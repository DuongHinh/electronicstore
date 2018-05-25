using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface INewsRepositories : IRepositories<News>
    {

    }
    public class NewsRepositories : Repositories<News>, INewsRepositories
    {
        public NewsRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
