using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface IBrandRepositories : IRepositories<Brand>
    {

    }
    public class BrandRepositories : Repositories<Brand>, IBrandRepositories
    {
        public BrandRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
