(function () {
   "use strict";
   var module = angular.module("lisa.leads");

   function LeadFactory($resource, hosts) {
      return {
         OrganizationType: $resource(hosts.webApiCoreBaseUrl + "/organizationtype/"),
         GroupType: $resource(hosts.webApiCoreBaseUrl + "/grouptypes/"),
         Lead: $resource(hosts.webApiCoreBaseUrl + "/leads/")
      };
   }
   LeadFactory.$inject = ["$resource", "hosts"];
   module.factory("LeadFactory", LeadFactory);

})();