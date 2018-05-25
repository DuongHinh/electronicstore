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
    [RoutePrefix("api/newsCategories")]
    [Authorize]
    public class NewsCategoryController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private INewsCategoryService newsCategoryService;
        public NewsCategoryController(ILogErrorService logErrorService, INewsCategoryService newsCategoryService) : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.newsCategoryService = newsCategoryService;
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, NewsCategory newsCategory)
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
                    newsCategory.CreatedDate = DateTime.Now;
                    newsCategory.CreatedBy = User.Identity.Name;
                    this.newsCategoryService.Add(newsCategory);
                    this.newsCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, newsCategory);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, NewsCategory newsCategory)
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
                    var dbNewsCategory = this.newsCategoryService.GetById(newsCategory.Id);
                    dbNewsCategory.Name = newsCategory.Name;
                    dbNewsCategory.Alias = newsCategory.Alias;
                    dbNewsCategory.Image = newsCategory.Image;
                    dbNewsCategory.Description = newsCategory.Description;
                    dbNewsCategory.ParentId = newsCategory.ParentId;
                    dbNewsCategory.UpdatedDate = DateTime.Now;
                    dbNewsCategory.Status = newsCategory.Status;
                    dbNewsCategory.UpdatedBy = User.Identity.Name;

                    this.newsCategoryService.Update(dbNewsCategory);
                    this.newsCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbNewsCategory);
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
                    var dbNews = this.newsCategoryService.Delete(id);
                    this.newsCategoryService.Save();

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
                var model = this.newsCategoryService.GetById(id);

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
                var models = this.newsCategoryService.GetAll(keyword);
                var response = request.CreateResponse(HttpStatusCode.OK, models);
                return response;
            });
        }
    }
}
