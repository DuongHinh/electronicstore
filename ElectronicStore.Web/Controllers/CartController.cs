using ElectronicStore.Service;
using ElectronicStore.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using ElectronicStore.Web.Models;
using System.Web.Script.Serialization;
using ElectronicStore.Data.Entities;
using ElectronicStore.Fulcrum;
using System.Text;

namespace ElectronicStore.Web.Controllers
{
    public class CartController : Controller
    {
        private IProductService productService;
        private IOrderService orderService;
        private ILogErrorService logErrorService;
        private IMailService mailService;
        private ApplicationUserManager userManager;
        private AppSettings appSettings;
        public CartController(IProductService productService, IOrderService orderService, ILogErrorService logErrorService, IMailService mailService, ApplicationUserManager userManager, AppSettings appSettings)
        {
            this.productService = productService;
            this.orderService = orderService;
            this.userManager = userManager;
            this.appSettings = appSettings;
            this.logErrorService = logErrorService;
            this.mailService = mailService;
        }
        // GET: ShopingCart
        public ActionResult Index()
        {
            var cart = Session[this.appSettings.SessionCart];
            var listItem = cart != null ? (List<CartItemViewModel>)cart : new List<CartItemViewModel>();
            return View(listItem);
        }

        public ActionResult AddItem(int productId)
        {
            if (!Request.IsAuthenticated)
            {
                return Redirect("/not-logged-in");
            }

            var product = this.productService.GetById(productId);
            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Alias = product.Alias,
                CategoryId = product.CategoryId,
                Image = product.Image,
                MoreImages = product.MoreImages,
                Price = product.Price,
                Quantity = product.Quantity,
                PromotionPrice = product.PromotionPrice,
                Description = product.Description,
                HotFlag = product.HotFlag,
                ViewCount = product.ViewCount,
                CreatedDate = product.CreatedDate,
                CreatedBy = product.CreatedBy,
                UpdatedDate = product.UpdatedDate,
                UpdatedBy = product.UpdatedBy,
                Status = product.Status
            };

            if (productViewModel.Quantity == 0)
            {
                return Redirect("/out-of-stock");
            }

            var cart = Session[this.appSettings.SessionCart];
            if (cart != null)
            {
                var list = (List<CartItemViewModel>)cart;
                if (list.Any(x => x.Product.Id == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.Id == productId)
                        {
                            item.Quantity += 1;
                        }
                    }
                }
                else
                {
                    // add new cart
                    var item = new CartItemViewModel();
                    item.Product = productViewModel;
                    item.Quantity = 1;
                    list.Add(item);
                   
                    Session[this.appSettings.SessionCart] = list;
                }
            }
            else
            {
                // add new cart
                var item = new CartItemViewModel();
                item.Product = productViewModel;
                item.Quantity = 1;
                var list = new List<CartItemViewModel>();
                list.Add(item);
                
                Session[this.appSettings.SessionCart] = list;
            }

            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItemViewModel>>(cartModel);
            var sessionCart = (List<CartItemViewModel>)Session[this.appSettings.SessionCart];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);

                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }

            Session[this.appSettings.SessionCart] = sessionCart;

            return Json(new
            {
                status = true
            });
        }

        /// <summary>
        /// Detete All Cart
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteAll()
        {
            Session[this.appSettings.SessionCart] = null;
            return Json(new
            {
                status = true
            });
        }

        /// <summary>
        /// Detete each item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Delete(int id)
        {
            bool status = false;
            var sessionCart = (List<CartItemViewModel>)Session[this.appSettings.SessionCart];
            if (sessionCart != null)
            {
                sessionCart.RemoveAll(x => x.Product.Id == id);
                Session[this.appSettings.SessionCart] = sessionCart;
                status = true;
            }
            else {
                status = false;
            }

            return Json(new
            {
                status = status
            });
        }

        [HttpGet]
        public ActionResult Order()
        {
            var order = new OrderViewModel();
            var cart = Session[this.appSettings.SessionCart];
            
            var listItem = cart != null ? (List<CartItemViewModel>)cart : new List<CartItemViewModel>();
            order.Cart = listItem;
            return View(order);
        }

        [HttpPost]
        public ActionResult Order(OrderViewModel orderViewModel)
        {
            var cartItem = Session[this.appSettings.SessionCart];
            var listItem = cartItem != null ? (List<CartItemViewModel>)cartItem : new List<CartItemViewModel>();
            orderViewModel.Cart = listItem;

            if (ModelState.IsValid)
            {
                var order = new Order();
                order.Name = orderViewModel.Name;
                order.Email = orderViewModel.Email;
                order.Address = orderViewModel.Address;
                order.PhoneNumber = orderViewModel.PhoneNumber;
                order.OrderDate = DateTime.Now;

                if (Request.IsAuthenticated)
                {
                    order.CustomerId = User.Identity.GetUserId();
                }


                try
                {
                    var cart = (List<CartItemViewModel>)Session[this.appSettings.SessionCart];
                    List<OrderDetail> orderDetails = new List<OrderDetail>();
                    bool checkQuantity = true;
                   
                    foreach (var item in cart)
                    {
                        var detail = new OrderDetail();
                        detail.ProductId = item.Product.Id;
                        detail.Quantity = item.Quantity;
                        detail.Price = item.Product.Price;
                        orderDetails.Add(detail);
                        checkQuantity = this.productService.SellProduct(item.Product.Id, item.Quantity);
                    }

                    this.orderService.CreateOrder(order, orderDetails);
                    this.productService.Save();

                    var orderDetailCreated = this.orderService.GetDetailOrderByOrderId(order.Id);
                    //send mail
                    if (!string.IsNullOrWhiteSpace(orderViewModel.Email) && cart != null)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append("Thông tin đơn hàng mới từ Electronic Store");
                        builder.Append("<br/>");
                        builder.AppendFormat("Khách hàng: {0}", orderDetailCreated.Name);
                        builder.Append("<br/>");
                        builder.AppendFormat("Địa chỉ: {0}", orderDetailCreated.Address);
                        builder.Append("<br/>");
                        builder.AppendFormat("Số điện thoại: {0}", orderDetailCreated.PhoneNumber);
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
                        //decimal totalAmount = 0;
                        int[] ArrQuantity = orderDetailCreated.Quantities.ToArray();
                        int i = 0;
                        foreach (var item in orderDetailCreated.Products)
                        {
                            if (i == orderDetailCreated.Products.Count())
                            {
                                break;
                            }

                            builder.Append("<tr>");
                            builder.AppendFormat("<td class='text - left'>{0}</td>", item.Name);
                            builder.AppendFormat("<td class='font-weight: initial'>{0}</td>", ArrQuantity[i]);
                            builder.AppendFormat("<td class='text - left'>{0}</td>", item.Price.ToString("N0") + " đ");
                            builder.Append("</tr>");

                            //totalAmount += (item.Price * ArrQuantity[i]);
                            i++;
                        }

                        builder.Append("</tbody>");
                        builder.Append("</table>");

                        builder.Append("<br/>");
                        builder.AppendFormat("Tổng tiền: {0}", orderDetailCreated.Amount.ToString("N0") + " đ");

                        this.mailService.SendMail(orderViewModel.Email, "Đơn hàng mới từ Electronic Store", builder.ToString());
                    }

                }
                catch (Exception ex)
                {
                    LogError error = new LogError();
                    error.Date = DateTime.Now;
                    error.Message = ex.Message;
                    error.StackTrace = ex.StackTrace;
                    this.logErrorService.Create(error);
                    this.logErrorService.Save();
                    return Redirect("/order-errors");
                }

                return Redirect("/order-success");
            }

            return View(orderViewModel);
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }

        public ActionResult OrderErrors()
        {
            return View();
        }
        public ActionResult NotLoggedIn()
        {
            return View();
        }
        public ActionResult OutOfStock()
        {
            return View();
        }
    }
}