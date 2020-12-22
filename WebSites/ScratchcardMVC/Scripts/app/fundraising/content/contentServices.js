(function () {
   "use strict";
   var module = angular.module("fundraising.content");

   function BannerFactory($resource, hosts) {
      return {
         Banner: $resource(hosts.webApiFundraisingBaseUrl + "/banners/:id", null,
         {
            "query": { method: "GET", isArray: true, cache: true }
         }),
         ViewPort: $resource(hosts.webApiFundraisingBaseUrl + "/viewports/:id")
      };
   }   
   BannerFactory.$inject = ["$resource", "hosts"];
   module.factory("BannerFactory", BannerFactory);
})();