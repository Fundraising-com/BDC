using System;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
    [Serializable]
    public class SupporterCenterStats : StatsBase
    {
        #region Private Fields
        private int _numSupporterReceivedEmail;
        private int _numSupporterBoughtSubs;
        #endregion

        #region Constructor
        public SupporterCenterStats() : this(0, 0) { }
        public SupporterCenterStats(int nb_supp_email, int nb_supp_bought) 
        {
            _numSupporterReceivedEmail = nb_supp_email;
            _numSupporterBoughtSubs = nb_supp_bought;
        }
        #endregion           
                
        #region Public/Private Properties
        public int NumberOfSupportersReceivedEmail
        {
            get { return _numSupporterReceivedEmail; }
            set { _numSupporterReceivedEmail = value; }
        }
        public int NumberOfSupportersBoughtSubs
        {
            get { return _numSupporterBoughtSubs; }
            set { _numSupporterBoughtSubs = value; }
        }
        public string DisplayNumberOfSupportersReceivedEmailText
        {
            get { return "<b>" + NumberOfSupportersReceivedEmail.ToString() + "</b> supporters received your email"; }
        }
        public string DisplayNumberOfSupportersBoughtSubsText
        {
            get { return "<b>" + NumberOfSupportersBoughtSubs.ToString() + "</b> supporters bought item(s)"; }
        }
        #endregion

        #region Public/Private Functions
        public static List<StatsBase> Load(eSubsGlobalEnvironment env)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetParticipantZoneStatsByReportType(ParticipantZone_ReportName.SUPPORTER_CENTER, env.Event.EventID, env.EventParticipation.EventParticipationID);
        }
        #endregion
    }
}
