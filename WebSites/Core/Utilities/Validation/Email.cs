using System;
using System.Text;
using System.Text.RegularExpressions;

namespace GA.BDC.Core.Utilities.Validation {

	/// <summary>
	/// Email validator
	/// </summary>
	public class Email {

		private const string __EmailRegEx = @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z0-9]{1,6}$";
		private const string __EmailOldRegEx = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
        //Added by Jiro Hidaka (October 1, 2008) for Pavel Tarassov
        private const string EmailRegEx = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        //Added by Jiro Hidaka (October 6, 2008) for Pavel Tarassov
        private const string RFCRegex = @"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

		public Email() {

		}

		/// <summary>
		/// Basic verification if an email address is valid
		/// </summary>
		/// <param name="emailAddress">Email Address</param>
		/// <returns>TRUE if the email is valid</returns>
		public static bool IsValidEmail(string emailAddress) {
            Regex regEx = new Regex(RFCRegex);
			return regEx.IsMatch(emailAddress);
		}

	}
}
