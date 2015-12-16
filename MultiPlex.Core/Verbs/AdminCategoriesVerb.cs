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
    public class AdminCategoriesVerb : IActionVerb
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
                return "WikiCategory";
            }
        }

        public string Description
        {
            get
            {
                return "Here you Administrage the Existing Categories on the Wiki";
            }
        }

        public string Name
        {
            get
            {
                return "Categories Administration";
            }
        }
    }
}
