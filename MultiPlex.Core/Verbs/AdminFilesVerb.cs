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
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "AdminNavigation")]
    public class AdminFilesVerb : IActionVerb
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
                return "WikiFile";
            }
        }

        public string Description
        {
            get
            {
                return "Here you Administrage the Existing Files on the Wiki";
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
                return "File Administration";
            }
        }
        public string Moduledescription
        {
            get
            {
                Info inf = new Info();
                return inf.Description;
            }
        }

        public string ModuleName
        {
            get
            {
                Info inf = new Info();
                return inf.Name;
            }
        }
    }
}
