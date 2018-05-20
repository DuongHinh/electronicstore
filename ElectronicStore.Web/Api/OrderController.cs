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
    [RoutePrefix("api/order")]
    [Authorize]
    public class OrderController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private IOrderDetailService orderDetailService;
        private IOrderService orderService;
        public OrderController(ILogErrorService logErrorService, IOrderDetailService orderDetailService, IOrderService orderService) : base(logErrorService)
        {
            this.orderDetailService = orderDetailService;
            this.orderService = orderService;
            this.logErrorService = logErrorService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var orders = this.orderService.GetAll();
                var orderDetail = this.orderDetailService.GetAll();

                object orderViewModel = null;
                if (orders != null && orderDetail != null)
                {
                    orderViewModel = from o in orders
                                     join od in orderDetail
                                     on o.Id equals od.OrderId
                                     group od by o.Id into gLine
                                     select new
                                     {
                                         OrderId = gLine.Key,
                                         Amount = gLine.Sum(x => x.Price * x.Quantity),
                                         CustumerName = gLine.Select(g => g.Order.CustomerName).FirstOrDefault(),
                                         CustumerAddress = gLine.Select(g => g.Order.CustomerAddress).FirstOrDefault(),
                                         CustumerEmail = gLine.Select(g => g.Order.CustomerEmail).FirstOrDefault(),
                                         CustumerPhone = gLine.Select(g => g.Order.CustomerPhone).FirstOrDefault(),
                                         OrderDate = gLine.Select(g => g.Order.OrderDate).FirstOrDefault(),
                                         ShipDate = gLine.Select(g => g.Order.ShipDate).FirstOrDefault(),
                                         IsPaymented = gLine.Select(g => g.Order.IsPaymented).FirstOrDefault(),
                                         IsShiped = gLine.Select(g => g.Order.IsShiped).FirstOrDefault(),
                                         Products = gLine.Select(g => g.Product).Select(p => new {Name = p.Name, Price = p.Price })
                                     };
                }
                

                var response = request.CreateResponse(HttpStatusCode.OK, orderViewModel);
                return response;
            });
        }
    }
}
