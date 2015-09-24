using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace MultiPlex.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for implementing a registrar to publish routes.
    /// </summary>
    public interface IRouteRegistrar
    {
        #region Methods
        /// <summary>
        /// Registers any routes to be ignored by the routing system.
        /// </summary>
        /// <param name="routes">The collection of routes to add to.</param>
        void RegisterIgnoreRoutes(RouteCollection routes);

        /// <summary>
        /// Registers any routes to be used by the routing system.
        /// </summary>
        /// <param name="routes">The collection of routes to add to.</param>
        void RegisterRoutes(RouteCollection routes);
        #endregion
    }
}
