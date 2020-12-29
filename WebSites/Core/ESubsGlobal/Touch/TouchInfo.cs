using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
	public enum RuleType
	{
		None = int.MinValue,
		FirstEmailToParticipantFromSponsor = 59,
		ReminderEmailToParticipantFromSponsor = 60,
		PersonalNoteToParticipantFromSponsor = 61,
		FirstEmailToSupporterFromParticipant = 62,
		FirstReminderEmailToSupporterFromParticipant = 63,
		LastReminderEmailToSupporterFromParticipant = 64,
		ReminderEmailToSupporterFromParticipant = 65,
		FirstEmailToSupporterFromSponsor = 66,
		FirstReminderEmailToSupporterFromSponsor = 67,
		LastReminderEmailToSupporterFromSponsor = 68,
		PersonalNoteToSupporterFromParticipant = 69,
	}
	
	/// <summary>
	/// Summary description for TouchInfo.
	/// </summary>
    [Serializable]
	public class TouchInfo : EnvironmentBase
	{
		#region Fields
		private int _touchInfoID = int.MinValue;
		private int _visitorLogID = int.MinValue;
		private int _ruleID = int.MinValue;
		private string _subject = null;
		private string _textBody = null;
		private string _htmlBody = null;
        private int _businessRuleId = int.MaxValue;
		private DateTime _launchDate = DateTime.MinValue;
        private int _reminder_interval_day = int.MinValue;
		#endregion

		#region Constructors
		public TouchInfo()
		{

		}

		public TouchInfo(int visitorLogId, int ruleID, string subject, string textBody, string htmlBody, DateTime launchDate)
		{
			_visitorLogID = visitorLogId;
			_ruleID = ruleID;
			_subject = subject;
			_textBody = textBody;
			_htmlBody = htmlBody;
			_launchDate = launchDate;
		}
		#endregion

		#region Methods
		public void InsertDatabase()
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				dbo.InsertTouchInfo(this);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}

        public void UpdateDatabase()
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.InsertTouchInfo(this);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }
        }


		public static TouchInfo Create(int touchInfoId)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.GetTouchInfo(touchInfoId);
		}

        public static TouchInfo LoadByTouchInfoId(int touchInfoId)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.LoadByTouchInfoId(touchInfoId);
        }
		#endregion

		#region Properties
		public int TouchInfoID
		{
			get { return _touchInfoID; }
			set { _touchInfoID = value; }
		}

		public int RuleID
		{
			get { return _ruleID; }
			set { _ruleID = value; }
		}

		public int VisitorLogID
		{
			get { return _visitorLogID; }
			set { _visitorLogID = value; }
		}

		public string Subject
		{
			get { return _subject; }
			set { _subject = value; }
		}

		public string TextBody
		{
			get { return _textBody; }
			set { _textBody = value; }
		}

		public string HtmlBody
		{
			get { return _htmlBody; }
			set { _htmlBody = value; }
		}

		public DateTime LaunchDate
		{
			get { return _launchDate; }
			set { _launchDate = value; }
		}

        public int BusinessRuleId
        {
            get { return _businessRuleId; }
            set { _businessRuleId = value; }
        }

        public int reminder_interval_day
        {
            set { _reminder_interval_day = value; }
            get { return _reminder_interval_day; }
        }

		#endregion

	}
}
