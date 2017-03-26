using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BlackCogs.Interfaces;
using BlackCogs.Tools;

namespace MultiPlex.Core.Application
{
    [Export(typeof(IRouteRegistrar)), ExportMetadata("Order", 101)]

    public class MultiPlexRouteConfig : IRouteRegistrar
    {
        public  void RegisterRoutes(RouteCollection routes)
        {
            /*
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{wikiname}",
                defaults: new { controller = "HomeWiki", action = "Index", wikiname = UrlParameter.Optional }
            );
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("content/{*pathInfo}");
            //routes.IgnoreRoute("WebForms/{*pathInfo}");

            routes.MapRoute(
                "History",
                "{wikiname}/{id}/{slug}/v{version}",
                new { controller = "HomeWiki", action = "ViewContentVersion" },
                new { id = @"\d+", version = @"\d+" }
                );

            routes.MapRoute(
                "Source",
                "{wikiname}/{id}/{slug}/source/v{version}",
                new { controller = "HomeWiki", action = "GetWikiSource" },
                new { id = @"\d+", version = @"\d+" }
                );

            routes.MapRoute(
                "Act",
                "{id}/{slug}/{action}",
                new { controller = "HomeWiki", action = "ViewContent" },
                new { id = @"\d+", action = @"\w+" }
                );
         */
         
            BlackRouteCollectionExtensions.MapRouteWithName(routes,"Default", "{controller}/{action}/{wikiname}",
              new { controller = "HomeWiki", action = "Index", wikiname = UrlParameter.Optional });

            BlackRouteCollectionExtensions.MapRouteWithName(routes, "History",
                "{wikiname}/{id}/{slug}/v{version}",
                new { controller = "HomeWiki", action = "ViewContentVersion" },  new { id = @"\d+", version = @"\d+" } );
            BlackRouteCollectionExtensions.MapRouteWithName(routes, "Source",
                "{wikiname}/{id}/{slug}/source/v{version}",
                new { controller = "HomeWiki", action = "GetWikiSource" },
                new { id = @"\d+", version = @"\d+" }
                );
            BlackRouteCollectionExtensions.MapRouteWithName(routes, "Act",
                "{id}/{slug}/{action}",
                new { controller = "HomeWiki", action = "ViewContent" },
                new { id = @"\d+", action = @"\w+" }
                );
            //routes.MapRoute(
            //    "Default",
            //    "{wikiname}/{id}/{slug}",
            //    new { controller = "Wiki", action = "ViewContent", id = 1, slug = "HomeWiki" },
            //    new { id = @"\d+" }
            //    );
        }

        public void RegisterIgnoreRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        }

       
    }
}
