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
                return View(wk);

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
            if (usr != null)
            {
                // if (ModelState.IsValid)
                {
                    
              
                    wk.Moderators = new List<ApplicationUser>();
                    wk.Moderators.Add(usr);
                    
                    wmngr.CreateWiki(wk);

                }
                return RedirectToAction("Index");
            }

           return View(wk);
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