angular.module("mgp.services", []).
    factory("homeService", [
    "$http", function ($http) {
        return {
            FindAGroup: function (view) {
                return $http.post(
                    "/Home/FindAGroup", { model: view },
                {
                });
            },
            Unsubscribe: function (view) {
                return $http.post(
                    "/Home/Unsubscribe", { model: view },
                {
                });
            }
        }
    }]).factory("groupService", [
    "$http", function ($http) {
        return {
            FindAParticipant: function (eventId, view) {
                return $http.post(
                    "/Group/FindAParticipant", {eventId : eventId, model: view },
                {
                });
            },
            RedirectToStore: function (eventId, participantId, entityId, storefrontCategoryId) {
                return $http.post(
                    "/Group/RedirectToStore", { eventId: eventId, participantId: participantId, entityId: entityId, storefrontCategoryId: storefrontCategoryId });
            },
        }
    }]).factory("campaignManagerService", [
    "$http", function ($http) {
        return {
            CheckEventRedirectAvailability: function (view) {
                return $http.post("/CampaignManager/CheckEventRedirectAvailability", { model: view });
            },
            Register: function (participantId, view) {
                return $http.post("/CampaignManager/Register", {participantId : participantId, model: view });
            },
            Step0: function (register) {
                return $http.post("/registration/step-0", { register: register });
            },
            Step1: function (participantId, view) {
               return $http.post("/registration/step-1", { participantId: participantId, goal: view.goal, title: view.title, postalCode: view.postalCode });
            },
            Step2: function (participantId, imageUrl) {
					 return $http.post("/registration/step-2", { participantId: participantId, imageUrl: imageUrl });
            },
            Step3: function (participantId, body) {
					 return $http.post("/registration/step-3", { participantId: participantId, body: body });
            },
            Step4: function (participantId, newLink) {
					 return $http.post("/registration/step-4", { participantId: participantId, newLink: newLink });
            },
            Step5: function (participantId, recipients, reminderFrecuency) {
					 return $http.post("/registration/step-5", { participantId: participantId, recipients: recipients, reminderFrecuency: reminderFrecuency });
            },
            KickOff: function (participantId, view) {
                return $http.post("/CampaignManager/KickOff", { participantId: participantId, model: view });
            },
            GetPersonalizationImages: function (view) {
                return $http.post("/CampaignManager/GetPersonalizationImages", { model: view });
            },
            GetGalleryImages: function (view) {
                return $http.post("/CampaignManager/GetGalleryImages", { model: view });
            },
            DeleteImageFromServer: function (filePath) {
                return $http.post("/CampaignManager/DeleteImageFromServer", { filePath: filePath });
            },
            DeletePersonalizationImage: function (participantId, id) {
               return $http.post("/CampaignManager/DeletePersonalizationImage", { participantId: participantId, id: id });
            },
            SetAlbumCover: function (id) {
                return $http.post("/CampaignManager/SetAlbumCover", { id: id });
            },
            UpdateMyInformation: function (participantId, view) {
                return $http.post("/CampaignManager/Information", { participantId: participantId, model: view });
            },
            RedirectToStore: function (eventId, participantId, entityId, storefrontCategoryId) {
                return $http.post(
                    "/Group/RedirectToStore", { eventId: eventId, participantId: participantId, entityId: entityId, storefrontCategoryId: storefrontCategoryId });
            },
            Create: function (view) {
                return $http.post("/CampaignManager/Create", { model: view });
            },
            End: function (participantId) {
                return $http.post("/CampaignManager/End", { participantId: participantId });
            },
            ChangeGoal: function (participantId, goal) {
                return $http.post("/CampaignManager/ChangeGoal", { participantId: participantId, goal: goal });
            },
            Relaunch: function (participantId) {
                return $http.post("/CampaignManager/Clone", { participantId: participantId });
            },
            SendNewMessage: function (participantId, view) {
                return $http.post("/CampaignManager/SendNewMessage", { participantId: participantId, model: view });
            },
            GetBannerLink: function (participantId, bannerName) {
                return $http.post("/CampaignManager/GetBannerLink", { participantId: participantId, bannerType: bannerName });
            },
            UpdateContact: function (participantId, model) {
                return $http.post("/CampaignManager/UpdateContact", { participantId : participantId, model: model });
            },
            DeleteContact: function (memberId) {
                return $http.post("/CampaignManager/DeleteContact", { memberId: memberId });
            },
            FindContacts: function (participantId, pageSize, page, searchText) {
                return $http.post("/CampaignManager/FindContacts", { participantId: participantId, pageSize: pageSize, page: page, searchText: searchText });
            },
            FindPendingEmails: function (participantId, pageSize, page, searchText) {
                return $http.post("/CampaignManager/FindPendingEmails", { participantId: participantId, pageSize: pageSize, page: page, searchText: searchText });
            },
            DeletePendingEmail: function (touchId) {
                return $http.post("/CampaignManager/DeletePendingEmail", { touchId: touchId });
            },
            UpdateInvalidContact: function (model) {
                return $http.post("/CampaignManager/UpdateInvalidContact", { model: model });
            },
            DeleteInvalidContact: function (memberId) {
                return $http.post("/CampaignManager/DeleteInvalidContact", { memberId: memberId });
            },
            FindInvalidContacts: function (participantId, pageSize, page, searchText) {
                return $http.post("/CampaignManager/FindInvalidContacts", { participantId: participantId, pageSize: pageSize, page: page, searchText: searchText });
            },
            FindSentEmails: function (participantId, pageSize, page, searchText) {
                return $http.post("/CampaignManager/FindSentEmails", { participantId: participantId, pageSize: pageSize, page: page, searchText: searchText });
            },
            SaveTwitterWidget: function (participantId, widgetId) {
                return $http.post("/CampaignManager/Twitter", { participantId: participantId, widgetId: widgetId });
            },
            SaveFacebookPost: function (participantId, code) {
                return $http.post("/CampaignManager/Facebook", { participantId: participantId, code: code });
            },
            CampaignReport: function (participantId, searchType, memberName, from, to, isDetail) {
                return $http.post("/CampaignManager/CampaignReport", { participantId: participantId, searchType: searchType, memberName: memberName, from: from, to: to, isDetail: isDetail });
            },
            CampaignDonationReport: function (participantId, isDetail) {
                return $http.post("/CampaignManager/CampaignDonationReport", { participantId: participantId, isDetail: isDetail });
            },
            GroupReport: function (participantId, searchType, from, to) {
                return $http.post("/CampaignManager/MyGroupReport", { participantId: participantId, searchType: searchType, from: from, to: to });
            },
            CampaignReportParticipant: function (participantId, searchType, from, to) {
                return $http.post("/CampaignManager/MyCampaignReport", { participantId: participantId, searchType: searchType, from: from, to: to });
            },
            CampaignDonationReportParticipant: function (participantId, searchType, from, to) {
                return $http.post("/CampaignManager/MyCampaignDonationReport", { participantId: participantId });
            },
            DonationGroupReport: function (participantId, searchType, from, to) {
                return $http.post("/CampaignManager/MyGroupDonationReport", { participantId: participantId, searchType: searchType, from: from, to: to });
            },
            RedeemPrize: function (participantId) {
                return $http.post("/CampaignManager/RedeemPrize", { participantId: participantId });
            }
        }
    }]).factory("loginService", [
    "$http", function ($http) {
        return {
            Login: function (view) {
                return $http.post("/Security/Login", { model: view });
            },
            RecoverPassword: function (email) {
                return $http.post("/Security/RecoverPassword", { email: email });
            },
            RegisterExternal: function (view, providerName, providerUserId) {
                return $http.post("/Security/Register", { model: view, providerName: providerName, providerUserId: providerUserId });
            },
            Register: function (view) {
                return $http.post("/Security/Register", { model: view });
            },
            ParticipantRegister: function (eventId, view) {
                return $http.post("/Security/ParticipantRegister", { eventId: eventId, model: view });
            },
            ParticipantJoin: function (eventId, view) {
                return $http.post("/Security/ParticipantJoin", { eventId: eventId, model: view });
            },
            RedirectToStore: function (eventId, participantId, entityId, storefrontCategoryId) {
                return $http.post(
                    "/Group/RedirectToStore", { eventId: eventId, participantId: participantId, entityId: entityId, storefrontCategoryId: storefrontCategoryId });
            },
        }
    }]).factory("donationService", [
    "$http", function ($http) {
        return {
            Donate: function (view) {
                return $http.post("/Donation/Donate", { model: view });
            },
        }
    }]);