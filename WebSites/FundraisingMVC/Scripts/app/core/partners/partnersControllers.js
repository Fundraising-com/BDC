(function () {
   "use strict";
   var module = angular.module("core.partners");

   function PartnersController(PartnersFactory, $scope, $localStorage, $rootScope) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.Partner = vm.scope.$storage.Partner;
      $rootScope.$on('partnerLoaded', function () {
         vm.Partner = vm.scope.$storage.Partner;
      });
      
   }
   PartnersController.$inject = ["PartnersFactory", "$scope", "$localStorage", "$rootScope"];
   module.controller("PartnersController", PartnersController);

})();