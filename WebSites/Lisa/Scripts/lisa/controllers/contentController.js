(function() {
   "use strict";
   var module = angular.module("lisa.content");

   function BlogController($log, $scope, ContentFactory, $localStorage, $rootScope, $mdDialog, $document, $q, ToastFactory) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.posts = [];
      vm.post = {};
      vm.category = {};
      vm.tag = {};
      vm.categories = [];
      vm.tags = [];
      vm.executing = false;
      vm.filter = "";
      vm.postsSelected = [];
      vm.tagsSelected = [];
      vm.categoriesSelected = [];
      vm.GetPosts = function() {
         vm.executing = true;
         ContentFactory.BlogEntry.query({ onlyPublishedReady: false }, { isArray: true, cache: true })
            .$promise
            .then(function(entries) {
               vm.posts = entries;
            })
            .catch(function(error) {
               console.error(error);
            })
            .finally(function() {
               vm.executing = false;
            });
      };
      vm.GetCategories = function () {
         vm.executing = true;
         ContentFactory.BlogCategory.query({ }, { isArray: true, cache: true })
            .$promise
            .then(function (entries) {
               vm.categories = entries;
            })
            .catch(function (error) {
               console.error(error);
            })
            .finally(function () {
               vm.executing = false;
            });
      };
      vm.GetTags = function () {
         vm.executing = true;
         ContentFactory.BlogTag.query({ }, { isArray: true, cache: true })
            .$promise
            .then(function (entries) {
               vm.tags = entries;
            })
            .catch(function (error) {
               console.error(error);
            })
            .finally(function () {
               vm.executing = false;
            });
      };
      vm.PostCheckboxChanged = function (post) {
         if (post.Selected) {
            vm.postsSelected.push(post);
         } else {
            vm.postsSelected.splice(vm.postsSelected.indexOf(post), 1);
         }
         
      };
      vm.CategoryCheckboxChanged = function (category) {
         if (category.Selected) {
            vm.categoriesSelected.push(category);
         } else {
            vm.categoriesSelected.splice(vm.categoriesSelected.indexOf(category), 1);
         }

      };
      vm.TagCheckboxChanged = function (tag) {
         if (tag.Selected) {
            vm.tagsSelected.push(tag);
         } else {
            vm.tagsSelected.splice(vm.tagsSelected.indexOf(tag), 1);
         }

      };
      vm.DeletePosts = function() {
         var dialog = $mdDialog.confirm()
          .title('Delete Posts')
          .textContent('Do you want to delete the selected(s) posts?')
          .ok('Yes')
          .cancel('No');

         $mdDialog.show(dialog).then(function () {
            var promises = [];
            for (var i = 0; i < vm.postsSelected.length; i++) {
               var promise = ContentFactory.BlogEntry.delete({id: vm.postsSelected[i].Id}).$promise;
               promises.push(promise);
               vm.posts.splice(vm.posts.indexOf(vm.postsSelected[i]), 1);
            }
            vm.postsSelected = [];
            $q.all(promises).then(
               function () {
                  ToastFactory.Success("Values deleted correctly!");                  
               },
               function (error) {
                  $log.error(error);
                  ToastFactory.Error(error);
               });
         }, function () {});
      };
      vm.DeleteCategories = function () {
         var dialog = $mdDialog.confirm()
          .title('Delete Categories')
          .textContent('Do you want to delete the selected(s) categories?')
          .ok('Yes')
          .cancel('No');

         $mdDialog.show(dialog).then(function () {
            var promises = [];
            for (var i = 0; i < vm.categoriesSelected.length; i++) {
               var promise = ContentFactory.BlogCategory.delete({ id: vm.categoriesSelected[i].Id }).$promise;
               promises.push(promise);
               vm.categories.splice(vm.categories.indexOf(vm.categoriesSelected[i]), 1);
            }
            vm.categoriesSelected = [];
            $q.all(promises).then(
               function () {
                  ToastFactory.Success("Values deleted correctly!");
               },
               function (error) {
                  $log.error(error);
                  ToastFactory.Error(error);
               });
         }, function () { });
      };
      vm.DeleteTags = function () {
         var dialog = $mdDialog.confirm()
          .title('Delete Tags')
          .textContent('Do you want to delete the selected(s) tags?')
          .ok('Yes')
          .cancel('No');

         $mdDialog.show(dialog).then(function () {
            var promises = [];
            for (var i = 0; i < vm.tagsSelected.length; i++) {
               var promise = ContentFactory.BlogTag.delete({ id: vm.tagsSelected[i].Id }).$promise;
               promises.push(promise);
               vm.tags.splice(vm.tags.indexOf(vm.tagsSelected[i]), 1);
            }
            vm.tagsSelected = [];
            $q.all(promises).then(
               function () {
                  ToastFactory.Success("Values deleted correctly!");
               },
               function (error) {
                  $log.error(error);
                  ToastFactory.Error(error);
               });
         }, function () { });
      };
      vm.OpenPostDialog = function (post) {
         if (post === null) {
            vm.post = { Id: 0, IsDraft: false, Tags: [], Category: {}, Published: new Date() };
            vm.postsSelected = [];
            for (var i = 0; i < vm.posts.length; i++) {
               vm.posts[i].Selected = false;
            }
         } else {
            vm.post = post;
         }
         vm.post.Published = new Date(vm.post.Published);
         $mdDialog.show({
               templateUrl: '/scripts/lisa/templates/FRCOM/blog/post.html',
               parent: $document.find("#posts"),
               locals: { post: vm.post },
               controller: function DialogController($scope, $mdDialog, ContentFactory) {
                  var ivm = this;
                  ivm.scope = $scope;
                  ivm.categories = ContentFactory.BlogCategory.query({}, { isArray: true, cache: true });
                  ivm.tags = ContentFactory.BlogTag.query({}, { isArray: true, cache: true });
                  ivm.cancel = function() {
                     vm.post = null;
                     $mdDialog.hide(null);
                  };
                  ivm.save = function() {
                     $mdDialog.hide(vm.post);
                  };
               },
               controllerAs: 'ctrl',
               bindToController: true,
               clickOutsideToClose: true,
               fullscreen: true
            })
            .then(
               function(post) {
                  if (post !== undefined && post !== null) {
                     if (post.Id > 0) {
                        ContentFactory.BlogEntry.update(post,
                              function () {
                                 ToastFactory.Success("Values saved correctly!");                                 
                              },
                              function(error) {
                                 $log.error(error);
                                 ToastFactory.Error(error);
                              });
                     } else {
                        ContentFactory.BlogEntry.save(post,
                           function(postCreated) {
                              vm.posts.push(postCreated);
                              ToastFactory.Success("Values saved correctly!");
                           },
                           function(error) {
                              $log.error(error);
                              ToastFactory.Error(error);
                           }
                        );
                     }
                  }
               },
               function() {
                  vm.post = null;
               });
      };
      vm.OpenCategoryDialog = function (category) {
         if (category === null) {
            vm.category = { Id: 0 };
            vm.categoriesSelected = [];
            for (var i = 0; i < vm.categories.length; i++) {
               vm.categories[i].Selected = false;
            }
         } else {
            vm.category = category;
         }
         $mdDialog.show({
            templateUrl: '/scripts/lisa/templates/FRCOM/blog/category.html',
            parent: $document.find("#categories"),
            locals: { category: vm.category },
            controller: function DialogController($scope, $mdDialog) {
               var ivm = this;
               ivm.scope = $scope;
               ivm.cancel = function () {
                  vm.category = null;
                  $mdDialog.hide(null);
               };
               ivm.save = function () {
                  $mdDialog.hide(vm.category);
               };
            },
            controllerAs: 'ctrl',
            bindToController: true,
            clickOutsideToClose: true,
            fullscreen: true
         })
            .then(
               function (category) {
                  if (category !== undefined && category !== null) {
                     if (category.Id > 0) {
                        ContentFactory.BlogCategory.update(category,
                              function () {
                                 ToastFactory.Success("Values saved correctly!");
                              },
                              function (error) {
                                 $log.error(error);
                                 ToastFactory.Error(error);
                              });
                     } else {
                        ContentFactory.BlogCategory.save(category,
                           function (categoryCreated) {
                              vm.categories.push(categoryCreated);
                              ToastFactory.Success("Values saved correctly!");
                           },
                           function (error) {
                              $log.error(error);
                              ToastFactory.Error(error);
                           }
                        );
                     }
                  }
               },
               function () {
                  vm.category = null;
               });
      };
      vm.OpenTagDialog = function (tag) {
         if (tag === null) {
            vm.tag = { Id: 0 };
            vm.tagsSelected = [];
            for (var i = 0; i < vm.tags.length; i++) {
               vm.tags[i].Selected = false;
            }
         } else {
            vm.tag = tag;
         }
         $mdDialog.show({
            templateUrl: '/scripts/lisa/templates/FRCOM/blog/tag.html',
            parent: $document.find("#tags"),
            locals: { tag: vm.tag },
            controller: function DialogController($scope, $mdDialog) {
               var ivm = this;
               ivm.scope = $scope;
               ivm.cancel = function () {
                  vm.tag = null;
                  $mdDialog.hide(null);
               };
               ivm.save = function () {
                  $mdDialog.hide(vm.tag);
               };
            },
            controllerAs: 'ctrl',
            bindToController: true,
            clickOutsideToClose: true,
            fullscreen: true
         })
            .then(
               function (tag) {
                  if (tag !== undefined && tag !== null) {
                     if (tag.Id > 0) {
                        ContentFactory.BlogTag.update(tag,
                              function () {
                                 ToastFactory.Success("Values saved correctly!");
                              },
                              function (error) {
                                 $log.error(error);
                                 ToastFactory.Error(error);
                              });
                     } else {
                        ContentFactory.BlogTag.save(tag,
                           function (tagCreated) {
                              vm.tags.push(tagCreated);
                              ToastFactory.Success("Values saved correctly!");
                           },
                           function (error) {
                              $log.error(error);
                              ToastFactory.Error(error);
                           }
                        );
                     }
                  }
               },
               function () {
                  vm.tag = null;
               });
      };
   }

   BlogController
      .$inject = ["$log", "$scope", "ContentFactory", "$localStorage", "$rootScope", "$mdDialog", "$document", "$q", "ToastFactory"];
   module.controller("BlogController", BlogController);

   function EzBlogController($log, $scope, ContentFactory, $localStorage, $rootScope, $mdDialog, $document, $q, ToastFactory) {
       var vm = this;
       vm.scope = $scope;
       vm.scope.$storage = $localStorage;
       vm.posts = [];
       vm.post = {};
       vm.category = {};
       vm.tag = {};
       vm.categories = [];
       vm.tags = [];
       vm.executing = false;
       vm.filter = "";
       vm.postsSelected = [];
       vm.tagsSelected = [];
       vm.categoriesSelected = [];
       vm.GetPosts = function () {
           vm.executing = true;
           ContentFactory.EzBlogEntry.query({ onlyPublishedReady: false }, { isArray: true, cache: true })
              .$promise
              .then(function (entries) {
                  vm.posts = entries;
              })
              .catch(function (error) {
                  console.error(error);
              })
              .finally(function () {
                  vm.executing = false;
              });
       };
       vm.GetCategories = function () {
           vm.executing = true;
           ContentFactory.EzBlogCategory.query({}, { isArray: true, cache: true })
              .$promise
              .then(function (entries) {
                  vm.categories = entries;
              })
              .catch(function (error) {
                  console.error(error);
              })
              .finally(function () {
                  vm.executing = false;
              });
       };
       vm.GetTags = function () {
           vm.executing = true;
           ContentFactory.EzBlogTag.query({}, { isArray: true, cache: true })
              .$promise
              .then(function (entries) {
                  vm.tags = entries;
              })
              .catch(function (error) {
                  console.error(error);
              })
              .finally(function () {
                  vm.executing = false;
              });
       };
       vm.PostCheckboxChanged = function (post) {
           if (post.Selected) {
               vm.postsSelected.push(post);
           } else {
               vm.postsSelected.splice(vm.postsSelected.indexOf(post), 1);
           }

       };
       vm.CategoryCheckboxChanged = function (category) {
           if (category.Selected) {
               vm.categoriesSelected.push(category);
           } else {
               vm.categoriesSelected.splice(vm.categoriesSelected.indexOf(category), 1);
           }

       };
       vm.TagCheckboxChanged = function (tag) {
           if (tag.Selected) {
               vm.tagsSelected.push(tag);
           } else {
               vm.tagsSelected.splice(vm.tagsSelected.indexOf(tag), 1);
           }

       };
       vm.DeletePosts = function () {
           var dialog = $mdDialog.confirm()
            .title('Delete Posts')
            .textContent('Do you want to delete the selected(s) posts?')
            .ok('Yes')
            .cancel('No');

           $mdDialog.show(dialog).then(function () {
               var promises = [];
               for (var i = 0; i < vm.postsSelected.length; i++) {
                   var promise = ContentFactory.EzBlogEntry.delete({ id: vm.postsSelected[i].Id }).$promise;
                   promises.push(promise);
                   vm.posts.splice(vm.posts.indexOf(vm.postsSelected[i]), 1);
               }
               vm.postsSelected = [];
               $q.all(promises).then(
                  function () {
                      ToastFactory.Success("Values deleted correctly!");
                  },
                  function (error) {
                      $log.error(error);
                      ToastFactory.Error(error);
                  });
           }, function () { });
       };
       vm.DeleteCategories = function () {
           var dialog = $mdDialog.confirm()
            .title('Delete Categories')
            .textContent('Do you want to delete the selected(s) categories?')
            .ok('Yes')
            .cancel('No');

           $mdDialog.show(dialog).then(function () {
               var promises = [];
               for (var i = 0; i < vm.categoriesSelected.length; i++) {
                   var promise = ContentFactory.EzBlogCategory.delete({ id: vm.categoriesSelected[i].Id }).$promise;
                   promises.push(promise);
                   vm.categories.splice(vm.categories.indexOf(vm.categoriesSelected[i]), 1);
               }
               vm.categoriesSelected = [];
               $q.all(promises).then(
                  function () {
                      ToastFactory.Success("Values deleted correctly!");
                  },
                  function (error) {
                      $log.error(error);
                      ToastFactory.Error(error);
                  });
           }, function () { });
       };
       vm.DeleteTags = function () {
           var dialog = $mdDialog.confirm()
            .title('Delete Tags')
            .textContent('Do you want to delete the selected(s) tags?')
            .ok('Yes')
            .cancel('No');

           $mdDialog.show(dialog).then(function () {
               var promises = [];
               for (var i = 0; i < vm.tagsSelected.length; i++) {
                   var promise = ContentFactory.EzBlogTag.delete({ id: vm.tagsSelected[i].Id }).$promise;
                   promises.push(promise);
                   vm.tags.splice(vm.tags.indexOf(vm.tagsSelected[i]), 1);
               }
               vm.tagsSelected = [];
               $q.all(promises).then(
                  function () {
                      ToastFactory.Success("Values deleted correctly!");
                  },
                  function (error) {
                      $log.error(error);
                      ToastFactory.Error(error);
                  });
           }, function () { });
       };
       vm.OpenPostDialog = function (post) {
           if (post === null) {
               vm.post = { Id: 0, IsDraft: false, Tags: [], Category: {}, Published: new Date() };
               vm.postsSelected = [];
               for (var i = 0; i < vm.posts.length; i++) {
                   vm.posts[i].Selected = false;
               }
           } else {
               vm.post = post;
           }
           vm.post.Published = new Date(vm.post.Published);
           $mdDialog.show({
               templateUrl: '/scripts/lisa/templates/FRCOM/blog/post.html',
               parent: $document.find("#posts"),
               locals: { post: vm.post },
               controller: function DialogController($scope, $mdDialog, ContentFactory) {
                   var ivm = this;
                   ivm.scope = $scope;
                   ivm.categories = ContentFactory.EzBlogCategory.query({}, { isArray: true, cache: true });
                   ivm.tags = ContentFactory.EzBlogTag.query({}, { isArray: true, cache: true });
                   ivm.cancel = function () {
                       vm.post = null;
                       $mdDialog.hide(null);
                   };
                   ivm.save = function () {
                       $mdDialog.hide(vm.post);
                   };
               },
               controllerAs: 'ctrl',
               bindToController: true,
               clickOutsideToClose: true,
               fullscreen: true
           })
              .then(
                 function (post) {
                     if (post !== undefined && post !== null) {
                         if (post.Id > 0) {
                             ContentFactory.EzBlogEntry.update(post,
                                   function () {
                                       ToastFactory.Success("Values saved correctly!");
                                   },
                                   function (error) {
                                       $log.error(error);
                                       ToastFactory.Error(error);
                                   });
                         } else {
                             ContentFactory.EzBlogEntry.save(post,
                                function (postCreated) {
                                    vm.posts.push(postCreated);
                                    ToastFactory.Success("Values saved correctly!");
                                },
                                function (error) {
                                    $log.error(error);
                                    ToastFactory.Error(error);
                                }
                             );
                         }
                     }
                 },
                 function () {
                     vm.post = null;
                 });
       };
       vm.OpenCategoryDialog = function (category) {
           if (category === null) {
               vm.category = { Id: 0 };
               vm.categoriesSelected = [];
               for (var i = 0; i < vm.categories.length; i++) {
                   vm.categories[i].Selected = false;
               }
           } else {
               vm.category = category;
           }
           $mdDialog.show({
               templateUrl: '/scripts/lisa/templates/FRCOM/blog/category.html',
               parent: $document.find("#categories"),
               locals: { category: vm.category },
               controller: function DialogController($scope, $mdDialog) {
                   var ivm = this;
                   ivm.scope = $scope;
                   ivm.cancel = function () {
                       vm.category = null;
                       $mdDialog.hide(null);
                   };
                   ivm.save = function () {
                       $mdDialog.hide(vm.category);
                   };
               },
               controllerAs: 'ctrl',
               bindToController: true,
               clickOutsideToClose: true,
               fullscreen: true
           })
              .then(
                 function (category) {
                     if (category !== undefined && category !== null) {
                         if (category.Id > 0) {
                             ContentFactory.EzBlogCategory.update(category,
                                   function () {
                                       ToastFactory.Success("Values saved correctly!");
                                   },
                                   function (error) {
                                       $log.error(error);
                                       ToastFactory.Error(error);
                                   });
                         } else {
                             ContentFactory.EzBlogCategory.save(category,
                                function (categoryCreated) {
                                    vm.categories.push(categoryCreated);
                                    ToastFactory.Success("Values saved correctly!");
                                },
                                function (error) {
                                    $log.error(error);
                                    ToastFactory.Error(error);
                                }
                             );
                         }
                     }
                 },
                 function () {
                     vm.category = null;
                 });
       };
       vm.OpenTagDialog = function (tag) {
           if (tag === null) {
               vm.tag = { Id: 0 };
               vm.tagsSelected = [];
               for (var i = 0; i < vm.tags.length; i++) {
                   vm.tags[i].Selected = false;
               }
           } else {
               vm.tag = tag;
           }
           $mdDialog.show({
               templateUrl: '/scripts/lisa/templates/FRCOM/blog/tag.html',
               parent: $document.find("#tags"),
               locals: { tag: vm.tag },
               controller: function DialogController($scope, $mdDialog) {
                   var ivm = this;
                   ivm.scope = $scope;
                   ivm.cancel = function () {
                       vm.tag = null;
                       $mdDialog.hide(null);
                   };
                   ivm.save = function () {
                       $mdDialog.hide(vm.tag);
                   };
               },
               controllerAs: 'ctrl',
               bindToController: true,
               clickOutsideToClose: true,
               fullscreen: true
           })
              .then(
                 function (tag) {
                     if (tag !== undefined && tag !== null) {
                         if (tag.Id > 0) {
                             ContentFactory.EzBlogTag.update(tag,
                                   function () {
                                       ToastFactory.Success("Values saved correctly!");
                                   },
                                   function (error) {
                                       $log.error(error);
                                       ToastFactory.Error(error);
                                   });
                         } else {
                             ContentFactory.EzBlogTag.save(tag,
                                function (tagCreated) {
                                    vm.tags.push(tagCreated);
                                    ToastFactory.Success("Values saved correctly!");
                                },
                                function (error) {
                                    $log.error(error);
                                    ToastFactory.Error(error);
                                }
                             );
                         }
                     }
                 },
                 function () {
                     vm.tag = null;
                 });
       };
   }

   EzBlogController
     .$inject = ["$log", "$scope", "ContentFactory", "$localStorage", "$rootScope", "$mdDialog", "$document", "$q", "ToastFactory"];
   module.controller("EzBlogController", EzBlogController);

   function PhrasesController($log, $scope, ContentFactory, $localStorage) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.phrases = ContentFactory.MotivationalPhrase;
      vm.phrase = undefined;
      vm.executing = false;
      vm.GetRandomPhrase = function() {
         var randomIndex = Math.floor(Math.random() * vm.phrases.length);
         vm.phrase = vm.phrases[randomIndex];
      }
      
   }

   PhrasesController
      .$inject = ["$log", "$scope", "ContentFactory", "$localStorage"];
   module.controller("PhrasesController", PhrasesController);
})();