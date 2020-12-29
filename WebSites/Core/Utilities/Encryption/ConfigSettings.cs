using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Specialized;

namespace QspEncryption
{
    public static class ConfigSettings
    {

        public static string GetValue(string key)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null && kvSettings[key] != null) //Check for Site Specific Settings
            {
                return kvSettings[key].Value;
            }
            else //No Site Specific setting so return value from web.config
            {
                return ConfigurationManager.AppSettings[key];
            }
        }

        public static string ConnectionString(string key)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString + "Application Name = " + Domain + ";";
        }

        private static KeyValueConfigurationCollection kvSettings{
            get
            {
                KeyValueConfigurationCollection kv = new KeyValueConfigurationCollection();
                string sCacheVar = "AppSettings" + Section;

                //Cache the AppSettings so we only need to pull from file once
                if (System.Web.HttpContext.Current.Cache[sCacheVar] == null)
                {
                    AppSettingsSection appSS = myAppSettings(Section);
                    if (appSS != null)
                    {
                        kv = appSS.Settings;
                        System.Web.HttpContext.Current.Cache.Insert(sCacheVar, kv);
                    }
                }
                else
                {
                    //Get from cache
                    kv = (KeyValueConfigurationCollection)System.Web.HttpContext.Current.Cache[sCacheVar];
                }
                return kv;
            }
        }

        private static AppSettingsSection myAppSettings(string section)
        {
            
            string configPath = HttpContext.Current.Request.CurrentExecutionFilePath;
            configPath = configPath.Substring(0, configPath.LastIndexOf('/'));
            if (configPath.Length == 0) configPath = "/";
            Configuration myRootConfig = WebConfigurationManager.OpenWebConfiguration(configPath);
            AppSettingsSection myAppSettings = myRootConfig.GetSection(section) as AppSettingsSection;
            return myAppSettings;
        }


        private static string Section
        {
            get 
            {
                return "SiteSettings/" + Domain; //This references the site section in the SiteSettings section group of the web.config
            }
        }


        private static string Domain
        {
            get
            {
                string sDomain = "qsp.com"; //Default
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["DomainString"] != null)
                {
                    sDomain = HttpContext.Current.Session["DomainString"].ToString();
                }
                return sDomain;
            }
        }
    }
}
