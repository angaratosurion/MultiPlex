﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiPlex.Core.Managers;

namespace MultiPlex.Web.Controllers
{
    [HandleError]
    [Export("Home", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : Controller
    {
        WikiManager wmngr = new WikiManager();
        // GET: Home
        public ActionResult Index()
        {
            var model = this.wmngr.ListWiki();
            return View(model.ToList());
        }
    }
}