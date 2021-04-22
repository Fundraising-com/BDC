(function () {
    "use strict";
    var module = angular.module("ezfund.api");

    function ShoppingCartController(ShoppingCartFactory, $scope, $window, $log, $q, ProductsFactory, $rootScope, $location, $localStorage, $timeout, referral) {
        var vm = this;
        //$log.debug(referral.domain);
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.ShippingFee = 0;
        vm.SubTotal = 0;
        vm.TotalAmount = 0;
        vm.Taxes = [];
        vm.PromotionCode = {};
        vm.SaleBillingRegion = null;
        vm.AddingItemToCart = false;
        Calculate();
        if (vm.scope.$storage.ShoppingCart!== null && vm.scope.$storage.ShoppingCart.Items.length >= 1) {
            vm.SuggestedCategoryId = vm.scope.$storage.ShoppingCart.Items[0].Product.Category.Id;
            GetRelatedProducts(vm.SuggestedCategoryId, 2, true);
        }
        $scope.$on("saleBillingRegionChanged", function (event, data) {
            vm.SaleBillingRegion = data;
            Calculate();
        });
        $scope.$on("promotionCodeChanged", function (event, data) {
            vm.PromotionCode = data;
            vm.scope.$storage.ShoppingCart.PromotionCodeId = data == null ? 0 : data.Id;
            Calculate();
        });
        /*
        * Calculates the SubTotal and Shipping Fee for the Shopping Cart
        */
        function Calculate() {
            if (typeof vm.scope.$storage.ShoppingCart === 'undefined' || vm.scope.$storage.ShoppingCart === null) {
                return;
            }
            vm.SubTotal = 0;
            vm.ShippingFee = 0;
            vm.TotalAmount = 0;
            vm.Taxes = [];
            vm.SuggestedCategoryId;
            var isPromotionCodeUsed = false;
            var productsCountries = [];
            if (vm.PromotionCode != null && typeof vm.PromotionCode != 'undefined') {
                vm.PromotionCode.DiscountedAmount = 0;
            }
            for (var i = 0; i < vm.scope.$storage.ShoppingCart.Items.length; i++) {
                ProductsFactory.UpdatePrice(vm.scope.$storage.ShoppingCart.Items[i].Product, vm.scope.$storage.ShoppingCart.Items[i].Quantity);
                ProductsFactory.UpdateShippingFee(vm.scope.$storage.ShoppingCart.Items[i].Product, vm.scope.$storage.ShoppingCart.Items[i].Quantity);
                vm.SubTotal += vm.scope.$storage.ShoppingCart.Items[i].Product.CalculatedPrice * vm.scope.$storage.ShoppingCart.Items[i].Quantity;
                vm.ShippingFee += vm.scope.$storage.ShoppingCart.Items[i].Product.ShippingFee;
                if (!vm.scope.$storage.ShoppingCart.Items[i].Product.IsStackedProduct) {
                    for (var x = 0; x < vm.scope.$storage.ShoppingCart.Items[i].Product.SubProducts.length; x++) {
                        if (vm.scope.$storage.ShoppingCart.Items[i].Product.SubProducts[x].SelectedQuantity > 0) {
                            vm.scope.$storage.ShoppingCart.Items[i].Product.SubProducts[x].SelectedQuantity = vm.scope.$storage.ShoppingCart.Items[i].Quantity;
                        }
                    }
                }

                if (productsCountries.indexOf(vm.scope.$storage.ShoppingCart.Items[i].Product.CountryCode) === -1) {
                    productsCountries.push(vm.scope.$storage.ShoppingCart.Items[i].Product.CountryCode);
                }
                if (vm.scope.$storage.ShoppingCart.Items[i].Product.RequireTaxes) {
                    if (vm.SaleBillingRegion !== null) {
                        var taxes = vm.scope.$storage.ShoppingCart.Items[i].Product.Taxes;
                        for (var j = 0; j < taxes.length; j++) {
                            if (taxes[j].StateCode === vm.SaleBillingRegion.Code) {
                                if (vm.Taxes.indexOf(vm.Taxes[taxes[j].TaxCode]) === -1) {
                                    vm.Taxes.push({ Code: taxes[j].TaxCode, Amount: ((vm.scope.$storage.ShoppingCart.Items[i].Product.CalculatedPrice * vm.scope.$storage.ShoppingCart.Items[i].Quantity) + vm.scope.$storage.ShoppingCart.Items[i].Product.ShippingFee) * (vm.scope.$storage.ShoppingCart.Items[i].Product.Taxes[j].Rate / 100.0) });
                                } else {
                                    vm.Taxes[vm.Taxes.indexOf(taxes[j].TaxCode)].Amount += ((vm.scope.$storage.ShoppingCart.Items[i].Product.CalculatedPrice * vm.scope.$storage.ShoppingCart.Items[i].Quantity) + vm.scope.$storage.ShoppingCart.Items[i].Product.ShippingFee) * (vm.scope.$storage.ShoppingCart.Items[i].Product.Taxes[j].Rate / 100.0);
                                }
                            }
                        }
                    }

                }
            }
            for (var k = 0; k < vm.Taxes.length; k++) {
                vm.TotalAmount += vm.Taxes[k].Amount;
            }
            vm.TotalAmount += (vm.SubTotal + vm.ShippingFee);
            vm.scope.$storage.ShoppingCart.TotalAmount = vm.TotalAmount;
            vm.scope.$storage.ShoppingCart.ShippingFee = vm.ShippingFee;
            vm.scope.$storage.ShoppingCart.HasProductsFromDifferentCountries = productsCountries.length > 1;
        };
        vm.AddItem = function (product) {
            vm.AddingItemToCart = true;
            var quantity = 1;
            if (!product.IsStackedProduct) {
                for (var i = 0; i < product.SubProducts.length; i++) {
                    if (typeof product.SubProducts[i].SelectedQuantity !== 'undefined' && product.SubProducts[i].SelectedQuantity > 0) {
                        quantity = product.SubProducts[i].SelectedQuantity;
                    }
                }
            }
            vm.scope.$storage.ShoppingCart.Items.push({ Quantity: quantity, ShoppingCartId: vm.scope.$storage.ShoppingCart.Id, ProductId: product.Id, Product: product });
            $timeout(function () {
                $window.location.href = "/shopping-cart/index";
            },
               3500,
               false);
        };
        vm.RemoveItem = function (index) {
            vm.scope.$storage.ShoppingCart.Items.splice(index, 1);
            Calculate();
        };
        vm.UpdateItem = function () {
            Calculate();
        };
        vm.CollapseItems = function (item) {
            item.Product.isCollapsed = !item.Product.isCollapsed;
        };
        function GetRelatedProducts(categoryId, maxResults, random) {
            ProductsFactory.RelatedProducts.query({ id: categoryId, maxResults: maxResults, isRandom: random }, { cache: true }).$promise.then(
                    function (results) {
                        vm.executing = true;
                        vm.banners = results;
                    },
                     function (error) {
                         $log.error(error);
                     });
        }
    }
    ShoppingCartController.$inject = ["ShoppingCartFactory", "$scope", "$window", "$log", "$q", "ProductsFactory", "$rootScope", "$location", "$localStorage", "$timeout", "referral"];
    module.controller("ShoppingCartController", ShoppingCartController);

    function SalesController($scope, $sce, ShoppingCartFactory, SalesFactory, $log, AddressHygieneFactory, MasksConstants, ExceptionFactory, $uibModal, LeadsFactory, NotificationFactory, $q, $window, hosts, $timeout, GoogleAnalyticsService, $rootScope, $localStorage, $document, $element) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$uibModal = $uibModal;
        vm.scope.$document = $document;
        vm.countries = AddressHygieneFactory.Countries;
        vm.states = AddressHygieneFactory.States.Get(vm.countries[0]);
        vm.ActionState = 0;
        vm.progress = 0;
        vm.progressMessage = "";
        vm.error = {};
        vm.phoneMask = MasksConstants.Phone;
        vm.creditCardNumberMask = MasksConstants.CreditCardNumber;
        vm.creditCardCVVMask = MasksConstants.CreditCardCVV;
        vm.creditCardCVVMaskAMEX = MasksConstants.CreditCardCVVAmex;
        vm.creditCardExpirationDateMask = MasksConstants.CreditCardExpirationDate;

    	
        /*Referrals*/
        LeadsFactory.Referral.query().$promise.then(
            function (result) {
                if (result.length > 0) {
                    vm.learnAboutUsOptions = result;
                }
            },
            function (error) {
                $log.error(error);
            });
        vm.Sale = {
            Items: [],
            Client: {
                Id: 0,
                DivisionId: 1,
                Addresses: [
                   {
                       Id: 0,
                       ClientId: 0,
                       Type: "bt",
                       AddressZoneId: 1,
                       Country: { Code: "US" },
                       Region: { Code: "AL" },
                   }, {
                       Id: 0,
                       ClientId: 0,
                       Type: "st",
                       AddressZoneId: 1,
                       Country: { Code: "US" },
                       Region: { Code: "AL" },
                   }
                ]
            },
            CreditCard: {},
            PaymentMethod: "1",
            InternalPaymentMethod: 2,
            UserId: vm.scope.$storage.AnonymousId,
            TotalAmount: vm.scope.$storage.ShoppingCart.TotalAmount
        };
        //Setup of Products
        for (var i = 0; i < vm.scope.$storage.ShoppingCart.Items.length; i++) {
            var shoppingCartItem = vm.scope.$storage.ShoppingCart.Items[i];
            vm.Sale.Items.push({
                OrderId: 0,
                InvoicedAmount: shoppingCartItem.Product.CalculatedPrice,
                Product: shoppingCartItem.Product,
                Quantity: shoppingCartItem.Quantity
            });
        }
        //Change of billing and shipping State/Country depending on the Products country
        var country = vm.Sale.Items[0].Product.CountryCode === "CA" ? vm.countries[1] : vm.countries[0];
        vm.postCodeMask = country.Code === "CA" ? MasksConstants.CanadaPostCode : MasksConstants.USPostCode;
        vm.states = AddressHygieneFactory.States.Get(country);
        vm.Sale.Client.Addresses[0].Country = country;
        vm.Sale.Client.Addresses[1].Country = country;
        vm.Sale.Client.Addresses[0].Region = vm.states[0];
        vm.Sale.Client.Addresses[1].Region = vm.states[0];
        // Raise the event for the first time for the default information
        $rootScope.$broadcast("saleBillingRegionChanged", vm.Sale.Client.Addresses[0].Region);

        vm.getMYOCList = function (item) {
            var productsList = "";
            for (var i = 0; i < item.Product.SubProducts.length; i++) {
            	if (typeof item.Product.SubProducts[i].SelectedQuantity !== 'undefined' &&
						item.Product.SubProducts[i].SelectedQuantity !== null &&
						item.Product.SubProducts[i].SelectedQuantity > 0) {
                    productsList += item.Product.SubProducts[i].Name + " <br> ";
                }
            }
            vm.MYOCList = $sce.trustAsHtml(productsList);
        }
        /*MYOC Info Modal*/
        vm.OpenModal = function (product) {
            var parentElem = angular.element(vm.scope.$document[0].querySelector('.myoc-info-modal'));
            var modalInstance = vm.scope.$uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'MYOCInfoModal.cshtml',
                controller: 'ModalCtrl',
                controllerAs: 'modalCtrl',
                size: 'lg',
                appendTo: parentElem,
                resolve: {
                    product: function () {
                        return product;
                    }
                }
            });
        };

        vm.BroadcastRegionSelection = function () {
            $rootScope.$broadcast("saleBillingRegionChanged", vm.Sale.Client.Addresses[0].Region);
        };

        function SelectedProductsCode() {
            var productsArray = [];
            var selectedProductsMap = {};
            for (var i = 0; i < vm.Sale.Items.length; i++) {
                for (var j = 0; j < vm.Sale.Items[i].Product.SubProducts.length; j++) {
                    if (typeof vm.Sale.Items[i].Product.SubProducts[j].SelectedQuantity !== 'undefined'
                        && vm.Sale.Items[i].Product.SubProducts[j].SelectedQuantity !== null
                        && vm.Sale.Items[i].Product.SubProducts[j].SelectedQuantity > 0) {
                        if (typeof selectedProductsMap[vm.Sale.Items[i].Product.SubProducts[j].ProductCode] === 'undefined') {
                            selectedProductsMap[vm.Sale.Items[i].Product.SubProducts[j].ProductCode] = "ok";
                            productsArray.push(vm.Sale.Items[i].Product.SubProducts[j].ProductCode);
                        }
                        vm.TotalSaleAmount;
                    }
                    else {
                        vm.Sale.Items[i].Product.SubProducts.splice(j--, 1);
                    }
                }
            }
            return productsArray
        }
        var promises = [];
        vm.Create = function () {
            vm.Sale.TotalAmount = vm.scope.$storage.ShoppingCart.TotalAmount;
            vm.Sale.ShippingCalculatedAmount = vm.scope.$storage.ShoppingCart.ShippingFee;
            vm.ActionState = 1;
            vm.progress = 10;
            //vm.progressMessage = "Please verify your Shipping Address";
            /*Address Verification*/
           // var adressVerificationPromises = [];
            //adressVerificationPromises.push(AddressHygieneFactory.AddressHygiene.save(vm.Sale.Client.Addresses[0]).$promise);
            //adressVerificationPromises.push(AddressHygieneFactory.AddressHygiene.save(vm.Sale.Client.Addresses[1]).$promise);
            //$q.all(adressVerificationPromises).then(
               //function (suggestedAddresses) {
                   //var modalScope = $scope.$new(true);
                  // angular.extend(modalScope, { header: 'Verify your Addresses', addresses: suggestedAddresses, showEditFields: false, states: vm.states });
                  // var modal = $uibModal.open({
                       //animation: true,
                      // backdrop: false,
                      // scope: modalScope,
                      // templateUrl: "/Scripts/app/templates/addressHygieneModalTemplateMultiple.html",
                      // windowTemplateUrl: "/Scripts/app/templates/addressHygieneWindowTemplate.html"
                  // });
            $q.all(promises).then(
                function (data) {
                    vm.progress = 15;
                    vm.progressMessage = "Sending your information...";
                          //vm.Sale.Client.Addresses[0].Address1 = modifiedAddresses[0].Address1;
                         // vm.Sale.Client.Addresses[0].City = modifiedAddresses[0].City;
                          //vm.Sale.Client.Addresses[0].Region = modifiedAddresses[0].Region;
                          //vm.Sale.Client.Addresses[0].Country = modifiedAddresses[0].Country;
                          //vm.Sale.Client.Addresses[0].PostCode = modifiedAddresses[0].PostCode;
                          //vm.Sale.Client.Addresses[1].Address1 = modifiedAddresses[1].Address1;
                          //vm.Sale.Client.Addresses[1].City = modifiedAddresses[1].City;
                          //vm.Sale.Client.Addresses[1].Region = modifiedAddresses[1].Region;
                          //vm.Sale.Client.Addresses[1].Country = modifiedAddresses[1].Country;
                          //vm.Sale.Client.Addresses[1].PostCode = modifiedAddresses[1].PostCode;

                          /*Checkout Notification*/
                                                

                          switch (vm.Sale.PaymentMethod) {
                          	case "2": // Purchase Order
                          		vm.Sale.InternalPaymentMethod = 16;
                          		break;
                          	case "3": //Paypal
                          		vm.Sale.InternalPaymentMethod = 15;
                          		break;
                          }
                          vm.Sale.Status = 15; // New Status
                      		/*Create the Sale*/
                          SalesFactory.Sale.save(vm.Sale).$promise.then(
									function (sale)
									{
										vm.Sale = sale;
										vm.progress = 30;
										/*Confirm the Sale adding the data to EzOps*/
										vm.progressMessage = "Creating your sale...";

										/*
										* Actions depending of payment method
										*/
										switch (vm.Sale.PaymentMethod) {
											case 1: //Credit Card
												vm.progress = 50;
												vm.progressMessage = "Doing the Credit Card charge...";
												chargeCreditCard().then(
                                       function (chargeCreditCardPromiseResult) {
                                       	vm.progress = 70;
                                       	vm.progressMessage = "Applying the payment...";
                                       	vm.Sale.SaleAuthorizationNumber = chargeCreditCardPromiseResult.AuthNumber;

                                           //applyPayments(chargeCreditCardPromiseResult.AuthNumber);
                                           //place order to invoiced in db for succesful cc processing
                                                        confirmSales(vm.Sale); 
                                                        applyPayments(vm.Sale);

                                       	vm.progressMessage = "Sending your confirmation email...";
                                       	/*Sale Notification*/
                                       	var extraData = "Sale Authorization Number: " + vm.Sale.SaleAuthorizationNumber;
                                       	NotificationFactory.Notification.save({ Type: 13, ExternalId: vm.Sale.OrderId, Email: "fake@fake.com" });
                                          vm.progress = 90;
                                          NotificationFactory.Notification.save({ Type: 7, ExternalId: vm.Sale.OrderId, Email: "fake@fake.com" });
                                       	/*Create Sale in Analytics*/
                                       	GoogleAnalyticsService.SaleCreated(vm.Sale);
                                       	confirmShoppingCart(vm.Sale.OrderId).then(
																function (confirmShoppingCartResult) {
																	gotoConfirmationPage(vm.Sale.OrderId);
																},
																function (error) {
																	//Shopping Cart confirmation failed but this is not a mandatory step.
																	gotoConfirmationPage(vm.Sale.OrderId);
																});
                                       },
                                       function (error) {
                                       	//We want to give them the option to enter a new Credit Card
                                           NotificationFactory.Notification.save({ Type: 10, ExternalId: vm.Sale.OrderId, Email: "fake@fake.com" });
                                       	vm.ActionState = 3;
                                       	vm.progress = 0;
                                       	console.error(error);
                                          vm.error.Message = "Oops! Sorry but we were not able to continue with your order. You don't need to place the order again, an Ezfund Expert will contact you soon.";
                                          vm.error = ExceptionFactory.Handle(error.data);



                                       });
												break;
											case 2: //Purchase Order
												vm.progress = 60;
												/*Confirm the Sale adding the data to EzOps*/
												vm.Sale.Status = 20; // WAIT-CHK Status
												vm.progress = 70;
												vm.progressMessage = "Sending your confirmation email...";

												/*Sale Notification*/
												var extraData = "Sale Waiting for Check ";
												NotificationFactory.Notification.save({ Type: 13, ExternalId: vm.Sale.OrderId, ExtraData: extraData });
                                    vm.progress = 90;
                                    NotificationFactory.Notification.save({ Type: 7, ExternalId: vm.Sale.OrderId, Email: "fake@fake.com" });

												/*Create Sale in Analytics*/
												GoogleAnalyticsService.SaleCreated(vm.Sale);
												confirmShoppingCart(vm.Sale.OrderId).then(
												function (confirmShoppingCartResult) {
													gotoConfirmationPage(vm.Sale.OrderId);
												},
												function (error) {
													//Shopping Cart confirmation failed but this is not a mandatory step.
													gotoConfirmationPage(vm.Sale.OrderId);
												}); 
												break;
											case 3: //Paypal
												var subTotal = 0;
												var shippingFee = 0;
												vm.progress = 70;
												vm.progressMessage = "Taking you to the Paypal website, be sure to come back to finish the Sale!";
                                                createPaypalPayment().then(
                                        function (data) {
                                            NotificationFactory.Notification.save({ Type: 13, ExternalId: vm.Sale.OrderId, Email: "fake@fake.com" });
                                            vm.progress = 90;
                                            NotificationFactory.Notification.save({ Type: 7, ExternalId: vm.Sale.OrderId, Email: "fake@fake.com" });
                                            $window.location.href = data.RedirectUrl;
                                       },
                                       function (error) {
                                       	vm.ActionState = 4;
                                       	vm.progress = 0;
                                       	vm.error = ExceptionFactory.Handle(error.data);
                                       	vm.error.Message = "Oops! Sorry but we were not able to continue with your order. You don't need to place the order again, a Fundraising Expert will contact you soon.";
                                       	vm.scope.$storage.ShoppingCart = null;
                                       });
												break;
										}

									},function (error) {
											var extraData = "Products attempted to be purchased: ";
											for (var j = 0; j < vm.scope.$storage.ShoppingCart.Items.length; j++) {
												extraData += vm.scope.$storage.ShoppingCart.Items[j].Product.Name + " (" + vm.scope.$storage.ShoppingCart.Items[j].Quantity + "), ";
											}
											vm.ActionState = 4;
											vm.progress = 0;
											vm.error = ExceptionFactory.Handle(error.data);
											vm.error.Message = "Oops! Sorry but we were not able to place your order. You don't need to place the order again, an EzFund Expert will contact you soon.";
											vm.scope.$storage.ShoppingCart = undefined;
									});								
                      }, function (error) {
                          //User canceled the Address Hygiene, no error here
                          vm.ActionState = 0;
                          vm.progress = 0;
                      }),
               //},
               function (error) {
                   //Address Hygiene Service failed
                   vm.ActionState = 3;
                   vm.progress = 0;
                   vm.error = ExceptionFactory.Handle(error.data);
                   vm.error.Message = "Sorry but we can't validate your Shipping Address. We're fixing the problem, please come back in a few minutes.";
               }
        };
       
        /*
        * Finishes the last steps of the payment after the Client comes back from the Paypal website
        */
        vm.FinishPaypal = function (paymentId, payerId) {
            executePaypalPayment(paymentId, payerId).then(
               function (data) {
                   var clientId = data.ClientId;
                   var authorizationCode = data.AuthorizationCode;
                   SalesFactory.Sale.query({ clientId: clientId }, { isArray: true, cache: false }).$promise.then(
                      function (sales) {
                          vm.Sales = sales;
                          for (var k = 0; k < vm.Sales.length; k++) {
                              GoogleAnalyticsService.SaleCreated(vm.Sales[k]);
                          }
                          vm.Sale = vm.Sales[0];
                          vm.Sale.CreditCard = {
                              Number: "1111111111111111",
                              Holder: "-",
                              ExpirationDate: "0000",
                              CVV: "000",
                              InternalPaymentMethod: 15,
                              Amount: 1,
                              Address: { Address1: "-", City: "-", Region: { Code: 'AL' }, Country: { Code: "US" }, PostCode: "00000" }
                          };
                          applyPayments(authorizationCode).then(
                             function (data) {
                                 confirmSales().then(
                                    function (data) {
                                        NotificationFactory.Notification.save({ Type: 13, ExternalId: clientId, Email: "fake@fake.com" });
                                        vm.progress = 70;
                                        vm.progressMessage = "Sending your confirmation email...";
                                        confirmShoppingCart(vm.Sales[0].Client.Id).then(
                                           function (confirmShoppingCartResult) {
                                               gotoConfirmationPage(clientId);
                                           },
                                           function (error) {
                                               //Shopping Cart confirmation failed but this is not a mandatory step.
                                               gotoConfirmationPage(clientId);
                                           });
                                    },
                                    function (error) {
                                        //Process failed but customer should continue, we will take care of the rest.
                                        gotoConfirmationPage(clientId);
                                        NotificationFactory.Notification.save({ Type: 9, ExternalId: clientId, Email: "fake@fake.com" });
                                    });
                             }, function (error) {
                                 //Process failed but customer should continue, we will take care of the rest.
                                 gotoConfirmationPage(clientId);
                                 NotificationFactory.Notification.save({ Type: 9, ExternalId: clientId, Email: "fake@fake.com" });
                             });
                      },
                      function (error) {
                          //Process failed but customer should continue, we will take care of the rest.
                          gotoConfirmationPage(clientId);
                          NotificationFactory.Notification.save({ Type: 9, ExternalId: clientId, Email: "fake@fake.com" });
                      });
               },
               function (error) {
                   vm.ActionState = 4;
                   vm.progress = 0;
                   vm.error = ExceptionFactory.Handle(error.data);
                   vm.error.Message = "Oops! Sorry but we were not able to place your order. You don't need to place the order again, a Fundraising Expert will contact you soon.";
                   vm.scope.$storage.ShoppingCart = null;
               });
        };
        
        //Takes the User to the Confirmation page
        function gotoConfirmationPage(orderId) {
            vm.progress = 100;
            vm.progressMessage = "Taking you to the Confirmation page, just one more second...";
            vm.scope.$storage.ShoppingCart = null;
            $timeout(function () {
                $window.location.href = "/shopping-cart/order-confirmation/" + orderId;
            },
               2000,
               false);
        };
        /*
        * Updates the Shopping Cart to Closed since the Sale was confirmed.
        */
        function confirmShoppingCart(orderId) {
            vm.scope.$storage.ShoppingCart.Status = 2;
            vm.scope.$storage.ShoppingCart.OrderId = orderId;
            return ShoppingCartFactory.ShoppingCart.save(vm.scope.$storage.ShoppingCart).$promise;
        };

        /*
        * Confirms the Sale as Paid
        */
        function confirmSales(sale) {
            var deferred = $q.defer();
            var promises = [];
            //for (var j = 0; j < vm.Sales.length; j++) {
                //var sale = vm.Sales[j];
                var now = new Date();
                //sale.Confirmed = now.toISOString().slice(0, 19).replace('T', ' ');
                //now.setDate(now.getDate() + 9);
                //sale.ScheduledDelivery = now.toISOString().slice(0, 19).replace('T', ' ');
                sale.Status = 90;
                //sale.ARStatus = 20;
                promises.push(SalesFactory.Sale.update(sale).$promise);
           // }
            $q.all(promises).then(
               function (data) {
                   NotificationFactory.Notification.save({ Type: 5, ExternalId: vm.Sales[0].ClientId, Email: "fake@fake.com", ExtraData: "15" });
                   deferred.resolve(data);
               },
               function (error) {
                   deferred.reject(error);
               });
            return deferred.promise;
        };
        /*
        * Charges the Credit Card with the Amount of the Shopping Cart
        */
        function chargeCreditCard() {
            var creditCard = {
                Email: vm.Sale.Client.Email, Number: vm.Sale.CreditCard.Number, Holder: vm.Sale.CreditCard.Holder, ExpirationDate: vm.Sale.CreditCard.ExpirationDate,
                CVV: vm.Sale.CreditCard.CVV, InternalPaymentMethod: vm.Sale.InternalPaymentMethod, Amount: vm.Sale.TotalAmount, Reference: vm.Sale.OrderId,
                Address: {
                    Address1: vm.Sale.Client.Addresses[0].Address1, City: vm.Sale.Client.Addresses[0].City, Region: vm.Sale.Client.Addresses[0].Region,
                    Country: vm.Sale.Client.Addresses[0].Country, PostCode: vm.Sale.Client.Addresses[0].PostCode
                }
            };
            return SalesFactory.CreditCard.save(creditCard).$promise;
        };
        /*
        * Creates a Paypal payment and returns the approval URL
        */
        function createPaypalPayment() {
            var client = vm.Sales[0].Client;
            return SalesFactory.Paypal.get({ clientId: client.Id }).$promise;
        };
        /*
        * Executes the Paypal payment and returns payment id
        */
        function executePaypalPayment(paymentId, payerId) {
            var paypalPaymentRequest = { PaymentId: paymentId, PayerId: payerId };
            return SalesFactory.Paypal.save(paypalPaymentRequest).$promise;
        };
        /*
        * Applies a Payment to all the Sales whos Credit Card went OK
        */
        function applyPayments(sale) {
            var deferred = $q.defer();
            var promises = [];
            var sale = vm.Sale;
            var payment = {
                OrdrId: sale.OrderId,
                InternalPaymentMethod: sale.InternalPaymentMethod,
                OrgID: sale.OrganizationId,
                Amount: sale.TotalAmount,
                TRNSAMT: sale.TotalAmount
            };

            promises.push(SalesFactory.Payment.save(payment).$promise);

            $q.all(promises).then(
               function (data) {
                   deferred.resolve(data);
               },
               function (error) {
                   deferred.reject(error);
               });
            return deferred.promise;
        };

        /**
        * Copies the Billing Address into the Shipping Address
        */
        vm.CopyAddress = function () {
            vm.Sale.Client.Addresses[1].AttentionOf = vm.Sale.Client.FirstName + ' ' + vm.Sale.Client.LastName;
            vm.Sale.Client.Addresses[1].Address1 = vm.Sale.Client.Addresses[0].Address1;
            vm.Sale.Client.Addresses[1].City = vm.Sale.Client.Addresses[0].City;
            vm.Sale.Client.Addresses[1].Region = vm.Sale.Client.Addresses[0].Region;
            vm.Sale.Client.Addresses[1].PostCode = vm.Sale.Client.Addresses[0].PostCode;
            vm.Sale.Client.Addresses[1].AddressZoneId = vm.Sale.Client.Addresses[0].AddressZoneId;
        };
    }
    SalesController.$inject = ["$scope", "$sce", "ShoppingCartFactory", "SalesFactory", "$log", "AddressHygieneFactory", "MasksConstants", "ExceptionFactory", "$uibModal", "LeadsFactory", "NotificationFactory", "$q", "$window", "hosts", "$timeout", "GoogleAnalyticsService", "$rootScope", "$localStorage", "$document", "$element"];
    module.controller("SalesController", SalesController);

    function ConfirmationController($scope, ShoppingCartFactory, SalesFactory, $log, $location, ProductsFactory, $localStorage) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        delete vm.scope.$storage.ShoppingCart;
        vm.scope.$storage.ShoppingCart = { Id: 0, AnonymousId: "0", Items: [] };
        var path = $location.path().split('/');
        vm.OrderId = path[path.length - 1];
        /*Retrieve the Sale by Order Id*/
        vm.ShoppingCart = ShoppingCartFactory.ShoppingCart.get({ orderId: vm.OrderId }, { isArray: false, cache: false }).$promise.
            then(function (result) {
                vm.Sale = result;
                for (var i = 0; i < vm.Sale.Items.length; i++) {
                    for (var x = 0; x < vm.Sale.Items[i].Product.SubProducts.length; x++) {
                        vm.Sale.Items[i].Product.CalculatedPrice += vm.Sale.Items[i].Product.SubProducts[x].SelectedQuantity * vm.Sale.Items[i].Product.SubProducts[x].Price;
                    }
                }
            },
            function (error) {

            }
        );
        vm.CollapseItems = function (item) {
            item.Product.isCollapsed = !item.Product.isCollapsed;
        };
    };
    ConfirmationController.$inject = ["$scope", "ShoppingCartFactory", "SalesFactory", "$log", "$location", "ProductsFactory", "$localStorage"];
    module.controller("ConfirmationController", ConfirmationController);
})();