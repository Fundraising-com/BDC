using System;
using System.ComponentModel;
using System.Web.Mail;
using System.Configuration;
using System.Text;
using System.Web;


namespace QSPMailerFTP
{
	/// <summary>
	/// Summary description for SendMail.  
	/// </summary>
	/// <Author>Dave Mustaikis</Author>
	/// <Date>20 octobre 2003</Date>
	public enum MailTypes{WebApplication, CmdLineApplication};

	public class SendMail 
	{
		Exception ErrorInfo;

		public SendMail(MailTypes MailType,string MoreInfo,string From,string TO,string SMTP,Exception EX)
		{
			ErrorInfo = EX;
			try
			{
				Create(MailType,MoreInfo,From,TO,SMTP);
			}
			catch
			{
			}
			
		}

		public SendMail(MailTypes MailType,string MoreInfo,string From,string TO,string SMTP,string Message)
		{
			try
			{
				Create(MailType,MoreInfo,From,TO,SMTP,Message);
			}
			catch
			{
			}
			
		}
		/*/// <summary>
		///	Send Email Notification that an occured
		/// </summary>
		/// <param name="From"></param>
		/// <param name="TO"></param>
		/// <param name="SMTP"></param>
		/// <param name="ErrorID_PK"></param>
		/// <param name="EX"></param>
		public SendMail(string From,string TO,string SMTP,int ErrorID_PK,Exception EX)
		{
				
			SendMailErrorToSupport("",From,TO,SMTP,ErrorID_PK);
			
		}*/
		private void Create(MailTypes MailType,string MoreInfo,string From,string TO,string SMTP)
		{
		
			switch(MailType)
			{
				case MailTypes.CmdLineApplication:
				case MailTypes.WebApplication:
					SendMailErrorToSupport(MoreInfo,From,TO,SMTP);
					break;
			
				default:
					throw new ArgumentOutOfRangeException();
			}	
		}

		private void Create(MailTypes MailType,string MoreInfo,string From,string TO,string SMTP, string Message)
		{
		
			switch(MailType)
			{
				case MailTypes.CmdLineApplication:
				case MailTypes.WebApplication:
					SendMailToSupport(MoreInfo,From,TO,SMTP,Message);
					break;
			
				default:
					throw new ArgumentOutOfRangeException();
			}	
		}
		
		private void SendMailErrorToSupport(string MoreInfo,string From,string TO,string SMTP)
		{
			StringBuilder SB = new StringBuilder();
			MailMessage Message = new MailMessage();
			CreateMailHeader(From,TO,Message);
			CreateMailBody(Message,SB);

			
			
			Message.Body = SB.ToString();

			SendMailMessage(SMTP,Message);
		}

		private void SendMailToSupport(string MoreInfo,string From,string TO,string SMTP, string BodyMessage)
		{
			StringBuilder SB = new StringBuilder();
			MailMessage Message = new MailMessage();
			CreateMailHeader(From,TO,Message);
			CreateMailBody(Message,SB, BodyMessage);

			
			
			Message.Body = SB.ToString();

			SendMailMessage(SMTP,Message);
		}
		
		private void CreateMailBody(MailMessage Message,StringBuilder SB)
		{
			
			AppendErrorMessage(SB);
			AppendStackTrace(SB);
			
		}

		private void CreateMailBody(MailMessage Message,StringBuilder SB, string BodyMessage)
		{
			
			AppendCustomMessage(SB, BodyMessage);
			
		}
		
		private void AppendErrorMessage(StringBuilder SB)
		{
			// Get the exception object for the last error message that occured.
						
			SB.Append("Error Message: " + ErrorInfo.Message);
			SB.Append("\nError Source: " + ErrorInfo.Source ); 
			SB.Append("\nError Target Site: " + ErrorInfo.TargetSite);
    		
				
			SB.Append("\n\n\n");
			
		}

		private void AppendStackTrace(StringBuilder SB)
		{
			SB.Append("Exception Stack Trace:\n----------------------\n" + ErrorInfo.StackTrace);
		}

		private void AppendCustomMessage(StringBuilder SB, string CustomMessage) 
		{
			SB.Append(CustomMessage);
		}

		private void CreateMailHeader(string From,string TO,MailMessage Message)
		{
			Message.From = From; 
			Message.To = TO;

			if(ErrorInfo != null) 
			{
				Message.Subject = ErrorInfo.Source + "Error";
			} 
			else 
			{
				Message.Subject = "Success";
			}
		}
		private void SendMailMessage(string SMTP,MailMessage Message)
		{
			SmtpMail.SmtpServer = SMTP;
			try
			{
				System.Web.Mail.SmtpMail.Send(Message);
			}
			catch
			{
			}
		}
	}
}