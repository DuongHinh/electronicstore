using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicStore.Web.Controllers
{
    public class ShopingCartController : Controller
    {
        // GET: ShopingCart
        public ActionResult Index()
        {
            return View();
        }
    }
}