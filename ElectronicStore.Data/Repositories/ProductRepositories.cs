using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicStore.Data.Repositories
{
    public interface IProductRepositories : IRepositories<Product>
    {
        IEnumerable<Product> GetListProductByTag(int tagId, int page, int pageSize, out int totalRow);
    }

    public class ProductRepositories : Repositories<Product>, IProductRepositories
    {
        public ProductRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public IEnumerable<Product> GetListProductByTag(int tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Product
                        join pt in DbContext.ProductTag
                        on p.Id equals pt.ProductId
                        where pt.TagId == tagId
                        select p;
            totalRow = query.Count();

            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}