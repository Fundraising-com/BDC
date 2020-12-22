using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.Mail;
using System.Diagnostics;
using System.Resources;
using System.Configuration;
using log4net;

namespace CRMWeb.Classes {
	/// <summary>
	/// Summary description for EmailHandler.
	/// </summary>
	public class EmailHandler {
      private readonly ILog _logger = LogManager.GetLogger(typeof(EmailHandler));
		public ArrayList generic_Parse_Pattern_Value = new  ArrayList(); 
		public ArrayList specific_Parse_Pattern_Value = new  ArrayList(); 
		public int message_count = 1;

		public struct Pattern_Value {
			public string pattern;
			public string patternvalue;
		
			public void SetPatternAndValue(string setpattern, string setpatternvalue) {
				pattern = setpattern;
				patternvalue = setpatternvalue;
			}
		}

		public struct Specific_Pattern_Value {
			public string pattern;
			public ArrayList patternvalues;
		
			public void SetPatternAndValue(string setpattern, ArrayList setpatternvalues) {
				pattern = setpattern;
				patternvalues = setpatternvalues;
			}
		}


		public EmailHandler(int count) {
			//
			// TODO: Add constructor logic here
			//
			message_count = count;
		}


	/*	public Helper.EmailError SendMail(string to, string from, string subject, string body, string reply, MailFormat mailformat) {
			try {
				MailMessage m = new MailMessage();

				if(!Helper.IsValidEmail(to)) {
					return Helper.EmailError.ToAddressBadSyntax;
				}

				//si on met ca, on peut pas mettre ca comme from: "\"John Smith\" <you@yourcompany.com>";
					if(!Helper.IsValidEmail(from)) 
					{
						return Helper.EmailError.FromAddressBadSyntax;
					}

				m.From = from + "<"+ reply + ">";
				m.To = to;
				m.Headers.Add( "Reply-To", reply);
				m.Subject = subject;
				m.Body = body;
				m.BodyFormat = mailformat;
				//	SmtpMail.SmtpServer = "EFUND_MIS_134";
				SmtpMail.SmtpServer = GetSmptServer();
				SmtpMail.Send(m);
			} 
			catch( System.Exception e) {
				// trow a warning
				LoggingSystem.LogWarning("EmailHandler.SendMail: Unable to Send Mail" + e.ToString());

				return Helper.EmailError.UnableToSend;
			}
			return Helper.EmailError.Ok;
		}
*/
		
		public void AddGenericParsePatternValue(string pattern, string patternvalue) {
			try {
				Pattern_Value newpattern = new Pattern_Value();
				newpattern.SetPatternAndValue(pattern,patternvalue);
				generic_Parse_Pattern_Value.Add(newpattern);

		}catch(Exception ex){
		    throw new Global.CRMException("",ex,0,"EmailHandler.AddGenericParsePatterValue ");
	      }	
		}

		public void AddSpecificPatternValue(string pattern, ArrayList patternvalues) {
			try {

				Specific_Pattern_Value newpattern = new Specific_Pattern_Value();
				newpattern.SetPatternAndValue(pattern,patternvalues);
				specific_Parse_Pattern_Value.Add(newpattern);
			}

			catch(Exception e) {
				// trow a warning
				_logger.Warn(e);
			}
		}

		public ArrayList ParseString(string stringtoparse) {
			int current;
			string output_message="";
			ArrayList message_array = new  ArrayList(); 

			
			foreach( Pattern_Value patvalue in generic_Parse_Pattern_Value) {
			//	Debug.Write(patvalue.ToString());
				stringtoparse = stringtoparse.Replace(patvalue.pattern,patvalue.patternvalue);
				output_message = stringtoparse;
			} 

			if (message_count == 1){
				message_array.Add(output_message);
			}else{
				
				// Parse speficific
				
				for(current = 0; current < message_count ;current++) {
					foreach(Specific_Pattern_Value patvalue in specific_Parse_Pattern_Value) {
						string pattern_value = patvalue.patternvalues[current].ToString();
						output_message = stringtoparse.Replace(patvalue.pattern,pattern_value);
					} 

					message_array.Add(output_message);
				}
			}

			return 	message_array;	
		}

	/*	public string GetSmptServer() {
			if(ConfigurationSettings.AppSettings[GlobalVariables.SMTP_SERVER] != null) {
				return  ConfigurationSettings.AppSettings[GlobalVariables.SMTP_SERVER].ToString();
			}

			else {
				// Log  warning
				LoggingSystem.LogWarning("No smtp server set in the ConfigurationSettings.AppSettings");
				return  "mail.efundraising.com";
			}	
		}*/

		#region Getters and setters
		/*
	 		
			public string To 
			{
				set { to = value; }
				get { return to; }
			}

			public string From 
			{
				set { from = value; }
				get { return from; }
			}

			public string Subject 
			{
				set { subject = value; }
				get { return subject; }
			}

			public string Html_email 
			{
				set { html_email = value; }
				get { return html_email; }
			}

			public string Txt_email 
			{
				set { txt_email = value; }
				get { return txt_email; }
			}
	*/	
		#endregion
	}
}
