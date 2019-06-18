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
    public class PageController : Controller
    {
        IPageService _pageService;
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        
        [OutputCache(Duration = 3600 * 24, Location = System.Web.UI.OutputCacheLocation.Server, VaryByParam = "alias")]
        public ActionResult Index(string alias)
        {
            var page = _pageService.GetByAlias(alias);
            var pageViewModel = Mapper.Map<Page, PageViewModel>(page);
            _pageService.IncreaseViewCount(alias);
            _pageService.Save();

            return View(pageViewModel);
        }
    }
}