using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlex.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for providing metadata for a controller.
    /// </summary>
    public interface IControllerMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        string Name { get; }
        #endregion
    }
}
