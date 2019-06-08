using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        ICommonService _commonService;
        IProductService _productService;

        public HomeController(IProductCategoryService productCategoryService, ICommonService commonService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;
        }

        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();
            var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var latestProductModel = _productService.GetLatest(3);
            var topsaleProductModel = _productService.GetHotProduct(3);
            var latestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(latestProductModel);
            var topsaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topsaleProductModel);

            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideViewModel;
            homeViewModel.LatestProducts = latestProductViewModel;
            homeViewModel.TopSaleProducts = topsaleProductViewModel;

            return View(homeViewModel);
        }

        [ChildActionOnly]
        public ActionResult _Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult _Category()
        {
            var model = _productCategoryService.GetAll().Where(m => m.Status == true && m.HomeFlag == true).OrderBy(m => m.DisplayOrder).ThenByDescending(m => m.CreatedDate);
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}