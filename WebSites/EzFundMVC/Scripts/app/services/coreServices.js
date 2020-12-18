(function () {
   "use strict";
   var module = angular.module("ezfund.core");

   function CoreFactory($resource, hosts) {
      return {
         Partner: $resource(hosts.webApiCoreBaseUrl + "/partners/")         
      };
   }
   CoreFactory.$inject = ["$resource", "hosts"];
   module.factory("CoreFactory", CoreFactory);

})();