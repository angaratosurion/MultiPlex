using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Data.Models;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    
   public class ViewWikiFile
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
       // [Required]
        public string AbsolutePath { get; set; }
        [Required]
        public string RelativePath { get; set; }
        [Required]
        public string FileType { get; set; }
        public Boolean isImage { get; set; }
        public int Version { get; set; }
        [DataType(DataType.DateTime)]

        public DateTime VersionDate { get; set; }

       
        [Required]
        public  ApplicationUser Owner { get; set; }
        [Required]
        public  Wiki Wiki { get; set; }
        [Required]
        public  WikiTitle Title { get; set; }
        public void ImportFromModel(WikiFile md)
        {
            try
            {
                if (md != null && CommonTools.isEmpty(md.Owner) == false)
                {
                    ApplicationUser user = CommonTools.usrmng.GetUserbyID(md.Owner);
                    if (user != null)
                    {
                        this.Id = md.Id;
                        this.FileName = md.FileName;
                        this.RelativePath = md.RelativePath;
                        this.AbsolutePath = md.AbsolutePath;
                        this.FileType= md.FileType;
                        this.isImage = md.isImage;
                        this.Title = md.Title;
                        this.Version = md.Version;
                        this.VersionDate = md.VersionDate;
                        this.Owner = user;
                        this.Wiki = md.Wiki;
                       


                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public WikiFile ExportToModel()
        {
            try
            {
                WikiFile ap = new WikiFile();
               
                        ap.Id = Id;
                        ap.FileName = FileName;
                        ap.RelativePath = RelativePath;
                        ap.AbsolutePath = AbsolutePath;
                        ap.FileType = FileType;
                        ap.isImage = isImage;
                        ap.Title = Title;
                        ap.Version = Version;
                       ap.VersionDate =VersionDate;
                        ap.Owner = Owner.Id;
                        ap.Wiki = Wiki;
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
