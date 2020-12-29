using System;

namespace GA.BDC.Core.efundraisingCore {

	public class TellFriend {

		private int id;
		private string cultureCode;
		private string fromName;
		private string fromEmail;
		private string toName;
		private string toEmail;
		private string subject;
		private string message;
		private DateTime dateSent;


		public TellFriend() {

		}


		#region Attributes

		public int Id {
			set { id = value; }
			get { return id; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string FromName {
			set { fromName = value; }
			get { return fromName; }
		}

		public string FromEmail {
			set { fromEmail = value; }
			get { return fromEmail; }
		}

		public string ToName {
			set { toName = value; }
			get { return toName; }
		}

		public string ToEmail {
			set { toEmail = value; }
			get { return toEmail; }
		}

		public string Subject {
			set { subject = value; }
			get { return subject; }
		}

		public string Message {
			set { message = value; }
			get { return message; }
		}

		public DateTime DateSent {
			set { dateSent = value; }
			get { return dateSent; }
		}

		#endregion
	}
}
