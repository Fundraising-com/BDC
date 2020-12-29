//
// 2005-06-16 - Stephen Lim - New class.
// 2006-06-27 - KO - modified for Komunik

using System;
using System.Threading;
using GA.BDC.Core.Email.MassMail.Data;

namespace GA.BDC.Core.Email.MassMail
{
	/// <summary>
	/// Queue. This is a singleton class. Any operation on the queue is thread-safe.
	/// </summary>
	public class EmailQueue
	{
		#region Enums
		/// <summary>
		/// Sent status.
		/// </summary>
		public enum EmailSentStatus
		{
			/// <summary>
			/// New mail not yet sent.
			/// </summary>
			New = 0,

			/// <summary>
			/// Mail retrieved and ready for sending.
			/// </summary>
			Pending = 1,

			/// <summary>
			/// Mail sent successfully.
			/// </summary>
			Sent = 2,

			/// <summary>
			/// Error communicating or sending through SMTP server.
			/// </summary>
			SmtpError = 3,

			/// <summary>
			/// Mail message format error.
			/// </summary>
			MailError = 4,

			/// <summary>
			/// Database error.
			/// </summary>
			DatabaseError = 5,

			/// <summary>
			/// Unknown error occured.
			/// </summary>
			UnknownError = 6,

			/// <summary>
			/// Spam detected.
			/// </summary>
			Spam = 7,
			
			/// <summary>
			/// Hendled by Komunik, see variable: "komunik_return_value_id"
			/// </summary>
			Komunik = 10,
         /// <summary>
			/// Hendled by Amazon, see variable: "komunik_return_value_id and ext_mail_id"
			/// </summary>
			Amazon = 20
		}

		/// <summary>
		/// Komunik Sent status.
		/// </summary>
		public enum KomunikReturnValue : short
		{
			/// <summary>
			/// New mail not yet sent.
			/// </summary>
			New = 10,

			/// <summary>
			/// Mail retrieved and ready for sending.
			/// </summary>
			Pending = 11,

            ///<summary>
            ///Mail pending without batch checking
            ///</summary>
			GreenLightPending =12,

            /// <summary>
			/// Mail sent successfully.
			/// </summary>
			Sent = 0,

            ///<summary>
            /// Mail to be sent without batch checking
            /// </summary>
            GreenNew = 9,

			/// <summary>
			/// Error communicating or sending through SMTP server.
			/// </summary>
			SmtpError = 13,

			/// <summary>
			/// Mail message format error.
			/// </summary>
			MailError = 14,

			/// <summary>
			/// Database error.
			/// </summary>
			DatabaseError = 15,

			/// <summary>
			/// Unknown error occured.
			/// </summary>
			UnknownError = 16,

			/// <summary>
			/// Duplicate email detected.
			/// </summary>
			DuplicateEmail = 17,

			KomunikatorReturnValueError = 18,


            /// <summary>
            /// Postponed due to number FromEmail limit
            /// </summary>
            
            Postponed = 19,

			/// <summary>
			/// Komunik errors
			/// </summary>
			/// 

			Unspecified = 100,

			InvalidEmailAddress = 101,

			TouchIdNotUnique = 102,

			InvalidToEmailParameter = 201,

			InvalidIdParameter = 202,

			UnknownKomunikError = 401
		}
		
		//Send email using
		public enum MailService
		{
			MassMailer = 0,
			Komunikator = 1,
         Amazon = 2
		}
		#endregion

		#region Fields
		private static EmailQueue _instance = null;
		private static EmailCollection _emails = new EmailCollection();
		private static MailService mailService;
		#endregion

		#region Constructors

		/// <summary>
		/// Private constructor. This is a singleton class.
		/// Use static method to create instance of this class.
		/// </summary>
		private EmailQueue()
		{

		}
		#endregion

		#region Methods

		/// <summary>
		/// Get the singleton instance of this class.
		/// </summary>
		/// <returns>EmailQueue instance.</returns>
		public static EmailQueue NewQueue(MailService ms)
		{
			if (_instance == null) {
                _instance = new EmailQueue();
				mailService = ms;  
			}
            
			return _instance;
		}


      public  MailService ServiceType
      {

         get { return mailService; }
         set { mailService = value; }
      }

		/// <summary>
		/// Retrieve the first Email in the queue.
		/// </summary>
		/// <returns>Email object.</returns>
		public Email DeQueue(string stringConnection)
		{
			if(mailService == MailService.MassMailer)
			{
				lock (_instance)
				{
					// If collection is empty, retrieve from database
					if (_emails.Count == 0)
					{
						MassMailDataInterface mmDI = new MassMailDataInterface(stringConnection);
						_emails = mmDI.GetEmailQueue();
					}

					// If collection is not empty, return the first Email object.
					if (_emails.Count > 0)
					{
						Email e = _emails[0];
						_emails.RemoveAt(0);
						return e;
					}

					return null;
				}			
			}
			else
			{
				lock (_instance)
				{
					// If collection is empty, retrieve from database
					if (_emails.Count == 0)
					{
						MassMailDataInterface mmDI = new MassMailDataInterface(stringConnection);
						_emails = mmDI.KomunikatorGetEmailQueue();
					}

					// If collection is not empty, return the first Email object.
					if (_emails.Count > 0)
					{
						Email e = _emails[0];
						_emails.RemoveAt(0);
						return e;
					}

					return null;
				}			
			}
		}

		/// <summary>
		/// Append the Email object to the end of the queue.
		/// </summary>
		/// <param name="emailObj">Email object.</param>
      public void EnQueue(string stringConnection, Email emailObj)
		{
			if(mailService == MailService.MassMailer)
			{
				lock (_instance)
				{
					MassMailDataInterface mmDI = new MassMailDataInterface(stringConnection);
					mmDI.InsertEmailQueue(emailObj);
				}
			}
         else if(mailService == MailService.Amazon)
         {
            lock (_instance)
            {
               MassMailDataInterface mmDI = new MassMailDataInterface(stringConnection);
               mmDI.AmazonInsertEmailQueue(emailObj);
            }
         }
			else
			{
				lock (_instance)
				{
					MassMailDataInterface mmDI = new MassMailDataInterface(stringConnection);
					mmDI.KomunikatorInsertEmailQueue(emailObj);
				}
			}
		}

		/// <summary>
		/// Update the status of the email.
		/// </summary>
		/// <param name="email">Email object.</param>
		/// <param name="status">Status number.</param>
		/// <param name="msg">Message</param>
      public void UpdateStatus(string stringConnection, Email email)
		{
			if(mailService == MailService.MassMailer)
			{
				lock (_instance)
				{
					MassMailDataInterface mmDI = new MassMailDataInterface(stringConnection);
					mmDI.UpdateStatus(email);
				}
			}
			else
			{
				lock (_instance)
				{
					MassMailDataInterface mmDI = new MassMailDataInterface(stringConnection);
					mmDI.KomunikatorUpdateStatus(email);
				}
			}
		}
      #endregion

    

	}
}
