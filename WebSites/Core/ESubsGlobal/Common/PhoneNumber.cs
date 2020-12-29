/* Title:	PhoneNumbers
 * Author:	Jean-Francois Buist
 * Summary:	Phone number of member.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal.Common {
	// eSubs forms accept many type of phone numbers
	public enum PhoneNumberType : int {
		DAY_PHONE = 1,
		EVENING_PHONE = 2,
		FAX = 3,
		MOBILE = 4
	}

	/// <summary>
	/// 
	/// </summary>
    [Serializable]
	public class PhoneNumber {
        private int _id = int.MinValue;	// database id
		private PhoneNumberType _phoneNumberTypeID;	// phone type, fax, home, evening phone, etc.
		private string _phoneNumber;	// eFr phone standard 
		private bool _isActive;

		public PhoneNumber()
		{
			_phoneNumberTypeID = PhoneNumberType.DAY_PHONE;
		}

		public PhoneNumber(string phoneNumber) : this(phoneNumber, PhoneNumberType.DAY_PHONE)
		{
		}

		public PhoneNumber(string phoneNumber, PhoneNumberType type) 
		{
			_phoneNumberTypeID = type;
			_phoneNumber = phoneNumber;
		}

		public void SetPhoneNumber(int indicatif, int prefix, int suffix) {
			_phoneNumber = indicatif.ToString() + "-" + prefix.ToString() + "-" + suffix.ToString();
		}

		public void SetPhoneNumber(int indicatif, int prefix, int suffix, int extension) {
			_phoneNumber = indicatif.ToString() + "-" + prefix.ToString() + "-" + suffix.ToString() + " " + extension;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>This method might throw an exception.</remarks>
		/// <param name="phoneNum"></param>
		public void SetPhoneNumber(string phoneNumber) {
			_phoneNumber = phoneNumber.Trim();
		}

		public string GetPhoneNumber() {
			try {
				string nphone = _phoneNumber.Substring(0, 3);
				nphone += "-";
				nphone += _phoneNumber.Substring(3, 3);
				nphone += "-";
				nphone += _phoneNumber.Substring(6, 4);
				if(_phoneNumber.Length > 10) {
					nphone += " Ext: " + _phoneNumber.Substring(10);
				}
				return nphone;
			} catch {
				return _phoneNumber;
			}
		}

		#region Properties
		public int ID {
			set { _id = value; }
			get { return _id; }
		}

		public PhoneNumberType PhoneNumberTypeID {
			set { _phoneNumberTypeID = value; }
			get { return _phoneNumberTypeID; }
		}

		public string FormattedPhoneNumber {
			get { return _phoneNumber; }
		}

		public bool IsActive {
			set { _isActive = value; }
			get { return _isActive; }
		}
		#endregion
	}
}
