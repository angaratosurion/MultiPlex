using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MultiPlex.Core;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;
using MultiPlex.Core.Managers;

namespace MultiPlex.Web.Controllers
{

    //[Export("Title", typeof(IController))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    //public class TitleController : Controller
    //{
    //    TitleManager tmngr = new TitleManager();
    //    // GET: Category
    //    //public ActionResult Index(string wid, int catid)
    //    //{
    //    //    try
    //    //    {
    //    //        if (CommonTools.isEmpty(wid))
    //    //        {
    //    //            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

    //    //        }
    //    //        Category cat=null;
    //    //        if (cat == null)
    //    //        {
    //    //            return HttpNotFound();
    //    //        }
    //    //        return View(cat);
    //    //    }
    //    //    catch (Exception ex)
    //    //    {

    //    //        CommonTools.ErrorReporting(ex);
    //    //        return null;
    //    //    }
    //    //}
    //    public ActionResult Index(string wikiname, int catid)
    //    {
    //        try
    //        {
    //            if (CommonTools.isEmpty(wikiname))
    //            {
    //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

    //            }
    //            List<Title> titles = this.tmngr.GetTitlesbyCategory(wikiname, catid);
    //            if (titles == null)
    //            {
    //                return HttpNotFound();
    //            }
    //            return View(titles);
    //        }
    //        catch (Exception ex)
    //        {

    //            CommonTools.ErrorReporting(ex);
    //            return null;
    //        }
    //    }
    //    //    [Authorize]
    //    //    public ActionResult Create()
    //    //    {
    //    //        try
    //    //        {
    //    //            return View();
    //    //        }
    //    //        catch (Exception ex)
    //    //        {

    //    //            CommonTools.ErrorReporting(ex);
    //    //            return null;
    //    //        }

    //    //    }

    //    //    [HttpPost]
    //    //    [ValidateAntiForgeryToken]
    //    //    public ActionResult Create(Title title)
    //    //    {
    //    //        try
    //    //        {
    //    //            if (ModelState.IsValid)
    //    //            {

    //    //            }
    //    //            return title;
    //    //        }
    //    //        catch (Exception ex)
    //    //        {

    //    //            CommonTools.ErrorReporting(ex);
    //    //            return null;
    //    //        }
    //    //    }
    //    //}
    //}
}