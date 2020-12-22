(function () {
   "use strict";
   var module = angular.module("lisa.addresses");

   function AddressFactory($resource, hosts) {
      return {
         Country: $resource(hosts.webApiCoreBaseUrl + "/country/"),
         Region: $resource(hosts.webApiCoreBaseUrl + "/region/")
      };
   }
   AddressFactory.$inject = ["$resource", "hosts"];
   module.factory("AddressFactory", AddressFactory);

})();