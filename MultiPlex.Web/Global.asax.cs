using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MultiPlex.Core.Application;

namespace MultiPlex.Web
{
    public class MvcApplication : MultiPlex.Core.Application.Application //System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MultiPlex.Web.Migrations.Configuration migr = new Migrations.Configuration();
            base.Application_Start();
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error()
        {
            base.Application_Error();
        }
    }
}
