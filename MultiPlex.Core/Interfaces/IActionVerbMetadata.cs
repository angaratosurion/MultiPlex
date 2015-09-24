using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlex.Core.Interfaces
{
    /// <summary>
    /// Defines metadata associated with an action verb.
    /// </summary>
    public interface IActionVerbMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the category for the verb.
        /// </summary>
        string Category { get; }
        #endregion
    }
}
