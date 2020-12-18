(function () {
   "use strict";
   var module = angular.module("core.partners");

   function PartnersFactory($log, $resource, hosts) {
      return {
         Partner: $resource(hosts.webApiCoreBaseUrl + "/partners/:id"),
         Promotion: $resource(hosts.webApiCoreBaseUrl + "/promotions/:id")
      };
   }
   PartnersFactory.$inject = ["$log", "$resource", "hosts"];
   module.factory("PartnersFactory", PartnersFactory);

})();

