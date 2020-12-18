(function () {
    "use strict";
    var module = angular.module("ezfund.api");
    function ContentFactory($resource, hosts) {
        return {
            HomePageBanners: $resource(hosts.webApiEzFundBaseUrl + "/banners/"),
            HomePageRotator: $resource(hosts.webApiEzFundBaseUrl + "/rotators/"),
            Testimonial: $resource(hosts.webApiEzFundBaseUrl + "/testimonials/")
        };
    }
    ContentFactory.$inject = ["$resource", "hosts"];
    module.factory("ContentFactory", ContentFactory);

    function LeadsFactory($resource, hosts) {
        return {
            Lead: $resource(hosts.webApiEzFundBaseUrl + "/leads/:id", null, { "update": { method: "PUT" } }),
            Product: $resource(hosts.webApiEzFundBaseUrl + "/productsclass/"),
            SalesStartingDate: $resource(hosts.webApiEzFundBaseUrl + "/salesstartingdate/"),
            Referral: $resource(hosts.webApiEzFundBaseUrl + "/referrals/")
        };
    }
    LeadsFactory.$inject = ["$resource", "hosts"];
    module.factory("LeadsFactory", LeadsFactory);


    function SellingKitsLeadsFactory($resource, hosts) {
        return {
            SellingKitLead: $resource(hosts.webApiEzFundBaseUrl + "/sellingkitleads/:id"),
            //Product: $resource(hosts.webApiEzFundBaseUrl + "/productsclass/"),
            OrganizationType: $resource(hosts.webApiEzFundBaseUrl + "/organizationtype/"),
            PrimaryProgram: $resource(hosts.webApiEzFundBaseUrl + "/primaryprogram/"),
            //Referral: $resource(hosts.webApiEzFundBaseUrl + "/referrals/")
        };
    }
    SellingKitsLeadsFactory.$inject = ["$resource", "hosts"];
    module.factory("SellingKitsLeadsFactory", SellingKitsLeadsFactory);


    function NewsletterSubscriptionFactory($resource, hosts) {
        return {
            NewsletterSubscription: $resource(hosts.webApiEzFundBaseUrl + "/newsletterssubscription/:id"),
            GetSubscriberByMail: $resource(hosts.webApiEzFundBaseUrl + "/newsletterssubscription/"),
        };
    }
    NewsletterSubscriptionFactory.$inject = ["$resource", "hosts"];
    module.factory("NewsletterSubscriptionFactory", NewsletterSubscriptionFactory);



    function ProductsFactory($resource, hosts) {
        return {
            Product: $resource(hosts.webApiEzFundBaseUrl + "/products/"),
            RelatedProducts: $resource(hosts.webApiEzFundBaseUrl + "/products/"),

            UpdatePrice: function (product, quantity) {
                if (!product.IsStackedProduct) {
                    for (var i = 0; i < product.SubProducts.length; i++) {
                        if (typeof product.SubProducts[i].SelectedQuantity !== 'undefined' && product.SubProducts[i].SelectedQuantity > 0) {
                        	if (product.SubProducts[i].Profit.length > 0) {
                        		//There are business rules for prize ranges
                        		//var selectedQty = product.SubProducts[i].SelectedQuantity;
                        		for (var x = 0; x < product.SubProducts[i].Profit.length; x++) {
                        			var range = product.SubProducts[i].Profit[x];
                        			if (range.Min <= quantity && range.Max >= quantity) {
                        				product.CalculatedPrice = range.Price;
                        				break;
                        			}
                        		}
                        	}
                        	else
                        	{
                        		product.CalculatedPrice = product.SubProducts[i].Price;
                        	}
                        }
                    }
                }
                else {
                    product.CalculatedPrice = 0;
                    for (var i = 0; i < product.SubProducts.length; i++) {
                        product.CalculatedPrice += product.SubProducts[i].Price * (typeof product.SubProducts[i].SelectedQuantity === 'undefined' ? 0 : product.SubProducts[i].SelectedQuantity);
                    }
                }
            },
            UpdateShippingFee: function (product, quantity) {
                var shippingFees = product.ShippingFees;
                product.ShippingFee = 0;
                for (var i = 0; i < shippingFees.length; i++) {
                    var shippingFee = shippingFees[i];
                    if (shippingFee.MinimumQuantity <= quantity && shippingFee.MaximumQuantity >= quantity) {
                        product.ShippingFee = shippingFee.Fee;
                        break;
                    }
                }
            }
        };
    }
    ProductsFactory.$inject = ["$resource", "hosts"];
    module.factory("ProductsFactory", ProductsFactory);

    function CategoriesFactory($resource, hosts) {
        return {
            Category: $resource(hosts.webApiEzFundBaseUrl + "/categories/"),
            GetAllCategories: $resource(hosts.webApiEzFundBaseUrl + "/categories/"),
            GetSubCategory: $resource(hosts.webApiEzFundBaseUrl + "/categories/")
        };
    }
    CategoriesFactory.$inject = ["$resource", "hosts"];
    module.factory("CategoriesFactory", CategoriesFactory);

    function BlogFactory($resource, hosts) {
        return {
            GetBlog: $resource(hosts.webApiEzFundBaseUrl + "/blog/"),
            GetBlogCategories: $resource(hosts.webApiEzFundBaseUrl + "/blogCategories/"),
            GetBlogTags: $resource(hosts.webApiEzFundBaseUrl + "/blogTags/")
        };
    }
    BlogFactory.$inject = ["$resource", "hosts"];
    module.factory("BlogFactory", BlogFactory);

    function MetaService() {
        var title = '';
        var metaDescription = '';
        var metaKeywords = '';
        var metaCanonicalUrl = '';
        return {
            set: function (newTitle, newMetaDescription, newKeywords, newCanonicalUrl) {
                metaKeywords = newKeywords;
                metaDescription = newMetaDescription;
                title = newTitle;
                metaCanonicalUrl = newCanonicalUrl;
            },
            //set: function (newTitle, newMetaDescription, newKeywords) {
            //    metaKeywords = newKeywords;
            //    metaDescription = newMetaDescription;
            //    title = newTitle;
            //},
            metaTitle: function () { return title; },
            metaDescription: function () { return metaDescription; },
            metaKeywords: function () { return metaKeywords; },
            metaCanonicalUrl: function () { return metaCanonicalUrl; }
        };
    }
    module.service("MetaService", MetaService);
    /*Begin - Sales Services*/
    function ShoppingCartFactory($resource, hosts) {
        var ShoppingCart = $resource(hosts.webApiEzFundBaseUrl + "/shoppingCarts/:id", null, {
            "update": { method: "PUT" }
        });

        return {
            ShoppingCart: ShoppingCart
        };
    }
    ShoppingCartFactory.$inject = ["$resource", "hosts"];
    module.factory("ShoppingCartFactory", ShoppingCartFactory);

    function SalesFactory($resource, hosts) {
        var Sale = $resource(hosts.webApiEzFundBaseUrl + "/sales/:id", null, {
            "update": { method: "PUT" }
        });
        return {
            Sale: Sale,
            //Paypal: $resource(hosts.webApiCoreBaseUrl + "/paypal/:id"),
            CreditCard: $resource(hosts.webApiEzFundBaseUrl + "/creditcard/:id"),
            Payment: $resource(hosts.webApiEzFundBaseUrl + "/payments/:id"),
            PromotionCode: $resource(hosts.webApiEzFundBaseUrl + "/promotioncodes/:id")
        };
    }
    SalesFactory.$inject = ["$resource", "hosts"];
    module.factory("SalesFactory", SalesFactory);
    /*End - Sales Services*/

    /*Begin - Notification Services*/
    function NotificationFactory($log, $resource, hosts) {
        return {
            Notification: $resource(hosts.webApiEzFundBaseUrl + "/notifications/:id")
        };
    }
    NotificationFactory.$inject = ["$log", "$resource", "hosts"];
    module.factory("NotificationFactory", NotificationFactory);
    /*End - Notification Services*/
})();