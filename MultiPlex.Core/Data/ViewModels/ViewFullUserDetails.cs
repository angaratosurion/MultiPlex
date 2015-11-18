using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
   public class ViewFullUserDetails
    {
        public ApplicationUser UserDetails { get; set; }
        public List<IdentityRole> Roles { get; set; }
        [Display(Name ="Wiki's Which he is Administrator")]
        public List<Wiki> WikisAsAdmin { get; set; }
        [Display(Name = "Wiki's Which he is a Moderator")]
        public List<Wiki> WikisAsMod { get; set; }

    }
}
