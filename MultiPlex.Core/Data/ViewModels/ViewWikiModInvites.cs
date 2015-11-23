using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
   public  class ViewWikiModInvites
    {
        public  Wiki Wiki { get; set; }
        public List<WikiModInvitations> ModeratorInvites { get; set; }
    }
}
