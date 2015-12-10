using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlex.Core.Interfaces
{
    public interface IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name of the verb.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the action.
        /// </summary>
        string Action { get; }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        string Controller { get; }
        string Description { get; }
        #endregion
    }
}
