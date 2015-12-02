using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MultiPlex.Core;

namespace MultiPlex.Web.Controllers
{
    [Export("File", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
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
    }
}