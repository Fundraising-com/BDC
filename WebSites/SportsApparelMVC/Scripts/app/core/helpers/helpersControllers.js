(function () {
   "use strict";
   var module = angular.module("core.helpers");

   function SearchController($window) {
      var vm = this;
      vm.q = "";

      vm.Search = function () {
         $window.location.href = "/search?q=" + vm.q;
      }
   }
   SearchController.$inject = ["$window"];
   module.controller("SearchController", SearchController);


   function DetectCountryController($window, $scope, $localStorage, $rootScope, DetectCountryFactory) {
      var vm = this;
      vm.scope = $scope;
      vm.country = DetectCountryFactory.CountryId();
   }

   DetectCountryController.$inject = ["$window", "$scope", "$localStorage", "$rootScope", "DetectCountryFactory"];
   module.controller("DetectCountryController", DetectCountryController);

   
   function GoogleAnalyticsController(GoogleAnalyticsService) {
      var vm = this;

      vm.ProductClicked = function (id, name, category, brand, price, position, list) {
         GoogleAnalyticsService.ProductClicked(id, name, category, brand, price, position, list);
      };

      vm.ProductShowed = function (id, name, category, brand, price, position, list) {
         GoogleAnalyticsService.ProductShowed(id, name, category, brand, price, position, list);
      };

      vm.BannerClicked = function (id) {
         GoogleAnalyticsService.BannerClicked(id);
      };

      vm.PopUpKitClicked = function () {
          GoogleAnalyticsService.PopUpKitClicked();
      };

      vm.ProductDetailed = function (id, name, category, brand, price) {
         GoogleAnalyticsService.ProductDetailed(id, name, category, brand, price);
      };
      vm.ProductAdded = function (id, name, category, brand, price, quantity) {
         GoogleAnalyticsService.ProductAdded(id, name, category, brand, price, quantity);
      };
      vm.ProductRemoved = function (id, name, category, brand, price, quantity) {
         GoogleAnalyticsService.ProductRemoved(id, name, category, brand, price, quantity);
      };
   }
   GoogleAnalyticsController.$inject = ["GoogleAnalyticsService"];
   module.controller("GoogleAnalyticsController", GoogleAnalyticsController);
})();