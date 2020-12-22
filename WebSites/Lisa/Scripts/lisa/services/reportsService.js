(function () {
   "use strict";
   var module = angular.module("lisa.reports");

   function ReportsFactory($resource, hosts, $http) {
      return {
         Spider: function (parameters) {
            return $http.post(
               hosts.webApiReportsBaseUrl + "/spider/",
               parameters);
          },

          SalesToProcess: function (parameters) {
              return $http.post(
                  hosts.webApiReportsBaseUrl + "/salestoprocess/",
                  parameters);
          },

          ProductList: function (parameters) {
              return $http.post(
                  hosts.webApiReportsBaseUrl + "/ProductList/",
                  parameters);
          },

          CustomerList: function (parameters) {
              return $http.post(
                  hosts.webApiReportsBaseUrl + "/CustomerList/",
                  parameters);
          },


          TraditionalConfirmedSalesByProductClass: function (parameters) {
              return $http.post(
                  hosts.webApiReportsBaseUrl + "/TraditionalConfirmedSalesByProductClass/",
                  parameters);
          },



         RepeatedBusiness: $resource(hosts.webApiReportsBaseUrl + "/repeatedBusiness/"),
         GrossProfit: $resource(hosts.webApiReportsBaseUrl + "/grossprofit/")
      };
   }

   ReportsFactory.$inject = ["$resource", "hosts", "$http"];
   module.factory("ReportsFactory", ReportsFactory);

})();