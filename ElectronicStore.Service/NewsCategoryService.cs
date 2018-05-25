using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Service
{
    public interface INewsCategoryService
    {
        NewsCategory Add(NewsCategory NewsCategory);

        void Update(NewsCategory Product);

        NewsCategory Delete(int id);

        IEnumerable<NewsCategory> GetAll();

        IEnumerable<NewsCategory> GetAll(string keyword);

        NewsCategory GetById(int id);

        void Save();
    }
    public class NewsCategoryService : INewsCategoryService
    {
        private INewsCategoryRepositories newsCategoryRepositories;
        private IUnitOfWork unitOfWork;

        public NewsCategoryService(INewsCategoryRepositories newsCategoryRepositories, IUnitOfWork unitOfWork)
        {
            this.newsCategoryRepositories = newsCategoryRepositories;
            this.unitOfWork = unitOfWork;
        }

        public NewsCategory Add(NewsCategory NewsCategory)
        {
            var newsCategory = this.newsCategoryRepositories.Add(NewsCategory);
            this.unitOfWork.Save();
            return newsCategory;
        }

        public NewsCategory Delete(int id)
        {
            return this.newsCategoryRepositories.Delete(id);
        }

        public IEnumerable<NewsCategory> GetAll()
        {
            return this.newsCategoryRepositories.GetAll();
        }

        public IEnumerable<NewsCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return this.newsCategoryRepositories.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return this.newsCategoryRepositories.GetAll();
        }

        public NewsCategory GetById(int id)
        {
            return this.newsCategoryRepositories.GetSingleById(id);
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }

        public void Update(NewsCategory NewsCategory)
        {
            this.newsCategoryRepositories.Update(NewsCategory);
        }
    }
}
