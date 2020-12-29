/* Title:	Participant
 * Author:	Jean-Francois Buist
 * Summary:	UnknownUser is a class used only to retreive users from database.
 *			This class is implicitly castable with Sponsor, Participant and Supporter.
 *			Please use this class only to retreive users from database, do not put
 *			logics in this class.  Do not process logic with this class.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal.Users {
    [Serializable]
	public class UnknownUser : eSubsGlobalUser {

		public UnknownUser() {

		}

		public UnknownUser(string fullName, 
							string emailAddress, string password, string comment, CreationChannel creationChannel, 
							Culture culture, Users.eSubsGlobalUser parent, Common.PhoneNumberCollection pnc, 
							Common.PostalAddressCollection pac, int partnerID)
							: base(fullName, emailAddress, password, comment, creationChannel, culture, parent, pnc, pac, partnerID) {

			// Set UserTypeInfo to Unknown
			UserTypeInfo = UserType.UNKNOWN;
		}

		public static implicit operator Sponsor(UnknownUser user) {
			Sponsor sponsor = new Sponsor(user);
			return sponsor;
		}

		public static implicit operator Participant(UnknownUser user) {
			Participant participant = new Participant(user);
			return participant;
		}

		public static implicit operator Supporter(UnknownUser user) {
			Supporter supporter = new Supporter(user);
			return supporter;
		}

		public static UnknownUser GetUserByHierarchyID(int hierarchyID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUnknownUser(hierarchyID);
		}

	}
}
