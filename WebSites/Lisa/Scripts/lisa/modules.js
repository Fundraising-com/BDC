(function () {
   "use strict";
   var app = angular.module("lisa", [
   "ngMaterial",
   "ngStorage",
   "ngResource",
   "ngCookies",
   "ngMessages",
   "ngMaterialSidemenu",
   "ngWig",
   "ngSanitize",
   "ngCsv",
   "ngMdIcons",
   "chart.js",
   "md.data.table",
    "lisa.security",
    "lisa.content",
    "lisa.sales",
    "lisa.reports",
    "lisa.addresses",
    "lisa.leads",
    "lisa.products",
    "lisa.partners",
    "lisa.helpers"
   ]);
   app.config(["$locationProvider", function ($locationProvider) {
      $locationProvider.html5Mode({ enabled: true, requireBase: false, rewriteLinks: false });
   }]);
   app.config(['$httpProvider', function ($httpProvider) {
      $httpProvider.interceptors.push('AuthenticationInterceptionFactory');
   }]);
   app.filter('percentage', ['$filter', function ($filter) {
      return function (input, decimals) {
         return $filter('number')(input * 100, decimals) + '%';
      };
   }]);
})();