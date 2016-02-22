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
    

    public class ViewWikiTitle
    {  
            [Required]                
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Slug { get; set; }
        public int MaxVersion { get; set; }
       
        public Boolean isLocked { get; set; }
        [Required]
        public virtual Wiki Wiki { get; set; }
        [Required]
        public virtual ApplicationUser WrittenBy { get; set; }
        public virtual List<WikiFile> Files { get; set; }
        public virtual List<WikiCategory> Categories { get; set; }
      
        public virtual WikiContent Content { get; set; }
        public void ImportFromModel(WikiTitle md)
        {
            try
            {
                if (md != null && CommonTools.isEmpty(md.WrittenBy))
                {
                    ApplicationUser user = CommonTools.usrmng.GetUserbyID(md.WrittenBy);
                    if (user != null)
                    {
                        this.Id = md.Id;
                        this.Files = md.Files;
                        this.Categories = md.Categories;
                        this.Content = md.Content;
                        this.isLocked = md.isLocked;
                        this.MaxVersion = md.MaxVersion;
                        this.Name = md.Name;
                        this.Slug = md.Slug;
                        this.Wiki = md.Wiki;
                        this.WrittenBy= user;
                       


                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public WikiTitle ExportToModel()
        {
            try
            {
                WikiTitle ap = new WikiTitle();
                        ap.Id = Id;
                        ap.Files = Files;
                        ap.Categories = Categories;
                        ap.Content = Content;
                        ap.isLocked = isLocked;
                        ap.MaxVersion = MaxVersion;
                        ap.Name = Name;
                        ap.Slug = Slug;
                        ap.Wiki = Wiki;
                        ap.WrittenBy = WrittenBy.Id;

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