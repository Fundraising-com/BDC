(function () {
   "use strict";
   var module = angular.module("lisa.partners");

   function PartnerFactory($resource, hosts) {
      return {
         Partner: $resource(hosts.webApiCoreBaseUrl + "/partners/")
      };
   }
   PartnerFactory.$inject = ["$resource", "hosts"];
   module.factory("PartnerFactory", PartnerFactory);

})();