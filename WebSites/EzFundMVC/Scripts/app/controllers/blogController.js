(function () {
    "use strict";
    var module = angular.module("ezfund.api");

    function BlogAllController($log, $window, $location, $scope, $rootScope, $timeout, BlogFactory, $localStorage, $sce, $document, $element, MetaService) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$document = $document;
        $rootScope.MetaService = MetaService;

        BlogFactory.GetBlog.query({}, { cache: true }).$promise.then(
                    function (results) {
                        vm.posts = results;
                    },
                     function (error) {
                         $log.error(error);
                     });
    }
    BlogAllController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "BlogFactory", "$localStorage", "$sce", "$document", "$element", "MetaService"];
    module.controller("BlogAllController", BlogAllController);

    function BlogIndexController($log, $window, $location, $scope, $rootScope, $timeout, BlogFactory, $localStorage, $sce, $document, $element, MetaService) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$document = $document;
        $rootScope.MetaService = MetaService;

        BlogFactory.GetBlog.query({sortByRecent:true, limit: 15}, { cache: true }).$promise.then(
                    function (results) {
                        vm.posts = results;
                    },
                     function (error) {
                         $log.error(error);
                     });
    }
    BlogIndexController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "BlogFactory", "$localStorage", "$sce", "$document", "$element", "MetaService"];
    module.controller("BlogIndexController", BlogIndexController);

    function BlogLastPostsController($log, $window, $location, $scope, $rootScope, $timeout, BlogFactory, $localStorage, $sce, $document, $element, MetaService) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$document = $document;
        $rootScope.MetaService = MetaService;

        BlogFactory.GetBlog.query({ sortByRecent: true, limit: 5 }, { cache: true }).$promise.then(
                   function (results) {
                       vm.lastPosts = results;
                   },
                    function (error) {
                        $log.error(error);
                    });

    }
    BlogLastPostsController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "BlogFactory", "$localStorage", "$sce", "$document", "$element", "MetaService"];
    module.controller("BlogLastPostsController", BlogLastPostsController);

    function BlogCategoriesController($log, $window, $location, $scope, $rootScope, $timeout, BlogFactory, $localStorage, $sce, $document, $element, MetaService) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$document = $document;
        $rootScope.MetaService = MetaService;
        
        BlogFactory.GetBlogCategories.query({}, { cache: true }).$promise.then(
                    function (results) {
                        vm.categories = results;
                    },
                     function (error) {
                         $log.error(error);
                     });
    }
    BlogCategoriesController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "BlogFactory", "$localStorage", "$sce", "$document", "$element", "MetaService"];
    module.controller("BlogCategoriesController", BlogCategoriesController);

    function BlogCategoryController($log, $window, $location, $scope, $rootScope, $timeout, BlogFactory, $localStorage, $sce, $document, $element, MetaService) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$document = $document;
        $rootScope.MetaService = MetaService;

        var paths = $location.path().split('/');
        var url = paths[3];
        BlogFactory.GetBlogCategories.get({ url: url }, { cache: true }).$promise.then(
                    function (result) {
                        vm.category = result;
                        var description = "Interested in " + vm.category.Name + "? We have all the articles, tips & tricks to help you make a success of your fundraising event!";
                        $rootScope.MetaService.set(vm.category.Name, description, "");
                        BlogFactory.GetBlog.query({ categoryId: vm.category.Id }, { cache: true }).$promise.then(
                           function (results) {
                               vm.posts = results;
                           },
                            function (error) {
                                $log.error(error);
                                $window.location.href = "/blog";
                            });
                    },
                     function (error) {
                         $log.error(error);
                         $window.location.href = "/blog";
                     });
    }
    BlogCategoryController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "BlogFactory", "$localStorage", "$sce", "$document", "$element", "MetaService"];
    module.controller("BlogCategoryController", BlogCategoryController);

    function BlogTagController($log, $window, $location, $scope, $rootScope, $timeout, BlogFactory, $localStorage, $sce, $document, $element, MetaService) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$document = $document;
        $rootScope.MetaService = MetaService;

        var paths = $location.path().split('/');
        var url = paths[3];
        BlogFactory.GetBlogTags.get({ url: url }, { cache: true }).$promise.then(
                    function (result) {
                        vm.tag = result;
                        var description = "Interested in " + vm.tag.Name + "? We have all the articles, tips & tricks to help you make a success of your fundraising event!";
                        var canonicalUrl = "/blog/category/" + vm.tag.Url;
                        $rootScope.MetaService.set(vm.tag.Name, description, "", canonicalUrl);
                        BlogFactory.GetBlog.query({ tagId: vm.tag.Id }, { cache: true }).$promise.then(
                           function (results) {
                               vm.posts = results;
                           },
                            function (error) {
                                $log.error(error);
                                $window.location.href = "/blog";
                            });
                    },
                     function (error) {
                         $log.error(error);
                         $window.location.href = "/blog";
                     });
    }
    BlogTagController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "BlogFactory", "$localStorage", "$sce", "$document", "$element", "MetaService"];
    module.controller("BlogTagController", BlogTagController);

    function BlogPostController($log, $window, $location, $scope, $rootScope, $timeout, BlogFactory, $localStorage, $sce, $document, $element, MetaService) {
        var vm = this;
        vm.scope = $scope;
        vm.scope.$storage = $localStorage;
        vm.scope.$sce = $sce;
        vm.scope.$location = $location;
        vm.scope.$document = $document;
        $rootScope.MetaService = MetaService;

        var paths = $location.path().split('/');
        var url = paths[3];
        BlogFactory.GetBlog.get({ url: url, isPreview: false }, { cache: true }).$promise.then(
                    function (result) {
                        vm.post = result;
                        var canonicalUrl = "/blog/post/"+vm.post.Url;
                        $rootScope.MetaService.set(vm.post.Title, vm.post.MetaDescription, vm.post.MetaTitle, canonicalUrl);
                        vm.shareUrl = "https://www.ezfund.com/blog/post/" + vm.post.Url;
                        BlogFactory.GetBlog.query({ random: true, limit: 4 }, { cache: true }).$promise.then(
                           function (results) {
                               vm.randomPosts = results;
                           },
                            function (error) {
                                $log.error(error);
                                $window.location.href = "/blog";
                            });
                    },
                     function (error) {
                         $log.error(error);
                         $window.location.href = "/blog";
                     });
    }
    BlogPostController.$inject = ["$log", "$window", "$location", "$scope", "$rootScope", "$timeout", "BlogFactory", "$localStorage", "$sce", "$document", "$element", "MetaService"];
    module.controller("BlogPostController", BlogPostController);
})();