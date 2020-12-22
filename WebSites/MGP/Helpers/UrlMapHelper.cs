using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Web.MGP.Helpers.Routes.Filters;
using GA.BDC.Data.MGP.esubs_global_v2.Models;

namespace GA.BDC.Web.MGP.Helpers
{
    public class UrlMapHelper
    {
        #region Public Static Methods

        public static string MapPath(HttpRequestBase currentRequest, page_route_mapper prm)
        {
            if (!currentRequest.QueryString.HasKeys())
            {
                if (prm.append_qs.IsNotEmpty())
                    return string.Concat(prm.destination_file_path, "?", prm.append_qs);
                else
                    return prm.destination_file_path;
            }

            int eventId = int.MinValue, participantId = int.MinValue;
            var query = string.Empty;
            var keys = currentRequest.QueryString.AllKeys.Cast<string>().ToList();

            if (keys.Contains(Constants.QUERY_PARAMETER_EVENT_ID_2))
            {
                eventId = int.Parse(currentRequest.QueryString[keys.FindIndex(k => k == Constants.QUERY_PARAMETER_EVENT_ID_2)]);
                participantId = int.MinValue;
            }

            if (keys.Contains(Constants.QUERY_PARAMETER_TOUCH_ID))
            {
                var touchId = EnvironmentFilter.DecryptInteger(currentRequest.QueryString[keys.FindIndex(k => k == Constants.QUERY_PARAMETER_TOUCH_ID)]);

                eventId = GetEventIdByTouchId(touchId);
                participantId = GetEventParticipationIdByTouchId(touchId);
            }
            else if (keys.Contains(Constants.QUERY_PARAMETER_TOUCH_ID_2))
            {
                var touchId = EnvironmentFilter.DecryptInteger(currentRequest.QueryString[keys.FindIndex(k => k == Constants.QUERY_PARAMETER_TOUCH_ID_2)]);

                eventId = GetEventIdByTouchId(touchId);
                participantId = GetEventParticipationIdByTouchId(touchId);
            }

            if (keys.Contains(Constants.QUERY_PARAMETER_EVENT_PARTICIPATION_ID))
            {
                participantId = EnvironmentFilter.DecryptInteger(currentRequest.QueryString[keys.FindIndex(k => k == Constants.QUERY_PARAMETER_EVENT_PARTICIPATION_ID)]);
                eventId = GetEventIdByEventParticipationId(participantId);
            }

            if (keys.Contains(Constants.QUERY_PARAMETER_PARTICIPANT_HOME_ID))
            {
                participantId = EnvironmentFilter.DecryptInteger(currentRequest.QueryString[keys.FindIndex(k => k == Constants.QUERY_PARAMETER_PARTICIPANT_HOME_ID)]);
                eventId = GetEventIdByEventParticipationId(participantId);
            }
            
            if (prm.participant_id_required)
            {
                if (participantId < 0)
                    throw new Exception("UrlMapHelper: ParticipantId is empty.");
            }

            if (participantId > 0  && 
                prm.enforce_parent_participant_id)
            {
                var userType = GetUserTypeByEventParticipationId(participantId);
                if (userType != UserType.SPONSOR && userType != UserType.PARTICIPANT)
                    participantId = GetParentEventParticipationId(participantId);
            }

            if (prm.is_public)
            {
                query = string.Format("?{0}{1}{2}",
                                      eventId > 0
                                       ? string.Concat("eventId=", eventId.ToString())
                                       : string.Empty,
                                      participantId > 0
                                       ? string.Concat("&participantId=", participantId.ToString())
                                       : string.Empty,
                                       string.Concat(eventId < 0 && participantId < 0 
                                                      ? string.Empty 
                                                      : "&",
                                                     currentRequest.Url.Query.TrimStart("?")));
            }
            else
            {
                query = string.Format("?participantId={0}&{1}",
                                      participantId.ToString(),
                                      currentRequest.Url.Query.TrimStart("?"));
            }

            // Validate query (Was getting errors with bad stuff being appended to the query)
            query = query.Replace("~/CMWizard2.aspx", "CMWizard2.aspx");
            if (query.LastIndexOf("/") > 0)
            {
                query = query.Substring(0, query.LastIndexOf("/"));
            }

            if (prm.append_qs.IsNotEmpty())
                query = string.Concat(query, "&", prm.append_qs);            

            return string.Concat(prm.destination_file_path, query);
        }

        #endregion

        #region Private Static Methods

        private static int GetEventIdByTouchId(int touchId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @event_id = (from t in dataProvider.touches
                                 from ep in dataProvider.event_participation
                                where t.touch_id == touchId
                                   && t.event_participation_id == ep.event_participation_id
                               select ep.event_id).SingleOrDefault();
                return @event_id;
            }
        }
        private static int GetEventIdByEventParticipationId(int epId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @event_id = (from ep in dataProvider.event_participation
                                where ep.event_participation_id == epId
                               select ep.event_id).SingleOrDefault();
                return @event_id;
            }
        }
        private static int GetEventParticipationIdByTouchId(int touchId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @epId = (from t in dataProvider.touches
                             from ep in dataProvider.event_participation
                            where t.touch_id == touchId
                               && t.event_participation_id == ep.event_participation_id
                           select ep.event_participation_id).SingleOrDefault();
                return @epId;
            }
        }
        private static UserType GetUserTypeByEventParticipationId(int epId)
        {
            using (var dataProvider = new DataProvider())
            {
                var memberTypeId = (from ep in dataProvider.event_participation
                                    from mh in dataProvider.member_hierarchy
                                    from cc in dataProvider.creation_channel
                                   where mh.member_hierarchy_id == ep.member_hierarchy_id
                                      && mh.creation_channel_id == cc.creation_channel_id
                                      && ep.event_participation_id == epId
                                  select cc.member_type_id).Single();
                return (UserType)memberTypeId;
            }
        }
        private static int GetParentEventParticipationId(int epId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @pId = (from mh in dataProvider.member_hierarchy
                            from ep in dataProvider.event_participation
                            from mhp in dataProvider.member_hierarchy
                            from epp in dataProvider.event_participation
                           where mh.member_hierarchy_id == ep.member_hierarchy_id
                              && ep.event_participation_id == epId
                              && mh.parent_member_hierarchy_id == mhp.member_hierarchy_id
                              && mhp.member_hierarchy_id == epp.member_hierarchy_id
                          select epp.event_participation_id).SingleOrDefault();
                return @pId;
            }
        }

        #endregion

    }
}