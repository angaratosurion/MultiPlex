using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using MultiPlex.Core.Configuration;
using MultiPlex.Core.Data;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;
using BlackCogs.Data.Models;
namespace MultiPlex.Core.Managers
{
 public   class WikiManager
    {
        
        WikiRepository wrepo = new WikiRepository();
      
        SettingsManager setmngr = new SettingsManager();
        FileSystemManager flmng= new FileSystemManager();
        
      
        public List<Data.Models.Wiki> ListWiki()
        {
            try
            {
                //return this.db.Wikis.ToList();
                return this.wrepo.ListWiki();
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<Data.Models.Wiki> ListWikiByAdmUser(string username)
        {
            try
            {
                List<Data.Models.Wiki> ap = null;
                if (!CommonTools.isEmpty(username)  && CommonTools.usrmng.UserExists(username))
                {
                    ap = this.wrepo.ListWikiByAdmUser(username);
                }

                    return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<Data.Models.Wiki> ListWikiByModUser(string username)
        {
            try
            {
                List<Data.Models.Wiki> ap = null;
                if (!CommonTools.isEmpty(username) && CommonTools.usrmng.UserExists(username))
                {
                    ap = this.wrepo.ListWikiByModUser(username);
                }

                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public Wiki GetWiki(string name)
        {
            try
            {
                Wiki ap = null;
                if ( !CommonTools.isEmpty(name))
                {
                    ap = this.wrepo.GetWiki(name);
                    if ( ap.Categories ==null)
                    {
                        ap.Categories = new List<WikiCategory>();
                    }
                    if ( ap.Titles ==null)
                    {
                        ap.Titles = new List<WikiTitle>();
                    }

                }


                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void CreateWiki(Wiki wk,string username)
        {
            try
            {
                ApplicationUser usr = null;
              
                if ( wk!=null && CommonTools.isEmpty(username)==false)
                {



                    usr = CommonTools.usrmng.GetUser(username);
                    if (usr != null)
                    {
                        wk.Administrator = usr.Id;//.Clone();
                        //wk.AdministratorId = wk.Administrator.Id;

                  

                       wk.Moderators = new List<string>();
                        wk.Moderators.Add(usr.Id);
                        this.wrepo.CreateWiki(wk);
                      
                        string wkrotfold = this.setmngr.WikiRootFolderName();
                        string wkpath;

                        //if (CommonTools.isEmpty(wkrotfold ))
                        //{
                        //    wkrotfold = "wikifiles";
                        //}
                        // wkpath = "~/" + AppDataDir + "/" + wkrotfold + "/" + wk.Name;
                        wkpath = FileSystemManager.GetWikiRootDataFolderRelativePath(wk.Name);
                        if (FileSystemManager.DirectoryExists(wkpath) == false)
                        {
                            FileSystemManager.CreateDirectory(wkpath);

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }
        }
        public Wiki EditBasicInfo(Wiki wk,string wikiname)
        {
            try
            {
                Wiki ap = null,wk2;
                if (wk != null && CommonTools.isEmpty(wikiname)==false)
                   {
                    
                    this.wrepo.EditWikiBasicInfo(wk, wikiname);
                    ap = this.wrepo.GetWiki(wikiname);
                }


                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<ApplicationUser> GetWikiModerators(string wikiname)
        {
            try
            {
                List<ApplicationUser> ap = null;

                if (CommonTools.isEmpty(wikiname) == false && this.wrepo.WikiExists(wikiname))
                {
                    Wiki wk = this.GetWiki(wikiname);

                    List<string> mods = wk.Moderators;
                    if (mods != null)
                    {
                        foreach (var m in mods)
                        {
                            ApplicationUser md = CommonTools.usrmng.GetUserbyID(m);
                            if (md != null)
                            {
                                ap.Add(md);
                            }
                        }
                    }
                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public ApplicationUser GetWikiAdministrator(string wikiname)
        {
            try
            {
                ApplicationUser ap = null;

                if (CommonTools.isEmpty(wikiname) ==false && this.wrepo.WikiExists(wikiname))
                {
                    Wiki wk = this.GetWiki(wikiname);
                    string adm= wk.Administrator;
                    if (CommonTools.isEmpty(adm) == false)
                    {
                        ap = CommonTools.usrmng.GetUserbyID(adm);
                            
                    } 
                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void DeleteWiki(string wikiname)
        {
            try
            {
                Wiki ap = null;
                if (!CommonTools.isEmpty(wikiname))
                {
                    string path = FileSystemManager.GetWikiRootDataFolderRelativePath(wikiname);
                    List<WikiFile> wkfiles = this.wrepo.GetWikiFiles(wikiname);
                     if ( wkfiles !=null)
                    {
                          foreach(WikiFile f in wkfiles)
                        {
                            FileSystemManager.DeleteFile(f.RelativePath);
                        }
                        FileSystemManager.DeleteDirectory(path);
                    }

                    this.wrepo.DeleteWiki(wikiname);

                }


               

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
               
            }
        }
        public void DeleteWikiByAdm(string username)
        {
            try
            {
                Wiki ap = null;
                if (!CommonTools.isEmpty(username))
                {

                    List<Wiki> wks = this.ListWikiByAdmUser(username);
                    if (wks != null)
                    {
                        foreach(Wiki w in wks)
                        {
                            this.DeleteWiki(w.Name);
                        }
                    }
                   

                }




            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
    }
}
