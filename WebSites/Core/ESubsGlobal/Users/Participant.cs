/* Title:	Participant
 * Author:	Jean-Francois Buist
 * Summary:	Participant is a level of user in esubs, this often represents the kid.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Users {
	/// <summary>
	/// Summary description for Participant.
	/// </summary>
    [Serializable]
	public class Participant : eSubsGlobalUser {

		public string _externalGroupID = string.Empty;

		public Participant() {

		}

		public static UnknownUser[] GetUsersFromPartnerTemporaryTable(string externalGroupID, int partnerID)
		{
			UnknownUser[] users = null;
			DataAccess.ESubsGlobalDatabase dbo =
				new DataAccess.ESubsGlobalDatabase();
			users = dbo.GetMembersFromTemp(externalGroupID, partnerID, false);
			return users;
		}

		public static InsertMemberIntoDatabaseReturnValue InsertIntoTemporaryTable(Participant participant)
		{
			InsertMemberIntoDatabaseReturnValue insertMemberIntoDatabaseReturnValue =
			 InsertMemberIntoDatabaseReturnValue.OK;
		
			// Insert member
			DataAccess.ESubsGlobalDatabase dbo =
				new DataAccess.ESubsGlobalDatabase();
			insertMemberIntoDatabaseReturnValue = dbo.InsertMemberIntoTempTable(null, participant._culture.CultureCode, (int)participant._optInStatusID ,
				participant._firstName, participant._middleName, participant._lastName, participant._gender, participant._emailAddress, participant._externalMemberID, participant._externalGroupID,
				participant._partnerID, participant._password, participant._comments, participant._creationChannel.ID); 
			return insertMemberIntoDatabaseReturnValue;
		}

		public static InsertMemberIntoDatabaseReturnValue Insert(Participant participant) {
			InsertMemberIntoDatabaseReturnValue insertMemberIntoDatabaseReturnValue =
				InsertMemberIntoDatabaseReturnValue.OK;

			if(participant.ID > -1) {
				insertMemberIntoDatabaseReturnValue = participant.UpdateInDatabase();
				participant.Save();
			} else {
				insertMemberIntoDatabaseReturnValue = participant.InsertIntoDatabase(false);
				participant.UserTypeInfo = UserType.PARTICIPANT;
				participant.Save();
			}
			return insertMemberIntoDatabaseReturnValue;
		}

		public static InsertMemberIntoDatabaseReturnValue Insert2(Participant p) 
		{

			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			CampaignStatus campStatus = dbo.InsertMemberAndMemberHierachy(null, p, false);
			if (campStatus != CampaignStatus.OK)
				return InsertMemberIntoDatabaseReturnValue.UNKNOWN_ERROR;

			return InsertMemberIntoDatabaseReturnValue.OK;
		}

		public Participant(string fullName, 
							string emailAddress, string password, string comments, CreationChannel creationChannel, 
							Culture culture, Users.eSubsGlobalUser parent, Common.PhoneNumberCollection pnc, 
							Common.PostalAddressCollection pac, int partnerID)
							: base(fullName, emailAddress, password, comments, creationChannel, culture, parent, pnc, pac, partnerID) {

			// Set UserTypeInfo to Participant
			UserTypeInfo = UserType.PARTICIPANT;
		}

		public Participant(string fullName, 
			string emailAddress, string password, string comments, CreationChannel creationChannel, 
			Culture culture, Users.eSubsGlobalUser parent, Common.PhoneNumberCollection pnc, 
			Common.PostalAddressCollection pac, int partnerID, string externalMemberId)
			: base(fullName, emailAddress, password, comments, creationChannel, culture, parent, pnc, pac, partnerID, externalMemberId) 
		{

			// Set UserTypeInfo to Participant
			UserTypeInfo = UserType.PARTICIPANT;
		}

		public Participant(eSubsGlobalUser user) : base(user) {

		}

		public static UnknownUser[] GetParticipantsByEvent(int eventId)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.GetParticipantsByEvent(eventId);
		}

		public bool Move(Group grp, Group newGrp)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.MoveParticipant(this, grp, newGrp);
		}
	}
}
