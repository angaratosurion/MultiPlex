using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using MultiPlex.Core.Interfaces;

namespace MultiPlex.Core
{
    public class Bootstrapper
    {
        private static CompositionContainer CompositionContainer;
        private static bool IsLoaded = false;
        //   static CommonTools cmTools = new CommonTools();
        [ImportMany]
        private IEnumerable<Lazy<IRouteRegistrar, IRouteRegistrarMetadata>> RouteRegistrars;
        private static IEnumerable<Lazy<IActionVerb, IActionVerbMetadata>> ActionVerbs;
        private static IEnumerable<Lazy<IModuleInfo>> ModuleInfos;
        public static void Compose(List<string> pluginFolders)
        {
            try
            {

                if (IsLoaded) return;

                var catalog = new AggregateCatalog();

                catalog.Catalogs.Add(new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")));

                foreach (var plugin in pluginFolders)
                {
                    var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        "Modules", plugin));
                    RegisterPath(directoryCatalog.FullPath);
                    catalog.Catalogs.Add(directoryCatalog);

                }
                CompositionContainer = new CompositionContainer(catalog);

                CompositionContainer.ComposeParts();
                ActionVerbs = CompositionContainer.GetExports<IActionVerb, IActionVerbMetadata>();
                ModuleInfos = CompositionContainer.GetExports<IModuleInfo>();
                IsLoaded = true;
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
            }
        }

        public static T GetInstance<T>(string contractName = null)
        {
            try
            {

                var type = default(T);
                if (CompositionContainer == null) return type;

                if (!string.IsNullOrWhiteSpace(contractName))
                    type = CompositionContainer.GetExportedValue<T>(contractName);
                else
                    type = CompositionContainer.GetExportedValue<T>();

                return type;
            }
            catch (Exception ex )
            {
                CommonTools.ErrorReporting(ex);
                return default(T);
            }
        }
        public static void RegisterPath(string path)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(path))
                {
                    AppDomain.CurrentDomain.AppendPrivatePath(path);
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }

        }
        /// <summary>
        /// Gets the available verbs for the given category.
        /// </summary>
        /// <param name="category">The category of verbs to get.</param>
        /// <returns>An enumerable of verbs.</returns>
        public static IEnumerable<IActionVerb> GetVerbsForCategory(string category)
        {
           

            return ActionVerbs
                .Where(l => l.Metadata.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase))
                .Select(l => l.Value);
        }
        public static IEnumerable<IModuleInfo> GetAllModulesInfo()
        {
            List<IModuleInfo> ap = new List<IModuleInfo>();
            
             foreach(var inf in ModuleInfos)
            {
                ap.Add(inf.Value);
            }

            return ap;
        }
    }
}
