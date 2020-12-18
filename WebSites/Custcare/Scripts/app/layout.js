angular.module("layoutModule", ["serviceModule"])
       .controller("layoutController", function ($scope) {
           if (document.URL) {
               $scope.isTestEnv = document.URL.indexOf("test") > -1 || document.URL.indexOf("localhost") > -1;
           }
           else {
               $scope.isTestEnv = window.location.href.indexOf("test") > -1 || window.location.href.indexOf("localhost") > -1;
           }
       })
       .factory("dataService", function (mgpDataService) {
           var _isBusy = false, _refreshAccount = false, _refreshMembers = false, _refreshOrders = false, _refreshSponsor = false, _refreshLinks = false;
           var _searchResults = [];
           var _selectedIndex;
           var _event = {}, _sponsor = {}, _account = {}, _orders = [], _participants = [], _supporters = [], _links = {};
           return {
               isBusy: _isBusy,
               refreshAccount: _refreshAccount,
               refreshMembers: _refreshMembers,
               refreshOrders: _refreshOrders,
               refreshSponsor: _refreshSponsor,
               refreshLinks: _refreshLinks,
               searchResults: _searchResults,
               dataLoaded: function () {
                   return _searchResults.length > 0;
               },
               selectedIndex: _selectedIndex,
               event: _event,
               sponsor: _sponsor,
               account: _account,
               orders: _orders,
               participants: _participants,
               supporters: _supporters,
               links: _links,
               getEventById: function (eventId) {
                   return mgpDataService.getEventById(eventId)
                          .then(function (result) {
                              // Succesful
                              angular.copy(result.data, _searchResults);
                              return result;
                          }, function (error) {
                              // Error
                          });
               },
               getEventByLeadId: function (leadId) {
                   return mgpDataService.getEventByLeadId(leadId)
                          .then(function (result) {
                              // Succesful
                              angular.copy(result.data, _searchResults);
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               getEventsByName: function (eventName) {
                   return mgpDataService.getEventsByName(eventName)
                          .then(function (result) {
                              // Succesful
                              angular.copy(result.data, _searchResults);
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               getEventsByEmail: function (email) {
                   return mgpDataService.getEventsByEmail(email)
                          .then(function (result) {
                              // Succesful
                              angular.copy(result.data, _searchResults);
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               getEventsBySponsorName: function (sponsorName) {
                   return mgpDataService.getEventsBySponsorName(sponsorName)
                          .then(function (result) {
                              // Succesful
                              angular.copy(result.data, _searchResults);
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               saveEvent: function (event) {
                   return mgpDataService.saveEvent(event)
                          .then(function (result) {
                              // Succesful
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               getSponsorById: function (eventId) {
                   return mgpDataService.getSponsorById(eventId)
                          .then(function (result) {
                              // Succesful
                              angular.copy(result.data, _sponsor);
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               saveSponsor: function (action, sponsor) {
                   return mgpDataService.saveSponsor(action, sponsor)
                          .then(function (result) {
                              // Succesful
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               getAccountInfoById: function (eventId) {
                   return mgpDataService.getAccountInfoById(eventId)
                          .then(function (result) {
                              // Succesful
                              angular.copy(result.data, _account);
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               saveAccount: function (account, externalGroupOnly) {
                   return mgpDataService.saveAccount(account, externalGroupOnly === undefined ? 0 : externalGroupOnly)
                          .then(function (result) {
                              // Succesful
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               getMembersById: function (action, eventId) {
                   return mgpDataService.getMembersById(action, eventId)
                          .then(function (result) {
                              // Succesful
                              angular.copy(result.data._participants, _participants);
                              angular.copy(result.data._supporters, _supporters);
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               saveMembers: function (movieTicketEpId, members) {
                   return mgpDataService.saveMembers(movieTicketEpId, members)
                          .then(function (result) {
                              // Succesful
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               getOrdersById: function (eventId) {
                   return mgpDataService.getOrdersById(eventId)
                          .then(function (result) {
                              // Succesful
                              angular.copy(result.data, _orders);
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               orderTransfer: function (eventParticipationId, parentMemberHierarchyId) {
                   return mgpDataService.orderTransfer(eventParticipationId, parentMemberHierarchyId)
                          .then(function (result) {
                              // Succesful
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               getLinksInfoByEventParticipationId: function (eventParticipationId) {
                   return mgpDataService.getLinksInfoByEventParticipationId(eventParticipationId)
                          .then(function (result) {
                              // Succesful
                              angular.copy(result.data, _links);
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               saveLinks: function (links) {
                   return mgpDataService.saveLinks(links)
                          .then(function (result) {
                              // Succesful
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },               
               getMemberPasswordByEmail: function (type, email) {
                   return mgpDataService.getMemberPasswordByEmail(type, email)
                          .then(function (result) {
                              // Succesful
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               },
               saveTools: function (type, memberPassword) {
                   return mgpDataService.saveTools(type, memberPassword)
                          .then(function (result) {
                              // Succesful
                              return result;
                          }, function (error) {
                              // Error
                              return error;
                          });
               }
           }
       });