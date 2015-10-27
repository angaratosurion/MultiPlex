using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using MultiPlex.Core;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;
using MultiPlex.Core.Data.ViewModels;
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
        CategoryManager catmngr = new CategoryManager();
        WikiManager wkmngr = new WikiManager();
       // WikiRepository rep = new WikiRepository();
        public ContentController()
        {

        }

        [Authorize]
        public ActionResult CreateCategory(string wikiname)
        {
            contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

            return View();
        }
        [Authorize]
        public ActionResult CreateContent(string wikiname,int cid)
        {
            try
            {
                Title title = new Title();
                Content content = new Content();
                
                return View(Tuple.Create(title,content));
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContent(string wikiname, int cid, [Bind(Prefix ="item1")]Title title,
            [Bind(Prefix ="item2")] Content cont)
        {
            try
            {

                if (CommonTools.isEmpty(wikiname) == false && (cid > 0)
                  && title != null && cont != null)
                {
                    contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);
                    ApplicationUser usr = CommonTools.usrmng.GetUser(this.User.Identity.Name);
                    this.contmngr.AddContent(wikiname, title, cont,cid,usr);

                    return RedirectToAction("ViewContent", new  {wikiname,title.Id,title.Slug });
                    }
                
                return RedirectToAction("CreateContent");
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category cat,string wikiname)
        {

            try
            {

                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

                if (cat != null && !CommonTools.isEmpty(wikiname))
                {
                  
                         Wiki wk=  this.wkmngr.GetWiki(wikiname);
                        if ( wk !=null)
                        {
                            
                            cat.Wiki = wk;
                           // if (ModelState.IsValid)
                            {
                                this.catmngr.Add(cat);
                                return RedirectToAction("Index");
                            }
                            

                        }
                    return RedirectToAction("CreateCategory");
                   
                    

                    
                }

                return View(cat);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

        public ActionResult Index(string wikiname, int? cid)
        {
            try
            {
                string wid = wikiname;
                int tcatid = Convert.ToInt32(cid);
                if (CommonTools.isEmpty(wid))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                contmngr = new ContentManager(new WikiEngine(), this.Url, wid);

                List<Title> titles = this.tmngr.GetTitlesbyCategory(wid, tcatid);
                if (titles == null)
                {
                    //return HttpNotFound();
                    RouteValueDictionary vals = new RouteValueDictionary();
                    vals.Add("wikiname", wikiname);
                    vals.Add("cid", cid);
                    //vals.Add("id", 0);
                    //vals.Add("slug", "");
                   return RedirectToAction("CreateContent", "Content", vals);
                  //  return RedirectToAction("EditContent", "Content", vals);
                }
                return View(titles);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public ActionResult ViewContent(string wikiname, int id, string slug)
        {
            try
            {

                if ( CommonTools.isEmpty(wikiname) && CommonTools.isEmpty(slug) && id <=0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

                var viewData =this.contmngr.ViewContent(wikiname, id, slug);
                if (viewData.Content == null)
                    return RedirectToAction("EditContent", new {wikiname, id, slug });

                return View("View", viewData);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

        public ActionResult ViewContentVersion(string wikiname, int id, string slug,
            int version)
        {
            try
            {
                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

                var viewData = this.contmngr.ViewContentVersion(wikiname, id, slug, version);

                if (viewData.Content == null)
                    return RedirectToAction("ViewContent", new { wikiname, id, slug });



                return View("View", viewData);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        [Authorize]
        public ActionResult EditContent(string wikiname, int? id, string slug)
        {
            try
            {
                if (!ContentManager.IsEditable()) { 
                    return RedirectToAction("ViewContent"); }
                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

                Content content = this.contmngr.GetContent(wikiname, Convert.ToInt32(id));
                content.Title = this.tmngr.GetTitlebyId(wikiname,Convert.ToInt32(id));
                EditContent cont = new EditContent();
                    cont.Source = content.Source;
                cont.Title = content.Title;
                content = null;
                return View("Edit", cont);
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
        public ActionResult EditContent(string wikiname, EditContent econt,int id)
        {
            try
            {
                Content cont = new Content();
                if (!ContentManager.IsEditable())
                {
                    return RedirectToAction("ViewContent");
                }
                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);
                if (cont != null)
                {
                    //int id = cont.Title.Id;
                    cont.Source = econt.Source;

                    
                     this.contmngr.EditContentPost(wikiname, cont, 
                        this.usrmng.GetUser(this.User.Identity.Name),id);
                    Content ct = this.contmngr.GetContent(wikiname, id);
                    return RedirectToAction("ViewContent", new { wikiname, id, ct.Title.Slug });
                }
                return View("Edit", econt);
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
                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

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
        public string GetWikiPreview(string wikiname,int id, string slug, string source)
        {
            try
            {
                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

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
