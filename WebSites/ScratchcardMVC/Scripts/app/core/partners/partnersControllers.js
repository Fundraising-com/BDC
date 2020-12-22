(function () {
   "use strict";
   var module = angular.module("core.partners");

   function PartnersController(PartnersFactory, $scope) {
      var vm = this;
      vm.scope = $scope;
      vm.Partner = PartnersFactory.Partner.get({ id: $scope.$storage.PartnerId }, { isArray: false, cache: true });
   }
   PartnersController.$inject = ["PartnersFactory", "$scope"];
   module.controller("PartnersController", PartnersController);

})();