using System;
using System.Web.Mail;
using System.Configuration;
using System.IO;
using GA.BDC.Core.EnterpriseComponents;
using System.Data;

using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

using System.Collections;
using System.ComponentModel;

using System.Web.SessionState;
using System.Web.UI;
using System.Text;


using System.Threading;


	


//using Email;

namespace CRMWeb.Classes
{
	/// <summary>
	/// Summary description for SendEmail.
	/// </summary>
	public class SendEmail : System.Web.UI.Page {
		// State information used in the task.
		protected string reply;
		protected string from;
		protected string subject;
		protected string body;
        protected string to;
        protected string cc;
        protected string bcc;

		private UserControls.AllAccountsForLead_Lotus acc;
		
		// The constructor obtains the state information.
		public SendEmail(){
			
		}
		

		public string REPLY{
			get {
				return reply;
			}
			set {
				reply = value;
			}
		}
        
		public string FROM{
			get {
				return from;
			}
			set {
				from = value;
			}
		}
        
		public string SUBJECT{
			get {
				return subject;
			}
			set {
				subject = value;
			}
		}
        
		public string BODY{
			get {
				return body;
			}
			set {
				body = value;
			}
		}
        
		public string TO{
			get {
				return to;
			}
			set {
				to = value;
			}
		}
        
		public string CC{
			get {
				return cc;
			}
			set {
				cc = value;
			}
		}
		
		public string BCC{
			get {
				return bcc;
			}
			set {
				bcc = value;
			}
		}
        
        
		// The thread procedure performs the task, such as formatting 
		// and printing a document.
		public void Send(){
			try{


	

				MailMessage m = new MailMessage();
			
                string[] cc_col = null;
                string[] to_col = new string[1]; 

				//get email variables
				string sendEmailForReal = ConfigurationSettings.AppSettings["SendTestEmails"].ToString();
				if (sendEmailForReal == "false"){
					to_col[0] = TO;
					string ccs = ConfigurationSettings.AppSettings["CC_Assignment_Email"].ToString();
					cc_col = ccs.Split(',');
					
				}else{
				    to_col[0] = ConfigurationSettings.AppSettings["SendTestEmailTo"].ToString();
				}

						
				m.From = from + "<"+ reply + ">";
				m.Headers.Add( "Reply-To", reply);
				
				m.Subject = subject;
				m.Body = body;
				m.BodyFormat = MailFormat.Html;
				string smtp = ConfigurationSettings.AppSettings["SMTP_SERVER"].ToString();
				SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["SMTP_SERVER"].ToString();

				//SmtpMail.Send(m);
            //efundraising.Email.SendMail.Send(smtp,m.From,to_col,cc_col,null,reply,"",m.Subject,m.Body,m.Body);

			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"SendEmail.Send");
			}	
		}

		public static void SendNotification(string message){
		
			try{
				bool sendNotif = Convert.ToBoolean(ConfigurationSettings.AppSettings["SendNotificationEmails"]);
				if (sendNotif){
										
					MailMessage m = new MailMessage();
					m.To = ConfigurationSettings.AppSettings["NotificationEmail"].ToString();
					m.From = "crm@efundraising.com";
					//m.Headers.Add( "Reply-To", reply);
					m.Subject = "CRM Notification";
					m.Body = message;
					m.BodyFormat = MailFormat.Text;
					SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["SMTP_SERVER"].ToString();
                    
					string smtp = ConfigurationSettings.AppSettings["SMTP_SERVER"].ToString();
				    //SmtpMail.Send(m);
               //efundraising.Email.SendMail.Send(smtp,m.From,m.To,"","","","",m.Subject,m.Body,m.Body);

				}
			}catch(Exception ex){
				StreamWriter sw = new StreamWriter("d:\\error666.txt");
				sw.Write("text" + ex.ToString());
				sw.Close();
		    
	        }	
		}


		public static void SendConfirmation(string message, int assigner){
		
			try{
					MailMessage m = new MailMessage();
					m.To = ConfigurationSettings.AppSettings["NotificationEmail"].ToString();
					m.From = "crm@efundraising.com";
					//m.Headers.Add( "Reply-To", reply);
					m.Subject = "Confirmation " + assigner;
					m.Body = message;
					m.BodyFormat = MailFormat.Text;
					SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["SMTP_SERVER"].ToString();
                    
					string smtp = ConfigurationSettings.AppSettings["SMTP_SERVER"].ToString();
					//SmtpMail.Send(m);
               //efundraising.Email.SendMail.Send(smtp,m.From,m.To,"","","","",m.Subject,m.Body,m.Body);

				
			}catch(Exception ex){
				StreamWriter sw = new StreamWriter("d:\\error666.txt");
				sw.Write("text" + ex.ToString());
				sw.Close();
		    
			}	
		}

		public void BuildConfirmationEmail(int leadID){
        try{
			string valeur, tag, message = "";
			EmailHandler handler = new EmailHandler(1);

			string projectLocation = Session[Global.SessionVariables.PROJECT_LOCATION].ToString();
			
			
			//get lead info
			DataTable dt =  DatabaseObjects.GetLeadInfo(leadID);
			if (dt.Rows.Count > 0){
				foreach (DataColumn dc in dt.Columns ){
					valeur =  dt.Rows[0][dc].ToString();
					tag = "[" + dc.ColumnName.ToString() + "]";
					handler.AddGenericParsePatternValue(tag,valeur);
				}


				handler.AddGenericParsePatternValue("[All_Accounts]",getAccountsHTML(leadID));
			

				
				string path = projectLocation +  "\\Doc_Confirmation.html";
   
				TextReader text_message = new StreamReader(path);
				
				// 2nd Line of the texfile contains the subject
				text_message.ReadLine();
				string emailsubject = text_message.ReadLine();
				
				// Replace the tags in the subject
				SUBJECT = handler.ParseString(emailsubject)[0].ToString();
				
				// Reads the file to the end
				message = text_message.ReadToEnd();
			
				// Close the node to the file
				text_message.Close();
			
				// Replace the tags in the message
				BODY = handler.ParseString(message)[0].ToString();
				
			}
	      
            }catch(Exception ex){
	          throw new Global.CRMException("",ex,0,"SendEmail.BuildConfirmationEmail");
            }	
		
		}

		
		public void BuildGirlScoutConfirmationEmail(int leadID)
		{
			try
			{
				string valeur, tag, message = "";
				EmailHandler handler = new EmailHandler(1);

				string projectLocation = Session[Global.SessionVariables.PROJECT_LOCATION].ToString();
			
	    		SUBJECT = "A Girl Scout lead was found.";
	 			BODY = "A Girl Scout lead was found. Lead ID is " + leadID;
	 				      
			}
			catch(Exception ex)
			{
				throw new Global.CRMException("",ex,0,"SendEmail.BuildConfirmationEmail");
			}	
		
		}


		public string getAccountsHTML(int leadID)
		{
			try{
				
		    	acc = (UserControls.AllAccountsForLead_Lotus) LoadControl("~\\UserControls\\AllAccountsForLead_Lotus.ascx");
				acc.GenerateControl(leadID);        
	
				//Get the rendered HTML
				StringBuilder sb = new StringBuilder(); 
				StringWriter sw = new StringWriter(sb);
				HtmlTextWriter htmlTW = new HtmlTextWriter(sw);
                
				acc.RenderControl(htmlTW);
				
				string accHTML = sb.ToString();
				
	
    			return (accHTML);
			
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"SendEmail.getAccountsHTML");
			}
			

		}

	


	}
}
