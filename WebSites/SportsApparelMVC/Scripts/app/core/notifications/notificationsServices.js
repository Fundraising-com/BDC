(function () {
   "use strict";
   var module = angular.module("core.notifications");

   function NotificationFactory($log, $resource, hosts) {
      return {
         Notification: $resource(hosts.webApiCoreBaseUrl + "/notifications/:id")
      };
   }
   NotificationFactory.$inject = ["$log", "$resource", "hosts"];
   module.factory("NotificationFactory", NotificationFactory);


    
})();