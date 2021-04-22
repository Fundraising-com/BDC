(function () {
    "use strict";
    var module = angular.module("core.sales");

    function ShoppingCartController(ShoppingCartFactory, $scope, $window, $log, $q, ProductsFactory, $rootScope, $location, $localStorage, $timeout) {
        var vm = this;
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
            if (vm.scope.$storage.ShoppingCart === undefined || vm.scope.$storage.ShoppingCart === null) {
                return;
            }
            vm.SubTotal = 0;
            vm.ShippingFee = 0;
            vm.TotalAmount = 0;
            vm.Taxes = [];
            var isPromotionCodeUsed = false;
            var productsCountries = [];
            var productsCategoriesShipping = [];
            var validateCategoryShipping;
            if (vm.PromotionCode != null && vm.PromotionCode != undefined) {
                vm.PromotionCode.DiscountedAmount = 0;
            }
            for (var i = 0; i < vm.scope.$storage.ShoppingCart.Items.length; i++) {
                ProductsFactory.Product.UpdatePrice(vm.scope.$storage.ShoppingCart.Items[i].Product, vm.scope.$storage.ShoppingCart.Items[i].Quantity);
                //ProductsFactory.Product.UpdateShippingFee(vm.scope.$storage.ShoppingCart.Items[i].Product, vm.scope.$storage.ShoppingCart.TotalAmount);
                vm.SubTotal += vm.scope.$storage.ShoppingCart.Items[i].Product.CalculatedPrice * vm.scope.$storage.ShoppingCart.Items[i].Quantity;
                ProductsFactory.Product.UpdateShippingFee(vm.scope.$storage.ShoppingCart.Items[i].Product, vm.SubTotal);

                // Start Promotion Code for Product Level
                if (vm.PromotionCode != null &&
                    vm.PromotionCode != undefined &&
                    vm.PromotionCode.ScopeType == 2 &&
                    !isPromotionCodeUsed) {
                    var scratchBookFound = false;
                    for (var w = 0; w < vm.PromotionCode.Products.length; w++) {
                        scratchBookFound = scratchBookFound || vm.PromotionCode.Products[w].ScratchBookId == vm.scope.$storage.ShoppingCart.Items[i].Product.ScratchBookId;
                    }

                    if (scratchBookFound) {
                        if (vm.PromotionCode.DiscountType == 1) {
                            vm.PromotionCode.DiscountedAmount = vm.PromotionCode.AmountDiscount;
                            isPromotionCodeUsed = true;
                        } else if (vm.PromotionCode.DiscountType == 2) {
                            vm.PromotionCode.DiscountedAmount = (vm.scope.$storage.ShoppingCart.Items[i].Product
                                .CalculatedPrice *
                                vm.scope.$storage.ShoppingCart.Items[i].Quantity *
                                1.0) *
                                (vm.PromotionCode.PercentageDiscount / 100.0);
                            isPromotionCodeUsed = true;
                        } else if (vm.PromotionCode.DiscountType == 3) {
                            vm.PromotionCode.DiscountedAmount += vm.scope.$storage.ShoppingCart.Items[i].Product.ShippingFee;
                        }
                        vm.TotalAmount += (vm.PromotionCode.DiscountedAmount * -1);
                    }
                }
                // End Promotion Code for Product Level
                
               
           
                    if (productsCategoriesShipping.length === 0) {
                        // No other product added to shipping
                        vm.ShippingFee += vm.scope.$storage.ShoppingCart.Items[i].Product.ShippingFee;
                        productsCategoriesShipping.push(vm.scope.$storage.ShoppingCart.Items[i].Product.Category.Id);
                    }
                    else {
                        // Check if there is not other product to ship for the same category
                        validateCategoryShipping = 0;
                        for (var s = 0; s < productsCategoriesShipping.length; s++) {
                            if (productsCategoriesShipping[s] === vm.scope.$storage.ShoppingCart.Items[i].Product.Category.Id) {
                                validateCategoryShipping = 1;
                            }
                        }
                        if (validateCategoryShipping === 0) {
                            vm.ShippingFee += vm.scope.$storage.ShoppingCart.Items[i].Product.ShippingFee;
                            productsCategoriesShipping.push(vm.scope.$storage.ShoppingCart.Items[i].Product.Category.Id);
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
            // Start Promotion Code for Shopping Cart Level
            if (vm.PromotionCode != null && vm.PromotionCode != undefined && vm.PromotionCode.ScopeType == 1) {
                if (vm.PromotionCode.DiscountType == 1) {
                    vm.PromotionCode.DiscountedAmount = vm.PromotionCode.AmountDiscount;
                } else if (vm.PromotionCode.DiscountType == 2) {
                    vm.PromotionCode.DiscountedAmount = (vm.SubTotal * 1.0) * (vm.PromotionCode.PercentageDiscount / 100.0);
                }
                vm.TotalAmount += (vm.PromotionCode.DiscountedAmount * -1);
            }
            // End Promotion Code for Shopping Cart Level
            vm.TotalAmount += (vm.SubTotal + vm.ShippingFee);
            vm.scope.$storage.ShoppingCart.TotalAmount = vm.TotalAmount;
            vm.scope.$storage.ShoppingCart.ShippingFee = vm.ShippingFee;
            vm.scope.$storage.ShoppingCart.HasProductsFromDifferentCountries = productsCountries.length > 1;
        };
        vm.AddItem = function (product, quantity) {
            vm.AddingItemToCart = true;
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

    }
    ShoppingCartController.$inject = ["ShoppingCartFactory", "$scope", "$window", "$log", "$q", "ProductsFactory", "$rootScope", "$location", "$localStorage", "$timeout"];
    module.controller("ShoppingCartController", ShoppingCartController);

    function SalesController($scope, ShoppingCartFactory, SalesFactory, $log, AddressHygieneFactory, MasksConstants, ExceptionFactory, $modal, LeadsFactory, NotificationFactory, $q, $window, hosts, $timeout, GoogleAnalyticsService, $rootScope, $localStorage) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
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

        var clientSequenceCode = vm.scope.$storage.Promotion.Id === 5961 ? "OF" : "IF";
        vm.Sale = {
            Items: [],
            Client: {
                Id: 0,
                DivisionId: 1,
                PromotionId: vm.scope.$storage.Promotion.Id,
                OrganizationClassCode: "OTH",
                SequenceCode: clientSequenceCode,
                Addresses: [
                    {
                        Id: 0,
                        ClientId: 0,
                        Type: "bt",
                        AddressZoneId: 1,
                        Country: { Code: "US" },
                        Region: { Code: "AL" },
                        ClientSequenceCode: clientSequenceCode
                    }, {
                        Id: 0,
                        ClientId: 0,
                        Type: "st",
                        AddressZoneId: 1,
                        Country: { Code: "US" },
                        Region: { Code: "AL" },
                        ClientSequenceCode: clientSequenceCode
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
                SaleId: 0,
                Quantity: shoppingCartItem.Quantity,
                Number: i + 1,
                ScratchBookId: shoppingCartItem.Product.ScratchBookId,
                Product: shoppingCartItem.Product
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

        vm.BroadcastRegionSelection = function () {
            $rootScope.$broadcast("saleBillingRegionChanged", vm.Sale.Client.Addresses[0].Region);
        };

        var appStr = '<%=ConfigurationManager.AppSettings["AddressHygieneON"]%>';

        var promises = [];
        vm.Create = function () {
            vm.Sale.TotalAmount = vm.scope.$storage.ShoppingCart.TotalAmount;
            vm.Sale.ShippingFee = vm.scope.$storage.ShoppingCart.ShippingFee;
            vm.ActionState = 1;
            vm.progress = 10;
            //vm.progressMessage = "Please verify your Shipping Address";
            //var adressVerificationPromises = [];
            //adressVerificationPromises.push(AddressHygieneFactory.AddressHygiene.save(vm.Sale.Client.Addresses[0]).$promise);
            //adressVerificationPromises.push(AddressHygieneFactory.AddressHygiene.save(vm.Sale.Client.Addresses[1]).$promise);
            //$q.all(adressVerificationPromises).then(
                //function (suggestedAddresses) {
                    //var modalScope = $scope.$new(true);
                    //angular.extend(modalScope, { header: 'Verify your Addresses', addresses: suggestedAddresses, showEditFields: false, states: vm.states });
                    //var modal = $modal.open({
                    //    backdrop: false,
                    //    scope: modalScope,
                    //    templateUrl: "/Scripts/app/core/templates/addressHygieneModalTemplateMultiple.html",
                     //   windowTemplateUrl: "/Scripts/app/core/templates/addressHygieneWindowTemplate.html"
                   // });
                    $q.all(promises).then(
                        function (data) {
                            vm.progress = 15;
                            vm.progressMessage = "Sending your information...";
                            //vm.Sale.Client.Addresses[0].Address1 = modifiedAddresses[0].Address1;
                            // vm.Sale.Client.Addresses[0].City = modifiedAddresses[0].City;
                            // vm.Sale.Client.Addresses[0].Region = modifiedAddresses[0].Region;
                            // vm.Sale.Client.Addresses[0].Country = modifiedAddresses[0].Country;
                            // vm.Sale.Client.Addresses[0].PostCode = modifiedAddresses[0].PostCode;
                            // vm.Sale.Client.Addresses[1].Address1 = modifiedAddresses[1].Address1;
                            // vm.Sale.Client.Addresses[1].City = modifiedAddresses[1].City;
                            // vm.Sale.Client.Addresses[1].Region = modifiedAddresses[1].Region;
                            //vm.Sale.Client.Addresses[1].Country = modifiedAddresses[1].Country;
                            //vm.Sale.Client.Addresses[1].PostCode = modifiedAddresses[1].PostCode;
                            LeadsFactory.Lead.save
                                ({
                                    Id: 0,
                                    RequestType: 2,
                                    TellMore: 2,
                                    FirstName: vm.Sale.Client.FirstName,
                                    LastName: vm.Sale.Client.LastName,
                                    Email: vm.Sale.Client.Email,
                                    Group: vm.Sale.Client.Organization,
                                    Address: vm.Sale.Client.Addresses[0], Website: "-",
                                    Phone: vm.Sale.Client.Phone, NumberOfMembers: "0",
                                    PromotionId: vm.scope.$storage.Promotion.Id,
                                    PartnerId: vm.scope.$storage.Partner.Id,
                                    KitType: 42,
                                    InitialPhoneNumberEntered: "true",
                                    Comments: vm.Sale.Comments,
                                    RepresentativeId: vm.scope.$storage.Representative.Id,
                                    ConsultantId: 3450,
                                    ChannelCode: 'INT'
                                }).$promise.then(
                                    function (lead) {
                                        vm.progress = 30;
                                        vm.progressMessage = "Almost there....";
                                        vm.Sale.Client.LeadId = lead.Id;
                                        vm.Sale.Client.ChannelCode = lead.ChannelCode;

                                        //-- TRACKING SALE FROM REP PORTAL
                                        if (lead.RepresentativeId == "0") {
                                            vm.Sale.ConsultantId = lead.ConsultantId;
                                            vm.Sale.Client.ConsultantId = lead.ConsultantId;
                                        }
                                        else {
                                            vm.Sale.ConsultantId = lead.RepresentativeId;
                                            vm.Sale.Client.ConsultantId = lead.RepresentativeId;
                                        }
                                        //-- END TRACKING SALE FROM REP PORTAL --

                                        //vm.Sale.Client.ConsultantId = lead.ConsultantId;
                                        //vm.Sale.ConsultantId = lead.ConsultantId;
                                        var notification = { Type: lead.IsDuplicated ? 6 : 2, ExternalId: lead.Id, Email: "fake@fake.com" }
                                        NotificationFactory.Notification.save(notification);
                                        vm.progress = 30;
                                        switch (vm.Sale.PaymentMethod) {
                                            case "2": // Purchase Order
                                                vm.Sale.InternalPaymentMethod = 16;
                                                break;
                                            case "3": //Paypal
                                                vm.Sale.InternalPaymentMethod = 15;
                                                break;
                                        }
                                        SalesFactory.Sale.save(vm.Sale).$promise.then(
                                            function (resultSaleSaved) {
                                                NotificationFactory.Notification.save({ Type: 7, ExternalId: resultSaleSaved.clientId, Email: "fake@fake.com" });
                                                SalesFactory.Sale.query({ clientId: resultSaleSaved.clientId }, { isArray: true, cache: false }).$promise.then(function (sales) {
                                                    vm.progress = 40;
                                                    vm.progressMessage = "Creating your sale...";
                                                    vm.Sales = sales;

                                                    switch (vm.Sale.PaymentMethod) {
                                                        case "1": //Credit Card
                                                            vm.progress = 50;
                                                            vm.progressMessage = "Doing the Credit Card charge...";
                                                            chargeCreditCard().then(
                                                                function (chargeCreditCardPromiseResult) {
                                                                    vm.progress = 60;
                                                                    vm.progressMessage = "Applying the payment...";
                                                                    applyPayments(chargeCreditCardPromiseResult.AuthNumber).then(
                                                                        function (data) {
                                                                            confirmSales().then(
                                                                                function (data) {
                                                                                    NotificationFactory.Notification.save({ Type: 13, ExternalId: resultSaleSaved.clientId, Email: "fake@fake.com" });
                                                                                    vm.progress = 70;
                                                                                    vm.progressMessage = "Sending your confirmation email...";
                                                                                    for (var k = 0; k < vm.Sales.length; k++) {
                                                                                        GoogleAnalyticsService.SaleCreated(vm.Sales[k]);
                                                                                    }
                                                                                    confirmShoppingCart(vm.Sales[0].Client.Id).then(
                                                                                        function (confirmShoppingCartResult) {
                                                                                            gotoConfirmationPage(resultSaleSaved.clientId);
                                                                                        },
                                                                                        function (error) {
                                                                                            //Shopping Cart confirmation failed but this is not a mandatory step.
                                                                                            gotoConfirmationPage(resultSaleSaved.clientId);
                                                                                        });
                                                                                },
                                                                                function (error) {
                                                                                    NotificationFactory.Notification.save({ Type: 9, ExternalId: resultSaleSaved.clientId, Email: "fake@fake.com" });
                                                                                    vm.ActionState = 4;
                                                                                    vm.progress = 0;
                                                                                    vm.error = ExceptionFactory.Handle(error.data);
                                                                                    vm.error.Message = "Oops! Sorry but we were not able to continue with your order. You don't need to place the order again, a Fundraising Expert will contact you soon.";
                                                                                    vm.scope.$storage.ShoppingCart = null;
                                                                                });
                                                                        },
                                                                        function (error) {
                                                                            NotificationFactory.Notification.save({ Type: 9, ExternalId: resultSaleSaved.clientId, Email: "fake@fake.com" });
                                                                            vm.ActionState = 4;
                                                                            vm.progress = 0;
                                                                            vm.error = ExceptionFactory.Handle(error.data);
                                                                            vm.error.Message = "Oops! Sorry but we were not able to continue with your order. You don't need to place the order again, a Fundraising Expert will contact you soon.";
                                                                            vm.scope.$storage.ShoppingCart = null;
                                                                        });
                                                                },
                                                                function (error) {
                                                                    //We want to give them the option to enter a new Credit Card
                                                                    NotificationFactory.Notification.save({ Type: 10, ExternalId: resultSaleSaved.clientId, Email: "fake@fake.com" });
                                                                    vm.ActionState = 3;
                                                                    vm.progress = 0;
                                                                    vm.Sale.Id = vm.Sales[0].Id; // To force a NO NEW sale
                                                                    vm.Sale.Client.Id = resultSaleSaved.clientId;
                                                                    console.error(error);
                                                                    vm.error = ExceptionFactory.Handle(error.data);
                                                                });
                                                            break;
                                                        case "2": //Purchase Order
                                                            vm.progress = 70;
                                                            vm.progressMessage = "Sending your confirmation email...";
                                                            NotificationFactory.Notification.save({ Type: 13, ExternalId: resultSaleSaved.clientId, Email: "fake@fake.com" });
                                                            for (var k = 0; k < vm.Sales.length; k++) {
                                                                GoogleAnalyticsService.SaleCreated(vm.Sales[k]);
                                                            }
                                                            confirmShoppingCart(vm.Sales[0].Client.Id).then(
                                                                function (confirmShoppingCartResult) {
                                                                    gotoConfirmationPage(resultSaleSaved.clientId);
                                                                },
                                                                function (error) {
                                                                    //Shopping Cart confirmation failed but this is not a mandatory step.
                                                                    gotoConfirmationPage(resultSaleSaved.clientId);
                                                                });
                                                            break;
                                                        case "3": //Paypal
                                                            var subTotal = 0;
                                                            var shippingFee = 0;
                                                            for (var j = 0; j < vm.Sales.length; j++) {
                                                                shippingFee += vm.Sales[j].ShippingFee;
                                                                subTotal += vm.Sales[j].TotalAmount;
                                                            }
                                                            subTotal = subTotal - shippingFee;
                                                            NotificationFactory.Notification.save({ Type: 11, ExternalId: resultSaleSaved.clientId, Email: "fake@fake.com" });
                                                            vm.progress = 70;
                                                            vm.progressMessage = "Taking you to the Paypal website, be sure to come back to finish the Sale!";
                                                            createPaypalPayment().then(
                                                                function (data) {
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
                                                }, function (error) {
                                                    //Sales Process failed
                                                    NotificationFactory.Notification.save({ Type: 9, ExternalId: resultSaleSaved.clientId, Email: "fake@fake.com" });
                                                    vm.ActionState = 4;
                                                    vm.progress = 0;
                                                    vm.error = ExceptionFactory.Handle(error.data);
                                                    vm.error.Message = "Oops! Sorry but we were not able to continue with your order. You don't need to place the order again, a Fundraising Expert will contact you soon.";
                                                    vm.scope.$storage.ShoppingCart = null;
                                                });
                                            },
                                            function (error) {
                                                //Sales Creation failed
                                                var extraData = "Products attempted to be purchased: ";
                                                for (var j = 0; j < vm.scope.$storage.ShoppingCart.Items.length; j++) {
                                                    extraData += vm.scope.$storage.ShoppingCart.Items[j].Product.Name + " (" + vm.scope.$storage.ShoppingCart.Items[j].Quantity + "), ";
                                                }
                                                NotificationFactory.Notification.save({ Type: 8, ExternalId: lead.Id, Email: "fake@fake.com", ExtraData: extraData });
                                                vm.ActionState = 4;
                                                vm.progress = 0;
                                                vm.error = ExceptionFactory.Handle(error.data);
                                                vm.error.Message = "Oops! Sorry but we were not able to place your order. You don't need to place the order again, a Fundraising Expert will contact you soon.";
                                                vm.scope.$storage.ShoppingCart = null;
                                            });
                                    },
                                    function (error) {
                                        //Lead Creation failed
                                        vm.ActionState = 3;
                                        vm.progress = 0;
                                        vm.error = ExceptionFactory.Handle(error.data);
                                        vm.error.Message = "Sorry but we were not able to save your information. We're fixing the problem, please come back in a few minutes.";
                                    });
                    }, function (error) {
                        //User canceled the Address Hygiene, no error here
                        vm.ActionState = 0;
                        vm.progress = 0;
                    }
                       ), //}//);
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
        /*
         * Searches for the Promotion Code and if it finds it, it runs the Business Rules to add it or reject it
         */
        vm.SearchPromotionCode = function () {
            SalesFactory.PromotionCode.get({ code: vm.Sale.PromotionCode.Code }, { isArray: false }).$promise.then(
                function (promotionCodeFound) {
                    validatePromotionCode(promotionCodeFound).then(
                        function (result) {
                            vm.Sale.PromotionCode = promotionCodeFound;
                            vm.Sale.PromotionCodeId = promotionCodeFound.Id;
                            $rootScope.$broadcast("promotionCodeChanged", promotionCodeFound);
                        }, function (error) {

                        }
                    );
                },
                function (error) {
                    vm.PromotionCodeErrorMessage = "Sorry! We can't find a Promotion with that Code.";
                    $log.error(error);
                });
        };
        /*
         * Removes the related Promotion Code
         */
        vm.RemovePromotionCode = function () {
            vm.Sale.PromotionCode = null;
            $rootScope.$broadcast("promotionCodeChanged", null);
        };
        //Validates the returned Promotion Code with the Shopping Cart
        function validatePromotionCode(promotionCode) {
            var deferred = $q.defer();
            var promises = [];
            // Is Enable
            if (!promotionCode.IsEnabled) {
                vm.PromotionCodeErrorMessage = "The Promotion Code is no longer valid.";
                return false;
            }
            // Is Country Valid
            if (promotionCode.Country.Code != vm.Sale.Items[0].Product.CountryCode) {
                vm.PromotionCodeErrorMessage = "The Promotion Code is not valid in your Country. The only valid country is " + promotionCode.Country.Code;
                return false;
            }
            // Is Partner Valid
            if (promotionCode.PartnerScopeType == 2 && promotionCode.Partner.Id != vm.scope.$storage.Partner.Id) {
                vm.PromotionCodeErrorMessage = "The Promotion Code is not valit for this Partner.";
                return false;
            }

            // Is Scope Valid
            if (promotionCode.ScopeType == 2) {
                var productFound = false;
                for (var i = 0; i < vm.Sale.Items.length; i++) {
                    for (var j = 0; j < promotionCode.Products.length; j++) {
                        productFound = productFound || promotionCode.Products[j].ScratchBookId == vm.Sale.Items[i].Product.ScratchBookId;
                    }
                }
                if (!productFound) {
                    vm.PromotionCodeErrorMessage = "The Promotion Code is not valid for the Products you selected.";
                    return false;
                }
            }
            // Is Minimum Quantity Required Valid
            if (promotionCode.MinimumRequirementType == 2) {
                if (promotionCode.ScopeType == 2) {
                    var requirementMet = false;
                    for (var i = 0; i < vm.Sale.Items.length; i++) {
                        for (var k = 0; k < promotionCode.Products.length; k++) {
                            if (promotionCode.Products[k].ScratchBookId == vm.Sale.Items[i].Product.ScratchBookId) {
                                requirementMet = vm.Sale.Items[i].Quantity >= promotionCode.MinimumQuantity;
                            }
                        }

                    }
                    if (!requirementMet) {
                        vm.PromotionCodeErrorMessage = "You don't have the minimum Product's Quantity to use this Promotion Code. The minimum amount is " + promotionCode.MinimumQuantity;
                        return false;
                    }
                } else if (promotionCode.ScopeType == 1) {
                    var quantity = 0;
                    for (var i = 0; i < vm.Sale.Items.length; i++) {
                        quantity += vm.Sale.Items[i].Quantity;
                    }
                    if (quantity < promotionCode.MinimumQuantity) {
                        vm.PromotionCodeErrorMessage = "You don't have the minimum Product's Quantity to use this Promotion Code. The minimum amount is " + promotionCode.MinimumQuantity;
                        return false;
                    }

                }
            }
            // Is Minimum Amount Required Valid
            if (promotionCode.MinimumRequirementType == 3) {
                if (promotionCode.ScopeType == 2) {
                    for (var i = 0; i < vm.Sale.Items.length; i++) {
                        for (var l = 0; l < promotionCode.Products.length; l++) {
                            if (promotionCode.Products[l].ScratchBookId == vm.Sale.Items[i].Product.ScratchBookId) {
                                if ((vm.Sale.Items[i].Product.CalculatedPrice * vm.Sale.Items[i].Quantity) < promotionCode.MinimumAmount) {
                                    vm.PromotionCodeErrorMessage = "You don't have the minimum Product's Amount to use this Promotion Code. The minimum amount is " + promotionCode.MinimumAmount;
                                    return false;
                                }
                            }
                        }

                    }

                } else if (promotionCode.ScopeType == 1) {
                    var subTotal = 0;
                    for (var i = 0; i < vm.Sale.Items.length; i++) {
                        subTotal += vm.Sale.Items[i].Product.CalculatedPrice * vm.Sale.Items[i].Quantity;
                    }
                    if (promotionCode.MinimumAmount > subTotal) {
                        vm.PromotionCodeErrorMessage = "You don't have the minimum Amount to use this Promotion Code.The minimum amount is " + promotionCode.MinimumAmount;
                        return false;
                    }
                }
            }
            // Is Limit Valid
            if (promotionCode.LimitType == 2 || promotionCode.LimitType == 3) {
                promises.push(SalesFactory.PromotionCode.get({ id: promotionCode.Id, isLimitValid: true }, { isArray: false }).$promise);
            }
            $q.all(promises).then(
                function (data) {
                    deferred.resolve(data);
                    vm.PromotionCodeErrorMessage = null;
                    return true;
                },
                function (error) {
                    deferred.reject(error);
                    vm.PromotionCodeErrorMessage = "The Promotion Code has reached its limit amount.";
                    return false;

                });
            return deferred.promise;
        };
        //Takes the User to the Confirmation page
        function gotoConfirmationPage(clientId) {
            vm.progress = 90;
            vm.progressMessage = "Taking you to the Confirmation page, just one more second...";
            vm.scope.$storage.ShoppingCart = null;
            $timeout(function () {
                $window.location.href = "/shopping-cart/order-confirmation/" + clientId;
            },
                2000,
                false);

        };
        /*
        * Updates the Shopping Cart to Closed since the Sale was confirmed.
        */
        function confirmShoppingCart(clientId) {
            vm.scope.$storage.ShoppingCart.Status = 2;
            vm.scope.$storage.ShoppingCart.ClientId = clientId;
            return ShoppingCartFactory.ShoppingCart.save(vm.scope.$storage.ShoppingCart).$promise;
        };
        /*
        * Confirms the Sale as Paid
        */
        function confirmSales() {
            var deferred = $q.defer();
            var promises = [];
            var GARepNoAutoConfirmSalesZips = [
                "60153", "60922", "60407", "60927", "60408", "60942", "60416", "60949", "60419", "61701", "60420", "61704", "61356",
                "60424", "61705", "60437", "61727", "60444", "61732", "60447", "61736", "60450", "61748", "60460", "61752", "61313",
                "60470", "61756", "60474", "61761", "60518", "61774", "60531", "61790", "60549", "61801", "60551", "61802", "60557",
                "61812", "60563", "61813", "60920", "61814", "60921", "61817", "60929", "61818", "60934", "61820", "60936", "61821",
                "60953", "60957", "61822", "60959", "61832", "60966", "61834", "60968", "61839", "60970", "61839", "61201", "61842",
                "61204", "61843", "61231", "61844", "61232", "61846", "61233", "61847", "61234", "61849", "61235", "61853", "61236",
                "61854", "61237", "61856", "61238", "61858", "61239", "61859", "61240", "61864", "61241", "61865", "61242", "61866",
                "61244", "61866", "61254", "61871", "61256", "61873", "61257", "61874", "61258", "61876", "61259", "61878", "61260",
                "61880", "61262", "61882", "61263", "61883", "61264", "61884", "61265", "61910", "61266", "61911", "61272", "61912",
                "61273", "61913", "61274", "61914", "61275", "61920", "61276", "61924", "61278", "61931", "61279", "61932", "61281",
                "61933", "61282", "61937", "61284", "61938", "61299", "61942", "61301", "61943", "61311", "61944", "61312", "61951",
                "61953", "61314", "61956", "61315", "61957", "61316", "62401", "61317", "62411", "61319", "62441", "61320", "62447",
                "61321", "62448", "61322", "62450", "61323", "62454", "61325", "62459", "61326", "62465", "61327", "62467", "61328",
                "62468", "61329", "62501", "61330", "62513", "61332", "62521", "61333", "62522", "61334", "62522", "61335", "62523",
                "61336", "62526", "61337", "62534", "61338", "62535", "61340", "62540", "61341", "62544", "61342", "62546", "61344",
                "62549", "61345", "62550", "61346", "62551", "61348", "62554", "61349", "62557", "61350", "62565", "61354", "62568",
                "62571", "61358", "62573", "61359", "62868", "61360", "61361", "61362", "61363", "61364", "61368", "61369", "61370",
                "61371", "61372", "61373", "61374", "61375", "61376", "61377", "61379", "61412", "61413", "61414", "61418", "61419",
                "61421", "61422", "61424", "61425", "61426", "61434", "61437", "61442", "61443", "61449", "61451", "61454", "61460", "61462",
                "61465", "61466", "61467", "61468", "61469", "61471", "61476", "61479", "61480", "61483", "61486", "61488", "61490", "61491",
                "61501", "61516", "61517", "61523", "61525", "61526", "61528", "61529", "61530", "61531", "61532", "61533", "61534", "61535",
                "61536", "61537", "61539", "61540", "61541", "61542", "61545", "61546", "61547", "61548", "61550", "61551", "61552", "61554",
                "61555", "61558", "61559", "61560", "61561", "61562", "61564", "61565", "61567", "61568", "61569", "61570", "61571", "61601",
                "61602", "61603", "61604", "61605", "61606", "61607", "61610", "61611", "61612", "61613", "61614", "61615", "61616", "61625",
                "61629", "61630", "61633", "61634", "61635", "61636", "61637", "61638", "61639", "61641", "61643", "61650", "61651", "61652",
                "61653", "61654", "61655", "61656", "61721", "61729", "61733", "61734", "61738", "61739", "61740", "61741", "61742", "61743",
                "61744", "61747", "61755", "61759", "61760", "61764", "61769", "61771", "61775", "62022", "62031", "62037", "62052", "62301",
                "62305", "62306", "62320", "62324", "62325", "62338", "62339", "62346", "62347", "62348", "62349", "62351", "62359", "62360",
                "62365", "62376", "62601", "62617", "62618", "62631", "62633", "62635", "62638", "62644", "62650", "62655", "62664",
                "62682", "62692"];
            for (var j = 0; j < vm.Sales.length; j++) {
                var sale = vm.Sales[j];
                var now = new Date();
                //sale.Confirmed = now.toISOString().slice(0, 19).replace('T', ' ');

                if (GARepNoAutoConfirmSalesZips.indexOf(sale.Client.Addresses[0].PostCode) > -1) {
                    sale.Status = 6; //on hold

                }
                else {

                    sale.Confirmed = now.toISOString().slice(0, 19).replace('T', ' ');
                    now.setDate(now.getDate() + 9);
                    sale.ScheduledDelivery = now.toISOString().slice(0, 19).replace('T', ' ');
                    sale.Status = 2;

                }
                sale.ARStatus = 20;
                promises.push(SalesFactory.Sale.update(sale).$promise);
            }
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
            var totalAmount = 0;
            var reference = "";
            
            for (var j = 0; j < vm.Sales.length; j++) {
                var sale = vm.Sales[j];
                totalAmount += sale.TotalAmount;
                reference += sale.Id + ",";
                

            }
            var creditCard = { Email: vm.Sale.Client.Email, Number: vm.Sale.CreditCard.Number, Holder: vm.Sale.CreditCard.Holder, ExpirationDate: vm.Sale.CreditCard.ExpirationDate, CVV: vm.Sale.CreditCard.CVV, InternalPaymentMethod: vm.Sale.InternalPaymentMethod, Amount: totalAmount, Reference: reference, Address: { Address1: vm.Sale.Client.Addresses[0].Address1, City: vm.Sale.Client.Addresses[0].City, Region: vm.Sale.Client.Addresses[0].Region, Country: vm.Sale.Client.Addresses[0].Country, PostCode: vm.Sale.Client.Addresses[0].PostCode } };
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
        function applyPayments(authorizationCode) {
            var deferred = $q.defer();
            var promises = [];
            for (var j = 0; j < vm.Sales.length; j++) {
                var sale = vm.Sales[j];
                var payment = {
                    SaleId: sale.Id,
                    Number: 1,
                    InternalPaymentMethod: vm.Sale.InternalPaymentMethod,
                    CollectionStatusId: 8,
                    Amount: sale.TotalAmount,
                    IsComissionPaid: false,
                    Status: 1,
                    AuthorizationNumber: authorizationCode,
                    CreditCard: {
                        Number: vm.Sale.CreditCard.Number,
                        Holder: vm.Sale.CreditCard.Holder,
                        ExpirationDate: vm.Sale.CreditCard.ExpirationDate,
                        CVV: vm.Sale.CreditCard.CVV,
                        InternalPaymentMethod: vm.Sale.InternalPaymentMethod,
                        Amount: 0,
                        Address: { Address1: vm.Sale.Client.Addresses[0].Address1, City: vm.Sale.Client.Addresses[0].City, Region: vm.Sale.Client.Addresses[0].Region, Country: vm.Sale.Client.Addresses[0].Country, PostCode: vm.Sale.Client.Addresses[0].PostCode }
                    }
                }
                promises.push(SalesFactory.Payment.save(payment).$promise);
            }
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
    SalesController.$inject = ["$scope", "ShoppingCartFactory", "SalesFactory", "$log", "AddressHygieneFactory", "MasksConstants", "ExceptionFactory", "$modal", "LeadsFactory", "NotificationFactory", "$q", "$window", "hosts", "$timeout", "GoogleAnalyticsService", "$rootScope", "$localStorage"];
    module.controller("SalesController", SalesController);

    function ConfirmationController($scope, ShoppingCartFactory, SalesFactory, $log, $location, ProductsFactory, $localStorage) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        delete vm.scope.$storage.ShoppingCart;
        vm.scope.$storage.ShoppingCart = { Id: 0, AnonymousId: "0", Items: [] };
        var path = $location.path().split('/');
        var clientId = path[path.length - 1];
        


        vm.ShoppingCart = ShoppingCartFactory.ShoppingCart.get({ clientId: clientId }, { isArray: false, cache: false }).$promise.then(function (shoppingCart) {
            vm.ShoppingCart = shoppingCart;
            vm.TotalAmount = 0;
            vm.SubTotal = 0;
            vm.ShippingFee = 0;
            vm.Taxes = [];
            var productsCategoriesShipping = [];
            var validateCategoryShipping;
            var billingRegion = vm.ShoppingCart.Client.Addresses[0].Region;
            for (var i = 0; i < vm.ShoppingCart.Items.length; i++) {
                ProductsFactory.Product.UpdatePrice(vm.ShoppingCart.Items[i].Product, vm.ShoppingCart.Items[i].Quantity);
                
                vm.SubTotal += vm.ShoppingCart.Items[i].Product.CalculatedPrice * vm.ShoppingCart.Items[i].Quantity;
                ProductsFactory.Product.UpdateShippingFee(vm.ShoppingCart.Items[i].Product, vm.SubTotal);
                if (vm.ShoppingCart.PromotionCode != null && vm.ShoppingCart.PromotionCode != undefined && vm.ShoppingCart.PromotionCode.ScopeType == 2) {
                    if (vm.ShoppingCart.PromotionCode.DiscountType == 1) {
                        vm.ShoppingCart.PromotionCode.DiscountedAmount = vm.ShoppingCart.PromotionCode.AmountDiscount;
                    } else if (vm.ShoppingCart.PromotionCode.DiscountType == 2) {
                        vm.ShoppingCart.PromotionCode.DiscountedAmount = (vm.scope.ShoppingCart.Items[i].Product.CalculatedPrice * vm.scope.ShoppingCart.Items[i].Quantity * 1.0) * (vm.ShoppingCart.PromotionCode.PercentageDiscount / 100.0);
                    }
                    else if (vm.ShoppingCart.PromotionCode.DiscountType == 3) {
                        vm.ShoppingCart.PromotionCode.DiscountedAmount += vm.ShoppingCart.Items[i].Product.ShippingFee;
                    }
                    vm.TotalAmount += (vm.ShoppingCart.PromotionCode.DiscountedAmount * -1);
                }

                for (var ss = 0; ss < vm.ShoppingCart.Items.length; ss++) {
                    if (productsCategoriesShipping.length === 0) {
                        // No other product added to shipping
                        vm.ShippingFee += vm.ShoppingCart.Items[i].Product.ShippingFee;
                        productsCategoriesShipping.push(vm.ShoppingCart.Items[i].Product.Category.Id);
                    }
                    else {
                        // Check if there is not other product to ship for the same category
                        validateCategoryShipping = 0;
                        for (var s = 0; s < productsCategoriesShipping.length; s++) {
                            if (productsCategoriesShipping[s] === vm.ShoppingCart.Items[i].Product.Category.Id) {
                                validateCategoryShipping = 1;
                            }
                        }
                        if (validateCategoryShipping === 0) {
                            vm.ShippingFee += vm.ShoppingCart.Items[i].Product.ShippingFee;
                            productsCategoriesShipping.push(vm.ShoppingCart.Items[i].Product.Category.Id);
                        }
                    }
                }


                //vm.ShippingFee += vm.ShoppingCart.Items[i].Product.ShippingFee;
                if (vm.ShoppingCart.Items[i].Product.RequireTaxes) {
                    for (var j = 0; j < vm.ShoppingCart.Items[i].Product.Taxes.length; j++) {
                        if (vm.ShoppingCart.Items[i].Product.Taxes[j].StateCode === billingRegion.Code) {
                            if (vm.Taxes.indexOf(vm.Taxes[vm.ShoppingCart.Items[i].Product.Taxes[j].TaxCode]) === -1) {
                                vm.Taxes.push({ Code: vm.ShoppingCart.Items[i].Product.Taxes[j].TaxCode, Amount: ((vm.ShoppingCart.Items[i].Product.CalculatedPrice * vm.ShoppingCart.Items[i].Quantity) + vm.ShoppingCart.Items[i].Product.ShippingFee) * (vm.ShoppingCart.Items[i].Product.Taxes[j].Rate / 100.0) });
                            } else {
                                vm.Taxes[vm.Taxes.indexOf(vm.Taxes[vm.ShoppingCart.Items[i].Product.Taxes[j].TaxCode])].Amount += ((vm.ShoppingCart.Items[i].Product.CalculatedPrice * vm.ShoppingCart.Items[i].Quantity) + vm.ShoppingCart.Items[i].Product.ShippingFee) * (vm.ShoppingCart.Items[i].Product.Taxes[j].Rate / 100.0);
                            }
                        }
                    }
                }
            }

            for (var k = 0; k < vm.Taxes.length; k++) {
                vm.TotalAmount += vm.Taxes[k].Amount;
            }
            if (vm.ShoppingCart.PromotionCode != null && vm.ShoppingCart.PromotionCode != undefined && vm.ShoppingCart.PromotionCode.ScopeType == 1) {
                if (vm.ShoppingCart.PromotionCode.DiscountType == 1) {
                    vm.ShoppingCart.PromotionCode.DiscountedAmount = vm.ShoppingCart.PromotionCode.AmountDiscount;
                } else if (vm.ShoppingCart.PromotionCode.DiscountType == 2) {
                    vm.ShoppingCart.PromotionCode.DiscountedAmount = (vm.SubTotal * 1.0) * (vm.ShoppingCart.PromotionCode.PercentageDiscount / 100.0);
                }
                vm.TotalAmount += (vm.ShoppingCart.PromotionCode.DiscountedAmount * -1);
            }
            vm.TotalAmount += (vm.SubTotal + vm.ShippingFee);
        });
        vm.Sales = SalesFactory.Sale.query({ clientId: clientId }, { isArray: true, cache: false });
    };
    ConfirmationController.$inject = ["$scope", "ShoppingCartFactory", "SalesFactory", "$log", "$location", "ProductsFactory", "$localStorage"];
    module.controller("ConfirmationController", ConfirmationController);
})();