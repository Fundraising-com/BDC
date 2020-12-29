using System;
using System.Collections.Generic;
using System.Linq;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
    public struct TouchInfoSalesStat
    {
        public decimal OrderAmount;
        public int Quantity;
        public int MemberHierarchyID;
        public int EventParticipationID;
        public int TouchID;

        public TouchInfoSalesStat(decimal order_amount, int quantity, int mem_h_id, int ep_id, int t_id)
        {
            OrderAmount = order_amount;
            Quantity = quantity;
            MemberHierarchyID = mem_h_id;
            EventParticipationID = ep_id;
            TouchID = t_id;
        }
    }

    /// <summary>
    /// Summary description for TouchInfoSalesReport.
    /// </summary>
    public class TouchInfoSalesReport
    {
        #region Public/Private Members
        public List<TouchInfoSalesStat> SalesStat;
        private const string SESSION_KEY = "_TOUCH_INFO_EMAIL_REPORT_";  
        public int TouchInfoID { get; set; }
        
        #endregion

        #region Public Contructor
        public TouchInfoSalesReport()
        {
            SalesStat = new List<TouchInfoSalesStat>();
            TouchInfoID = int.MinValue;
        }
        #endregion

        #region Public Static/Instance Methods
        public decimal OrderAmountTotal(int member_hierarchy_id)
        {
            decimal results = 0M;
            if (SalesStat.Count > 0)
            {
                foreach (TouchInfoSalesStat stat in SalesStat)
                {
                    if (member_hierarchy_id == int.MinValue || stat.MemberHierarchyID == member_hierarchy_id)
                        results += stat.OrderAmount;
                }
            }
            return results;
        }

        public int QuantityTotal(int member_hierarchy_id)
        {
            int results = 0;
            if (SalesStat.Count > 0)
            {
                foreach (TouchInfoSalesStat stat in SalesStat)
                {
                    if (member_hierarchy_id == int.MinValue || stat.MemberHierarchyID == member_hierarchy_id)
                        results += stat.Quantity;
                }
            }
            return results;
        }

        public static TouchInfoSalesReport GetSalesReportByTouchInfoID(int touch_info_id)
        {
            // retreive the current session
			System.Web.SessionState.HttpSessionState session = System.Web.HttpContext.Current.Session;

            Dictionary<int, TouchInfoSalesReport> email_stats = null;
            TouchInfoSalesReport tisr = null;

            if (session != null && session[SESSION_KEY] != null)
            {
                email_stats = (Dictionary<int, TouchInfoSalesReport>)session[SESSION_KEY];
                if (email_stats != null)
                {
                    if (email_stats.ContainsKey(touch_info_id))
                        tisr = email_stats[touch_info_id];
                }
            }
            
            if (tisr == null)
            {
                DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
                tisr = dbo.GetSalesReportByTouchInfoID(touch_info_id);
                if (tisr != null)
                {
                    if (email_stats == null)
                        email_stats = new Dictionary<int, TouchInfoSalesReport>();

                    email_stats.Add(touch_info_id, tisr);
                    session[SESSION_KEY] = email_stats;
                }
            }

            return tisr;
        }

        public static void Clear()
        {
            // retreive the current session
            System.Web.SessionState.HttpSessionState session = System.Web.HttpContext.Current.Session;
            if (session != null)
            {
                session.Remove(SESSION_KEY);
            }
        }
        #endregion
    }
}
