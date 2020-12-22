using System.Web.Optimization;

namespace GA.BDC.Web.Fundraising.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
           var bundle = new ScriptBundle("~/bundles/angular").Include(
              "~/Scripts/angularUI/angular-ui.js",
              "~/Scripts/angularUI/ui-bootstrap-0.13.4.min.js",
              "~/Scripts/angular/mask.js",
              "~/Scripts/angularUI/carouselFix.js",
              "~/Scripts/angular/angular-slick.js",
              "~/Scripts/slick-carousel.js"
              );
           bundles.Add(bundle);

            bundle = new ScriptBundle("~/bundles/flipbook").Include(
              "~/Scripts/flipbook/jquery-2.1.0.min.js",
              "~/Scripts/flipbook/jquery-ui-1.10.4.min.js",
              "~/Scripts/flipbook/jquery.booklet.latest.js",
              "~/Scripts/flipbook/jquery.easing.1.3.js",
              "~/Scripts/flipbook/jquery.booklet.latest.min.js"
              );
            bundles.Add(bundle);


            bundle = new ScriptBundle("~/bundles/app").Include(
                 "~/Scripts/app/*.js",
                 "~/Scripts/app/core/notifications/notificationsModules.js",
                 "~/Scripts/app/core/notifications/notificationsServices.js",
                 "~/Scripts/app/core/notifications/notificationsControllers.js",
                 "~/Scripts/app/fundraising/content/contentModules.js",
                 "~/Scripts/app/fundraising/content/contentControllers.js",
                 "~/Scripts/app/fundraising/content/contentServices.js",
                 "~/Scripts/app/fundraising/content/contentDirectives.js",
                 "~/Scripts/app/core/partners/partnersModules.js",
                 "~/Scripts/app/core/partners/partnersControllers.js",
                 "~/Scripts/app/core/partners/partnersServices.js",
                 "~/Scripts/app/core/leads/leadsModules.js",
                 "~/Scripts/app/core/leads/leadsControllers.js",
                 "~/Scripts/app/core/leads/leadsServices.js",
                 "~/Scripts/app/core/helpers/helpersModules.js",
                 "~/Scripts/app/core/helpers/helpersServices.js",
                 "~/Scripts/app/core/helpers/helpersControllers.js",
                 "~/Scripts/app/core/representatives/representativesModules.js",
                 "~/Scripts/app/core/representatives/representativesServices.js",
                 "~/Scripts/app/core/representatives/representativesControllers.js",
                 "~/Scripts/app/core/sales/salesModules.js",
                 "~/Scripts/app/core/sales/salesServices.js",
                 "~/Scripts/app/core/sales/salesControllers.js",
                 "~/Scripts/app/fundraising/categories/categoriesModules.js",
                 "~/Scripts/app/fundraising/categories/categoriesServices.js",
                 "~/Scripts/app/fundraising/categories/categoriesControllers.js",
                 "~/Scripts/app/fundraising/products/productsModules.js",
                 "~/Scripts/app/fundraising/products/productsServices.js",
                 "~/Scripts/app/fundraising/products/productsControllers.js",
                 "~/Scripts/app/fundraising/products/productsDirectives.js",
                 "~/Scripts/app/modules.js"
                 );
           bundles.Add(bundle);
           
           bundle = new StyleBundle("~/bundles/styles").Include(
                 
               "~/Content/styles/core.css",
                 "~/Content/styles/footer.css",
                 "~/Content/styles/angular/*.css",

                 "~/Content/styles/menu-min.css",
                 "~/Content/styles/brochureflipbook/jquery.booklet.latest.css",
                  "~/Content/styles/bootstrap/*.css",
                  "~/Content/styles/slickJs/slick.css"
                 );
           bundles.Add(bundle);

            
        }
    }
}
