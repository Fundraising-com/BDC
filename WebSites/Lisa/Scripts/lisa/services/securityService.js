(function () {
   "use strict";
   var module = angular.module("lisa.security");

   function AuthenticationFactory($resource, $http, $localStorage, $q, $cookies, hosts) {
      var serviceBase = hosts.webApiSecurityBaseUrl;
      var result = {
         Login: function(loginData) {
            var data = "grant_type=password&username=" + loginData.Username + "&password=" + loginData.Password;
            if (loginData.UseRefreshTokens) {
               data = data + "&client_id=" + hosts.clientId;
            }
            var deferred = $q.defer();
            $http.post(serviceBase + '/token',
                  data,
                  { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
               .success(function(response) {
                  $cookies.put("lisa.auth",
                     "access_token=" + response.access_token + "&refresh_token=" + response.refresh_token, { expires: new Date(response[".expires"]) });
                  deferred.resolve(response);
               })
               .error(function(err, status) {
                  deferred.reject(err);
               });
            return deferred.promise;
         },
         LogOut: function () {
            $cookies.remove("lisa.auth");
         }
      };
      return result;
   };
   AuthenticationFactory.$inject = ["$resource", "$http", "$localStorage", "$q", "$cookies", "hosts"];
   module.factory("AuthenticationFactory", AuthenticationFactory);

   function AuthenticationInterceptionFactory($injector, $location, $localStorage, $q, $cookies, $document) {
      return {
         request: function (config) {
             config.headers = config.headers || {};
             var authCookie = $cookies.get("lisa.auth");
             if (authCookie) {
                 var cookieValues = authCookie.split("&");
                 var authToken = cookieValues[0].split("=")[1];
                 var refreshToken = cookieValues[1].split("=")[1];
                 config.headers.Authorization = 'Bearer ' + authToken; // <== this token!
             }
            return config;
         },
         responseError: function (rejection) {
            if (rejection.status === 401) {
               var authService = $injector.get('AuthenticationFactory');
               authService.LogOut();
               $location.path('/login');
            }
            return $q.reject(rejection);
         }
      };
   }
   AuthenticationInterceptionFactory.$inject = ["$injector", "$location", "$localStorage", "$q", "$cookies", "$document"];
   module.factory("AuthenticationInterceptionFactory", AuthenticationInterceptionFactory);
})();