using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MultiPlex.Core;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;
using MultiPlex.Core.Managers;

namespace MultiPlex.Web.Controllers
{
    [Export("Content", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ContentController : Controller
    {
        UserManager usrmng = new UserManager();
        TitleManager tmngr = new TitleManager();
        ContentManager contmngr;
        public ContentController()
        {
            contmngr = new ContentManager(new WikiEngine(), this.Url, this.RouteData.Values["wid"].ToString());
        }
        public ActionResult Index(string wid, int catid)
        {
            try
            {
                if (CommonTools.isEmpty(wid))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                List<Title> titles = this.tmngr.GetTitlesbyCategory(wid, catid);
                if (titles == null)
                {
                    return HttpNotFound();
                }
                return View(titles);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public ActionResult ViewWiki(string wikiname, int id, string slug)
        {
            try
            {

                if ( CommonTools.isEmpty(wikiname) && CommonTools.isEmpty(slug) && id <=0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var viewData =this.contmngr.ViewWiki(wikiname, id, slug);
                if (viewData.Content == null)
                    return RedirectToAction("EditWiki", new { id, slug });

                return View("View", viewData);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

        public ActionResult ViewWikiVersion(string wikiname, int id, string slug,
            int version)
        {
            try
            {
                var viewData = this.contmngr.ViewWikiVersion(wikiname, id, slug, version);

                if (viewData.Content == null)
                    return RedirectToAction("ViewWiki", new { wikiname, id, slug });



                return View("View", viewData);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        [Authorize]
        public ActionResult EditWiki(string wikiname, int id, string slug)
        {
            try
            {
                if (!ContentManager.IsEditable())
                    return RedirectToAction("ViewWiki");
                Content content = this.contmngr.GetWikiforEditWiki(wikiname, id, slug);




                return View("Edit", content);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }


        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditWiki(string wikiname, int id, string slug,
            string name,
            string source)
        {
            try
            { 
                if (!ContentManager.IsEditable())
                    return RedirectToAction("ViewWiki");

                id = this.contmngr.EditWikiPost(wikiname, id, slug, name, source, this.usrmng.GetUser(this.User.Identity.Name));
                return RedirectToAction("ViewWiki", new { wikiname, id, slug });
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(Location = OutputCacheLocation.None)]
        public string GetWikiSource(string wikiname, int id, string slug, int version)
        {
            try {
                Content content = this.contmngr.GetWikiSource(wikiname, id, slug, version);

                return content.Source;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [OutputCache(Location = OutputCacheLocation.None)]
        [ValidateInput(false)]
        public string GetWikiPreview(int id, string slug, string source)
        {
            try
            {
                return this.contmngr.GetWikiPreview(id, slug, source);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
    }



    }
