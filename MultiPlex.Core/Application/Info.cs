using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MultiPlex.Core.Interfaces;

namespace MultiPlex.Core.Application
{
    [Export(typeof(IModuleInfo))]
       public class Info : IModuleInfo
    {
        public string Description
        {
            get
            {
                string description = "A Wiki Software using Wkiplex engine for multitiple wikis ";
                return description;
            }
        }

        public string Name
        {
            get
            {
                return "MultiPlex";
            }
        }

        public string SourceCode
        {
            get
            {
                return "https://github.com/angaratosurion/MultiPlex";
            }
        }

        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string WebSite
        {
            get
            {
                return "http://pariskoutsioukis.net/blog/category/MultiPlex";
            }
        }
    }
}
