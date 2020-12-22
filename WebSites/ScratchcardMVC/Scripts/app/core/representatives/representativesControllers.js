(function () {
   "use strict";
   var module = angular.module("core.representatives");

   function RepresentativesController($log, RepresentativesFactory, $scope) {
      var vm = this;
      vm.scope = $scope;
      vm.Representative = {};
      vm.FundraiserCategories = RepresentativesFactory.FundraiserCategory.query({ isArray: true, cache: true });

      if ($scope.$storage.RepresentativeId !== null && $scope.$storage.RepresentativeId !== undefined && $scope.$storage.RepresentativeId > 0) {
         vm.Representative = $scope.$storage.Representative = RepresentativesFactory.Representative.get({ id: $scope.$storage.RepresentativeId }, { isArray: false, cache: true });
      }
      
      vm.GetFundraiserProducts = function(categoryId) {
         vm.FundraiserProducts = RepresentativesFactory.FundraiserProduct.query({categoryId: categoryId}, { isArray: true, cache: true });
      };
      vm.GetFundraiserProduct = function (id) {
         vm.FundraiserProduct = RepresentativesFactory.FundraiserProduct.get({ id: id }, { isArray: false, cache: true });
      };
   }
   RepresentativesController.$inject = ["$log", "RepresentativesFactory", "$scope"];
   module.controller("RepresentativesController", RepresentativesController);

   function RepresentativeInformationRequestedController($window, $scope, $log, $q, NotificationFactory, AddressHygieneFactory, LeadsFactory, MasksConstants, ExceptionFactory) {
      var vm = this;
      vm.countries = AddressHygieneFactory.Countries;
      vm.states = AddressHygieneFactory.States.Get(vm.countries[0]);
      vm.view = { Address: { Region: vm.states[0], Country: vm.countries[0] }, RequestType: 2, KitType: 39, PartnerId: $scope.$storage.PartnerId, PromotionId: 11915, RepresentativeId: $scope.$storage.RepresentativeId, ChannelCode: 'INT' }; //Default Initialization
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
   RepresentativeInformationRequestedController.$inject = ["$window", "$scope", "$log", "$q", "NotificationFactory", "AddressHygieneFactory", "LeadsFactory", "MasksConstants", "ExceptionFactory"];
   module.controller("RepresentativeInformationRequestedController", RepresentativeInformationRequestedController);

})();