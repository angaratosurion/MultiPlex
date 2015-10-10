﻿using System;
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
        public ActionResult CreateContent(string wikiname, int cid,Tuple<Title,Content> tuple)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) == false && (cid > 0) && tuple !=null 
                    && tuple.Item1 !=null && tuple.Item2!=null)
                {
                    Category cat = this.catmngr.GetCategoryListById(cid);
                    Wiki wk = wkmngr.GetWiki(wikiname);
                    ApplicationUser usr = this.usrmng.GetUser(this.User.Identity.Name);
                     if ( wk !=null && cat !=null && usr !=null)
                    {
                        tuple.Item1.Categories = new List<Category>();
                        tuple.Item1.Categories.Add(cat);
                        tuple.Item1.Wiki = wk;
                        tuple.Item1.WrittenBy = usr;
                        
                        tuple.Item2.Title = tuple.Item1;
                        tuple.Item2.Version = 1;
                        tuple.Item2.Wiki = wk;
                        tuple.Item2.WrittenBy = usr;
                        tuple.Item2.VersionDate = DateTime.Now;

                        return RedirectToAction("Index");
                    }
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
                    return RedirectToAction("CreateContent", "Content", vals);
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
                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

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
                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

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
                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

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
                contmngr = new ContentManager(new WikiEngine(), this.Url, wikiname);

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
