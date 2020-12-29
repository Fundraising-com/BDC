using System;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
    [Serializable]
    public class ParticipationCenterStats : StatsBase
    {
        #region Private Fields
        private int _numGroupMember;
        private int _numGroupMemberReceivedEmail;
        private int _numGroupMemberInvitedFreinds;
        private int _numGroupMemberBoughtSubs;
        #endregion

        #region Constructor
        public ParticipationCenterStats() : this(0, 0, 0, 0) { }
        public ParticipationCenterStats(int nb_gm, int nb_gm_email, int gm_invited, int gm_bought) 
        {
            _numGroupMember = nb_gm;
            _numGroupMemberReceivedEmail = nb_gm_email;
            _numGroupMemberInvitedFreinds = gm_invited;
            _numGroupMemberBoughtSubs = gm_bought;
        }
        #endregion           
                
        #region Public/Private Properties
        public int NumberOfGroupMembers
        {
            get { return _numGroupMember; }
            set { _numGroupMember = value; }
        }
        public int NumberOfGroupMembersReceivedEmail
        {
            get { return _numGroupMemberReceivedEmail; }
            set { _numGroupMemberReceivedEmail = value; }
        }
        public int NumberOfGroupMembersInvitedFriends
        {
            get { return _numGroupMemberInvitedFreinds; }
            set { _numGroupMemberInvitedFreinds = value; }
        }
        public int NumberOfGroupMembersBoughtSubs
        {
            get { return _numGroupMemberBoughtSubs; }
            set { _numGroupMemberBoughtSubs = value; }
        }
        public string DisplayNumberOfGroupMembersText
        {
            get { return "<b>" + NumberOfGroupMembers.ToString() + "</b> group members"; }
        }
        public string DisplayNumberOfGroupMembersReceivedEmailText
        {
            get { return "<b>" + NumberOfGroupMembersReceivedEmail.ToString() + "</b> group members received your email"; }
        }
        public string DisplayNumberOfGroupMembersInvitedFriendsText
        {
            get { return "<b>" + NumberOfGroupMembersInvitedFriends.ToString() + "</b> group members invited friends & family"; }
        }
        public string DisplayNumberOfGroupMembersBoughtSubsText
        {
            get { return "<b>" + NumberOfGroupMembersBoughtSubs.ToString() + "</b> group members bought item(s)"; }
        }
        #endregion

        #region Public/Private Functions
        public static List<StatsBase> Load(eSubsGlobalEnvironment env)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetCampaignManagerStatsByReportType(CampaignManagerReport.ReportName.PARTICIPATION_CENTER, env.Event.EventID);
        }
        #endregion
    }
}
