using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Interfaces;

namespace MultiPlex.Core.Verbs
{
    //[Export(typeof(IActionVerb)), ExportMetadata("Category", "WikiNavigation")]
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "Navigation")]
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

        public string Name
        {
            get
            {
                return "Site Administration";
            }
        }
    }
}
