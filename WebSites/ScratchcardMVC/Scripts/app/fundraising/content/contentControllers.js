(function () {
   "use strict";
   var module = angular.module("fundraising.content");

   function BannersController($log, $scope, BannerFactory) {
      var vm = this;
      vm.scope = $scope;
      vm.banners = [];
      vm.GetAll = function(type, sort) {
         vm.banners = BannerFactory.Banner.query({type: type, partnerId: vm.scope.$storage.PartnerId, sort: sort}, { isArray: true, cache: true });
      }
   }
   BannersController.$inject = ["$log", "$scope", "BannerFactory"];
   module.controller("BannersController", BannersController);

})();