﻿using System;
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
using BlackCogs.Data.Models;
namespace MultiPlex.Core.Controllers
{
    [Export("WikiManager", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class WikiManagerController : Controller
    {
        WikiUserManager usremngr = new WikiUserManager();
        WikiManager wkmngr = CommonTools.wkmngr;
        WikiModeratorsInvitesManager wkmodinvngr = CommonTools.wkinvmngr;
        public ActionResult GetWikiUsers(string wikiname)
        {
            try
            {


                if (CommonTools.isEmpty(wikiname) == true)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                List<ApplicationUser> modusr;
                ApplicationUser adm = this.wkmngr.GetWikiAdministrator(wikiname);
                if (adm == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }
                ViewWikiUsers vwkus = new ViewWikiUsers();

                vwkus.Administrator = adm;
                if (this.wkmngr.GetWikiModerators(wikiname) == null)
                {
                    modusr = new List<ApplicationUser>();
                }
                else
                {
                    modusr= this.wkmngr.GetWikiModerators(wikiname); 
                }
                vwkus.Moderators = modusr;
                vwkus.Wiki = new ViewWiki();
                Wiki wk = this.wkmngr.GetWiki(wikiname);
                vwkus.Wiki.ImportFromModel(wk);

                return View(vwkus);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
      
        [Authorize]
        public ActionResult EditBasicInfo(string wikiname)
        {
            if (CommonTools.isEmpty(wikiname))
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
            ViewWiki vwk = new ViewWiki();
            vwk.ImportFromModel(wk);
            return View(vwk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBasicInfo(ViewWiki vwk, string wikiname)
        {
            Wiki wk = vwk.ExportToModel();
            //  Wiki wk2 = this.wmngr.GetWiki(wikiname);
            //  wk.Name = wk2.Name;
            wikiname = vwk.Name;
            wk.Administrator = this.usremngr.GetUser(this.User.Identity.Name).Id;
            // if (ModelState.IsValid)
            {
                wk = this.wkmngr.EditBasicInfo(wk, wikiname);
                return RedirectToAction("Index","HomeWiki");
            }
            return View(vwk);
        }
        [Authorize]
        public ActionResult EditWiki(string wikiname)
        {
            
            if (CommonTools.isEmpty(wikiname))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Wiki wk = this.wkmngr.GetWiki(wikiname);
            ViewBag.wikiname = wikiname;
            ViewWiki vwk = new ViewWiki();
            vwk.ImportFromModel(wk);
            return View(vwk);


        }

        ///GetModeratorInvitesByWiki
        /// 

        #region ModInvites
        public ActionResult GetModeratorInvitesByWiki(string wikiname)
        {
            try
            {


                if (CommonTools.isEmpty(wikiname) == true)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                Wiki wk = this.wkmngr.GetWiki(wikiname);
                if (wk == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }
                List<WikiModInvitations> wkinvs = this.wkmodinvngr.GetModeratorInvitesByWiki(wikiname);
                ViewWikiModInvites wkminv = new ViewWikiModInvites();
                wkminv.Wiki = new ViewWiki();
                wkminv.Wiki.ImportFromModel(wk);
                wkminv.ModeratorInvites = new List<ViewWikiModInvitations>();
                foreach(var vk in wkinvs)
                {
                    ViewWikiModInvitations i = new ViewWikiModInvitations();
                    i.ImportFromModel(vk);
                    wkminv.ModeratorInvites.Add(i);
                }
                
               

                return View(wkminv);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult RequestModeratorInvite(string wikiname)
        {
            try
            {


                if (CommonTools.isEmpty(wikiname) == true)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                Wiki wk = this.wkmngr.GetWiki(wikiname);
                if (wk == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }
                WikiModInvitations inv = new WikiModInvitations();
                inv.Moderator = this.usremngr.GetUser(this.User.Identity.Name).Id;//.UserName;
                inv.Wiki = wk;
                this.wkmodinvngr.CreateModRequest(wikiname,inv);
                RouteValueDictionary vals = new RouteValueDictionary();
                vals.Add("wikiname", wikiname);
                return RedirectToAction("Details", "HomeWiki",vals);
                
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult GetModeratorInviteDetails(string wikiname,int ? id)
        {
            try
            {


                if (CommonTools.isEmpty(wikiname) == true && id>0)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                WikiModInvitations inv = this.wkmodinvngr.GetModeratorInviteById(wikiname, Convert.ToInt32(id));
                Wiki wk = this.wkmngr.GetWiki(wikiname);
                if (wk == null &&inv==null )
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                ViewWikiModInvitations vinv = new ViewWikiModInvitations();
                vinv.ImportFromModel(inv);
                return View(vinv);


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult AcceptModInvitation(string wikiname, int? id)
        {
            try
            {


                if (CommonTools.isEmpty(wikiname) == true && id > 0)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                WikiModInvitations inv = this.wkmodinvngr.GetModeratorInviteById(wikiname, Convert.ToInt32(id));
                Wiki wk = this.wkmngr.GetWiki(wikiname);
                if (wk == null && inv == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                this.wkmodinvngr.AcceptModRequest(wikiname, Convert.ToInt32(id), this.User.Identity.Name);
                RouteValueDictionary vals = new RouteValueDictionary();
                vals.Add("wikiname", wikiname);
                return RedirectToAction("GetModeratorInvitesByWiki", vals);
               


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public ActionResult RejectModInvitation(string wikiname, int? id)
        {
            try
            {


                if (CommonTools.isEmpty(wikiname) == true && id > 0)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                WikiModInvitations inv = this.wkmodinvngr.GetModeratorInviteById(wikiname, Convert.ToInt32(id));
                Wiki wk = this.wkmngr.GetWiki(wikiname);
                if (wk == null && inv == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                this.wkmodinvngr.RejectModRequest(wikiname, Convert.ToInt32(id), this.User.Identity.Name);
                RouteValueDictionary vals = new RouteValueDictionary();
                vals.Add("wikiname", wikiname);
                return RedirectToAction("GetModeratorInvitesByWiki", vals);


            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }



}