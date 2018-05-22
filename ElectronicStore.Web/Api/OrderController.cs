using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectronicStore.Service;
using ElectronicStore.Web.Models;

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
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
            var orders = this.orderService.GetAll();
            var orderDetail = this.orderDetailService.GetAll();

            object orderViewModel = null;
            if (orders != null && orderDetail != null)
            {
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    orders = orders.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
                }
                    orderViewModel = from o in orders
                                     join od in orderDetail
                                     on o.Id equals od.OrderId
                                     group od by o.Id into gLine
                                     select new
                                     {
                                         OrderId = gLine.Key,
                                         Amount = gLine.Sum(x => x.Price * x.Quantity),
                                         Name = gLine.Select(g => g.Order.Name).FirstOrDefault(),
                                         Address = gLine.Select(g => g.Order.Address).FirstOrDefault(),
                                         Email = gLine.Select(g => g.Order.Email).FirstOrDefault(),
                                         PhoneNumber = gLine.Select(g => g.Order.PhoneNumber).FirstOrDefault(),
                                         OrderDate = gLine.Select(g => g.Order.OrderDate).FirstOrDefault(),
                                         ShipDate = gLine.Select(g => g.Order.ShipDate).FirstOrDefault(),
                                         PaymentStatus = gLine.Select(g => g.Order.PaymentStatus).FirstOrDefault(),
                                         ShipStatus = gLine.Select(g => g.Order.ShipStatus).FirstOrDefault(),
                                         Status = gLine.Select(g => g.Order.Status).FirstOrDefault(),
                                         Products = gLine.Select(g => g.Product).Select(p => new {Name = p.Name, Price = p.Price, Image = p.Image }),
                                         Quantities = gLine.Select(g => g.Quantity)
                                     };
                }
                

                var response = request.CreateResponse(HttpStatusCode.OK, orderViewModel);
                return response;
            });
        }

        [Route("getdetail")]
        [HttpGet]
        public HttpResponseMessage GetDetail(HttpRequestMessage request, int orderId)
        {
            return CreateHttpResponse(request, () =>
            {
                var orders = this.orderService.GetAll();
                var orderDetail = this.orderDetailService.GetAll();

                object orderViewModel = null;
                if (orders != null && orderDetail != null)
                {
                    orders.Where(o => o.Id == orderId);

                    orderViewModel = (from o in orders
                                     join od in orderDetail
                                     on o.Id equals od.OrderId
                                     group od by o.Id into gLine
                                     select new
                                     {
                                         OrderId = gLine.Key,
                                         Amount = gLine.Sum(x => x.Price * x.Quantity),
                                         Name = gLine.Select(g => g.Order.Name).FirstOrDefault(),
                                         Address = gLine.Select(g => g.Order.Address).FirstOrDefault(),
                                         Email = gLine.Select(g => g.Order.Email).FirstOrDefault(),
                                         PhoneNumber = gLine.Select(g => g.Order.PhoneNumber).FirstOrDefault(),
                                         OrderDate = gLine.Select(g => g.Order.OrderDate).FirstOrDefault(),
                                         ShipDate = gLine.Select(g => g.Order.ShipDate).FirstOrDefault(),
                                         PaymentStatus = gLine.Select(g => g.Order.PaymentStatus).FirstOrDefault(),
                                         ShipStatus = gLine.Select(g => g.Order.ShipStatus).FirstOrDefault(),
                                         Status = gLine.Select(g => g.Order.Status).FirstOrDefault(),
                                         Products = gLine.Select(g => g.Product).Select(p => new { Name = p.Name, Price = p.Price, Image = p.Image }),
                                         Quantities = gLine.Select(g => g.Quantity)
                                     }).FirstOrDefault();
                }


                var response = request.CreateResponse(HttpStatusCode.OK, orderViewModel);
                return response;
            });
        }

        [Route("updateOrderStatus")]
        [HttpPut]
        public HttpResponseMessage UpdateOrderStatus(HttpRequestMessage request, UpdateOrderViewModel orderVm)
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
                    var dbOrder = this.orderService.GetById(orderVm.Id);
                    dbOrder.ShipDate = orderVm.ShipDate;
                    dbOrder.PaymentStatus = orderVm.PaymentStatus;
                    dbOrder.ShipStatus = orderVm.ShipStatus;
                    dbOrder.Status = orderVm.Status;
                    this.orderService.Update(dbOrder);
                    this.orderService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbOrder);
                }

                return response;
            });
        }
    }
}
