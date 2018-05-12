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
    [RoutePrefix("api/productCategories")]
    [Authorize]
    public class ProductCategoryController : BaseApiController
    {
        private IProductCategoryService productCategoryService;
        public ProductCategoryController(ILogErrorService logErrorService, IProductCategoryService productCategoryService) : base(logErrorService)
        {
            this.productCategoryService = productCategoryService;
        }

        [Route("getall")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                var models = this.productCategoryService.GetAll(keyword).OrderByDescending(x => x.CreatedDate);
                var response = request.CreateResponse(HttpStatusCode.OK, models);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int skip, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                var models = this.productCategoryService.GetAll(keyword).OrderByDescending(x => x.CreatedDate);
                var results = models.Skip(skip).Take(pageSize);
                var responseData = new Pagination<ProductCategory>()
                {
                    Skip = skip,
                    PageSize = pageSize,
                    Results = results,
                    TotalResults = models.Count(),
                };

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategory category)
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
                    category.CreatedDate = DateTime.Now;
                    this.productCategoryService.Add(category);
                    this.productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategory category)
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
                    var dbCategory = this.productCategoryService.GetById(category.Id);
                    dbCategory.Alias = category.Alias;
                    dbCategory.CreatedBy = category.CreatedBy;
                    dbCategory.CreatedDate = category.CreatedDate;
                    dbCategory.Description = category.Description;
                    dbCategory.HomeFlag = category.HomeFlag;
                    dbCategory.Image = category.Image;
                    dbCategory.Name = category.Name;
                    dbCategory.Status = category.Status;
                    dbCategory.UpdatedBy = category.UpdatedBy;
                    dbCategory.UpdatedDate = DateTime.Now;
                    dbCategory.ParentId = category.ParentId;
                    this.productCategoryService.Update(dbCategory);
                    this.productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbCategory);
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
                    var oldCategory = this.productCategoryService.Delete(id);
                    this.productCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, oldCategory);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listCategoryIds)
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
                    var lstCategoryIds = listCategoryIds.Split(',').ToList();
                    foreach (var item in lstCategoryIds)
                    {
                        var id = int.Parse(item);
                        this.productCategoryService.Delete(id);
                    }

                    this.productCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, lstCategoryIds.Count);
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
                var model = this.productCategoryService.GetById(id);

                var response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }
    }
}
