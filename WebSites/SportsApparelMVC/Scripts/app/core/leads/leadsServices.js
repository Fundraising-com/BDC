(function () {
    "use strict";
    var module = angular.module("core.leads");

    function LeadsFactory($resource, $log, hosts) {
        return {
            Lead: $resource(hosts.webApiCoreBaseUrl + "/leads/:id")
        }
    }
    LeadsFactory.$inject = ["$resource", "$log", "hosts"];
    module.factory("LeadsFactory", LeadsFactory);


    
})();