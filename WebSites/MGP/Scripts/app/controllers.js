angular.module("mgp.controllers", []).controller("homeController", [
    "$scope", "$window", "homeService", "$uibModal", function ($scope, $window, homeService, $uibModal) {
       $scope.view = { SearchType: "G" };
       $scope.Groups = [];
       $scope.currentPage = 0;
       $scope.pageSize = 12;
       $scope.executingAction = false;
       $scope.resultFailed = false;
       $scope.findAGroup = function () {
          $scope.currentPage = 0;
          $scope.executingAction = true;
          $scope.resultFailed = false;
          $scope.FoundGroups = false;
          $scope.NotFoundGroups = false;
          homeService.FindAGroup($scope.view)
              .then(function (response) {
                 if (!response.data.success) {
                    //something went wrong here and we should display an error
                    $scope.responseText = response.data.responseText;
                    $scope.resultFailed = true;
                 } else {
                    if (response.data.groups.length > 0) {
                       //groups found
                       $scope.Groups = response.data.groups;
                       $scope.FoundGroups = true;
                       $scope.NotFoundGroups = false;
                    } else {
                       //groups not found
                       $scope.FoundGroups = false;
                       $scope.NotFoundGroups = true;
                    }
                 }
                 $scope.executingAction = false;
              }, function (error) {
                 $scope.responseText = "Oops! Something is wrong, please try again later.";
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
                 $log.error(error);
              });
       },
       $scope.numberOfPages = function () {
          return Math.ceil($scope.Groups.length / $scope.pageSize);
       },
       $scope.setCheckBoxDirty = function ($event) {
          var checkbox = $event.target;
          if ($scope.view.SelectedCampaigns === null)
             $scope.view.SelectedCampaigns = [];
          $scope.view.SelectedCampaigns.push({ EventId: $(checkbox).data("ev-id"), MemberHierarchyId: $(checkbox).data("mh-id"), MemberHierarchySubscribed: $(checkbox).prop("checked") });
       },
       $scope.unsubscribe = function () {
          $scope.responseText = "";
          $scope.executingAction = true;
          $scope.resultFailed = false;
          homeService.Unsubscribe($scope.view)
              .then(function (response) {
                 if (!response.data.success) {
                    //something went wrong here and we should display an error
                    $scope.responseText = response.data.responseText;
                    $scope.resultFailed = true;
                 } else {
                    $scope.responseText = response.data.responseText;
                 }
                 $scope.executingAction = false;
              }, function (error) {
                 $scope.responseText = "Oops! Something is wrong, please try again later.";
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
                 $log.error(error);
              });
       }
    }
]).controller("groupPageController", [
    "$scope", "$window", "groupService", "$uibModal", function ($scope, $window, groupService, $uibModal) {
       $scope.view = {};
       $scope.executingAction = false;
       $scope.resultFailed = false;
       $scope.reportSent = false;
       $scope.currentPage = 0;
       $scope.pageSize = 20;
       $scope.carouselImages = [],
       $scope.groupedCarouselImages = [],
       $scope.addToCarouselImage = function (eventId, participantId, anchorUrl, redirectToStore, alternativeText, imageUrl) {
          $scope.carouselImages.push({ EventId: eventId, ParticipantId: participantId, AnchorUrl: anchorUrl.replace('&amp;', '&'), RedirectToStore: redirectToStore, AlternativeText: alternativeText, ImageUrl: imageUrl.replace('&amp;', '&') });
       },
       $scope.requestAKit = function (partnerName) {
          $window.ga('send', 'event', 'Fundraising', 'Kit Request');
          var popup_window = $window.open("http://www.fundraising.com/free-fundraising-kit.aspx?a_aid=" + partnerName, "_blank");
          try {
             popup_window.focus();
          }
          catch (e) {
             $window.location.href = "http://www.fundraising.com/free-fundraising-kit.aspx?a_aid=" + partnerName;
          }
       },
       $scope.setGroupedCarouselImages = function () {
          var i, a = [], b;
          for (i = 0; i < $scope.carouselImages.length; i += 4) {
             b = { image1: $scope.carouselImages[i] };
             if ($scope.carouselImages[i + 1]) {
                b.image2 = $scope.carouselImages[i + 1];
             }
             if ($scope.carouselImages[i + 2]) {
                b.image3 = $scope.carouselImages[i + 2];
             }
             if ($scope.carouselImages[i + 3]) {
                b.image4 = $scope.carouselImages[i + 3];
             }
             a.push(b);
          }
          $scope.groupedCarouselImages = a;
       },
       $scope.safeApply = function () {
          var phase = this.$root.$$phase;
          if (phase != '$apply' && phase != '$digest') {
             $scope.$apply();
          }
       },
       $scope.carousselRedirectToStore = function () {
          if ($("#carousselRedirectToStore").val() === "False") return false;
          $scope.executingAction = true;
          $scope.resultFailed = false;
          groupService.RedirectToStore($("#carousselEventId").val(), $("#carousselParticipantId").val())
              .then(function (response) {
                 if (response.data.success) {
                    $window.ga('send', 'event', 'Store', 'Redirect', response.data.supporterId);
                    $window.location.href = response.data.url;
                 } else {
                    $scope.responseText = response.data.responseText;
                    $scope.resultFailed = true;
                    $scope.executingAction = false;
                 }
              }, function (error) {
                 $scope.responseText = "Oops! Something is wrong, please try again later.";
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
                 $log.error(error);
              });
       },
       $scope.popularStoreItems = [],
       $scope.addToPopularStoreItems = function (name, imagePath, entityId, storefrontCategoryId) {
          $scope.popularStoreItems.push({ Name: name.replace('&gt;', '>').replace('&amp;', '&'), ImagePath: imagePath, EntityId: entityId, StorefrontCategoryId: storefrontCategoryId });
       },
       $scope.popularStoreItemsRedirectToStore = function (obj, displayModal) {          
           if (displayModal) {
		       var modal = $uibModal.open({
			       templateUrl: 'select-participant-modal.html',
			       controller: ("modalController", ["$uibModalInstance", function ($uibModalInstance) {
				       var ivm = this;
				       ivm.cancel = function () {
					       $uibModalInstance.dismiss('cancel');
				       };
				       ivm.ok = function () {
					       $uibModalInstance.close();
				       };
			       }]),
			       controllerAs: '$ctrl'
		       });

		       modal.result.then(
			       function (result) {
                    $window.location.href = "/Group/FindAParticipant?eventId=" + $("#popularItemsEventId").val();
			       },
                function () {
	                $scope.executingAction = true;
	                $scope.resultFailed = false;
				       groupService.RedirectToStore($("#popularItemsEventId").val(),
						       $("#popularItemsParticipantId").val(),
						       obj.target.attributes.eid.value,
						       obj.target.attributes.scid.value)
					       .then(function (response) {
							       if (response.data.success) {
								       $window.ga('send', 'event', 'Store', 'Redirect', response.data.supporterId);
								       $window.location.href = response.data.url;
							       } else {
								       $scope.responseText = response.data.responseText;
								       $scope.resultFailed = true;
								       $scope.executingAction = false;
							       }
						       },
						       function (error) {
							       $scope.responseText = "Oops! Something is wrong, please try again later.";
							       $scope.resultFailed = true;
							       $scope.executingAction = false;
							       $log.error(error);
						       });
			       });
          } else {
		       $scope.executingAction = true;
		       $scope.resultFailed = false;
		       groupService.RedirectToStore($("#popularItemsEventId").val(),
				       $("#popularItemsParticipantId").val(),
				       obj.target.attributes.eid.value,
				       obj.target.attributes.scid.value)
			       .then(function(response) {
					       if (response.data.success) {
						       $window.ga('send', 'event', 'Store', 'Redirect', response.data.supporterId);
						       $window.location.href = response.data.url;
					       } else {
						       $scope.responseText = response.data.responseText;
						       $scope.resultFailed = true;
						       $scope.executingAction = false;
					       }
				       },
				       function(error) {
					       $scope.responseText = "Oops! Something is wrong, please try again later.";
					       $scope.resultFailed = true;
					       $scope.executingAction = false;
					       $log.error(error);
				       });
	       }
       },
       $scope.findAParticipant = function () {
          $scope.executingAction = true;
          $scope.resultFailed = false;
          $scope.currentPage = 0;
          groupService.FindAParticipant($scope.eventId, $scope.view)
              .then(function (response) {
                 if (!response.data.success) {
                    $scope.responseText = response.data.responseText;
                    $scope.resultFailed = true;
                 } else {
                    $scope.Participants = response.data.participants;
                 }
                 $scope.executingAction = false;
              }, function (error) {
                 $scope.responseText = "Oops! Something is wrong, please try again later.";
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
                 $log.error(error);
              });
       },
       $scope.numberOfPages = function () {
          return Math.ceil($scope.Participants.length / $scope.pageSize);
       }
    }
]).controller("loginController", ["$scope", "$window", "$log", "loginService", function ($scope, $window, $log, loginService) {
   $scope.executingAction = false;
   $scope.resultFailed = false;
   $scope.registerView = { Newsletter: true, GroupType: 1 };
   $scope.login = function (returnUrl) {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      loginService.Login($scope.view)
          .then(function (response) {
             if (response.data.success) {
                $window.ga('send', 'event', 'User', 'Login', 'Forms');
                if (returnUrl)
                   $window.location.href = returnUrl;
                else
                   $window.location.href = "/CampaignManager/Campaigns";
             } else {
                $scope.responseText = response.data.responseText;
                $scope.resultFailed = true;
                $scope.executingAction = false;
             }
          }, function (error) {
             $scope.responseText = "Oops! Something is wrong, please try again later.";
             $scope.resultFailed = true;
             $scope.executingAction = false;
             $log.error(error);
          });
   },
   $scope.registerExternal = function () {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      loginService.RegisterExternal($scope.registerView, $scope.providerName, $scope.providerUserId)
          .then(function (response) {
             if (response.data.success) {
                $window.ga('send', 'event', 'User', 'Register', 'Sponsor');
                $window.location.href = "/CampaignManager/Register?participantId=" + response.data.participantId;
             } else {
                $scope.responseText = response.data.responseText;
                $scope.resultFailed = true;
                $scope.executingAction = false;
             }
          }, function (error) {
             $scope.responseText = "Oops! Something is wrong, please try again later.";
             $scope.resultFailed = true;
             $scope.executingAction = false;
             $log.error(error);
          });
   },
   $scope.register = function () {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      loginService.Register($scope.registerView)
          .then(
              function (response) {
                 if (response.data.success) {
                    $window.ga('send', 'event', 'User', 'Register', 'Sponsor');
                    $window.location.href = "/registration/step-1?participantId=" + response.data.participantId;
                 } else {
                    $scope.responseText = response.data.responseText;
                    $scope.resultFailed = true;
                    $scope.executingAction = false;
                 }
              }, function (error) {
                 $scope.responseText = "Oops! Something is wrong, please try again later.";
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
                 $log.error(error);
              }
          );
   },
   $scope.recoverPassword = function () {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      loginService.RecoverPassword($scope.email)
          .then(function (response) {
             if (response.data.success) {
                $window.ga('send', 'event', 'User', 'Recover Password');
             } else {
                $scope.resultFailed = true;
             }
             $scope.success = response.data.success;
             $scope.executingAction = false;
             $scope.responseText = response.data.responseText;
          }, function (error) {
             $scope.responseText = "Oops! Something is wrong, please try again later.";
             $scope.resultFailed = true;
             $scope.executingAction = false;
             $log.error(error);
          });
   },
   $scope.participantRegister = function () {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      loginService.ParticipantRegister($scope.eventId, $scope.registerView)
          .then(function (response) {
             if (response.data.success) {
                $window.ga('send', 'event', 'User', 'Register', 'Participant');
                $window.location.href = response.data.url;
             } else {
                $scope.responseText = response.data.responseText;
                $scope.resultFailed = true;
                $scope.executingAction = false;
             }
          }, function (error) {
             $scope.responseText = "Oops! Something is wrong, please try again later.";
             $scope.resultFailed = true;
             $scope.executingAction = false;
             $log.error(error);
          });
   },
   $scope.participantJoin = function () {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      loginService.ParticipantJoin($scope.eventId, $scope.registerView)
          .then(function (response) {
             if (response.data.success) {
                $window.ga('send', 'event', 'User', 'Register', 'Participant');
                $window.location.href = response.data.url;
             } else {
                $scope.responseText = response.data.responseText;
                $scope.resultFailed = true;
                $scope.executingAction = false;
             }
          }, function (error) {
             $scope.responseText = "Oops! Something is wrong, please try again later.";
             $scope.resultFailed = true;
             $scope.executingAction = false;
             $log.error(error);
          });
   }
}]).controller("campaignManagerController", ["$scope", "$window", "$rootScope", "$uibModal", "$log", "$filter", "campaignManagerService", "Upload", "$q", function ($scope, $window, $rootScope, $uibModal, $log, $filter, campaignManagerService, Upload, $q) {
    
    $scope.executingAction = false;
   $scope.resultFailed = false;
   $scope.recipients = "";
   $scope.reminderFrecuency = "7";
   $scope.importContacts = true;
   $scope.registerView = {};
	$scope.step0 = function () {
		$scope.executingAction = true;
		$scope.resultFailed = false;
      campaignManagerService.Step0($scope.register)
			.then(function (response) {
					if (response.data.success) {
						$window.location.href = "/registration/step-1?participantId=" + response.data.participantId;
					} else {
						$scope.responseText = response.data.responseText;
						$scope.resultFailed = true;
						$scope.executingAction = false;
					}
				},
				function (error) {
					$scope.responseText = "Oops! Something is wrong, please try again later.";
					$scope.resultFailed = true;
					$scope.executingAction = false;
					$log.error(error);
				});
	};
   $scope.step1 = function() {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      campaignManagerService.Step1($scope.participantId, $scope.view)
         .then(function (response) {
            if (response.data.success) {
               $window.location.href = "/registration/step-2?participantId=" + response.data.participantId;
            } else {
               $scope.responseText = response.data.responseText;
               $scope.resultFailed = true;
               $scope.executingAction = false;
            }
         },
         function (error) {
            $scope.responseText = "Oops! Something is wrong, please try again later.";
            $scope.resultFailed = true;
            $scope.executingAction = false;
            $log.error(error);
         });
   };
   $scope.step2 = function() {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      var imageUploadPromises = [];
      if ($scope.registerView.Image) {
         var blob = Upload.dataUrltoBlob($scope.registerView.CroppedImage, "i.jpg");
         var promise = Upload.upload({ url: '/CampaignManager/ImageFileUpload', data: { file: blob } });
         imageUploadPromises.push(promise);
      }
      $q.all(imageUploadPromises)
         .then(function(promiseResults) {
            if (promiseResults.length == 1) {
               $scope.registerView.PersonalizationImages[0].ImageURL = promiseResults[0].data.savedImagePath;
            }
            campaignManagerService.Step2($scope.participantId, $scope.registerView.PersonalizationImages[0].ImageURL)
                  .then(function (response) {
                     if (response.data.success) {
                        $window.location.href = "/registration/step-3?participantId=" + $scope.participantId;
                     } else {
                        $scope.responseText = response.data.responseText;
                        $scope.resultFailed = true;
                        $scope.executingAction = false;
                     }
                     
                  },
                     function (error) {
                        $scope.responseText = "Oops! Something is wrong, please try again later.";
                        $scope.resultFailed = true;
                        $scope.executingAction = false;
                        $log.error(error);
                     });
         });
   };
   $scope.step3 = function (isPreview) {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      campaignManagerService.Step3($scope.participantId, $scope.registerView.Body)
         .then(function (response) {
            if (response.data.success) {
               if (isPreview) {
                  var iframe = document.querySelector('#previewIframe');
                  iframe.contentWindow.location.reload(true);  
                  $('#preview-modal').modal('toggle');
                  $scope.executingAction = false;
               } else {
                  $window.location.href = "/registration/step-4?participantId=" + response.data.participantId;
               }
               
            } else {
               $scope.responseText = response.data.responseText;
               $scope.resultFailed = true;
               $scope.executingAction = false;
            }
            
         },
         function (error) {
            $scope.responseText = "Oops! Something is wrong, please try again later.";
            $scope.resultFailed = true;
            $scope.executingAction = false;
            $log.error(error);
         });
   };

   $scope.step4 = function () {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      campaignManagerService.Step4($scope.participantId, $scope.redirect)
         .then(function (response) {
            if (response.data.success) {
               $("#redirect").html($scope.redirect);
               $("#modify-link-modal").modal("hide");
            } else {
               $scope.responseText = response.data.responseText;
               $scope.resultFailed = true;
            }
            $scope.executingAction = false;
         },
         function (error) {
            $scope.responseText = "Oops! Something is wrong, please try again later.";
            $scope.resultFailed = true;
            $scope.executingAction = false;
            $log.error(error);
         });
   };
   $scope.step5 = function () {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      campaignManagerService.Step5($scope.participantId, $scope.registerView.Recipients, $scope.reminderFrecuency)
         .then(function (response) {
            if (response.data.success) {
               $window.location.href = "/registration/step-6?participantId=" + $scope.participantId;
            } else {
               $scope.responseText = response.data.responseText;
               $scope.resultFailed = true;
               $scope.executingAction = false;
            }
         },
         function (error) {
            $scope.responseText = "Oops! Something is wrong, please try again later.";
            $scope.resultFailed = true;
            $scope.executingAction = false;
            $log.error(error);
         });
   };
   $scope.checkEventRedirectAvailability = function() {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      campaignManagerService.CheckEventRedirectAvailability($scope.registerView)
         .then(function(response) {
               if (response.data.success) {
                  $scope.isAvailable = response.data.isAvailable;
               } else {
                  $scope.responseText = response.data.responseText;
                  $scope.resultFailed = true;
               }
               $scope.executingAction = false;
            },
            function(error) {
               $scope.responseText = "Oops! Something is wrong, please try again later.";
               $scope.resultFailed = true;
               $scope.executingAction = false;
               $log.error(error);
            });
   };
  $scope.uploadPicture = function (file) {
     Upload.resize(file, { width: 300, height: 210 })
        .then(function (resizedFile) {
           Upload.upload({
              url: '/CampaignManager/ImageFileUpload',
              data: { file: resizedFile }
           })
              .then(function (resp) {
                 $log.info(resp);
                 $scope.registerView.PersonalizationImages[0].ImageURL = resp.data.savedImagePath;
              },
                 function (resp) {
                    $log.error(resp);
                 },
                 function (evt) {
                    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                    console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
                 });
        }
        );
  };
   $scope.register = function () {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      var imageUploadPromises = [];
      if ($scope.registerView.Image) {
         var blob = Upload.dataUrltoBlob($scope.registerView.CroppedImage, "i.jpg");
         console.log(blob);
         var promise = Upload.upload({ url: '/CampaignManager/ImageFileUpload', data: { file: blob } });
         imageUploadPromises.push(promise);
      }
         $q.all(imageUploadPromises)
            .then(function(promiseResults) {
               if (promiseResults.length == 1) {
                  $scope.registerView.PersonalizationImages[0].ImageURL = promiseResults[0].data.savedImagePath;
               }
               campaignManagerService.Register($scope.participantId, $scope.registerView)
                  .then(function(response) {
                        if (response.data.success) {
                           $window.location.href = "/CampaignManager/KickOff?participantId=" +
                              response.data.participantId;
                        } else {
                           $scope.responseText = response.data.responseText;
                           $scope.resultFailed = true;
                        }
                        $scope.executingAction = false;
                     },
                     function(error) {
                        $scope.responseText = "Oops! Something is wrong, please try again later.";
                        $scope.resultFailed = true;
                        $scope.executingAction = false;
                        $log.error(error);
                     });
            });
      },
      $scope.pagePreview = function () {
         $scope.executingAction = true;
         $scope.resultFailed = false;
         campaignManagerService.Register($scope.participantId, $scope.registerView)
            .then(function (response) {
               if (response.data.success) {
                  $window.open("/Group/Index?eventId=" + $scope.eventId);
               } else {
                  $scope.responseText = response.data.responseText;
                  $scope.resultFailed = true;
               }
               $scope.executingAction = false;
            },
               function (error) {
                  $scope.responseText = "Oops! Something is wrong, please try again later.";
                  $scope.resultFailed = true;
                  $scope.executingAction = false;
                  $log.error(error);
               });
      },
      $scope.kickoff = function () {
         $scope.executingAction = true;
         $scope.resultFailed = false;
         campaignManagerService.KickOff($scope.participantId, $scope.registerView)
            .then(function (response) {
               if (response.data.success) {
                  $window.ga('send', 'social', 'Email', 'Share', '/CampaignManager/Kickoff');
                  $window.location.href = "/CampaignManager/Index?participantId=" + response.data.participantId;
               } else {
                  if (response.data.responseInfo !== undefined) {
                     var responseInfo = $.parseJSON(response.data.responseInfo);
                     if (responseInfo.Type == 1 && // ERROR ResponseType
                        responseInfo.ContentType == "application/json; charset=utf-8") {
                        if (responseInfo.ModelStateError.Value.Errors[0].ErrorMessage
                           .indexOf("e-mail address") !=
                           -1) {
                           var index = responseInfo.ModelStateError.Key.match(/\d+/);
                           $scope.kickOffFilterOptions.filterIndex = parseInt(index);
                           $scope.openContactsModal();
                        }
                     }
                  }
                  $scope.responseText = response.data.responseText;
                  $scope.resultFailed = true;
               }
               $scope.executingAction = false;
            },
               function (error) {
                  $scope.responseText = "Oops! Something is wrong, please try again later.";
                  $scope.resultFailed = true;
                  $scope.executingAction = false;
                  $log.error(error);
               });
      },
      $scope.isGoalInEditMode = false,
      $scope.enableGoalEditMode = function () {
         $scope.isGoalInEditMode = true;
      },
      $scope.cancelGoalEditMode = function () {
         $scope.isGoalInEditMode = false;
         $scope.responseText = "";
         $scope.resultFailed = false;
      },
      $scope.changeGoal = function () {
         $scope.executingAction = true;
         $scope.resultFailed = false;
         if ($scope.goal === undefined || $scope.goal === null) {
            $scope.responseText = "Invalid input for Goal";
            $scope.resultFailed = true;
            return;
         }
         if ($scope.goal < 0) {
            $scope.responseText = "The Goal should be a positive number";
            $scope.resultFailed = true;
            return;
         }
         campaignManagerService.ChangeGoal($scope.participantId, $scope.goal)
            .then(function (response) {
               if (response.data.success) {
                  $scope.cancelGoalEditMode();
               } else {
                  $scope.responseText = response.data.responseText;
                  $scope.resultFailed = true;
               }
               $scope.executingAction = false;
            });
      },
    $scope.deletePersonalizationImage = function (id) {
       $scope.executingAction = false;
       $scope.resultFailed = false;
       campaignManagerService.DeletePersonalizationImage($scope.participantId, id)
           .then(function (response) {
              if (response.data.success) {
                 $scope.registerView.PersonalizationImages = [];
                 $scope.registerView.PersonalizationImages.push({
                    ImageId: 0,
                    PersonalizationId: 0,
                    IsCoverAlbum: true,
                    ImageURL: response.data.defaultImage,
                    IsGalleryImage: true
                 });
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
              $scope.executingAction = false;
           });
    },
   $scope.getGalleryImages = function () {
      $scope.executingAction = false;
      $scope.resultFailed = false;
      campaignManagerService.GetGalleryImages($scope.registerView)
          .then(function (response) {
             if (response.data.success) {
                $scope.galleryImages = response.data.galleryImages;
             } else {
                $scope.responseText = response.data.responseText;
                $scope.resultFailed = true;
             }
             $scope.executingAction = false;
          });
   },
   $scope.selectImageFromGallery = function (image) {
      $scope.registerView.Image = undefined;
      $scope.registerView.PersonalizationImages = [];
      $scope.registerView.PersonalizationImages.push({
         ImageId: 0,
         PersonalizationId: 0,
         IsCoverAlbum: true,
         ImageURL: image.FullFilePath,
         IsGalleryImage: true
      });
   },
    $scope.deleteImageFromServer = function (filePath) {
       $scope.executingAction = false;
       $scope.resultFailed = false;
       campaignManagerService.DeleteImageFromServer(filePath)
           .then(function (response) {
              if (response.data.success) {

              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
              $scope.executingAction = false;
           });
    },
    $rootScope.safeApply = function (fn) {
       var phase = this.$root.$$phase;
       if (phase == '$apply' || phase == '$digest') {
          if (fn) {
             fn();
          }
       } else {
          this.$apply(fn);
       }
    },
    $scope.updateInformation = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.UpdateMyInformation($scope.participantId, $scope.registerView)
           .then(function (response) {
              if (response.data.success) {
                 $scope.success = true;
                 $scope.responseText = response.data.responseText;
                 $scope.registerView.IgnoreAddressHygiene = true;
              } else {
                 if (response.data.proposedAddress !== null && response.data.proposedAddress !== undefined) {
                    $scope.newAddress = response.data.proposedAddress.Address1;
                    $scope.newCity = response.data.proposedAddress.City;
                    $scope.newState = response.data.proposedAddress.Region;
                    $scope.newZipCode = response.data.proposedAddress.PostCode;
                    angular.element("#modalAddressHygiene").modal("show");
                 } else {
                    $scope.responseText = response.data.responseText;
                    $scope.resultFailed = true;
                 }
              }
              $scope.executingAction = false;
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.defaultRedirectToStore = function (eventId, participantId, newWindow) {
       if (newWindow === undefined) {
          newWindow = false;
       }
       $scope.executingAction = true;
       $scope.resultFailed = false;
       campaignManagerService.RedirectToStore(eventId, participantId)
           .then(function (response) {
              if (response.data.success) {
                 $window.ga('send', 'event', 'Store', 'Redirect', response.data.supporterId);
                 if (newWindow) {
                    var popup_window = $window.open(response.data.url, "_blank");
                    try {
                       popup_window.focus();
                    }
                    catch (e) {
                       $window.location.href = response.data.url;
                    }
                 } else {
                    $window.location.href = response.data.url;
                 }
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
              }
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.create = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       campaignManagerService.Create($scope.registerView)
           .then(function (response) {
              if (response.data.success) {
                 $window.location.href = "/CampaignManager/Register?participantId=" + response.data.participantId;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
              $scope.executingAction = false;
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.end = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       campaignManagerService.End($scope.participantId)
           .then(function (response) {
              if (response.data.success) {
                 $window.location.href = "/CampaignManager/Index?participantId=" + $scope.participantId;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
              $scope.executingAction = false;
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    
    $scope.personalizePage = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       var imageUploadPromises = [];
       if ($scope.registerView.Image) {
          var blob = Upload.dataUrltoBlob($scope.registerView.CroppedImage, "i.jpg");
          console.log(blob);
          var promise = Upload.upload({ url: '/CampaignManager/ImageFileUpload', data: { file: blob } });
          imageUploadPromises.push(promise);
       }
       $q.all(imageUploadPromises).then(function (promiseResults) {
          if (promiseResults.length == 1) {
             $scope.registerView.PersonalizationImages[0].ImageURL = promiseResults[0].data.savedImagePath;
          }
          campaignManagerService.Register($scope.participantId, $scope.registerView)
           .then(function (response) {
              $scope.resultFailed = !response.data.success;
              $scope.responseText = response.data.responseText;
              $scope.success = response.data.success;
              $scope.executingAction = false;

              if (response.data.success == true) {
                  $scope.registerView.PersonalizationImages = [];
                  $scope.registerView.PersonalizationImages.push(response.data.image);
                  $scope.registerView.Image = undefined;
              }
             }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
       });
       
    },
    $scope.manageImportedContacts = function (recipients, providerName) {
       $window.ga('send', 'event', 'Campaign Manager', 'Import Contacts', providerName, recipients.split(",").length);
       var text = recipients;
       var result = [];
       var recipientsSplited = text.split(",");
       for (var i = 0; i < recipientsSplited.length; i++) {
          if (recipientsSplited[i] != "") {
             var recipientSplited = [];
             if (recipientsSplited[i].indexOf("|") != -1)
                recipientSplited = recipientsSplited[i].trim().split("|");
             else
                recipientSplited = recipientsSplited[i].trim().split(" ");
             if (recipientSplited.length == 1) {
                //only email specified
                result.push({ Email: recipientSplited[0], FirstName: "", LastName: "" });
             } else {
                //email and name specified
                var lastName = "";
                for (var j = 1; j < recipientSplited.length - 1; j++) {
                   lastName += " " + recipientSplited[j];
                }
                result.push({ Email: recipientSplited[recipientSplited.length - 1], FirstName: recipientSplited[0], LastName: lastName });
             }
          }
       }

       if ($scope.registerView.Recipients == null) {
          $scope.registerView.Recipients = [];
       }
       // Add the ones that don't exist and update the ones that already exist
       for (var i = 0; i < result.length; i++) {
          var email = result[i].Email, firstName = result[i].FirstName, lastName = result[i].LastName;
          var recipientAlreadyExists = false;
          for (var j = 0; j < $scope.registerView.Recipients.length; j++) {
             if ($scope.registerView.Recipients[j].Email === email) {
                $scope.registerView.Recipients[j].FirstName = firstName;
                $scope.registerView.Recipients[j].LastName = lastName;
                recipientAlreadyExists = true;
                j = $scope.registerView.Recipients.length;
             }
          }
          if (!recipientAlreadyExists) {
             $scope.registerView.Recipients.push({ Email: email, FirstName: firstName, LastName: lastName, IsManual: false });
          }
       }
       if ($scope.recipients == "") {
          $scope.recipients = recipients;
       } else {
          $scope.recipients += ", " + recipients;
       }
    },
    $scope.manageContacts = function () {
       var text = $scope.recipients;
       var result = [];
       if (typeof text != 'undefined') {
          var recipientsSplited = text.split(",");
          for (var i = 0; i < recipientsSplited.length; i++) {
             if (recipientsSplited[i] != "") {
                var recipientSplited = [];
                if (recipientsSplited[i].indexOf("|") != -1)
                   recipientSplited = recipientsSplited[i].trim().split("|");
                else
                   recipientSplited = recipientsSplited[i].trim().split(" ");
                if (recipientSplited.length == 1) {
                   //only email specified
                   result.push({ Email: recipientSplited[0], FirstName: "", LastName: "" });
                } else {
                   //email and name specified
                   var lastName = "";
                   for (var j = 1; j < recipientSplited.length - 1; j++) {
                      lastName += " " + recipientSplited[j];
                   }
                   result.push({ Email: recipientSplited[recipientSplited.length - 1], FirstName: recipientSplited[0], LastName: lastName });
                }
             }
          }
       } else {
          $scope.registerView.Recipients = [];
          $scope.recipients = [];
       }

       if ($scope.registerView.Recipients == null) {
          $scope.registerView.Recipients = [];
       }
       // Add the ones that don't exist and update the ones that already exist
       for (var i = 0; i < result.length; i++) {
          var email = result[i].Email, firstName = result[i].FirstName, lastName = result[i].LastName;
          var recipientAlreadyExists = false;
          for (var j = 0; j < $scope.registerView.Recipients.length; j++) {
             if ($scope.registerView.Recipients[j].Email === email) {
                $scope.registerView.Recipients[j].FirstName = firstName;
                $scope.registerView.Recipients[j].LastName = lastName;
                recipientAlreadyExists = true;
                j = $scope.registerView.Recipients.length;
             }
          }
          if (!recipientAlreadyExists) {
             $scope.registerView.Recipients.push({ Email: email, FirstName: firstName, LastName: lastName, IsManual: true });
          }
       }
       var tempRecipients = [];
       //delete the ones that don't exist anymore
       for (var i = 0; i < $scope.registerView.Recipients.length; i++) {
          var recipientAlreadyExists = false;
          for (var j = 0; j < result.length; j++) {
             var modelEmail = $scope.registerView.Recipients[i].Email;
             var newEmail = result[j].Email;
             if (modelEmail == newEmail) {
                recipientAlreadyExists = true;
                j = result.length;
             }
          }
          if (recipientAlreadyExists) {
             tempRecipients.push({ Email: $scope.registerView.Recipients[i].Email, FirstName: $scope.registerView.Recipients[i].FirstName, LastName: $scope.registerView.Recipients[i].LastName, IsManual: $scope.registerView.Recipients[i].IsManual });
          }
       }
       $scope.registerView.Recipients = tempRecipients;
    },
    $scope.kickOffFilterOptions = {
       filterIndex: "",
       useExternalFilter: true
    },
    $scope.openContactsModal = function () {

       var modalInstance = $uibModal.open({
          templateUrl: 'myModalContent.html',
          controller: ContactsModalInstanceCtrl,
          size: 'lg',
          resolve: {
             items: function () {
                return $scope.registerView.Recipients;
             },
             filterOptions: function () {
                return $scope.kickOffFilterOptions;
             }
          }
       });
       modalInstance.result.then(function (items) {
          $scope.registerView.Recipients = items;
          var textAreaString = "";
          for (var i = 0; i < items.length; i++) {
             textAreaString = textAreaString + items[i]["FirstName"] + " " + items[i]["LastName"] + " " + items[i]["Email"] + ", ";
          }
          $scope.recipients = textAreaString;
       });
    },
    $scope.relaunch = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       campaignManagerService.Relaunch($scope.participantId)
           .then(function (response) {
              if (response.data.success) {
                 $window.location.href = "/CampaignManager/RelaunchInformation?participantId=" + response.data.participantId;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
              $scope.executingAction = false;
           });
    },
    $scope.relaunchInformation = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.UpdateMyInformation($scope.participantId, $scope.registerView)
           .then(function (response) {
              if (response.data.success) {
                 $scope.success = true;
                 $window.location.href = "/CampaignManager/Register?participantId=" + response.data.participantId;
              } else {
                 if (response.data.proposedAddress !== null && response.data.proposedAddress !== undefined) {
                    $scope.newAddress = response.data.proposedAddress.Address1;
                    $scope.newCity = response.data.proposedAddress.City;
                    $scope.newState = response.data.proposedAddress.Region;
                    $scope.newZipCode = response.data.proposedAddress.PostCode;
                    angular.element("#modalAddressHygiene").modal("show");
                 } else {
                    $scope.responseText = response.data.responseText;
                    $scope.resultFailed = true;
                 }
              }
              $scope.executingAction = false;
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.sendNewMessage = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.SendNewMessage($scope.participantId, $scope.registerView)
           .then(function (response) {
              if (response.data.success) {
                 $scope.success = true;
                 $scope.responseText = response.data.responseText;
                 $window.ga('send', 'social', 'Email', 'Share', '/CampaignManager/SendNewMessage');
              } else {
                 if (response.data.responseInfo !== undefined) {
                    var responseInfo = $.parseJSON(response.data.responseInfo);
                    if (responseInfo.Type == 1 && // ERROR ResponseType
                        responseInfo.ContentType == "application/json; charset=utf-8") {
                       if (responseInfo.ModelStateError.Value.Errors[0].ErrorMessage.indexOf("e-mail address") != -1) {
                          var index = responseInfo.ModelStateError.Key.match(/\d+/);
                          $scope.kickOffFilterOptions.filterIndex = parseInt(index);
                          $scope.openContactsModal();
                       }
                    }
                 }

                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
              $scope.executingAction = false;
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.totalAmount = 0,
    $scope.totalProfit = 0,
    $scope.findCampaignReport = function (hideProfit) {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.CampaignReport($scope.participantId, $scope.searchType, null, $scope.from, $scope.to, false)
           .then(function (response) {
              if (response.data.success) {
                 $scope.totalAmount = response.data.totalAmount;
                 $scope.totalProfit = response.data.totalProfit;
                 $scope.data = response.data.results;
                 $scope.from = response.data.from;
                 $scope.to = response.data.to;
                 $window.from.setValue(response.data.from);
                 $window.to.setValue(response.data.to);
                 $window.ga('send', 'event', 'Campaign Manager', 'View Report', "Campaign Report");
                 var categories = [];
                 var profit = [];
                 var amountSold = [];
                 for (var i = 0; i < $scope.data.length; i++) {
                    var row = $scope.data[i];
                    categories.push(row.MemberName);
                    profit.push(row.Profit);
                    if (hideProfit) {
                       amountSold.push(row.AmountSold);
                    } else {
                       amountSold.push(row.AmountSold - row.Profit);
                    }
                 }
                 if (hideProfit) {
                    var series = [{ name: 'Amount Sold', data: amountSold }];
                 } else {
                    var series = [{ name: 'Profit', data: profit }, { name: 'Amount Sold', data: amountSold }];
                 }
                 $('#chartContainer').highcharts({
                    chart: {
                       type: 'bar'
                    },
                    title: {
                       text: ''
                    },
                    xAxis: {
                       categories: categories
                    },
                    yAxis: {
                       min: 0,
                       title: {
                          text: ''
                       }
                    },
                    legend: {
                       reversed: true
                    },
                    plotOptions: {
                       series: {
                          stacking: 'normal'
                       }
                    },
                    series: series
                 });
                 $scope.executingAction = false;
                 $scope.success = true;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
              }
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.findCampaignReportDetail = function (memberName) {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.CampaignReport($scope.participantId, $scope.searchType, memberName, $scope.from, $scope.to, true)
           .then(function (response) {
              if (response.data.success) {
                 $scope.detail = response.data.detail;
                 $scope.memberName = memberName;
                 $scope.from = response.data.from;
                 $scope.to = response.data.to;
                 $window.from.setValue(response.data.from);
                 $window.to.setValue(response.data.to);
                 $window.ga('send', 'event', 'Campaign Manager', 'View Report', "Campaign Report Detail");
                 $scope.executingAction = false;
                 $scope.success = true;
                 var modalInstance = $uibModal.open({
                    templateUrl: 'myModalContent.html',
                    controller: CampaignReportDetailModalInstanceCtrl,
                    size: 'lg',
                    resolve: {
                       items: function () {
                          return response.data.detail;
                       },
                       memberName: function () {
                          return memberName;
                       },
                       totalAmount: function () {
                          return response.data.totalAmount;
                       },
                       totalProfit: function () {
                          return response.data.totalProfit;
                       }
                    }
                 });
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
              }
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.findCampaignDonationReport = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.CampaignDonationReport($scope.participantId, false)
           .then(function (response) {
              if (response.data.success) {
                 $scope.totalAmount = response.data.totalAmount;
                 $scope.totalProfit = response.data.totalProfit;
                 $scope.data = response.data.results;
                 $window.ga('send', 'event', 'Campaign Manager', 'View Report', "Campaign Donation Report");
                 $scope.executingAction = false;
                 $scope.success = true;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
              }
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.findCampaignDonationReportDetail = function (memberName) {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.CampaignDonationReport($scope.participantId, true)
           .then(function (response) {
              if (response.data.success) {
                 $scope.detail = response.data.detail;
                 $scope.memberName = memberName;
                 $window.ga('send', 'event', 'Campaign Manager', 'View Report', "Campaign Donation Report Detail");
                 $scope.executingAction = false;
                 $scope.success = true;
                 var modalInstance = $uibModal.open({
                    templateUrl: 'myModalContent.html',
                    controller: CampaignReportDetailModalInstanceCtrl,
                    size: 'lg',
                    resolve: {
                       items: function () {
                          return response.data.detail;
                       },
                       memberName: function () {
                          return memberName;
                       },
                       totalAmount: function () {
                          return response.data.totalAmount;
                       },
                       totalProfit: function () {
                          return response.data.totalProfit;
                       }
                    }
                 });
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
              }
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.findCampaignReportParticipant = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.CampaignReportParticipant($scope.participantId, $scope.searchType, $scope.from, $scope.to)
           .then(function (response) {
              if (response.data.success) {
                 $scope.totalAmount = response.data.totalAmount;
                 $scope.totalProfit = response.data.totalProfit;
                 $scope.data = response.data.results;
                 $scope.from = response.data.from;
                 $scope.to = response.data.to;
                 $window.from.setValue(response.data.from);
                 $window.to.setValue(response.data.to);
                 $window.ga('send', 'event', 'Campaign Manager', 'View Report', "Campaign Report Participant");
                 $scope.executingAction = false;
                 $scope.success = true;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
              }
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.findCampaignDonationReportParticipant = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.CampaignDonationReportParticipant($scope.participantId)
           .then(function (response) {
              if (response.data.success) {
                 $scope.totalAmount = response.data.totalAmount;
                 $scope.totalProfit = response.data.totalProfit;
                 $scope.data = response.data.results;
                 $window.ga('send', 'event', 'Campaign Manager', 'View Report', "Campaign Donation Report Participant");
                 $scope.executingAction = false;
                 $scope.success = true;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
              }
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.findGroupReport = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.GroupReport($scope.participantId, $scope.searchType, $scope.from, $scope.to)
           .then(function (response) {
              if (response.data.success) {
                 $scope.totalAmount = response.data.totalAmount;
                 $scope.totalProfit = response.data.totalProfit;
                 $scope.data = response.data.results;
                 $window.ga('send', 'event', 'Campaign Manager', 'View Report', "Group Report");
                 var categories = [];
                 var profit = [];
                 var amountSold = [];
                 for (var i = 0; i < $scope.data.length; i++) {
                    var row = $scope.data[i];
                    categories.push(row.MemberName);
                    profit.push(row.Profit);
                    amountSold.push(row.AmountSold - row.Profit);
                 }
                 $('#chartContainer').highcharts({
                    chart: {
                       type: 'bar'
                    },
                    title: {
                       text: ''
                    },
                    xAxis: {
                       categories: categories
                    },
                    yAxis: {
                       min: 0,
                       title: {
                          text: ''
                       }
                    },
                    legend: {
                       reversed: true
                    },
                    plotOptions: {
                       series: {
                          stacking: 'normal'
                       }
                    },
                    series: [{
                       name: 'Profit',
                       data: profit
                    }, {
                       name: 'Amount Sold',
                       data: amountSold
                    }]
                 });
                 $scope.executingAction = false;
                 $scope.success = true;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
              }
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
    $scope.findGroupDonationReport = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.DonationGroupReport($scope.participantId)
           .then(function (response) {
              if (response.data.success) {
                 $scope.totalAmount = response.data.totalAmount;
                 $scope.totalProfit = response.data.totalProfit;
                 $scope.data = response.data.results;
                 $window.ga('send', 'event', 'Campaign Manager', 'View Report', "Group Report");
                 var categories = [];
                 var profit = [];
                 var amountSold = [];
                 for (var i = 0; i < $scope.data.length; i++) {
                    var row = $scope.data[i];
                    categories.push(row.MemberName);
                    profit.push(row.Profit);
                    amountSold.push(row.AmountSold - row.Profit);
                 }
                 $('#chartContainer').highcharts({
                    chart: {
                       type: 'bar'
                    },
                    title: {
                       text: ''
                    },
                    xAxis: {
                       categories: categories
                    },
                    yAxis: {
                       min: 0,
                       title: {
                          text: ''
                       }
                    },
                    legend: {
                       reversed: true
                    },
                    plotOptions: {
                       series: {
                          stacking: 'normal'
                       }
                    },
                    series: [{
                       name: 'Profit',
                       data: profit
                    }, {
                       name: 'Amount Sold',
                       data: amountSold
                    }]
                 });
                 $scope.executingAction = false;
                 $scope.success = true;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
                 $scope.executingAction = false;
              }
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });
    },
   /** START CONTACTS GRID **/
    $scope.contacts = [],
    $scope.contactsPagingOptions = {
       pageSizes: [10, 25, 50],
       pageSize: 10,
       currentPage: 1
    },
    $scope.contactsFilterOptions = {
       filterText: "",
       useExternalFilter: true
    },
    $scope.contactsGridOptions = {
       data: 'contacts',
       enableCellSelection: true,
       enableRowSelection: false,
       enableCellEdit: true,
       enablePaging: true,
       showFooter: true,
       totalServerItems: 'contactsTotalServerItems',
       pagingOptions: $scope.contactsPagingOptions,
       filterOptions: $scope.contactsFilterOptions,
       rowTemplate: '<div ng-style="{\'cursor\': row.cursor, \'z-index\': col.zIndex() }" ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell {{col.cellClass}}" ng-cell></div>',
       columnDefs: [
           { field: 'Id', displayName: 'Id', visible: false },
           { field: 'FirstName', displayName: 'First Name' },
           { field: 'LastName', displayName: 'Last Name' },
           { field: 'Email', displayName: 'Email' },
           { field: '', enableCellEdit: false, width: 50, displayName: ' ', cellTemplate: '<div class="text-center tip" data-toggle="tooltip" data-placement="left" title="Delete" ng-click="contactsDelete(row)"><a href="#"><span class="glyphicon glyphicon-remove"></span></a></div>' }
       ]
    },
    $scope.contactsGetPagedDataAsync = function (pageSize, page, searchText) {
       setTimeout(function () {
          $scope.executingAction = true;
          campaignManagerService.FindContacts($scope.participantId, pageSize, page, searchText)
          .then(function (response) {
             $scope.contacts = response.data.contactsPaged;
             $scope.contactsTotalServerItems = response.data.total;
             if (!$scope.$$phase) {
                $scope.$apply();
             }
             $scope.executingAction = false;
          });
       }, 1);

    },
    $scope.contactsDelete = function (row) {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.DeleteContact(row.entity.Id)
           .then(function (response) {
              if (response.data.success) {
                 $scope.success = true;
                 $scope.responseText = response.data.responseText;
                 $scope.contacts.splice(row.rowIndex, 1);
                 $scope.contactsTotalServerItems--;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
              $scope.executingAction = false;
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });

    },
    $scope.$on('ngGridEventEndCellEdit', function (evt) {
       var entity = evt.targetScope.row.entity;
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.UpdateContact($scope.participantId, entity)
       .then(function (response) {
          if (response.data.success) {
             $scope.success = true;
             $scope.responseText = response.data.responseText;
          } else {
             $scope.responseText = response.data.responseText;
             $scope.resultFailed = true;
          }
          $scope.executingAction = false;
       });
    }),
    $scope.$watch('contactsPagingOptions', function (newVal, oldVal) {
       if (newVal !== oldVal) {
          $scope.contactsGetPagedDataAsync($scope.contactsPagingOptions.pageSize, $scope.contactsPagingOptions.currentPage, $scope.contactsFilterOptions.filterText);
       }
    }, true),
    $scope.$watch('contactsFilterOptions', function (newVal, oldVal) {
       if (newVal !== oldVal) {
          $scope.contactsGetPagedDataAsync($scope.contactsPagingOptions.pageSize, $scope.contactsPagingOptions.currentPage, $scope.contactsFilterOptions.filterText);
       }
    }, true),
   /** END CONTACTS GRID **/
   /** START INVALID CONTACTS GRID **/
    $scope.invalidContacts = [],
    $scope.invalidContactsPagingOptions = {
       pageSizes: [10, 25, 50],
       pageSize: 10,
       currentPage: 1
    },
    $scope.invalidContactsFilterOptions = {
       filterText: "",
       useExternalFilter: true
    },
    $scope.invalidContactsGridOptions = {
       data: 'invalidContacts',
       enableCellSelection: true,
       enableRowSelection: false,
       enableCellEdit: true,
       enablePaging: true,
       showFooter: true,
       totalServerItems: 'invalidContactsTotalServerItems',
       pagingOptions: $scope.invalidContactsPagingOptions,
       filterOptions: $scope.invalidContactsFilterOptions,
       rowTemplate: '<div ng-style="{\'cursor\': row.cursor, \'z-index\': col.zIndex() }" ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell {{col.cellClass}}" ng-cell></div>',
       columnDefs: [
           { field: 'Id', displayName: 'Id', visible: false },
           { field: 'FirstName', displayName: 'First Name', enableCellEdit: false },
           { field: 'LastName', displayName: 'Last Name', enableCellEdit: false },
           { field: 'Email', displayName: 'Email' },
           { field: '', enableCellEdit: false, width: 50, displayName: ' ', cellTemplate: '<div class="text-center tip" data-toggle="tooltip" data-placement="left" title="Delete" ng-click="invalidContactsDelete(row)"><a href="#"><span class="glyphicon glyphicon-remove"></span></a></div>' }
       ]
    },
    $scope.invalidContactsGetPagedDataAsync = function (pageSize, page, searchText) {
       setTimeout(function () {
          $scope.executingAction = true;
          campaignManagerService.FindInvalidContacts($scope.participantId, pageSize, page, searchText)
          .then(function (response) {
             $scope.invalidContacts = response.data.invalidContactsPaged;
             $scope.invalidContactsTotalServerItems = response.data.total;
             if (!$scope.$$phase) {
                $scope.$apply();
             }
             $scope.executingAction = false;
          });
       }, 1);

    },
    $scope.invalidContactsDelete = function (row) {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.DeleteInvalidContact(row.entity.Id)
           .then(function (response) {
              if (response.data.success) {
                 $scope.success = true;
                 $scope.responseText = response.data.responseText;
                 $scope.invalidContacts.splice(row.rowIndex, 1);
                 $scope.invalidContactsTotalServerItems--;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
              $scope.executingAction = false;
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });

    },
    $scope.$watch('invalidContactsPagingOptions', function (newVal, oldVal) {
       if (newVal !== oldVal) {
          $scope.invalidContactsGetPagedDataAsync($scope.invalidContactsPagingOptions.pageSize, $scope.invalidContactsPagingOptions.currentPage, $scope.invalidContactsFilterOptions.filterText);
       }
    }, true),
    $scope.$watch('invalidContactsFilterOptions', function (newVal, oldVal) {
       if (newVal !== oldVal) {
          $scope.invalidContactsGetPagedDataAsync($scope.invalidContactsPagingOptions.pageSize, $scope.invalidContactsPagingOptions.currentPage, $scope.invalidContactsFilterOptions.filterText);
       }
    }, true),
   /** END INVALID CONTACTS GRID **/
   /** START PENDING EMAILS GRID **/
    $scope.pendingEmails = [],
    $scope.pendingEmailsPagingOptions = {
       pageSizes: [10, 25, 50],
       pageSize: 10,
       currentPage: 1
    },
    $scope.pendingEmailsFilterOptions = {
       filterText: "",
       useExternalFilter: true
    },
    $scope.pendingEmailsGridOptions = {
       data: 'pendingEmails',
       enableCellSelection: false,
       enableRowSelection: false,
       enableCellEdit: false,
       enablePaging: true,
       showFooter: true,
       totalServerItems: 'pendingEmailsTotalServerItems',
       pagingOptions: $scope.pendingEmailsPagingOptions,
       filterOptions: $scope.pendingEmailsFilterOptions,
       rowTemplate: '<div ng-style="{\'cursor\': row.cursor, \'z-index\': col.zIndex() }"  ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell {{col.cellClass}}" ng-cell></div>',
       columnDefs: [
           { field: 'Id', displayName: 'Id', visible: false },
           { field: 'Body', displayName: 'Body', visible: false },
           { field: 'To', displayName: 'Recipient' },
           { field: 'Subject', displayName: 'Subject' },
           { field: 'Created', displayName: 'Schedule', cellFilter: 'date' },
           { field: '', width: 50, displayName: ' ', cellTemplate: '<div class="text-center"><a href="#" ng-click="pendingEmailsView(row)" class="tip" data-toggle="tooltip" data-placement="left" title="View"><span class="glyphicon glyphicon-eye-open"></span></a> <a href="#" ng-click="pendingEmailsDelete(row)" class="tip" data-toggle="tooltip" data-placement="left" title="Delete"><span class="glyphicon glyphicon-remove"></span></a></div>' }
       ]
    },
    $scope.pendingEmailsGetPagedDataAsync = function (pageSize, page, searchText) {
       setTimeout(function () {
          $scope.executingAction = true;
          campaignManagerService.FindPendingEmails($scope.participantId, pageSize, page, searchText)
          .then(function (response) {
             $scope.pendingEmails = response.data.pendingEmailsPaged;
             $scope.pendingEmailsTotalServerItems = response.data.total;
             if (!$scope.$$phase) {
                $scope.$apply();
             }
             $scope.executingAction = false;
          });
       }, 1);

    },
    $scope.pendingEmailsDelete = function (row) {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       $scope.success = false;
       campaignManagerService.DeletePendingEmail(row.entity.Id)
           .then(function (response) {
              if (response.data.success) {
                 $scope.success = true;
                 $scope.responseText = response.data.responseText;
                 $scope.pendingEmails.splice(row.rowIndex, 1);
                 $scope.pendingEmailsTotalServerItems--;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
              $scope.executingAction = false;
           }, function (error) {
              $scope.responseText = "Oops! Something is wrong, please try again later.";
              $scope.resultFailed = true;
              $scope.executingAction = false;
              $log.error(error);
           });

    },
    $scope.pendingEmailsView = function (row) {
       //Comment by Javi, July 2014. The following javascript HTML lines are not supposed to happen, this is not the correct implementation but because of lack of time, it will be like this. If you read this and have time to fix it, feel free!
       $("#pendingEmailSubject").html(row.entity.Subject);
       $("#pendingEmailTo").html(row.entity.To);
       $("#pendingEmailMessage").html(row.entity.Body);
       $('#pendingEmailModal').modal('toggle');
    },
    $scope.$watch('pendingEmailsPagingOptions', function (newVal, oldVal) {
       if (newVal !== oldVal) {
          $scope.pendingEmailsGetPagedDataAsync($scope.pendingEmailsPagingOptions.pageSize, $scope.pendingEmailsPagingOptions.currentPage, $scope.pendingEmailsFilterOptions.filterText);
       }
    }, true),
    $scope.$watch('pendingEmailsFilterOptions', function (newVal, oldVal) {
       if (newVal !== oldVal) {
          $scope.pendingEmailsGetPagedDataAsync($scope.pendingEmailsPagingOptions.pageSize, $scope.pendingEmailsPagingOptions.currentPage, $scope.pendingEmailsFilterOptions.filterText);
       }
    }, true),
   /** END PENDING EMAILS GRID **/
   /** START SENT EMAILS GRID **/
    $scope.sentEmails = [],
    $scope.sentEmailsPagingOptions = {
       pageSizes: [10, 25, 50],
       pageSize: 10,
       currentPage: 1
    },
    $scope.sentEmailsFilterOptions = {
       filterText: "",
       useExternalFilter: true
    },
    $scope.sentEmailsGridOptions = {
       data: 'sentEmails',
       enableCellSelection: false,
       enableRowSelection: false,
       enableCellEdit: false,
       enablePaging: true,
       showFooter: true,
       totalServerItems: 'sentEmailsTotalServerItems',
       pagingOptions: $scope.sentEmailsPagingOptions,
       filterOptions: $scope.sentEmailsFilterOptions,
       rowTemplate: '<div ng-style="{\'cursor\': row.cursor, \'z-index\': col.zIndex() }"  ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell {{col.cellClass}}" ng-cell></div>',
       columnDefs: [
           { field: 'Id', displayName: 'Id', visible: false },
           { field: 'Body', displayName: 'Body', visible: false },
           { field: 'To', displayName: 'Recipient' },
           { field: 'Subject', displayName: 'Subject' },
           { field: 'Created', displayName: 'Created', cellFilter: 'date' },
           { field: '', width: 50, displayName: ' ', cellTemplate: '<div class="text-center"><a href="#" ng-click="sentEmailsView(row)" class="tip" data-toggle="tooltip" data-placement="left" title="View"><span class="glyphicon glyphicon-eye-open"></span></a></div>' }
       ]
    },
    $scope.sentEmailsGetPagedDataAsync = function (pageSize, page, searchText) {
       setTimeout(function () {
          $scope.executingAction = true;
          campaignManagerService.FindSentEmails($scope.participantId, pageSize, page, searchText)
          .then(function (response) {
             $scope.sentEmails = response.data.sentEmailsPaged;
             $scope.sentEmailsTotalServerItems = response.data.total;
             if (!$scope.$$phase) {
                $scope.$apply();
             }
             $scope.executingAction = false;
          });
       }, 1);

    },
    $scope.sentEmailsView = function (row) {
       //Comment by Javi, July 2014. The following javascript HTML lines are not supposed to happen, this is not the correct implementation but because of lack of time, it will be like this. If you read this and have time to fix it, feel free!
       $("#sentEmailSubject").html(row.entity.Subject);
       $("#sentEmailTo").html(row.entity.To);
       $("#sentEmailMessage").html(row.entity.Body);
       $('#sentEmailModal').modal('toggle');
    },
    $scope.$watch('sentEmailsPagingOptions', function (newVal, oldVal) {
       if (newVal !== oldVal) {
          $scope.sentEmailsGetPagedDataAsync($scope.sentEmailsPagingOptions.pageSize, $scope.sentEmailsPagingOptions.currentPage, $scope.sentEmailsFilterOptions.filterText);
       }
    }, true),
    $scope.$watch('sentEmailsFilterOptions', function (newVal, oldVal) {
       if (newVal !== oldVal) {
          $scope.sentEmailsGetPagedDataAsync($scope.sentEmailsPagingOptions.pageSize, $scope.sentEmailsPagingOptions.currentPage, $scope.sentEmailsFilterOptions.filterText);
       }
    }, true),
   /** END SENT EMAILS GRID **/
    $scope.bannerLink = function (value, participantId) {
       $scope.registerView.link = "Generating your link....";
       campaignManagerService.GetBannerLink(participantId, value)
           .then(function (response) {
              if (response.data.success) {
                 $scope.registerView.link = response.data.bannerName;
              } else {
                 // $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
           });
    },
    $scope.redeemPrize = function () {
       $scope.executingAction = true;
       $scope.resultFailed = false;
       campaignManagerService.RedeemPrize($scope.participantId)
           .then(function (response) {
              if (response.data.success) {
                 $window.location.href = response.data.url;
              } else {
                 $scope.responseText = response.data.responseText;
                 $scope.resultFailed = true;
              }
              $scope.executingAction = false;
           });
    },
    $scope.shareOnFacebook = function (eventId) {
       $window.FB.ui({
          method: 'share_open_graph',
          action_type: 'og.likes',
          action_properties: JSON.stringify({
             object: 'http://www.efundraising.com/Group/Index?eventId=' + eventId,
          })
       },
           function (response) {

           });
       $scope.sharedOnFacebook = true;
       $window.ga('send', 'social', 'Facebook', 'Share', '/Group/Index');
    },
    $scope.shareOnTwitter = function (eventId) {
       $window.open("http://twitter.com/share?url=http://www.efundraising.com/Group/Index?eventId=" + eventId, "_blank");
       $scope.sharedOnTwitter = true;
       $window.ga('send', 'social', 'Twitter', 'Share', '/Group/Index');
    }
}]).controller('imageFileUploadController', ['$scope', '$window', '$upload', "Upload", function ($scope, $window, $upload, Upload) {
   $scope.personalizePageName;
   $scope.onFileSelect = function ($files) {
      if ($scope.personalizePageName == 'Register') {
         if (angular.element("#registration").scope().registerView.UserType == 2) {
            if (angular.element("#registration").scope().registerView.PersonalizationImages.length == 1 &&
                angular.element("#registration").scope().registerView.PersonalizationImages[0].ImageURL !=
                angular.element("#registration").scope().registerView.DefaultImage) {
               alert("Sorry you can only add 1 image");
               return false;
            }
         }
         else {
            if (angular.element("#registration").scope().registerView.PersonalizationImages.length == 12) {
               alert("Sorry you can only add 12 images");
               return false;
            }
         }
      }
      else if ($scope.personalizePageName == 'Page') {
         if (angular.element("#personalizePage").scope().registerView.UserType == 2) {
            if (angular.element("#personalizePage").scope().registerView.PersonalizationImages.length == 1 &&
                angular.element("#personalizePage").scope().registerView.PersonalizationImages[0].ImageURL !=
                angular.element("#personalizePage").scope().registerView.DefaultImage) {
               alert("Sorry you can only add 1 image");
               return false;
            }
         }
         else {
            if (angular.element("#personalizePage").scope().registerView.PersonalizationImages.length == 12) {
               alert("Sorry you can only add 12 images");
               return false;
            }
         }
      }
      $scope.fileUploadAction = true;
      $scope.fileUploadFailed = false;
      for (var i = 0; i < $files.length; i++) {
         var file = $files[i];
         $scope.upload = $upload.upload({
            url: '/CampaignManager/ImageFileUpload',
            method: 'POST',
            file: file,
            fileFormDataName: 'ImageFile',
         }).progress(function (evt) {
            $scope.percentUploaded = parseInt(100.0 * evt.loaded / evt.total);
         }).success(function (data, status, headers, config) {
            if (data.success) {
               if ($scope.personalizePageName == 'Register') {
                  $window.ga('send', 'event', 'Campaign Manager', 'Upload Images', 'Registration Page');
                  angular.element("#registration").scope().addToImageList(0, 0, false, data.savedImagePath, false, true);
               } else if ($scope.personalizePageName == 'Page') {
                  $window.ga('send', 'event', 'Campaign Manager', 'Upload Images', 'Personalization Page');
                  angular.element("#personalizePage").scope().addToImageList(0, 0, false, data.savedImagePath, false, true);
               }
            } else {
               $scope.responseText = data.responseText;
               $scope.fileUploadFailed = true;
            }
            $scope.fileUploadAction = false;
         });
      }
   },
   $scope.abortUpload = function () {
      $scope.upload.abort();
      $scope.fileUploadAction = false;
   };
}]).controller("donationController", ["$scope", "$window", "donationService", function ($scope, $window, donationService) {
   $scope.donationView = {};
   $scope.executingAction = false;
   $scope.resultFailed = false;
   $scope.CanadaSubdivisionCodes =
       [
           { id: "CA-AB", name: "Alberta" },
           { id: "CA-BC", name: "British Columbia" },
           { id: "CA-MB", name: "Manitoba" },
           { id: "CA-NB", name: "New Brunswick" },
           { id: "CA-NL", name: "Newfoundland and Labrador" },
           { id: "CA-NS", name: "Nova Scotia" },
           { id: "CA-NT", name: "Northwest Territories" },
           { id: "CA-NU", name: "Nunavut" },
           { id: "CA-ON", name: "Ontario" },
           { id: "CA-PE", name: "Prince Edward Island" },
           { id: "CA-QC", name: "Quebec" },
           { id: "CA-SK", name: "Saskatchewan" },
           { id: "CA-YT", name: "Yukon Territory" },
       ];
   $scope.USASubdivisionCodes =
       [
           { id: "US-AA", name: "Armed Forces Americas (except Canada)" },
           { id: "US-AE", name: "Armed Forces (Africa, Canada, Europe, Middle East)" },
           { id: "US-AK", name: "Alaska" },
           { id: "US-AL", name: "Alabama" },
           { id: "US-AP", name: "Armed Forces Pacific" },
           { id: "US-AR", name: "Arkansas" },
           { id: "US-AZ", name: "Arizona" },
           { id: "US-CA", name: "California" },
           { id: "US-CO", name: "Colorado" },
           { id: "US-CT", name: "Connecticut" },
           { id: "US-DC", name: "District of Columbia" },
           { id: "US-DE", name: "Delaware" },
           { id: "US-FL", name: "Florida" },
           { id: "US-GA", name: "Georgia" },
           { id: "US-HI", name: "Hawaii" },
           { id: "US-IA", name: "Iowa" },
           { id: "US-ID", name: "Idaho" },
           { id: "US-IL", name: "Illinois" },
           { id: "US-IN", name: "Indiana" },
           { id: "US-KS", name: "Kansas" },
           { id: "US-KY", name: "Kentucky" },
           { id: "US-LA", name: "Louisiana" },
           { id: "US-MA", name: "Massachusetts" },
           { id: "US-MD", name: "Maryland" },
           { id: "US-ME", name: "Maine" },
           { id: "US-MI", name: "Michigan" },
           { id: "US-MN", name: "Minnesota" },
           { id: "US-MO", name: "Missouri" },
           { id: "US-MS", name: "Mississippi" },
           { id: "US-MT", name: "Montana" },
           { id: "US-NC", name: "North Carolina" },
           { id: "US-ND", name: "North Dakota" },
           { id: "US-NE", name: "Nebraska" },
           { id: "US-NH", name: "New Hampshire" },
           { id: "US-NJ", name: "New Jersey" },
           { id: "US-NM", name: "New Mexico" },
           { id: "US-NV", name: "Nevada" },
           { id: "US-NY", name: "New York" },
           { id: "US-OH", name: "Ohio" },
           { id: "US-OK", name: "Oklahoma" },
           { id: "US-OR", name: "Oregon" },
           { id: "US-PA", name: "Pennsylvania" },
           { id: "US-PR", name: "Puerto Rico" },
           { id: "US-RI", name: "Rhode Island" },
           { id: "US-SC", name: "South Carolina" },
           { id: "US-SD", name: "South Dakota" },
           { id: "US-TN", name: "Tennessee" },
           { id: "US-TX", name: "Texas" },
           { id: "US-UT", name: "Utah" },
           { id: "US-VA", name: "Virginia" },
           { id: "US-VI", name: "Virgin Islands" },
           { id: "US-VT", name: "Vermont" },
           { id: "US-WA", name: "Washington" },
           { id: "US-WI", name: "Wisconsin" },
           { id: "US-WV", name: "West Virginia" },
           { id: "US-WY", name: "Wyoming" }
       ];
   $scope.YearChoices = [];
   $scope.LoadYearChoices = function () {
      var currentDate = new Date();
      var currentYear = currentDate.getFullYear();
      for (var i = currentYear; i < (currentYear + 10) ; i++) {
         $scope.YearChoices.push(i);
      }
   },
   $scope.stateSelectChange = function () {
      if ($scope.donationView.Country === "US")
         $scope.donationView.State = "US-AA";
      else
         $scope.donationView.State = "CA-AB";
   },
   $scope.donate = function () {
      $scope.executingAction = true;
      $scope.resultFailed = false;
      donationService.Donate($scope.donationView)
                     .then(function (response) {
                        if (response.data.success) {
                           //$window.ga('send', 'event', 'User', 'Register', 'Forms');
                           $window.location.href = response.data.url;
                        } else {
                           $scope.responseText = response.data.responseText;
                           $scope.resultFailed = true;
                           $scope.executingAction = false;
                        }
                     });
   }
}]).controller("CsvFileUploadController", ["$scope", "$upload", function ($scope, $upload) {
   $scope.onFileSelect = function ($files) {
      $scope.fileUploadAction = true;
      $scope.fileUploadFailed = false;
      $scope.responseText = "";
      for (var i = 0; i < $files.length; i++) {
         var file = $files[i];
         $scope.upload = $upload.upload({
            url: '/CampaignManager/CsvFileUpload',
            method: 'POST',
            file: file,
            fileFormDataName: 'CsvFile',
         }).progress(function (evt) {
            $scope.percentUploaded = parseInt(100.0 * evt.loaded / evt.total);
         }).success(function (data, status, headers, config) {
            if (data.success) {
               var recipients = "";
               $.each(data.contacts, function (k, v) {
                  recipients += ", " + v.FirstName + "|" + v.LastName + "|" + v.EmailAddress;
               });
               if (recipients.substring(0, 2) === ", ")
                  recipients = recipients.substring(2, recipients.length);
               //recipients = recipients.replace(/\||/g, " ");
               if ($("#recipientsTextArea").val() == "")
                  $("#recipientsTextArea").val(recipients);
               else
                  $("#recipientsTextArea").val($("#recipientsTextArea").val() + ", " + recipients);
               $scope.manageImportedContacts(recipients, "CSV File");
               $('#csvUploadModal').modal('hide');
            } else {
               $scope.responseText = data.responseText;
               $scope.fileUploadFailed = true;
            }
            $scope.fileUploadAction = false;
         });
      }
   },
   $scope.abortUpload = function () {
      $scope.upload.abort();
      $scope.fileUploadAction = false;
   };
}]);

var ContactsModalInstanceCtrl = function ($scope, $modalInstance, items, filterOptions) {
   $scope.items = items;
   $scope.filterOptions = filterOptions;
   $scope.totalServerItems = items.length;
   $scope.pagingOptions = {
      pageSizes: [10],
      pageSize: 10,
      currentPage: 1
   };
   $scope.setPagingData = function (data, page, pageSize, searchIndex) {
      var pagedData = null;
      if (searchIndex != "")
         pagedData = data.slice(searchIndex, searchIndex + 1);
      else
         pagedData = data.slice((page - 1) * pageSize, page * pageSize);
      $scope.myData = pagedData;
      $scope.totalServerItems = data.length;
      if (!$scope.$$phase) {
         $scope.$apply();
      }
   };
   $scope.getPagedDataAsync = function (pageSize, page, searchIndex) {
      setTimeout(function () {
         $scope.setPagingData($scope.items, page, pageSize, searchIndex);
      }, 100);
   };
   $scope.delete = function (row) {
      var itemsIndex = (($scope.pagingOptions.currentPage - 1) * 10) + row.rowIndex;
      $scope.items.splice(itemsIndex, 1);
      $scope.myData.splice(row.rowIndex, 1);
   };
   $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterIndex);

   $scope.$watch('pagingOptions', function (newVal, oldVal) {
      if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
         $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterIndex);
      }
   }, true);
   $scope.$watch('filterOptions', function (newVal, oldVal) {
      if (newVal !== oldVal) {
         $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterIndex);
      }
   }, true);
   $scope.gridOptions = {
      data: 'myData',
      enableCellSelection: true,
      enableRowSelection: false,
      enableCellEdit: true,
      enablePaging: true,
      showFooter: true,
      totalServerItems: 'totalServerItems',
      pagingOptions: $scope.pagingOptions,
      filterOptions: $scope.filterOptions,
      columnDefs: [
          { field: 'FirstName', displayName: 'First Name' },
          { field: 'LastName', displayName: 'Last Name' },
          { field: 'Email', displayName: 'Email' },
          { field: '', enableCellEdit: false, width: 100, displayName: 'Delete', cellTemplate: '<span class="glyphicon glyphicon-remove" ng-click="delete(row)"></span>' }]
   },
   $scope.ok = function () {
      $modalInstance.close($scope.items);
   };
   $scope.cancel = function () {
      $modalInstance.dismiss('cancel');
   };
};

var CampaignReportDetailModalInstanceCtrl = function ($scope, $uibModalInstance, items, memberName, totalAmount, totalProfit) {
   $scope.items = items;
   $scope.memberName = memberName;
   $scope.totalAmount = totalAmount;
   $scope.totalProfit = totalProfit;
   $scope.cancel = function () {
      $uibModalInstance.dismiss('cancel');
   };
};