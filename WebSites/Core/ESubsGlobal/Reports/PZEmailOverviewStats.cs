using System;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
    [Serializable]
    public class PZEmailOverviewStats : StatsBase
    {
        #region Private Fields
        private DateTime _launchDate;
        private string _description;
        private int _emailsSent;
        #endregion

        #region Constructor
        public PZEmailOverviewStats() : this(DateTime.MinValue, string.Empty, 0) { }
        public PZEmailOverviewStats(DateTime launchDate, string description, int emailsSent) 
        {
            _launchDate = launchDate;
            _description = description;
            _emailsSent = emailsSent;
        }
        #endregion

        #region Public/Private Properties
        public DateTime LaunchDate
        {
            get { return _launchDate; }
            set { _launchDate = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public int EmailsSent
        {
            get { return _emailsSent; }
            set { _emailsSent = value; }
        }
        #endregion

        #region Public/Private Functions
        public static List<StatsBase> Load(eSubsGlobalEnvironment env)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetParticipantZoneStatsByReportType(ParticipantZone_ReportName.EMAIL_OVERVIEW, env.Event.EventID, env.EventParticipation.EventParticipationID);
        }       
        #endregion
    }
}
