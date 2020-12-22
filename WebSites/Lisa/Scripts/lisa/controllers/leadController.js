(function () {
   "use strict";
   var module = angular.module("lisa.leads");

   function LeadsController($log, $scope, LeadFactory, $localStorage) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.todayLeads = [];
      vm.aYearAgoLeads = [];
      vm.lead = {};
      vm.executing = false;
      vm.filter = "";
      vm.today = new Date();
      vm.yearAgo = new Date(new Date().setFullYear(new Date().getFullYear() - 1));
      vm.GetTodayLeads = function () {
         vm.executing = true;
         LeadFactory.Lead.query({start: vm.today, end : vm.today}, { isArray: true, cache: true })
            .$promise
            .then(function (result) {
               vm.todayLeads = result;
            })
            .catch(function (error) {
               console.error(error);
            })
            .finally(function () {
               vm.executing = false;
            });
      };
      vm.GetAYearAgoLeads = function () {
         vm.executing = true;
         LeadFactory.Lead.query({ start: vm.yearAgo, end: vm.yearAgo }, { isArray: true, cache: true })
            .$promise
            .then(function (result) {
               vm.aYearAgoLeads = result;
            })
            .catch(function (error) {
               console.error(error);
            })
            .finally(function () {
               vm.executing = false;
            });
      };
   }

   LeadsController
      .$inject = ["$log", "$scope", "LeadFactory", "$localStorage"];
   module.controller("LeadsController", LeadsController);

})();