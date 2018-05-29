using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectronicStore.Service;
using ElectronicStore.Web.Models;
using System.Text;
using static ElectronicStore.Data.Entities.Enum.OrderEnum;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/order")]
    [Authorize]
    public class OrderController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private IOrderDetailService orderDetailService;
        private IOrderService orderService;
        private IMailService mailService;
        private IProductService productService;
        public OrderController(ILogErrorService logErrorService, 
                                IOrderDetailService orderDetailService, 
                                IOrderService orderService, 
                                IMailService mailService,
                                IProductService productService
                                ) : base(logErrorService)
        {
            this.orderDetailService = orderDetailService;
            this.orderService = orderService;
            this.logErrorService = logErrorService;
            this.mailService = mailService;
            this.productService = productService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int? status)
        {
            return CreateHttpResponse(request, () =>
            {
                var orderViewModels = this.orderService.GetAllOrder(keyword, status);

                var response = request.CreateResponse(HttpStatusCode.OK, orderViewModels);
                return response;
            });
        }

        [Route("getdetail")]
        [HttpGet]
        public HttpResponseMessage GetDetail(HttpRequestMessage request, int orderId)
        {
            return CreateHttpResponse(request, () =>
            {
                var orderViewModel = this.orderService.GetDetailOrderByOrderId(orderId);
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

                    var orderDetail = this.orderService.GetDetailOrderByOrderId(orderVm.Id);

                    if (orderDetail != null && orderVm.Status == OrderStatus.Cancelled)
                    {
                        int[] ArrQuantity = orderDetail.Quantities.ToArray();
                        int i = 0;
                        foreach (var item in orderDetail.Products)
                        {
                            if (i == orderDetail.Products.Count())
                            {
                                break;
                            }
                            var dbProduct = this.productService.GetById(item.Id);
                            if (dbProduct != null)
                            {
                                dbProduct.Quantity += ArrQuantity[i];
                                this.productService.Update(dbProduct);
                                this.productService.Save();
                            }

                            i++;
                        }
                    }


                    if (orderDetail != null && orderVm.PaymentStatus == PaymentStatus.Paid && dbOrder.PaymentStatus != orderVm.PaymentStatus)
                    {
                        string title = "Thanh toán thành công đơn hàng từ Electrolic Store";
                        StringBuilder builder = new StringBuilder();
                        builder.AppendFormat("{0}", title);
                        builder.Append("<br/>");
                        builder.AppendFormat("Khách hàng: {0}", orderDetail.Name);
                        builder.Append("<br/>");
                        builder.AppendFormat("Địa chỉ: {0}", orderDetail.Address);
                        builder.Append("<br/>");
                        builder.AppendFormat("Số điện thoại: {0}", orderDetail.PhoneNumber);
                        builder.Append("<br/>");
                        builder.Append("<br/>");

                        builder.Append("<table style='width: 100 %' cellpadding='5' border='1'>");
                        builder.Append("<thead>");
                        builder.Append("<tr>");
                        builder.Append("<th style='width: 40 %; '>Tên sản phẩm</th>");
                        builder.Append("<th style'width: 30 %; '>Số lượng</th>");
                        builder.Append("<th style='width: 30 %; '>Đơn giá</th>");
                        builder.Append("</tr>");
                        builder.Append("</thead>");
                        builder.Append("<tbody>");
                        int[] ArrQuantity = orderDetail.Quantities.ToArray();
                        decimal[] ArrPrice = orderDetail.Prices.ToArray();
                        int i = 0;
                        foreach (var item in orderDetail.Products)
                        {
                            if (i == orderDetail.Products.Count())
                            {
                                break;
                            }
                            builder.Append("<tr>");
                            builder.AppendFormat("<td class='text - left'>{0}</td>", item.Name);
                            builder.AppendFormat("<td class='font-weight: initial'>{0}</td>", ArrQuantity[i]);
                            builder.AppendFormat("<td class='text - left'>{0}</td>", ArrPrice[i].ToString("N0") + " đ");
                            builder.Append("</tr>");
                            i++;
                        }

                        builder.Append("</tbody>");
                        builder.Append("</table>");

                        builder.Append("<br/>");
                        builder.AppendFormat("Tổng tiền: {0}", orderDetail.Amount.ToString("N0") + " đ");
                        this.mailService.SendMail(orderDetail.Email, title, builder.ToString());
                    }

                }

                return response;
            });
        }
    }
}
