(function () {
   "use strict";
   var module = angular.module("core.representatives");

   function RepresentativesFactory($log, $resource, hosts) {
      return {
         Representative: $resource(hosts.webApiCoreBaseUrl + "/representatives/:id"),
         FundraiserCategory: $resource(hosts.webApiCoreBaseUrl + "/fundraisercategories/:id"),
         FundraiserProduct: $resource(hosts.webApiCoreBaseUrl + "/fundraiserproducts/:id")
      }
   }
   RepresentativesFactory.$inject = ["$log", "$resource", "hosts"];
   module.factory("RepresentativesFactory", RepresentativesFactory);
})();