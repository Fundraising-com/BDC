using System;

namespace GA.BDC.Core.EnterpriseStandards {

	public class PhoneException : Exception {
		public PhoneException() {

		}

		public PhoneException(string message) : base( message) {

		}

		public PhoneException(string message, Exception innerException) : base(message, innerException) {

		}
	}

	/// <summary>
	/// Summary description for PhoneNumber.
	/// </summary>
	public class Phone {
		private string phoneNumber;

		private Phone() {
			//
			// TODO: Add constructor logic here
			//
		}

		public static implicit operator string(Phone phone) {
			return phone.FormattedPhoneNumber;
		}

		public static Phone ByNorthAmericaFormattedString(string formattedString) {
			Phone p = new Phone();
            
			string[] s = null;

			// try to split phone number (should look like this '555-555-5555'
			try {
				s = formattedString.Split('-');
			} catch(System.Exception ex) {
				throw new PhoneException("Unable to split North American Phone Number : " + formattedString, ex);
			}

			// check indicatif
			try {
				int.Parse(s[0]);
			} catch(System.Exception ex) {
				throw new PhoneException("Unable to understand indicatif North American Phone Number : " + formattedString, ex);
			}

			if(s[0].Length != 3) {
				throw new PhoneException("Unable to understand indicatif North American Phone Number : " + formattedString);
			}

			// check prefix
			try {
				int.Parse(s[1]);
			} catch(System.Exception ex) {
				throw new PhoneException("Unable to understand prefix North American Phone Number : " + formattedString, ex);
			}

			if(s[1].Length != 3) {
				throw new PhoneException("Unable to understand prefix North American Phone Number : " + formattedString);
			}

			// check sufix and extention

			string extention = "";
			try {
				string[] ss = s[2].Split('x');
				s[2] = ss[0];
				extention = ss[1];
			} catch {
				// no extention
			}

			try {
				int.Parse(s[2]);
			} catch(System.Exception ex) {
				throw new PhoneException("Unable to understand sufix North American Phone Number : " + formattedString, ex);
			}

			if(s[2].Length != 4) {
				throw new PhoneException("Unable to understand sufix North American Phone Number : " + formattedString);
			}

			if(extention != "") {
				try {
					int.Parse(extention);
				} catch(System.Exception ex) {
					throw new PhoneException("Unable to understand extention North American Phone Number : " + formattedString, ex);
				}
			}

			p.phoneNumber = formattedString;
			return p;
		}

		public static Phone NorthAmericaPhoneNumber(int indicatif, int prefix, int sufix) {
			Phone p = new Phone();
			p.phoneNumber = indicatif.ToString("000") + "-" + prefix.ToString("000") + "-" + sufix.ToString("0000");
			return p;
		}

		public static Phone NorthAmericaPhoneNumber(int indicatif, int prefix, int sufix, int extention) {
			Phone p = new Phone();
			p.phoneNumber = indicatif.ToString("000") + "-" + prefix.ToString("000") + "-" + sufix.ToString("0000") + "x" + extention.ToString();
			return p;
		}

		#region Attributes
		public string FormattedPhoneNumber {
			// set { phoneNumber = value; }
			get { return phoneNumber; }
		}
		#endregion
	}
}
