using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;

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