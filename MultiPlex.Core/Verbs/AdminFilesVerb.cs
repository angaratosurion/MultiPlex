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
                return "File";
            }
        }

        public string Description
        {
            get
            {
                return "Here you Administrage the Existing Files on the Wiki";
            }
        }

        public string Name
        {
            get
            {
                return "File Administration";
            }
        }
    }
}
