(function () {
    "use strict";
    var module = angular.module("core.leads");

    function LeadsFactory($resource, $log, hosts) {
        return {
            Lead: $resource(hosts.webApiCoreBaseUrl + "/leads/:id", null, { "update": { method: "PUT" } })
        }
    }
    LeadsFactory.$inject = ["$resource", "$log", "hosts"];
    module.factory("LeadsFactory", LeadsFactory);

    function NewsletterSubscriptionFactory($resource, $log, hosts) {
        return {
            NewsletterSubscription: $resource(hosts.webApiFundraisingBaseUrl + "/NewsletterSubscription/:id")
        }
    }
    NewsletterSubscriptionFactory.$inject = ["$resource", "$log", "hosts"];
    module.factory("NewsletterSubscriptionFactory", NewsletterSubscriptionFactory);
})();
