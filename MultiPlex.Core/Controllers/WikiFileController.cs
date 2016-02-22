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
    [Export("WikiFile", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WikiFileController : Controller
    {
        FileManager filemngr = new FileManager();
        // GET: File
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            try {

                List<WikiFile> lst = this.filemngr.GetFiles();
                List<ViewWikiFile> vlist = new List<ViewWikiFile>();
                foreach ( var v in lst)
                {
                    ViewWikiFile vf = new ViewWikiFile();
                    vf.ImportFromModel(v);
                    vlist.Add(vf);

                }
                return View(vlist);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [Authorize]
        public ActionResult AddFileToTitle(string wikiname,int tid)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                WikiTitle title = CommonTools.titlemngr.GetTitlebyId(wikiname, tid);
                Wiki wk = CommonTools.wkmngr.GetWiki(wikiname);
                 if (wk==null  )
                {
                    return HttpNotFound();
                }
                 if(title == null)
                {
                    return HttpNotFound();
                }
                ViewTitleFile mod = new ViewTitleFile();
                mod.File = new WikiFile();
                mod.Title = title;
                mod.ToBeAdded = true;


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
        public ActionResult AddFileToTitle(string wikiname, int tid,HttpPostedFileBase file, ViewTitleFile mod)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mod.File = new WikiFile();
                WikiTitle title = CommonTools.titlemngr.GetTitlebyId(wikiname, tid);
                mod.Title = title;
                if ( mod !=null && mod.File !=null && mod.Title !=null && file.ContentLength>0)
                {
                    WikiFile fmod = mod.File;
                    

                    this.filemngr.AddFile(wikiname, mod.File, file, tid, CommonTools.usrmng.GetUser(this.User.Identity.Name));
                }
                return View(mod);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult GetTitleFiles(string wikiname,int tid)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                List<WikiFile> lst = this.filemngr.GetFilesByTitle(wikiname, tid);
                 if ( lst== null && CommonTools.wkmngr.GetWiki(wikiname)==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                List<ViewWikiFile> vlist = new List<ViewWikiFile>();
                foreach (var v in lst)
                {
                    ViewWikiFile vf = new ViewWikiFile();
                    vf.ImportFromModel(v);
                    vlist.Add(vf);

                }

                return View(vlist);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult Gallery(string wikiname, int tid)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                List<WikiFile> lst = this.filemngr.GetImageFilesByTitle(wikiname, tid);
                if (lst == null && CommonTools.wkmngr.GetWiki(wikiname) == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }


                List<ViewWikiFile> vlist = new List<ViewWikiFile>();
                foreach (var v in lst)
                {
                    ViewWikiFile vf = new ViewWikiFile();
                    vf.ImportFromModel(v);
                    vlist.Add(vf);

                }
                return View(vlist);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult GetFilesByWiki(string wikiname)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) )
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                List<WikiFile> lst = this.filemngr.GetFilesByWiki(wikiname);
                if (lst == null && CommonTools.wkmngr.GetWiki(wikiname) == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                List<ViewWikiFile> vlist = new List<ViewWikiFile>();
                foreach (var v in lst)
                {
                    ViewWikiFile vf = new ViewWikiFile();
                    vf.ImportFromModel(v);
                    vlist.Add(vf);

                }

                return View(vlist);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult GetImageFilesByWiki(string wikiname)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                List<WikiFile> lst = this.filemngr.GetImageFilesByWiki(wikiname);
                if (lst == null && CommonTools.wkmngr.GetWiki(wikiname) == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }


                List<ViewWikiFile> vlist = new List<ViewWikiFile>();
                foreach (var v in lst)
                {
                    ViewWikiFile vf = new ViewWikiFile();
                    vf.ImportFromModel(v);
                    vlist.Add(vf);

                }
                return View(vlist);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult Details(string wikiname, int fid)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && fid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                WikiFile lst = this.filemngr.GetFileById(wikiname, fid);
                if (lst == null && CommonTools.wkmngr.GetWiki(wikiname) == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }


                ViewWikiFile vlst = new ViewWikiFile();
                vlst.ImportFromModel(lst);
                return View(vlst);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        [Authorize]
        public ActionResult Delete(string wikiname, int fid)
        {
            try
            {
                if (fid<=0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                WikiFile file = this.filemngr.GetFileById(wikiname, fid);
                if (file == null)
                {
                    return HttpNotFound();
                }

                ViewWikiFile vfile = new ViewWikiFile();
                vfile.ImportFromModel(file);

                return View(vfile);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        // POST: ProjectNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string wikiname, int fid)
        {

            try
            {
              
                if (fid <=0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
               
                  
                     this.filemngr.DeleteFileById(wikiname, fid,CommonTools.usrmng.GetUser(this.User.Identity.Name));
              
                RouteValueDictionary vals = new RouteValueDictionary();
                vals.Add("wikiname", wikiname);
               
                return RedirectToAction("Details","HomeWiki", vals);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

    }
}