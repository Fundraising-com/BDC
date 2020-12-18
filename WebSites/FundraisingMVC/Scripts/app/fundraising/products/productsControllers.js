(function () {
    "use strict";
    var module = angular.module("fundraising.products");

    function ProductsController(ProductsFactory, $window, $scope, $log, NotificationFactory) {
        var vm = this;
        vm.scope = $scope;
        vm.SuggestedProducts = [];
        vm.Products = [];
        vm.Price = 0;
        vm.SortBy = 0;
        vm.Profit = 0;
        vm.ProductTypes = [];
        vm.SubCategories = [];
        vm.Product = { Profits: [] };
        vm.Quantity = 0;
        vm.ActionState = 0;
        vm.Savearound = 0;
        vm.UpdateActionState = 0;
        vm.hideBrochure = 0;



        vm.ReDir1 = window.location.href;
        if (vm.ReDir1 === 'https://www.fundraising.com/products/ship-to-home/all-ship-to-home/') {
            $window.location.href = 'tel:18004435353';
        }
        else if (vm.ReDir1 === 'https://www.fundraising.com/products/ship-to-home/all-ship-to-home/') {
            $window.location.href = 'tel:18004435353';
        }
        else if (vm.ReDir1 === 'https://www.fundraising.com/products/ship-to-home/all-ship-to-home/efundraising') {
            $window.location.href = "https://www.efundraising.com";
        }




        vm.FeaturedProducts = ProductsFactory.Product.query({ isFeatured: true }, { isArray: true, cache: true });
        vm.Get = function (productId) {
            ProductsFactory.Product.get({ id: productId }, { isArray: false, cache: true }).$promise.then(
                function (data) {
                    vm.Product = data;
                    vm.Quantity = vm.Product.MinimumQuantity;
                    vm.Calculate();
                });
        };

        vm.min = function (arr) {
            return $filter('min')
                ($filter('map')(arr, 'Category'));
        };



        vm.GetSuggestedProducts = function (productId, countryId, maxResults) {
            vm.SuggestedProducts = ProductsFactory.Product.query({ currentProductId: productId, country: countryId, maxSuggestions: maxResults }, { isArray: true, cache: false });
        };
        vm.Calculate = function () {
            ProductsFactory.Product.UpdatePrice(vm.Product, vm.Quantity);
            //ProductsFactory.Product.UpdateShippingFee(vm.Product, vm.Quantity);
            ProductsFactory.Product.UpdateShippingFee(vm.Product, vm.Product.Price);
        };
        vm.GetFilteredProducts = function () {
            vm.ActionState = 1;
            var stringTypes = "";
            for (var i = 0; i < vm.ProductTypes.length; i++) {
                stringTypes += vm.ProductTypes[i] + ",";
            }
            ProductsFactory.Product.query({ price: vm.Price, profit: vm.Profit, productTypes: stringTypes }, { isArray: true, cache: true }).$promise.then(function (data) {
                vm.Products = data;
                //if (vm.Products[i].Category.Name == 857)
                //{
                //    vm.Savearound = 1;

                //}
                for (var i = 0; i < vm.Products.length; i++) {
                    var subCategoryFound = false;

                    for (var j = 0; j < vm.SubCategories.length; j++) {
                        subCategoryFound = subCategoryFound || vm.SubCategories[j] === vm.Products[i].Category.Name;
                        if (vm.Products[i].Category.ParentId === 857) {
                            vm.Savearound = 1;
                        }
                    }
                    if (!subCategoryFound) {
                        vm.SubCategories.push(vm.Products[i].Category.Name);
                    }
                }
                vm.ActionState = 0;
            });
        };
        vm.GetProductsByCategory = function (categoryId) {
            vm.Products = ProductsFactory.Product.query({ categoryId: categoryId }, { isArray: true, cache: true });
        };
        vm.CreateNewReview = function () {
            vm.NewReview = { Id: 0, Name: '', Email: '', Comments: '', ProductId: vm.Product.Id };
            vm.Product.Reviews.push(vm.NewReview);
        };
        vm.Update = function () {
            vm.UpdateActionState = 1;
            ProductsFactory.Product.update(vm.Product).$promise.then(
                function (data) {
                    var notification = { Type: 16, ExternalId: vm.Product.Id, Email: "fake@fake.com" };
                    NotificationFactory.Notification.save(notification);
                    vm.UpdateActionState = 2;
                },
                function (error) {
                    vm.UpdateActionState = 3;
                    $log.error(error);
                });
        };

        vm.OrderBrochure = function () {

            var notification = {
                Type: 22,
                Name: vm.OrderBrochure.Name,
                GroupName: vm.OrderBrochure.GroupName,
                Email: vm.OrderBrochure.Email,
                Phone: vm.OrderBrochure.Phone,
                Members: vm.OrderBrochure.Members,
                Address: vm.OrderBrochure.Address,
                City: vm.OrderBrochure.City,
                State: vm.OrderBrochure.State,
                Zipcode: vm.OrderBrochure.Zipcode,
                Product: vm.Product.Name

            };
            NotificationFactory.Notification.save(notification);

            $window.location.href = "/ordertaker-confirmation";



        };




    }
    ProductsController.$inject = ["ProductsFactory", "$window", "$scope", "$log", "NotificationFactory"];
    module.controller("ProductsController", ProductsController);

})();