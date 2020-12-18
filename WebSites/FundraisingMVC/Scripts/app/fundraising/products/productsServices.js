(function () {
   "use strict";
   var module = angular.module("fundraising.products");

   function ProductsFactory($resource, hosts) {
      var Product = $resource(hosts.webApiFundraisingBaseUrl + "/products/:id", null, {
         "update": { method: "PUT" }
      });
      Product.UpdatePrice = function(product, quantity) {
         for (var i = 0; i < product.Profits.length; i++) {
            var profit = product.Profits[i];
            if (profit.Min <= quantity && profit.Max >= quantity) {
               product.CalculatedPrice = profit.Price;
            }
         }
      };
      Product.UpdateShippingFee = function(product, price) {
         var shippingFees = product.Category.ShippingFees;
         product.ShippingFee = 0;
         for (var i = 0; i < shippingFees.length; i++) {
            var shippingFee = shippingFees[i];
            if (shippingFee.MinimumQuantity <= price && shippingFee.MaximumQuantity >= price) {
               product.ShippingFee = shippingFee.Fee;
               break;
            }
         }
      };
      return {
         Product: Product
      };
   }
   ProductsFactory.$inject = ["$resource", "hosts"];
   module.factory("ProductsFactory", ProductsFactory);
})();