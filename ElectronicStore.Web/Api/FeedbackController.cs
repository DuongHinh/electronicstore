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
    [RoutePrefix("api/feedbacks")]
    [Authorize]
    public class FeedbackController : BaseApiController
    {
        ILogErrorService logErrorService;
        IFeedbackService feedbackService;
        public FeedbackController(ILogErrorService logErrorService, IFeedbackService feedbackService) : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.feedbackService = feedbackService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int skip, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                var models = this.feedbackService.GetAll(keyword);
                var results = models.Skip(skip).Take(pageSize);
                var responseData = new Pagination<Feedback>()
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

        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = this.feedbackService.GetById(id);

                var response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }
    }
}
