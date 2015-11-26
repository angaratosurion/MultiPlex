using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    public class ViewDeleteUserFromRole
    {
        public IdentityRole Role { get; set; }
        public ApplicationUser UserToDelete { get; set; }
    }
}
