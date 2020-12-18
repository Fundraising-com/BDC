(function () {
    "use strict";
    var module = angular.module("ezfund.api");
    
    function ProductsController($log, $window, $location, $scope, $rootScope, $timeout, ProductsFactory, $localStorage, $sce, $uibModal, $document, $element, MetaService, referral) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$uibModal = $uibModal;
        vm.scope.$document = $document;
        vm.executing = false;
        vm.productLoaded = false;
        $rootScope.MetaService = MetaService;
        vm.animationsEnabled = true;        
        vm.DisableAddToCardButton = false; // Enableds/Disables the Add to Cart Button depending on the IsStackProduct and if its divisiable by the Product Divisor
        var paths = $location.path().split('/');
        var rootCategoryUrl = paths[2];
        var categoryUrl = paths[3];
        var url = paths[4];
    	ProductsFactory.Product.get({ rootCategoryUrl: rootCategoryUrl, categoryUrl: categoryUrl, url: url }, { isArray: false, cache: true })
            .$promise.then(
            function (productFound) {
                if ((productFound === null || productFound === undefined) && productFound.SubProducts.length > 0) {
                    $window.location.href = "/products";
                }
                vm.product = productFound;
                vm.productLoaded = true;

                /*We have three different types of products
                * Product with a Single SubProduct
                * Product with a Multiple SubProduct
                * Product of type MYOC (Make Your Own Case) with a Multiple SubProducts (IsStackedProduct flag is used for this purpose)
                */
                vm.DisableAddToCardButton = vm.product.IsStackedProduct;
                //First, we need to know if its a product or a brochure
                if (vm.product.CanBePurchased) {
                    //Then, we need to know which kind of product it is
                    if (vm.product.IsStackedProduct) {
                        //MYOC
                    }
                    else {
                        vm.subProductIndex = 0;
                        vm.productOptSelected = productFound.SubProducts[vm.subProductIndex];
                        vm.productOptSelected.SelectedQuantity = productFound.MinimumQuantity;
                        vm.SetCalculatedPrice();
                        vm.productProfitTxt = Math.round(vm.CalculateProfit(vm.productOptSelected)) + '%';
                    }
                }               
                $rootScope.MetaService.set(vm.product.METATitle, vm.product.METADescription, vm.product.METAKeywords);
                GetRelatedProducts(vm.product.Category.Id, 12, false, true);
            },
            function (error) {
                $window.location.href = "/products";
            });


        vm.Calculate = function () {
           
            ProductsFactory.Product.UpdateShippingFee(vm.Product, vm.Quantity);
        };


        /*Featured Info Modal*/
        vm.OpenModal = function (parentSelector) {
            var parentElem = parentSelector ?
              angular.element(vm.scope.$document[0].querySelector('.featured-info-modal' + parentSelector)) : undefined;
            var modalInstance = vm.scope.$uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'FeaturedInfoModal.cshtml',
                controller: 'ModalCtrl',
                controllerAs: 'modalCtrl',
                size: 'lg',
                appendTo: parentElem,
                resolve: {
                    product: function () {
                        return vm.product;
                    }
                }
            });
        };
        vm.ToggleAnimation = function () {
            vm.animationsEnabled = !vm.animationsEnabled;
        };/*Featured Info Modal*/
        function GetRelatedProducts(categoryId, maxResults, random, slick) {
            slick = typeof slick !== 'undefined' ? slick : false;
            ProductsFactory.RelatedProducts.query({ id: categoryId, maxResults: maxResults, isRandom: random }, { cache: true }).$promise.then(
                    function (results) {
                        vm.executing = true;
                        vm.banners = results;
                        if (slick) {
                            ActivateSlick();
                        }
                    },
                     function (error) {
                         $log.error(error);
                     });
        }
        function GetRelatedPrograms() {
            ProductsFactory.RelatedProducts.query({ code: vm.product.Category.Name, maxResults: 12, isRandom: false, canBePurchased: false }, { cache: true }).$promise.then(
                    function (results) {
                        vm.executing = true;
                        vm.banners = results;
                        ActivateSlick();
                    },
                     function (error) {
                         $log.error(error);
                     });
        }
        vm.UpdateItem = function () {
            if (vm.product.IsStackedProduct) {
                var numberOfSelectedProducts = 0;
                for (var i = 0; i < vm.product.SubProducts.length; i++) {
                    numberOfSelectedProducts += (vm.product.SubProducts[i].SelectedQuantity === undefined ? 0 : vm.product.SubProducts[i].SelectedQuantity);
                }
                if (numberOfSelectedProducts > 0) {
                    vm.DisableAddToCardButton = (numberOfSelectedProducts % vm.product.MinimumDivisor !== 0);
                } else {
                    vm.DisableAddToCardButton = true;
                }
            }
            else {
                for (var i = 0; i < vm.product.SubProducts.length; i++) {
                    vm.product.SubProducts[i].SelectedQuantity = 0;
                }
                vm.subProductIndex = vm.product.SubProducts.indexOf(vm.productOptSelected);
                vm.productOptSelected.SelectedQuantity = 1;
                vm.SetCalculatedPrice();
                vm.productProfitTxt = Math.round(vm.CalculateProfit(vm.productOptSelected)) + '%';
            } 
        };
        vm.SetCalculatedPrice = function () {

        	if (vm.productOptSelected.Profit.length > 0) {
        		//There are business rules for prize ranges
        		var selectedQty = vm.productOptSelected.SelectedQuantity;
        		for (var i = 0; i < vm.productOptSelected.Profit.length; i++) {
        			var range = vm.productOptSelected.Profit[i];
        			if (range.Min <= selectedQty && range.Max >= selectedQty)
        			{
        				vm.productPriceTxt = 'Cost: $' + roundTo((range.Price * selectedQty), 2);
        				vm.productProfitTxt = Math.round(vm.CalculateProfit(vm.productOptSelected)) + '%';
        				break;
        			}
        		}
        	}
        	else
        	{
        		//No price rule, claculate it normally
        		vm.productPriceTxt = 'Cost: $' + roundTo((vm.productOptSelected.Price * vm.productOptSelected.SelectedQuantity), 2);
        	}
        		
        	//vm.productPriceTxt = (vm.productOptSelected.SizeName !== null ? vm.productOptSelected.SizeName : 'Mastercase') + ' cost: $' + vm.productOptSelected.Price * vm.productOptSelected.SelectedQuantity;


        }
        vm.CalculateProfit = function (product) {
        		if (product.Profit.length > 0)
        		{
        			//There are business rules for prize ranges
        			var selectedQty = product.SelectedQuantity;
        			for (var i = 0; i < product.Profit.length; i++) {
        				var range = product.Profit[i];
                          if (range.Min <= selectedQty && range.Max >= selectedQty) {
                              return (((product.ProductQuantity * product.ProductSuggestedPrice) - range.Price) / (product.ProductQuantity * product.ProductSuggestedPrice)).toFixed(4) * 100;
        				}
        			}
        		}
        		else {
        			//No price rule, claculate it normally
                     return (((product.ProductQuantity * product.ProductSuggestedPrice) - product.Price) / (product.ProductQuantity * product.ProductSuggestedPrice)).toFixed(4) * 100
        		}  
        }

        function roundTo(n, digits) {
        	if (digits === undefined) {
        		digits = 0;
        	}

        	var multiplicator = Math.pow(10, digits);
        	n = parseFloat((n * multiplicator).toFixed(11));
        	var test = (Math.round(n) / multiplicator);
        	return +(test.toFixed(digits));
        }

        function ActivateSlick() {
            
            $timeout(function () {
                $("#related-products-rotator-slick").slick({
                    dots: false,
                    infinite: true,
                    rows: 1,
                    slidesToShow: 4,
                    slidesToScroll: 4,
                    prevArrow: '<button type="button" class="btn" style="position: absolute; top: 45%; display: block;  left: 1%; z-index: 99; background-color: #8C8B89; color:#FFFFFF; font-size:large;"><span class="glyphicon glyphicon-chevron-left"></span></button>',
                    nextArrow: '<button type="button" class="btn" style="position: absolute; top: 45%; display: block;  right: 1%; z-index: 99; background-color: #8C8B89; color:#FFFFFF; font-size:large;"><span class="glyphicon glyphicon-chevron-right"></span></button>',
                    responsive: [
                      {
                          breakpoint: 1024,
                          settings: {
                              rows: 1,
                              slidesToShow: 3,
                              slidesToScroll: 3,
                          }
                      },
                      {
                          breakpoint: 600,
                          settings: {
                              rows: 1,
                              slidesToShow: 2,
                              slidesToScroll: 2,
                          }
                      },
                      {
                          breakpoint: 480,
                          settings: {
                              rows: 1,
                              slidesToShow: 1,
                              slidesToScroll: 1,
                          }
                      }
                    ]
                });
                vm.executing = false;
            }, 1 * 1000);
        }
    }
    ProductsController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "ProductsFactory", "$localStorage", "$sce", "$uibModal", "$document", "$element", "MetaService", "referral"];
    module.controller("ProductsController", ProductsController);

    function ModalCtrl($uibModalInstance, $sce, product) {
        var vm = this;
        vm.product = product;
        vm.$sce = $sce;
        vm.CloseModal = function () {
            $uibModalInstance.dismiss('close');
        };
    }
    ModalCtrl.$inject = ["$uibModalInstance","$sce", "product"];
    module.controller("ModalCtrl", ModalCtrl);

    function ProductRelatedRotatorController($log, $scope, $timeout, ProductsFactory, $localStorage) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.executing = false;
        vm.interval = 0;
        vm.noWrap = false;
        ProductsFactory.RelatedProductsRotator.query({ isActive: true }, { cache: true }).$promise.then(
            function (results) {
                vm.executing = true;
                vm.banners = results;
                $timeout(function () {
                    $("#product-description-related-rotator-slick").slick({
                        dots: false,
                        infinite: true,
                        rows: 2,
                        slidesToShow: 4,
                        slidesToScroll: 4,
                        prevArrow: '<button type="button" class="btn" style="position: absolute; top: 45%; display: block;  left: 1%; z-index: 99; background-color: #8C8B89; color:#FFFFFF; font-size:large;"><span class="glyphicon glyphicon-chevron-left"></span></button>',
                        nextArrow: '<button type="button" class="btn" style="position: absolute; top: 45%; display: block;  right: 1%; z-index: 99; background-color: #8C8B89; color:#FFFFFF; font-size:large;"><span class="glyphicon glyphicon-chevron-right"></span></button>',
                        responsive: [
                          {
                              breakpoint: 1024,
                              settings: {
                                  rows: 2,
                                  slidesToShow: 3,
                                  slidesToScroll: 3,
                              }
                          },
                          {
                              breakpoint: 600,
                              settings: {
                                  rows: 1,
                                  slidesToShow: 2,
                                  slidesToScroll: 2,
                              }
                          },
                          {
                              breakpoint: 480,
                              settings: {
                                  rows: 1,
                                  slidesToShow: 1,
                                  slidesToScroll: 1,
                              }
                          }
                        ]
                    });
                    vm.executing = false;
                }, 1 * 1000);
            },
            function (error) {
                $log.error(error);
            });
    }
    ProductRelatedRotatorController.$inject = ["$log", "$scope", "$timeout", "ProductsFactory", "$localStorage"];
    module.controller("ProductRelatedRotatorController", ProductRelatedRotatorController);
})();