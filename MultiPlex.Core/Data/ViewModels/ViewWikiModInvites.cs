using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Data.Models;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
   public  class ViewWikiModInvites
    {
        public  ViewWiki Wiki { get; set; }
        public List<ViewWikiModInvitations> ModeratorInvites { get; set; }
       
    }
}
