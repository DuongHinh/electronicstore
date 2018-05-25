using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectronicStore.Service;
using ElectronicStore.Data.Entities;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/news")]
    [Authorize]
    public class NewsController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private INewsService newsService;
        public NewsController(ILogErrorService logErrorService, INewsService newsService) : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.newsService = newsService;
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, News news)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    news.CreatedDate = DateTime.Now;
                    news.CreatedBy = User.Identity.Name;
                    this.newsService.Add(news);
                    this.newsService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, news);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, News news)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbNews= this.newsService.GetById(news.Id);
                    dbNews.Alias = news.Alias;
                    dbNews.CategoryId = news.CategoryId;
                    dbNews.Image = news.Image;
                    dbNews.ViewCount = news.ViewCount;
                    dbNews.Title = news.Title;
                    dbNews.Description = news.Description;
                    dbNews.UpdatedDate = DateTime.Now;
                    dbNews.Status = news.Status;
                    dbNews.UpdatedBy = User.Identity.Name;

                    this.newsService.Update(dbNews);
                    this.newsService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbNews);
                }

                return response;
            });
        }
        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbNews = this.newsService.Delete(id);
                    this.newsService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, dbNews);
                }

                return response;
            });
        }

        [Route("getbyid")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = this.newsService.GetById(id);

                var response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetListAll(HttpRequestMessage request, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                var models = this.newsService.GetAll(keyword);
                var response = request.CreateResponse(HttpStatusCode.OK, models);
                return response;
            });
        }
    }
}
