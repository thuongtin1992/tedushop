using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using TeduShop.Service;
using TeduShop.Common;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;
using AutoMapper;
using TeduShop.Model.Models;

namespace TeduShop.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        [OutputCache(Duration = 3600, VaryByParam = "id")]
        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetById(id);
            var productViewModel = Mapper.Map<Product, ProductViewModel>(productModel);

            #region view count
            _productService.IncreaseViewCount(id);
            _productService.Save();
            #endregion

            #region realted products
            var relatedProducts = _productService.GetRelatedProducts(id, 6);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProducts);
            #endregion

            #region more images
            var moreImages = productViewModel.MoreImages;
            if (moreImages != null)
            {
                List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(moreImages);
                ViewBag.MoreImages = listImages;
            }
            #endregion

            #region Tags
            var tagsViewModel = _productService.GetListTagByProductId(id); 
            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(tagsViewModel);
            #endregion

            return View(productViewModel);
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPages = (int)Math.Ceiling((double)(totalRow / pageSize));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPages = int.Parse(ConfigHelper.GetByKey("MaxPages")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };

            var category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);

            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPages = (int)Math.Ceiling((double)(totalRow / pageSize));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPages = int.Parse(ConfigHelper.GetByKey("MaxPages")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };

            ViewBag.Keyword = keyword;

            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetListProductByName(keyword);

            return Json(new {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListProductByTags(string tagId, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByTag(tagId, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPages = (int)Math.Ceiling((double)(totalRow / pageSize));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPages = int.Parse(ConfigHelper.GetByKey("MaxPages")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };

            ViewBag.Tag = Mapper.Map<Tag, TagViewModel>(_productService.GetTag(tagId));

            return View(paginationSet);
        }
    }
}