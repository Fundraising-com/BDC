var homeModule = angular.module("homeIndexModule", ["layoutModule", "ui.router"]);

homeModule.config(function ($stateProvider, $httpProvider) {
    $stateProvider.state("index", {
        url: "",
        templateUrl: "/Templates/angular/homeIndex.html"
    });
    $stateProvider.state("results", {
        url: "/results",
        controller: "homeResultsController",
        templateUrl: "/Templates/angular/homeResults.html"
    });
    $stateProvider.state("sponsor", {
        url: "/sponsor",
        controller: "homeSponsorController",
        templateUrl: "/Templates/angular/homeSponsor.html"
    });
    $stateProvider.state("account", {
        url: "/account",
        controller: "homeAccountController",
        templateUrl: "/Templates/angular/homeAccount.html"
    });
    $stateProvider.state("members", {
        url: "/members",
        controller: "homeMembersController",
        templateUrl: "/Templates/angular/homeMembers.html"
    });
    $stateProvider.state("orders", {
        url: "/orders",
        controller: "homeOrdersController",
        templateUrl: "/Templates/angular/homeOrders.html"
    });
    $stateProvider.state("links", {
        url: "/links",
        controller: "homeLinksController",
        templateUrl: "/Templates/angular/homeLinks.html"
    });
    $stateProvider.state("tools", {
        url: "/tools",
        controller: "homeToolsController",
        templateUrl: "/Templates/angular/homeTools.html"
    });

    $httpProvider.interceptors.push(function ($rootScope) {
        return {
            request: function (config) {
                if (config.url.indexOf("/api/") > -1) {
                    $rootScope.$broadcast("loading:show");
                }
                return config;
            },
            response: function (response) {
                if (response.config.url.indexOf("/api/") > -1) {
                    $rootScope.$broadcast("loading:hide");
                }
                return response;
            }
        }
    });
});

homeModule.run(function ($rootScope, dataService) {
    $rootScope.$on("loading:show", function () {
        dataService.isBusy = true;
    });
    $rootScope.$on("loading:hide", function () {
        dataService.isBusy = false;
    });
});

homeModule.controller("tabsController", function ($scope, dataService, $state) {
    $scope.isBusy = function () {
        if (dataService.isBusy)
            return true;
        else
            return false;
    };

    $scope.dataLoaded = function () {
        return dataService.dataLoaded();
    };

    $scope.changeTab = function ($event) {
        if ($event.target.id == "results-tab") {
            $state.transitionTo("results");
        }
        else if ($event.target.id == "sponsors-tab") {
            $state.transitionTo("sponsor");
        }
        else if ($event.target.id == "account-tab") {
            $state.transitionTo("account");
        }
        else if ($event.target.id == "members-tab") {
            $state.transitionTo("members");
        }
        else if ($event.target.id == "orders-tab") {
            $state.transitionTo("orders");
        }
        else if ($event.target.id == "links-tab") {
            $state.transitionTo("links");
        }
        else if ($event.target.id == "tools-tab") {
            $state.transitionTo("tools");
        }
    };
});

homeModule.controller("homeIndexController", function ($scope, $state, dataService) {
    $scope.eventId;
    $scope.eventName;
    $scope.email;
    $scope.memberName;
    $scope.leadId;

    function isInt(value) {
        return !isNaN(value) &&
               parseInt(Number(value)) == value &&
               !isNaN(parseInt(value, 10));
    }
    function validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    $scope.loadEventById = function () {
        if (!isInt($scope.eventId)) {
            alert('Please provide a valid value for Event Id');
            return false;
        }
        $('#myTab a[id="home-tab"]').tab('show');
        $state.transitionTo("index");
        dataService.getEventById($scope.eventId)
                   .then(function (result) {
                       if (result.status === 200) {
                           $('#myTab a[id="results-tab"]').tab('show');
                           $state.transitionTo("results");
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning data!");
                       }
                   });
    };

    $scope.loadEventByLeadId = function () {
        if (!isInt($scope.leadId)) {
            alert('Please provide a valid value for Lead Id');
            return false;
        }
        $('#myTab a[id="home-tab"]').tab('show');
        $state.transitionTo("index");
        dataService.getEventByLeadId($scope.leadId)
                   .then(function (result) {
                       if (result.status === 200) {
                           $('#myTab a[id="results-tab"]').tab('show');
                           $state.transitionTo("results");
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning data!");
                       }
                   });
    };

    $scope.loadEventsByName = function () {
        if ($scope.eventName.length < 3) {
            alert('Please enter at least 3 characters for Event Name');
            return false;
        }
        $('#myTab a[id="home-tab"]').tab('show');
        $state.transitionTo("index");
        var enc_txt = encodeURIComponent($scope.eventName) + '/'; // Add a trailing slash to account for periods
        dataService.getEventsByName(enc_txt)
                   .then(function (result) {
                       if (result.status === 200) {
                           $('#myTab a[id="results-tab"]').tab('show');
                           $state.transitionTo("results");
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning data!");
                       }
                   });
    };

    $scope.loadEventsByEmail = function () {
        if (!validateEmail($scope.email)) {
            alert('Please provide a valid email address');
            return false;
        }
        $('#myTab a[id="home-tab"]').tab('show');
        $state.transitionTo("index");
        var enc_txt = encodeURIComponent($scope.email) + '/'; // Add a trailing slash to account for periods
        dataService.getEventsByEmail(enc_txt)
                   .then(function (result) {
                       if (result.status === 200) {
                           $('#myTab a[id="results-tab"]').tab('show');
                           $state.transitionTo("results");
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning data!");
                       }
                   });
    };

    $scope.loadEventsBySponsorName = function () {
        if ($scope.memberName.length < 3) {
            alert('Please enter at least 3 characters for Sponsor Name');
            return false;
        }
        $('#myTab a[id="home-tab"]').tab('show');
        $state.transitionTo("index");
        var enc_txt = encodeURIComponent($scope.memberName) + '/'; // Add a trailing slash to account for periods
        dataService.getEventsBySponsorName(enc_txt)
                   .then(function (result) {
                       if (result.status === 200) {
                           $('#myTab a[id="results-tab"]').tab('show');
                           $state.transitionTo("results");
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning data!");
                       }
                   });
    };
});

homeModule.controller("homeResultsController", function ($scope, dataService) {
    $scope.data = dataService.searchResults;
    $scope.openEvent = function (index) {
        dataService.selectedIndex = index;
        dataService.event = dataService.searchResults[index];
        dataService.refreshAccount = true;
        dataService.refreshMembers = true;
        dataService.refreshOrders = true;
        dataService.refreshSponsor = true;
        dataService.refreshLinks = true;
        $('#myTab a[id="sponsors-tab"]').tab('show');
    };
});

homeModule.controller("homeSponsorController", function ($scope, $sce, $state, dataService) {
    if (dataService.selectedIndex === undefined)
        return;

    // Lazy load payment info, shipping, sales and partner info
    if (dataService.refreshSponsor) {
        dataService.refreshSponsor = false;
        dataService.getSponsorById(dataService.event.eventId)
                   .then(function (result) {
                       if (result.status === 200) {
                           $scope.sponsor = result.data;
                           $scope.commentsView = $sce.trustAsHtml($scope.sponsor.comments);
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning sponsor info!");
                       }
                   });
    }
    else {
        $scope.sponsor = dataService.sponsor;
        $scope.commentsView = $sce.trustAsHtml($scope.sponsor.comments);
    }

    $scope.issueNewMovieTicket = function () {
        var resp = confirm("Are you sure want to issue a new ticket?");
        if (resp == true) {
            $scope.saveSponsor('MOVIE');
        } else {
            $('#movieTicketModal').modal('hide');
        }
    };

    $scope.saveSponsor = function (action) {
        dataService.saveSponsor(action, $scope.sponsor)
                   .then(function (result) {
                       if (result.status === 200) {
                           if (result.data) {
                               if (result.data.earnedPrize &&
                                   result.data.earnedPrize.newMovieCode) {
                                   $scope.sponsor.movieTicket = 'Yes';
                                   $scope.sponsor.earnedPrize = result.data.earnedPrize;
                               }
                               else {
                                   $scope.sponsor.comments = result.data.comments;
                                   $scope.commentsView = $sce.trustAsHtml($scope.sponsor.comments);
                                   alert("Succesfully saved!");
                               }
                           }
                       }
                       else {
                           dataService.isBusy = false;
                           alert(result.data);
                       }
                   });
    };

    $scope.refresh = function () {
        dataService.refreshSponsor = true;
        $state.transitionTo($state.current, $state.params, { reload: true, inherit: false, notify: true });
    }
});

homeModule.controller("homeAccountController", function ($scope, $state, dataService) {
    if (dataService.selectedIndex === undefined)
        return;

    $scope.eventTypes = [{ name: "Group Fundraiser w/ Subpage" },
                         { name: "Group Fundraiser w/ out Subpage" },
                         { name: "Individual Fundraiser" },
                         { name: "Coupon Groups" }];
    $scope.activeState = [{ name: "No" },
                          { name: "Yes" }];

    $scope.data = dataService.event;

    // Lazy load payment info, shipping, sales and partner info
    if (dataService.refreshAccount) {
        dataService.refreshAccount = false;
        dataService.getAccountInfoById($scope.data.eventId)
                   .then(function (result) {
                       if (result.status === 200) {
                           if (result.data) {
                               $scope.account = result.data;
                               if ($scope.account.shippingAddress && $scope.account.shippingAddress.subdivisionCode) {
                                   $scope.selectedShippingState = $scope.account.shippingStates[findStatesIndex($scope.account.shippingStates, $scope.account.shippingAddress.subdivisionCode)];
                               }
                           }
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning shipping address!");
                       }
                   });
    }
    else {
        $scope.account = dataService.account;
        $scope.selectedShippingState = $scope.account.shippingStates[findStatesIndex($scope.account.shippingStates, $scope.account.shippingAddress.subdivisionCode)];
    }

    if ($scope.data.eventTypeId == 1) {
        $scope.selectedEventType = $scope.eventTypes[0];
    }
    else if ($scope.data.eventTypeId == 2) {
        $scope.selectedEventType = $scope.eventTypes[1];
    }
    else if ($scope.data.eventTypeId == 3) {
        $scope.selectedEventType = $scope.eventTypes[2];
    }
    else if ($scope.data.eventTypeId == 4) {
        $scope.selectedEventType = $scope.eventTypes[3];
    }

    if ($scope.data.active) {
        $scope.selectedActive = $scope.activeState[0];
    }
    else {
        $scope.selectedActive = $scope.activeState[1];
    }

    $scope.saveEvent = function () {
        $scope.data.eventTypeId = findArrayIndexWithNameProperty($scope.eventTypes, $scope.selectedEventType.name, false);
        $scope.data.active = findArrayIndexWithNameProperty($scope.activeState, $scope.selectedActive.name, true) == 0 ? true : false;
        dataService.saveEvent($scope.data)
                   .then(function (result) {
                       if (result.status === 200) {
                           if (result.data) {
                               $scope.data = result.data;
                               dataService.event.endDate = result.data.endDate;
                               alert("Succesfully saved!");
                           }
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error saving event!");
                       }
                   });
    }

    $scope.saveAccount = function () {
        if ($scope.selectedShippingState) {
            $scope.account.shippingAddress.subdivisionCode = $scope.selectedShippingState.subdivisionCode;
        }
        else {
            alert("Please enter Shipping State");
            return false;
        }
        var country = $scope.account.shippingAddress.countryCode.toLowerCase();
        if (country === "usa" || country === "united states of america" || country === "united states")
            $scope.account.shippingAddress.countryCode = "US";
        else if (country === "canada")
            $scope.$account.shippingAddress.countryCode = "CAN";
        dataService.saveAccount($scope.account)
                   .then(function (result) {
                       if (result.status === 200) {
                           alert("Succesfully saved!");
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error saving account!");
                       }
                   });
    }

    $scope.saveExternalGroup = function () {
        dataService.saveAccount($scope.account, 1)
                   .then(function (result) {
                       if (result.status === 200) {
                           if (result.data) {
                               $scope.account.linkToEventId = '';
                               $scope.account.group.externalGroupId = result.data;
                               alert("Succesfully saved!");
                           }
                       }
                       else {
                           dataService.isBusy = false;
                           if (result.data)
                               alert(result.data);
                           else
                               alert("Error saving external group id!");
                       }
                   });
    }

    function findStatesIndex(array, obj) {
        var index = 0;
        $.each(array, function (key, value) {
            if (obj == value.subdivisionCode) {
                index = key;
                return false;
            }
        });
        return index;
    }
    function findArrayIndexWithNameProperty(array, obj, zeroBased) {
        var index = 0;
        $.each(array, function (key, value) {
            if (obj == value.name) {
                index = key + (!zeroBased ? 1 : 0);
                return false;
            }
        });
        return index;
    }

    $scope.refresh = function () {
        dataService.refreshAccount = true;
        $state.transitionTo($state.current, $state.params, { reload: true, inherit: false, notify: true });
    }
});

homeModule.controller("homeMembersController", function ($scope, $filter, $state, dataService) {
    if (dataService.selectedIndex === undefined)
        return;
    
    $scope.selectedParticipant = 'All';
    $scope.selectParticipant = function (mhId, name) {
        $scope.selectedParticipant = name;        
        $scope.filteredItems = [];
        var log = [];

        angular.forEach($scope.supporters, function (value, key) {
            if (value.parentMemberHierarchyId === mhId)
                $scope.filteredItems.push(value);
        }, log);

        // Take care of the sorting order
        if ($scope.suppSortingOrder !== '') {
            $scope.filteredItems = $filter('orderBy')($scope.filteredItems, $scope.suppSortingOrder, $scope.suppReverse);
        }
        $scope.currentSuppPage = 0;

        $scope.groupToPages('supp');
    }

    // Init paging
    // Paging logic courtesy of http://jsfiddle.net/SAWsA/11/
    $scope.partSortingOrder = 'name';
    $scope.suppSortingOrder = 'name';
    $scope.partReverse = false;
    $scope.suppReverse = false;
    $scope.itemsPerPage = 10;
    $scope.filteredItems = [];
    $scope.partPagedItems = [];
    $scope.suppPagedItems = [];
    $scope.currentPartPage = 0;
    $scope.currentSuppPage = 0;

    var searchMatch = function (haystack, needle) {
        if (!needle) {
            return true;
        }
        if (!haystack) {
            return false;
        }
        return haystack.toString().toLowerCase().indexOf(needle.toLowerCase()) !== -1;
    };

    // Init the filtered items
    $scope.search = function (type) {
        var temp = [];
        var query = '';
        $scope.selectedParticipant = 'All';
        if (type == 'part') {
            temp = $scope.participants;
            query = $scope.queryPart;
        }            
        else {
            temp = $scope.supporters;
            query = $scope.querySupp;
        }

        $scope.filteredItems = $filter('filter')(temp, function (item) {
            for (var attr in item) {
                if (searchMatch(item[attr], query))
                    return true;
            }
            return false;
        });

        // Take care of the sorting order
        if (type == 'part') {
            if ($scope.partSortingOrder !== '') {
                $scope.filteredItems = $filter('orderBy')($scope.filteredItems, $scope.partSortingOrder, $scope.partReverse);
            }
            $scope.currentPartPage = 0;
        }
        else {
            if ($scope.suppSortingOrder !== '') {
                $scope.filteredItems = $filter('orderBy')($scope.filteredItems, $scope.suppSortingOrder, $scope.suppReverse);
            }
            $scope.currentSuppPage = 0;
        }
        
        $scope.groupToPages(type);
    };

    // Change sorting order
    $scope.partSortBy = function (newSortingOrder) {
        if ($scope.partSortingOrder == newSortingOrder)
            $scope.partReverse = !$scope.partReverse;

        $scope.partSortingOrder = newSortingOrder;
    };
    $scope.suppSortBy = function (newSortingOrder) {
        if ($scope.suppSortingOrder == newSortingOrder)
            $scope.suppReverse = !$scope.suppReverse;

        $scope.suppSortingOrder = newSortingOrder;
    };

    // Paging functions
    $scope.groupToPages = function (type) {
        var pagedItems = [];

        for (var i = 0; i < $scope.filteredItems.length; i++) {
            if (i % $scope.itemsPerPage === 0) {
                pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.filteredItems[i]];
            } else {
                pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.filteredItems[i]);
            }
        }

        if (type == 'part')
            $scope.partPagedItems = pagedItems;
        else
            $scope.suppPagedItems = pagedItems;
    };
    $scope.range = function (start, end) {
        var ret = [];
        if (!end) {
            end = start;
            start = 0;
        }
        for (var i = start; i < end; i++) {
            ret.push(i);
        }
        return ret;
    };
    $scope.partPrevPage = function () {
        if ($scope.currentPartPage > 0) {
            $scope.currentPartPage--;
        }
    };
    $scope.suppPrevPage = function () {
        if ($scope.currentSuppPage > 0) {
            $scope.currentSuppPage--;
        }
    };
    $scope.partNextPage = function () {
        if ($scope.currentPartPage < $scope.partPagedItems.length - 1) {
            $scope.currentPartPage++;
        }
    };
    $scope.suppNextPage = function () {
        if ($scope.currentSuppPage < $scope.suppPagedItems.length - 1) {
            $scope.currentSuppPage++;
        }
    };
    $scope.setPartPage = function () {
        $scope.currentPartPage = this.n;
    };
    $scope.setSuppPage = function () {
        $scope.currentSuppPage = this.n;
    };

    // Lazy load members and supporters info
    if (dataService.refreshMembers) {
        dataService.refreshMembers = false;
        dataService.getMembersById('REGULAR', dataService.event.eventId)
                   .then(function (result) {
                       if (result.status === 200) {
                           if (result.data) {
                               $scope.participants = result.data._participants;
                               $scope.supporters = result.data._supporters;

                               // Process the data for display
                               $scope.search('part');
                               $scope.search('supp');
                           }
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning members!");
                       }
                   });
    }
    else {
        $scope.participants = dataService.participants;
        $scope.supporters = dataService.supporters;

        // Process the data for display
        $scope.search('part');
        $scope.search('supp');
    }

    $scope.prizeItemCode = '';
    $scope.dateIssued = '';
    $scope.expirationDate = '';
    $scope.selectedMovieTicketEpId = 0;
    $scope.getEarnedMovieTicket = function (eventParticipationId) {
        $scope.prizeItemCode = '';
        $scope.dateIssued = '';
        $scope.expirationDate = '';
        $scope.selectedMovieTicketEpId = eventParticipationId;
        dataService.getMembersById('MOVIE', eventParticipationId)
                   .then(function (result) {
                       if (result.status === 200) {
                           if (result.data) {
                               var prize = result.data.split(";");
                               $scope.prizeItemCode = prize[0];
                               $scope.expirationDate = prize[1];
                               $scope.dateIssued = prize[2];
                           }
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning movie ticket!");
                       }
                   });
    }
    $scope.issueNewMovieTicket = function () {
        var resp = confirm("Are you sure want to issue a new ticket?");
        if (resp == true) {
            $scope.saveMembers('MOVIE');
        } else {
            $('#movieTicketModal').modal('hide');
        }
    };

    $scope.saveMembers = function (type) {
        var items = [];
        if (type == 'PARTICIPANTS')
            items = $scope.participants;
        else if (type == 'SUPPORTERS')
            items = $scope.supporters;
        else if (type == 'MOVIE')
            items = [];
        dataService.saveMembers($scope.selectedMovieTicketEpId > 0 ? $scope.selectedMovieTicketEpId : 0, items)
                   .then(function (result) {
                       if (result.status === 200) {
                           if (result.data) {
                               var prize = result.data.split(";");
                               $scope.prizeItemCode = prize[0];
                               $scope.expirationDate = prize[1];
                               $scope.dateIssued = prize[2];
                               $scope.selectedMovieTicketEpId = 0;
                           }
                           else
                               alert("Succesfully saved!");
                       }
                       else {
                           dataService.isBusy = false;
                           $scope.selectedMovieTicketEpId = 0;
                           alert(result.data);
                       }
                   });
    };

    $scope.refresh = function () {
        dataService.refreshMembers = true;
        $state.transitionTo($state.current, $state.params, { reload: true, inherit: false, notify: true });
    }
});

homeModule.controller("homeOrdersController", function ($scope, $state, dataService) {
    if (dataService.selectedIndex === undefined)
        return;

    // Lazy load payment info, shipping, sales and partner info
    if (dataService.refreshOrders) {
        dataService.refreshOrders = false;
        dataService.getOrdersById(dataService.event.eventId)
                   .then(function (result) {
                       if (result.status === 200) {
                           if (result.data) {
                               $scope.orders = result.data.salesInfo;
                               $scope.parentUsers = result.data.parentUsers;
                               if ($scope.parentUsers !== undefined && $scope.parentUsers.length > 0) {
                                   $scope.selectedParent = $scope.parentUsers[0];
                               }
                               $scope.profitUrl = result.data.profitStatementUrl;
                           }
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning orders!");
                       }
                   });
    }
    else {
        $scope.orders = dataService.orders.salesInfo;
        $scope.parentUsers = dataService.orders.parentUsers;
        if ($scope.parentUsers !== undefined && $scope.parentUsers.length > 0) {
            $scope.selectedParent = $scope.parentUsers[0];
        }
        $scope.profitUrl = dataService.orders.profitStatementUrl;
    }

    $scope.predicate = "orderID";
    $scope.setOrderTransfer = function (index) {
        $scope.currentIndex = index;
        $scope.memberName = $scope.orders[index].memberName;
        $scope.currentParentName = $scope.orders[index].parentName;
    };

    $scope.orderTransfer = function (eventParticipationId, parentMemberHierarchyId) {
        dataService.orderTransfer(eventParticipationId, parentMemberHierarchyId)
                   .then(function (result) {
                       if (result.status === 200) {
                           dataService.refreshOrders = true;
                           $scope.orders[$scope.currentIndex].parentName = $scope.selectedParent.firstName + ' ' + $scope.selectedParent.lastName;
                           $scope.currentParentName = $scope.selectedParent.firstName + ' ' + $scope.selectedParent.lastName;
                           alert("Succesfully saved!");
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error order transferring!");
                       }
                   });
    };

    $scope.refresh = function () {
        dataService.refreshOrders = true;
        $state.transitionTo($state.current, $state.params, { reload: true, inherit: false, notify: true });
    }
});

homeModule.controller("homeLinksController", function ($scope, $sce, $state, dataService) {
    if (dataService.selectedIndex === undefined)
        return;

    // Lazy load payment info, shipping, sales and partner info
    if (dataService.refreshLinks) {
        dataService.refreshLinks = false;
        dataService.getLinksInfoByEventParticipationId(dataService.event.eventId)
                   .then(function (result) {
                       if (result.status === 200) {
                           $scope.links = result.data;
                           $scope.commentsView = $sce.trustAsHtml($scope.links.comments);
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning links info!");
                       }
                   });
    }
    else {
        $scope.links = dataService.links;
        $scope.commentsView = $sce.trustAsHtml($scope.links.comments);
    }

    $scope.saveLinks = function () {
        dataService.saveLinks($scope.links)
                   .then(function (result) {
                       if (result.status === 200) {
                           if (result.data) {
                               $scope.links.comments = result.data.comments;
                               dataService.links.comments = result.data.comments; // sync with dataService
                               $scope.links.newComments = '';
                               $scope.commentsView = $sce.trustAsHtml($scope.links.comments);
                               alert("Succesfully saved!");
                           }
                       }
                       else {
                           dataService.isBusy = false;
                           alert(result.data);
                       }
                   });
    };

    $scope.refresh = function () {
        dataService.refreshLinks = true;
        $state.transitionTo($state.current, $state.params, { reload: true, inherit: false, notify: true });
    }
});

homeModule.controller("homeToolsController", function ($scope, dataService) {
    $scope.email = '';
    $scope.name = '';
    $scope.password = '';
    $scope.selectedPartner = {};
    $scope.memberPassword = [];    

    function validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    $scope.changePartner = function () {
        if ($scope.selectedPartner) {
            $scope.name = $scope.selectedPartner.name;
            $scope.password = $scope.selectedPartner.password;
        }
    }

    $scope.getMemberPasswordByEmail = function () {
        if (!validateEmail($scope.email)) {
            alert('Please provide a valid email address');
            return false;
        }
        $scope.name = '';
        $scope.password = '';
        $scope.selectedPartner = {};
        $scope.memberPassword = [];
        var enc_txt = encodeURIComponent($scope.email) + '/'; // Add a trailing slash to account for periods
        dataService.getMemberPasswordByEmail('PASSWORD', enc_txt)
                   .then(function (result) {
                       if (result.status === 200) {
                           if (result.data) {
                               if (result.data.length > 0) {
                                   $scope.memberPassword = result.data;
                                   $scope.selectedPartner = $scope.memberPassword[0];
                                   $scope.name = $scope.selectedPartner.name;
                                   $scope.password = $scope.selectedPartner.password;
                               }                               
                           }
                       }
                       else {
                           dataService.isBusy = false;
                           alert("Error returning member passwords!");
                       }
                   });
    }
    
    $scope.saveTools = function () {
        $scope.selectedPartner.password = $scope.password;
        dataService.saveTools('PASSWORD', $scope.selectedPartner)
                   .then(function (result) {
                       if (result.status === 200) {
                           alert("Succesfully saved!");
                       }
                       else {
                           dataService.isBusy = false;
                           alert(result.data);
                       }
                   });
    };
});