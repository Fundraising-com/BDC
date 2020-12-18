(function () {
   "use strict";
   var module = angular.module("ezfund.core");

   function PartnerController($log, $scope, CoreFactory, $localStorage, $rootScope, $q) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.executing = false;
      vm.GetPartner = function(id) {
      	vm.partner = CoreFactory.Partner.get({ id: id }, { cache: true });
      };
      vm.slides = [{ image: '/Content/images/public/test-banner.png', id: 1 }, { image: '/Content/images/public/test-banner.png', id: 2 }];
   }

   PartnerController
      .$inject = ["$log", "$scope", "CoreFactory", "$localStorage", "$rootScope", "$q"];
   module.controller("PartnerController", PartnerController);

})();