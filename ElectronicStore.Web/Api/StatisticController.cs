using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectronicStore.Service;
using System.Web;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/statistic")]
    [Authorize]
    public class StatisticController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private IStaticsticService staticsticService;
        private IOrderService orderService;
        private IOrderDetailService orderDetailService;
        public StatisticController(ILogErrorService logErrorService, 
                                    IStaticsticService staticsticService, 
                                    IOrderService orderService, 
                                    IOrderDetailService orderDetailService) 
                                    : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.staticsticService = staticsticService;
            this.orderService = orderService;
            this.orderDetailService = orderDetailService;
        }


        [Route("GetProfitAndRevenue")]
        [HttpGet]
        public HttpResponseMessage GetWeeklyProfitAndRevenue(HttpRequestMessage request, string date)
        {
            return CreateHttpResponse(request, () =>
            {
                var responseData = this.staticsticService.GetWeeklyProfitAndRevenue(date);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("GetHomePageStaticstic")]
        [HttpGet]
        public HttpResponseMessage GetHomePageStatistic(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var visitedCount = HttpContext.Current.Application["VisitedCount"].ToString();
                var onlineCount = HttpContext.Current.Application["OnlineCount"].ToString();
                var orders = this.orderService.GetAll();
                var orderDetails = this.orderDetailService.GetAll();
                var totalProductSales = (from o in orders
                                         join od in orderDetails
                                         on o.Id equals od.OrderId
                                         where (int)o.Status == 1
                                         select od.Quantity).Sum();
                var newOrder = orders.Where(o => (int)o.Status == 0).Count();

                var responseData = new
                {
                    VisitedCount = visitedCount,
                    OnlineCount = onlineCount,
                    TotalProductSales = totalProductSales,
                    NewOrder = newOrder
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

    }
}
