/* Title:	eSubsGlobalUser (abstract class)
 * Author:	Jean-Francois Buist
 * Summary:	Base class for every eSubs users.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using GA.BDC.Core.Xml.Serialization;
using GA.BDC.Core.EnterpriseStandards;
using GA.BDC.Core.ESubsGlobal.DataAccess;
using GA.BDC.Core.Data.Sql;
using System.Collections.Generic;
using GA.BDC.Core.ESubsGlobal.Common;

namespace GA.BDC.Core.ESubsGlobal.Users {

	#region Enums
	public enum InsertMemberIntoDatabaseReturnValue : int 
	{
		OK = 0,
		MEMBER_NAME_AND_EMAIL_ADDRESS_ALREADY_EXISTS = 1,
		EXTERNAL_MEMBER_ID_ALREADY_EXISTS = 2,
		EMAIL_ADDRESS_PARTNER_ID_ALREADY_EXISTS = 3,
		EXIST_MEMBER_HIERACHY_ID_WITH_MEMBER_ID_AND_PARENT_MEMBER_HIERARCHY_ID,
		UNKNOWN_ERROR = -1,
		INSERT_INTO_MEMBER_FAILED = -3,
		FETCH_NEW_MEMBER_IDENTITY_FAILED = -4,
		INSERT_INTO_MEMBER_HIERARCHY_FAILED = -5,
		FETCH_NEW_MEMBER_HIERARCHY_IDENTITY_FAILED = -6,
		//
		FAIL_UPDATE_MEMBER = -7,
		FAIL_UPDATE_PASSWORD = -8
	}

    public enum UserIntoDatabaseReturnValue : int
    {
        OK = 0,
        ALREADY_EXISTS = -3,
        UNKNOWN_ERROR = -1
    }

	public enum UpdateMemberHierarchyUnsubscribeReturnValue : int 
	{
		OK = 0,
		UPDATE_INTO_MEMBER_HIERARCHY_FAILED = -1,
		UNSUBSCRIBE_MEMBER_FAILED = -2,
	}

	public enum SortUserBy {
		HierarchyID,
		FirstName,
		LastName,
		CompleteName,
		EmailAddress,
		CreationDate,
		CreationChannel,
		UserType,
		Bounced
	}

	public enum SortUserOrder {
		Ascending,
		Descending
	}

	public enum OptInStatus : int {
        OPTIN = 1,
		OPTOUT = 2
	}

	public enum UserType : int {
		UNKNOWN = 0, 
		SPONSOR = 1,
		PARTICIPANT = 2,
		SUPPORTER = 3
	}

	public enum NewParticipationEventOnInactiveCampaignStatus : int {
		OK,
		CLOSED,
		INTERNAL_ERROR
	}

	public struct NewParticipationEventOnInactiveCampaign {
		public int newEventParticipationID;
		public NewParticipationEventOnInactiveCampaignStatus status;
		
		public NewParticipationEventOnInactiveCampaign(int _newEventParticipationID, NewParticipationEventOnInactiveCampaignStatus _status) {
			newEventParticipationID = _newEventParticipationID;
			status = _status;
		}
	}
	#endregion

	/// <summary>
	/// This is the abstract class of all eSubs users.
	/// </summary>
    [Serializable]
   public abstract class eSubsGlobalUser : GA.BDC.Core.BusinessBase.BusinessBase, ICloneable, IComparer, IComparable
	{

		#region Constants
		private const string SESSION_KEY = "_ESUBS_USER_";
		#endregion

		#region Fields
		[NonSerialized]
		private SortUserBy sortBy = SortUserBy.FirstName;
		[NonSerialized]
		private SortUserOrder sortOrder = SortUserOrder.Ascending;

        protected int _userID = int.MinValue;
		protected int _id = int.MinValue;
		protected int _hierarchyID = int.MinValue;
		protected int _hierarchyParentID = int.MinValue;
		[NonSerialized]
		protected Culture _culture = null;
        protected int _intOptInStatus = int.MinValue;
		protected OptInStatus _optInStatusID = OptInStatus.OPTIN;
		protected string _firstName = null;
		protected string _middleName = null;
        protected string _greeting = null;
		protected string _externalMemberID = null;

		// Default to GA.BDC.Core.com
		protected int _partnerID = 0;

		protected string _gender = null;
		
		protected string _parentFirstName = null;
		protected string _parentLastName = null;

		protected string _lastName = null;
		protected EmailAddress _emailAddress = null;
		protected string _password = null;
		protected bool _bounced = false;
		protected string _comments = null;
		protected Common.PhoneNumberCollection _phoneNumbers = 	new Common.PhoneNumberCollection();
		protected Common.PostalAddressCollection _postalAddresses =	new Common.PostalAddressCollection();
		protected DateTime _createdDate = DateTime.MinValue;

		protected CreationChannel _creationChannel = null;
        protected int _creationChannelId = int.MinValue;

		protected UserType _userTypeInfo = UserType.UNKNOWN;
		protected bool _unsubscribe = false;
		protected bool _unsubscribeAll = false;

		protected int leadID = int.MinValue;
		protected int facebookID = int.MinValue;

		private UserCollection children;
		private string salutation = null;
		private bool _isMemberIDRetrieved = false;

		private eSubsGlobalUser _parent = null;

        private int _coppaMonth = int.MinValue;
        private int _coppaYear = int.MinValue;
        private bool _agreeToTermServices = false;

        private bool emailRequired = true;

		#endregion

		#region Constructors
//		public eSubsGlobalUser()
//		{
//			_culture = Culture.EN_US;
//		}

		public eSubsGlobalUser() : this(int.MinValue) { }
		public eSubsGlobalUser(int _id) : this(_id, int.MinValue) { }
		public eSubsGlobalUser(int _id, int _hierarchyID) : this(_id, _hierarchyID, int.MinValue) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID) : this(_id, _hierarchyID, _hierarchyParentID, ( eSubsGlobalEnvironment.Create() == null ? Culture.DEFAULT : eSubsGlobalEnvironment.Create().CurrentCulture ) )  { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture) : this(_id, _hierarchyID, _hierarchyParentID, _culture, OptInStatus.OPTIN) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, null)  { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, int.MinValue)  { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, null)  { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, null)  { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, false) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced) : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, DateTime.Now) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, false) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, int.MinValue) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe, int leadID)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, leadID, null) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe, int leadID, UserCollection children)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, leadID, children, UserType.UNKNOWN) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe, int leadID, UserCollection children, UserType _userTypeInfo)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, leadID, children, _userTypeInfo, SortUserBy.EmailAddress) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe, int leadID, UserCollection children, UserType _userTypeInfo, SortUserBy sortBy)  : this(_id, _hierarchyID, _hierarchyParentID, _culture, _optInStatusID, _firstName, _middleName, _externalMemberID, _partnerID, _gender, _parentFirstName, _parentLastName, _lastName, _emailAddress, _password, _bounced, _comments, _phoneNumbers, _postalAddresses, _createdDate, _creationChannel, _unsubscribe, leadID, children, _userTypeInfo, sortBy, SortUserOrder.Ascending) { }
		public eSubsGlobalUser(int _id, int _hierarchyID, int _hierarchyParentID, Culture _culture, OptInStatus _optInStatusID, string _firstName, string _middleName, string _externalMemberID, int _partnerID, string _gender, string _parentFirstName, string _parentLastName, string _lastName, EmailAddress _emailAddress, string _password, bool _bounced, string _comments, Common.PhoneNumberCollection _phoneNumbers, Common.PostalAddressCollection _postalAddresses, DateTime _createdDate, CreationChannel _creationChannel, bool _unsubscribe, int leadID, UserCollection children, UserType _userTypeInfo, SortUserBy sortBy, SortUserOrder sortOrder) {
			this.sortBy = sortBy;
			this.sortOrder = sortOrder;

			this._id = _id;
			this._hierarchyID = _hierarchyID;
			this._hierarchyParentID = _hierarchyParentID;
			this._culture = _culture;
			this._optInStatusID = _optInStatusID;
			this._firstName = _firstName;
			this._middleName = _middleName;
			this._externalMemberID = _externalMemberID;

			this._partnerID = _partnerID;

			this._gender = _gender;
			
			this._parentFirstName = _parentFirstName;
			this._parentLastName = _parentLastName;

			this._lastName = _lastName;
			this._emailAddress = _emailAddress;
			this._password = _password;
			this._bounced = _bounced;
			this._comments = _comments;
			this._phoneNumbers = _phoneNumbers;
			this._postalAddresses =	_postalAddresses;
			this._createdDate = _createdDate;
			this._creationChannel = _creationChannel;
			this._userTypeInfo = _userTypeInfo;
			this._unsubscribe = _unsubscribe;
			this.leadID = leadID;
		}

        public eSubsGlobalUser(string fullName,
            string emailAddress, string password, string comments,
            CreationChannel creationChannel, Culture culture,
            Users.eSubsGlobalUser parent, Common.PhoneNumberCollection pnc,
            Common.PostalAddressCollection pac, int partnerID) : this(fullName, emailAddress, password, comments,
            creationChannel, culture, parent, pnc, pac, partnerID, true)
        {

        }

		public eSubsGlobalUser(string fullName, 
			string emailAddress, string password, string comments, 
			CreationChannel creationChannel, Culture culture, 
			Users.eSubsGlobalUser parent, Common.PhoneNumberCollection pnc,
			Common.PostalAddressCollection pac, int partnerID, bool emailRequired) 
		{
			ConvertName(fullName);
			_middleName = null;

            this.emailRequired = emailRequired;

            if (emailRequired)
            {
                _emailAddress = EnterpriseStandards.EmailAddress.CreateEmailAddress(emailAddress);
            }
            else if (emailAddress != string.Empty)
            {
                _emailAddress = EnterpriseStandards.EmailAddress.CreateEmailAddress(emailAddress);
            }

			_password = password;
			_comments = comments;
			_creationChannel = creationChannel;
			_culture = culture;
			_partnerID = partnerID;

			// set default
			if(parent != null) 
			{
				_hierarchyParentID = parent.HierarchyID;
			} 
			else 
			{
				_hierarchyParentID = int.MinValue;
			}

			_optInStatusID = OptInStatus.OPTIN;
			_bounced = false;
			_userTypeInfo = UserType.UNKNOWN;

			if(pnc != null) 
			{
				_phoneNumbers = pnc;
			}

			if(pac != null) 
			{
				_postalAddresses = pac;
			}
		}

		public eSubsGlobalUser(string fullName, 
			string emailAddress, string password, string comments, 
			CreationChannel creationChannel, Culture culture, 
			Users.eSubsGlobalUser parent, Common.PhoneNumberCollection pnc,
			Common.PostalAddressCollection pac, int partnerID, string externalMemberId) 
		{
			ConvertName(fullName);
			_middleName = null;
			_emailAddress = EnterpriseStandards.EmailAddress.CreateEmailAddress(emailAddress);
			_password = password;
			_comments = comments;
			_creationChannel = creationChannel;
			_culture = culture;
			_partnerID = partnerID;
			_externalMemberID = externalMemberId;

			// set default
			if(parent != null) 
			{
				_hierarchyParentID = parent.HierarchyID;
			} 
			else 
			{
				_hierarchyParentID = int.MinValue;
			}

			_optInStatusID = OptInStatus.OPTIN;
			_bounced = false;
			_userTypeInfo = UserType.UNKNOWN;

			if(pnc != null) 
			{
				_phoneNumbers = pnc;
			}

			if(pac != null) 
			{
				_postalAddresses = pac;
			}
		}

		public eSubsGlobalUser(eSubsGlobalUser user) {
			_id = user.ID;
			_hierarchyID = user.HierarchyID;
			_hierarchyParentID = user.HierarchyParentID;
			_bounced = user.Bounced;
			_comments = user.Comments;
			_createdDate = user.CreatedDate;
			_creationChannel = user.CreationChannel;
			_culture = user.Culture;
			_emailAddress = user.EmailAddress;
			_firstName = user.FirstName;
			_parentFirstName = user.ParentFirstName;
			_parentLastName = user.ParentLastName;
			_lastName = user.LastName;
			_middleName = user.MiddleName;
			_optInStatusID = user.OptInStatusID;
			_password = user.Password;
			_userTypeInfo = user.UserTypeInfo;
			_phoneNumbers = user.PhoneNumbers;
			_postalAddresses = user.PostalAddresses;
			_externalMemberID = user.ExternalMemberID;
			_partnerID = user.PartnerID;
			facebookID = user.FacebookID;
			this.leadID = user.leadID;
		}
		#endregion

		#region Methods

        public List<eSubsGlobalUser> GetDirectMailRecipients(int eventParticipationId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetDirectMailRecipients(eventParticipationId);
        }

		public void GetAllChild(int eventID) {
			GetAllChild(false, eventID);
		}

		public bool IsMemberSubscribe()
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			this.UnsubscribeAll = (dbo.GetMemberUnsubscribe(this.ID) == 0);
			return this.UnsubscribeAll;
		}

		public void GetAllChild(bool expandAllChildren, int eventID) {
			DataAccess.ESubsGlobalDatabase dbo =
				new DataAccess.ESubsGlobalDatabase();
			eSubsGlobalUser[] users = dbo.GetAllChildMembers(HierarchyID, eventID);

			if(children == null) {
				children = new UserCollection();
			} else {
				children.Clear();
			}

			if(users != null) {
				foreach(eSubsGlobalUser u in users) {
					children.Add(u);
				}

				if(expandAllChildren) {
					foreach(eSubsGlobalUser u in children) {
						u.GetAllChild(true, eventID);
					}
				}
			}

			children.Sort();
		}

		public void ConvertParentName(string fullName) {
			if(fullName == null) {
				_parentFirstName = null;
				_parentLastName = null;
				return;
			}

			if(fullName == "") {
				_parentFirstName = null;
				_parentLastName = null;
				return;
			}

			string[] name = fullName.Split(' ');
			string pFirstName = "";
			string pLastName = "";
			bool firstName = true;
			foreach(string splitedName in name) {
				if(splitedName.Trim() != "") {
					if(firstName) {
						pFirstName = splitedName;
						firstName = false;
					} else {
						pLastName += splitedName + " ";
					}
				}
			}
			_parentFirstName = pFirstName.Trim();
			_parentLastName = pLastName.Trim();
		}

		public void ConvertName(string fullName) {
			string[] name = fullName.Split(' ');
			string pFirstName = "";
			string pLastName = "";
			bool firstName = true;
			foreach(string splitedName in name) {
				if(splitedName.Trim() != "") {
					if(firstName) {
						pFirstName = splitedName;
						firstName = false;
					} else {
						pLastName += splitedName + " ";
					}
				}
			}
			FirstName = pFirstName.Trim();
			LastName = pLastName.Trim();
		}

		public UpdateMemberHierarchyUnsubscribeReturnValue UpdateUnsubscribeInMemberTable(bool unsubscribe)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.UpdateUnsubscribeInMemberTable(this.ID, unsubscribe);
		}

        public UpdateMemberHierarchyUnsubscribeReturnValue UpdateUserUnsubscribeInMemberTable(bool unsubscribe)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.UpdateUserUnsubscribeInMemberTable(this.ID, unsubscribe);
        }

		public UpdateMemberHierarchyUnsubscribeReturnValue UnsubscribeAllMemberhierarchies(bool unsubscribe, int[] hierarchy_ids)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.UnsubscribeAllMemberhierarchys(hierarchy_ids, unsubscribe);
		}

		public UpdateMemberHierarchyUnsubscribeReturnValue UnsubscribeMember(bool unsubscribe){
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.UnsubscribeMember(this.HierarchyID, unsubscribe );
		}

		public UpdateMemberHierarchyUnsubscribeReturnValue UnsubscribeMember(){
			return UnsubscribeMember(this._unsubscribe);
		}

		public InsertMemberIntoDatabaseReturnValue UpdateInDatabase() {
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.UpdateMember(this, this.PartnerID);
		}

        public InsertMemberIntoDatabaseReturnValue UpdateUserInDatabase()
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.UpdateUser(this, this.PartnerID);
        }

		public void UpdatePassword() {
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			dbo.UpdatePassword(_id, _password);
            dbo.UpdateUserPassword(null, _id, _password);
		}

		public static UnknownUser LoadByPreviousEsubsUserID(int campaignID, int supporterID, int participantID, int organizerID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUserFromPreviousESubsVersionUserID(campaignID, supporterID, participantID, organizerID);
		}

        public static UnknownUser[] GetContact(int hierarchyID, bool bounced)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetContact(hierarchyID, bounced);
        }

        public static List<UnknownUser> GetContactAll(int hierarchyID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetContactAll(hierarchyID);
        }

        /// <summary>
        /// This method is used only to retrieve a small subset of the eSubsGlobalUser object.
        /// For the full object data, use "GetContact(int hierarchyID, bool bounced)" instead.
        /// 
        /// Currently only contains the following data:
        ///     HierarchyID  
        ///     ID
        ///     ExternalMemberID
        ///     FirstName
        ///     MiddleName
        ///     LastName
        ///     EmailAddress
        ///     CreationChannel
        ///     UserTypeInfo
        ///     
        ///     If you need more data, add it within GetLightWeightContact data access method
        /// </summary>
        /// <param name="hierarchyID"></param>
        /// <param name="bounced"></param>
        /// <returns></returns>
        public static List<UnknownUser> GetLightWeightContact(int hierarchyID, bool bounced)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetLightWeightContact(hierarchyID, bounced);
        }

        /// <summary>
        /// This method is used only to retrieve a small subset of the eSubsGlobalUser object.
        /// For the full object data, use "GetContact(int hierarchyID, bool bounced)" instead.
        /// 
        /// Currently only contains the following data:
        ///     HierarchyID  
        ///     ID
        ///     ExternalMemberID
        ///     FirstName
        ///     MiddleName
        ///     LastName
        ///     EmailAddress
        ///     CreationChannel
        ///     UserTypeInfo
        ///     
        ///     If you need more data, add it within GetLightWeightContactAll data access method
        /// </summary>
        /// <param name="hierarchyID"></param>
        /// <param name="bounced"></param>
        /// <returns></returns>
        public static List<UnknownUser> GetLightWeightContactAll(int hierarchyID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetLightWeightContactAll(hierarchyID);
        }

        public static int GetNbSendSupporter(int member_hierarchy_id)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetNbSendSupporter(member_hierarchy_id);
        }

		public static UnknownUser[] GetChildMember(int hierarchyID, int eventID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetChildMembers(hierarchyID, eventID);
		}

        public static List<UnknownUser> GetChildMemberWithPostalAddress(int hierarchyID, int eventID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetChildMembersWithPostalAddress(hierarchyID, eventID);
        }

        public static int GetNbReminderContact(int hierarchyID, int eventID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetNbReminderContact(hierarchyID, eventID);
        }

		public static int LoadByLoginInfo(string username, string password, int partnerID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUserFromLogin(username, password, partnerID);
		}

		public static UnknownUser LoadByExternalMemberID(int partnerID, string extMemberID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUserByExternalMemberID(partnerID, extMemberID);
		}

		public static UnknownUser LoadByHierarchyID(int hierarchyID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUnknownUser(hierarchyID);
		}

		public static UnknownUser[] LoadByNameAndEmail(string firstName, string lastName, string emailAddress, int partnerID)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUserByNameAndEmail(firstName, lastName, emailAddress, partnerID);
		}

		public static UnknownUser[] LoadByEmail(string emailAddress, int partnerID)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUsersByEmail(emailAddress, partnerID);
		}

		public static UnknownUser[] LoadByEmailNoPartner(string emailAddress)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUsersByEmailNoPartner(emailAddress);
		}

		public static UnknownUser LoadByEmailAndPassword(string emailAddress, int partnerID, string password)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			UnknownUser user = dbo.LoadByEmailAndPassword(null, emailAddress, partnerID, password);
			return user;
		}

        public static UnknownUser LoadUserByEmailAndPassword(string emailAddress, int partnerID, string password)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            UnknownUser user = dbo.LoadUserByEmailAndPassword(null, emailAddress, partnerID, password);
            return user;
        }

		public static eSubsGlobalUser[] LoadByFacebookID(int facebookID, int campaignID) {
			ArrayList users = new ArrayList();
			EventParticipation[] eventParticipations =
				EventParticipation.GetEventParticipationsByFacebookID(facebookID);
			foreach(EventParticipation eventParticipation in eventParticipations) {
				if(eventParticipation.EventID == campaignID) {
					users.Add(eSubsGlobalUser.LoadByHierarchyID(eventParticipation.MemberHierarchyID));
				}
			}
			return (eSubsGlobalUser[])users.ToArray(typeof(eSubsGlobalUser));
		}

		public static string GetMostRecentUserFullName(string emailAddress, string password)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetMostRecentUserFullName(emailAddress, password);
		}
		
		public static UnknownUser[] LoadByGroup(Group grp)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetUserByGroup(grp.GroupID);
		}

		public UnknownUser[] GetSupportersFromLastCampaign(int eventID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetSupporterFromPreviousRelaunch(eventID, HierarchyID);
		}

		public virtual InsertMemberIntoDatabaseReturnValue IsExist()
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.CheckMember(this);
		}

		
		// If exist return member_id. Otherwise return int.MinValue.
		public static int IsUserRegistered(string emailAddress) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.IsUserRegistered(emailAddress);
		}

		/// <summary>
		/// Create new member.
		/// </summary>
		/// <param name="createNew">If true, create a new member hierarchy if member already exist. 
		/// If false, just return and set the member id and member hierarchy id.</param>
		/// <returns></returns>
        public virtual InsertMemberIntoDatabaseReturnValue InsertIntoDatabase(bool createNew, bool insertUser)
		{	
			// Always create a new member hierarchy?
			if (! createNew)
			{
				// Check if member exists. If it exists, IsExist will NOT return OK.
				InsertMemberIntoDatabaseReturnValue ret = this.IsExist();

				// Member exists
				if (ret != InsertMemberIntoDatabaseReturnValue.OK)
				{
					UnknownUser user = null;

					// Get user by External member id
					if (this.ExternalMemberID != null)
						user = Users.eSubsGlobalUser.LoadByExternalMemberID(this.PartnerID, this.ExternalMemberID);

					// If not found, try searching by name and email.
					if (user == null)
					{
						UnknownUser[] users = Users.eSubsGlobalUser.LoadByNameAndEmail(this.FirstName, this.LastName, this.EmailAddress.Email, this.PartnerID);
						if (users != null)
						{
                            user = users.ToList<UnknownUser>().FirstOrDefault(x => x.HierarchyParentID == this.HierarchyParentID);

                            /*
							// Because LoadByNameAndEmail could return multiple users, 
							// we need to look for the closest matching user by type.
							for (int i = 0; i < users.Length; i++)
							{
								if (this.IsSponsor && users[i].IsSponsor)
								{
									user = users[i];
									break;
								}
								else if (this.IsParticipant && users[i].IsParticipant)
								{
									user = users[i];
									break;
								}
								else if (this.IsSupporter && users[i].IsSponsor)
								{
									user = users[i];
									break;
								}
								else 
								{
									user = users[i];
									break;
								}
							}	
							// Still empty, just set to the first user
							if (user == null && users.Length > 0)
								user = users[0]; */
						}
					}

					// If user is found, copy id and member hierarchy id.
					if (user != null)
					{
						// Copy id
						this.HierarchyID = user.HierarchyID;
						this.ID = user.ID;
                        this.UserID = user.UserID;
					}

					return ret;
				}
			}
			
			// Insert member
			DataAccess.ESubsGlobalDatabase dbo =
				new DataAccess.ESubsGlobalDatabase();
			
			return dbo.InsertMember(_culture.CultureCode, (int) _optInStatusID, _firstName, _middleName, _lastName,_greeting,
				"", _emailAddress, _parentFirstName, _parentLastName, _externalMemberID, _partnerID, _password, _bounced, _comments, _hierarchyParentID,
				_creationChannel.ID, leadID, facebookID, _coppaMonth, _coppaYear, _agreeToTermServices, insertUser, ref _hierarchyID, ref _id, ref _userID, this.emailRequired);
		}

        public virtual InsertMemberIntoDatabaseReturnValue InsertIntoDatabase(bool insertUser)
        {
			// Create a new member hierarchy even if member exists already.
            return InsertIntoDatabase(true, insertUser);
		}

		

		public virtual void InsertPhoneNumberInDatabase(Common.PhoneNumber phoneNumber) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			dbo.InsertMemberPhone(_id, (int)phoneNumber.PhoneNumberTypeID, phoneNumber.FormattedPhoneNumber);
		}

		public virtual void InsertPostalAddressInDatabase(Common.PostalAddress postalAddress) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			int pID = int.MinValue;
			int fID = int.MinValue;
			dbo.CreatePostalCode(_id, postalAddress.Address1, postalAddress.Address2,
				postalAddress.City, postalAddress.ZipCode, (string)postalAddress.CountryCode, 
				postalAddress.SubDivisionCode, (int)postalAddress.PostalAddressTypeID,
				ref pID, ref fID);
			postalAddress.Id = pID;			
		}

		internal void SetSalutation(string salutation) {
			this.salutation = salutation;
		}

		public string GetSalutation() {
            if (!string.IsNullOrEmpty(salutation))
                return salutation;
            else
                return CompleteName;
		}

		// retreive the user from the current project datasource
		public static eSubsGlobalUser Create() {
			if(System.Web.HttpContext.Current != null) {
				if(System.Web.HttpContext.Current.Session != null) { // web base
					if(System.Web.HttpContext.Current.Session[SESSION_KEY] == null) {
						return new UnknownUser();
					} else {
						return (eSubsGlobalUser)System.Web.HttpContext.Current.Session[SESSION_KEY];
					}
				}
			}
			return null;
		}

		// save the object into the current project datasource
		public void Save() {
			if(System.Web.HttpContext.Current.Session != null) { // web base
				System.Web.HttpContext.Current.Session[SESSION_KEY] = this;
			}
		}

		// clear the session of this value
		public void Clear() {
			if(System.Web.HttpContext.Current != null) {
				if(System.Web.HttpContext.Current.Session != null) { // web base
					System.Web.HttpContext.Current.Session.Remove(SESSION_KEY);
				}
			}
		}
		#endregion

		#region ICloneable Members

		/// <summary>
		/// Creates a clone of the object.
		/// </summary>
		/// <returns>A new object containing the exact data of the original object.</returns>
		public object Clone()
		{
			MemoryStream buffer = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();

			formatter.Serialize(buffer, this);
			buffer.Position = 0;
			return formatter.Deserialize(buffer);
		}
		#endregion

		// this method should be called when a user tries to register to an inactive campaign.
		public static NewParticipationEventOnInactiveCampaign GetNewParticipantEvent(EventParticipation ep, eSubsGlobalUser user) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			int newEventParticipationID = dbo.GetNewEventParticipationID(ep.EventParticipationID, user.HierarchyID);

			NewParticipationEventOnInactiveCampaign newEventParticipationUserOnInactiveCampaign = 
				new NewParticipationEventOnInactiveCampaign(newEventParticipationID, NewParticipationEventOnInactiveCampaignStatus.OK);
			if(newEventParticipationID > 0) {
				newEventParticipationUserOnInactiveCampaign.status = NewParticipationEventOnInactiveCampaignStatus.OK;
			} else if(newEventParticipationID == 0) {
				newEventParticipationUserOnInactiveCampaign.status = NewParticipationEventOnInactiveCampaignStatus.CLOSED;
			} else {
				newEventParticipationUserOnInactiveCampaign.status = NewParticipationEventOnInactiveCampaignStatus.INTERNAL_ERROR;
			}
			return newEventParticipationUserOnInactiveCampaign;
		}

		#region Properties
		public bool IsSponsor 
		{
			get { return (_userTypeInfo == UserType.SPONSOR); }
		}

		public bool IsParticipant 
		{
			get { return (_userTypeInfo == UserType.PARTICIPANT); }
		}

		public bool IsSupporter 
		{
			get { return (_userTypeInfo == UserType.SUPPORTER); }
		}

		public bool IsUnknown {
			get { return (_userTypeInfo == UserType.UNKNOWN); }
		}

		public bool IsUnknownSupporter {
			get {
				if(IsSupporter) {
					if(EmailAddress != null) {
						if(EmailAddress.Email != null) {
							if(EmailAddress.Email.StartsWith("es") &&
								(EmailAddress.Email.EndsWith("@GA.BDC.Core.com") ||
                                 EmailAddress.Email.EndsWith("@GA.BDC.Core.ca"))) {
								return true;
							}
						}
					}
				}
				return false;
			}
		}

		public virtual string CompleteName 
		{
			get { return _firstName + " " + (_middleName != null? _middleName + " ": "") + _lastName; }
		}

		public Common.PhoneNumberCollection PhoneNumbers 
		{
			set { _phoneNumbers = value; }
			get { return _phoneNumbers; }
		}

		public string Gender
		{
			set {_gender = value;}
			get {return _gender;}
		}

		public Common.PostalAddressCollection PostalAddresses {
			set { _postalAddresses = value; }
			get 
            {
                if (_postalAddresses == null)
                {
                    this.PostalAddresses = PostalAddressCollection.GetPostalAddressCollectionForMember(this);
                }
                return _postalAddresses; 
            }
		}

		public bool HasCreationChannel {
			get { return (_creationChannel != null); }
		}

        public int UserID
        {
            set { _userID = value; }
            get { return _userID; }
        }

		public int ID {
			set { _id = value; }
			get { return _id; }
		}

        public bool Deleted { get; set; }

		public string ExternalMemberID 
		{
			set { _externalMemberID = value; }
			get { return _externalMemberID; }
		}

		public int PartnerID 
		{
			set { _partnerID = value; }
			get { return _partnerID; }
		}

		public int HierarchyParentID 
		{
			set { _hierarchyParentID = value; }
			get { return _hierarchyParentID; }
		}

		private DateTime _MemberHierarchyCreatedDate = DateTime.Now;

		public DateTime  MemberHierarchyCreatedDate {
			set { _MemberHierarchyCreatedDate = value; }
			get { return _MemberHierarchyCreatedDate; }
		}

		public int HierarchyID
		{
			set { _hierarchyID = value; }
			get { return _hierarchyID; }
		}

		public Culture Culture {
			set { _culture = value; }
			get { return _culture; }
		}

		public OptInStatus OptInStatusID {
			set { _optInStatusID = value; }
			get {
                if (IntOptInStatus != int.MinValue)
                {
                    switch (IntOptInStatus)
                    {
                        case (int)Users.OptInStatus.OPTIN:
                            OptInStatusID = Users.OptInStatus.OPTIN;
                            break;
                        case (int)Users.OptInStatus.OPTOUT:
                            OptInStatusID = Users.OptInStatus.OPTOUT;
                            break;
                        default:
                            OptInStatusID = Users.OptInStatus.OPTIN;
                            break;
                    }
                }
                return _optInStatusID; }
		}

        public int IntOptInStatus
        {
            set { _intOptInStatus = value; }
            get { return _intOptInStatus; }
        }

		public string FirstName {
			set { _firstName = value; }
			get { return _firstName; }
		}

		public string MiddleName {
			set { _middleName = value; }
			get { return _middleName; }
		}

		public string LastName {
			set { _lastName = value; }
			get { return _lastName; }
		}

        public string Greeting
        {
            set { this._greeting = value; }
            get { return _greeting; }
        }


		public string ParentFirstName {
			set { _parentFirstName = value; }
			get { return _parentFirstName; }
		}

		public string ParentLastName {
			set { _parentLastName = value; }
			get { return _parentLastName; }
		}

		public GA.BDC.Core.EnterpriseStandards.EmailAddress EmailAddress {
			set { _emailAddress = value; }
			get { return _emailAddress; }
		}

		public string Password {
			set { _password = value; }
			get { return _password; }
		}

		public bool Bounced {
			set { _bounced = value; }
			get { return _bounced; }
		}

		public string Comments {
			set { _comments = value; }
			get { return _comments; }
		}

		public DateTime CreatedDate {
			set { _createdDate = value; }
			get { return _createdDate; }
		}

		public CreationChannel CreationChannel {
			set { _creationChannel = value; }
			get {
                if (_creationChannel != null)
                    return _creationChannel;
                else
                {
                    if (CreationChannelID != int.MinValue)
                        _creationChannel = Users.CreationChannel.CreateFromID(CreationChannelID);
                    return _creationChannel;
                }
            }
		}

        public int CreationChannelID
        {
            set { _creationChannelId = value; }
            get { return _creationChannelId; }
        }

		public UserType UserTypeInfo {
			set { _userTypeInfo = value; }
			get { return _userTypeInfo; }
		}

		public int LeadID
		{
			get {return this.leadID;}
			set {this.leadID = value;}
		}

		public int FacebookID {
			get { return this.facebookID; }
			set { facebookID = value; }
		}

		/// <summary>
		/// Get the email addres (Read Only, use EmailAddress to set the email)
		/// </summary>
		/// 

		

		public string Email {
            get { return this.EmailAddress != null ? this.EmailAddress.Email : string.Empty; }
		}

		public UserCollection Children {
			get { return children; }
		}

		public SortUserBy SortBy {
			set { sortBy = value; }
			get { return sortBy; }
		}

		public SortUserOrder SortOrder {
			set { sortOrder = value; }
			get { return sortOrder; }
		}
		public bool Unsubscribe 
		{
			set { _unsubscribe = value; }
			get { return _unsubscribe; }
		}
		public bool UnsubscribeAll
		{
			set {_unsubscribeAll = value;}
			get {return _unsubscribeAll;}
		}

		public eSubsGlobalUser Parent
		{
			set {_parent = value;}
			get {return _parent;}
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

		public bool IsMemberIDRetrieved
		{
			get
			{
				return _isMemberIDRetrieved;
			}

			set
			{
				_isMemberIDRetrieved = value;
			}
		}

        public bool IsEmailRequired
        {
            get { return this.emailRequired; }
        }

        public string Name
        {
            get
            {
                string name = string.Empty;

                if (this.FirstName != null && this.FirstName != string.Empty)
                {
                    name = this.FirstName;
                }

                if (this.LastName != null && this.LastName != string.Empty)
                {
                    name = (name == string.Empty ? this.LastName : this.FirstName + " " + this.LastName); 
                }

                return name;
            }
        }

        public string Salutation
        {
            get
            {
                if (!string.IsNullOrEmpty(salutation))
                    return salutation;
                else
                    return CompleteName;
            }
            set
            {
                this.salutation = value;
            }
        }

		#endregion

		#region IComparer Members

		public int Compare(object x, object y) {
			eSubsGlobalUser user1;
			eSubsGlobalUser user2;

			if(x is eSubsGlobalUser) {
				user1 = (eSubsGlobalUser)x;
			} else {
				throw new ArgumentException("Object must be an eSubsGlobalUser");
			}

			if(y is eSubsGlobalUser) {
				user2 = (eSubsGlobalUser)y;
			} else {
				throw new ArgumentException("Object must be an eSubsGlobalUser");
			}

			return user1.FirstName.CompareTo(user2.FirstName);
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj) {
			eSubsGlobalUser user1;
			eSubsGlobalUser user2;

			user1 = (eSubsGlobalUser)this;

			if(obj is eSubsGlobalUser) {
				user2 = (eSubsGlobalUser)obj;
			} else {
				throw new ArgumentException("Object must be an eSubsGlobalUser");
			}

			if(sortOrder == SortUserOrder.Descending) {
				eSubsGlobalUser tmp = user2;
				user2 = user1;
				user1 = tmp;
			}

			switch(sortBy) {
				case SortUserBy.HierarchyID:
					return user1.HierarchyID.CompareTo(user2.HierarchyID);
				case SortUserBy.FirstName:
					return user1.FirstName.CompareTo(user2.FirstName);
				case SortUserBy.LastName:
					return user1.LastName.CompareTo(user2.LastName);
				case SortUserBy.EmailAddress:
					return user1.EmailAddress.Email.CompareTo(user2.EmailAddress.Email);
				case SortUserBy.CreationDate:
					return user1.CreatedDate.CompareTo(user2.CreatedDate);
				case SortUserBy.CompleteName:
					return user1.CompleteName.CompareTo(user2.CompleteName);
				case SortUserBy.UserType:
					return user1.UserTypeInfo.CompareTo(user2.UserTypeInfo);
				case SortUserBy.CreationChannel:
					return user1.CreationChannel.ID.CompareTo(user2.CreationChannel.ID);
				case SortUserBy.Bounced:
					return user1.Bounced.CompareTo(user2.Bounced);
			}
			return 0;
		}

		#endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
