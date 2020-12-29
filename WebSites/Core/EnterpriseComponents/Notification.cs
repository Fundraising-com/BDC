//
// 2005-05-24 - Stephen Lim - Use Email.SendMail instead of Helper.SendMail to remove dependency on Cdosys.dll.
//

using System;
using System.Web.Mail;
using System.Configuration;
using GA.BDC.Core.Email;

namespace GA.BDC.Core.EnterpriseComponents{

	/// <summary>
	/// Notification system.
	/// </summary>
	public sealed class Notification{

		public enum SeverityLevel{
			Low,
			Medium,
			High
		}

		public enum NotifyType {
			Email,
			EventViewer
		}

		/// <summary>
		/// constructor
		/// </summary>
		public Notification() {

		}

		public static void Notify(string applicationName, string message) {
			
		}

		public static void Notify(NotifyType notificationType, string applicationName, string message) {
			
		}

		/// <summary>
		/// This method sends a notification depending on Config settings (email, pager,...)
		/// </summary>
		/// <param name="severity">Severity level.</param>
		/// <param name="data">Data to send.</param>
		public static void Send(SeverityLevel severity, string data){

		   try{
			   // If email, we send by email
			   string notificationMethod = ConfigurationSettings.AppSettings["NotificationMethod"]; 
			   string notificationEmail = ConfigurationSettings.AppSettings["ErrorNotificationEmail"]; 

			   // We currently support email type only
			   if (notificationMethod.Equals("email")){
				
				   // Set the email parameters
				   string from = "eSubs@efundraising.com"; 
				   string to = notificationEmail;
				   string subject = "Error notification from eSubs!"; 
				   string body = data;

				   // Send the mail
				   SendMail.Send("mail.efundraising.com", from, to, "", "", "", "", subject, body, "");   				   
			   }

        	} catch {
				throw;
			}
		}

		/// <summary>
		/// Not yet implemented.
		/// </summary>
		/// <param name="severity"></param>
		/// <param name="data"></param>
		public static void SendEmail(SeverityLevel severity, string data)
		{
		}

		/// <summary>
		/// Not yet implemented.
		/// </summary>
		/// <param name="severity"></param>
		/// <param name="data"></param>
		public static void SendPager(SeverityLevel severity, string data)
		{
		} 
	}
}
