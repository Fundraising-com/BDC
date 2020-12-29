using System;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
    [Serializable]
    public class ImageMotivatorStatsCollection
    {
        private Dictionary<int, List<Reports.StatsBase>> _statsCollection;

        public ImageMotivatorStatsCollection() 
        {
            _statsCollection = new Dictionary<int, List<StatsBase>>();
        }

        public Dictionary<int, List<Reports.StatsBase>> StatsCollection
        {
            get { return _statsCollection; }
        }

        public static List<Reports.StatsBase> Load(eSubsGlobalEnvironment env)
        {
            // retreive the current session
            System.Web.SessionState.HttpSessionState session =
                System.Web.HttpContext.Current.Session;

            // check if this image motivator stats for the event_id has been load before
            ImageMotivatorStatsCollection imsc = null;
            List<Reports.StatsBase> result = null;

            if (session["_IMAGE_MOTIVATOR_"] != null)
            {
                imsc = (ImageMotivatorStatsCollection)session["_IMAGE_MOTIVATOR_"];
                Dictionary<int, List<Reports.StatsBase>> stats = imsc.StatsCollection;
                if (stats.TryGetValue(env.Event.EventID, out result))
                    return result;  
            }

			//If it gets to this point, the collection has not been loaded yet
            if (env.Event != null)
            {
                result = ImageMotivatorStats.LoadByEventID(env.Event.EventID);
                if (imsc == null) imsc = new ImageMotivatorStatsCollection();
                imsc.StatsCollection.Add(env.Event.EventID, result);
                session["_IMAGE_MOTIVATOR_"] = imsc;
            }
            return result;
        }

        public static List<Reports.StatsBase> GetTopRandomImageMotivatorByPartnerID(Int32 PartnerID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetTopRandomImageMotivatorByPartnerID(PartnerID);
        }
    }

    class ImageMotivatorStats : StatsBase, IComparable<ImageMotivatorStats>
    {
        #region Public/Private Fields
        private string _fullName;
        private decimal _totalAmount;
        private DateTime _createdate;
        private string _comment;
        #endregion

        #region Constructor
        public ImageMotivatorStats() : this(string.Empty, 0M, DateTime.MinValue, string.Empty) { }
        public ImageMotivatorStats(string fullname, decimal totalamount, DateTime createdate, string comment) 
        {
            _fullName = fullname;
            _totalAmount = totalamount;
            _createdate = createdate;
            _comment = comment;
        }
        #endregion

        #region Public/Private Properties
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        public DateTime CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        public string TotalAmountString
        {
            get { return _totalAmount.ToString("$###,###,##0.00"); }
        }
        #endregion

        #region Public/Private Functions
        /* Define sorting rule: Sort by descending order of Contribution amount */
        public int CompareTo(ImageMotivatorStats other) { return other.TotalAmount.CompareTo(TotalAmount); }

        public static List<Reports.StatsBase> LoadByEventID(int eventid)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetCampaignSupportersV2(eventid);
        }        
        #endregion
    }
}
