using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlex.Core.Interfaces
{
    public interface IModuleInfo
    {
        string Name { get; }
       string  Description { get; }
        string Version { get; }
        string WebSite { get;  }
        string SourceCode { get; }
    }
}
