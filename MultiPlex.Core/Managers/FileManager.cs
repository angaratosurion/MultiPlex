using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MultiPlex.Core.Data.Models;
using MultiPlex.Core.Data.Repositories;
using System.IO;
using MultiPlex.Core.Configuration;

namespace MultiPlex.Core.Managers
{
   public class FileManager
    {
        FileSystemManager fsmngr = new FileSystemManager();
        WikiRepository rep = new WikiRepository();
        SettingsManager setmngr = new SettingsManager();
        public void AddFile ( string wikiname, MultiPlex.Core.Data.Models.File tfile, 
            HttpPostedFileBase filcnt ,int tid,ApplicationUser user)
        {
            try
            {
                if ( CommonTools.isEmpty(wikiname) ==false && rep.WikiExists(wikiname)
                    && filcnt !=null &tid>0 & user !=null)
                {
                    Wiki wk = rep.GetWiki(wikiname);
                    string wkrotfold = FileSystemManager.GetWikiRootDataFolderRelativePath(wk.Name);
                    string wkpath = wkrotfold + "/" + filcnt.FileName;
                    if ( FileSystemManager.FileExists(wkpath))
                    {
                       Boolean ap= FileSystemManager.CreateFile(wkpath, filcnt);
                        if (ap)
                        {
                            rep.AddFile(wikiname, tfile, tid, user);
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
               // return null;
            }
        }
    }
}
