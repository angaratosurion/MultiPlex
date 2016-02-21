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
        public virtual List<WikiTitle> Titles { get; set; }
        public virtual List<WikiContent> Content { get; set; }
      
      //  public string AdministratorId { get; set; }
        [Required]
      //  [ForeignKey("AdministratorId")]
        public virtual  ApplicationUser Administrator { get; set; }
        public virtual List<ApplicationUser> Moderators { get; set; }
        public virtual List<WikiCategory> Categories { get; set; }
        public virtual List<WikiFile> Files { get; set; }
       
          
        public DateTime UpdatedAt { get; set; }
        public void ImportFromModel(Wiki md)
        {
            try
            {
                if ( md !=null && CommonTools.isEmpty(md.Administrator))
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
                                ApplicationUser ms = CommonTools.usrmng.GetUserbyID(m);
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
    }
}
