using System.Web;
using System.Web.Optimization;

namespace GA.BDC.Web.SportsApparel.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angularUI/angular-ui.js"
                      
                        
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/*.css"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
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
                  "~/Scripts/app/fundraising/categories/categoriesModules.js",
                 "~/Scripts/app/fundraising/categories/categoriesServices.js",
                 "~/Scripts/app/fundraising/categories/categoriesControllers.js",
                 "~/Scripts/app/fundraising/products/productsModules.js",
                 "~/Scripts/app/fundraising/products/productsServices.js",
                 "~/Scripts/app/fundraising/products/productsControllers.js",
                 "~/Scripts/app/fundraising/products/productsDirectives.js",
                 "~/Scripts/app/modules.js"));
           
        }
    }
}
