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

namespace ElectronicStore.Web.Controllers
{
    public class CartController : Controller
    {
        private IProductService productService;
        private IOrderService orderService;
        private ApplicationUserManager userManager;
        public CartController(IProductService productService, IOrderService orderService, ApplicationUserManager userManager)
        {
            this.productService = productService;
            this.orderService = orderService;
            this.userManager = userManager;
        }
        // GET: ShopingCart
        public ActionResult Index()
        {
            var cart = Session[ConfigurationManager.AppSettings["CartSession"].ToString()];
            var listItem = cart != null ? (List<CartItemViewModel>)cart : new List<CartItemViewModel>();
            return View(listItem);
        }

        public ActionResult AddItem()
        {if (!Request.IsAuthenticated)
            {
                return Redirect("/chua-dang-nhap");
            }
            
            return View();
        }
    }
}