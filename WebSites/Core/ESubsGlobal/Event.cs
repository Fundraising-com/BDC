/* Title:	Event
 * Author:	Jean-Francois Buist
 * Summary:	An event is when a group creates a fundraise for a period of time.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using GA.BDC.Core.ESubsGlobal.DataAccess;
using GA.BDC.Core.EnterpriseStandards;
using GA.BDC.Core.Data.Sql;

namespace GA.BDC.Core.ESubsGlobal {

	public enum InsertUpdateStatus : int 
	{
		OK,
		NAME_OR_REDIRECT_ALREADY_EXISTS,
		UNKNOW_ERROR
	}


	public enum InsertUpdateEventStatus : int 
	{
		OK = 0,
		REDIRECT_ALREADY_EXISTS = 1,
		EVENT_NAME_OR_REDIRECT_ALREADY_EXISTS = 2,
		INTERNAL_ERROR = -1
	}


	/// <summary>
	/// Summary description for Event.
	/// </summary>
	[Serializable]
	public class Event : EnvironmentBase {


		public enum EventStatusInfo : int {
			FUNDRAISING_CAMPAIGN = 1,
			SCHEDULE = 2,
            FUNDRAISING_CAMPAIGN_RELAUNCH = 3 
		}

        public enum EventTypeInfo : int
        {
            GROUP_FUNDRAISER_WITH_SUBPAGE = 1,
            GROUP_FUNDRAISER_WITHOUT_SUBPAGE = 2,
            INDIVIDUAL_FUNDRAISER = 3
        }

        public enum GroupTypeInfo : int {
			UNKNOWN = 1,
			SPORTS = 2,
            SCHOOL = 3,
            RELIGIOUS = 4,
            COMMUNITY = 5,
            FOUNDATION = 6,
            YOUTH = 7,
            OTHER = 8
		}

		#region Fields
		private int eventID = int.MinValue;
		private int eventStatusID = int.MinValue;
		private string name = null;
		private int groupID = int.MinValue;
		private string cultureCode = null;
		private string eventStatus = null;
		private DateTime startDate = DateTime.MinValue;
		private DateTime endDate = DateTime.MinValue;
		private bool active = true;
		private string comments = null;
		private DateTime createDate = DateTime.MinValue;
		private int partnerID = int.MinValue;
		private int sponsorEventParticipationID = int.MinValue;
		private string redirect = null;
		private bool salesRep = false;
        private int grouptypeID = int.MinValue;
        private string grouptype = null;
        private int profitGroupID = int.MinValue;
        private double profitCalculated = double.MinValue;
        private bool processingFee = false;
        private int eventTypeID = int.MinValue;
        private string eventType = null;
        private DateTime dateOfEvent = DateTime.MinValue;

		#endregion

		#region Constructors
		public Event() : this(int.MinValue) {}
		public Event(int eventID) : this (eventID, Event.EventStatusInfo.FUNDRAISING_CAMPAIGN) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo) : this(eventID, eventStatusInfo, null) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name) : this(eventID, eventStatusInfo, name, int.MinValue) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID) : this(eventID, eventStatusInfo, name, groupID, null) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode) : this(eventID, eventStatusInfo, name, groupID, cultureCode, DateTime.MinValue) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, DateTime.MinValue) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, false) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, active, null) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active, string comments) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, active, comments, DateTime.Now) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active, string comments, DateTime createDate) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, active, comments, createDate, int.MinValue) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active, string comments, DateTime createDate, int partnerID) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, active, comments, createDate, partnerID, int.MinValue) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active, string comments, DateTime createDate, int partnerID, int sponsorEventParticipationID) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, active, comments, createDate, partnerID, sponsorEventParticipationID, false) {}
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active, string comments, DateTime createDate, int partnerID, int sponsorEventParticipationID, bool salesRep) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, active, comments, createDate, partnerID, sponsorEventParticipationID, salesRep, null) {}
        public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active, string comments, DateTime createDate, int partnerID, int sponsorEventParticipationID, bool salesRep, string redirect) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, active, comments, createDate, partnerID, sponsorEventParticipationID, salesRep, redirect, Event.GroupTypeInfo.UNKNOWN) { }
		public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active, string comments, DateTime createDate, int partnerID, int sponsorEventParticipationID, bool salesRep, string redirect, GroupTypeInfo grouptypeinfo) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, active, comments, createDate, partnerID, sponsorEventParticipationID, salesRep, redirect, grouptypeinfo, int.MinValue) { }
        public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active, string comments, DateTime createDate, int partnerID, int sponsorEventParticipationID, bool salesRep, string redirect, GroupTypeInfo grouptypeinfo, int profitGroupID) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, active, comments, createDate, partnerID, sponsorEventParticipationID, salesRep, redirect, grouptypeinfo, profitGroupID, double.MinValue) { }
        public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active, string comments, DateTime createDate, int partnerID, int sponsorEventParticipationID, bool salesRep, string redirect, GroupTypeInfo grouptypeinfo, int profitGroupID, double profitCalculated) : this(eventID, eventStatusInfo, name, groupID, cultureCode, startDate, endDate, active, comments, createDate, partnerID, sponsorEventParticipationID, salesRep, redirect, grouptypeinfo, profitGroupID, profitCalculated, false, Event.EventTypeInfo.GROUP_FUNDRAISER_WITH_SUBPAGE) { }
        public Event(int eventID, EventStatusInfo eventStatusInfo, string name, int groupID, string cultureCode, DateTime startDate, DateTime endDate, bool active, string comments, DateTime createDate, int partnerID, int sponsorEventParticipationID, bool salesRep, string redirect, GroupTypeInfo grouptypeinfo, int profitGroupID, double profitCalculated, bool processingFee, EventTypeInfo eventTypeInfo) 
        {
			this.eventID = eventID;
			this.name = name;
			this.groupID = groupID;
			this.cultureCode = cultureCode;

            SetEventStatus(eventStatusInfo);
            SetGroupType(grouptypeinfo);
            SetEventType(eventTypeInfo);

			this.startDate = startDate;
			this.endDate = endDate;
			// this.active = active;
			this.active = true;
			this.comments = comments;
			this.createDate = createDate;
			this.partnerID = partnerID;
			this.sponsorEventParticipationID = sponsorEventParticipationID;
			this.redirect = redirect; //UPDATE August 2010: Replaced by Personalization.Redirect
			this.salesRep = salesRep;
            this.profitGroupID = profitGroupID;
            this.profitCalculated = profitCalculated;
            this.processingFee = processingFee;            
		}

        public Event(string eventName, Partner partner, Group group, Users.eSubsGlobalUser user, EventStatusInfo eventStatusInfo, EventTypeInfo eventTypeInfo)
        {
			CultureCode = user.Culture.CultureCode;
			Active = true;
			StartDate = DateTime.Now;
			EndDate = DateTime.MinValue;
            SetEventStatus(eventStatusInfo);
            SetEventType(eventTypeInfo);
			GroupID = group.GroupID;
			Name = eventName;
            grouptypeID = 1;
            grouptype = "Unknown";
			//Name = group.Name;            
		}

        public Event(Group group, string eventName, string cultureCode, EventStatusInfo eventStatusInfo, EventTypeInfo eventTypeInfo)
        {
			CultureCode = cultureCode;
			Active = true;
			StartDate = DateTime.Now;
			EndDate = DateTime.MinValue;
            SetEventStatus(eventStatusInfo);
            SetEventType(eventTypeInfo);
			GroupID = group.GroupID;
			Name = eventName;
            grouptypeID = 1;
            grouptype = "Unknown";
		}

        public Event(string eventName, Group group, string cultureCode, EventStatusInfo eventStatusInfo, EventTypeInfo eventTypeInfo) 
		{
			CultureCode = cultureCode;
			Active = true;
			StartDate = DateTime.Now;
			EndDate = DateTime.MinValue;
            SetEventStatus(eventStatusInfo);
            SetEventType(eventTypeInfo);
			GroupID = group.GroupID;
			Name = eventName;
            grouptypeID = 1;
            grouptype = "Unknown";
			//Name = group.Name;
		}

		public static eSubsGlobalEnvironment[] GetEventsByMemberHierarchyID(int memberHierarchyID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();

			EventParticipation[] eventParticipations = dbo.GetEventParticipationsByMemberHierarchyID(memberHierarchyID);
            if (eventParticipations == null)
                return null;

			eSubsGlobalEnvironment[] events = new eSubsGlobalEnvironment[eventParticipations.Length];

			int index = 0;
			foreach(EventParticipation eventParticipation in eventParticipations) {
				events[index] = new eSubsGlobalEnvironment();
				events[index].EventParticipation = eventParticipation;
				events[index++].Event = Event.GetEventByEventID(eventParticipation.EventID);
			}
			return events;
		}

		public Event[] GetEventByOrderDate(DateTime start, DateTime end)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventByOrderDate(start, end);
		}
		#endregion

		#region Methods

        private void SetEventStatus(EventStatusInfo eventStatusInfo)
        {
            switch (eventStatusInfo)
            {
                case Event.EventStatusInfo.FUNDRAISING_CAMPAIGN:
                    this.eventStatusID = 1;
                    this.eventStatus = "Fundraising Campaign";
                    break;
                case Event.EventStatusInfo.SCHEDULE:
                    this.eventStatusID = 2;
                    this.eventStatus = "Schedule";
                    break;
                case Event.EventStatusInfo.FUNDRAISING_CAMPAIGN_RELAUNCH:
                    this.eventStatusID = 3;
                    this.eventStatus = "Fundraising Campaign Relaunch";
                    break;
            }
        }

        private void SetGroupType(GroupTypeInfo grouptypeinfo)
        {
            switch (grouptypeinfo)
            {
                case Event.GroupTypeInfo.UNKNOWN:
                    this.grouptypeID = 1;
                    this.grouptype = "Unknown";
                    break;
                case Event.GroupTypeInfo.SPORTS:
                    this.grouptypeID = 2;
                    this.grouptype = "Sports";
                    break;
                case Event.GroupTypeInfo.SCHOOL:
                    this.grouptypeID = 3;
                    this.grouptype = "School";
                    break;
                case Event.GroupTypeInfo.RELIGIOUS:
                    this.grouptypeID = 4;
                    this.grouptype = "Religious";
                    break;
                case Event.GroupTypeInfo.COMMUNITY:
                    this.grouptypeID = 5;
                    this.grouptype = "Community";
                    break;
                case Event.GroupTypeInfo.FOUNDATION:
                    this.grouptypeID = 6;
                    this.grouptype = "Foundation";
                    break;
                case Event.GroupTypeInfo.YOUTH:
                    this.grouptypeID = 7;
                    this.grouptype = "Youth";
                    break;
                case Event.GroupTypeInfo.OTHER:
                    this.grouptypeID = 8;
                    this.grouptype = "Other";
                    break;
            }
        }

        private void SetEventType(EventTypeInfo eventTypeInfo)
        {
            switch (eventTypeInfo)
            {
                case Event.EventTypeInfo.GROUP_FUNDRAISER_WITH_SUBPAGE:
                    this.eventTypeID = 1;
                    this.eventType = "Group Fundraiser With Subpage";
                    break;
                case Event.EventTypeInfo.GROUP_FUNDRAISER_WITHOUT_SUBPAGE:
                    this.eventTypeID = 2;
                    this.eventType = "Group Fundraiser Without Subpage";
                    break;
                case Event.EventTypeInfo.INDIVIDUAL_FUNDRAISER:
                    this.eventTypeID = 3;
                    this.eventType = "Individual Fundraiser";
                    break;
            }
        }

		public InsertUpdateStatus UpdateInDatabase() {
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();

			InsertUpdateEventStatus eventStatus = dbo.UpdateEvent(eventID, name, endDate, redirect, comments, active, salesRep, grouptypeID, Donation, DateOfEvent, cultureCode, profitCalculated);
			switch(eventStatus) {
				case InsertUpdateEventStatus.OK:
					return InsertUpdateStatus.OK;
				case InsertUpdateEventStatus.INTERNAL_ERROR:
				case InsertUpdateEventStatus.EVENT_NAME_OR_REDIRECT_ALREADY_EXISTS:
					return InsertUpdateStatus.NAME_OR_REDIRECT_ALREADY_EXISTS;
			}
			return InsertUpdateStatus.OK;
		}

		// insert this event into the database,
		// this method will set the eventID automatically
		public InsertUpdateStatus InsertEventInDatabase() 
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				InsertUpdateEventStatus status = InsertUpdateEventStatus.OK;
                status = dbo.InsertEvent(groupID, eventStatusID, cultureCode, name, startDate, endDate, active, comments, redirect, salesRep, grouptypeID, profitGroupID, profitCalculated, eventTypeID , dateOfEvent, HumeurRepresentative, ref eventID);
				switch(status) {
					case InsertUpdateEventStatus.OK:
						return InsertUpdateStatus.OK;
					case InsertUpdateEventStatus.INTERNAL_ERROR:
					case InsertUpdateEventStatus.EVENT_NAME_OR_REDIRECT_ALREADY_EXISTS:
						return InsertUpdateStatus.NAME_OR_REDIRECT_ALREADY_EXISTS;
				}
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
			return InsertUpdateStatus.OK;
		}

		

		public static Event GetEventByEventID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventByEventID(id);
		}

		public static Event GetEventByGroupID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventByGroupID(id);
		}

		public static Event GetLatestActiveEventByGroupID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetLatestActiveEventByGroupID(id);
		}

        public static Event GetEventByOrderDetailID(int id)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetEventByOrderDetailID(id);
        }

		public static Event[] GetEventsByOrders(DateTime start, DateTime end) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventsByOrderDates(start, end);
		}

        // Facebook is no longer supported
		public static Event[] GetEventsByFacebookID(int facebookID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventsByFacebookID(facebookID);
		}

        // GoogleMap is no longer supported
		public static Event[] GetEventsForGoogleMap() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventsForGoogleMap();
		}

		public static string[] GetEventsByPaymentID(int paymentID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventsByPaymentID(paymentID);
		}

		public static Event[] GetEventsByGroupID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventsByGroupID(id);
		}

		public static Event GetEventByGroupRedirect(string redirect) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetGroupRedirect(redirect);
		}

		// If exist return member_id. Otherwise return int.MinValue.
		public static int IsExistingRedirect(string redirect) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.IsExistingRedirect(redirect);
		}

		public static int EndEvent(int eventID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.EndEvent(eventID);
		}

        public static Dictionary<int, string> CreateGroupType()
        {
            DataAccess.ESubsGlobalDatabase dba = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            return dba.GetEventGroupType();
        }

        public static bool IsEventProcessingFeeEnabled(int id)
        {
            DataAccess.ESubsGlobalDatabase dba = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            return dba.IsEventProssingFeeEnabled(id);
        }

        public static void UpdateEventProcessingFee(int eventID, bool processingFee)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            dbo.UpdateEventProssingFee(eventID, processingFee);
        }

		public bool Terminate()
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.TerminateEvent(this);
		}

		public int RelaunchEvent() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.RelaunchCampaign(eventID, cultureCode);
		}
		#endregion

		#region Properties
		public int EventID 
		{
			set { eventID = value; }
			get { return eventID; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public int EventStatusID {
			set { eventStatusID = value; }
			get { return eventStatusID; }
		}

		public int GroupID {
			set { groupID = value; }
			get { return groupID; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string EventStatus {
			set { eventStatus = value; }
			get { return eventStatus; }
		}

		public DateTime StartDate {
			set { startDate = value; }
			get { return startDate; }
		}

		public DateTime EndDate {
			set { endDate = value; }
			get { return endDate; }
		}

        public DateTime DateOfEvent
        {
            set { dateOfEvent= value; }
            get { return dateOfEvent; }
        }

		public bool Active {
			set { active = value; }
			get { return active; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}
		
		public int SponsorEventParticipationID {
			set { sponsorEventParticipationID = value; }
			get { return sponsorEventParticipationID; }
		}

        //UPDATE August 2010: Replaced by Personalization.Redirect
		public string Redirect {
			set { redirect = value; }
			get { return redirect; }
		}
        //--------------------------------------------------------

		public bool SalesRep {
			get { return salesRep; }
			set { salesRep = value; }
		}

        public int GroupTypeID
        {
            set { grouptypeID = value; }
            get { return grouptypeID; }
        }

        public string GroupType
        {
            set { grouptype = value; }
            get { return grouptype; }
        }

        public int ProfitGroupID
        {
            get { return profitGroupID; }
            set { profitGroupID = value; }
        }

        public double ProfitCalculated
        {
            get { return profitCalculated; }
            set { profitCalculated = value; }
        }

        public bool ProcessingFee
        {
            get { return processingFee; }
            set { processingFee = value; }
        }

        public int EventTypeID
        {
            set { eventTypeID = value; }
            get { return eventTypeID; }
        }

        public string EventType
        {
            set { eventType = value; }
            get { return eventType; }
        }

        public bool Donation
        {
            get;
            set;
        }

        public bool DiscountSite
        {
            get;
            set;
        }

        public string HumeurRepresentative
        {
            get;
            set;
        }
		#endregion
	}
}
