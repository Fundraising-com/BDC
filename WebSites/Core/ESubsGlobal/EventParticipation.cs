/* Title:	Event participation
 * Author:	Jean-Francois Buist
 * Summary:	Event participation is the join between the member and the event.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;
using GA.BDC.Core.ESubsGlobal.Touch;

namespace GA.BDC.Core.ESubsGlobal {

	public enum EventParticipationStatus: int
	{
		OK =0,
		UNKNOW_ERROR,        
		EVENT_PARTICIPATION_ALREADY_EXISTS
	}

	/// <summary>
	/// Summary description for Invitation.
	/// </summary>
    [Serializable]
	public class EventParticipation : EnvironmentBase {

		#region Fields
		private int _eventParticipationID = int.MinValue;
		private int _eventID = int.MinValue;
		private int _memberHierarchyID = int.MinValue;
		private ParticipationChannel _participationChannel = null;
		private string _salutation = null;
        private int _coppaMonth = int.MinValue;
        private int _coppaYear = int.MinValue;
        private bool _agreeToTermServices = false;
        private bool _holidayReminders = false;
		#endregion

		#region Constructors
		public EventParticipation() 
		{
			_eventParticipationID = int.MinValue;
			_participationChannel = new ParticipationChannel();
		}

		public EventParticipation(Event _event, Users.eSubsGlobalUser user, ParticipationChannel pc):this(_event, user, pc, int.MinValue, int.MinValue, false){}
        public EventParticipation(Event _event, Users.eSubsGlobalUser user, ParticipationChannel pc, int coppaYear, int coppaMonth, bool terms)
        {
            _eventID = _event.EventID;
            _memberHierarchyID = user.HierarchyID;
            _participationChannel = pc;
            _salutation = user.CompleteName;
            _coppaYear = coppaYear;
            _coppaMonth = coppaMonth;
            _agreeToTermServices = terms;
        }
		#endregion

		#region Methods
        public EventParticipationStatus InsertIntoDatabase() 
		{
			try 
			{
				DataAccess.ESubsGlobalDatabase dbo =
					new DataAccess.ESubsGlobalDatabase();

				return dbo.InsertEventParticipation(_eventID, _memberHierarchyID, _participationChannel.ParticipationChannelID, _salutation, 
                    _coppaMonth, _coppaYear, _agreeToTermServices, _holidayReminders, ref _eventParticipationID);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}

        public void UpdateInDatabase()
        {
            try
            {
                DataAccess.ESubsGlobalDatabase dbo =
                    new DataAccess.ESubsGlobalDatabase();

                dbo.UpdateEventParticipation(_eventParticipationID, _eventID, _memberHierarchyID, _participationChannel.ParticipationChannelID, _salutation,
                    _coppaMonth, _coppaYear, _agreeToTermServices, _holidayReminders);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }
        }        		

		#region TBD
		/*
		public void InsertInvitation7777(Invitation inv)
		{
			if (inv != null)
			{
				// Make sure EventParticipation has been inserted 
				// first before creating invitation.
				if (_eventParticipationID != int.MinValue && _eventParticipationID == inv.EventParticipationID)
				{
					try 
					{
						ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
						dbo.InsertInvitation(inv);
					}
					catch (Exception ex)
					{
						throw new ESubsGlobalException("Cannot insert invitation into database.", ex, this);
					}
				}
				else 
				{
					throw new ESubsGlobalException("Cannot insert invitation without a valid EventParticipation.", null, this);
				}
			}
		}*/
		#endregion

		public static EventParticipation GetEventParticipationByMemberHierarchyID(int memberHierarchyID) {
			return EventParticipation.GetEventParticipationByMemberHierarchyIDandCheckEventID(memberHierarchyID, int.MinValue);
		}

		public static EventParticipation GetEventParticipationByMemberHierarchyIDandCheckEventID(int memberHierarchyID, int eventID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventParticipationByMemberHierarchyID(memberHierarchyID, eventID);
		}

		public static EventParticipation GetEventParticipationByTouchID(int touchID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventParticipationByTouchID(touchID);
		}

		public static EventParticipation[] GetEventParticipationsByFacebookID(int facebookID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventParticipationsByFacebookID(facebookID);
		}

		public static EventParticipation GetEventParticipationByEventParticipationID(int eventParticipationID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEventParticipationByEventParticipationID(eventParticipationID);
		}

        public static bool IsExistEventParticipationByMemberHierarchyIDAndEventID(int memberHierarchyID, int eventID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.IsExistEventParticipationByMemberHierarchyIDAndEventID(memberHierarchyID, eventID);
        }
		#endregion

		#region Properties
		public int EventParticipationID 
		{
			set { _eventParticipationID = value; }
			get { return _eventParticipationID; }
		}

		public int EventID {
			set { _eventID = value; }
			get { return _eventID; }
		}

		public int MemberHierarchyID {
			set { _memberHierarchyID = value; }
			get { return _memberHierarchyID; }
		}

		public ParticipationChannel ParticipationChannel {
			set { _participationChannel = value; }
			get { return _participationChannel; }
		}

		public string Salutation {
			get { return _salutation; }
			set { _salutation = value; }
		}

        public int CoppaYear
        {
            set { _coppaYear = value; }
            get { return _coppaYear; }
        }

        public int CoppaMonth
        {
            set { _coppaMonth = value; }
            get { return _coppaMonth; }
        }

        public bool AgreeToTermServices
        {
            set { _agreeToTermServices = value; }
            get { return _agreeToTermServices; }
        }

        public bool HolidayReminders 
        {
            set { _holidayReminders = value; }
            get { return _holidayReminders; }
        }

		#endregion
	}
}
