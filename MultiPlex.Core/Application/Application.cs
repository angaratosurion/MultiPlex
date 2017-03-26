using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using BlackCogs.Application;
using BlackCogs.Controllers.Factory;
using BlackCogs.Views.Engines;

namespace MultiPlex.Core.Application
{
    public class Application : BlackCogs.Application.Application //System.Web.HttpApplication
    {


        protected void Application_Start()
        {
            try
            {
                base.Application_Start();

                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                //RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                BootStrap();



            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }
        }
        protected void Application_Error()
        {

            base.Application_Error();

        }

    }
}
