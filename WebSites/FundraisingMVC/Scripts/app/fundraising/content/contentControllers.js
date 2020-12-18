(function () {
   "use strict";
   var module = angular.module("fundraising.content");

   function BannersController($log, $scope, BannerFactory, $localStorage, $rootScope) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.banners = [];
      vm.type = 0;
      vm.sort = false;
      vm.viewPortId = 0;
      vm.FindBanners = function (type, sort, viewPortId) {
         vm.type = type;
         vm.sort = sort;
         vm.viewPortId = viewPortId;
         if (vm.scope.$storage.Partner !== null && vm.scope.$storage.Partner !== undefined) {
            BannerFactory.Banner.query({ type: vm.type, partnerId: vm.scope.$storage.Partner.Id, sort: vm.sort, viewPortId: vm.viewPortId }, { isArray: true, cache: false }).$promise.then(
               function (data) {
                  vm.banners = data;
               }, function (error) {
                  console.warn(error);
               });
         }
      };
      
      $rootScope.$on("partnerLoaded", function() {
         
         vm.FindBanners(vm.type, vm.sort, vm.viewPortId);
      });
   }
   BannersController.$inject = ["$log", "$scope", "BannerFactory", "$localStorage", "$rootScope"];
   module.controller("BannersController", BannersController);

   function BlogController($log, $scope, BannerFactory, $localStorage, $rootScope) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.feeds = [];
      vm.categories = [];
      vm.tags = [];
      vm.GetBlogEntries = function (sortByRecent, limit) {
         BannerFactory.BlogEntry.query({ sortByRecent: sortByRecent, limit: limit }, { isArray: true, cache: true })
            .$promise.then(function(entries) {
                  vm.feeds = entries;
               },
            function(error) {
               console.error(error);
            });
      };
   }
   BlogController.$inject = ["$log", "$scope", "BannerFactory", "$localStorage", "$rootScope"];
   module.controller("BlogController", BlogController);

   function HomePageRotatorController($log, $scope, $timeout, BannerFactory, $localStorage) {
       var vm = this;
       vm.scope = $scope;
       vm.scope.$storage = $localStorage;
       vm.executing = false;
       vm.interval = 0;
       vm.noWrap = false;
       BannerFactory.HomePageRotator.query({ isActive: true }, { cache: true }).$promise.then(
           function (results) {
               vm.executing = true;
               vm.banners = results;
               $timeout(function () {
                  
                   $("#home-page-rotator-slick").slick({
                       dots: true,
                       infinite: true,
                       rows: 2,
                       slidesToShow: 4,
                       slidesToScroll: 4,
                       prevArrow: false,
                       nextArrow: false,
                       autoplaySpeed: 3000,
                       autoplay:true,
                       //prevArrow: '<button type="button" class="btn" style="position: absolute; top: 45%; display: block;  left: 1%; z-index: 99; background-color: #8C8B89; color:#FFFFFF; font-size:large;"><span class="glyphicon glyphicon-chevron-left"></span></button>',
                       //nextArrow: '<button type="button" class="btn" style="position: absolute; top: 45%; display: block;  right: 1%; z-index: 99; background-color: #8C8B89; color:#FFFFFF; font-size:large;"><span class="glyphicon glyphicon-chevron-right"></span></button>',
                       responsive: [
                         {
                             breakpoint: 3000,
                             settings: {
                                 rows: 1,
                                 autoplaySpeed: 3000,
                                 slidesToShow: 6,
                                 slidesToScroll: 6,
                                 prevArrow: false,
                                 nextArrow: false,
                                 infinite: true,
                                 autoplay: true,
                                 dots: true
                             }
                         },
                         {
                             breakpoint: 992,
                             settings: {
                                 rows: 1,
                                 autoplaySpeed: 3000,
                                 slidesToShow: 4,
                                 slidesToScroll: 4,
                                 prevArrow: false,
                                 nextArrow: false,
                                 infinite: true,
                                 autoplay: true,
                                 dots: true
                             }
                         },
                         {
                             breakpoint: 768,
                             settings: {
                                 rows: 2,
                                 autoplaySpeed: 3000,
                                 slidesToShow: 2,
                                 slidesToScroll: 2,
                                 prevArrow: false,
                                 nextArrow: false,
                                 infinite: true,
                                 autoplay: true,
                                 dots: true
                             }
                         }
                       ]
                   });
                   
                   vm.executing = false;
               }, 1 * 1000);
               
           },
           function (error) {
               $log.error(error);
           });


   }

   HomePageRotatorController.$inject = ["$log", "$scope", "$timeout", "BannerFactory", "$localStorage"];
   module.controller("HomePageRotatorController", HomePageRotatorController);




   function FooterController($log, $scope, ContentFactory, NewsletterSubscriptionFactory, $localStorage, $uibModal, $document) {
       var vm = this;
       vm.scope = $scope;
       vm.scope.$uibModal = $uibModal;
       vm.scope.$document = $document;
       vm.scope.$storage = $localStorage;
       vm.aboutUs = true;
       vm.products = true;
       vm.fundraisingIdeas = true;
       vm.reachUs = true;
       vm.showModal = false;

       vm.Subscribe = function () {
           NewsletterSubscriptionFactory.GetSubscriberByMail.get({ email: vm.newsletterSubscription.Email }, { isArray: false, cache: true }).$promise.then(
               function (subscription) {
                   if (subscription != null && subscription.Email != null) {
                       //The user is already subscribed
                       vm.showModal = true;
                       vm.alreadySubscribbed = true;
                       vm.OpenModal();
                   }
                   else {
                       //New subscription
                       vm.newsletterSubscription.Unsubscribed = false;
                       NewsletterSubscriptionFactory.NewsletterSubscription.save(vm.newsletterSubscription).$promise.then(
                           function (subscriptionId) {
                               vm.alreadySubscribbed = false;
                               vm.showModal = true;
                               vm.OpenModal();
                           }, function (error) {
                               $log.error(error);
                           });
                   }
               }, function (error) {
                   $log.error(error);
               });
       };

       /*Featured Info Modal*/
       vm.OpenModal = function (parentSelector) {
           var parentElem = parentSelector ?
             angular.element(vm.scope.$document[0].querySelector('.newsletter-info-modal' + parentSelector)) : undefined;
           var modalInstance = vm.scope.$uibModal.open({
               animation: vm.animationsEnabled,
               ariaLabelledBy: 'modal-title',
               ariaDescribedBy: 'modal-body',
               templateUrl: 'NewsletterInfoModal.cshtml',
               controller: 'SubscriptionModalCtrl',
               controllerAs: 'modalCtrl',
               size: 'sm',
               appendTo: parentElem,
               resolve: {
                   subscription: function () {
                       return vm.alreadySubscribbed;
                   },
                   email: function () {
                       return vm.newsletterSubscription.Email;
                   }
               }
           });
       };
   }

   FooterController.$inject = ["$log", "$scope", "ContentFactory", "NewsletterSubscriptionFactory", "$localStorage", "$uibModal", "$document"];
   module.controller("FooterController", FooterController);






})();