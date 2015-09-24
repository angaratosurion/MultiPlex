using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlex.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for providing metadata for a route registrar.
    /// </summary>
    public interface IRouteRegistrarMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the order in which the registrar must be processed.
        /// </summary>
        int Order { get; }
        #endregion
    }
}
