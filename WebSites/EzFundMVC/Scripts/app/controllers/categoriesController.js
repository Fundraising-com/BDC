(function () {
    "use strict";
    var module = angular.module("ezfund.api");

    function CategoriesController($log, $window, $location, $scope, $rootScope, $timeout, CategoriesFactory, ProductsFactory, $localStorage, $sce, $document, $element, MetaService) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$document = $document;
        $rootScope.MetaService = MetaService;
        vm.executing = false;
        vm.categoryLoaded = false;

        var paths = $location.path().split('/');
        var rootCategory = paths[2];

        CategoriesFactory.Category.query({ url: rootCategory }, { cache: true })
            .$promise.then(
            function (results) {
                vm.executing = true;
                if (results === null || results === undefined || results.length <= 0) {
                    $window.location.href = "/products";
                }
                vm.subCategories = results;
                vm.parentCategory = results[0].Parent;
                $rootScope.MetaService.set(vm.parentCategory.METATitle, vm.parentCategory.METADescription, vm.parentCategory.METAKeywords);
            },
            function (error) {
                $log.error(error);
                $window.location.href = "/products";
            });
    }
    CategoriesController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "CategoriesFactory", "ProductsFactory", "$localStorage", "$sce", "$document", "$element", "MetaService"];
    module.controller("CategoriesController", CategoriesController);

    function SubCategoriesController($log, $window, $location, $scope, $rootScope, $timeout, CategoriesFactory, ProductsFactory, $localStorage, $sce, $document, $element, MetaService) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$document = $document;
        $rootScope.MetaService = MetaService;
        vm.executing = false;
        vm.categoryLoaded = false;

        var paths = $location.path().split('/');
        var rootCategory = paths[2];
        var url = paths[3];

    	CategoriesFactory.GetSubCategory.get({ rootCategory: rootCategory, url: url }, { isArray: false, cache: true })
            .$promise.then(
            function (result) {
                if (result === null || result === undefined) {
                    $window.location.href = "/products";
                }
                vm.subCategory = result;
                vm.categoryLoaded = true;
                $rootScope.MetaService.set(vm.subCategory.METATitle, vm.subCategory.METADescription, vm.subCategory.METAKeywords);
                //Load products
                GetRelatedProducts();
            },
            function (error) {
                $window.location.href = "/products";
            });
       
        function GetRelatedProducts() {
            ProductsFactory.RelatedProducts.query({ id: vm.subCategory.Id, maxResults: -1, isRandom: false }, { cache: true }).$promise.then(
                function (results) {
                    vm.executing = true;
                    vm.products = results;
                },
                    function (error) {
                        $log.error(error);
                    });
        }
    }
    SubCategoriesController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "CategoriesFactory", "ProductsFactory", "$localStorage", "$sce", "$document", "$element", "MetaService"];
    module.controller("SubCategoriesController", SubCategoriesController);

    function CategoryIndexController($log, $window, $location, $scope, $rootScope, $timeout, CategoriesFactory, ProductsFactory, $localStorage, $sce, $document, $element, MetaService) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$document = $document;
        $rootScope.MetaService = MetaService;
        vm.executing = false;
        vm.categoryLoaded = false;

        vm.categoryPath = $location.path();
        CategoriesFactory.GetAllCategories.query({}, { cache: true }).$promise.then(
                    function (results) {
                        vm.executing = true;
                        vm.categories = results;
                    },
                     function (error) {
                         $log.error(error);
                     });
    }
    CategoryIndexController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "CategoriesFactory", "ProductsFactory", "$localStorage", "$sce", "$document", "$element", "MetaService"];
    module.controller("CategoryIndexController", CategoryIndexController);
})();