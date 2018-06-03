using ElectronicStore.Service;
using ElectronicStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService productCategoryService;
        private IProductService productService;
        private IContactService contactService;
        private IBrandService brandService;
        private INewsCategoryService newsCategoryService;
        public HomeController(IProductCategoryService productCategoryService, IProductService productService, IContactService contactService, IBrandService brandService, INewsCategoryService newsCategoryService)
        {
            this.productCategoryService = productCategoryService;
            this.productService = productService;
            this.contactService = contactService;
            this.brandService = brandService;
            this.newsCategoryService = newsCategoryService;
        }
        // GET: Home
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel();
            var newArrivalProductModel = this.productService.GetNewArrival(4);
            var promotionProductModel = this.productService.GetPromotionProduct(4);
            var brandsModel = this.brandService.GetAll();
            var newArrivalProductViewModel = newArrivalProductModel.Select(p => new ProductViewModel() {
                Id = p.Id,
                Name = p.Name,
                Alias = p.Alias,
                CategoryId = p.CategoryId,
                Image = p.Image,
                MoreImages = p.MoreImages,
                Price = p.Price,
                Quantity = p.Quantity,
                PromotionPrice = p.PromotionPrice,
                Description = p.Description,
                HotFlag = p.HotFlag,
                ViewCount = p.ViewCount,
                CreatedDate = p.CreatedDate,
                CreatedBy = p.CreatedBy,
                UpdatedDate = p.UpdatedDate,
                UpdatedBy = p.UpdatedBy,
                Status = p.Status
            });

            var promotionProductViewModel = promotionProductModel.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Alias = p.Alias,
                CategoryId = p.CategoryId,
                Image = p.Image,
                MoreImages = p.MoreImages,
                Price = p.Price,
                Quantity = p.Quantity,
                PromotionPrice = p.PromotionPrice,
                Description = p.Description,
                HotFlag = p.HotFlag,
                ViewCount = p.ViewCount,
                CreatedDate = p.CreatedDate,
                CreatedBy = p.CreatedBy,
                UpdatedDate = p.UpdatedDate,
                UpdatedBy = p.UpdatedBy,
                Status = p.Status
            });

            var brandsViewModel = brandsModel.Select(b => new BrandViewModel()
            {
                Id = b.Id,
                Name = b.Name,
                Logo = b.Logo,
                Description = b.Description,
                Alias = b.Alias,
                Status = b.Status,
            });

            homeViewModel.NewArrivalProducts = newArrivalProductViewModel;
            homeViewModel.HotProducts = promotionProductViewModel;
            homeViewModel.Brands = brandsViewModel;

            return View(homeViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var cart = Session[ConfigurationManager.AppSettings["CartSession"].ToString()];
            var list = new List<CartItemViewModel>();
            if (cart != null)
            {
                list = (List<CartItemViewModel>)cart;
            }
            return PartialView(list);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var footerViewModel = new FooterViewModel();
            var productCategoriesModel = this.productCategoryService.GetAll();
            var contactModel = this.contactService.GetInforContact();

            var productCategoriesViewModel = productCategoriesModel.Select(c => new ProductCategoryViewModel()
            {
               Id = c.Id,
               Name = c.Name,
               Alias = c.Alias,
               Description = c.Description,
               ParentId = c.ParentId,
               DisplayOrder = c.DisplayOrder,
               Image = c.Image,
               HomeFlag = c.HomeFlag,
               CreatedDate = c.CreatedDate,
               CreatedBy = c.CreatedBy,
               UpdatedDate = c.UpdatedDate,
               UpdatedBy = c.UpdatedBy,
               Status = c.Status
            });

            var contactViewModel = new ContactViewModel();
            contactViewModel.Id = contactModel.Id;
            contactViewModel.Name = contactModel.Name;
            contactViewModel.Address = contactModel.Address;
            contactViewModel.Email = contactModel.Email;
            contactViewModel.PhoneNumber = contactModel.PhoneNumber;
            contactViewModel.Fax = contactModel.Fax;
            contactViewModel.Other = contactModel.Other;
            contactViewModel.Status = contactModel.Status;

            footerViewModel.ContactInfo = contactViewModel;
            footerViewModel.ProductCategories = productCategoriesViewModel;

            return PartialView(footerViewModel);
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

        [ChildActionOnly]
        public ActionResult NewsCategory()
        {
            var model = this.newsCategoryService.GetAll();
            var viewModel = model.Select(c => new NewsCategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Alias = c.Alias,
                Description = c.Description,
                ParentId = c.ParentId,
                Image = c.Image,
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