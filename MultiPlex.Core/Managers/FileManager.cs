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
using System.Web.Hosting;

namespace MultiPlex.Core.Managers
{
   public class FileManager
    {
        FileSystemManager fsmngr = new FileSystemManager();
        WikiRepository rep = new WikiRepository();
        SettingsManager setmngr = new SettingsManager();
        public void AddFile ( string wikiname, MultiPlex.Core.Data.Models.WikiFile tfile, 
            HttpPostedFileBase filcnt ,int tid,ApplicationUser user)
        {
            try
            {
                if ( CommonTools.isEmpty(wikiname) ==false && rep.WikiExists(wikiname)
                    && filcnt !=null &tid>0 & user !=null)
                {
                    Wiki wk = rep.GetWiki(wikiname);
                    string wkrotfold = FileSystemManager.GetWikiRootDataFolderRelativePath(wk.Name);
                    string wkpath = wkrotfold + "/" + Path.GetFileName(filcnt.FileName);
                    if ( FileSystemManager.FileExists(wkpath)==false)
                    {
                        tfile.RelativePath = wkpath;
                       Boolean ap= FileSystemManager.CreateFile(wkpath, filcnt);
                        if (ap)
                        {
                            tfile.FileName = Path.GetFileName(filcnt.FileName);
                            tfile.AbsolutePath = HostingEnvironment.MapPath(wkpath);
                            tfile.FileType = filcnt.ContentType;
                            tfile =this.MarkAFileAsImage(filcnt, tfile);
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
        public List<Data.Models.WikiFile> GetFilesByWiki(string wikiname)
        {
            try
            {
                List<Data.Models.WikiFile> ap = null;

                if ( CommonTools.isEmpty(wikiname)==false && this.rep.WikiExists(wikiname))
                {
                    ap = this.rep.GetWikiFiles(wikiname);
                }

                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<Data.Models.WikiFile> GetFiles()
        {
            try
            {
                List<Data.Models.WikiFile> ap = null;


                ap = this.rep.GetFiles();
                

                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<Data.Models.WikiFile> GetFilesByTitle(string wikiname,int tid)
        {
            try
            {
                List<Data.Models.WikiFile> ap = null;

                if (CommonTools.isEmpty(wikiname) == false && this.rep.WikiExists(wikiname)
                    && tid>0)
                {
                    ap = this.rep.GetFilesByTitle(wikiname, tid);
                }

                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void DeleteFileById(string wikiname, int fid, ApplicationUser user)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) == false && this.rep.WikiExists(wikiname)
                   && fid > 0 && user != null)
                {  
                    Wiki wk = CommonTools.wkmngr.GetWiki(wikiname);
                    if (wk != null  && CommonTools.usrmng.UserHasAccessToWiki(user, wk, true))
                    {
                        WikiFile file = this.rep.GetFilesById(wikiname, fid);
                        Boolean r=FileSystemManager.DeleteFile(file.RelativePath);
                         if ( r==true)
                        {
                            this.rep.DeleteFileById(wikiname, fid);
                        }

                    }

                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public void DeleteFileBTitle(string wikiname, int tid, ApplicationUser user)
        {
            try
            {
                if (CommonTools.isEmpty(wikiname) == false && this.rep.WikiExists(wikiname)
                   && tid > 0 && user != null)
                {
                    Wiki wk = CommonTools.wkmngr.GetWiki(wikiname);
                    if (wk != null && CommonTools.usrmng.UserHasAccessToWiki(user, wk, true))
                    {
                        List<WikiFile> files = this.rep.GetFilesByTitle(wikiname, tid);
                         if ( files!=null)
                        {
                            foreach(var file in files)
                            {
                                this.DeleteFileById(wikiname, file.Id, user);
                            }

                        }
                        

                    }

                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public Boolean isUplaodedFileImage(HttpPostedFileBase file)
        {
            try
            {
                Boolean ap = false;
                 if ( file !=null && file.ContentType.Contains("image") ==true)
                {
                    ap = true;
                }



                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return false;

            }
        }
        public WikiFile MarkAFileAsImage(HttpPostedFileBase pfile , WikiFile wfile)
        {
            try
            {
                WikiFile ap = wfile;


                 if ( pfile !=null && wfile!=null )
                {
                    ap.isImage = this.isUplaodedFileImage(pfile);
                }
                return ap; 

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return wfile;

            }
        }

    }
}
