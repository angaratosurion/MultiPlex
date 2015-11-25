using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Interfaces;

namespace MultiPlex.Core.Verbs
{
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "AdminNavigation")]
    public class AdminRolesVerb : IActionVerb
    {
        public string Action
        {
            get
            {
                return "GetRoles";
            }
        }

        public string Controller
        {
            get
            {
                return "WikiUser";
            }
        }

        public string Name
        {
            get
            {
                return "Roles Administration";
            }
        }
    }
}
