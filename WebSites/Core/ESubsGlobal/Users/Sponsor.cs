/* Title:	Participant
 * Author:	Jean-Francois Buist
 * Summary:	Sponsor is a level of user in esubs, this often represents the coachs.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using GA.BDC.Core.ESubsGlobal.Common;
using GA.BDC.Core.EnterpriseStandards;

namespace GA.BDC.Core.ESubsGlobal.Users {
	/// <summary>
	/// Summary description for Sponsor.
	/// </summary>
    [Serializable]
	public class Sponsor : eSubsGlobalUser {
		
		public Sponsor() : this(int.MinValue) {}
		public Sponsor(int _id) : this(_id, int.MinValue) { }
		public Sponsor(int _id, int _hierarchyID) : this(_id, _hierarchyID, int.MinValue) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID) : this(_id, _hierarchyID, _hierarchyParentID, Culture.DEFAULT)  { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture) : this(_id, _hierarchyID, _hierarchyParentID, _culture, OptInStatus.OPTIN) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, null)  { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, int.MinValue)  { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, null)  { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, null)  { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, false) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, DateTime.Now) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, false) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, int.MinValue) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe, int leadID)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, leadID, null) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe, int leadID, UserCollection children)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, leadID, children, UserType.UNKNOWN) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe, int leadID, UserCollection children, UserType _userTypeInfo)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, leadID, children, _userTypeInfo, SortUserBy.EmailAddress) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe, int leadID, UserCollection children, UserType _userTypeInfo, SortUserBy sortBy)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, leadID, children, _userTypeInfo, sortBy, SortUserOrder.Ascending) { }
		public Sponsor(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe, int leadID, UserCollection children, UserType _userTypeInfo, SortUserBy sortBy, SortUserOrder sortOrder) : base(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, leadID, children, _userTypeInfo, sortBy, sortOrder) {
			UserTypeInfo = UserType.SPONSOR;
		}


		public Sponsor(string fullName, 
						string emailAddress, string password, string comments, 
						CreationChannel creationChannel, Culture culture, Users.eSubsGlobalUser parent,
						Common.PhoneNumberCollection pnc, Common.PostalAddressCollection pac, int partnerID)
			: base(fullName, emailAddress, password, comments, creationChannel, culture, parent, pnc, pac, partnerID) {
			
			// Set UserTypeInfo to Sponsor
			UserTypeInfo = UserType.SPONSOR;

		}

		public Sponsor(eSubsGlobalUser user) : base(user) {

		}

		public static InsertMemberIntoDatabaseReturnValue Insert(Sponsor sponsor) {
			InsertMemberIntoDatabaseReturnValue insertMemberIntoDatabaseReturnValue =
				InsertMemberIntoDatabaseReturnValue.OK;

			if(sponsor.ID > -1) {
				insertMemberIntoDatabaseReturnValue = sponsor.UpdateInDatabase();
                insertMemberIntoDatabaseReturnValue = sponsor.UpdateUserInDatabase();
				sponsor.Save();
			} else {
				insertMemberIntoDatabaseReturnValue = sponsor.InsertIntoDatabase(true);
				sponsor.UserTypeInfo = UserType.SPONSOR;
				sponsor.Save();
			}
			return insertMemberIntoDatabaseReturnValue;
		}
	}
}
