using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface IProductTagRepositories : IRepositories<ProductTag>
    {
    }
    public class ProductTagRepositories : Repositories<ProductTag>, IProductTagRepositories
    {
        public ProductTagRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
