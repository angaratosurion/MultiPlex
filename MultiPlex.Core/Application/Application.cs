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
    public class Application : System.Web.HttpApplication
    {
       
        //[Import]
        //private CustomControllerFactory ControllerFactory;

        protected void Application_Start()
        {
            try
            {
                //var pluginFolders = new List<string>();

                //var plugins = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                //    "Modules")).ToList();
                

                //    plugins.ForEach(s =>
                //    {
                //        var di = new DirectoryInfo(s);
                //        pluginFolders.Add(di.Name);
                //    });
              
                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                BlackCogs.Application.Application.BootStrap();
                
             
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }
        }
        protected void Application_Error()
        {
            Exception lastException = Server.GetLastError();
            CommonTools.ErrorReporting(lastException);
        }

    }
}
