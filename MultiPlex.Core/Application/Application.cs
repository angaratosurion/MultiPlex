using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MultiPlex.Core.Controllers;
using MultiPlex.Core.Controllers.Factory;
using MultiPlex.Core.Interfaces;
using MultiPlex.Core.Views;

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
                var pluginFolders = new List<string>();

                var plugins = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Modules")).ToList();

                plugins.ForEach(s =>
                {
                    var di = new DirectoryInfo(s);
                    pluginFolders.Add(di.Name);
                });

                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                Bootstrapper.Compose(pluginFolders);
                ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());
                ViewEngines.Engines.Add(new CustomViewEngine(pluginFolders));
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }
        }
    }
}
