(function () {
   "use strict";
   var module = angular.module("lisa.security");

   function LoginController($log, $scope, $localStorage, $timeout, $window, $location, AuthenticationFactory, $rootScope, ToastFactory) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.user = { Username: "", Password: "", UseRefreshTokens: true };
      vm.executing = false;
      vm.Login = function () {
         vm.executing = true;
         AuthenticationFactory.Login(vm.user)
            .then(
            function (response) {
               $timeout(function (response) {
                  var urlParameters = $location.search();
                  var redirectUrl = urlParameters.redirectUrl;
                  if (redirectUrl !== undefined) {
                     $window.location.href = redirectUrl;
                  } else {
                     $window.location.href = "/home/index";
                  }
               },
                  3500,
                  false);
            },
            function (error) {
               $log.error(error);
               ToastFactory.Error(error);
               vm.executing = false;
            });
      };
   }
   LoginController.$inject = ["$log", "$scope", "$localStorage", "$timeout", "$window", "$location", "AuthenticationFactory", "$rootScope", "ToastFactory"];
   module.controller("LoginController", LoginController);

   function TopMenuController(hosts, $mdDialog) {
      var vm = this;
      vm.hosts = hosts;
      vm.ShowSoonDialog = function() {
         $mdDialog.show(
            $mdDialog.alert()
            .clickOutsideToClose(true)
            .title('Section Under Construction')
            .textContent('This section will be available soon.')
            .ariaLabel('Section Under Construction')
            .ok('OK')
         );
      };
   }
   TopMenuController.$inject = ["hosts", "$mdDialog"];
   module.controller("TopMenuController", TopMenuController);
})();