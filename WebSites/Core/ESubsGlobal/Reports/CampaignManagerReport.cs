using System;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
    [Serializable]
    public class CampaignManagerReport
    {
        #region Private Fields
        private List<StatsBase> _emailOverviewStats;
        private List<StatsBase> _participationCenterStats;
        private List<StatsBase> _topSellersStats;

        private const string SESSION_KEY = "_CAMPAIGN_MANAGER_REPORT_";
        #endregion

        #region Public Fields
        public enum ReportName
        {
            EMAIL_OVERVIEW,
            PARTICIPATION_CENTER,
            TOP_SELLERS
        }
        #endregion

        #region Constructor
        public CampaignManagerReport(){ }
        #endregion

        #region Public/Private Properties
        public List<StatsBase> CMEmailOverview
        {
            get { return _emailOverviewStats; }
            set { _emailOverviewStats = value; }
        }
        public List<StatsBase> CMParticipationCenter
        {
            get { return _participationCenterStats; }
            set { _participationCenterStats = value; }
        }
        public List<StatsBase> CMTopSellers
        {
            get { return _topSellersStats; }
            set { _topSellersStats = value; }
        }
        #endregion

        #region Public/Private Functions
        public static bool IsCampaignManagerReportLoadedInSession()
        {
            return (System.Web.HttpContext.Current.Session[SESSION_KEY] != null);
        }

        public static void RemoveCampaignManagerReportFromSession()
        {
            if (System.Web.HttpContext.Current.Session[SESSION_KEY] != null)
                System.Web.HttpContext.Current.Session.Remove(SESSION_KEY);
        }

        public static void RemoveFromSession(ReportName rname)
        {
            CampaignManagerReport cmReport = Load(System.Web.HttpContext.Current.Session);
            switch (rname)
            {
                case ReportName.EMAIL_OVERVIEW:
                    cmReport.CMEmailOverview = null;
                    break;
                case ReportName.PARTICIPATION_CENTER:
                    cmReport.CMParticipationCenter = null;
                    break;
                case ReportName.TOP_SELLERS:
                    cmReport.CMTopSellers = null;
                    break;
            }
            Save(cmReport);
        }

        public static void Save(CampaignManagerReport cmReport)
        {
            System.Web.HttpContext.Current.Session[SESSION_KEY] = cmReport;
        }

        public static void Save(System.Web.SessionState.HttpSessionState session, ReportName rname, List<StatsBase> cmStats)
        {
            CampaignManagerReport cmReport = Load(session);
            if (cmReport == null)
                return;
            switch (rname)
            {
                case ReportName.EMAIL_OVERVIEW:
                    cmReport.CMEmailOverview = cmStats;
                    break;
                case ReportName.PARTICIPATION_CENTER:
                    cmReport.CMParticipationCenter = cmStats;
                    break;
                case ReportName.TOP_SELLERS:
                    cmReport.CMTopSellers = cmStats;
                    break;
            }
            session[SESSION_KEY] = cmReport;
        }

        public static CampaignManagerReport Load(System.Web.SessionState.HttpSessionState session)
        {
            if (session[SESSION_KEY] == null)
                return new CampaignManagerReport();
            else
                return session[SESSION_KEY] as CampaignManagerReport;
        }

        public static List<StatsBase> Load(System.Web.SessionState.HttpSessionState session, ReportName rname, eSubsGlobalEnvironment env, bool saveToSession)
        {
            // check if this reporttype has been loaded before in session
            bool exists = ReportExistInSession(session, rname);

			if (!exists)
            {
                List<StatsBase> stats = null;
                switch (rname)
                {
                    case ReportName.EMAIL_OVERVIEW:
                        stats = EmailOverviewStats.Load(env);
                        break;
                    case ReportName.PARTICIPATION_CENTER:
                        stats = ParticipationCenterStats.Load(env);
                        break;
                    case ReportName.TOP_SELLERS:
                        stats = TopSellersStats.Load(env);
                        break;
                }
                if (saveToSession)
                    Save(session, rname, stats);
                return stats;
            }
            else
            {
                CampaignManagerReport cmReport = Load(session);
                switch (rname)
                {
                    case ReportName.EMAIL_OVERVIEW:
                        return cmReport.CMEmailOverview;
                    case ReportName.PARTICIPATION_CENTER:
                        return cmReport.CMParticipationCenter;
                    case ReportName.TOP_SELLERS:
                        return cmReport.CMTopSellers;
                }
            }
            return null;
        }

        private static bool ReportExistInSession(System.Web.SessionState.HttpSessionState session, ReportName rname)
        {
            if (session[SESSION_KEY] == null)
                return false;
            else
            {
                CampaignManagerReport cmReport = session[SESSION_KEY] as CampaignManagerReport;
                if (cmReport == null)
                    return false;
                else
                {
                    switch (rname)
                    {
                        case ReportName.EMAIL_OVERVIEW:
                            if (cmReport.CMEmailOverview != null)
                                return true;
                            else
                                return false;
                        case ReportName.PARTICIPATION_CENTER:
                            if (cmReport.CMParticipationCenter != null)
                                return true;
                            else
                                return false;
                        case ReportName.TOP_SELLERS:
                            if (cmReport.CMTopSellers != null)
                                return true;
                            else
                                return false;
                    }
                    return false;
                }
            }
        }
        #endregion
    }
}
