(function () {
   "use strict";
   var app = angular.module("fundraising", [
   "ngAnimate",
   "ngStorage",
   "ngLocale",
    "ui.directives",
    "ui.filters",
    "ui.bootstrap",
    "ui.mask",
    "angular.filter",
    "core.notifications",
    "core.leads",
    "core.helpers"
   ]);
   app.config(["$locationProvider", function ($locationProvider) {
      $locationProvider.html5Mode({ enabled: true, requireBase: false, rewriteLinks: false });
   }]);
   app.constant('MasksConstants', {
      Phone: "(999) 999 9999",
      CreditCardNumber: "9999 9999 9999 999?9",
      CreditCardCVV: "999?9",
      CreditCardExpirationDate: "9999",
      USPostCode: "99999",
      CanadaPostCode: "******"
   });
   app.filter('percentage', ["$filter", function ($filter) {
      return function (input, decimals) {
         return $filter('number')(input * 100, decimals) + '%';
      };
   }]);
   app.filter('toCurrencyCode', [function () {
      return function (input) {
         switch (input) {
            case "US":
               return "USD";
            case "CA":
               return "CAD";
            default:
               return "";
         }
      };
   }]);
   app.filter('to_trusted', ['$sce', function ($sce) {
      return function (text) {
         return $sce.trustAsHtml(text);
      };
   }]);
   app.filter('makeRange', function () {
      return function (input) {
         var lowBound, highBound;
         switch (input.length) {
            case 1:
               lowBound = 0;
               highBound = parseInt(input[0]) - 1;
               break;
            case 2:
               lowBound = parseInt(input[0]);
               highBound = parseInt(input[1]);
               break;
            default:
               return input;
         }
         var result = [];
         for (var i = lowBound; i <= highBound; i++)
            result.push(i);
         return result;
      };
   });
})();