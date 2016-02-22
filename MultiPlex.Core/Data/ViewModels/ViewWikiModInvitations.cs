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
   public class ViewWikiModInvitations
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public ViewWiki Wiki { get; set; }
        [Required]
        public ApplicationUser Moderator { get; set; }
        public void ImportFromModel(WikiModInvitations md)
        {
            try
            {
                if (md != null && CommonTools.isEmpty(md.Moderator) == false)
                {
                    ApplicationUser user = CommonTools.usrmng.GetUserbyID(md.Moderator);
                    if (user != null)
                    {
                        this.Id = md.Id;
                        this.Moderator = user;
                        this.Wiki = new ViewWiki();
                        this.Wiki.ImportFromModel(md.Wiki);



                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public WikiModInvitations ExportToModel()
        {
            try
            {
                WikiModInvitations ap = new WikiModInvitations();
                ap.Id = Id;
                ap.Wiki = Wiki.ExportToModel();
                ap.Moderator = Moderator.Id;

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
