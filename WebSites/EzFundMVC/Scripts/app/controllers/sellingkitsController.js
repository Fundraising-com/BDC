(function () {
    "use strict";
    var module = angular.module("ezfund.api");
    function SellingKitFormController($http ,$log, $scope, $timeout, SellingKitsLeadsFactory, LeadsFactory, $localStorage, $window, AddressHygieneFactory, MasksConstants, $rootScope, ExceptionFactory) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.executing = false;

        vm.countries = AddressHygieneFactory.Countries;
        vm.states = AddressHygieneFactory.States.Get(vm.countries[0]);
        vm.ActionState = 0;
        vm.progress = 0;
        vm.MasksConstants = MasksConstants;
        vm.kit = { PrizeRequired: "2"};

        SellingKitsLeadsFactory.PrimaryProgram.query().$promise.then(
            function (result) {
                if (result.length > 0) {
                    vm.sellingkitOptions = result;
                }
            },
            function (error) {
                $log.error(error);
            });

        SellingKitsLeadsFactory.OrganizationType.query().$promise.then(
            function (result) {
                if (result.length > 0) {
                    vm.organizationtTypeOptions = result;
                }
            },
            function (error) {
                $log.error(error);
            });

        LeadsFactory.Referral.query().$promise.then(
            function (result) {
                if (result.length > 0) {
                    vm.learnAboutUsOptions = result;
                }
            },
            function (error) {
                $log.error(error);
            });

            vm.Send = function () {
            vm.ActionState = 1;
            vm.progress = 20;




                //get IP addres from client
            var url = "https://freegeoip.net/json/";
            $http.get(url).then(function (response) {
                vm.kit.RemoteIpAddress = response.data.ip;
            });

            if (vm.kit.PrizeRequired == "1") {
                vm.kit.PrizeRequired = true;
            }
            else {
                vm.kit.PrizeRequired = false;
            }

           

            SellingKitsLeadsFactory.SellingKitLead.save(vm.kit).$promise.then(
                           function (sellingkitlead) {
                               vm.progress = 60;
                               $timeout(function () {
                                   vm.progress = 90;
                                   $window.location.href = "/selling-kit-confirmation";
                                   vm.progress = 95;
                               }, 2500);
                           }, function (error) {
                               vm.ActionState = 3;
                               vm.progress = 0;
                               vm.error = ExceptionFactory.Handle("There was a problem submitting the form, Please contact customer service for assistance 1-800-991-8779");
                           });




        };

    }
    SellingKitFormController.$inject = ["$http", "$log", "$scope", "$timeout", "SellingKitsLeadsFactory", "LeadsFactory", "$localStorage", "$window", "AddressHygieneFactory", "MasksConstants", "$rootScope", "ExceptionFactory"];
    module.controller("SellingKitFormController", SellingKitFormController);
})();