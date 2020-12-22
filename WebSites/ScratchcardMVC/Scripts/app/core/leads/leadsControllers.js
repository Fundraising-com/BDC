(function () {
   "use strict";
   var module = angular.module("core.leads");

   function KitRequestController($window, $scope, $log, $q, $timeout, $localStorage, $modal, $attrs, NotificationFactory, AddressHygieneFactory, LeadsFactory, MasksConstants, ExceptionFactory) {
      var vm = this;
      vm.scope = $scope;
      vm.countries = AddressHygieneFactory.Countries;
      vm.hidePhone = false;
      //set first province to alberta
      var firststate = 0;


      vm.states = AddressHygieneFactory.States.Get(vm.countries[0]);
      vm.kit = { Address: { Region: vm.states[firststate], Country: vm.countries[0] }, RequestType: 2, TellMore: 2, PartnerId: 500, PromotionId: 2469, RepresentativeId: 0, ChannelCode: 'INT' }; //Default Initialization. FORCING MERGE
      vm.ActionState = 0;
      vm.progress = 0;
      vm.phoneMask = MasksConstants.Phone;

      vm.GetGeolocation = function() {
         AddressHygieneFactory.FreeGeoIP.get().$promise.then(
            function (result) {
               if (result.country_code === "CA" || result.country_code === "US") {
                  vm.kit.Address.Country = { Code: result.country_code, Name: result.country_name };
                  vm.UpdateStates();
                  vm.kit.Address.Region = { Code: result.region_code, Name: result.region_name };
               } else {
                  $log.warn(result);
               }
            }
            ,function(error) {
               $log.warn(error);
            }
         );
      };

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

         //if (vm.kit.RequestType == "2") {
         //   var addressFactoryPromise = AddressHygieneFactory.AddressHygiene.save(vm.kit.Address).$promise;
         //   promises.push(addressFactoryPromise);
         //   var deferred = $q.defer();
         //   promises.push(deferred.promise);
         //   addressFactoryPromise.then(
         //      function (suggestedAddress) {
         //         vm.progress = 30;
         //         var modalScope = $scope.$new(true);
         //         angular.extend(modalScope, { header: 'Verify your Address', address: suggestedAddress, showEditFields: false, states: vm.states });
         //         $('#kit-request-modal').modal('hide'); //we close the modal for the address hygiene modal     
         //         var modal = $modal.open({
         //            backdrop: false,
         //            scope: modalScope,
         //            templateUrl: "/Scripts/app/core/templates/addressHygieneModalTemplate.html",
         //            windowTemplateUrl: "/Scripts/app/core/templates/addressHygieneWindowTemplate.html"
         //         });
         //         modal.result.then(
         //            function (newAddress) {
         //               vm.progress = 50;
         //               vm.kit.Address = newAddress;
         //               deferred.resolve();
         //            }, function (error) {
         //               deferred.reject();
         //            });
         //      }, function (error) {
         //         vm.ActionState = 3;
         //         vm.progress = 0;
         //         vm.error = ExceptionFactory.Handle(error.data);
         //      });
         //}
         $q.all(promises).then(
            function (data) {
               vm.kit.KitType = vm.kit.RequestType == "2" ? 42 : 43;
               vm.kit.Comments = vm.kit.RequestType == "1" ? "Emailed kit requested - no physical address needed" : "Mailed kit requested";
               if (vm.kit.Phone === "") {
                  vm.hidePhone = true;
                  vm.kit.Phone = "0000000000";
               }
               LeadsFactory.Lead.save(vm.kit).$promise.then(
                  function (lead) {
                     vm.progress = 60;
                     var notification = { Type: 1, ExternalId: lead.Id, Email: lead.Email }
                     NotificationFactory.Notification.save(notification);
                     notification = { Type: lead.IsDuplicated ? 6 : lead.IsPotentiallyDuplicated ? 12 : 2, ExternalId: lead.Id, Email: "fake@fake.com" }
                     NotificationFactory.Notification.save(notification);
                     vm.progress = 80;
                     $timeout(function () {
                        vm.progress = 90;
                        $localStorage.Lead = lead;
                        //$window.location.href = "/kit-request-confirmation?c=" + vm.kit.Address.Country.Code;
                        //$window.location.href = "/";
                        ga('send', 'event', 'Kitsubmit', 'Click', 'Kitsubmit');
                        vm.progress = 100;
                        vm.ActionState = 4;
                     }, 1000 * 2);
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
   KitRequestController.$inject = ["$window", "$scope", "$log", "$q", "$timeout", "$localStorage", "$modal", "$attrs", "NotificationFactory", "AddressHygieneFactory", "LeadsFactory", "MasksConstants", "ExceptionFactory"];
   module.controller("KitRequestController", KitRequestController);

   function NewsletterSubscriptionController($window, $scope, $log, $q, $timeout, $localStorage, NotificationFactory, ExceptionFactory, NewsletterSubscriptionFactory) {

      /**
        * Creates a new newsletter entry with the newsletter object received
        *
        **/
      var vm = this;
      vm.scope = $scope;
      vm.newsletterSubscription = { Email: "", PartnerId: 500, CultureCode: "en-US", Referrer: "NULL", Fullname: "-" };
      vm.ActionState = 0;
      vm.Send = function () {
         vm.ActionState = 1;



         //NewsletterSubscriptionService.Post(vm.newsletterSubscription)
         NewsletterSubscriptionFactory.NewsletterSubscription.save(vm.newsletterSubscription).$promise.then(
             function (response) {
                $log.info(response);
                vm.ActionState = 2;
                ga('send', 'event', 'newslettersubmit', 'Click', 'newslettersubmit');
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

   NewsletterSubscriptionController.$inject = ["$window", "$scope", "$log", "$q", "$timeout", "$localStorage", "NotificationFactory", "ExceptionFactory", "NewsletterSubscriptionFactory"];
   module.controller("NewsletterSubscriptionController", NewsletterSubscriptionController);

})();