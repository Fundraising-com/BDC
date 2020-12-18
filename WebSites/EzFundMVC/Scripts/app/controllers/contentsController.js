(function () {
    "use strict";
    var module = angular.module("ezfund.api");

    function CollapsedHeaderMenuController($log, $scope, ContentFactory, $localStorage) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.navCollapsed = true;
    }

    CollapsedHeaderMenuController.$inject = ["$log", "$scope", "ContentFactory", "$localStorage"];
    module.controller("CollapsedHeaderMenuController", CollapsedHeaderMenuController);



    function SubscriptionModalCtrl($uibModalInstance, $sce, subscription, email) {
        var vm = this;
        subscription;
        vm.email = email;
        if (!subscription)
        {
            vm.title = "Thanks for subscribing!!!"
            vm.body = "We'll do our best to keep you informed with our latest and best fundraising ideas!"
        }
        else
        {
            vm.title = "E-mail already registered"
            vm.body = "We are sorry, but the e-mail "+ vm.email +" has already been used."
        }
        vm.$sce = $sce;
        vm.CloseModal = function () {
            $uibModalInstance.dismiss('close');
        };
    }
    SubscriptionModalCtrl.$inject = ["$uibModalInstance", "$sce", "subscription", "email"];
    module.controller("SubscriptionModalCtrl", SubscriptionModalCtrl);




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



    function HomePageRotatorController($log, $scope, $timeout, ContentFactory, $localStorage) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.executing = false;
        vm.interval = 0;
        vm.noWrap = false;
        ContentFactory.HomePageRotator.query({ isActive: true }, { cache: true }).$promise.then(
            function (results) {
                vm.executing = true;
                vm.banners = results;
            },
            function (error) {
                $log.error(error);
            });


    }

    HomePageRotatorController.$inject = ["$log", "$scope", "$timeout", "ContentFactory", "$localStorage"];
    module.controller("HomePageRotatorController", HomePageRotatorController);


    /*OLD SLICK FUNCTIONALITY*/
    //function HomePageRotatorController($log, $scope, $timeout, ContentFactory, $localStorage) {
    //    var vm = this;
    //    vm.scope = $scope;
    //    vm.scope.$storage = $localStorage;
    //    vm.executing = false;
    //    vm.interval = 0;
    //    vm.noWrap = false;
    //    ContentFactory.HomePageRotator.query({ isActive: true }, { cache: true }).$promise.then(
    //        function (results) {
    //            vm.executing = true;
    //            vm.banners = results;
    //            $timeout(function () {
    //                $("#home-page-rotator-slick").slick({
    //                    dots: false,
    //                    infinite: true,
    //                    rows: 2,
    //                    slidesToShow: 4,
    //                    slidesToScroll: 4,
    //                    prevArrow: '<button type="button" class="btn" style="position: absolute; top: 45%; display: block;  left: 1%; z-index: 99; background-color: #8C8B89; color:#FFFFFF; font-size:large;"><span class="glyphicon glyphicon-chevron-left"></span></button>',
    //                    nextArrow: '<button type="button" class="btn" style="position: absolute; top: 45%; display: block;  right: 1%; z-index: 99; background-color: #8C8B89; color:#FFFFFF; font-size:large;"><span class="glyphicon glyphicon-chevron-right"></span></button>',
    //                    responsive: [
    //                      {
    //                          breakpoint: 1024,
    //                          settings: {
    //                              rows: 2,
    //                              slidesToShow: 3,
    //                              slidesToScroll: 3,
    //                          }
    //                      },
    //                      {
    //                          breakpoint: 600,
    //                          settings: {
    //                              rows: 1,
    //                              slidesToShow: 2,
    //                              slidesToScroll: 2,
    //                          }
    //                      },
    //                      {
    //                          breakpoint: 480,
    //                          settings: {
    //                              rows: 1,
    //                              slidesToShow: 1,
    //                              slidesToScroll: 1,
    //                          }
    //                      }
    //                    ]
    //                });
    //                vm.executing = false;
    //            }, 1 * 1000); 
    //        },
    //        function (error) {
    //            $log.error(error);
    //        });

        
    //}

    //HomePageRotatorController.$inject = ["$log", "$scope", "$timeout", "ContentFactory", "$localStorage"];
    //module.controller("HomePageRotatorController", HomePageRotatorController);

    function BannerController($log, $scope, ContentFactory, $localStorage) {
    	var vm = this;
    	vm.scope = $scope;
    	vm.scope.$storage = $localStorage;
    	vm.interval = 5000;
    	vm.noWrap = false;
    	vm.slides = ContentFactory.HomePageBanners.query({ isActive: true}, { cache: true });
    }
    BannerController.$inject = ["$log", "$scope", "ContentFactory", "$localStorage"];
    module.controller("BannerController", BannerController);



    function BannerControllerDesktop($log, $scope, ContentFactory, $localStorage) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.interval = 5000;
        vm.noWrap = false;
        vm.slides = ContentFactory.HomePageBanners.query({ isActive: true, bannerType: 0}, { cache: true });
    }

    BannerControllerDesktop.$inject = ["$log", "$scope", "ContentFactory", "$localStorage"];
    module.controller("BannerControllerDesktop", BannerControllerDesktop);

    function BannerControllerMobile($log, $scope, ContentFactory, $localStorage) {
    	var vm = this;
    	vm.scope = $scope;
    	vm.scope.$storage = $localStorage;
    	vm.interval = 5000;
    	vm.noWrap = false;
    	vm.slides = ContentFactory.HomePageBanners.query({ isActive: true, bannerType: 1 }, { cache: true });
    }

    BannerControllerMobile.$inject = ["$log", "$scope", "ContentFactory", "$localStorage"];
    module.controller("BannerControllerMobile", BannerControllerMobile);

    function TestimonialsController($log, $scope, ContentFactory, $localStorage, $sce) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        ContentFactory.Testimonial.query({ maxResults: 1, isRandom: true }, { cache: true }).$promise.then(
            function (result) {
                if (result.length > 0) {
                    vm.testimonial = result[0];
                }
            },
            function (error) {
                $log.error(error);
            });
    }
    TestimonialsController.$inject = ["$log", "$scope", "ContentFactory", "$localStorage","$sce"];
    module.controller("TestimonialsController", TestimonialsController);
})();