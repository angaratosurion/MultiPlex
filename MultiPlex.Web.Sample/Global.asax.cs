using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MultiPlex.Web.Sample.Wiki;

namespace MultiPlex.Web.Sample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("content/{*pathInfo}");
            routes.IgnoreRoute("WebForms/{*pathInfo}");

            routes.MapRoute(
                "History",
                "{id}/{slug}/v{version}",
                new {controller = "HomeWiki", action = "ViewWikiVersion"},
                new {id = @"\d+", version = @"\d+"}
                );

            routes.MapRoute(
                "Source",
                "{id}/{slug}/source/v{version}",
                new {controller = "HomeWiki", action = "GetWikiSource"},
                new {id = @"\d+", version = @"\d+"}
                );

            routes.MapRoute(
                "Act",
                "{id}/{slug}/{action}",
                new {controller = "HomeWiki", action = "ViewWiki"},
                new {id = @"\d+", action = @"\w+"}
                );

            routes.MapRoute(
                "Default",
                "{id}/{slug}",
                new {controller = "HomeWiki", action = "ViewWiki", id = 1, slug = "HomeWiki"},
                new {id = @"\d+"}
                );
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            Macros.Register<TitleLinkMacro>();
        }
    }
}