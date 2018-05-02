using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using ElectronicStore.Fulcrum;
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
    }
}