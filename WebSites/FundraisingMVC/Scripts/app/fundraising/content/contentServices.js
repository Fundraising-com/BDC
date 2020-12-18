(function () {
   "use strict";
   var module = angular.module("fundraising.content");

   function BannerFactory($resource, hosts) {
      return {
         Banner: $resource(hosts.webApiFundraisingBaseUrl + "/banners/:id", null,
         {
            "query": { method: "GET", isArray: true, cache: true }
         }),
         ViewPort: $resource(hosts.webApiFundraisingBaseUrl + "/viewports/:id"),
         BlogCategory: $resource(hosts.webApiFundraisingBaseUrl + "/blogcategories/:id"),
         BlogEntry: $resource(hosts.webApiFundraisingBaseUrl + "/blog/:id"),
         BlogTag: $resource(hosts.webApiFundraisingBaseUrl + "/blogtags/:id"),
         HomePageRotator: $resource(hosts.webApiFundraisingBaseUrl + "/homepagerotator/:id")
      };
   }   
   BannerFactory.$inject = ["$resource", "hosts"];
   module.factory("BannerFactory", BannerFactory);



})();