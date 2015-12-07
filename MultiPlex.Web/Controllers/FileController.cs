using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MultiPlex.Core;
using MultiPlex.Core.Data.Models;
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
        public ActionResult CreateFile(string wikiname,int tid)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return View();
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
        public ActionResult CreateFile(string wikiname, int tid,HttpPostedFile file)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) && tid <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return View();
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