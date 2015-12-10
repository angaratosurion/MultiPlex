using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MultiPlex.Core;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.ViewModels;
using MultiPlex.Core.Managers;

namespace MultiPlex.Web.Controllers
{
    [Export("File", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileController : Controller
    {
        FileManager filemngr = new FileManager();
        // GET: File
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            try {

                List<WikiFile> lst = this.filemngr.GetFiles();
                return View(lst);
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



                return View(lst);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult GetFilesByWiki(string wikiname, int tid)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                List<WikiFile> lst = this.filemngr.GetFilesByWiki(wikiname);
                if (lst == null && CommonTools.wkmngr.GetWiki(wikiname) == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }



                return View(lst);

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }

    }
}