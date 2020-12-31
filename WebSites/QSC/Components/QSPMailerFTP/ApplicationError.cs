using System;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace QSPMailerFTP
{
	/// <summary>
	/// Summary description for ApplicationError.
	/// </summary>
	public class ApplicationError
	{
		private static string mail_from = "FTP_error@qsp.ca";
		private static string mail_to = "Nicolas.Hamel@ReadersDigest.com, Karen.Tracy@ReadersDigest.com";
		private static string smtp = "nasmtp.us.rdigest.com";
		
		public static void ManageError(Exception ex)
		{
			new SendMail(MailTypes.CmdLineApplication,"",MailFrom,MailTo,Smtp,ex);
		}

		public static string MailFrom 
		{
			get 
			{
				return mail_from;
			}
			set 
			{
				mail_from = value;
			}
		}

		public static string MailTo 
		{
			get 
			{
				return mail_to;
			}
			set 
			{
				mail_to = value;
			}
		}

		public static string Smtp
		{
			get 
			{
				return smtp;
			}
			set 
			{
				smtp = value;
			}
		}
	}
}
