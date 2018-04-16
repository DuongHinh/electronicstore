using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using System.Collections.Generic;

namespace ElectronicStore.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory Product);

        void Update(ProductCategory Product);

        ProductCategory Delete(int id);

        IEnumerable<ProductCategory> GetAll();

        IEnumerable<ProductCategory> GetAll(string keyword);

        ProductCategory GetById(int id);

        void Save();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepositories productCategoryRepositories;
        private IUnitOfWork unitOfWork;

        public ProductCategoryService(IProductCategoryRepositories productCategoryRepositories, IUnitOfWork unitOfWork)
        {
            this.productCategoryRepositories = productCategoryRepositories;
            this.unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory ProductCategory)
        {
            var productCategory = this.productCategoryRepositories.Add(ProductCategory);
            this.unitOfWork.Save();
            return productCategory;
        }

        public ProductCategory Delete(int id)
        {
            return this.productCategoryRepositories.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return this.productCategoryRepositories.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return this.productCategoryRepositories.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return this.productCategoryRepositories.GetAll();
        }

        public ProductCategory GetById(int id)
        {
            return this.productCategoryRepositories.GetSingleById(id);
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }

        public void Update(ProductCategory ProductCategory)
        {
            this.productCategoryRepositories.Update(ProductCategory);
        }
    }
}