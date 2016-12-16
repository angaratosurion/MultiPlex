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
using BlackCogs.Data.Models;
using MultiPlex.Core.Data.ViewModels;

namespace MultiPlex.Core.Controllers
{
    [Export("HomeWiki", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeWikiController : Controller
    { WikiManager wmngr;
        WikiUserManager usrmngr = new WikiUserManager();
        public HomeWikiController()
        {
            wmngr = new WikiManager();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // GET: HomeWiki
        public ActionResult Index()
        {
            try
            {


                List<Wiki> wikis = this.wmngr.ListWiki();
                List<ViewWiki> wkv = new List<ViewWiki>();
                foreach ( var w in wikis)
                {
                    ViewWiki v = new ViewWiki();
                    v.ImportFromModel(w);
                    wkv.Add(v);
                }

                return View(wkv);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }


        }
        public ActionResult Details(string wikiname)
        {
            try
            {
                string id = wikiname;
                if ( CommonTools.isEmpty(id))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                Wiki wk = this.wmngr.GetWiki(id);
                if ( wk==null)
                {
                    return HttpNotFound();
                }
                ViewWiki vwk = new ViewWiki();
                vwk.ImportFromModel(wk);
                return View(vwk);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public ActionResult ListTitles(string wikiname, int cid)
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
              return  RedirectToAction("Index", "WikiContent", vals);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public ActionResult ListWikiTitles(string wikiname)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                //   RouteDataValueProvider
                RouteValueDictionary vals = new RouteValueDictionary();
                vals.Add("wikiname", wikiname);
                return RedirectToAction("TitlesByWiki", "WikiContent", vals);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public ActionResult CreateCategories(string wikiname)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                //   RouteDataValueProvider
                RouteValueDictionary vals = new RouteValueDictionary();
                vals.Add("wikiname", wikiname);
                return RedirectToAction("CreateCategory", "WikiContent", vals);

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
        public ActionResult Create(ViewWiki vwk)
        {
            string ttusr = this.User.Identity.Name;

            if (ttusr != null)
            {
                // if (ModelState.IsValid)
                {

                    vwk.Administrator = CommonTools.usrmng.GetUser(ttusr);
                    Wiki wk = vwk.ExportToModel();
                     
                    
                    wmngr.CreateWiki(wk,ttusr);

                }
                return RedirectToAction("Index");
            }

           return View(vwk);
        }
        [Authorize]
        public ActionResult CreateWikiByExternSrc(string newwikiname)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWikiByExternSrc(ViewWiki vwk, string newwikiname)
        {
            ApplicationUser usr = null;
            string ttusr = this.User.Identity.Name;
           
            if (ttusr != null)
            {
                // if (ModelState.IsValid)
                {



                    Wiki wk = vwk.ExportToModel();
                    wmngr.CreateWiki(wk,ttusr);

                }
                return RedirectToAction("Index");
            }

            return View(vwk);
        }
        public ActionResult EditWiki(string wikiname)
        {
            
            if (CommonTools.isEmpty(wikiname))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            RouteValueDictionary vals = new RouteValueDictionary();
            vals.Add("wikiname", wikiname);
            return RedirectToAction("EditWiki", "WikiManager", vals);

            


        }
        public ActionResult About()
        {
            try
            {


                //  List<WikiModel> wikis = this.wmngr.ListWiki();
                return View();
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }


        }

    }
}