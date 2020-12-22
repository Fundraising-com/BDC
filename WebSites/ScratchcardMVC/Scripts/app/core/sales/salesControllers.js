(function () {
   "use strict";
   var module = angular.module("core.sales");

   function ShoppingCartController(ShoppingCartFactory, $scope, $window, $log, $q, ProductsFactory) {
      var vm = this;
      vm.scope = $scope;
      vm.ShoppingCartUrl = "/shopping-cart/index";
      vm.ShippingFee = 0;
      vm.SubTotal = 0;
      vm.TotalAmount = 0;
      vm.Taxes = [];
      vm.ShowShoppingCart = false;
      vm.PromotionCode = {};
      vm.SaleBillingRegion = null;
      if (vm.scope.$storage.ShoppingCart.Id === 0) {
         GetOrCreateShoppingCart();
      } else {
         Calculate();
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
      /**
      * Returns the Shopping Cart by the Anonymous Id or creates one if it doesn't exist.
      **/
      function GetOrCreateShoppingCart() {
         vm.ShowShoppingCart = false;
         ShoppingCartFactory.ShoppingCart.get({ anonymousId: vm.scope.$storage.AnonymousId }, { isArray: false, cache: false }).$promise.then(
            function (data) {
               if (data.Id === undefined) {
                  ShoppingCartFactory.ShoppingCart.save(vm.scope.$storage.ShoppingCart).$promise.then(
                     function (newShoppingCart) {
                        vm.scope.$storage.ShoppingCart = newShoppingCart;
                        Calculate();
                     });
               } else {
                  vm.scope.$storage.ShoppingCart = data;
                  Calculate();
               }
            },
            function(error) {
               $log.error(error);
            });
      };
      /*
      * Calculates the SubTotal and Shipping Fee for the Shopping Cart
      */
      function Calculate() {
         vm.SubTotal = 0;
         vm.ShippingFee = 0;
         vm.TotalAmount = 0;
         vm.Taxes = [];
         var isPromotionCodeUsed = false;
         var productsCountries = [];
         for (var i = 0; i < vm.scope.$storage.ShoppingCart.Items.length; i++) {
            ProductsFactory.Product.UpdatePrice(vm.scope.$storage.ShoppingCart.Items[i].Product, vm.scope.$storage.ShoppingCart.Items[i].Quantity);
            ProductsFactory.Product.UpdateShippingFee(vm.scope.$storage.ShoppingCart.Items[i].Product, vm.scope.$storage.ShoppingCart.Items[i].Quantity);
            vm.SubTotal += vm.scope.$storage.ShoppingCart.Items[i].Product.CalculatedPrice * vm.scope.$storage.ShoppingCart.Items[i].Quantity;
            if (vm.PromotionCode != null && vm.PromotionCode != undefined && vm.PromotionCode.ScopeType == 2 && !isPromotionCodeUsed && vm.PromotionCode.ScratchBookId == vm.scope.$storage.ShoppingCart.Items[i].Product.ScratchBookId) {
               if (vm.PromotionCode.DiscountType == 1) {
                  vm.PromotionCode.DiscountedAmount = vm.PromotionCode.AmountDiscount;
               } else if (vm.PromotionCode.DiscountType == 2) {
                  vm.PromotionCode.DiscountedAmount = (vm.scope.$storage.ShoppingCart.Items[i].Product.CalculatedPrice * vm.scope.$storage.ShoppingCart.Items[i].Quantity * 1.0) * (vm.PromotionCode.PercentageDiscount / 100.0);
               }
               vm.TotalAmount += (vm.PromotionCode.DiscountedAmount * -1);
               isPromotionCodeUsed = true;
            }
            vm.ShippingFee += vm.scope.$storage.ShoppingCart.Items[i].Product.ShippingFee;
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
         if (vm.PromotionCode != null && vm.PromotionCode != undefined && vm.PromotionCode.ScopeType == 1) {
            if (vm.PromotionCode.DiscountType == 1) {
               vm.PromotionCode.DiscountedAmount = vm.PromotionCode.AmountDiscount;
            } else if (vm.PromotionCode.DiscountType == 2) {
               vm.PromotionCode.DiscountedAmount = (vm.SubTotal * 1.0) * (vm.PromotionCode.PercentageDiscount / 100.0);
            }
            vm.TotalAmount += (vm.PromotionCode.DiscountedAmount * -1);
         }
         vm.TotalAmount += (vm.SubTotal + vm.ShippingFee);
         vm.scope.$storage.ShoppingCart.TotalAmount = vm.TotalAmount;
         vm.scope.$storage.ShoppingCart.HasProductsFromDifferentCountries = productsCountries.length > 1;
         vm.ShowShoppingCart = true;
      };
      vm.AddItem = function (product, quantity) {
         vm.scope.$storage.ShoppingCart.Items.push({ Quantity: quantity, ShoppingCartId: vm.scope.$storage.ShoppingCart.Id, ProductId: product.Id });
         ShoppingCartFactory.ShoppingCart.update(vm.scope.$storage.ShoppingCart).$promise.then(function (data) {
            vm.scope.$storage.ShoppingCart = data;
            Calculate();
            $window.location.href = vm.ShoppingCartUrl;
         });

      };
      vm.RemoveItem = function (index) {
         vm.ShowShoppingCart = false;
         vm.scope.$storage.ShoppingCart.Items.splice(index, 1);
         ShoppingCartFactory.ShoppingCart.update(vm.scope.$storage.ShoppingCart).$promise.then(
            function () {
               vm.scope.$storage.ShoppingCart = null; //Nullify the Shopping Cart to foce a full reload.
               GetOrCreateShoppingCart();
            },
            function(error) {
               $log.error(error);
            });
      };
      vm.UpdateItem = function () {
         ShoppingCartFactory.ShoppingCart.update(vm.scope.$storage.ShoppingCart).$promise.then(
            function () {
               Calculate();
            },
            function (error) {
               $log.error(error);
            });
      };
   }
   ShoppingCartController.$inject = ["ShoppingCartFactory", "$scope", "$window", "$log", "$q", "ProductsFactory"];
   module.controller("ShoppingCartController", ShoppingCartController);

   function SalesController($scope, ShoppingCartFactory, SalesFactory, $log, AddressHygieneFactory, MasksConstants, ExceptionFactory, $modal, LeadsFactory, NotificationFactory, $q, $window, hosts, $timeout, GoogleAnalyticsService, $rootScope) {
      var vm = this;
      vm.scope = $scope;
      vm.countries = AddressHygieneFactory.Countries;
      vm.states = AddressHygieneFactory.States.Get(vm.countries[0]);
      vm.ActionState = 0;
      vm.progress = 0;
      vm.progressMessage = "";
      vm.error = {};
      vm.phoneMask = MasksConstants.Phone;
      vm.creditCardNumberMask = MasksConstants.CreditCardNumber;
      vm.creditCardCVVMask = MasksConstants.CreditCardCVV;
      vm.creditCardExpirationDateMask = MasksConstants.CreditCardExpirationDate;
      
      var clientSequenceCode = vm.scope.$storage.PromotionId === 5961 ? "OF" : "IF";
      vm.Sale = {
         Items: [],
         Client: {
            Id: 0,
            DivisionId: 1,
            PromotionId: vm.scope.$storage.PromotionId,
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
      vm.Create = function () {
         vm.ActionState = 1;
         vm.progress = 10;
         vm.progressMessage = "Please verify your Shipping Address";
         var adressVerificationPromises = [];
         adressVerificationPromises.push(AddressHygieneFactory.AddressHygiene.save(vm.Sale.Client.Addresses[0]).$promise);
         adressVerificationPromises.push(AddressHygieneFactory.AddressHygiene.save(vm.Sale.Client.Addresses[1]).$promise);
         $q.all(adressVerificationPromises).then(
            function (suggestedAddresses) {
               var modalScope = $scope.$new(true);
               angular.extend(modalScope, { header: 'Verify your Addresses', addresses: suggestedAddresses, showEditFields: false, states: vm.states });
               var modal = $modal.open({
                  backdrop: false,
                  scope: modalScope,
                  templateUrl: "/Scripts/app/core/templates/addressHygieneModalTemplateMultiple.html",
                  windowTemplateUrl: "/Scripts/app/core/templates/addressHygieneWindowTemplate.html"
               });
               modal.result.then(
                  function (modifiedAddresses) {
                     vm.progress = 15;
                     vm.progressMessage = "Sending your information...";
                     vm.Sale.Client.Addresses[0].Address1 = modifiedAddresses[0].Address1;
                     vm.Sale.Client.Addresses[0].City = modifiedAddresses[0].City;
                     vm.Sale.Client.Addresses[0].Region = modifiedAddresses[0].Region;
                     vm.Sale.Client.Addresses[0].Country = modifiedAddresses[0].Country;
                     vm.Sale.Client.Addresses[0].PostCode = modifiedAddresses[0].PostCode;
                     vm.Sale.Client.Addresses[1].Address1 = modifiedAddresses[1].Address1;
                     vm.Sale.Client.Addresses[1].City = modifiedAddresses[1].City;
                     vm.Sale.Client.Addresses[1].Region = modifiedAddresses[1].Region;
                     vm.Sale.Client.Addresses[1].Country = modifiedAddresses[1].Country;
                     vm.Sale.Client.Addresses[1].PostCode = modifiedAddresses[1].PostCode;
                     LeadsFactory.Lead.save({ Id: 0, RequestType: 2, TellMore: 2, FirstName: vm.Sale.Client.FirstName, LastName: vm.Sale.Client.LastName, Email: vm.Sale.Client.Email, Group: vm.Sale.Client.Organization, Address: vm.Sale.Client.Addresses[0], Website: "-", Phone: vm.Sale.Client.Phone, NumberOfMembers: "0", PromotionId: vm.scope.$storage.PromotionId, PartnerId: vm.scope.$storage.PartnerId, KitType: 42, Comments: vm.Sale.Comments, RepresentativeId: vm.scope.$storage.RepresentativeId, ConsultantId: vm.scope.$storage.ConsultantId, ChannelCode: 'INT' }).$promise.then(
                        function (lead) {
                           vm.progress = 30;
                           vm.progressMessage = "Almost there....";
                           vm.Sale.Client.LeadId = lead.Id;
                           vm.Sale.Client.ChannelCode = lead.ChannelCode;
                           vm.Sale.Client.ConsultantId = lead.ConsultantId;
                           vm.Sale.ConsultantId = lead.ConsultantId;
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
                  });
            },
            function (error) {
               //Address Hygiene Service failed
               vm.ActionState = 3;
               vm.progress = 0;
               vm.error = ExceptionFactory.Handle(error.data);
               vm.error.Message = "Sorry but we can't validate your Shipping Address. We're fixing the problem, please come back in a few minutes.";
            });
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
                  function(result) {
                        vm.Sale.PromotionCode = promotionCodeFound;
                        vm.Sale.PromotionCodeId = promotionCodeFound.Id;
                        $rootScope.$broadcast("promotionCodeChanged", promotionCodeFound);
                  }, function(error) {
                  
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
         if (promotionCode.CountryCode != vm.Sale.Items[0].Product.CountryCode) {
            vm.PromotionCodeErrorMessage = "The Promotion Code is not valid in your Country. The only valid country is " + promotionCode.CountryCode;
            return false;
         }
         // Is Partner Valid
         if (promotionCode.PartnerScopeType == 2 && promotionCode.PartnerId != vm.scope.$storage.PartnerId) {
            vm.PromotionCodeErrorMessage = "The Promotion Code is not valit for this Partner.";
            return false;
         }
         
                  

         // Is Scope Valid
         if (promotionCode.ScopeType == 2) {
            var productFound = false;
            for (var i = 0; i < vm.Sale.Items.length; i++) {
               productFound = productFound || vm.Sale.Items[i].Product.ScratchBookId == promotionCode.ScratchBookId;
            }
            if (!productFound) {
               vm.PromotionCodeErrorMessage = "The Promotion Code is not valid for the Products you selected.";
               return false;
            }
         }
         // Is Minimum Required Valid
         if (promotionCode.MinimumRequirementType == 2) {
            if (promotionCode.ScopeType == 2) {
               var requirementMet = false;
               for (var i = 0; i < vm.Sale.Items.length; i++) {
                  if (promotionCode.ScratchBookId == vm.Sale.Items[i].Product.ScratchBookId) {
                     requirementMet = promotionCode.MinimumQuantity >= vm.Sale.Items[i].Quantity;
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
               if (promotionCode.MinimumQuantity >= quantity) {
                  vm.PromotionCodeErrorMessage = "You don't have the minimum Product's Quantity to use this Promotion Code. The minimum amount is " + promotionCode.MinimumQuantity;
                  return false;
               }
              
            }
         }

         if (promotionCode.MinimumRequirementType == 3) {
            if (promotionCode.ScopeType == 2) {
               for (var i = 0; i < vm.Sale.Items.length; i++) {
                  if (promotionCode.ScratchBookId == vm.Sale.Items[i].Product.ScratchBookId) {
                     if (promotionCode.MinimumAmount > vm.Sale.Items[i].Product.CalculatedPrice * vm.Sale.Items[i].Quantity) {
                        vm.PromotionCodeErrorMessage = "You don't have the minimum Product's Amount to use this Promotion Code. The minimum amount is " + promotionCode.MinimumAmount;
                        return false;
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
         $window.location.href = "/shopping-cart/order-confirmation/" + clientId;
      };
      /*
      * Updates the Shopping Cart to Closed since the Sale was confirmed.
      */
      function confirmShoppingCart(clientId) {
         vm.scope.$storage.ShoppingCart.Status = 2;
         vm.scope.$storage.ShoppingCart.ClientId = clientId;
         return ShoppingCartFactory.ShoppingCart.update(vm.scope.$storage.ShoppingCart).$promise;
      };
      /*
      * Confirms the Sale as Paid
      */
      function confirmSales() {
         var deferred = $q.defer();
         var promises = [];
         for (var j = 0; j < vm.Sales.length; j++) {
            var sale = vm.Sales[j];
            var now = new Date();
            sale.Confirmed = now.toISOString().slice(0, 19).replace('T', ' ');
            now.setDate(now.getDate() + 9);
            sale.ScheduledDelivery = now.toISOString().slice(0, 19).replace('T', ' ');
            sale.Status = 2;
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
            reference += sale.Id + ","
         }
         var creditCard = { Number: vm.Sale.CreditCard.Number, Holder: vm.Sale.CreditCard.Holder, ExpirationDate: vm.Sale.CreditCard.ExpirationDate, CVV: vm.Sale.CreditCard.CVV, InternalPaymentMethod: vm.Sale.InternalPaymentMethod, Amount: totalAmount, Reference: reference, Address: { Address1: vm.Sale.Client.Addresses[0].Address1, City: vm.Sale.Client.Addresses[0].City, Region: vm.Sale.Client.Addresses[0].Region, Country: vm.Sale.Client.Addresses[0].Country, PostCode: vm.Sale.Client.Addresses[0].PostCode } };
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
   SalesController.$inject = ["$scope", "ShoppingCartFactory", "SalesFactory", "$log", "AddressHygieneFactory", "MasksConstants", "ExceptionFactory", "$modal", "LeadsFactory", "NotificationFactory", "$q", "$window", "hosts", "$timeout", "GoogleAnalyticsService", "$rootScope"];
   module.controller("SalesController", SalesController);

   function ConfirmationController($scope, ShoppingCartFactory, SalesFactory, $log, $location, ProductsFactory) {
      var vm = this;
      vm.scope = $scope;
      var path = $location.path().split('/');
      var clientId = path[path.length - 1];
      vm.ShoppingCart = ShoppingCartFactory.ShoppingCart.get({ clientId: clientId }, { isArray: false, cache: false }).$promise.then(function (shoppingCart) {
         vm.ShoppingCart = shoppingCart;
         vm.TotalAmount = 0;
         vm.SubTotal = 0;
         vm.ShippingFee = 0;
         vm.Taxes = [];
         var billingRegion = vm.ShoppingCart.Client.Addresses[0].Region;
         for (var i = 0; i < vm.ShoppingCart.Items.length; i++) {
            ProductsFactory.Product.UpdatePrice(vm.ShoppingCart.Items[i].Product, vm.ShoppingCart.Items[i].Quantity);
            ProductsFactory.Product.UpdateShippingFee(vm.ShoppingCart.Items[i].Product, vm.ShoppingCart.Items[i].Quantity);
            vm.SubTotal += vm.ShoppingCart.Items[i].Product.CalculatedPrice * vm.ShoppingCart.Items[i].Quantity;
            if (vm.ShoppingCart.PromotionCode != null && vm.ShoppingCart.PromotionCode != undefined && vm.ShoppingCart.PromotionCode.ScopeType == 2) {
               if (vm.ShoppingCart.PromotionCode.DiscountType == 1) {
                  vm.ShoppingCart.PromotionCode.DiscountedAmount = vm.ShoppingCart.PromotionCode.AmountDiscount;
               } else if (vm.ShoppingCart.PromotionCode.DiscountType == 2) {
                  vm.ShoppingCart.PromotionCode.DiscountedAmount = (vm.scope.$storage.ShoppingCart.Items[i].Product.CalculatedPrice * vm.scope.$storage.ShoppingCart.Items[i].Quantity * 1.0) * (vm.ShoppingCart.PromotionCode.PercentageDiscount / 100.0);
               }
               vm.TotalAmount += (vm.ShoppingCart.PromotionCode.DiscountedAmount * -1);
            }
            vm.ShippingFee += vm.ShoppingCart.Items[i].Product.ShippingFee;
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
   ConfirmationController.$inject = ["$scope", "ShoppingCartFactory", "SalesFactory", "$log", "$location", "ProductsFactory"];
   module.controller("ConfirmationController", ConfirmationController);
})();