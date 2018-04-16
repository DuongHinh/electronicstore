using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectronicStore.Service;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/productCategories")]
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
    }
}
