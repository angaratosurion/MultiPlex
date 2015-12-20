using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Administration;
using System.Runtime.InteropServices;
using System.IO;
using System.Web.Hosting;
using MultiPlex.Core.Configuration;

namespace MultiPlex.Core.Managers
{
   public  class FileSystemManager:BlackCogs.Managers.FileSystemManager
   {
       CommonTools cmtools = new CommonTools();
        //  static HttpServerUtilityBase util;
        //const string   filesdir="files",AppDataDir="App_Data";
        const string AppDataDir = "App_Data";
       public static  SettingsManager setmngr = new SettingsManager();
        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

        static int SYMLINK_FLAG_DIRECTORY = 1;

        public static string GetWikiRootDataFolderRelativePath(string wikiname)
        {
            try
            {
                string ap = "";
                if (CommonTools.isEmpty(wikiname) == false)
                {
                    string wkrotfold = setmngr.WikiRootFolderName();
                    ap = "~/" + AppDataDir + "/" + wkrotfold + "/" + wikiname;
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
