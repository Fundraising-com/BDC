/* Title:	Invitation
 * Author:	Jean-Francois Buist
 * Summary:	Hold invitation information about an Event Participation.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Touch {
	/// <summary>
	/// Summary description for Invitation.
	/// </summary>
    [Serializable]
	public class Invitation : EnvironmentBase {

		#region Fields
		private int _touchID = int.MinValue;
		private int _eventParticipationID = int.MinValue;
		private int _touchInfoID = int.MinValue;
		#endregion

		#region Constructors
		public Invitation() 
		{

		}

		public Invitation(int eventParticipationId, int touchInfoId)
		{
			_eventParticipationID = eventParticipationId;
			_touchInfoID = touchInfoId;
		}
		#endregion

		#region Methods
		public void InsertIntoDatabase() 
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				dbo.InsertInvitation(this);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException("Cannot insert invitation into database.", ex, this);
			}
		}

        public void InsertIntoDatabase(int extTrackID)
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.InsertInvitation(this, extTrackID);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException("Cannot insert invitation into database.", ex, this);
            }
        }
		#endregion

        public static void InsertEmail(int template, int businessRule, int evtParticipation)
        {
            eSubsGlobalEnvironment env = eSubsGlobalEnvironment.Create();
            ESubsGlobal.Touch.TouchInfo touch = null;
            ESubsGlobal.Touch.EmailTemplate emailTemplate = ESubsGlobal.Touch.EmailTemplate.Create(template, env.CurrentCulture.CultureCode);
            if (emailTemplate != null)
            {
                touch = new GA.BDC.Core.ESubsGlobal.Touch.TouchInfo(-1, businessRule, emailTemplate.Subject, emailTemplate.TextBody, emailTemplate.HtmlBody, DateTime.MinValue);
                touch.InsertDatabase();
            }
            else
            {
                throw new ESubsGlobalException("Failed to create email template object");
            }

            if (touch.TouchInfoID != int.MinValue)
            {
                ESubsGlobal.Touch.Invitation inv = new GA.BDC.Core.ESubsGlobal.Touch.Invitation(evtParticipation, touch.TouchInfoID);
                inv.InsertIntoDatabase();
            }
            else
            {
                throw new ESubsGlobalException("Failed to create touch object!");
            }
        }



        public static void InsertEmail(int template, int businessRule, int evtParticipation, int extTrackingID)
        {
            eSubsGlobalEnvironment env = eSubsGlobalEnvironment.Create();
            ESubsGlobal.Touch.TouchInfo touch = null;
            ESubsGlobal.Touch.EmailTemplate emailTemplate = ESubsGlobal.Touch.EmailTemplate.Create(template, env.CurrentCulture.CultureCode);
            if (emailTemplate != null)
            {
                touch = new GA.BDC.Core.ESubsGlobal.Touch.TouchInfo(-1, businessRule, emailTemplate.Subject, emailTemplate.TextBody, emailTemplate.HtmlBody, DateTime.MinValue);
                touch.InsertDatabase();
            }
            else
            {
                throw new ESubsGlobalException("Failed to create email template object");
            }

            if (touch.TouchInfoID != int.MinValue)
            {
                ESubsGlobal.Touch.Invitation inv = new GA.BDC.Core.ESubsGlobal.Touch.Invitation(evtParticipation, touch.TouchInfoID);
                inv.InsertIntoDatabase(extTrackingID);
            }
            else
            {
                throw new ESubsGlobalException("Failed to create touch object!");
            }
        }

		#region Properties
		public int TouchInfoID
		{
			get {return _touchInfoID;}
			set {_touchInfoID = value;}
		}

		public int TouchID
		{
			get { return _touchID; }
			set { _touchID = value; }
		}

		public int EventParticipationID
		{
			get { return _eventParticipationID; }
			set { _eventParticipationID = value; }
		}

		#endregion
	}
}
