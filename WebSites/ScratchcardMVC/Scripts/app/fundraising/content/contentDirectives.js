(function () {
   "use strict";
   var module = angular.module("fundraising.content");

   function bannerRotator($log, BannerFactory) {
      return {
         restrict: "E",
         replace: true,
         controller: function ($scope) {
            $scope.viewPorts = BannerFactory.ViewPort.query({ isArray: true, cache: true });
         },
         templateUrl: "/Scripts/app/fundraising/templates/bannerRotatorTemplate.html"
      };
   }
   bannerRotator.$inject = ["$log", "BannerFactory"];
   module.directive("bannerRotator", bannerRotator);

   function bannerRotatorImage($log, BannerFactory) {
      return {
         restrict: "E",
         scope: {
            viewPort: "="
         },
         replace: true,
         controller: function ($scope) {
            $scope.slides = BannerFactory.Banner.query({ type: 1, partnerId: $scope.$parent.$parent.partnerId, sort: true, viewPortId: $scope.viewPort.Id }, { isArray: true, cache: true });
         },
         templateUrl: "/Scripts/app/fundraising/templates/bannerRotatorImageTemplate.html"
      };
   }
   bannerRotatorImage.$inject = ["$log", "BannerFactory"];
   module.directive("bannerRotatorImage", bannerRotatorImage);
})();