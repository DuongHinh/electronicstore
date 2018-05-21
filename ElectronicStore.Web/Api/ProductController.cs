using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectronicStore.Service;
using ElectronicStore.Data.Entities;
using System.Web.Script.Serialization;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/product")]
    [Authorize]
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
        public HttpResponseMessage Create(HttpRequestMessage request, Product product)
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
                    product.CreatedDate = DateTime.Now;
                    product.CreatedBy = User.Identity.Name;
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
                    dbProduct.Alias = product.Alias;
                    dbProduct.CategoryId = product.CategoryId;
                    dbProduct.CreatedBy = product.CreatedBy;
                    dbProduct.CreatedDate = product.CreatedDate;
                    dbProduct.Description = product.Description;
                    dbProduct.HomeFlag = product.HomeFlag;
                    dbProduct.HotFlag = product.HotFlag;
                    dbProduct.Image = product.Image;
                    dbProduct.MoreImages = product.MoreImages;
                    dbProduct.Name = product.Name;
                    dbProduct.OriginalPrice = product.OriginalPrice;
                    dbProduct.Price = product.Price;
                    dbProduct.PromotionPrice = product.PromotionPrice;
                    dbProduct.Quantity = product.Quantity;
                    dbProduct.Status = product.Status;
                    dbProduct.Tags = product.Tags;
                    dbProduct.UpdatedBy = User.Identity.Name;
                    dbProduct.UpdatedDate = DateTime.Now;
                    dbProduct.ViewCount = product.ViewCount;

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

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listProductIds)
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
                    var lstProductIds = listProductIds.Split(',').ToList();
                    foreach (var item in lstProductIds)
                    {
                        var id = int.Parse(item);
                        this.productService.Delete(id);
                    }

                    this.productService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, lstProductIds.Count);
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
                var model = this.productService.GetById(id);

                var response = request.CreateResponse(HttpStatusCode.OK, model);

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
                var models = this.productService.GetAll(keyword).OrderByDescending(x => x.CreatedDate);
                var results = models.Skip(skip).Take(pageSize);
                var responseData = new Pagination<Product>()
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
