using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BlackCogs.Configuration;

namespace MultiPlex.Core.Configuration
{
   public  class SettingsManager:BlackCogsSettingManager
    {
        public static string FoldernameSetting;
        AppSettingsReader rdr = new AppSettingsReader();
       
       public string WikiRootFolderName()
        {
            try
            {
                string app = null;
               app= rdr.GetValue(ConfigurationConstants.FolderNameSetting, typeof(string)).ToString();

                if (CommonTools.isEmpty(app))
                {
                    app = "wikifiles";
                }
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
