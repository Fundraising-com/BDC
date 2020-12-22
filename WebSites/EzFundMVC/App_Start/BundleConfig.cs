using System.Web.Optimization;

namespace GA.BDC.Web.EzFundMVC
{
   public class BundleConfig
   {
      public static void RegisterBundles(BundleCollection bundles)
      {
         bundles.Add(new ScriptBundle("~/angular").Include(
                     "~/Scripts/angular.min.js",
                     "~/Scripts/angular-animate.min.js",
                     "~/Scripts/angular-cookies.min.js",
                     "~/Scripts/angular-messages.min.js",
                     "~/Scripts/angular-mocks.js",
                     "~/Scripts/angular-resource.min.js",
                     "~/Scripts/angular-route.min.js",
                     "~/Scripts/angular-sanitize.min.js",
                     "~/Scripts/ngStorage.js",
                     "~/Scripts/slick-carousel.js",
                     "~/Scripts/angular-slick.js",
                     "~/Scripts/mask.js"
                     ));

         bundles.Add(new ScriptBundle("~/jquery").Include(
                     "~/Scripts/jquery-3.1.1.min.js"
                     ));

         bundles.Add(new ScriptBundle("~/bootstrap").Include(
                     "~/Scripts/bootstrap.min.js"
                     ));

         bundles.Add(new ScriptBundle("~/bootstrap-ui").Include(
                     "~/Scripts/angular-ui/ui-bootstrap.min.js",
                     "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js"
                     ));

         bundles.Add(new ScriptBundle("~/application").Include(
                     "~/Scripts/app/*.js",
                     "~/Scripts/app/modules/*.js",
                     "~/Scripts/app/services/*.js",
                     "~/Scripts/app/controllers/*.js",
                     "~/Scripts/app/directives/*.js",
                     "~/Scripts/app/templates/*.js"));

         bundles.Add(new StyleBundle("~/bootstrap").Include(
                   "~/Content/styles/bootstrap/bootstrap.min.css",
                   "~/Content/styles/bootstrap/ui-bootstrap-csp.css",
                   "~/Content/styles/bootstrap/*.css"
                   ));

         bundles.Add(new StyleBundle("~/ezfund/styles").Include(
                   "~/Content/site.css",
                   "~/Content/footer.css",
                   "~/Content/styles/angular/*.css"
                   ));
        bundles.Add(new StyleBundle("~/ezfund/styles/slick").Include(
                "~/Content/styles/slick/slick.css",
                "~/Content/styles/slick/slick-theme.css"
                ));
        }
   }
}
