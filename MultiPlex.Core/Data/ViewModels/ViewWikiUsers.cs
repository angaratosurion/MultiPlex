using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    public class ViewWikiUsers
    {
        public ApplicationUser Administrator { get; set; }
        public List<ApplicationUser> Moderators { get; set; }

    }
}
