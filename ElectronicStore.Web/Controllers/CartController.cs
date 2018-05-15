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

namespace ElectronicStore.Web.Controllers
{
    public class CartController : Controller
    {
        private IProductService productService;
        private IOrderService orderService;
        private ApplicationUserManager userManager;
        private string SessionCart = ConfigurationManager.AppSettings["CartSession"].ToString();
        public CartController(IProductService productService, IOrderService orderService, ApplicationUserManager userManager)
        {
            this.productService = productService;
            this.orderService = orderService;
            this.userManager = userManager;
        }
        // GET: ShopingCart
        public ActionResult Index()
        {
            var cart = Session[SessionCart];
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
                Price = product.OriginalPrice,
                Quantity = product.Quantity,
                PromotionPrice = product.PromotionPrice,
                Warranty = product.Warranty,
                Description = product.Description,
                Detail = product.Detail,
                HomeFlag = product.HomeFlag,
                HotFlag = product.HotFlag,
                ViewCount = product.ViewCount,
                CreatedDate = product.CreatedDate,
                CreatedBy = product.CreatedBy,
                UpdatedDate = product.UpdatedDate,
                UpdatedBy = product.UpdatedBy,
                Status = product.Status,
                Tags = product.Tags
            };

            if (productViewModel.Quantity == 0)
            {
                return Redirect("/out-of-stock");
            }

            var cart = Session[SessionCart];
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
                   
                    Session[SessionCart] = list;
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
                
                Session[SessionCart] = list;
            }

            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItemViewModel>>(cartModel);
            var sessionCart = (List<CartItemViewModel>)Session[SessionCart];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);

                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }

            Session[SessionCart] = sessionCart;

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
            Session[SessionCart] = null;
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
            var sessionCart = (List<CartItemViewModel>)Session[SessionCart];
            if (sessionCart != null)
            {
                sessionCart.RemoveAll(x => x.Product.Id == id);
                Session[SessionCart] = sessionCart;
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

        public ActionResult Order(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = new Order();
                order.CustomerName = orderViewModel.CustomerName;
                order.CustomerEmail = orderViewModel.CustomerEmail;
                order.CustomerAddress = orderViewModel.CustomerAddress;
                order.CustomerPhone = orderViewModel.CustomerPhone;
                order.PaymentMethod = orderViewModel.PaymentMethod;
                order.OrderDate = DateTime.Now;

                try
                {
                    var cart = (List<CartItemViewModel>)Session[SessionCart];
                    List<OrderDetail> orderDetails = new List<OrderDetail>();
                    bool isEnough = true;
                    foreach (var item in cart)
                    {
                        var detail = new OrderDetail();
                        detail.ProductId = item.ProductId;
                        detail.Quantity = item.Quantity;
                        detail.Price = item.Product.Price;
                        orderDetails.Add(detail);

                        isEnough = this.productService.SellProduct(item.ProductId, item.Quantity);
                    }

                    this.orderService.CreateOrder(order, orderDetails);
                    this.productService.Save();

                    //send mail
                }
                catch (Exception)
                {

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