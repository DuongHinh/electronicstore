﻿using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using ElectronicStore.Fulcrum;
using System.Collections.Generic;
using System;
using System.Linq;

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

        IEnumerable<string> GetListProductByName(string name);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> GetReatedProducts(int id, int top);

        IEnumerable<Tag> GetListTagByProductId(int id);

        Tag GetTag(int tagId);

        IEnumerable<Product> GetListProductByTag(int tagId, int page, int pagesize, out int totalRow);

        bool SellProduct(int productId, int quantity);

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
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagAlias = StringHelper.GetUnsignString(tags[i]);
                    if (!this.tagRepositories.Any(x => x.Alias == tagAlias))
                    {
                        Tag tag = new Tag();
                        tag.Alias = tagAlias;
                        tag.Name = tags[i];                     
                        tag.Type = TagType.Product;
                        this.tagRepositories.Add(tag);
                        this.unitOfWork.Save();

                        ProductTag productTag = new ProductTag();
                        productTag.ProductId = Product.Id;
                        productTag.TagId = tag.Id;
                        this.productTagRepositories.Add(productTag);
                        //this.unitOfWork.Save();
                    }
                }
            }
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
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagAlias = StringHelper.GetUnsignString(tags[i]);
                    if (!this.tagRepositories.Any(x => x.Alias == tagAlias))
                    {
                        Tag tag = new Tag();
                        tag.Alias = tagAlias;
                        tag.Name = tags[i];
                        tag.Type = TagType.Product;
                        this.tagRepositories.Add(tag);
                        this.unitOfWork.Save();

                        this.productTagRepositories.DeleteMulti(x => x.ProductId == Product.Id);
                        ProductTag productTag = new ProductTag();
                        productTag.ProductId = Product.Id;
                        productTag.TagId = tag.Id;
                        this.productTagRepositories.Add(productTag);
                    }
                }
            }
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
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetListProductByName(string name)
        {
            return this.productRepositories.GetMulti(x => x.Status && x.Name.Contains(name)).Select(y => y.Name);
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetReatedProducts(int id, int count)
        {
            var product = this.productRepositories.GetSingleById(id);
            return this.productRepositories.GetMulti(x => x.Status && x.Id != id && x.CategoryId == product.CategoryId).OrderByDescending(x => x.CreatedDate).Take(count);
        }

        public IEnumerable<Tag> GetListTagByProductId(int id)
        {
            return this.productTagRepositories.GetMulti(x => x.ProductId == id, new string[] { "Tag" }).Select(y => y.Tag);
        }

        public Tag GetTag(int tagId)
        {
            return this.tagRepositories.GetSingleByCondition(x => x.Id == tagId);
        }

        public IEnumerable<Product> GetListProductByTag(int tagId, int page, int pagesize, out int totalRow)
        {
            var model = this.productRepositories.GetListProductByTag(tagId, page, pagesize, out totalRow);
            return model;
        }

        public bool SellProduct(int productId, int quantity)
        {
            var product = this.productRepositories.GetSingleById(productId);
            if (product.Quantity < quantity)
                return false;
            product.Quantity -= quantity;
            return true;
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
    }
}