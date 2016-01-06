using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MultiPlex.Core.Application
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
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
         

            //routes.MapRoute(
            //    "Default",
            //    "{wikiname}/{id}/{slug}",
            //    new { controller = "Wiki", action = "ViewContent", id = 1, slug = "HomeWiki" },
            //    new { id = @"\d+" }
            //    );
        }
        
    }
}
