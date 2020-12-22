angular.module("serviceModule", [])
       .factory("mgpDataService", function ($http, $q) {

            var _getEventById = function (eventId) {
                var deferred = $q.defer();

                $http.get("/api/event/" + eventId + "?type=1")
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _getEventByLeadId = function (leadId) {
                var deferred = $q.defer();

                $http.get("/api/event/" + leadId + "?type=2")
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _getEventsByName = function (eventName) {
                var deferred = $q.defer();

                $http.get("/api/event/" + eventName + "?type=3")
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _getEventsByEmail = function (email) {
                var deferred = $q.defer();

                $http.get("/api/event/" + email + "?type=4")
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _getEventsBySponsorName = function (sponsorName) {
                var deferred = $q.defer();

                $http.get("/api/event/" + sponsorName + "?type=5")
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _saveEvent = function (event) {
                var deferred = $q.defer();

                $http.post("/api/event", event)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _getSponsorById = function (eventId) {
                var deferred = $q.defer();

                $http.get("/api/sponsor/" + eventId)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _saveSponsor = function (action, sponsor) {
                var deferred = $q.defer();

                $http.post("/api/sponsor?action=" + action, sponsor)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _getAccountInfoById = function (eventId) {
                var deferred = $q.defer();

                $http.get("/api/account/" + eventId)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     }, function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _saveAccount = function (account, externalGroupOnly) {
                var deferred = $q.defer();

                $http.post("/api/account?externalGroupOnly=" + externalGroupOnly, account)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _getMembersById = function (action, eventId) {
                var deferred = $q.defer();

                $http.get("/api/members/" + eventId + "?action=" + action)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _saveMembers = function (movieTicketEpId, members) {
                var deferred = $q.defer();

                $http.post("/api/members?movieTicketEpId=" + movieTicketEpId, members)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _getOrdersById = function (eventId) {
                var deferred = $q.defer();

                $http.get("/api/orders/" + eventId + "?type=1")
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };            

            var _orderTransfer = function (eventParticipationId, parentMemberHierarchyId) {
                var deferred = $q.defer();

                $http.post("/api/orders?eventParticipationId=" + eventParticipationId + "&parentMemberHierarchyId=" + parentMemberHierarchyId)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _getLinksInfoByEventParticipationId = function (eventParticipationId) {
                var deferred = $q.defer();

                $http.get("/api/links/" + eventParticipationId)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _saveLinks = function (links) {
                var deferred = $q.defer();

                $http.post("/api/links", links)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _getMemberPasswordByEmail = function (type, email) {
                var deferred = $q.defer();

                $http.get("/api/tools/" + email + "?type=" + type)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            var _saveTools = function (type, memberPassword) {
                var deferred = $q.defer();

                $http.post("/api/tools?type=" + type, memberPassword)
                     .then(function (result) {
                         // Successful
                         deferred.resolve(result);
                     },
                     function (error) {
                         // Error
                         deferred.reject(error);
                     });

                return deferred.promise;
            };

            return {
                getEventById: _getEventById,
                getEventByLeadId: _getEventByLeadId,
                getEventsByName: _getEventsByName,
                getEventsByEmail: _getEventsByEmail,
                getEventsBySponsorName: _getEventsBySponsorName,
                saveEvent: _saveEvent,
                getSponsorById: _getSponsorById,
                saveSponsor: _saveSponsor,
                getAccountInfoById: _getAccountInfoById,
                saveAccount: _saveAccount,
                getMembersById: _getMembersById,
                saveMembers: _saveMembers,
                getOrdersById: _getOrdersById,
                orderTransfer: _orderTransfer,
                getLinksInfoByEventParticipationId: _getLinksInfoByEventParticipationId,
                saveLinks: _saveLinks,
                getMemberPasswordByEmail: _getMemberPasswordByEmail,
                saveTools: _saveTools                
            };
        });