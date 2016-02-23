using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Data.Models;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    public class ViewWiki
    {
        [Required]
        public int id { get; set; }
        [Required]
        [Key]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        [Required]
        public string WikiTitle { get; set; }
        public  List<WikiTitle> Titles { get; set; }
        public  List<WikiContent> Content { get; set; }
      
      //  public string AdministratorId { get; set; }
        [Required]
      //  [ForeignKey("AdministratorId")]
        public   ApplicationUser Administrator { get; set; }
        public  List<ApplicationUser> Moderators { get; set; }
        public  List<WikiCategory> Categories { get; set; }
        public  List<WikiFile> Files { get; set; }
       
          
        public DateTime UpdatedAt { get; set; }
        public void ImportFromModel(Wiki md)
        {
            try
            {
                if ( md !=null && CommonTools.isEmpty(md.Administrator)==false)
                {
                    ApplicationUser user = CommonTools.usrmng.GetUserbyID(md.Administrator);
                    if (user != null)
                    {
                        this.id = md.id;
                        this.Files = md.Files;
                        this.Categories = md.Categories;
                        this.Content = md.Content;
                        this.Description = md.Description;
                        this.Name = md.Name;
                        this.Titles = md.Titles;
                        this.UpdatedAt = md.UpdatedAt;
                        this.WikiTitle = md.WikiTitle;
                        this.Administrator = user;
                        if (md.Moderators != null)
                        {
                            List<ApplicationUser> mods = new List<ApplicationUser>();

                            foreach(var m in md.Moderators)
                            {
                                ApplicationUser ms = CommonTools.usrmng.GetUserbyID(m.Moderator);
                                 if ( ms !=null)
                                {
                                    mods.Add(ms);

                                }
                            }
                            this.Moderators = mods;

                        }


                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                
            }
        }
        public Wiki ExportToModel()
        {
            try
            {
                Wiki ap = new Wiki();
                    //ApplicationUser user = CommonTools.usrmng.GetUserbyID(md.Administrator);
                   
                        ap.id = this.id;
                 
                        ap.Files = this.Files;
                        ap.Categories = Categories;
                        ap.Content = Content;
                        ap.Description = Description;
                        ap.Name = Name;
                        ap.Titles = Titles;
                        ap.UpdatedAt = UpdatedAt;
                        ap.WikiTitle = WikiTitle;
                        ap.Administrator = Administrator.Id;
                if ( Files==null)
                {
                    ap.Files = new List<WikiFile>();
                }
                 if ( Categories== null)
                {
                    ap.Categories = new List<WikiCategory>();
                }
                        if (Moderators != null)
                        {
                            List<string> mods = new List<string>();
                    ap.Moderators = new List<WikiMods>();

                            foreach (var m in this.Moderators)
                            {

                        //mods.Add(m.Id);
                        WikiMods wm = new WikiMods();
                        wm.Wiki = ap;
                        wm.Moderator = m.Id;
                        ap.Moderators.Add(wm);

                              
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
    }
}
