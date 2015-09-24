using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MultiPlex.Core.Views
{
    public class CustomViewEngine : RazorViewEngine
    {
        private List<string> _plugins = new List<string>();

        public CustomViewEngine(List<string> pluginFolders)
        {
            try
            {

                _plugins = pluginFolders;

                ViewLocationFormats = GetViewLocations();
                MasterLocationFormats = GetMasterLocations();
                PartialViewLocationFormats = GetViewLocations();
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }
        }

        public string[] GetViewLocations()
        {
            try
            {
                var views = new List<string>();
                views.Add("~/Views/{1}/{0}.cshtml");

                _plugins.ForEach(plugin =>
                    views.Add("~/Modules/" + plugin + "/Views/{1}/{0}.cshtml")
                );
                return views.ToArray();
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

        public string[] GetMasterLocations()
        {
            try
            {
                var masterPages = new List<string>();

                masterPages.Add("~/Views/Shared/{0}.cshtml");

                _plugins.ForEach(plugin =>
                    masterPages.Add("~/Modules/" + plugin + "/Views/Shared/{0}.cshtml")
                );

                return masterPages.ToArray();
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

    }
}
