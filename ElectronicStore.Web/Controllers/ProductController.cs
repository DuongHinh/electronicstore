using ElectronicStore.Service;
using ElectronicStore.Web.Core;
using ElectronicStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ElectronicStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;
        private IProductCategoryService productCategoryService;
        private IBrandService brandService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService, IBrandService brandService)
        {
            this.productService = productService;
            this.productCategoryService = productCategoryService;
            this.brandService = brandService;
        }
        // GET: Product
        public ActionResult Detail(int id)
        {
            this.productService.IncreaseView(id);
            var productModel = this.productService.GetById(id);
            var productViewModel = new ProductViewModel() {
                Id = productModel.Id,
                Name = productModel.Name,
                Alias = productModel.Alias,
                CategoryId = productModel.CategoryId,
                Image = productModel.Image,
                MoreImages = productModel.MoreImages,
                Price = productModel.Price,
                Quantity = productModel.Quantity,
                PromotionPrice = productModel.PromotionPrice,
                Description = productModel.Description,
                HotFlag = productModel.HotFlag,
                ViewCount = productModel.ViewCount,
                CreatedDate = productModel.CreatedDate,
                CreatedBy = productModel.CreatedBy,
                UpdatedDate = productModel.UpdatedDate,
                UpdatedBy = productModel.UpdatedBy,
                Status = productModel.Status
            };

            var relatedProduct = this.productService.GetReatedProducts(id, 6);
            ViewBag.RelatedProducts = relatedProduct.Select(p => new ProductViewModel() {
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

            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(productViewModel.MoreImages);
            ViewBag.MoreImages = listImages;

            return View(productViewModel);
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = 6;
            int totalRow = 0;
            var productModel = this.productService.GetListProductByCategoryId(id, page, pageSize, sort, out totalRow);
            var productViewModel = productModel.Select(p => new ProductViewModel() {
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

            var category = this.productCategoryService.GetById(id);

            ViewBag.Category = new ProductCategoryViewModel() {
                Id = category.Id,
                Name = category.Name,
                Alias = category.Alias,
                Description = category.Description,
                ParentId = category.ParentId,
                DisplayOrder = category.DisplayOrder,
                Image = category.Image,
                HomeFlag = category.HomeFlag,
                CreatedDate = category.CreatedDate,
                CreatedBy = category.CreatedBy,
                UpdatedDate = category.UpdatedDate,
                UpdatedBy = category.UpdatedBy,
                Status = category.Status
            };

            var lstCategory = this.productCategoryService.GetAll();
            ViewBag.ListCategory = lstCategory.Select(c => new ProductCategoryViewModel()
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

            var lstBrand = this.brandService.GetAll();
            ViewBag.ListBrand = lstBrand.Select(b => new BrandViewModel()
            {
                Id = b.Id,
                Name = b.Name,
                Logo = b.Logo,
                Description = b.Description,
                Alias = b.Alias,
                Status = b.Status,
            });

            var paginationProduct = new Pagination<ProductViewModel>()
            {
                PageSize = pageSize,
                Skip = (page - 1) * pageSize,
                Results = productViewModel,
                MaxPage = 5,
                TotalResults = totalRow
            };

            return View(paginationProduct);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = 6;
            int totalRow = 0;
            var productModel = this.productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productViewModel = productModel.Select(p => new ProductViewModel()
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

            ViewBag.Keyword = keyword;

            var lstCategory = this.productCategoryService.GetAll();
            ViewBag.ListCategory = lstCategory.Select(c => new ProductCategoryViewModel()
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

            var lstBrand = this.brandService.GetAll();
            ViewBag.ListBrand = lstBrand.Select(b => new BrandViewModel()
            {
                Id = b.Id,
                Name = b.Name,
                Logo = b.Logo,
                Description = b.Description,
                Alias = b.Alias,
                Status = b.Status,
            });

            var paginationProduct = new Pagination<ProductViewModel>()
            {
                PageSize = pageSize,
                Skip = (page - 1) * pageSize,
                Results = productViewModel,
                MaxPage = 5,
                TotalResults = totalRow
            };

            return View(paginationProduct);
        }

        public ActionResult ListByTag(int tagId, int page = 1, string sort = "")
        {
            int pageSize = 6;
            int totalRow = 0;
            var productModel = this.productService.GetListProductByTag(tagId, page, pageSize, sort, out totalRow);
            var productViewModel = productModel.Select(p => new ProductViewModel()
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


            var paginationProduct = new Pagination<ProductViewModel>()
            {
                PageSize = pageSize,
                Skip = (page - 1) * pageSize,
                Results = productViewModel,
                MaxPage = 5,
                TotalResults = totalRow
            };

            return View(paginationProduct);
        }

        public JsonResult GetListNameProduct(string keyword)
        {
            var model = this.productService.GetListNameProduct(keyword);
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }
    }
}