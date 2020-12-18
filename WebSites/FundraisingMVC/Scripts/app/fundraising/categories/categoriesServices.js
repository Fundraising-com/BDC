(function () {
   "use strict";
   var module = angular.module("fundraising.categories");

   function CategoriesFactory($resource, hosts) {
      return {
         Category: $resource(hosts.webApiFundraisingBaseUrl + "/categories/:id")
      };
   }
   CategoriesFactory.$inject = ["$resource", "hosts"];
   module.factory("CategoriesFactory", CategoriesFactory);
})();