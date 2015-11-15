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
        UserManager usrmngr = new UserManager();
        public HomeWikiController()
        {
            wmngr = new WikiManager();
        }
        // GET: HomeWiki
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
        public ActionResult Details(string id)
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
        public ActionResult ListCategories(string wikiname, int cid)
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
                return RedirectToAction("CreateCategory", "Content", vals);

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
            ApplicationUser usr =null ;
            string ttusr = this.User.Identity.Name;
            usr=this.usrmngr.GetUser(ttusr);
            wk.Administrator = usr;
          if (ModelState.IsValid)
            {
                
                if (usr != null)
                {
                    wk.Moderators = new List<ApplicationUser>();
                    wk.Moderators.Add(usr);
                    
                    wmngr.CreateWiki(wk);

                }
                return RedirectToAction("Index");
            }

            return View(wk);
        }
        [Authorize]
        public ActionResult EditWiki(string id)
        {
            string wikiname = id;
            if (CommonTools.isEmpty(wikiname))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            return View();
           
           
        }
        [Authorize]
        public ActionResult EditBasicInfo(string id)
        {
            string wikiname = id;
            if (CommonTools.isEmpty(wikiname))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Wiki wk = this.wmngr.GetWiki(wikiname);
             if ( wk==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
             if ( usrmngr.UserHasAccessToWiki(this.usrmngr.GetUser(this.User.Identity.Name),wk,true)==false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
           
            return View(wk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBasicInfo(Wiki wk,string id)
        {
            string wikiname = id;
            //  Wiki wk2 = this.wmngr.GetWiki(wikiname);
            //  wk.Name = wk2.Name;
            wikiname = wk.Name;
         // if (ModelState.IsValid)
            {
                wk=this.wmngr.EditBasicInfo(wk, wikiname);
                return RedirectToAction("Index");
            }
            return View(wk);
        }

    }
}