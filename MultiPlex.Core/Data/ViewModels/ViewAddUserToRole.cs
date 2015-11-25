using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
   public class ViewAddUserToRole
    {
        public IdentityRole Role { get; set; }
        public List<ApplicationUser> Users{ get; set; }
        public ApplicationUser UserToAdd { get; set; }
    }
}
