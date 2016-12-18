using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Interfaces;
using MultiPlex.Core.Application;

namespace MultiPlex.Core.Verbs
{
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "WikiNavigation")]
    //[Export(typeof(IActionVerb)), ExportMetadata("Category", "Navigation")]
    public class AdminSiteVerb : IActionVerb
    {
        public string Action
        {
            get
            {
                return "Index";
            }
        }

        public string Controller
        {
            get
            {
                return "WikiAdminSite";
            }
        }

        public string Description
        {
            get
            {
                return "";
            }
        }

        public bool isAdminPalnel
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return "Site Administration";
            }
        }
        public string Moduledescription
        {
            get
            {
                MultiPlexInfo inf = new MultiPlexInfo();
                return inf.Description;
            }
        }

        public string ModuleName
        {
            get
            {
                MultiPlexInfo inf = new MultiPlexInfo();
                return inf.Name;
            }
        }
    }
}
