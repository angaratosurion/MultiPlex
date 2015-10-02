using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MultiPlex.Core.Configuration;
using MultiPlex.Core.Data;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;

namespace MultiPlex.Core.Managers
{
 public   class WikiManager
    {
        HttpServerUtilityBase util;
        WikiRepository wrepo = new WikiRepository();
        Context db = new Context();
        SettingsManager setmngr = new SettingsManager();
        FileManager flmng;
        const string AppDataDir = "App_Data";
        public WikiManager(HttpServerUtilityBase tul)
        {
            if (tul != null)
            {
                util = tul;
                this.flmng = new FileManager(tul);
            }
        }
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

                    if (CommonTools.isEmpty(wkrotfold ))
                    {
                        wkrotfold = "wikifiles";
                    }
                    wkpath = "~/" + AppDataDir + "/" + wkrotfold + "/" + wk.Name;
                    if (! this.flmng.DirectoryExists(wkpath))
                    {
                        this.flmng.CreateDirectory(wkpath);

                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                //return null;
            }
        }
    }
}
