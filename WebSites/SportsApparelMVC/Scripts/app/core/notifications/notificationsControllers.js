(function () {
   "use strict";
   var module = angular.module("core.notifications");

   function GetStartedController($window, $scope, $log, $q, $timeout, $localStorage, $modal, $attrs, NotificationFactory, ExceptionFactory, $rootScope, FileUpLoadFactory) {
      //not being used at the moment

}

   GetStartedController.$inject = ["$window", "$scope", "$log", "$q", "$timeout", "$localStorage", "$modal", "$attrs", "NotificationFactory", "ExceptionFactory", "$rootScope", "FileUpLoadFactory"];
   module.controller("GetStartedController", GetStartedController);

  

})();