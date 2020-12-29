using System;
using GA.BDC.Core.ESubsGlobal.Users;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for GroupStat.
	/// </summary>
    [Serializable]
	public class GroupStat {
        private int eventID = int.MinValue;
        private int numberOfMembers = int.MinValue;
        private int numberOfParticipants = int.MinValue;
        private int numberOfActiveParticipants = int.MinValue;
        private int numberOfSupporters = int.MinValue;
        private int numberOfReminderContacts = int.MinValue;
        private int numberOfSubs = int.MinValue;
        private Decimal amount = Decimal.MinValue;
        private Decimal donationAmount = Decimal.MinValue;
        private Decimal amountGross = Decimal.MinValue;
        private Decimal profit = Decimal.MinValue;


        GA.BDC.Core.ESubsGlobal.Users.eSubsGlobalUser m_user=null;

		private const string SESSION_KEY = "_GROUP_STATS_";

		public GroupStat() {

		}

        public void SetUser(GA.BDC.Core.ESubsGlobal.Users.eSubsGlobalUser user)
        {
            m_user = user;
        }

		public void Save() {
			if(System.Web.HttpContext.Current != null) {	// web base
				System.Web.HttpContext.Current.Session[SESSION_KEY] = this;
			}
		}

        public static void Clear()
        {
            System.Web.HttpContext.Current.Session.Remove(SESSION_KEY);
        }

		public static GroupStat Create(eSubsGlobalEnvironment env) {
            return GroupStat.Load(env);			
		}


        public static GroupStat Create(eSubsGlobalEnvironment env, eSubsGlobalUser  user)
        {
            return GroupStat.Load(env,user);
        }


        public static GroupStat Load(eSubsGlobalEnvironment env)
        {
            return Load(env, eSubsGlobalUser.Create());
        }

        public static GroupStat Load(eSubsGlobalEnvironment env, eSubsGlobalUser user)
        {
            // retreive the current session
            System.Web.SessionState.HttpSessionState session =
                System.Web.HttpContext.Current.Session;

            // check if this summary has been load before
            bool exists = (session[SESSION_KEY] != null);

            // at every !Postback, recreate the summary report,
            // isPostBack, return the campaign summary in the session
            if (System.Web.HttpContext.Current.Request.HttpMethod == "GET" || !exists)
            {
                DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
                GroupStat gs = dbo.GetGroupStat(env.Event.EventID);

                gs.NumberOfReminderContacts = (user.HierarchyID != int.MinValue ? GroupStat.GetNumberOfReminderContacts(user) : 0);
                gs.ResetNulls();

                // save the report object in the session
                session[SESSION_KEY] = gs;

                return gs;
            }
            else
            {
                return (GroupStat)session[SESSION_KEY];
            }
            
        }

        public static int GetNumberOfReminderContacts()
        {
            return GetNumberOfReminderContacts(null);
        }

        public static int GetNumberOfReminderContacts(eSubsGlobalUser p_user)
        {
            // retreive the user and it's environment
            GA.BDC.Core.ESubsGlobal.Users.eSubsGlobalUser user;
            if (p_user == null)
            { 
                user = GA.BDC.Core.ESubsGlobal.Users.eSubsGlobalUser.Create();
            }
            else
            {
                user = p_user;
            }

            eSubsGlobalEnvironment env = eSubsGlobalEnvironment.Create();

            return GA.BDC.Core.ESubsGlobal.Users.eSubsGlobalUser.GetNbReminderContact(user.HierarchyID, env.Event.EventID);;
        }

		private void ResetNulls() {
			if(eventID == int.MinValue) {
				eventID = 0;
			}

			if(numberOfMembers == int.MinValue) {
				numberOfMembers = 0;
			}
			if(numberOfParticipants == int.MinValue) {
				numberOfParticipants = 0;
			}
			if(numberOfActiveParticipants == int.MinValue) {
				numberOfActiveParticipants = 0;
			}
			if(numberOfSupporters == int.MinValue) {
				numberOfSupporters = 0;
			}
			if(numberOfSubs == int.MinValue) {
				numberOfSubs = 0;
			}
            if (amount == Decimal.MinValue)
            {
                amount = 0;
            }
            if (donationAmount == Decimal.MinValue)
            {
                donationAmount = 0;
            }
            if (amountGross == Decimal.MinValue)
            {
                amountGross = 0;
            }
            if (profit == Decimal.MinValue)
            {
				profit = 0;
			}
            if (numberOfReminderContacts == int.MinValue)
            {
                numberOfReminderContacts = 0;
            }
		}        

		#region Properties
		public int EventID {
			set { eventID = value; }
			get { return (eventID); }
		}

		public int NumberOfMembers {
			set { numberOfMembers = value; }
			get { return (numberOfMembers); }
		}

		public int NumberOfParticipants {
			set { numberOfParticipants = value; }
			get { return numberOfParticipants; }
		}

		public int NumberOfActiveParticipants {
			set { numberOfActiveParticipants = value; }
			get { return numberOfActiveParticipants; }
		}

		public int NumberOfSupporters {
			set { numberOfSupporters = value; }
			get { return numberOfSupporters; }
		}

        public int NumberOfReminderContacts
        {
            set { numberOfReminderContacts = value; }
            get { return numberOfReminderContacts; }
        }

		public int NumberOfSubs {
			set { numberOfSubs= value; }
			get { return numberOfSubs; }
		}

        public Decimal Amount
        {
            set { amount = value; }
            get { return amount; }
        }

        public Decimal DonationAmount
        {
            set { donationAmount = value; }
            get { return donationAmount; }
        }

        public Decimal AmountGross
        {
            set { amountGross = value; }
            get { return amountGross; }
        }

        public Decimal Profit
        {
			set { profit = value; }
			get { return profit; }
		}
		#endregion
	}
}
