(function () {
   "use strict";
   var module = angular.module("core.leads");

   function KitRequestController($window, $scope, $log, $q, $timeout, $localStorage, $modal, $attrs, NotificationFactory, AddressHygieneFactory, LeadsFactory, MasksConstants, ExceptionFactory, $rootScope) {
       ////
       //$scope.formParams = {};
       //$scope.stage = "";
       //$scope.formValidation = false;
       //$scope.toggleJSONView = false;
       //$scope.toggleFormErrorsView = false;

       //$scope.formParams = {
       //    ccEmail: '',
       //    ccEmailList: []
       //};

       //// Navigation functions
       //$scope.next = function (stage) {
       //    //$scope.direction = 1;
       //    //$scope.stage = stage;

       //    $scope.formValidation = true;

       //    if ($scope.kitRequestForm.$valid) {
       //        $scope.direction = 1;
       //        $scope.stage = stage;
       //        $scope.formValidation = false;
       //    }
       //};

       //$scope.back = function (stage) {
       //    $scope.direction = 0;
       //    $scope.stage = stage;
       //};

       //// CC email list functions
       //$scope.addCCEmail = function () {
       //    $scope.rowId++;

       //    var email = {
       //        email: $scope.formParams.ccEmail,
       //        row_id: $scope.rowId
       //    };

       //    $scope.formParams.ccEmailList.push(email);

       //    $scope.formParams.ccEmail = '';
       //};

       //$scope.removeCCEmail = function (row_id) {
       //    for (var i = 0; i < $scope.formParams.ccEmailList.length; i++) {
       //        if ($scope.formParams.ccEmailList[i].row_id === row_id) {
       //            $scope.formParams.ccEmailList.splice(i, 1);
       //            break;
       //        }
       //    }
       //};


       //// Post to desired exposed web service.
       //$scope.submitForm = function () {
       //    var wsUrl = "someURL";

       //    // Check form validity and submit data using $http
       //    if ($scope.kitRequestForm.$valid) {
       //        $scope.formValidation = false;

       //        $http({
       //            method: 'POST',
       //            url: wsUrl,
       //            data: JSON.stringify($scope.formParams)
       //        }).then(function successCallback(response) {
       //            if (response
       //                && response.data
       //                && response.data.status
       //                && response.data.status === 'success') {
       //                $scope.stage = "success";
       //            } else {
       //                if (response
       //                    && response.data
       //                    && response.data.status
       //                    && response.data.status === 'error') {
       //                    $scope.stage = "error";
       //                }
       //            }
       //        }, function errorCallback(response) {
       //            $scope.stage = "error";
       //            console.log(response);
       //        });
       //    }
       //};

       //$scope.reset = function () {
       //    // Clean up scope before destorying
       //    $scope.formParams = {};
       //    $scope.stage = "";
       //}



       ////






       var vm = this;
      var defaultKitForm = 2;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.countries = AddressHygieneFactory.Countries;
      vm.hidePhone = false;
     
      //set first province to alberta
      var firststate = 0;

      vm.KitRequestLanding = 0;
      vm.states = AddressHygieneFactory.States.Get(vm.countries[0]);
      vm.ActionState = 0;
      vm.progress = 0;
      vm.phoneMask = MasksConstants.Phone;

      

      vm.Send = function () {
         var promises = [];
         vm.ActionState = 1;
         vm.progress = 10;

         // Divide Name into First Name and Last Name
         var names = vm.kit.FirstName.split(" ");
         if (names.length === 1) {
            vm.kit.LastName = "-";
         } else {
            var firstName = "";
            for (var i = 0; i < names.length - 1; i++) {
               firstName += names[i] + " ";
            }
            vm.kit.FirstName = firstName;
            vm.kit.LastName = names[names.length - 1];
         }
         vm.progress = 20;

         if (vm.kit.RequestType == "2") {
            var addressFactoryPromise = AddressHygieneFactory.AddressHygiene.save(vm.kit.Address).$promise;
            promises.push(addressFactoryPromise);
            var deferred = $q.defer();
            promises.push(deferred.promise);
            addressFactoryPromise.then(
               function (suggestedAddress) {
                  vm.progress = 30;
                  var modalScope = $scope.$new(true);
                  angular.extend(modalScope, { header: 'Verify your Address', address: suggestedAddress, showEditFields: false, states: vm.states });
                  $('#kit-request-modal').toggle(); //we close the modal for the address hygiene modal     
                  var modal = $modal.open({
                     backdrop: false,
                     scope: modalScope,
                     templateUrl: "/Scripts/app/core/templates/addressHygieneModalTemplate.html",
                     windowTemplateUrl: "/Scripts/app/core/templates/addressHygieneWindowTemplate.html"
                  });
                  modal.result.then(
                     function (newAddress) {
                        vm.progress = 50;
                        vm.kit.Address = newAddress;
                        deferred.resolve();
                     }, function (error) {
                        deferred.reject();
                     });
               }, function (error) {
                  vm.ActionState = 3;
                  vm.progress = 0;
                  vm.error = ExceptionFactory.Handle(error.data);
               });
         }
         $q.all(promises).then(
            function (data) {
               vm.kit.KitType = vm.kit.RequestType == "2" ? 42 : 43;
               vm.kit.Comments = vm.kit.RequestType == "1" ? "Emailed kit requested - no physical address needed" : "Mailed kit requested";
               if (vm.kit.Phone === "") {
                   vm.hidePhone = true;
                   vm.kit.Phone = "0000000000";
                   vm.kit.InitialPhoneNumberEntered = false;
               }
               else {
                   vm.kit.InitialPhoneNumberEntered = true;
               }
               LeadsFactory.Lead.save(vm.kit).$promise.then(
                  function (lead) {
                     $localStorage.Lead = lead;
                     vm.progress = 60;
                     var notification = { Type: 1, ExternalId: lead.Id, Email: lead.Email }
                     NotificationFactory.Notification.save(notification);
                     notification = { Type: lead.IsDuplicated ? 6 : lead.IsPotentiallyDuplicated ? 12 : 2, ExternalId: lead.Id, Email: "fake@fake.com" }
                     NotificationFactory.Notification.save(notification);
                     vm.progress = 80;
                     $timeout(function () {
                        vm.progress = 90;
                        if (vm.kit.Address.Country.Code == 'US') {
                            if (vm.KitRequestLanding == 1) {
                                $window.location.href = "/kit-request-confirmation-pop-up?c=" + vm.kit.Address.Country.Code + "&partnerId=" + vm.scope.$storage.Partner.Id;
                            }
                            else if (vm.KitRequestLanding == 2) {
                               $window.location.href = "/kit-request-confirmation?c=" + vm.kit.Address.Country.Code + "&partnerId=" + vm.scope.$storage.Partner.Id;
                            }
                        }
                        else
                        {
                            if (vm.KitRequestLanding == 1) {
                               $window.location.href = "/kit-request-confirmation-pop-up-canada?c=" + vm.kit.Address.Country.Code + "&partnerId=" + vm.scope.$storage.Partner.Id;
                            }
                            else if (vm.KitRequestLanding == 2) {
                               $window.location.href = "/kit-request-confirmation-canada?c=" + vm.kit.Address.Country.Code + "&partnerId=" + vm.scope.$storage.Partner.Id;
                            }
                        }
                        vm.progress = 95;
                     }, 2500);
                  }, function (error) {
                     vm.ActionState = 3;
                     vm.progress = 0;
                     vm.error = ExceptionFactory.Handle(error.data);
                  });
            },
            function (error) {
               vm.ActionState = 3;
               vm.progress = 0;
               vm.error = ExceptionFactory.Handle(error);
            });
      };

      vm.UpdateStates = function () {
         vm.states = AddressHygieneFactory.States.Get(vm.kit.Address.Country);
         vm.kit.Address.Region = vm.states[0];
      };
   }
   KitRequestController.$inject = ["$window", "$scope", "$log", "$q", "$timeout", "$localStorage", "$modal", "$attrs", "NotificationFactory", "AddressHygieneFactory", "LeadsFactory", "MasksConstants", "ExceptionFactory", "$rootScope"];
   module.controller("KitRequestController", KitRequestController);

   function NewsletterSubscriptionController($window, $scope, $log, $q, $timeout, $localStorage, NotificationFactory, ExceptionFactory, NewsletterSubscriptionFactory, $rootScope) {

      /**
        * Creates a new newsletter entry with the newsletter object received
        *
        * */
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.ActionState = 0;
      if (vm.scope.$storage.Partner !== null && vm.scope.$storage.Partner !== undefined) {
         vm.newsletterSubscription = { Email: "", PartnerId: vm.scope.$storage.Partner.Id, CultureCode: "en-US", Referrer: "NULL", Fullname: "-" };
      }
      $rootScope.$on("partnerLoaded", function() {
         vm.newsletterSubscription = { Email: "", PartnerId: vm.scope.$storage.Partner.Id, CultureCode: "en-US", Referrer: "NULL", Fullname: "-" };
      });
      vm.Send = function () {
         vm.ActionState = 1;
         NewsletterSubscriptionFactory.NewsletterSubscription.save(vm.newsletterSubscription).$promise.then(
             function (response) {
                $log.info(response);
                vm.ActionState = 2;
                $scope.newsletterSubscriptionCtrl.newsletterSubscription.Email = "Thank You";
             },
             function (error) {
                // $scope.handleError
                $log.error(error);

                //(error);
                vm.ActionState = 3;
             });
      }
   };

   NewsletterSubscriptionController.$inject = ["$window", "$scope", "$log", "$q", "$timeout", "$localStorage", "NotificationFactory", "ExceptionFactory", "NewsletterSubscriptionFactory", "$rootScope"];
   module.controller("NewsletterSubscriptionController", NewsletterSubscriptionController);

})();