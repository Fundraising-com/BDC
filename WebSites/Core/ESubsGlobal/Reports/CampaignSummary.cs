using System;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for CampaignSummary.
	/// </summary>
    [Serializable]
	public class CampaignSummary {
        private int _groupID = int.MinValue;
        private int _totalNumberOfEmailSentToGroupMembers = int.MinValue;
        private int _totalNumberOfEmailSentToSupporters = int.MinValue;
		private int _totalNumberOfItemSold;
        private Decimal _totalAmount = Decimal.MinValue;
        private Decimal _totalAmountGross = Decimal.MinValue;
        private Decimal _totalProfit = Decimal.MinValue;
        private DateTime _lauchDate;
		private bool _status;
        private Decimal _goal = Decimal.MinValue;
        private DateTime _lastActivity;
        private Decimal _totalDonationAmount = Decimal.MinValue;

		private const string SESSION_KEY = "_CAMPAIGN_SUMMARY_REPORT_";

		public CampaignSummary() {
			
		}

		public static CampaignSummary Create(eSubsGlobalEnvironment env) {
			return CampaignSummary.Load(env);
		}

		public void Save() {
			if(System.Web.HttpContext.Current != null) {	// web base
				System.Web.HttpContext.Current.Session[SESSION_KEY] = this;
			}
		}


        public static CampaignSummary Load(int eventID) 
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetCampaigSummaryReportByEventID(eventID);
        }

		public static CampaignSummary Load(eSubsGlobalEnvironment env) {
			// retreive the current session
			System.Web.SessionState.HttpSessionState session =
                System.Web.HttpContext.Current.Session;

			// check if this summary has been load before
			bool exists = (session["_CAMPAIGN_SUMMARY_"] != null);

			// at every !Postback, recreate the summary report,
			// isPostBack, return the campaign summary in the session
			if(System.Web.HttpContext.Current.Request.HttpMethod == "GET" || !exists) {
				DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();

				if(env.Event == null) {
					throw new ESubsGlobalException("Unable to get campaign summary report, Event is not set", null, env);
				}

				Personalization pesonalization = Personalization.GetCurrentPersonalization(env.EventParticipation);

				CampaignSummary campaignSummary = dbo.GetCampaigSummaryReportByEventID(env.Event.EventID);
				try {
					campaignSummary.Goal = pesonalization.FundraisingGoal; //env.Personalization.FundraisingGoal;
				} catch { campaignSummary.Goal = 0; }
				campaignSummary.GroupID = env.Group.GroupID;
				campaignSummary.LauchDate = env.Event.StartDate;
				campaignSummary.Status = env.Event.Active;

				if(campaignSummary._goal == Decimal.MinValue) {
					campaignSummary._goal = 0;
				}

				if(campaignSummary._groupID == int.MinValue) {
					campaignSummary._groupID = 0;
				}

				if(campaignSummary._lauchDate == DateTime.MinValue) {
					campaignSummary._lauchDate = DateTime.Now;
				}

				if(campaignSummary._totalNumberOfEmailSentToGroupMembers == int.MinValue) {
					campaignSummary._totalNumberOfEmailSentToGroupMembers = 0;
				}

				if(campaignSummary._totalNumberOfEmailSentToSupporters == int.MinValue) {
					campaignSummary._totalNumberOfEmailSentToSupporters = 0;
				}

				if(campaignSummary._totalNumberOfItemSold == int.MinValue) {
					campaignSummary._totalNumberOfItemSold = 0;
				}

                if (campaignSummary._totalAmountGross == Decimal.MinValue)
                {
                    campaignSummary._totalAmountGross = 0;
                }

                if (campaignSummary._totalProfit == Decimal.MinValue)
                {
                    campaignSummary._totalProfit = 0;
                }

                if (campaignSummary._lastActivity == DateTime.MinValue)
                {
					campaignSummary._lastActivity = DateTime.Now;
				}

                if (campaignSummary._totalAmount == Decimal.MinValue)
                {
                    campaignSummary._totalAmount = 0;
                }

                if (campaignSummary._totalDonationAmount == Decimal.MinValue)
                {
                    campaignSummary._totalDonationAmount = 0;
                }

				// save the report object in the session
				session["_CAMPAIGN_SUMMARY_"] = campaignSummary;

				return campaignSummary;
			} else {
				return (CampaignSummary)session["_CAMPAIGN_SUMMARY_"];
			}
		}

		#region Attributes
		public int GroupID {
			set { _groupID = value; }
			get { return _groupID; }
		}

		public int TotalNumberOfEmailSentToGroupMembers {
			set { _totalNumberOfEmailSentToGroupMembers = value; }
			get { return _totalNumberOfEmailSentToGroupMembers; }
		}

		public int TotalNumberOfEmailSentToSupporters {
			set { _totalNumberOfEmailSentToSupporters = value; }
			get { return _totalNumberOfEmailSentToSupporters; }
		}

		public int TotalNumberOfItemSold {
			set { _totalNumberOfItemSold = value; }
			get { return _totalNumberOfItemSold; }
		}

		public Decimal TotalAmount {
			set { _totalAmount = value; }
			get { return _totalAmount; }
		}

        public Decimal TotalAmountGross
        {
            set { _totalAmountGross = value; }
            get { return _totalAmountGross; }
        }

        public Decimal TotalProfit
        {
			set { _totalProfit = value; }
			get { return _totalProfit; }
		}

		public DateTime LauchDate {
			set { _lauchDate = value; }
			get { return _lauchDate; }
		}

		public bool Status {
			set { _status = value; }
			get { return _status; }
		}

		public Decimal Goal {
			set { _goal = value; }
			get { return _goal; }
		}

		public DateTime LastActivity {
			set { _lastActivity = value; }
			get { return _lastActivity; }
		}

        public Decimal TotalDonationAmount
        {
            set { _totalDonationAmount = value; }
            get { return _totalDonationAmount; }
        }

		#endregion
	}
}
