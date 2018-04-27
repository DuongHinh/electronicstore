using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using System.Collections.Generic;

namespace ElectronicStore.Service
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product Product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);

        Product GetById(int id);

        void Save();
    }

    public class ProductService : IProductService
    {
        private IProductRepositories productRepositories;
        private IUnitOfWork unitOfWork;
        private ITagRepositories tagRepositories;
        private IProductTagRepositories productTagRepositories;

        public ProductService(IProductRepositories productRepositories, IUnitOfWork unitOfWork, ITagRepositories tagRepositories, IProductTagRepositories productTagRepositories)
        {
            this.productRepositories = productRepositories;
            this.unitOfWork = unitOfWork;
            this.tagRepositories = tagRepositories;
            this.productRepositories = productRepositories;
        }

        public Product Add(Product Product)
        {
            var product = this.productRepositories.Add(Product);
            this.unitOfWork.Save();
            return product;
        }

        public Product Delete(int id)
        {
            return this.productRepositories.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return this.productRepositories.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return this.productRepositories.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return this.productRepositories.GetAll();
        }

        public Product GetById(int id)
        {
            return this.productRepositories.GetSingleById(id);
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }

        public void Update(Product Product)
        {
            this.productRepositories.Update(Product);
        }
    }
}