using System.Web.Optimization;

namespace GA.BDC.Web.Lisa
{
   public class BundleConfig
   {
      public static void RegisterBundles(BundleCollection bundles)
      {
         bundles.Add(new ScriptBundle("~/bundles/angular").Include(
            "~/Scripts/ngStorage.js",
            "~/Scripts/Chart.min.js",
            "~/Scripts/md-data-table.min.js",
            "~/Scripts/angular-chart.js",
            "~/Scripts/angular-animate/angular-animate.js",
            "~/Scripts/angular-material/angular-material.js",
            "~/Scripts/angular-material/angular-material-sidemenu.js",
            "~/Scripts/angular-aria/angular-aria.js",
            "~/Scripts/lisa/modules/*.js",
            "~/Scripts/lisa/services/*.js",
            "~/Scripts/lisa/controllers/*.js",
            "~/Scripts/lisa/modules.js",
            "~/Scripts/lisa/directives/*.js"
         ));

         bundles.Add(new StyleBundle("~/Content/css").Include(
            "~/Content/*.css"
         ));
      }
   }
}