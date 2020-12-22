(function () {
   "use strict";
   var module = angular.module("fundraising.products");

   function Reviews() {
      return {
         scope: {
            datasource: '=',
            max: '='
         },
         templateUrl: "/Scripts/app/fundraising/templates/reviewsTemplate.html"
      };
   }
   Reviews.$inject = [];
   module.directive("reviews", Reviews);
})();