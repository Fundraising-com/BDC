(function () {
   "use strict";
   var module = angular.module("lisa.sales");

   function SalesFactory($resource, hosts) {
      return {
         PromotionCode: $resource(hosts.webApiFundraisingBaseUrl + "/promotioncodes/:id", null, { 'update': { method: 'PUT' } }),
         Consultant: $resource(hosts.webApiCoreBaseUrl + "/consultant/")
      };
   }
   SalesFactory.$inject = ["$resource", "hosts"];
   module.factory("SalesFactory", SalesFactory);

})();