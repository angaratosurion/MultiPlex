using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MultiPlex.Core;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;
using MultiPlex.Core.Managers;

namespace MultiPlex.Web.Controllers
{
    public class TitleController : Controller
    {
        TitleManager tmngr = new TitleManager();
        // GET: Category
        //public ActionResult Index(string wid, int catid)
        //{
        //    try
        //    {
        //        if (CommonTools.isEmpty(wid))
        //        {
        //            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //        }
        //        Category cat=null;
        //        if (cat == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(cat);
        //    }
        //    catch (Exception ex)
        //    {

        //        CommonTools.ErrorReporting(ex);
        //        return null;
        //    }
        //}
        public ActionResult Details(string wid,int catid)
        {
            try
            {
                if (CommonTools.isEmpty(wid))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                List<Title> titles = this.tmngr.GetTitlesbyCategory(wid, catid);
                if (titles== null)
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
    }
}