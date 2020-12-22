(function() {
   "use strict";
   var module = angular.module("lisa.sales");

   function PromotionCodesController($log, $scope, SalesFactory, $localStorage, $rootScope, $mdDialog, $document, $q, AddressFactory, ProductFactory, PartnerFactory, ToastFactory) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.promotionCodes = [];
      vm.promotionCode = {};
      vm.scopeTypes = [{ Id: 1, Name: 'Shopping Cart' }, { Id: 2, Name: 'Products' }];
      vm.limitTypes = [{ Id: 1, Name: 'Unlimited' }, { Id: 2, Name: 'Date' }, { Id: 3, Name: 'Quantity' }];
      vm.discountTypes = [{ Id: 1, Name: 'Amount Discount' }, { Id: 2, Name: 'Percentage Discount' }, { Id: 3, Name: 'Free Shipping' }];
      vm.minimumRequirementTypes = [{ Id: 1, Name: 'No Minimum Required' }, { Id: 2, Name: 'Minimum Quantity' }, { Id: 3, Name: 'Minimum Amount' }];
      vm.partnerTypes = [{ Id: 1, Name: 'All Partners' }, { Id: 2, Name: 'Single Partner' }];
      vm.executing = false;
      vm.filter = "";
      vm.promotionCodesSelected = [];      
      vm.GetPromotionCodes = function() {
         vm.executing = true;
         SalesFactory.PromotionCode.query({}, { isArray: true, cache: true })
            .$promise
            .then(function(result) {
               vm.promotionCodes = result;
            })
            .catch(function(error) {
               console.error(error);
            })
            .finally(function() {
               vm.executing = false;
            });
      };
      vm.PromotionCodeCheckboxChanged = function (object) {
         if (object.Selected) {
            vm.promotionCodesSelected.push(object);
         } else {
            vm.promotionCodesSelected.splice(vm.promotionCodesSelected.indexOf(object), 1);
         }
         
      };
      vm.DisablePromotionCodes = function() {
         var dialog = $mdDialog.confirm()
          .title('Disable Promotion Codes')
          .textContent('Do you want to disable the selected(s) promotion codes?')
          .ok('Yes')
          .cancel('No');

         $mdDialog.show(dialog).then(function () {
            var promises = [];
            for (var i = 0; i < vm.promotionCodesSelected.length; i++) {
               vm.promotionCodesSelected[i].Selected = false;
               vm.promotionCodesSelected[i].IsEnabled = false;
               var promise = SalesFactory.PromotionCode.update(vm.promotionCodesSelected[i]).$promise;
               promises.push(promise);
               
            }
            vm.promotionCodesSelected = [];
            $q.all(promises).then(
               function () {
                  ToastFactory.Success("Values disabled correctly!");
               },
               function (error) {
                  $log.error(error);
                  ToastFactory.Error(error);
            });
         }, function () {});
      };
      vm.OpenPromotionCodeDialog = function (object) {
         if (object === null) {
            vm.promotionCode = { Id: 0, Country: {Code: 'US', Name: 'United States'}, DateLimit: new Date() };
         } else {
            vm.promotionCode = object;
            if (vm.promotionCode.DateLimit !== undefined && vm.promotionCode.DateLimit !== null) {
               vm.promotionCode.DateLimit = new Date(vm.promotionCode.DateLimit);
            }
         }
         $mdDialog.show({
               templateUrl: '/scripts/lisa/templates/FRCOM/sales/promotionCode.html',
               parent: $document.find("#promotionCodes"),
               locals: { promotionCode: vm.promotionCode, scopeTypes: vm.scopeTypes, countries: vm.countries, limitTypes: vm.limitTypes, discountTypes: vm.discountTypes, minimumRequirementTypes: vm.minimumRequirementTypes, partnerTypes: vm.partnerTypes },
               controller: function DialogController($scope, $mdDialog, AddressFactory, ProductFactory, PartnerFactory) {
                  var ivm = this;
                  ivm.scope = $scope;
                  ivm.countries = AddressFactory.Country.query({}, { isArray: true, cache: true });
                  ivm.products = [];
                  if (ivm.promotionCode.ScopeType === 2) {
                     ivm.products = ProductFactory.Product.query({ country: ivm.promotionCode.Country.Code === "CA" ? 1 : 2 }, { isArray: true, cache: true });
                  }
                  ivm.partners = PartnerFactory.Partner.query({}, { isArray: true, cache: true });
                  ivm.cancel = function() {
                     vm.promotionCode = null;
                     $mdDialog.hide(null);
                  };
                  ivm.save = function() {
                     $mdDialog.hide(vm.promotionCode);
                  };
                  ivm.GetProducts = function () {
                     var countryId = ivm.promotionCode.Country.Code === "CA" ? 1 : 2;
                     if (ivm.promotionCode.ScopeType === 2) {
                        ivm.products = ProductFactory.Product.query({ country: countryId }, { isArray: true, cache: true });
                     }
                  };
                  ivm.DiscountTypeChanged = function() {
                     if (ivm.promotionCode.DiscountType === 3) {
                        ivm.promotionCode.ScopeType = 2;
                        ivm.products = ProductFactory.Product.query({ country: ivm.promotionCode.Country.Code === "CA" ? 1 : 2 }, { isArray: true, cache: true });
                     }
                  };
               },
               controllerAs: 'ctrl',
               bindToController: true,
               clickOutsideToClose: true,
               fullscreen: true
            })
            .then(
               function(result) {
                  if (result !== undefined && result !== null) {
                     if (result.Id > 0) {
                        SalesFactory.PromotionCode.update(result,
                              function() {
                                 ToastFactory.Success("Values saved correctly!");
                              },
                              function(error) {
                                 $log.error(error);
                                 ToastFactory.Error(error);
                              });
                     } else {
                        SalesFactory.PromotionCode.save(result,
                           function(resultCreated) {
                              vm.promotionCodes.push(resultCreated);
                              ToastFactory.Error(error);
                           },
                           function(error) {
                              $log.error(error);
                              ToastFactory.Error(error);
                           }
                        );
                     }
                  }
               },
               function() {
                  vm.promotionCode = null;
               });
      };
   }

   PromotionCodesController
      .$inject = ["$log", "$scope", "SalesFactory", "$localStorage", "$rootScope", "$mdDialog", "$document", "$q", "AddressFactory", "ProductFactory", "PartnerFactory", "ToastFactory"];
   module.controller("PromotionCodesController", PromotionCodesController);

})();