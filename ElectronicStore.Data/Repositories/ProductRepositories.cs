using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ElectronicStore.Data.Repositories
{
    public interface IProductRepositories : IRepositories<Product>
    {
    }
    public class ProductRepositories : Repositories<Product>, IProductRepositories
    {
        public ProductRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
