(function () {
   "use strict";
   var module = angular.module("lisa.products");

   function ProductFactory($resource, hosts) {
      return {
         ProductClass: $resource(hosts.webApiCoreBaseUrl + "/productclass/"),
         Review: $resource(hosts.webApiFundraisingBaseUrl + "/reviews/", null, { 'update': { method: 'PUT' } }),
         Product: $resource(hosts.webApiFundraisingBaseUrl + "/products/", null, { 'update': { method: 'PUT' } })
      };
   }
   ProductFactory.$inject = ["$resource", "hosts"];
   module.factory("ProductFactory", ProductFactory);

})();