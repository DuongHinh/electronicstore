using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using ElectronicStore.Fulcrum;
using System.Collections.Generic;
using System;
using System.Linq;
using ElectronicStore.Data.Entities.Enum;

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

        IEnumerable<Product> GetNewArrival(int top);

        IEnumerable<Product> GetHotProduct(int top);

        IEnumerable<Product> GetListProductByCategoryId(int categoryId, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<string> GetListNameProduct(string name);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> GetReatedProducts(int id, int top);

        void IncreaseView(int id);

        IEnumerable<Product> GetListProductByBrandId(int brandId, int page, int pageSize, string sort, out int totalRow);

        void SellProduct(int productId, int quantity);

        IEnumerable<Product> GetListProduct(string keyword);

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
            this.productTagRepositories = productTagRepositories;
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

        public IEnumerable<Product> GetNewArrival(int count)
        {
            return this.productRepositories.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(count);
        }

        public IEnumerable<Product> GetHotProduct(int count)
        {
            return this.productRepositories.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(count);
        }

        public IEnumerable<Product> GetListProductByCategoryId(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = this.productRepositories.GetMulti(x => x.Status && x.CategoryId == categoryId);

            switch (sort)
            {
                case "view_count":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "price_asc":
                    query = query.OrderBy(x => x.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListNameProduct(string name)
        {
            return this.productRepositories.GetMulti(x => x.Status && x.Name.Contains(name)).Select(y => y.Name);
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = this.productRepositories.GetMulti(x => x.Status && x.Name.Contains(keyword));

            switch (sort)
            {
                case "view_count":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "price_asc":
                    query = query.OrderBy(x => x.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetReatedProducts(int id, int count)
        {
            var product = this.productRepositories.GetSingleById(id);
            return this.productRepositories.GetMulti(x => x.Status && x.Id != id && x.CategoryId == product.CategoryId).OrderByDescending(x => x.CreatedDate).Take(count);
        }

        public IEnumerable<Product> GetListProductByBrandId(int branId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = this.productRepositories.GetMulti(x => x.Status && x.BrandId == branId);

            switch (sort)
            {
                case "view_count":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "price_asc":
                    query = query.OrderBy(x => x.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void SellProduct(int productId, int quantity)
        {
            var product = this.productRepositories.GetSingleById(productId);
            if (product.Quantity < quantity)
                product.Quantity = 0;
            else
            {
                product.Quantity -= quantity;
            }
        }

        public IEnumerable<Product> GetListProduct(string keyword)
        {
            IEnumerable<Product> query;
            if (!string.IsNullOrEmpty(keyword))
                query = this.productRepositories.GetMulti(x => x.Name.Contains(keyword));
            else
                query = this.productRepositories.GetAll();
            return query;
        }

        public void IncreaseView(int id)
        {
            var product = this.productRepositories.GetSingleById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
            this.unitOfWork.Save();
        }
    }
}