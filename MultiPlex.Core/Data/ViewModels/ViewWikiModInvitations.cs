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
        public Wiki Wiki { get; set; }
        [Required]
        public ApplicationUser Moderator { get; set; }
       
    }
}
