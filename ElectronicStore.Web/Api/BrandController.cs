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
    [RoutePrefix("api/brands")]
    [Authorize]
    public class BrandController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private IBrandService brandService;
        public BrandController(ILogErrorService logErrorService, IBrandService brandService) : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.brandService = brandService;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, Brand brand)
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
                    this.brandService.Add(brand);
                    this.brandService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, brand);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, Brand brand)
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
                    var dbBrand = this.brandService.GetById(brand.Id);
                    dbBrand.Name = brand.Name;
                    dbBrand.Description = brand.Description;
                    dbBrand.Logo = brand.Logo;
                    dbBrand.Status = brand.Status;
                    dbBrand.Alias = brand.Alias;
                    this.brandService.Update(dbBrand);
                    this.brandService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbBrand);
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
                    var oldProduct = this.brandService.Delete(id);
                    this.brandService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, oldProduct);
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
                var model = this.brandService.GetById(id);

                var response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int skip, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                var models = this.brandService.GetAll(keyword);
                var results = models.Skip(skip).Take(pageSize);
                var responseData = new Pagination<Brand>()
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

        [Route("getlistall")]
        [HttpGet]
        public HttpResponseMessage GetListAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var models = this.brandService.GetAll();
                var response = request.CreateResponse(HttpStatusCode.OK, models);
                return response;
            });
        }

    }
}
