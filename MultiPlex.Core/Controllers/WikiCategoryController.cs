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
using MultiPlex.Core.Data.ViewModels;
using MultiPlex.Core.Managers;

namespace MultiPlex.Core.Controllers
{
    [Export("WikiCategory", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class WikiCategoryController : Controller
    {
        CategoryManager catmngr = new CategoryManager();
        WikiUserManager usremngr = new WikiUserManager();
        WikiManager wkmngr = CommonTools.wkmngr;
        TitleManager tmngr = new TitleManager();

        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            try
            {
                List<WikiCategory> cats = this.catmngr.GetCategories();
                return View(cats);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }

        }
        public ActionResult CategoriesByWiki(string wikiname)
        {
            try
            {


                if (CommonTools.isEmpty(wikiname))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                List<WikiCategory> cats = catmngr.GetCategoryListByWiki(wikiname);
                if (cats == null)
                {
                    return HttpNotFound();
                }


                return View(cats);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult Details(string wikiname, int id)
        {
            try
            {


                if (CommonTools.isEmpty(wikiname))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                WikiCategory cat = catmngr.GetCategoryById(id);
                if (cat == null)
                {
                    return HttpNotFound();
                }

                ViewCategoryTitles modl = new ViewCategoryTitles();
                modl.Category = cat;
                List<WikiTitle> titles = this.tmngr.GetTitlesbyCategory(wikiname, id);
                if (titles == null)
                {
                    titles = new List<WikiTitle>();
                }
                modl.Titles = titles;
                return View(modl);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [Authorize]
        public ActionResult EditCategory(string wikiname, int? id)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && Convert.ToInt32(id) <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }

                Wiki wk = this.wkmngr.GetWiki(wikiname);
                if (wk == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                if (usremngr.UserHasAccessToWiki(this.usremngr.GetUser(this.User.Identity.Name), wk, true) == false)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                WikiCategory cat = this.catmngr.GetCategoryById(Convert.ToInt32(id));
                if (cat == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                return View(cat);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(WikiCategory cat, string wikiname, int id)
        {

            try
            {

                if (cat != null)
                {
                    this.catmngr.EditCatrgory(wikiname, id, cat);
                    RouteValueDictionary vals = new RouteValueDictionary();
                    vals.Add("wikiname", wikiname);
                    vals.Add("id", id);

                    return RedirectToAction("Details", vals);
                }

                return View(cat);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }


        }
        [Authorize]
        public ActionResult Delete(string wikiname, int id)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) == true && id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                WikiCategory cat = this.catmngr.GetCategoryById(id);
                if (cat == null)
                {
                    return HttpNotFound();
                }

                ViewCategoryTitles modl = new ViewCategoryTitles();
                modl.Category = cat;
                List<WikiTitle> titles = this.tmngr.GetTitlesbyCategory(wikiname, id);
                if (titles == null)
                {
                    titles = new List<WikiTitle>();
                }
                modl.Titles = titles;

                return View(modl);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        // POST: ProjectNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string wikiname, int id)
        {

            try
            {

                if (CommonTools.isEmpty(wikiname) == true && id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                WikiCategory cat = this.catmngr.GetCategoryById(id);
                if (cat != null)
                {
                    this.catmngr.DeleteCatrgory(id);
                }
                RouteValueDictionary vals = new RouteValueDictionary();
                vals.Add("wikiname", wikiname);
                return RedirectToAction("CategoriesByWiki", vals);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [Authorize]
        public ActionResult AddTitleToCategory(string wikiname, int tid)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }

                WikiTitle title = this.tmngr.GetTitlebyId(wikiname, tid);
                if (title == null)
                {
                    return HttpNotFound();
                }
                ViewTitleCategories mod = new ViewTitleCategories();
                mod.Title = title;
                mod.Categories = this.catmngr.GetCategoryListByWiki(wikiname);
                return View(mod);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddTitleToCategory(string wikiname, ViewTitleCategories model, int tid)
        {
            try
            {
                int catid = 0;
                string catname;
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }

                WikiTitle title = this.tmngr.GetTitlebyId(wikiname, tid);
                if (title == null)
                {
                    return HttpNotFound();
                }
                if (tid > 0 && model != null && model.CategoryToAddOrRemove != null)
                //&& CommonTools.isEmpty( con["UserToAdd"])==false)
                {
                    catname = model.CategoryToAddOrRemove.Title;
                    WikiCategory cat = this.catmngr.GetCategoryByTitle(catname, wikiname);
                    if (cat==null)
                    {
                        return HttpNotFound();
                    }
                    catid = cat.Id;
                    if (catid > 0)
                    {
                        this.catmngr.AddTitleToCategory(wikiname, catid, title);
                    }

                }
                RouteValueDictionary vals = new RouteValueDictionary();
                vals.Add("wikiname", wikiname);
                vals.Add("cid", catid);
                return RedirectToAction("Index", "WikiContent", vals);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [Authorize]
        public ActionResult RemoveTitleFromCategory(string wikiname, int tid)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }

                WikiTitle title = this.tmngr.GetTitlebyId(wikiname, tid);
                if (title == null)
                {
                    return HttpNotFound();
                }
                ViewTitleCategories mod = new ViewTitleCategories();
                mod.Title = title;
                mod.Categories = this.catmngr.GetCategoryListByWiki(wikiname);
                return View(mod);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult RemoveTitleFromCategory(string wikiname, ViewTitleCategories model, int tid)
        {
            try
            {
                int catid = 0;
                string catname;
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }

                WikiTitle title = this.tmngr.GetTitlebyId(wikiname, tid);
                if (title == null)
                {
                    return HttpNotFound();
                }
                if (tid > 0 && model != null && model.CategoryToAddOrRemove != null)
                //&& CommonTools.isEmpty( con["UserToAdd"])==false)
                {
                    catname = model.CategoryToAddOrRemove.Title;
                    WikiCategory cat = this.catmngr.GetCategoryByTitle(catname, wikiname);
                    if (cat == null)
                    {
                        return HttpNotFound();
                    }
                    catid = cat.Id;
                    if (catid > 0)
                    {
                        this.catmngr.RemoveTitleFromCategory(wikiname, catid, title);
                    }

                }
                RouteValueDictionary vals = new RouteValueDictionary();
                vals.Add("wikiname", wikiname);
                vals.Add("cid", catid);
                return RedirectToAction("Index", "WikiContent", vals);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }

}