(function () {
   "use strict";
   var app = angular.module("ezfund", [
   "ui.bootstrap",
   "ngAnimate",
   "ngStorage",
   "ngResource",
   "ngCookies",
   "ngMessages",
   "ui.mask",
   "ezfund.core",
   "ezfund.api",
   "ezfund.helpers",
   "slick"
   ]);
   app.config(["$locationProvider", function ($locationProvider) {
      $locationProvider.html5Mode({ enabled: true, requireBase: false, rewriteLinks: false });
   }]);   

   app.run([
      "$localStorage", "$location", "$q", "$rootScope",
      function ($localStorage, $location, PartnersFactory, RepresentativesFactory, $q, $rootScope) {
      // Shopping Cart
          if ($localStorage.ShoppingCart === undefined || $localStorage.ShoppingCart === null) {
            $localStorage.ShoppingCart = { Id: 0, AnonymousId: "0", Items: [] };
        }

      }
   ]);
   app.constant("MasksConstants", {
       Phone: "(999) 999 9999",
       CreditCardNumber: "9999 9999 9999 999?9",
       CreditCardCVV: "999",
       CreditCardCVVAmex: "9999",
       CreditCardExpirationDate: "9999",
       USPostCode: "99999",
       CanadaPostCode: "******"
   });
   app.filter('percentage', ["$filter", function ($filter) {
       return function (input, decimals) {
           return $filter('number')(input * 100, decimals) + "%";
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