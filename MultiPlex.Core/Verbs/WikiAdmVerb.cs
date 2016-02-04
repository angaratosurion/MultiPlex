using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Interfaces;

namespace MultiPlex.Core.Verbs
{
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "WikiNavigation")]
    public class WikiAdmVerb : IActionVerb
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
                return "HomeWiki";
            }
        }

        public string Description
        {
            get
            {
                return "Here you Administrage the Existing Wikis";
            }
        }

        public string Name
        {
            get
            {
                return "Wiki Administration";
            }
        }
    }
}
