(function () {
   "use strict";
   var module = angular.module("core.representatives");

   function RepresentativesController($log, RepresentativesFactory, $scope, $localStorage, $rootScope) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.Representative = {};
      vm.Representative = $localStorage.Representative;
      vm.FundraiserCategories = RepresentativesFactory.FundraiserCategory.query({ isArray: true, cache: true });
      vm.FundraiserProducts = RepresentativesFactory.FundraiserProducts.query({ isArray: false, cache: true });

      $rootScope.$on('representativeLoaded', function () {
         vm.Representative = $localStorage.Representative;
      });
      
      
      vm.GetFundraiserProducts = function(categoryId) {
         vm.FundraiserProducts = RepresentativesFactory.FundraiserProduct.query({categoryId: categoryId}, { isArray: true, cache: true });
      };
      vm.GetFundraiserProduct = function (id) {
         vm.FundraiserProduct = RepresentativesFactory.FundraiserProduct.get({ id: id }, { isArray: false, cache: true });
      };
      
      

   }
   RepresentativesController.$inject = ["$log", "RepresentativesFactory", "$scope", "$localStorage", "$rootScope"];
   module.controller("RepresentativesController", RepresentativesController);

   function RepresentativeInformationRequestedController($window, $scope, $log, $q, NotificationFactory, AddressHygieneFactory, LeadsFactory, MasksConstants, ExceptionFactory, $localStorage) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.countries = AddressHygieneFactory.Countries;
      vm.states = AddressHygieneFactory.States.Get(vm.countries[0]);
      vm.view = { Address: { Region: vm.states[0], Country: vm.countries[0] }, RequestType: 2, KitType: 39, PartnerId: $scope.$storage.Partner.Id, PromotionId: 11915, RepresentativeId: $scope.$storage.Representative.Id, ChannelCode: 'INT' }; //Default Initialization
      vm.ActionState = 0;
      vm.progress = 0;
      vm.phoneMask = MasksConstants.Phone;
      vm.Send = function () {
         vm.progress = 20;
         vm.ActionState = 1;
         LeadsFactory.Lead.save(vm.view).$promise.then(
            function (newLead) {
               vm.progress = 35;
               var notification = { Type: 3, ExternalId: newLead.Id, Email: $scope.$storage.Representative.Email }
               NotificationFactory.Notification.save(notification);
               notification = { Type: newLead.IsDuplicated ? 6 : 2, ExternalId: newLead.Id, Email: "fake@fake.com" }
               NotificationFactory.Notification.save(notification);
               vm.progress = 60;
               $window.location.href = "/representatives/thank-you";
               vm.progress = 90;
            },
            function (error) {
               vm.ActionState = 3;
               vm.progress = 0;
               vm.error = ExceptionFactory.Handle(error.data);
            });
      };
      vm.UpdateStates = function () {
         vm.states = AddressHygieneFactory.States.Get(vm.view.Address.Country);
         vm.view.Address.Region = vm.states[0];
      };
   }
   RepresentativeInformationRequestedController.$inject = ["$window", "$scope", "$log", "$q", "NotificationFactory", "AddressHygieneFactory", "LeadsFactory", "MasksConstants", "ExceptionFactory", "$localStorage"];
   module.controller("RepresentativeInformationRequestedController", RepresentativeInformationRequestedController);

})();