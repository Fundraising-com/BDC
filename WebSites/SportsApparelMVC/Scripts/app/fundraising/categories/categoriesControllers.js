(function () {
    "use strict";
    var module = angular.module("fundraising.categories");
    function CategoriesController(CategoriesFactory, $scope, $localStorage, $rootScope, DetectCountryFactory) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.categories = vm.scope.$storage.Partner !== undefined ? CategoriesFactory.Category.query({ country: DetectCountryFactory.CountryId(), partnerId: vm.scope.$storage.Partner.Id }, { isArray: true, cache: true }) : [];
        vm.categoryPath = DetectCountryFactory.CountryPath();
        $rootScope.$on("partnerLoaded",
            function () {
                CategoriesFactory.Category
                    .query({ country: DetectCountryFactory.CountryId(), partnerId: vm.scope.$storage.Partner.Id },
                    { isArray: true, cache: true })
                    .$promise.then(function (data) {
                        vm.categories = data;
                    }, function (error) {
                        console.error(error);
                    });
            });
    }
    CategoriesController.$inject = ["CategoriesFactory", "$scope", "$localStorage", "$rootScope", "DetectCountryFactory"];
    module.controller("CategoriesController", CategoriesController);
})();