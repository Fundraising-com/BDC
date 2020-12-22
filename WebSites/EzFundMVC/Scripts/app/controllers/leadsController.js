(function () {
    "use strict";
    var module = angular.module("ezfund.api");
    function KitRequestFormController($log, $scope, $timeout, SellingKitsLeadsFactory, LeadsFactory, $localStorage, $window, AddressHygieneFactory, MasksConstants, $rootScope, NotificationFactory, referral) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.executing = false;
        var defaultKitForm = 2;
        var selectedProductsMap = {};
        vm.countries = AddressHygieneFactory.Countries;
        vm.states = AddressHygieneFactory.States.Get(vm.countries[0]);
        vm.ActionState = 0;
        vm.progress = 0;
        vm.MasksConstants = MasksConstants;
        //vm.kit = { TellMore: "1", RequestType: "2" }; //Default Initialization. FORCING MERGE
      
        vm.kit = {
            Address: { Region: null, Country: vm.countries[0] }
        };
        

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
        LeadsFactory.SalesStartingDate.query().$promise.then(
            function (result) {
                if (result.length > 0) {
                    vm.startDateOptions = result;
                }
            },
            function (error) {
                $log.error(error);
            });
        LeadsFactory.Product.query().$promise.then(
            function (result) {
                if (result.length > 0) {
                    vm.productOptions = result;
                }
            },
            function (error) {
                $log.error(error);
            });

        function removeDuplicates(arr) {
            var cleanArr = [];
            for (var i = 0; i < arr.length; i++) {
                if (typeof selectedProductsMap[arr[i]] === 'undefined') {
                    selectedProductsMap[arr[i]] = "ok";
                    cleanArr.push(arr[i]);
                }
            }
            selectedProductsMap = {};
            return cleanArr;
        }
        vm.Send = function () {
            vm.ActionState = 1;
            vm.progress = 20;
				
            var prodsArr = [];
            prodsArr.push(vm.productOptOneSelected.Code); //Should never be null as it is required. If Ever becomes null, do the same as below
            if (typeof vm.productOptTwoSelected !== 'undefined')
        		{
					prodsArr.push(vm.productOptTwoSelected.Code);
            }
				if (typeof vm.productOptThreeSelected !== 'undefined')
    			{
					prodsArr.push(vm.productOptThreeSelected.Code);
				}
				if (prodsArr.length > 1) {
					vm.kit.SelectedProducts = removeDuplicates(prodsArr);
				}
				else
				{
					vm.kit.SelectedProducts = prodsArr;
				}

				if (vm.kit.RequestType == "1") {
				    if (typeof vm.kit.Comments === 'undefined') {
				        vm.kit.Comments = "Emailed guide";
				    }
				    else {
				        vm.kit.Comments = vm.kit.Comments + " / Emailed guide";
				    }

				}
				else {
				    if (typeof vm.kit.Comments === 'undefined') {
				        vm.kit.Comments = "Mailed guide";
				    }
				    
				    else {
				        vm.kit.Comments = vm.kit.Comments + " / Mailed guide";
				    }
				}

                //if org name NUll, fill field with customer name
				if (typeof vm.kit.Group === 'undefined') {
				    vm.kit.Group = vm.kit.FirstName;
				}

                //if referral code NUll, add default value
				if (typeof vm.kit.ReferralCode === 'undefined') {
				    vm.kit.ReferralCode = 'UNKNOWN';
				}

                //if Starting date NUll, add default value
				if (typeof vm.kit.StartRange === 'undefined') {
				    vm.kit.StartRange = 'I am not quite sure';
				}

				//vm.kit.ReferralUrl = localStorage.referral;
				vm.kit.ReferralUrl = referral.domain;
				
            //vm.kit.SelectedProducts = removeDuplicates([vm.productOptOneSelected.Code, vm.productOptTwoSelected.Code, vm.productOptThreeSelected.Code]);
            LeadsFactory.Lead.save(vm.kit).$promise.then(
               function (lead) {
                   $localStorage.Lead = lead;
                   vm.progress = 60;
                   NotificationFactory.Notification.save({ Type: 2, ExternalId: lead.Id, Email: lead.Email });

                   $timeout(function () {
                       vm.progress = 90;
                       $window.location.href = "/request-a-kit-confirmation";
                       vm.progress = 95;
                   }, 2500);
               }, function (error) {
                   vm.ActionState = 3;
                   vm.progress = 0;
                   $log.error(error);
               });
        };


        vm.step1 = 1;
        vm.step2 = 0;
        vm.step3 = 0;
        vm.step4 = 0;
        vm.step5 = 0;
        vm.step6 = 0;


        vm.BackToStep1 = function () {
            vm.step1 = 1
            vm.step2 = 0

            vm.ActionState = 0
        };

        vm.BackToStep2 = function () {
            vm.step2 = 1
            vm.step3 = 0
            vm.ActionState = 0
        };

        vm.BackToStep3 = function () {
            vm.step3 = 1
            vm.step4 = 0
            vm.ActionState = 0
        };

        vm.BackToStep4 = function () {
            vm.step4 = 1
            vm.step5 = 0
            vm.ActionState = 0
        };

       

        vm.SendNew = function () {

            vm.ActionState = 1;
            vm.progress = 20;
            var promises = [];
           
            //if Starting date NUll, add default value
            if (typeof vm.kit.StartRange === 'undefined') {
                vm.kit.StartRange = 'I am not quite sure';
            }

            //if org name NUll, fill field with customer name
            if (typeof vm.kit.Group === 'undefined') {
                vm.kit.Group = vm.kit.FirstName;
            }

            //if referral code NUll, add default value
            if (typeof vm.kit.ReferralCode === 'undefined') {
                vm.kit.ReferralCode = 'UNKNOWN';
            }

            //if Starting date NUll, add default value
            if (typeof vm.kit.StartRange === 'undefined') {
                vm.kit.StartRange = 'I am not quite sure';
            }

            //vm.kit.ReferralUrl = localStorage.referral;
            vm.kit.ReferralUrl = referral.domain;

            if (typeof vm.kit.AmountToRaise === 'undefined')
            {
                vm.kit.AmountToRaise = 1000;
            }

            //vm.kit.SelectedProducts = removeDuplicates([vm.productOptOneSelected.Code, vm.productOptTwoSelected.Code, vm.productOptThreeSelected.Code]);
            LeadsFactory.Lead.save(vm.kit).$promise.then(
                function (lead) {
                    //$localStorage.Lead = lead;
                    vm.kit = lead;
                    vm.progress = 60;
                    NotificationFactory.Notification.save({ Type: 2, ExternalId: lead.Id, Email: lead.Email });

                    $timeout(function () {
                        vm.progress = 90;
                        vm.step1 = 0;
                        vm.step2 = 1
                        vm.ActionState = 0;
                        //$('#kit-request-modal-New').modal('hide');
                    }, 2500);
                }, function (error) {
                    vm.ActionState = 3;
                    vm.progress = 0;
                    $log.error(error);
                });
        };


        vm.Step2 = function () {
            var promises = [];
            vm.ActionState = 1;

            var kittype = vm.kit.RType
            

            if (vm.kit.RType == "1") {
                if (typeof vm.kit.Comments === 'undefined') {
                    vm.kit.Comments = "Emailed guide";
                }
                else {
                    vm.kit.Comments = vm.kit.Comments + " / Emailed guide";
                }

            }
            else {
                if (typeof vm.kit.Comments === 'undefined') {
                    vm.kit.Comments = "Mailed guide";
                }

                else {
                    vm.kit.Comments = vm.kit.Comments + " / Mailed guide";
                }
            }




            LeadsFactory.Lead.update(vm.kit).$promise.then(
                function (lead) {
                    vm.kit = lead;

                    if (kittype == "1")
                    {
                        vm.step2 = 0;
                        vm.step4 = 1;
                    }
                    else
                    {
                        vm.step2 = 0;
                        vm.step3 = 1;
                    }
                    vm.ActionState = 0;
                    // $('#kit-request-modal-New').modal('hide');
                    vm.ActionState = 0;
                }, function (error) {
                    vm.ActionState = 3;
                    vm.progress = 0;
                    vm.error = ExceptionFactory.Handle(error.data);
                });
        };


        vm.Step3 = function () {

            vm.ActionState = 1;
            var promises = [];
            vm.kit.StCde = vm.kit.State;

            LeadsFactory.Lead.update(vm.kit).$promise.then(
                function (lead) {
                    vm.kit = lead;
                    vm.step3 = 0;
                    vm.step4 = 1;
                    vm.ActionState = 0;
                    // $('#kit-request-modal-New').modal('hide');
                    vm.ActionState = 0;
                }, function (error) {
                    vm.ActionState = 3;
                    vm.progress = 0;
                    vm.error = ExceptionFactory.Handle(error.data);
                });
        };


    }
    KitRequestFormController.$inject = ["$log", "$scope", "$timeout","SellingKitsLeadsFactory", "LeadsFactory", "$localStorage", "$window", "AddressHygieneFactory", "MasksConstants", "$rootScope", "NotificationFactory", "referral"];
    module.controller("KitRequestFormController", KitRequestFormController);
})();