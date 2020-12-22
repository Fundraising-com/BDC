(function () {
   "use strict";
   var module = angular.module("lisa.helpers");

   function ToastFactory($mdToast) {
      return {
         Error: function (error) {
            var message = error.error_description !== undefined ? error.error_description : "Oops! Something went wrong.";
            var toast = $mdToast.simple().textContent(message).capsule(true).action("close").toastClass("toast-error");
            toast._options.hideDelay = false;
            toast._options.position = "bottom right";
            $mdToast.show(toast);
         },
         Success: function(message) {
            var toast = $mdToast.simple().textContent(message).capsule(true).action("close").toastClass("toast-success");
            toast._options.position = "bottom right";
            $mdToast.show(toast);
         }
      };
   }
   ToastFactory.$inject = ["$mdToast"];
   module.factory("ToastFactory", ToastFactory);

})();