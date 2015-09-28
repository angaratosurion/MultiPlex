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

namespace MultiPlex.Core.Managers
{
   public  class FileManager
   {
       CommonTools cmtools = new CommonTools();
       HttpServerUtilityBase util;
        //const string   filesdir="files",AppDataDir="App_Data";

        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

        static int SYMLINK_FLAG_DIRECTORY = 1;

        public FileManager (HttpServerUtilityBase tul)
       {
           if ( tul !=null)
           {
               util = tul;
           }
       }
       #region Common
     
       public string PhysicalPathFromUrl(string path)
       {
           try
           {
               string ap = null;
              
               if (path != null && this.DirectoryExists(path))
               {
                   ap = util.MapPath(path);
               }
               return ap;

           }
            catch (Exception ex){CommonTools.ErrorReporting(ex);
               return null;
           }
       }
       #endregion
       #region Directory
     
       public Boolean DirectoryExists(String path)
       {
           try
           {
               Boolean ap = false;
               path = this.PhysicalPathFromUrl(path);
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
       public Boolean CreateDirectory(string path)
       {
           try
           {
               Boolean ap = false;

               if (this.DirectoryExists(path)!=false)
               {
                   string t = this.PhysicalPathFromUrl(path);
                   Directory.CreateDirectory(t);
                   ap = true;
               }

               return ap;

           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);
               
               
               return false;
           }
       }
       public Boolean DeleteDirectory(string path)
       {
           try
           {
               Boolean ap = false;
               if (CommonTools.isEmpty(path) && this.DirectoryExists(path))
               {
                   string t = this.PhysicalPathFromUrl(path);
                   Directory.Delete(t,true);
                   ap = true;
               }



               return ap;

           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);

               
               return false;
           }
       }
       public Boolean MoveDirectory(string src, string trg)
       {
           try
           {
               Boolean ap = false;

               if (CommonTools.isEmpty(src) && CommonTools.isEmpty(trg)
                   && this.DirectoryExists(src))//&& this.Exists(trg))
               {
                   src = this.PhysicalPathFromUrl(src);
                   trg = this.PhysicalPathFromUrl(trg);
                   Directory.Move(src, trg);
                   ap = true;
               }


               return ap;
           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);

               
               return false;
           }
       }
        public Boolean CreateDirectoryLink(string src, string trg)
        {
            try
            {
                Boolean ap = false;

                if (CommonTools.isEmpty(src) && CommonTools.isEmpty(trg)
                    && this.DirectoryExists(src))//&& this.Exists(trg))
                {
                    src = this.PhysicalPathFromUrl(src);
                    trg = this.PhysicalPathFromUrl(trg);
                   ap= CreateSymbolicLink(src, trg, SYMLINK_FLAG_DIRECTORY);

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
        public Boolean FileExists(String path)
       {
           try
           {
               Boolean ap = false;
               path = this.PhysicalPathFromUrl(path);
               if (CommonTools.isEmpty(path) && File.Exists(path))
               {
                   ap = true;
               }
               return ap;

           }
            catch (Exception ex){CommonTools.ErrorReporting(ex);

               return false;
           }
       }
       public Boolean CreateFile(string path, HttpPostedFileBase data)
       {
           try
           {
               Boolean ap = false;
               if (CommonTools.isEmpty(path) && !this.FileExists(path) && data !=null)
               {
                    /* int count = data.Count();
                     path = this.PhysicalPathFromUrl(path);
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
       public Boolean DeleteFile(string path)
       {
           try
           {
               Boolean ap = false;
               if (CommonTools.isEmpty(path) && this.FileExists(path))
               {
                   path = this.PhysicalPathFromUrl(path);
                   File.Delete(path);
                   ap = true;
               }



               return ap;

           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);

               
               return false;
           }
       }
       public Boolean CopyFile(string src,string trg)
       {
           try
           {
               Boolean ap = false;

               if (CommonTools.isEmpty(src) && CommonTools.isEmpty(trg)
                   && this.FileExists(src) )//&& this.Exists(trg))
               {
                   src = this.PhysicalPathFromUrl(src);
                   trg = this.PhysicalPathFromUrl(trg);
                   File.Copy(src, trg,true);
                   ap = true;
               }


               return ap;
           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);
               
               
               return false;
           }
       }
       public Boolean MoveFile(string src, string trg)
       {
           try
           {
               Boolean ap = false;

               if (CommonTools.isEmpty(src) && CommonTools.isEmpty(trg)
                   && this.FileExists(src))//&& this.Exists(trg))
               {
                   src = this.PhysicalPathFromUrl(src);
                   trg = this.PhysicalPathFromUrl(trg);
                   File.Move(src, trg);
                   ap = true;
               }


               return ap;
           }
           catch (Exception ex){CommonTools.ErrorReporting(ex);

               
               return false;
           }
       }
     
        public Boolean CreateFileLink(string src,string trg)
        {
            try
            {
                Boolean ap = false;

                if (CommonTools.isEmpty(src) && CommonTools.isEmpty(trg)
                    && this.FileExists(src))//&& this.Exists(trg))
                {
                    src = this.PhysicalPathFromUrl(src);
                    trg = this.PhysicalPathFromUrl(trg);
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
