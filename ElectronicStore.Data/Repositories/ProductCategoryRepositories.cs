using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;

namespace ElectronicStore.Data.Repositories
{
    public interface IProductCategoryRepositories : IRepositories<ProductCategory>
    {
    }

    public class ProductCategoryRepositories : Repositories<ProductCategory>, IProductCategoryRepositories
    {
        public ProductCategoryRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}