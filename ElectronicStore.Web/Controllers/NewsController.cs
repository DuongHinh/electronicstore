using ElectronicStore.Service;
using ElectronicStore.Web.Core;
using ElectronicStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicStore.Web.Controllers
{
    public class NewsController : Controller
    {
        private INewsService newsService;
        private INewsCategoryService newsCategoryService;
        public NewsController(INewsService newsService, INewsCategoryService newsCategoryService)
        {
            this.newsCategoryService = newsCategoryService;
            this.newsService = newsService;
        }
        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = 10;
            int totalRow = 0;

            var newsModel = this.newsService.GetListNewstByCategoryId(id, page, pageSize, sort, out totalRow);
            var newsViewModel = newsModel.Select(p => new NewsViewModel()
            {
                Id = p.Id,
                Alias = p.Alias,
                Title = p.Title,
                CategoryId = p.CategoryId,
                Image = p.Image,
                Description = p.Description,
                ViewCount = p.ViewCount,
                CreatedDate = p.CreatedDate,
                CreatedBy = p.CreatedBy,
                UpdatedDate = p.UpdatedDate,
                UpdatedBy = p.UpdatedBy,
                Status = p.Status
            });

            var category = this.newsCategoryService.GetById(id);

            ViewBag.Category = new NewsCategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Alias = category.Alias,
                Description = category.Description,
                ParentId = category.ParentId,          
                Image = category.Image,             
                CreatedDate = category.CreatedDate,
                CreatedBy = category.CreatedBy,
                UpdatedDate = category.UpdatedDate,
                UpdatedBy = category.UpdatedBy,
                Status = category.Status
            };

            var paginationNews = new Pagination<NewsViewModel>()
            {
                PageSize = pageSize,
                Skip = (page - 1) * pageSize,
                Results = newsViewModel,
                MaxPage = 5,
                TotalResults = totalRow
            };

            return View(paginationNews);
        }

        public ActionResult Detail(int id)
        {
            this.newsService.IncreaseView(id);
            var newsModel = this.newsService.GetById(id);
            var newsViewModel = new NewsViewModel()
            {
                Id = newsModel.Id,
                Title = newsModel.Title,
                Alias = newsModel.Alias,
                CategoryId = newsModel.CategoryId,
                Image = newsModel.Image,
                Description = newsModel.Description,
                ViewCount = newsModel.ViewCount,
                CreatedDate = newsModel.CreatedDate,
                CreatedBy = newsModel.CreatedBy,
                UpdatedDate = newsModel.UpdatedDate,
                UpdatedBy = newsModel.UpdatedBy,
                Status = newsModel.Status
            };

            return View(newsViewModel);
        }
    }
}