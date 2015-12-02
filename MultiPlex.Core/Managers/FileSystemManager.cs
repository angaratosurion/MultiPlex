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
   public  class FileSystemManager
   {
       CommonTools cmtools = new CommonTools();
        //  static HttpServerUtilityBase util;
        //const string   filesdir="files",AppDataDir="App_Data";
        const string AppDataDir = "App_Data";
       public static  SettingsManager setmngr = new SettingsManager();
        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

        static int SYMLINK_FLAG_DIRECTORY = 1;

     
       #region Common
     
      
       #endregion
       #region Directory
      public static string GetWikiRootDataFolderRelativePath(string wikiname)
        {
            try
            {
                string ap = "";
                if (CommonTools.isEmpty(wikiname)==false)
                {
                    string wkrotfold =setmngr.WikiRootFolderName();
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
       public static Boolean DirectoryExists(String path)
       {
           try
           {
               Boolean ap = false;
               path =  HostingEnvironment.MapPath(path);
               if (CommonTools.isEmpty(path) && Directory.Exists(path))
               {
                   ap = true;
               }
               return ap;

           }
            catch (Exception ex){CommonTools.ErrorReporting(ex);

               return false;
           }
       }
        public static Boolean CreateDirectory(string relpath)
       {
           try
           {
               Boolean ap = false;

               if ( DirectoryExists(relpath) ==false)
               {
                   string t =  HostingEnvironment.MapPath(relpath);
                   Directory.CreateDirectory(t);
                   ap = true;
               }

               return ap;

           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);
               
               
               return false;
           }
       }
       public static Boolean DeleteDirectory(string relpath)
       {
           try
           {
               Boolean ap = false;
               if (CommonTools.isEmpty(relpath) &&  DirectoryExists(relpath))
               {
                   string t =  HostingEnvironment.MapPath(relpath);
                   Directory.Delete(t,true);
                   ap = true;
               }



               return ap;

           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);

               
               return false;
           }
       }
       public static Boolean MoveDirectory(string relsrc, string reltrg)
       {
           try
           {
               Boolean ap = false;

               if (CommonTools.isEmpty(relsrc) && CommonTools.isEmpty(reltrg)
                   &&  DirectoryExists(relsrc))//&&  Exists(trg))
               {
                    relsrc = HostingEnvironment.MapPath(relsrc);
                    reltrg = HostingEnvironment.MapPath(reltrg);
                   Directory.Move(relsrc, reltrg);
                   ap = true;
               }


               return ap;
           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);

               
               return false;
           }
       }
        public static Boolean CreateDirectoryLink(string relsrc, string reltrg)
        {
            try
            {
                Boolean ap = false;

                if (CommonTools.isEmpty(relsrc) && CommonTools.isEmpty(reltrg)
                    &&  DirectoryExists(relsrc))//&&  Exists(trg))
                {
                    relsrc = HostingEnvironment.MapPath(relsrc);
                    reltrg = HostingEnvironment.MapPath(reltrg);
                   ap= CreateSymbolicLink(relsrc, reltrg, SYMLINK_FLAG_DIRECTORY);

                }


                return ap;
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);


                return false;
            }
        }
        #endregion
        #region files
        public static Boolean FileExists(String relpath)
       {
           try
           {
               Boolean ap = false;
                relpath =  HostingEnvironment.MapPath(relpath);
               if (CommonTools.isEmpty(relpath) && File.Exists(relpath))
               {
                   ap = true;
               }
               return ap;

           }
            catch (Exception ex){CommonTools.ErrorReporting(ex);

               return false;
           }
       }
       public static Boolean CreateFile(string relpath, HttpPostedFileBase data)
       {
           try
           {
               Boolean ap = false;
                string path= relpath;
               if (CommonTools.isEmpty(path)==false && !FileExists(path) && data !=null)
               {
                    /* int count = data.Count();
                     path =  HostingEnvironment.MapPath(path);
                     FileStream fil=File.Create(path);
                     fil.Write(data, 0, count);
                     fil.Flush();
                     fil.Close();*/
                    data.SaveAs(path);
                   ap = true;
               }



               return ap;

           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);

               
               return false;
           }
       }
       public static Boolean DeleteFile(string relpath)
       {
           try
           {
                string path = relpath;
                Boolean ap = false;
               if (CommonTools.isEmpty(path) &&  FileExists(path))
               {
                   path =  HostingEnvironment.MapPath(path);
                   File.Delete(path);
                   ap = true;
               }



               return ap;

           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);

               
               return false;
           }
       }
       public static Boolean CopyFile(string relsrc, string reltrg)
       {
           try
           {
               Boolean ap = false;
                string src=relsrc,trg=reltrg;

                if (CommonTools.isEmpty(src) && CommonTools.isEmpty(trg)
                   &&  FileExists(src) )//&&  Exists(trg))
               {
                   src = HostingEnvironment.MapPath(src);
                   trg = HostingEnvironment.MapPath(trg);
                   File.Copy(src, trg,true);
                   ap = true;
               }


               return ap;
           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);
               
               
               return false;
           }
       }
        public static Boolean MoveFile(string relsrc, string reltrg)
       {
           try
           {
               Boolean ap = false;
                string src = relsrc, trg = reltrg;

                if (CommonTools.isEmpty(src) && CommonTools.isEmpty(trg)
                   &&  FileExists(src))//&&  Exists(trg))
               {
                   src = HostingEnvironment.MapPath(src);
                   trg = HostingEnvironment.MapPath(trg);
                   File.Move(src, trg);
                   ap = true;
               }


               return ap;
           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);

               
               return false;
           }
       }
     
        public static Boolean CreateFileLink(string relsrc, string reltrg)
        {
            try
            {
                Boolean ap = false;
                string src = relsrc, trg = reltrg;

                if (CommonTools.isEmpty(src) && CommonTools.isEmpty(trg)
                    &&  FileExists(src))//&&  Exists(trg))
                {
                    src = HostingEnvironment.MapPath(src);
                    trg = HostingEnvironment.MapPath(trg);
                 ap=   CreateSymbolicLink(src, trg, 0);
                   // ap = true;
                }


                return ap;
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);


                return false;
            }
        }
        #endregion
    }
}
