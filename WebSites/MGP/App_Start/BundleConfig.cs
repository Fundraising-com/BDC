using System.Web.Optimization;

namespace GA.BDC.Web.MGP
{
   public class BundleConfig
   {
      public static void RegisterBundles(BundleCollection bundleCollection)
      {
         var bundle = new ScriptBundle("~/jquery").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/bootstrap.js");

         bundleCollection.Add(bundle);

         bundle = new ScriptBundle("~/angular").Include("~/Scripts/angular.js", "~/Scripts/angular-animate.js",
            "~/Scripts/angular-file-upload/angular-file-upload.js",
            "~/Scripts/angular-file-upload/angular-file-upload-shim.js", "~/Scripts/angular-ui.js",
            "~/Scripts/angular-ui/ui-bootstrap-tpls.js", "~/Scripts/ng-grid-2.0.11.min.js", "~/Scripts/ng-wig.js", "~/Scripts/ng-file-upload.js", "~/Scripts/ng-file-upload-shim.js", "~/Scripts/ng-img-crop.js");
         bundleCollection.Add(bundle);

         bundle = new ScriptBundle("~/app").Include("~/Scripts/app/modules.js", "~/Scripts/app/controllers.js", "~/Scripts/app/services.js");
         bundleCollection.Add(bundle);

         bundle = new StyleBundle("~/general").Include("~/Content/bootstrap.css", "~/Content/ng-wig.css", "~/Content/font-awesome.min.css", "~/Content/bootstrap-social.css");
         bundleCollection.Add(bundle);

      }
   }
}