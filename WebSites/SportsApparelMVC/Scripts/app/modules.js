(function () {
   "use strict";
   var app = angular.module("fundraising", [
   "ngAnimate",
   "ngStorage",
   "ngLocale",
   "ngRoute",
    "ui.directives",
    "ui.filters",
    "ui.bootstrap",
    "ui.mask",
    "angular.filter",
    "core.notifications",
    "core.leads",
    "core.helpers",
    "fundraising.categories",
       "fundraising.products",
     
      
      
   
   ]);
    app.constant("MasksConstants", {
        Phone: "(999) 999 9999",
        CreditCardNumber: "9999 9999 9999 999?9",
        CreditCardCVV: "999?9",
        CreditCardExpirationDate: "9999",
        USPostCode: "99999",
        CanadaPostCode: "******"
    });
   

   app.config(["$locationProvider", function ($locationProvider) {
      $locationProvider.html5Mode({ enabled: true, requireBase: false, rewriteLinks: false });
   }]);
   
   app.filter('to_trusted', ['$sce', function ($sce) {
      return function (text) {
         return $sce.trustAsHtml(text);
      };
   }]);
   
})();