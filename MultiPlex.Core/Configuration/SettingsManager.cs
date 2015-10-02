using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MultiPlex.Core.Configuration
{
   public  class SettingsManager
    {
        public static string FoldernameSetting;
        AppSettingsReader rdr = new AppSettingsReader();
       public string WikiRootFolderName()
        {
            try
            {
                string app = null;
               app= rdr.GetValue(ConfigurationConstants.FolderNameSetting, typeof(string)).ToString();
                FoldernameSetting = app;


                return app;

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        
    }
}
