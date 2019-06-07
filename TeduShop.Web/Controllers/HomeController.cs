using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _Category()
        {
            return PartialView();
        }
    }
}