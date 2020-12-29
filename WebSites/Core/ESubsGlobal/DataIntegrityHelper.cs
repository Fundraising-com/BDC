using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.BDC.Core.ESubsGlobal.DataAccess;
using GA.BDC.Core.ESubsGlobal.Users;

namespace GA.BDC.Core.ESubsGlobal
{    
    public class DataIntegrityHelper
    {
        public enum ErrorCode
        {
            OK, NOUSER, NOGROUP, NOEVENT, NOPARTNER, NOEVENTPARTICIPATION
        }

        #region Missing Event Participation

        public static ErrorCode CreateMissingEventParticipationAndPersonalization(Int32 member_hierarchy_id)
        {
            //Get eSubsGlobalUser object
            eSubsGlobalUser user = eSubsGlobalUser.LoadByHierarchyID(member_hierarchy_id);
            if (user == null)
                return ErrorCode.NOUSER;

            // Get Group
            Group g = Group.LoadGroupByMemberHierarchyID(member_hierarchy_id);
            if (g == null)
                return ErrorCode.NOGROUP;

            // Get Event
            Event ev = Event.GetLatestActiveEventByGroupID(g.GroupID);
            if (ev == null)
                ev = Event.GetEventByGroupID(g.GroupID);
            if (ev == null)
                return ErrorCode.NOEVENT;

            return CreateMissingEventParticipationAndPersonalization(member_hierarchy_id, ev);
        }

        public static ErrorCode CreateMissingEventParticipationAndPersonalization(Int32 member_hierarchy_id, Int32 event_id)
        {
            Event ev = Event.GetEventByEventID(event_id);
            if (ev == null)
                return ErrorCode.NOEVENT;

            return CreateMissingEventParticipationAndPersonalization(member_hierarchy_id, ev);
        }

        public static ErrorCode CreateMissingEventParticipationAndPersonalization(Int32 member_hierarchy_id, Event ev)
        {
            EventParticipation evp = null;
            Personalization pers = null;
            PersonalizationImage persImg = null;

            //Get eSubsGlobalUser object
            eSubsGlobalUser user = eSubsGlobalUser.LoadByHierarchyID(member_hierarchy_id);
            if (user == null)
                return ErrorCode.NOUSER;            

            Partner p = Partner.LoadByID(user.PartnerID, user.Culture);
            if (p == null)
                return ErrorCode.NOPARTNER;

            // Create EventParticipation
            if (user.IsSponsor)
                evp = new EventParticipation(ev, user, ParticipationChannel.SponsorCreated);
            else if (user.IsParticipant)
                evp = new EventParticipation(ev, user, ParticipationChannel.InvitedBySponsor);
            else
                evp = new EventParticipation(ev, user, ParticipationChannel.FindMyGroup);

            evp.Salutation = user.CompleteName;
            if (user.CoppaMonth != int.MinValue && user.CoppaYear != int.MinValue)
            {
                evp.CoppaMonth = user.CoppaMonth;
                evp.CoppaYear = user.CoppaYear;
            }
            EventParticipationStatus code = evp.InsertIntoDatabase();
            if (code != EventParticipationStatus.OK)
                return ErrorCode.NOEVENTPARTICIPATION;

            // Create default personalization
            if (user.IsSponsor)
                pers = Personalization.CreateDefaultSponsorPersonalization(evp, p, user.Culture, ev.Name, user.CompleteName);
            else if (user.IsParticipant)
                pers = Personalization.CreateDefaultParticipantPersonalization(evp, p, user.Culture, ev.Name, user.CompleteName);

            if (pers != null)
            {
                pers.Body = System.Web.HttpUtility.HtmlDecode(pers.Body);
                if (string.IsNullOrEmpty(pers.Redirect))
                    pers.Redirect = ev.Redirect;
                pers.InsertIntoDatabase();

                // Create default personalization image
                persImg = new PersonalizationImage();
                persImg.ImageUrl = pers.ImageUrl;
                persImg.Deleted = (byte)0;
                persImg.ImageApprovalStatusId = 3;
                persImg.IsCoverAlbum = (byte)1;
                persImg.PersonalizationID = pers.PersonalizationId;
                persImg.InsertIntoDatabase();
            }           

            return ErrorCode.OK;
        }

        #endregion
    }
}
