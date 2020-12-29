using System;

using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Payment {
	/// <summary>
	/// Summary description for PaymentInfo.
	/// </summary>
	/// 
	public enum PaymentInfoStatus
	{
		Error,
		Ok
	}

	public class PaymentInfo : EnvironmentBase {
		private int _paymentInfoID;
		private int _groupID;
		private int _eventID;
    	private Common.PhoneNumber _phoneNumber;
		private Common.PostalAddress _postalAddress;
		private string _paymentName;
		private string _onBehalfOfName;
		private string _shipToName;
		private string _ssn;
		private bool _active;
		private DateTime _createDate;
		private int isValidated = 0;

		private int postal_address_id;
		private int phone_number_id;


		public PaymentInfo() : this(int.MinValue) {}
		public PaymentInfo(int _paymentInfoID) : this(_paymentInfoID, int.MinValue) {}
		public PaymentInfo(int _paymentInfoID, int _groupID) : this(_paymentInfoID, _groupID, int.MinValue) {}
		public PaymentInfo(int _paymentInfoID, int _groupID, int _eventID) : this(_paymentInfoID, _groupID, _eventID, null) {}
		public PaymentInfo(int _paymentInfoID, int _groupID, int _eventID, Common.PhoneNumber _phoneNumber) : this(_paymentInfoID, _groupID, _eventID, _phoneNumber, null) {}
		public PaymentInfo(int _paymentInfoID, int _groupID, int _eventID, Common.PhoneNumber _phoneNumber, Common.PostalAddress _postalAddress) : this(_paymentInfoID, _groupID, _eventID, _phoneNumber, _postalAddress, null) {}
		public PaymentInfo(int _paymentInfoID, int _groupID, int _eventID, Common.PhoneNumber _phoneNumber, Common.PostalAddress _postalAddress, string _paymentName) : this(_paymentInfoID, _groupID, _eventID, _phoneNumber, _postalAddress, _paymentName, null) {}
		public PaymentInfo(int _paymentInfoID, int _groupID, int _eventID, Common.PhoneNumber _phoneNumber, Common.PostalAddress _postalAddress, string _paymentName, string _onBehalfOfName) : this(_paymentInfoID, _groupID, _eventID, _phoneNumber, _postalAddress, _paymentName, _onBehalfOfName, null) {}
		public PaymentInfo(int _paymentInfoID, int _groupID, int _eventID, Common.PhoneNumber _phoneNumber, Common.PostalAddress _postalAddress, string _paymentName, string _onBehalfOfName, string _shipToName) : this(_paymentInfoID, _groupID, _eventID, _phoneNumber, _postalAddress, _paymentName, _onBehalfOfName, _shipToName, null) {}
		public PaymentInfo(int _paymentInfoID, int _groupID, int _eventID, Common.PhoneNumber _phoneNumber, Common.PostalAddress _postalAddress, string _paymentName, string _onBehalfOfName, string _shipToName, string _ssn) : this(_paymentInfoID, _groupID, _eventID, _phoneNumber, _postalAddress, _paymentName, _onBehalfOfName, _shipToName, _ssn, true) {}
		public PaymentInfo(int _paymentInfoID, int _groupID, int _eventID, Common.PhoneNumber _phoneNumber, Common.PostalAddress _postalAddress, string _paymentName, string _onBehalfOfName, string _shipToName, string _ssn, bool _active) : this(_paymentInfoID, _groupID, _eventID, _phoneNumber, _postalAddress, _paymentName, _onBehalfOfName, _shipToName, _ssn, _active, DateTime.Now) {}
		public PaymentInfo(int _paymentInfoID, int _groupID, int _eventID, Common.PhoneNumber _phoneNumber, Common.PostalAddress _postalAddress, string _paymentName, string _onBehalfOfName, string _shipToName, string _ssn, bool _active, DateTime _createDate) {
			this._paymentInfoID = _paymentInfoID;
			this._groupID = _groupID;
			this._eventID = _eventID;
			this._phoneNumber = _phoneNumber;
			this._postalAddress = _postalAddress;
			this._paymentName = _paymentName;
			this._onBehalfOfName = _onBehalfOfName;
			this._shipToName = _shipToName;
			this._ssn = _ssn;
			this._active = _active;
		}

		public PaymentInfo(string name, Common.PostalAddress postalAddress, Common.PhoneNumber phoneNumber) {
			_groupID = int.MinValue;
			_phoneNumber = phoneNumber;
			_postalAddress = postalAddress;
			_paymentName = name;
			_onBehalfOfName = null;
			_shipToName = null;
			_ssn = null;
			_active = true;
			_createDate = DateTime.Now;
		}

		public PaymentInfo(eSubsGlobalEnvironment env, string name, Common.PostalAddress postalAddress, Common.PhoneNumber phoneNumber) {
			if(env.Group == null) {
				throw new ESubsGlobalException("Unable to create PaymentInfo/Sponsor without Group", null, env, null);
			}
			_groupID = env.Group.GroupID;
			_phoneNumber = phoneNumber;
			_postalAddress = postalAddress;
			_paymentName = name;
			_onBehalfOfName = null;
			_shipToName = null;
			_ssn = null;
			_active = true;
			_createDate = DateTime.Now;
		}

		public PaymentInfo(int groupID, Common.PostalAddress postalAddress, string paymentName, 
			string onBehalfOfName, string shipToName, string ssn, bool active, DateTime createDate) {

			_groupID = groupID;
			_postalAddress = postalAddress;
			_paymentName = paymentName;
			_onBehalfOfName = onBehalfOfName;
			_shipToName = shipToName;
			_ssn = ssn;
			_active = active;
			_createDate = createDate;

		}

		public static PaymentInfo LoadPaymentInfo(int paymentInfoID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentInfo(paymentInfoID);
		}

		public static PaymentInfo LoadPaymentInfoBySponsorID(int sponsorID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentInfoBySponsorID(sponsorID);
		}

		public static PaymentInfo GetPaymentInfoByID(int ID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentInfoByID(ID);
		}


		public static PaymentInfo GetPaymentInfoByIDActiveOrNot(int ID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentInfoByIDActiveOrNot(ID);
		}

		public static bool Match(int paymentId)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			string result = dbo.GetQSPMatchingCodeByPaymentId(paymentId);
			return (result != null && result != string.Empty);
		}

		// obsolete method, payment info no longer link to group
		// now it links by event 
		public static PaymentInfo TBD_LoadPaymentInfoByGroupID(int groupID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.TBD_GetPaymentInfoByGroupID(groupID);
		}

		public static PaymentInfo LoadPaymentInfoByEventID(int groupID, int eventID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentInfoByEventID(groupID, eventID);
		}

		public static GA.BDC.Core.ESubsGlobal.Common.PostalAddressCollection GetPaymentAddress(DateTime lowerDate, DateTime upperDate)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentAddress(lowerDate, upperDate);
		}

		public void UpdateIntoDatabase() {
			try {
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				int phoneNumID = int.MinValue;
				int postalAddID = int.MinValue;
				string formattedPhoneNumber = null;
				string address1 = null;
				string address2 = null;
				string city = null;
				string subDivisionCode = null;
				string zipCode = null;
				string countryCode = null;
				
				if (_phoneNumber != null)
					formattedPhoneNumber = _phoneNumber.FormattedPhoneNumber;

				if (_postalAddress != null)
				{
					address1 = _postalAddress.Address1;
					address2 = _postalAddress.Address2;
					city = _postalAddress.City;
					subDivisionCode = _postalAddress.SubDivisionCode;
					zipCode = _postalAddress.ZipCode;
					countryCode = (string) _postalAddress.CountryCode;
				}

				dbo.UpdatePayment(_paymentInfoID, _paymentName, _onBehalfOfName,
					_shipToName, formattedPhoneNumber, _ssn, address1, address2,
					city, zipCode, countryCode, 
					subDivisionCode, isValidated, ref postalAddID, ref phoneNumID, ref _paymentInfoID);

                if (_phoneNumber != null)
				    _phoneNumber.ID = phoneNumID;
                if (_postalAddress != null)
				    _postalAddress.Id = postalAddID;
			} catch(System.Exception ex) {
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}



		public void UpdateIntoDatabase(GA.BDC.Core.Data.Sql.SqlInterface si) 
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				int phoneNumID = int.MinValue;
				int postalAddID = int.MinValue;
				string formattedPhoneNumber = null;
				string address1 = null;
				string address2 = null;
				string city = null;
				string subDivisionCode = null;
				string zipCode = null;
				string countryCode = null;
				
				if (_phoneNumber != null)
					formattedPhoneNumber = _phoneNumber.FormattedPhoneNumber;

				if (_postalAddress != null)
				{
					address1 = _postalAddress.Address1;
					address2 = _postalAddress.Address2;
					city = _postalAddress.City;
					subDivisionCode = _postalAddress.SubDivisionCode;
					zipCode = _postalAddress.ZipCode;
					countryCode = (string) _postalAddress.CountryCode;
				}

				dbo.UpdatePayment(si, _paymentInfoID, _paymentName, _onBehalfOfName,
					_shipToName, formattedPhoneNumber, _ssn, address1, address2,
					city, zipCode, countryCode, subDivisionCode, isValidated, 
					ref postalAddID, ref phoneNumID, ref _paymentInfoID);

                if (_phoneNumber != null)
				    _phoneNumber.ID = phoneNumID;
                if (_postalAddress != null)
				    _postalAddress.Id = postalAddID;
			} 
			catch(System.Exception ex) 
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}


		public void InsertIntoDatabase(int eventID) 
		{
			try 
			{
				_eventID = eventID;

				int postalAddID = int.MinValue;
				int phoneNumID = int.MinValue;
				string formattedPhoneNumber = null;
				string address1 = null;
				string address2 = null;
				string city = null;
				string subDivisionCode = null;
				string zipCode = null;
				string countryCode = null;
				
				if (_phoneNumber != null)
					formattedPhoneNumber = _phoneNumber.FormattedPhoneNumber;

				if (_postalAddress != null)
				{
					address1 = _postalAddress.Address1;
					address2 = _postalAddress.Address2;
					city = _postalAddress.City;
					subDivisionCode = _postalAddress.SubDivisionCode;
					zipCode = _postalAddress.ZipCode;
					countryCode = (string) _postalAddress.CountryCode;
				}

				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				dbo.InsertPayment(_groupID, eventID, _paymentName, _onBehalfOfName, _shipToName,
					formattedPhoneNumber, _ssn, address1, address2,
					city, zipCode, countryCode, 
					subDivisionCode, isValidated, ref postalAddID, ref phoneNumID,
					ref _paymentInfoID);

                if (_postalAddress != null)
				    _postalAddress.Id = postalAddID;
                if (_phoneNumber != null)
				    _phoneNumber.ID = phoneNumID;
			} 
			catch(System.Exception ex) 
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}
		
		public int Update() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePaymentInfo(this);
		}

		public int Insert() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPaymentInfo(this);
		}

		#region Properties
		public int PaymentInfoID 
		{
			set { _paymentInfoID = value; }
			get { return _paymentInfoID; }
		}

		public int GroupID {
			set { _groupID = value; }
			get { return _groupID; }
		}

		public int EventID {
			set { _eventID = value; }
			get { return _eventID; }
		}

		public Common.PostalAddress PostalAddress {
			set { _postalAddress = value; }
			get { return _postalAddress; }
		}

		public Common.PhoneNumber PhoneNumber {
			set { _phoneNumber = value; }
			get { return _phoneNumber; }
		}

		public string PaymentName {
			set { _paymentName = value; }
			get { return _paymentName; }
		}

		public string OnBehalfOfName {
			set { _onBehalfOfName = value; }
			get { return _onBehalfOfName; }
		}

		public string ShipToName {
			set { _shipToName = value; }
			get { return _shipToName; }
		}

		public string Ssn {
			set { _ssn = value; }
			get { return _ssn; }
		}

		public bool Active {
			set { _active = value; }
			get { return _active; }
		}

		public int IsValidated {
			set { isValidated = value; }
			get { return isValidated; }
		}

		public DateTime CreateDate {
			set { _createDate = value; }
			get { return _createDate; }
		}

		public int PostalAddressID 
		{
			set { postal_address_id = value; }
			get { return postal_address_id; }
		}
	
		public int PhoneNumberID 
		{
			set { phone_number_id = value; }
			get { return phone_number_id; }
		}

		#endregion
	}
}
