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
    public interface INewsService
    {
        News Add(News Product);

        void Update(News Product);

        News Delete(int id);

        IEnumerable<News> GetAll();

        IEnumerable<News> GetAll(string keyword);

        News GetById(int id);

        IEnumerable<News> GetListNewstByCategoryId(int categoryId, int page, int pageSize, string sort, out int totalRow);

        void Save();

        void IncreaseView(int id);

    }
    public class NewsService : INewsService
    {
        private INewsRepositories newsRepositories;
        private IUnitOfWork unitOfWork;

        public NewsService(INewsRepositories newsRepositories, IUnitOfWork unitOfWork)
        {
            this.newsRepositories = newsRepositories;
            this.unitOfWork = unitOfWork;
        }

        public News Add(News News)
        {
            var news = this.newsRepositories.Add(News);
            this.unitOfWork.Save();
            return news;
        }

        public News Delete(int id)
        {
            return this.newsRepositories.Delete(id);
        }

        public IEnumerable<News> GetAll()
        {
            return this.newsRepositories.GetAll();
        }

        public IEnumerable<News> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return this.newsRepositories.GetMulti(x => x.Title.Contains(keyword) || x.Description.Contains(keyword));
            else
                return this.newsRepositories.GetAll();
        }

        public News GetById(int id)
        {
            return this.newsRepositories.GetSingleById(id);
        }

        public IEnumerable<News> GetListNewstByCategoryId(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = this.newsRepositories.GetMulti(x => x.Status && x.CategoryId == categoryId);
            totalRow = query.Count();

            switch (sort)
            {
                case "view_count":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void IncreaseView(int id)
        {
            var news = this.newsRepositories.GetSingleById(id);
            if (news.ViewCount.HasValue)
                news.ViewCount += 1;
            else
                news.ViewCount = 1;
            this.unitOfWork.Save();
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }

        public void Update(News news)
        {
            this.newsRepositories.Update(news);
        }
    }
}
