(function () {
   "use strict";
   var module = angular.module("core.sales");

   function ShoppingCartFactory($resource, hosts) {
      var ShoppingCart = $resource(hosts.webApiFundraisingBaseUrl + "/shoppingCarts/:id", null, {
         "update": { method: "PUT" }
      });

      return {
         ShoppingCart: ShoppingCart
      };
   }
   ShoppingCartFactory.$inject = ["$resource", "hosts"];
   module.factory("ShoppingCartFactory", ShoppingCartFactory);

   function SalesFactory($resource, hosts) {
      var Sale = $resource(hosts.webApiCoreBaseUrl + "/sales/:id", null, {
         "update": { method: "PUT" }
      });
      return {
         Sale: Sale,
         Paypal: $resource(hosts.webApiCoreBaseUrl + "/paypal/:id"),
         CreditCard: $resource(hosts.webApiCoreBaseUrl + "/creditcard/:id"),
         Payment: $resource(hosts.webApiCoreBaseUrl + "/payments/:id"),
         PromotionCode: $resource(hosts.webApiFundraisingBaseUrl + "/promotioncodes/:id")
      };
   }
   SalesFactory.$inject = ["$resource", "hosts"];
   module.factory("SalesFactory", SalesFactory);
})();