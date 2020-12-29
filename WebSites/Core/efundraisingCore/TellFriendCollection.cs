using System;
using System.Collections;

using GA.BDC.Core.Email;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.Collections;
using System.Configuration;

namespace GA.BDC.Core.efundraisingCore {

	public class TellFriendCollection {
		private string fromName;
		private string fromEmail;
		private string subject;
		private string message;
		private string cultureCode;
		private ArrayList tellFriends = null;

		public TellFriendCollection(string fromName, string fromEmail, string subject, string message, string cultureCode) {
			this.fromName = fromName;
			this.fromEmail = fromEmail;
			this.subject = subject;
			this.message = message;
			this.cultureCode = cultureCode;
			tellFriends = new ArrayList();
		}

		public void AddNewFriend(string name, string email) {
			TellFriend tellFriend = new TellFriend();

			// set the from info
			tellFriend.FromName = this.fromName;
			tellFriend.FromEmail = this.fromEmail;

			// set the from info
			tellFriend.ToName = name;
			tellFriend.ToEmail = email;

			// set the subject and messages
			tellFriend.Subject = this.subject;
			tellFriend.Message = this.message;

			// set the culture code of the sender
			tellFriend.CultureCode = this.cultureCode;

			// set the date sent to now
			tellFriend.DateSent = DateTime.Now;

			// add the new frien into the collection
			tellFriends.Add(tellFriend);
		}

		public void SendEmails() {
			DataAccess.EFundDatabase dbo = new DataAccess.EFundDatabase();
			dbo.InsertTellAFriend(this);

            string server = ConfigurationManager.AppSettings["Common.Email.SmtpServer"];
			
			foreach(TellFriend tf in tellFriends) {
				string msg = message.Replace("<Participant Name>", fromName);
				msg = msg.Replace("<Name>", tf.ToName);

				SendMail.AsyncSend(server, "\"" + fromName + "\" <" + fromEmail + ">", "\"" + tf.ToName + "\" <" + tf.ToEmail + ">", 
					null, null, fromEmail , "alaska2006@efundraising.com", subject, msg.Replace("<br>", "\r\n"), msg);
			}
		}		

		public ArrayList Friends {
			get { return tellFriends; }
		}
	}
}
