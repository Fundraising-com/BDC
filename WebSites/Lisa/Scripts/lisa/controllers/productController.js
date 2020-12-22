(function () {
   "use strict";
   var module = angular.module("lisa.products");

   function ReviewsController($log, $scope, ProductFactory, $localStorage, $rootScope, $mdDialog, $document, $q, ToastFactory) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.reviews = [];
      vm.reviewsSelected = [];
      vm.showApprovedSwitch = false;
      vm.filter = "";
      vm.executing = false;      
      vm.GetReviews = function () {
         vm.executing = true;
         ProductFactory.Review.query({ }, { isArray: true, cache: true })
            .$promise
            .then(function (result) {
               vm.reviews = result;
            })
            .catch(function (error) {
               console.error(error);
            })
            .finally(function () {
               vm.executing = false;
            });
      };      
      vm.OpenReviewDialog = function(review) {
         $mdDialog.show({
               templateUrl: '/scripts/lisa/templates/FRCOM/products/review.html',
               parent: $document.find("#reviews"),
               locals: { review },
               controller: function DialogController($scope, $mdDialog) {
                  var ivm = this;
                  ivm.scope = $scope;
                  ivm.cancel = function() {
                     $mdDialog.hide(null);
                  };
                  ivm.save = function() {
                     $mdDialog.hide(review);
                  };
               },
               controllerAs: 'ctrl',
               bindToController: true,
               clickOutsideToClose: true,
               fullscreen: true
            })
            .then(
               function(review) {
                  if (review !== undefined && review !== null) {
                     ProductFactory.Review.update(review,
                        function() {
                           ToastFactory.Success("Values saved correctly!");
                        },
                        function(error) {
                           $log.error(error);
                           ToastFactory.Error(error);
                        });
                  }
               });
      };
      vm.DeleteReviews = function () {
         var dialog = $mdDialog.confirm()
          .title('Delete Reviews')
          .textContent('Do you want to delete the selected(s) reviews?')
          .ok('Yes')
          .cancel('No');

         $mdDialog.show(dialog).then(function () {
            var promises = [];
            for (var i = 0; i < vm.reviewsSelected.length; i++) {
               var promise = ProductFactory.Review.delete({ id: vm.reviewsSelected[i].Id }).$promise;
               promises.push(promise);
               vm.reviews.splice(vm.reviews.indexOf(vm.reviewsSelected[i]), 1);
            }
            vm.reviewsSelected = [];
            $q.all(promises).then(
               function () {
                  ToastFactory.Success("Values deleted correctly!");                  
               },
               function (error) {
                  $log.error(error);
                  ToastFactory.Error(error);
               });
         }, function () { });
      };
      vm.ReviewCheckboxChanged = function (review) {
         if (review.Selected) {
            vm.reviewsSelected.push(review);
         } else {
            vm.reviewsSelected.splice(vm.reviewsSelected.indexOf(review), 1);
         }

      };
   }

   ReviewsController
      .$inject = ["$log", "$scope", "ProductFactory", "$localStorage", "$rootScope", "$mdDialog", "$document", "$q", "ToastFactory"];
   module.controller("ReviewsController", ReviewsController);

})();