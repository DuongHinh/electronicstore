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
    [RoutePrefix("api/product")]
    public class ProductController : BaseApiController
    {
        private IProductService productService;
        public ProductController(ILogErrorService logErrorService, IProductService productService) : base(logErrorService)
        {
            this.productService = productService;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request)
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
                    var product = new Product();
                    product.CreatedDate = DateTime.Now;
                    this.productService.Add(product);
                    this.productService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, product);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, Product product)
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
                    var dbProduct = this.productService.GetById(product.Id);

                    dbProduct.UpdatedDate = DateTime.Now;
                    this.productService.Update(dbProduct);
                    this.productService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbProduct);
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
                    var oldProduct = this.productService.Delete(id);
                    this.productService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, oldProduct);
                }

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = this.productService.GetById(id);

                var response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int skip, int pageSize = 16)
        {
            return CreateHttpResponse(request, () =>
            {
                var models = this.productService.GetAll(keyword).OrderByDescending(x => x.CreatedDate);
                var results = models.Skip(skip).Take(pageSize);
                var responseData = new PagedList<Product>()
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
    }
}
