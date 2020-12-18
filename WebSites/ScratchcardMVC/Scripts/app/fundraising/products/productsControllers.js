(function () {
   "use strict";
   var module = angular.module("fundraising.products");

   function ProductsController(ProductsFactory, $scope, $log) {
      var vm = this;
      vm.scope = $scope;
      vm.SuggestedProducts = [];
      vm.Products = [];
      vm.Price = 0;
      vm.SortBy = 0;
      vm.Profit = 0;
      vm.ProductTypes = [];
      vm.SubCategories = [];
      vm.Product = {Profits: []};
      vm.Quantity = 0;
      vm.ActionState = 0;
      vm.FeaturedProducts = ProductsFactory.Product.query({ isFeatured: true }, { isArray: true, cache: true });
      vm.Get = function(productId) {
         ProductsFactory.Product.get({ id: productId }, { isArray: false, cache: true }).$promise.then(
            function(data) {
               vm.Product = data;
               vm.Quantity = vm.Product.MinimumQuantity;
               vm.Calculate();
            });
      };
      vm.GetSuggestedProducts = function (productId, countryId, maxResults) {
         vm.SuggestedProducts = ProductsFactory.Product.query({ currentProductId: productId, country: countryId, maxSuggestions: maxResults }, { isArray: true, cache: false });
      };
      vm.Calculate = function() {
         ProductsFactory.Product.UpdatePrice(vm.Product, vm.Quantity);
         ProductsFactory.Product.UpdateShippingFee(vm.Product, vm.Quantity);
      };
      vm.GetFilteredProducts = function () {
         vm.ActionState = 1;
         var stringTypes = "";
         for (var i = 0; i < vm.ProductTypes.length; i++) {
            stringTypes += vm.ProductTypes[i] + ",";
         }
         ProductsFactory.Product.query({ price: vm.Price, profit: vm.Profit, productTypes: stringTypes }, { isArray: true, cache: true }).$promise.then(function (data) {
            vm.Products = data;
            for (var i = 0; i < vm.Products.length; i++) {
               var subCategoryFound = false;
               for (var j = 0; j < vm.SubCategories.length; j++) {
                  subCategoryFound = subCategoryFound || vm.SubCategories[j] === vm.Products[i].Category.Name;
               }
               if (!subCategoryFound) {
                  vm.SubCategories.push(vm.Products[i].Category.Name);
               }
            }
            vm.ActionState = 0;
         });
      };
      vm.GetProductsByCategory = function (categoryId) {
         vm.Products = ProductsFactory.Product.query({ categoryId: categoryId }, { isArray: true, cache: true });
      };
   }
   ProductsController.$inject = ["ProductsFactory", "$scope", "$log"];
   module.controller("ProductsController", ProductsController);

})();