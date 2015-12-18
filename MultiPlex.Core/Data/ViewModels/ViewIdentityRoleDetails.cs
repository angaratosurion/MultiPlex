using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MultiPlex.Core.Data.Models;
using BlackCogs.Data.Models;
namespace MultiPlex.Core.Data.ViewModels
{
    public class ViewIdentityRoleDetails
    {
        public IdentityRole Role { get; set; }
        public List<ApplicationUser> Members { get; set; }
    }
}
