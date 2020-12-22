(function () {
   "use strict";
   var app = angular.module("lisa");
   app.directive('trim',
      function() {
         return {
            require: 'ngModel',
            link: function(scope, elm, attrs, ctrl) {
               ctrl.$validators.trim = function(modelValue, viewValue) {
                  if (ctrl.$isEmpty(modelValue)) {
                     // consider empty models to be valid
                     return true;
                  }

                  if (modelValue.indexOf(' ') === -1) {
                     return true;
                  }
                  // it is invalid
                  return false;
               };
            }
         };
      });
})();