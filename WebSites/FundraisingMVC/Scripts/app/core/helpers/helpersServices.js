(function () {
   "use strict";
   var module = angular.module("core.helpers");

   function GoogleAnalyticsService() {
      return {
         SaleCreated: function (sale) {
            ga('set', '&cu', sale.Items.length > 0 ? sale.Items[0].Product.CurrencyCode : "USD"); // set up currency   

            for (var i = 0; i < sale.Items.length; i++) {
               var item = sale.Items[i];
               ga('ec:addProduct', {
                  'id': item.Product.Id,
                  'name': item.Product.Name,
                  'category': item.Product.Category.Parent.Name,
                  'brand': item.Product.Category.Name,
                  'price': item.UnitPrice,
                  'quantity': item.Quantity
               });
            }
            var taxes = 0;
            for (var j = 0; j < sale.Taxes.length; j++) {
               taxes += sale.Taxes[j].Amount;
            }
            var revenue = sale.TotalAmount - sale.ShippingFee - taxes;
            if (sale.PromotionCode != null && sale.PromotionCode != undefined) {
               revenue += (sale.PromotionCode.DiscountedAmount * -1);
            }
            ga('ec:setAction', 'purchase', {
               'id': sale.Id,
               'affiliation': 'Fundraising.com',
               'revenue': revenue,
               'tax': taxes,
               'shipping': sale.ShippingFee
            });
            ga('send', 'event', 'Sales', 'Purchase', sale.Id);
         },
         ProductRemoved: function (id, name, category, brand, price, quantity) {
            ga('ec:addProduct', {
               'id': id,
               'name': name,
               'category': category,
               'brand': brand,
               'price': price,
               'quantity': quantity
            });
            ga('ec:setAction', 'remove');
            ga('send', 'event', 'Sales', 'remove item from Shopping Cart', id);
         },
         ProductAdded: function (id, name, category, brand, price, quantity) {
            ga('ec:addProduct', {
               'id': id,
               'name': name,
               'category': category,
               'brand': brand,
               'price': price,
               'quantity': quantity
            });
            ga('ec:setAction', 'add');
            ga('send', 'event', 'Sales', 'add item to Shopping Cart', id);
         },
         ProductDetailed: function (id, name, category, brand, price) {
            ga('ec:addProduct', {
               'id': id,
               'name': name,
               'category': category,
               'brand': brand,
               'price': price
            });

            ga('ec:setAction', 'detail');
         },
         BannerClicked: function (id) {
            ga('send', 'event', 'Banners', 'click', id);
         },

         PopUpKitClicked: function () {
             //ga('send', 'pageview', '/request-a-kit-pop-up');
             //ga('send', { 'hitType': 'pageview', 'page': '/request-a-kit-pop-up', 'title': 'Kit pop up clicked' });
             ga('send', { 'hitType': 'pageview', 'page': '/NEW-request-a-kit-pop-up', 'title': 'New Kit pop up clicked' });
         },

         ProductShowed: function (id, name, category, brand, price, position, list) {
            ga('ec:addImpression', {
               'id': id,
               'name': name,
               'category': category,
               'brand': brand,
               'list': list,
               'position': position
            });
            ga('send', 'event', 'Products', 'impression', id);
         },
         ProductClicked: function (id, name, category, brand, price, position, list) {
            ga('ec:addProduct', { 'id': id, 'name': name, 'category': category, 'brand': brand, 'position': position });
            ga('ec:setAction', 'click', { list: list });
            ga('send', 'event', 'Products', 'click', id);
         }
      };
   };
   GoogleAnalyticsService.$inject = [];
   module.factory("GoogleAnalyticsService", GoogleAnalyticsService);

   function AddressHygieneFactory($resource, $log, hosts) {
      return {
         AddressHygiene: $resource(hosts.webApiCoreBaseUrl + "/addresshygiene"),
         States: {
            Get: function (country) {
               if (country.Code === 'CA') {
                  return [{ Code: "AB", Name: "Alberta" }, { Code: "BC", Name: "British Columbia" }, { Code: "MB", Name: "Manitoba" }, { Code: "NB", Name: "New Brunswick" }, { Code: "NF", Name: "Newfoundland and Labrador" }, { Code: "NS", Name: "Nova Scotia" }, { Code: "ON", Name: "Ontario" }, { Code: "PE", Name: "Prince Edward Island" }, { Code: "QC", Name: "Quebec" }, { Code: "SA", Name: "Saskatchewan" }, { Code: "NT", Name: "Northwest Territories" }, { Code: "NU", Name: "Nunavut" }, { Code: "YT", Name: "Yukon" }];
               } else {
                   return [{ Code: "AL", Name: "Alabama" }, { Code: "AK", Name: "Alaska" }, { Code: "AS", Name: "American Samoa" }, { Code: "AZ", Name: "Arizona" }, { Code: "AR", Name: "Arkansas" }, { Code: "CA", Name: "California" }, { Code: "CO", Name: "Colorado" }, { Code: "CT", Name: "Connecticut" }, { Code: "DE", Name: "Delaware" }, { Code: "DC", Name: "District of Columbia" }, { Code: "FM", Name: "Federated States of Micronesia" }, { Code: "FL", Name: "Florida" }, { Code: "GA", Name: "Georgia" }, { Code: "GU", Name: "Guam" }, { Code: "HI", Name: "Hawaii" }, { Code: "ID", Name: "Idaho" }, { Code: "IL", Name: "Illinois" }, { Code: "IN", Name: "Indiana" }, { Code: "IA", Name: "Iowa" }, { Code: "KS", Name: "Kansas" }, { Code: "KY", Name: "Kentucky" }, { Code: "LA", Name: "Louisiana" }, { Code: "ME", Name: "Maine" }, { Code: "MH", Name: "Marshall Islands" }, { Code: "MD", Name: "Maryland" }, { Code: "MA", Name: "Massachusetts" }, { Code: "MI", Name: "Michigan" }, { Code: "MN", Name: "Minnesota" }, { Code: "MS", Name: "Mississippi" }, { Code: "MO", Name: "Missouri" }, { Code: "MT", Name: "Montana" }, { Code: "NE", Name: "Nebraska" }, { Code: "NV", Name: "Nevada" }, { Code: "NH", Name: "New Hampshire" }, { Code: "NJ", Name: "New Jersey" }, { Code: "NM", Name: "New Mexico" }, { Code: "NY", Name: "New York" }, { Code: "NC", Name: "North Carolina" }, { Code: "ND", Name: "North Dakota" }, { Code: "MP", Name: "Northen Mariana Islands" }, { Code: "OH", Name: "Ohio" }, { Code: "OK", Name: "Oklahoma" }, { Code: "OR", Name: "Oregon" }, { Code: "Palau", Name: "PW" }, { Code: "PA", Name: "Pennsylvania" }, { Code: "PR", Name: "Puerto Rico" }, { Code: "RI", Name: "Rhode Island" }, { Code: "SC", Name: "South Carolina" }, { Code: "SD", Name: "South Dakota" }, { Code: "TN", Name: "Tennessee" }, { Code: "TX", Name: "Texas" }, { Code: "UT", Name: "Utah" }, { Code: "VT", Name: "Vermont" }, { Code: "VI", Name: "Virgin Islands" }, { Code: "VA", Name: "Virginia" }, { Code: "WA", Name: "Washington" }, { Code: "WV", Name: "West Virginia" }, { Code: "WI", Name: "Wisconsin" }, { Code: "WY", Name: "Wyoming" }];
               }
            }
         },
         Countries: [{ Name: "United States", Code: "US" }, { Name: "Canada", Code: "CA" }],
         FreeGeoIP: $resource("https://freegeoip.net/json/?callback=")
      }
   };
   AddressHygieneFactory.$inject = ["$resource", "$log", "hosts"];
   module.factory("AddressHygieneFactory", AddressHygieneFactory);

   function ExceptionFactory($log) {
      return {
         Handle: function (err) {
            var error = { Message: "", ModelStateErrors: [] };
            error.Message = ((err.Message !== null && err.Message !== undefined) ? err.Message : err) + ((err.ExceptionMessage !== null && err.ExceptionMessage !== undefined) ? " - " + err.ExceptionMessage : "");
            if (err.ModelStateErrors !== null && err.ModelStateErrors !== undefined) {
               for (var i = 0; i < err.ModelStateErrors.length; i++) {
                  error.ModelStateErrors.push(err.ModelStateErrors[i]);
               }
            }
            $log.error(err);
            return error;
         }
      };
   };
   ExceptionFactory.$inject = ["$log"];
   module.factory("ExceptionFactory", ExceptionFactory);

   function DetectCountryFactory($window) {
      return {
         CountryId: function () {
            return $window.location.href.contains("canada") ? 1 : 0;
         },
         CountryPath: function () {
            return $window.location.href.contains("canada") ? "canada" : "products";
         }
      };
   };
   DetectCountryFactory.$inject = ["$window"];
   module.factory("DetectCountryFactory", DetectCountryFactory);
})();