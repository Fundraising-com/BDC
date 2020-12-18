(function () {
   "use strict";
   var module = angular.module("core.helpers");

   function SessionController($scope, $localStorage) {
      var vm = this;
      $scope.$storage = $localStorage;

      vm.SetSession = function (partnerId, promotionId, representativeId, anonymousId) {
         if (partnerId !== null && partnerId !== undefined) {
            $scope.$storage.PartnerId = partnerId;
         }
         if (promotionId !== null && promotionId !== undefined) {
            $scope.$storage.PromotionId = promotionId;
         }
         if (representativeId !== null && representativeId !== undefined) {
            if ($scope.$storage.RepresentativeId === undefined) {
               $scope.$storage.RepresentativeId = representativeId;
            } else {
               if (representativeId > 0) {
                  $scope.$storage.RepresentativeId = representativeId;
               }
            }
         }
         if (anonymousId !== null && anonymousId !== undefined) {
            $scope.$storage.AnonymousId = anonymousId;
         }
         if ($scope.$storage.ShoppingCart === undefined || $scope.$storage.ShoppingCart === null) {
            $scope.$storage.ShoppingCart = { Id: 0, AnonymousId: $scope.$storage.AnonymousId, Items: [] };
         }
         if ($scope.$storage.ConsultantId === undefined) {
            $scope.$storage.ConsultantId = $scope.$storage.PromotionId == 5953 ? 3518 : 3450;
         }
      }
   }
   SessionController.$inject = ["$scope", "$localStorage"];
   module.controller("SessionController", SessionController);

   function SearchController($window) {
      var vm = this;
      vm.q = "";

      vm.Search = function () {
         $window.location.href = "/search?q=" + vm.q;
      }
   }
   SearchController.$inject = ["$window"];
   module.controller("SearchController", SearchController);




   function DetectCountryController($window, $scope, $log) {
        var vm = this;
        vm.country = 0;
       
        if ($window.location.href.contains("canada")) {
                   vm.country = 1;
               }
        //vm.DetectCountry = function () {

        //    if ($window.location.href.contains("canada")) {
        //        vm.country = 1;
        //    }
        //}

          
     
    }
   DetectCountryController.$inject = ["$window", "$scope", "$log"];
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