(function () {
    "use strict";
    var module = angular.module("core.leads");

    function KitRequestController($window, $scope, $log, $q, $timeout, $localStorage, $modal, $attrs, NotificationFactory, AddressHygieneFactory, LeadsFactory, MasksConstants, ExceptionFactory, $rootScope) {
         var vm = this;
         vm.scope = $scope;
         vm.scope.$storage = $localStorage;
         vm.hidePhone = false;
         vm.ActionState = 0;
         vm.progress = 0;
         vm.phoneMask = MasksConstants.Phone;
         vm.countries = AddressHygieneFactory.Countries;
         vm.states = AddressHygieneFactory.States.Get(vm.countries[0]);
         //vm.kit.ChannelCode = 'INT';
         vm.kit = { Address: { Region: null, Country: vm.countries[0] }, TellMore: 2, ChannelCode: 'INT' }; //Default Initialization. FORCING MERGE
       

         vm.UpdateStates = function () {
             vm.states = AddressHygieneFactory.States.Get(vm.kit.Address.Country);
             vm.kit.Address.Region = vm.states[0];
         };




    }
    KitRequestController.$inject = ["$window", "$scope", "$log", "$q", "$timeout", "$localStorage", "$modal", "$attrs", "NotificationFactory", "AddressHygieneFactory", "LeadsFactory", "MasksConstants", "ExceptionFactory", "$rootScope"];
    module.controller("KitRequestController", KitRequestController);

   
})();