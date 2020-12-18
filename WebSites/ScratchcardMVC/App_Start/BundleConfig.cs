using System.Web.Optimization;

namespace GA.BDC.Web.Scratchcard.MVC
{
    public class BundleConfig
    {
         // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
            public static void RegisterBundles(BundleCollection bundles)
            {

                var bundle = new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/app/*.js",
                      "~/Scripts/app/core/notifications/notificationsModules.js",
                      "~/Scripts/app/core/notifications/notificationsServices.js",
                      "~/Scripts/app/core/notifications/notificationsControllers.js",
                      
                    
                      "~/Scripts/app/core/leads/leadsModules.js",
                      "~/Scripts/app/core/leads/leadsControllers.js",
                      "~/Scripts/app/core/leads/leadsServices.js",
                      "~/Scripts/app/core/helpers/helpersModules.js",
                      "~/Scripts/app/core/helpers/helpersServices.js",
                      "~/Scripts/app/core/helpers/helpersControllers.js",
                     
                    
                     
                      "~/Scripts/app/modules.js"
                      );
                bundles.Add(bundle);



            }
        }
    }
