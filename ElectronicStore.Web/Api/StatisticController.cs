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
    [RoutePrefix("api/statistic")]
    [Authorize]
    public class StatisticController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private IStaticsticService staticsticService;
        public StatisticController(ILogErrorService logErrorService, IStaticsticService staticsticService) : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.staticsticService = staticsticService;
        }


        [Route("GetProfitAndRevenue")]
        [HttpGet]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request, string date)
        {
            return CreateHttpResponse(request, () =>
            {
                var responseData = this.staticsticService.GetStaticsticProfitAndRevenuePerWeek(date);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

    }
}
