﻿using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}