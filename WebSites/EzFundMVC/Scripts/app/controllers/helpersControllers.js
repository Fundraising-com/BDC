(function () {
    "use strict";
    var module = angular.module("ezfund.helpers");

    function SearchController($window) {
        var vm = this;
        vm.q = "";

        vm.Search = function () {
            $window.location.href = "/search?q=" + vm.q;
        }
    }
    SearchController.$inject = ["$window"];
    module.controller("SearchController", SearchController);


    function DetectCountryController($window, $scope, $localStorage, $rootScope, DetectCountryFactory) {
        var vm = this;
        vm.scope = $scope;
        vm.country = DetectCountryFactory.CountryId();
    }

    DetectCountryController.$inject = ["$window", "$scope", "$localStorage", "$rootScope", "DetectCountryFactory"];
    module.controller("DetectCountryController", DetectCountryController);


    //function DetectBrowserReferralController($window, $scope, $localStorage, $rootScope, DetectReferralFactory) {
    //    var vm = this;
    //    vm.scope = $scope;
    //    vm.refer = DetectReferralFactory.Referral();
    //    if (localStorage.referral === null) {
    //        localStorage.referral = vm.refer;
    //    } 
    //}

    //DetectBrowserReferralController.$inject = ["$window", "$scope", "$localStorage", "$rootScope", "DetectReferralFactory"];
    //module.controller("DetectBrowserReferralController", DetectBrowserReferralController);


    function GoogleAnalyticsController(GoogleAnalyticsService) {
        var vm = this;

        vm.ProductClicked = function (id, name, category, brand, position, list) {
            GoogleAnalyticsService.ProductClicked(id, name, category, brand, position, list);
        };

        vm.ProductShowed = function (id, name, category, brand, position, list) {
            GoogleAnalyticsService.ProductShowed(id, name, category, brand, position, list);
        };

        vm.BannerClicked = function (id) {
            GoogleAnalyticsService.BannerClicked(id);
        };

        vm.KitSubmitClicked = function () {
            GoogleAnalyticsService.KitSubmitClicked();
        };

        vm.PopUpKitClicked = function () {
            GoogleAnalyticsService.PopUpKitClicked();
        };

        vm.ProductDetailed = function (id, name, category, brand, price) {
            GoogleAnalyticsService.ProductDetailed(id, name, category, brand, price);
        };
        vm.ProductAdded = function (product) {
            var quantity = 1;
            var name = '';
            var price = product.CalculatedPrice;
            if (!product.IsStackedProduct) {
                for (var i = 0; i < product.SubProducts.length; i++) {
                    if (product.SubProducts[i].SelectedQuantity !== undefined && product.SubProducts[i].SelectedQuantity > 0) {
                        quantity = product.SubProducts[i].SelectedQuantity;
                        name = product.SubProducts[i].Name;
                        price = product.SubProducts[i].Price;
                    }
                }
            }
            GoogleAnalyticsService.ProductAdded(product.Id, name, product.Category.Name, product.Name, price, quantity);
        };
        vm.ProductRemoved = function (id, name, category, brand, price, quantity) {
            GoogleAnalyticsService.ProductRemoved(id, name, category, brand, price, quantity);
        };
    }
    GoogleAnalyticsController.$inject = ["GoogleAnalyticsService"];
    module.controller("GoogleAnalyticsController", GoogleAnalyticsController);
})();