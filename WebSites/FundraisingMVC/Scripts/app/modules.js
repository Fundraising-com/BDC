(function () {
   "use strict";
   var app = angular.module("fundraising", [
   "ngAnimate",
   "ngStorage",
   "ngLocale",
   "ngRoute",
    "ui.directives",
    "ui.filters",
    "ui.bootstrap",
    "ui.mask",
    "angular.filter",
    "core.partners",
    "core.notifications",
    "core.leads",
    "core.representatives",
    "core.helpers",
    "core.sales",
    "fundraising.content",
    "fundraising.categories",
    "fundraising.products"
   ]);
   app.config(["$locationProvider", function ($locationProvider) {
      $locationProvider.html5Mode({ enabled: true, requireBase: false, rewriteLinks: false });
   }]);
   app.run([
      "$localStorage", "$location", "PartnersFactory", "RepresentativesFactory", "$q", "$rootScope",
      function ($localStorage, $location, PartnersFactory, RepresentativesFactory, $q, $rootScope) {
         var partnerPromises = [];

         var urlParameters = $location.search();
         // Partner Id
         var aaid = urlParameters.a_aid;
         if (aaid !== undefined || $localStorage.Partner === undefined) {
            partnerPromises.push(PartnersFactory.Partner
               .get(aaid !== undefined ? { aaid: aaid } : { id: 686 }, { isArray: false, cache: true })
               .$promise);
         }
         $q.all(partnerPromises)
            .then(
               function (data) {
                  if (data.length > 0) {
                     var partner = data[0];
                     $localStorage.Partner = partner;
                     $rootScope.$emit('partnerLoaded');
                  }
                  // Representative Id
                  if ($localStorage.Representative === undefined) {
                     $localStorage.Representative = { Id: 0 };
                  }
                  var representativeId = urlParameters.representativeId;
                  
                  if (representativeId == undefined && $location.path().toLowerCase().indexOf("/representatives/index/") !== -1) {
                     representativeId = $location.path().split('/')[3];
                  }
                  if (representativeId !== undefined) {
                     RepresentativesFactory.Representative.get({ id: representativeId }, { isArray: false, cache: true })
                        .$promise.then(
                           function (representative) {
                              $localStorage.Representative = representative;
                              $rootScope.$emit('representativeLoaded');
                              PartnersFactory.Partner.get({ id: representative.PartnerId }, { isArray: false, cache: true })
                                 .$promise.then(
                                    function (partner) {
                                       $localStorage.Partner = partner;
                                       $rootScope.$emit('partnerLoaded');
                                    },
                                    function (error) {
                                       console.error(error);
                                    });
                              angular.element('#representative-intro-modal-shower').trigger('click');
                           },
                           function (error) {
                              console.error(error);
                              $localStorage.Representative = { Id: 0 };
                              $rootScope.$emit('representativeLoaded');
                           });
                  }
                  // Promotion Id
                  var abid = urlParameters.a_bid;
                  if ($localStorage.Promotion == undefined || abid !== undefined) {
                     PartnersFactory.Promotion.get(abid === undefined
                           ? { partnerId: $localStorage.Partner.Id }
                           : { partnerId: $localStorage.Partner.Id, abid: abid },
                           { isArray: false, cache: false })
                        .$promise.then(
                           function (promotion) {
                              if (promotion != null) {
                                 $localStorage.Promotion = promotion;
                                 $rootScope.$emit('promotionLoaded');
                                 if ($localStorage.Partner.Id !== promotion.PartnerId) {
                                    PartnersFactory.Partner
                                       .get({ id: promotion.PartnerId }, { isArray: false, cache: true })
                                       .$promise.then(
                                          function(partner) {
                                             $localStorage.Partner = partner;
                                             $rootScope.$emit('partnerLoaded');
                                          },
                                          function(error) {
                                             console.warn(error);
                                          });
                                 }
                              } else {
                                 //Promotion PAP Whitelist
                                 var promotionAbidsWhiteList = [
                                    "004bdf10", "01b3e396", "021f071a", "03bc2348", "065d698d", "0745b974", "09aaa823", "0a8cfc6b",
                                    "0b413468", "0bed7c00", "0f5ee715", "0f6ddf5b", "100755bc", "12cc9556", "169b1808", "1724d05c",
                                    "1957bb4a", "19877fd0", "1aa1a4e9", "1c7939ff", "1ce1bd19", "1d15facb", "1f22b255", "1f6facf4",
                                    "21d5542d", "21f3b7ef", "2225d8a6", "2463e711", "25d45b15", "261b8931", "29747ec9", "2aa73f36",
                                    "2aca34fd", "2b22f85c", "2c7a685f", "2c7ec71e", "2cb0583a", "2d1c7d2a", "2da9ff9d", "2e044d39",
                                    "2eb58c10", "2ebac756", "2f54d123", "2f82818d", "314ed41f", "344b5e6a", "36601f0e", "36eac605",
                                    "396c1bdd", "3e361aed", "3ef9f5d7", "42460def", "424a56b7", "42bbc804", "44599dd8", "45102fbc",
                                    "47d71869", "4ae4b669", "4cc8d7db", "4ec75360", "4ed11768", "53dc80a3", "56144bea", "566d6318",
                                    "581d5adf", "5a4b64db", "5a735ac2", "6193f469", "62da524e", "637c652a", "676a7ad5", "6962b2da",
                                    "6afa7e40", "6ba7e670", "6c1765ce", "6ced5b34", "6f41f6fa", "710c1e75", "71a87b8f", "72fe9152",
                                    "73713f67", "73ed3304", "76027381", "768b2493", "76c486f1", "782e601f", "793443fa", "7b1cdf9c",
                                    "7d7a3170", "83f25521", "844150b2", "848a6944", "8581068d", "86091748", "8668aa88", "869ab0af",
                                    "8716f27e", "8764d7a2", "8f19f270", "8fa81d9c", "92489afb", "988a2f3d", "988f51f5", "9895c3ae",
                                    "9a3329e2", "9b49d8f8", "9e7eeb06", "9ebb52ee", "a201fe7f", "a2db299d", "a43ecbbf", "a4dde4f1",
                                    "a62031e8", "a69d4d71", "a7481f84", "a937c091", "aa18383f", "ab97b81d", "adfa4962", "b2149035",
                                    "b225cd2f", "b43fbb57", "b4585ab5", "b75a727d", "b78aa436", "bd3d9e71", "bf6a4b13", "c0a4cc88",
                                    "c0adf7aa", "c1f4df84", "c22bb130", "c2a084c8", "c7fc1040", "c86c8dbc", "ca230e48", "ca51b05c",
                                    "ce75d525", "cf53c1d9", "cf9d6822", "cfae197f", "d501ca6b", "d7061dd6", "da57d3cd", "db164d0f",
                                    "ddda3274", "e14d7a4f", "e585a352", "e779a136", "e7823ff6", "ea63a197", "eafa95a1", "ebe5a3a0",
                                    "ec6b7cad", "ede6445f", "eeaa0da6", "ef3a3765", "f293f561", "f3bddd42", "f68aad73", "fbac0b69",
                                    "fbdf8d9d", "fc548e9c", "fcf1baf8", "fcfafb35", "fd7f7b57", "ffa9b919", "242fc68c", "155bde7c",
                                    "5c51df9d", "8febe118", "05e01d4e"
                                 ];
                                 if (promotionAbidsWhiteList.indexOf(abid) > -1) {
                                    PartnersFactory.Promotion.save({
                                       IsActive: true,
                                       ScriptName: abid,
                                       Name: "Fundraising.com PAP",
                                       PartnerId: $localStorage.Partner.Id,
                                       Type: "PAP"
                                    })
                                       .$promise.then(
                                          function (newPromotion) {
                                             $localStorage.Promotion = newPromotion;
                                             $rootScope.$emit('promotionLoaded');
                                          },
                                          function (error) {
                                             console.error(error);
                                             $localStorage.Promotion = { Id: 5961 };
                                             $rootScope.$emit('promotionLoaded');
                                          });
                                 } else {
                                    $localStorage.Promotion = { Id: 5961 };
                                    $rootScope.$emit('promotionLoaded');
                                 }
                              }
                           },
                           function (error) {
                              console.warn(error);
                              $localStorage.Promotion = { Id: 5961 };
                              $rootScope.$emit('promotionLoaded');
                           });
                  }
               },
               function (error) {
                  console.error(error);
               });
         $rootScope.$on("promotionLoaded",
            function () {
               if ($localStorage.Consultant === undefined) {
                  RepresentativesFactory.Consultant.get({ id: $localStorage.Promotion.Id === 5953 ? 3518 : 3450 },
                        { isArray: false, cache: true })
                     .$promise.then(
                        function (consultant) {
                           $localStorage.Consultant = consultant;
                           $rootScope.$emit('consultantLoaded');
                        },
                        function (error) {
                           console.error(error);
                           $localStorage.Consultant = { Id: 0 };
                           $rootScope.$emit('consultantLoaded');
                        });
               }
            });

         // Shopping Cart
         if ($localStorage.ShoppingCart === undefined) {
            $localStorage.ShoppingCart = { Id: 0, AnonymousId: "0", Items: [] };
         }

      }
   ]);
   app.constant("MasksConstants", {
      Phone: "(999) 999 9999",
      CreditCardNumber: "9999 9999 9999 99?9?9",
       CreditCardCVV: "999",
       CreditCardCVVAmex: "9999",
      CreditCardExpirationDate: "9999",
      USPostCode: "99999",
      CanadaPostCode: "******"
   });
   app.filter('percentage', ["$filter", function ($filter) {
      return function (input, decimals) {
         return $filter('number')(input * 100, decimals) + "%";
      };
   }]);
   app.filter('toCurrencyCode', [function () {
      return function (input) {
         switch (input) {
            case "US":
               return "USD";
            case "CA":
               return "CAD";
            default:
               return "";
         }
      };
   }]);
   app.filter('toDateDifference', [function () {
      return function (input) {
         var miliseconds = Date.now() - new Date(input);
         var difference = "";
         if (miliseconds > 1000 * 3600 * 24 * 365) {
            difference = "More than a year";
         } else if (miliseconds > (1000 * 3600 * 24 * 62)) {
            var months = Math.floor(miliseconds / (1000 * 3600 * 24 * 62));
            difference = months + " month(s)";
         } else if (miliseconds > (1000 * 3600 * 24)) {
            var days = Math.floor(miliseconds / (1000 * 3600 * 24));
            difference = days + " days";
         } else if (miliseconds > (1000 * 3600 * 2)) {
            var hours = Math.floor(miliseconds / (1000 * 3600));
            difference = hours + " hours";
         } else {
            difference = "A moment";
         }
         return difference + " ago";
      };
   }]);
   app.filter('to_trusted', ['$sce', function ($sce) {
      return function (text) {
         return $sce.trustAsHtml(text);
      };
   }]);
   app.filter('makeRange', function () {
      return function (input) {
         var lowBound, highBound;
         switch (input.length) {
            case 1:
               lowBound = 0;
               highBound = parseInt(input[0]) - 1;
               break;
            case 2:
               lowBound = parseInt(input[0]);
               highBound = parseInt(input[1]);
               break;
            default:
               return input;
         }
         var result = [];
         for (var i = lowBound; i <= highBound; i++)
            result.push(i);
         return result;
      };
   });
})();