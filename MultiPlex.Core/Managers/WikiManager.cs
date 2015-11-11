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
        public void CreateWiki(Wiki wk)
        {
            try
            {
                if ( wk!=null)
                {
                    this.wrepo.CreateWiki(wk);
                    string wkrotfold = this.setmngr.WikiRootFolderName();
                    string wkpath;

                    //if (CommonTools.isEmpty(wkrotfold ))
                    //{
                    //    wkrotfold = "wikifiles";
                    //}
                   // wkpath = "~/" + AppDataDir + "/" + wkrotfold + "/" + wk.Name;
                    wkpath = FileSystemManager.GetWikiRootDataFolderRelativePath(wk.Name);
                    if ( FileSystemManager.DirectoryExists(wkpath)==false)
                    {
                        FileSystemManager.CreateDirectory(wkpath);

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
                    //wk2 = this.GetWiki(wikiname);
                    //wk.Categories = wk2.Categories;
                    //wk.Content = wk2.Content;
                    //wk.Files = wk2.Files;
                    //wk.Moderators = wk2.Moderators;
                    //wk.Titles = wk2.Titles;
                    //wk.Administrtor = wk2.Administrtor;
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
    }
}
