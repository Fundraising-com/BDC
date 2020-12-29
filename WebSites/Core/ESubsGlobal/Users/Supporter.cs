/* Title:	Supporter
 * Author:	Jean-Francois Buist
 * Summary:	Participant is a level of user in esubs, this often represents the person
 *			who decides to help the fundraise.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal.Users {
	/// <summary>
	/// Summary description for Supporter.
	/// </summary>
    [Serializable]
	public class Supporter : eSubsGlobalUser	{
		public Supporter() {

		}

		public static InsertMemberIntoDatabaseReturnValue Insert(Supporter supporter) {
			InsertMemberIntoDatabaseReturnValue insertMemberIntoDatabaseReturnValue =
				InsertMemberIntoDatabaseReturnValue.OK;

			if(supporter.ID > -1) {
				insertMemberIntoDatabaseReturnValue = supporter.UpdateInDatabase();
			} else {
				insertMemberIntoDatabaseReturnValue = supporter.InsertIntoDatabase(false);
				supporter.UserTypeInfo = UserType.SUPPORTER;
			}
			return insertMemberIntoDatabaseReturnValue;
		}

        public Supporter(string fullName,
                            string emailAddress, string password, string comments,
                            CreationChannel creationChannel, Culture culture, Users.eSubsGlobalUser parent,
                            Common.PhoneNumberCollection pnc, Common.PostalAddressCollection pac, int partnerID)
            : base(fullName, emailAddress, password, comments, creationChannel, culture, parent, pnc, pac, partnerID, true)
        {

            // Set UserTypeInfo to Supporter
            UserTypeInfo = UserType.SUPPORTER;
        }


		public Supporter(string fullName, 
							string emailAddress, string password, string comments, 
							CreationChannel creationChannel, Culture culture, Users.eSubsGlobalUser parent,
							Common.PhoneNumberCollection pnc, Common.PostalAddressCollection pac, int partnerID, bool emailRequired)
            : base(fullName, emailAddress, password, comments, creationChannel, culture, parent, pnc, pac, partnerID, emailRequired)
        {

			// Set UserTypeInfo to Supporter
			UserTypeInfo = UserType.SUPPORTER;
		}

		public Supporter(eSubsGlobalUser user) : base(user) {

		}
	}
}
