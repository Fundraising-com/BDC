(function () {
   "use strict";
   var module = angular.module("core.representatives");

   function RepresentativesFactory($log, $resource, hosts) {
      return {
         Representative: $resource(hosts.webApiCoreBaseUrl + "/representatives/:id"),
         FundraiserCategory: $resource(hosts.webApiCoreBaseUrl + "/fundraisercategories/:id"),
         FundraiserProduct: $resource(hosts.webApiCoreBaseUrl + "/fundraiserproducts/:id"),
         FundraiserProducts: $resource(hosts.webApiCoreBaseUrl + "/fundraiserproducts/"),
         Consultant: $resource(hosts.webApiCoreBaseUrl + "/consultant/:id")
      }
   }
   RepresentativesFactory.$inject = ["$log", "$resource", "hosts"];
   module.factory("RepresentativesFactory", RepresentativesFactory);
})();