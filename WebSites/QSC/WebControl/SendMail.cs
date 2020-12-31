using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.Mail;
using System.Configuration;
using System.Text;
using System.Web;


namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for SendMail.  
	/// </summary>
	/// <Author>Dave Mustaikis</Author>
	/// <Date>20 octobre 2003</Date>
	public enum MailTypes{ErrorWebApplication};

	public class SendMail 
	{
		Exception ErrorInfo;

		public SendMail(MailTypes MailType,string MoreInfo,string From,string TO,string SMTP)
		{
			ErrorInfo = System.Web.HttpContext.Current.Server.GetLastError().GetBaseException();
			Create(MailType,MoreInfo,From,TO,SMTP);
		}
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
				case MailTypes.ErrorWebApplication:
					SendMailErrorToSupport(MoreInfo,From,TO,SMTP);
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
			AppendMoreInfo(SB,MoreInfo);
			CreateMailBody(Message,SB);

			
			
			Message.Body = SB.ToString();

			SendMailError(SMTP,Message);
		}
		
		private void CreateMailBody(MailMessage Message,StringBuilder SB)
		{
			
			AppendErrorMessage(SB);
			AppendQueryStringInfo(SB);
			AppendPostDataInfo(SB);
			AppendUserInfo(SB);
			AppendStackTrace(SB);
			AppendServerVariableInfo(SB);
			
		}
		
		private void AppendErrorMessage(StringBuilder SB)
		{
			SB.Append("Error in: " + System.Web.HttpContext.Current.Request.Path); 
			SB.Append("\nUrl: " + System.Web.HttpContext.Current.Request.RawUrl + "\n\n");
    
			// Get the exception object for the last error message that occured.
						
			SB.Append("Error Message: " + ErrorInfo.Message);
			SB.Append("\nError Source: " + ErrorInfo.Source ); 
			SB.Append("\nError Target Site: " + ErrorInfo.TargetSite);
			SB.Append("\n\nQueryString Data:\n-----------------\n");
    		
				
			SB.Append("\n");
			
		}

		private void AppendQueryStringInfo(StringBuilder SB)
		{
			
			// Gathering QueryString information
			for (int i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++) 
					SB.Append(System.Web.HttpContext.Current.Request.QueryString.Keys[i] + ":\t\t" + System.Web.HttpContext.Current.Request.QueryString[i] + "\n");
					SB.Append("\nPost Data:\n----------\n");
				
		}

		private void AppendPostDataInfo(StringBuilder SB)
		{
			bool showViewState =
				((ErrorInfo is HttpException &&
				ErrorInfo.Message == "Unable to validate data.") ||
				(ErrorInfo is FormatException &&
				ErrorInfo.Message == "Invalid length for a Base-64 char array.") ||
				(ErrorInfo is FormatException &&
				ErrorInfo.Message == "Invalid character in a Base-64 string."));

			// Gathering Post Data information
			for (int i = 0; i < System.Web.HttpContext.Current.Request.Form.Count; i++) 
			{
				if(showViewState || System.Web.HttpContext.Current.Request.Form.Keys[i].IndexOf("VIEWSTATE")== -1)
				{
					SB.Append(System.Web.HttpContext.Current.Request.Form.Keys[i] + ":\t\t" + System.Web.HttpContext.Current.Request.Form[i] + "\n");
					SB.Append("\n");
				}
			}
		}

		private void AppendUserInfo(StringBuilder SB)
		{
			if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
				SB.Append("User:\t\t" + System.Web.HttpContext.Current.User.Identity.Name + "\n\n");
		}
		private void AppendStackTrace(StringBuilder SB)
		{
			SB.Append("Exception Stack Trace:\n----------------------\n" + ErrorInfo.StackTrace);
			SB.Append("\n\nServer Variables:\n-----------------\n");
		}

		private void AppendServerVariableInfo(StringBuilder SB)
		{
			// Gathering Server Variables information
			for (int i = 0; i < System.Web.HttpContext.Current.Request.ServerVariables.Count; i++) 
				SB.Append(System.Web.HttpContext.Current.Request.ServerVariables.Keys[i] + ":\t\t" + HttpContext.Current.Request.ServerVariables[i] + "\n");
		}
		private void CreateMailHeader(string From,string TO,MailMessage Message)
		{
			Message.From = From; 
			Message.To = TO;
			Message.Subject = MailTypes.ErrorWebApplication.ToString();
		
		}
		private void SendMailError(string SMTP,MailMessage Message)
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
		private void AppendMoreInfo(StringBuilder SB,string MoreInfo)
		{
			SB.Append("\n\nMore info:\n----------------------\n");
			SB.Append(MoreInfo);
			SB.Append("\n\nServer Variables:\n-----------------\n");
		}
	}
}
