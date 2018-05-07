using ElectronicStore.Service;
using ElectronicStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService productCategoryService;
        private IProductService productService;

        public HomeController(IProductCategoryService productCategoryService, IProductService productService)
        {
            this.productCategoryService = productCategoryService;
            this.productService = productService;
        }
        // GET: Home
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel();
            var newArrivalProductModel = this.productService.GetNewArrival(4);
            var hotProductModel = this.productService.GetHotProduct(4);
            var newArrivalProductViewModel = newArrivalProductModel.Select(p => new ProductViewModel() {
                Id = p.Id,
                Name = p.Name,
                Alias = p.Alias,
                CategoryId = p.CategoryId,
                Image = p.Image,
                MoreImages = p.MoreImages,
                Price = p.OriginalPrice,
                Quantity = p.Quantity,
                PromotionPrice = p.PromotionPrice,
                Warranty = p.Warranty,
                Description = p.Description,
                Detail = p.Detail,
                HomeFlag = p.HomeFlag,
                HotFlag = p.HotFlag,
                ViewCount = p.ViewCount,
                CreatedDate = p.CreatedDate,
                CreatedBy = p.CreatedBy,
                UpdatedDate = p.UpdatedDate,
                UpdatedBy = p.UpdatedBy,
                Status = p.Status,
                Tags = p.Tags
            });

            var hotProductViewModel = hotProductModel.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Alias = p.Alias,
                CategoryId = p.CategoryId,
                Image = p.Image,
                MoreImages = p.MoreImages,
                Price = p.OriginalPrice,
                Quantity = p.Quantity,
                PromotionPrice = p.PromotionPrice,
                Warranty = p.Warranty,
                Description = p.Description,
                Detail = p.Detail,
                HomeFlag = p.HomeFlag,
                HotFlag = p.HotFlag,
                ViewCount = p.ViewCount,
                CreatedDate = p.CreatedDate,
                CreatedBy = p.CreatedBy,
                UpdatedDate = p.UpdatedDate,
                UpdatedBy = p.UpdatedBy,
                Status = p.Status,
                Tags = p.Tags
            });

            homeViewModel.NewArrivalProducts = newArrivalProductViewModel;
            homeViewModel.HotProducts = hotProductViewModel;

            return View(homeViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = this.productCategoryService.GetAll();
            var viewModel = model.Select(c => new ProductCategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Alias = c.Alias,
                Description = c.Description,
                ParentId = c.ParentId,
                DisplayOrder = c.ParentId,
                Image = c.Image,
                HomeFlag = c.HomeFlag,
                CreatedDate = c.CreatedDate,
                CreatedBy = c.CreatedBy,
                UpdatedDate = c.UpdatedDate,
                UpdatedBy = c.UpdatedBy,
                Status = c.Status
            });
            return PartialView(viewModel);
        }
    }
}