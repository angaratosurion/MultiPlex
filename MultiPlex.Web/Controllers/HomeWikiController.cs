using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MultiPlex.Core;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Managers;

namespace MultiPlex.Web.Controllers
{
    [Export("HomeWiki", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeWikiController : Controller
    { WikiManager wmngr;
        public HomeWikiController()
        {
            wmngr = new WikiManager(this.Server);
        }
        // GET: Home
        public ActionResult Index()
        {
            try
            {


                //  List<WikiModel> wikis = this.wmngr.ListWiki();
                return View(this.wmngr.ListWiki());
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }


        }
        public ActionResult Detais(string id)
        {
            try
            {
                if ( CommonTools.isEmpty(id))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                Wiki wk = this.wmngr.GetWiki(id);
                if ( wk==null)
                {
                    return HttpNotFound();
                }
                return View(wk);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public ActionResult Categories(string wikiname, int cid)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && cid>=0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                //   RouteDataValueProvider
               RouteValueDictionary vals = new RouteValueDictionary();
                vals.Add("wikiname", wikiname);
                vals.Add("cid", cid);
              return  RedirectToAction("Index", "Content", vals);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Wiki wk)
        {
            if (ModelState.IsValid)
            {


                wmngr.CreateWiki(wk);

                return RedirectToAction("Index");
            }

            return View(wk);
        }
    }
}