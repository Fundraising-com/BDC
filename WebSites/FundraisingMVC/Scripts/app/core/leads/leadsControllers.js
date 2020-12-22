(function () {
   "use strict";
   var module = angular.module("core.leads");

   function KitRequestController($window, $scope, $log, $q, $timeout, $localStorage, $modal, $attrs, NotificationFactory, AddressHygieneFactory, LeadsFactory, MasksConstants, ExceptionFactory, $rootScope) {
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

      vm.kit = { Address: { Region: null, Country: vm.countries[0] }, RequestType: defaultKitForm, TellMore: 2, ChannelCode: 'INT' }; //Default Initialization. FORCING MERGE
      if (vm.scope.$storage.Partner !== null && vm.scope.$storage.Partner !== undefined) {
         vm.kit.PartnerId = vm.scope.$storage.Partner.Id;
      }
      if (vm.scope.$storage.Promotion !== null && vm.scope.$storage.Promotion !== undefined) {
         vm.kit.PromotionId = vm.scope.$storage.Promotion.Id;
      }
      if (vm.scope.$storage.Representative !== null && vm.scope.$storage.Representative !== undefined) {
         vm.kit.RepresentativeId = vm.scope.$storage.Representative.Id;
      }
      $rootScope.$on("partnerLoaded", function () {
         vm.kit.PartnerId = vm.scope.$storage.Partner.Id;
      });
      $rootScope.$on("promotionLoaded", function () {
         vm.kit.PromotionId = vm.scope.$storage.Promotion.Id;
      });
      $rootScope.$on("representativeLoaded", function () {
         vm.kit.RepresentativeId = vm.scope.$storage.Representative.Id;
      });

     

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
         //         $('#kit-request-modal').toggle(); //we close the modal for the address hygiene modal     
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
                        else {
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
           // },
            //function (error) {
            //   vm.ActionState = 3;
            //   vm.progress = 0;
            //   vm.error = ExceptionFactory.Handle(error);
            });
    };


      vm.step1 = 1;
      vm.step2 = 0;
      vm.step3 = 0;
      vm.step4 = 0;
      vm.step5 = 0;
      vm.step6 = 0;

      vm.BackToStep1 = function () {
          vm.step1 = 1;
          vm.step2 = 0;
          
          vm.ActionState = 0;         
      };

      vm.BackToStep2 = function () {
          vm.step2 = 1;
          vm.step3 = 0;
          vm.ActionState = 0;
      };

      vm.BackToStep3 = function () {
          vm.step3 = 1;
          vm.step4 = 0;
          vm.ActionState = 0;
      };

      vm.BackToStep4 = function () {
          vm.step4 = 1;
          vm.step5 = 0;
          vm.ActionState = 0;
      };

      vm.SkipInterest = function () {
          vm.step5 = 0;
          vm.step6 = 1;

      };

      vm.SkipAddress = function () {
          vm.step3 = 0;
          vm.step4 = 1;

      };

     
      vm.SendNew = function () {
        
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
          vm.kit.NumberOfMembers = 0;
          vm.kit.AmountToRaise = 0;
          $q.all(promises).then(
              function (data) {
                 // vm.kit.KitType = vm.kit.RequestType == "2" ? 42 : 43;
                 // vm.kit.Comments = vm.kit.RequestType == "1" ? "Emailed kit requested - no physical address needed" : "Mailed kit requested";
                  //if (vm.kit.Phone === "") {
                  //    vm.hidePhone = true;
                  //    vm.kit.Phone = "0000000000";
                  //    vm.kit.InitialPhoneNumberEntered = false;
                  //}
                  //else {
                  //    vm.kit.InitialPhoneNumberEntered = true;
                  //}
                  vm.kit.InitialPhoneNumberEntered = true;
                  LeadsFactory.Lead.save(vm.kit).$promise.then(
                      function (lead) {
                          //$localStorage.Lead = lead; //WHY ARE WE DOING THIS? MAYBE WE CAN DELETE THIS LINE? JAVI
                          vm.kit = lead;
                          vm.progress = 60;
                          var notification = { Type: 1, ExternalId: lead.Id, Email: lead.Email }
                          NotificationFactory.Notification.save(notification);
                          notification = { Type: lead.IsDuplicated ? 6 : lead.IsPotentiallyDuplicated ? 12 : 2, ExternalId: lead.Id, Email: "fake@fake.com" }
                          NotificationFactory.Notification.save(notification);
                          vm.progress = 80;
                          $timeout(function () {
                              vm.progress = 90;
                              vm.step1 = 0;
                              vm.step2 = 1;
                              vm.ActionState = 0;
                            
                          }, 2500);
                      }, function (error) {
                          vm.ActionState = 3;
                          vm.progress = 0;
                          vm.error = "Opps! There was a problem submitting form - Please call us 1.800.443.5353";
                      },
                         


                  );
              },
              function (error) {
                  vm.ActionState = 3;
                  vm.progress = 0;
                  vm.error = "Opps! There was a problem submitting form - Please call us 1.800.443.5353";
              });
      };

      vm.Step2 = function () {

          vm.ActionState = 1;
          var promises = [];
          vm.kit.Comments = "Raising Funds For: ";
          if (vm.kit.HasUniforms) {
              vm.kit.Comments += "Uniforms / "
          }
          if (vm.kit.HasEquipment) {
              vm.kit.Comments += "Equipment / "
          }
          if (vm.kit.HasSupplies) {
              vm.kit.Comments += "Supplies / "
          }
          if (vm.kit.HasPlayground) {
              vm.kit.Comments += "Playground / "
          }
          if (vm.kit.HasFees) {
              vm.kit.Comments += "Fees / "
          }
          if (vm.kit.HasUpkeep) {
              vm.kit.Comments += "Upkeep / "
          }
          if (vm.kit.HasTravel) {
              vm.kit.Comments += "Travel / "
          }
          if (vm.kit.HasRainydays) {
              vm.kit.Comments += "RainyDays / "
          }
          if (vm.kit.HasCollege) {
              vm.kit.Comments += "College / "
          }
          if (vm.kit.HasOther) {
              vm.kit.Comments += "Other / "
          }

          if (vm.kit.Raise != null) {

              if (vm.kit.Raise.match('$')) {
                  var raise = vm.kit.Raise.replace('$', '');
                  vm.kit.AmountToRaise = raise;
              }
              else
              {
                  vm.kit.AmountToRaise = vm.kit.Raise;
              }

          }
          vm.kit.Group = vm.kit.GroupName;

         
          LeadsFactory.Lead.update(vm.kit).$promise.then(
              function (lead) {
                  vm.kit = lead;
                  vm.step2 = 0;
                  vm.step3 = 1;
                  vm.ActionState = 0;
              }, function (error) {
                  vm.ActionState = 3;
                  vm.progress = 0;
                  vm.error = ExceptionFactory.Handle(error.data);
              });
 

      };

      

      vm.Step3 = function () {

          var promises = [];
          vm.ActionState = 1;
          
          //var addressFactoryPromise = AddressHygieneFactory.AddressHygiene.save(vm.kit.Address).$promise;
          //promises.push(addressFactoryPromise);
          //var deferred = $q.defer();
          //promises.push(deferred.promise);
          //addressFactoryPromise.then(
          //    function (suggestedAddress) {
          //        //vm.progress = 30;
          //        var modalScope = $scope.$new(true);
          //        angular.extend(modalScope, { header: 'Verify your Address', address: suggestedAddress, showEditFields: false, states: vm.states });
          //        //$('#kit-request-modal').toggle(); //we close the modal for the address hygiene modal     
          //        var modal = $modal.open({
          //            backdrop: false,
          //            scope: modalScope,
          //            templateUrl: "/Scripts/app/core/templates/addressHygieneModalTemplate.html",
          //            windowTemplateUrl: "/Scripts/app/core/templates/addressHygieneWindowTemplate.html"
          //        });
          //        modal.result.then(
          //            function (newAddress) {
          //                vm.progress = 50;
          //                vm.kit.Address = newAddress;

          //                LeadsFactory.Lead.update(vm.kit).$promise.then(
          //                    function (lead) {
          //                        vm.kit = lead;
          //                        vm.step3 = 0;
          //                        vm.step4 = 1;
          //                        vm.ActionState = 0;
          //                        deferred.resolve();
          //                    }, function (error) {
          //                        vm.ActionState = 3;
          //                        vm.progress = 0;
          //                        vm.error = ExceptionFactory.Handle(error.data);
          //                    });


          //            }, function (error) {
          //                deferred.reject();
          //            });
          //    }, function (error) {
          //        vm.ActionState = 3;
          //        vm.progress = 0;
          //        vm.error = ExceptionFactory.Handle(error.data);
          //    });


          vm.kit.Address.Region.Code = vm.kit.State;
          
          
          LeadsFactory.Lead.update(vm.kit).$promise.then(
              function (lead) {
                  vm.kit = lead;
                  vm.step3 = 0;
                  vm.step4 = 1;
                  vm.ActionState = 0;
              }, function (error) {
                  vm.ActionState = 3;
                  vm.progress = 0;
                  vm.error = ExceptionFactory.Handle(error.data);
              });

 
      };

      vm.Step4 = function () {
          vm.ActionState = 1;
          var promises = [];

          vm.kit.NumberOfMembers = vm.kit.Members;

          LeadsFactory.Lead.update(vm.kit).$promise.then(
              function (lead) {
                  vm.kit = lead;
                  vm.step4 = 0;
                  vm.step5 = 1;
                  vm.ActionState = 0;
              }, function (error) {
                  vm.ActionState = 3;
                  vm.progress = 0;
                  vm.error = ExceptionFactory.Handle(error.data);
              });

      };


      vm.Step5 = function () {

          var promises = [];
          vm.ActionState = 1;
          vm.kit.Interest = "Also Interested in : ";
          if (vm.kit.IntLollipops) {
              vm.kit.Interest += "Lollipops / "
          }
          if (vm.kit.IntCookies) {
              vm.kit.Interest += "Cookies / "
          }
          if (vm.kit.IntPretzels) {
              vm.kit.Interest += "Pretzels / "
          }
          if (vm.kit.IntSmencils) {
              vm.kit.Interest += "Smencils / "
          }
          if (vm.kit.IntDinner) {
              vm.kit.Interest += "Dinner & Movie / "
          }
          if (vm.kit.IntBeefJerky) {
              vm.kit.Interest += "BeefJerky / "
          }
          
          var selected = vm.kit.SelectedDate;
          vm.kit.Interest = vm.kit.Interest + " Starting Date - " + selected;

          LeadsFactory.Lead.update(vm.kit).$promise.then(
              function (lead) {
                  vm.kit = lead;
                  vm.ActionState = 0;
                  vm.step5 = 0;
                  vm.step6 = 1;
              }, function (error) {
                  vm.ActionState = 3;
                  vm.progress = 0;
                  vm.error = ExceptionFactory.Handle(error.data);
              });

      };


      vm.Step6 = function () {
                  $('#kit-request-modal-New').modal('hide');
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
      $rootScope.$on("partnerLoaded", function () {
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