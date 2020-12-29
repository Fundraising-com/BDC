using System;
using System.Text.RegularExpressions;

namespace GA.BDC.Core.EnterpriseStandards {

	public class EmailAddressException : Exception {
		public EmailAddressException() {

		}

		public EmailAddressException(string message) : base(message) {

		}

		public EmailAddressException(string message, Exception innerException) : base(message, innerException) {

		}
	}
	/// <summary>
	/// Summary description for EmailAddress.
	/// </summary>
    [Serializable]
   public class EmailAddress : GA.BDC.Core.BusinessBase.BusinessBase
   {
		private string email;

		private EmailAddress() {

		}

		// implicit converstion from EmailAddress to string
		public static implicit operator string(EmailAddress email) {
			if (email == null)
				return null;
			else
				return email.Email;
		}

		// create email address
		public static EmailAddress CreateEmailAddress(string emailAddress) {
			EmailAddress email = new EmailAddress();
			if(GA.BDC.Core.Validation.Email.EmailValidator.ValidateEmail(emailAddress)) {
				email.email = emailAddress;
			} else {
				throw new EmailAddressException("Unable to validate email address: " + emailAddress);
			}
			return email;
		}

        // added by Jiro Hidaka (dec 9, 2008)
        public static EmailAddress CreateEmailAddressWithStrictValidation(string emailAddress)
        {
            EmailAddress email = new EmailAddress();
            if (GA.BDC.Core.Validation.Email.EmailValidator.ValidateEmailStrict(emailAddress))
            {
                email.email = emailAddress;
            }
            else
            {
                throw new EmailAddressException("Unable to strictly validate email address: " + emailAddress);
            }
            return email;
        }

		#region Attributes
		public string Email {
			get { return email; }
		}
		#endregion
	}
}
