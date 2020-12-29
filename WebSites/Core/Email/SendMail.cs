// 
// 2004-12-06 - Stephen Lim - New class.
// 2005-02-07 - Stephen Lim - Send() can now accept a Smtp string in addition to the SmtpServer object.
// 2005-02-13 - Stephen Lim - Fix CDO null reference by using simple HTMLBody & TextBody properties instead.
// 2005-02-15 - Stephen Lim - Fix Return-Path not sending bounce back to correct address.
// 2005-05-17 - Stephen Lim - Use DotNetOpenMail 0.5.1b instead of CDO.
//

using System;
using System.Threading;
using DotNetOpenMail;
using System.Web.Mail;


namespace GA.BDC.Core.Email
{
	/// <summary>
	/// Simple mailer to send multipart HTML & TEXT email.
	/// </summary>
	/// <example>
	/// Calling Send using the SmtpServer object.
	/// <code></code>
	/// SmtpServer s = new SmtpServer("localhost");
	/// s.ConnectionTimeout = 60;
	/// try {
	///		SendMail.Send(s, "sender@example.com", "recipient@example.com", "", 
	///						"", "", "", "Subject", "Text body", "Html body"); 
	///	}
	///	catch (Exception e) {
	///		Console.WriteLine(e.ToString());
	///	}
	///	</code>
	///	
	/// Calling Send by passing a smtp string.
	/// <code></code>
	/// try {
	///		SendMail.Send("localhost:25", "sender@example.com", "recipient@example.com", "", 
	///						"", "", "", "Subject", "Text body", "Html body"); ;
	///	}
	///	catch (Exception e) {
	///		Console.WriteLine(e.ToString());
	///	}
	///	</code>
	/// </example>
	/// 
    [Serializable]
	public class SendMail
	{
		private SmtpServer _smtpServer = null;
		private string _from = "";
		private string[] _to = null;
		private string[] _cc = null;
		private string[] _bcc = null;
		private string _replyTo = "";
		private string _returnPath = "";
		private string _subject = "";
		private string _textBody = "";
		private string _htmlBody = "";

		private SendMail(string smtpServerString, string from, string to, 
			string cc, string bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody) : 
			this(new SmtpServer(smtpServerString), from, to, cc, bcc, replyTo, returnPath, subject, textBody, htmlBody)
		{
		}

		private SendMail(string smtpServerString, string from, string[] to, 
			string[] cc, string[] bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody) : 
				this(new SmtpServer(smtpServerString), from, to, cc, bcc, replyTo, returnPath, subject, textBody, htmlBody)
		{
		}

		private SendMail(SmtpServer smtpServer, string from, string to, 
			string cc, string bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody) :
			this(smtpServer, from, new String[] {to}, new String[] {cc}, new String[] {bcc}, 
			replyTo, returnPath, subject, textBody, htmlBody)
		{
		}

		private SendMail(SmtpServer smtpServer, string from, string[] to, 
			string[] cc, string[] bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody)
		{
			_smtpServer = smtpServer;
			_from = from;
			_to = to;
			_cc = cc;
			_bcc = bcc;
			_replyTo = replyTo;
			_returnPath = returnPath;
			_subject = subject;
			_textBody = textBody;
			_htmlBody = htmlBody;
		}

		/// <summary>
		/// Send mail asynchronously.
		/// </summary>
		/// <param name="smtpServerString"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="cc"></param>
		/// <param name="bcc"></param>
		/// <param name="replyTo"></param>
		/// <param name="returnPath"></param>
		/// <param name="subject"></param>
		/// <param name="textBody"></param>
		/// <param name="htmlBody"></param>
		public static void AsyncSend(string smtpServerString, string from, string to, 
			string cc, string bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody)
		{
			try {
				SendMail s = new SendMail(smtpServerString, from, to, 
					cc, bcc, replyTo, returnPath, 
					subject, textBody, htmlBody);
				Thread t = new Thread(new ThreadStart(s.AsyncSend));
				t.Start();
			}
			catch (ThreadAbortException) {
				// Do nothing on thread abort.
			} catch(System.Exception ex) {
				string s = ex.Message;
			}
		}


		/// <summary>
		/// Send mail asynchronously.
		/// </summary>
		/// <param name="smtpServer"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="cc"></param>
		/// <param name="bcc"></param>
		/// <param name="replyTo"></param>
		/// <param name="returnPath"></param>
		/// <param name="subject"></param>
		/// <param name="textBody"></param>
		/// <param name="htmlBody"></param>
		public static void AsyncSend(SmtpServer smtpServer, string from, string to, 
			string cc, string bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody)
		{
			try 
			{
				SendMail s = new SendMail(smtpServer, from, to, 
					cc, bcc, replyTo, returnPath, 
					subject, textBody, htmlBody);
				Thread t = new Thread(new ThreadStart(s.AsyncSend));
				t.Start();
			}
			catch (ThreadAbortException)
			{
				// Do nothing on thread abort.
			} catch(System.Exception ex) {
				string s = ex.Message;
			}
		}

		/// <summary>
		/// Send mail asynchronously.
		/// </summary>
		/// <param name="smtpServer"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="cc"></param>
		/// <param name="bcc"></param>
		/// <param name="replyTo"></param>
		/// <param name="returnPath"></param>
		/// <param name="subject"></param>
		/// <param name="textBody"></param>
		/// <param name="htmlBody"></param>
		public static void AsyncSend(SmtpServer smtpServer, string from, string[] to, 
			string[] cc, string[] bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody)
		{
			try 
			{
				SendMail s = new SendMail(smtpServer, from, to, 
					cc, bcc, replyTo, returnPath, 
					subject, textBody, htmlBody);
				Thread t = new Thread(new ThreadStart(s.AsyncSend));
				t.Start();
			}
			catch (ThreadAbortException)
			{
				// Do nothing on thread abort.
			} catch(System.Exception ex) {
				string s = ex.Message;
			}
		}


		/// <summary>
		/// Send mail asynchronously.
		/// </summary>
		/// <param name="smtpServerString"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="cc"></param>
		/// <param name="bcc"></param>
		/// <param name="replyTo"></param>
		/// <param name="returnPath"></param>
		/// <param name="subject"></param>
		/// <param name="textBody"></param>
		/// <param name="htmlBody"></param>
		public static void AsyncSend(string smtpServerString, string from, string[] to, 
			string[] cc, string[] bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody)
		{
			try 
			{
				SendMail s = new SendMail(smtpServerString, from, to, 
					cc, bcc, replyTo, returnPath, 
					subject, textBody, htmlBody);
				Thread t = new Thread(new ThreadStart(s.AsyncSend));
				t.Start();
			}
			catch (ThreadAbortException)
			{
				// Do nothing on thread abort.
			} catch(System.Exception ex) {
				string s = ex.Message;
			}
		}

		public void AsyncSend() 
		{
			SendMail.Send(_smtpServer, _from, _to, _cc, _bcc, _replyTo, _returnPath, _subject, _textBody, _htmlBody);
		}

		/// <summary>
		/// Send mail by SMTP.
		/// </summary>
		/// <param name="smtpServerString">SmtpServer connection string in URI format "user:password@host:port". 
		/// An empty string will default to "localhost:25".</param>
		/// <param name="from">
		/// Sender address using the following format: "Name" &lt;name@example.com&gt;
		/// </param>
		/// <param name="to">
		/// Recipient address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="cc">
		/// CC recipient address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="bcc">
		/// BCC recipient address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="replyTo">
		/// Reply back address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="returnPath">
		/// Bounce mail address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="subject">Mail subject.</param>
		/// <param name="textBody">Email body in plain text.</param>
		/// <param name="htmlBody">Email body in HTML.</param>
		/// <remarks>
		/// The from, to, subject and textBody or htmlBody parameters must be set to send mail.
		/// The other parameters can be set to an empty string "".
		/// 
		/// You are not required to specify the name for an address. 
		/// For example: name@example.com and &lt;name@example.com&gt; are functionally equivalent to "" &lt;name@example.com&gt;.
		/// 
		/// The Return-Path may not work depending on the Smtp server employed. Certain Smtp servers
		/// will automatically overwrite the Return-path with the From header information. The Return-Path class
		/// has been known to work on Sendmail, Postfix and IIS.
		/// </remarks>
		public static void Send(string smtpServerString, string from, string to, 
			string cc, string bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody) 
		{
			// Send email
			Send(new SmtpServer(smtpServerString), from, to, cc, bcc, replyTo, returnPath, subject, textBody, htmlBody);
		}

		/// <summary>
		/// Send mail by SMTP.
		/// </summary>
		/// <param name="smtpServerString">SmtpServer connection string in URI format "user:password@host:port". 
		/// An empty string will default to "localhost:25".</param>
		/// <param name="from">
		/// Sender address using the following format: "Name" &lt;name@example.com&gt;
		/// </param>
		/// <param name="to">
		/// Array of recipient address using the following format: "Name" &lt;name@example.com&gt;.
		/// </param>
		/// <param name="cc">
		/// Array of CC recipient address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="bcc">
		/// Array of BCC recipient address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="replyTo">
		/// Reply back address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="returnPath">
		/// Bounce mail address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="subject">Mail subject.</param>
		/// <param name="textBody">Email body in plain text.</param>
		/// <param name="htmlBody">Email body in HTML.</param>
		/// <remarks>
		/// The from, to, subject and textBody or htmlBody parameters must be set to send mail.
		/// The other parameters can be set to an empty string "".
		/// 
		/// You are not required to specify the name for an address. 
		/// For example: name@example.com and &lt;name@example.com&gt; are functionally equivalent to "" &lt;name@example.com&gt;.
		/// 
		/// The Return-Path may not work depending on the Smtp server employed. Certain Smtp servers
		/// will automatically overwrite the Return-path with the From header information. The Return-Path class
		/// has been known to work on Sendmail, Postfix and IIS.
		/// </remarks>
		public static void Send(string smtpServerString, string from, string[] to, 
			string[] cc, string[] bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody) 
		{		
			// Send email
			Send(new SmtpServer(smtpServerString), from, to, cc, bcc, replyTo, returnPath, subject, textBody, htmlBody);
		}

		/// <summary>
		/// Send mail by SMTP.
		/// </summary>
		/// <param name="smtpServer">SmtpServer connection info object.</param>
		/// <param name="from">
		/// Sender address using the following format: "Name" &lt;name@example.com&gt;
		/// </param>
		/// <param name="to">
		/// Recipient address using the following format: "Name" &lt;name@example.com&gt;. 
		/// Multiple addresses can be specified by separating each address 
		/// with a comma: "Name1" &lt;name1@example.com&gt;, "Name2" &lt;name2@example.com&gt;
		/// </param>
		/// <param name="cc">
		/// CC recipient address using the following format: "Name" &lt;name@example.com&gt;. 
		/// Multiple addresses can be specified by separating each address 
		/// with a comma: "Name1" &lt;name1@example.com&gt;, "Name2" &lt;name2@example.com&gt;
		/// </param>
		/// <param name="bcc">
		/// BCC recipient address using the following format: "Name" &lt;name@example.com&gt;. 
		/// Multiple addresses can be specified by separating each address 
		/// with a comma: "Name1" &lt;name1@example.com&gt;, "Name2" &lt;name2@example.com&gt;
		/// </param>
		/// <param name="replyTo">
		/// Reply back address using the following format: "Name" &lt;name@example.com&gt;. 
		/// Multiple addresses can be specified by separating each address 
		/// with a comma: "Name1" &lt;name1@example.com&gt;, "Name2" &lt;name2@example.com&gt;
		/// </param>
		/// <param name="returnPath">
		/// Bounce mail address using the following format: "Name" &lt;name@example.com&gt;. 
		/// Multiple addresses can be specified by separating each address 
		/// with a comma: "Name1" &lt;name1@example.com&gt;, "Name2" &lt;name2@example.com&gt;.
		/// </param>
		/// <param name="subject">Mail subject.</param>
		/// <param name="textBody">Email body in plain text.</param>
		/// <param name="htmlBody">Email body in HTML.</param>
		/// <remarks>
		/// The from, to, subject and textBody or htmlBody parameters must be set to send mail.
		/// The other parameters can be set to an empty string "".
		/// 
		/// You are not required to specify the name for an address. 
		/// For example: name@example.com and &lt;name@example.com&gt; are functionally equivalent to "" &lt;name@example.com&gt;.
		/// 
		/// The Return-Path may not work depending on the Smtp server employed. Certain Smtp servers
		/// will automatically overwrite the Return-path with the From header information. The Return-Path class
		/// has been known to work on Sendmail, Postfix and IIS.
		/// </remarks>
		public static void Send(SmtpServer smtpServer, string from, string to, 
			string cc, string bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody) 
		{	
			// Send email
			Send(smtpServer, from, new String[] {to}, new String[] {cc}, new String[] {bcc}, 
				replyTo, returnPath, subject, textBody, htmlBody);
		}

		public static void Send(SmtpServer smtpServer, string from, string to, 
			string cc, string bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody, string filename) {	
			// Send email
			Send(smtpServer, from, new String[] {to}, new String[] {cc}, new String[] {bcc}, 
				replyTo, returnPath, subject, textBody, htmlBody, new String[] {filename});
		}


		/// <summary>
		/// Send mail by SMTP.
		/// </summary>
		/// <param name="smtpServer">SmtpServer object.</param>
		/// <param name="from">
		/// Sender address using the following format: "Name" &lt;name@example.com&gt;
		/// </param>
		/// <param name="to">
		/// Array of recipient address using the following format: "Name" &lt;name@example.com&gt;.
		/// </param>
		/// <param name="cc">
		/// Array of CC recipient address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="bcc">
		/// Array of BCC recipient address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="replyTo">
		/// Reply back address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="returnPath">
		/// Bounce mail address using the following format: "Name" &lt;name@example.com&gt;. 
		/// </param>
		/// <param name="subject">Mail subject.</param>
		/// <param name="textBody">Email body in plain text.</param>
		/// <param name="htmlBody">Email body in HTML.</param>
		/// <remarks>
		/// The from, to, subject and textBody or htmlBody parameters must be set to send mail.
		/// The other parameters can be set to an empty string "".
		/// 
		/// You are not required to specify the name for an address. 
		/// For example: name@example.com and &lt;name@example.com&gt; are functionally equivalent to "" &lt;name@example.com&gt;.
		/// 
		/// The Return-Path may not work depending on the Smtp server employed. Certain Smtp servers
		/// will automatically overwrite the Return-path with the From header information. The Return-Path class
		/// has been known to work on Sendmail, Postfix and IIS.
		/// </remarks>
		public static void Send(SmtpServer smtpServer, string from, string[] to, 
			string[] cc, string[] bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody) {		
			Send(smtpServer, from, to, cc, bcc, replyTo, returnPath, subject,
				textBody, htmlBody, null);
			/*
			// Initialize email addresses
			EmailAddress fromEmail = CreateEmailAddress(from);
			EmailAddressCollection toEmail = CreateEmailAddress(to);
						
			EmailAddressCollection ccEmail = null;
			if(cc != null)
				ccEmail = CreateEmailAddress(cc);
			
			EmailAddressCollection bccEmail = null;
			if(bcc != null)
				bccEmail = CreateEmailAddress(bcc);		
			EmailAddress replyToEmail = CreateEmailAddress(replyTo);
			EmailAddress returnPathEmail = CreateEmailAddress(returnPath);

			// Validate parameters
			if (textBody == "" && htmlBody == "") {
				throw new ArgumentException("Body cannot be empty.");
			}

			EmailMessage eMsg = new EmailMessage();

			// Set Smtp server
			DotNetOpenMail.SmtpServer server = new DotNetOpenMail.SmtpServer(smtpServer.Host, smtpServer.Port);
			if (smtpServer.Username != "")
				server.SmtpAuthToken = new DotNetOpenMail.SmtpAuth.SmtpAuthToken(smtpServer.Username, smtpServer.Password);

			// Set connection timeout in milliseconds
			server.ServerTimeout = smtpServer.ConnectionTimeout * 1000;			

			// Set Return-path header for bounce mail
			if (returnPathEmail != null) {
				eMsg.AddCustomHeader("Return-Path", "\"" + returnPathEmail.Name + "\" <" + returnPathEmail.Email + ">");
				eMsg.EnvelopeFromAddress = returnPathEmail;
			}

			// Set the From header
			eMsg.FromAddress = fromEmail;

			// Set the To header
			if (toEmail != null && toEmail.Count > 0)
				eMsg.ToAddresses.AddCollection(toEmail);
			
			// Set the CC header
			if (ccEmail != null && ccEmail.Count > 0)
				eMsg.CcAddresses.AddCollection(ccEmail);

			// Set the BCC header
			if (bccEmail != null && bccEmail.Count > 0)
				eMsg.BccAddresses.AddCollection(bccEmail);

			// Set the ReplyTo header
			if (replyToEmail != null)
				eMsg.AddCustomHeader("Reply-To", "\"" + replyToEmail.Name + "\" <" + replyToEmail.Email + ">");

			// Set the Subject header
			eMsg.Subject = subject;

			// Set the Text Body
			if (textBody != "") {
				eMsg.TextPart = new TextAttachment(textBody);
				eMsg.TextPart.Encoding = DotNetOpenMail.Encoding.EncodingType.QuotedPrintable;
			}

			// Set the Html Body
			if (htmlBody != "") {
				eMsg.HtmlPart = new HtmlAttachment(htmlBody);
				eMsg.HtmlPart.Encoding = DotNetOpenMail.Encoding.EncodingType.QuotedPrintable;
			}

			// Send email
			eMsg.Send(server);
			*/
		}

		public static void Send(SmtpServer smtpServer, string from, string[] to, 
			string[] cc, string[] bcc, string replyTo, string returnPath, 
			string subject,	string textBody, string htmlBody, string []filenames) {	
			
			// Initialize email addresses
			EmailAddress fromEmail = CreateEmailAddress(from);
			EmailAddressCollection toEmail = CreateEmailAddress(to);
						
			EmailAddressCollection ccEmail = null;
			if(cc != null)
				ccEmail = CreateEmailAddress(cc);
			
			EmailAddressCollection bccEmail = null;
			if(bcc != null)
				bccEmail = CreateEmailAddress(bcc);		
			EmailAddress replyToEmail = CreateEmailAddress(replyTo);
			EmailAddress returnPathEmail = CreateEmailAddress(returnPath);

			// Validate parameters
			if (textBody == "" && htmlBody == "") {
				throw new ArgumentException("Body cannot be empty.");
			}

			EmailMessage eMsg = new EmailMessage();

			// Set Smtp server
			DotNetOpenMail.SmtpServer server = new DotNetOpenMail.SmtpServer(smtpServer.Host, smtpServer.Port);
			if (smtpServer.Username != "")
				server.SmtpAuthToken = new DotNetOpenMail.SmtpAuth.SmtpAuthToken(smtpServer.Username, smtpServer.Password);

			// Set connection timeout in milliseconds
			server.ServerTimeout = smtpServer.ConnectionTimeout * 1000;			

			// Set Return-path header for bounce mail
			if (returnPathEmail != null) {
				eMsg.AddCustomHeader("Return-Path", "\"" + returnPathEmail.Name + "\" <" + returnPathEmail.Email + ">");
				eMsg.EnvelopeFromAddress = returnPathEmail;
			}

			// Set the From header
			eMsg.FromAddress = fromEmail;

			// Set the To header
			if (toEmail != null && toEmail.Count > 0)
				eMsg.ToAddresses.AddCollection(toEmail);
			
			// Set the CC header
			if (ccEmail != null && ccEmail.Count > 0)
				eMsg.CcAddresses.AddCollection(ccEmail);

			// Set the BCC header
			if (bccEmail != null && bccEmail.Count > 0)
				eMsg.BccAddresses.AddCollection(bccEmail);

			// Set the ReplyTo header
			if (replyToEmail != null)
				eMsg.AddCustomHeader("Reply-To", "\"" + replyToEmail.Name + "\" <" + replyToEmail.Email + ">");

			// Set the Subject header
			eMsg.Subject = subject;

			// Set the Text Body
			if (textBody != "") {
				eMsg.TextPart = new TextAttachment(textBody);
				eMsg.TextPart.Encoding = DotNetOpenMail.Encoding.EncodingType.QuotedPrintable;
			}

			// Set the Html Body
			if (htmlBody != "") {
				eMsg.HtmlPart = new HtmlAttachment(htmlBody);
				eMsg.HtmlPart.Encoding = DotNetOpenMail.Encoding.EncodingType.QuotedPrintable;
			}

			if(filenames != null) {
				foreach(string filename in filenames) {
					// add the file in attachment
					DotNetOpenMail.FileAttachment attachment =
						new DotNetOpenMail.FileAttachment(new System.IO.FileInfo(filename));
					System.IO.FileInfo fi = new System.IO.FileInfo(filename);
					attachment.FileName = fi.Name;
					attachment.CharSet = System.Text.Encoding.ASCII;
					attachment.ContentType = "text/plain";
					
					//eMsg.AddRelatedAttachment(attachment);
					eMsg.AddMixedAttachment(attachment);
					//eMsg.AddRelatedAttachment(new DotNetOpenMail.FileAttachment(new System.IO.FileInfo(filename)));
				}
			}

			// Send email
			eMsg.Send(server);
		}

		/// <summary>
		/// Extract the name and email parts from: "Name" &lt;email&gt;
		/// </summary>
		/// <param name="nameAddress">The name address pair in the form of "name" &lt;email&gt;</param>
		/// <returns>EmailAddress object.</returns>
		private static EmailAddress CreateEmailAddress(string nameAddress) {

			if (nameAddress == null || nameAddress == "" || nameAddress == "\"\" <>")
				return null;

			string name = "";
			string address = "";

			// nameAddress may be formatted as:
			// "name" <email>
			// <email> 
			// email
			string[] nameAddressPair = nameAddress.Split('<');
			if (nameAddressPair.Length > 1)
			{
				name = nameAddressPair[0].Trim(" \"".ToCharArray());
				address = nameAddressPair[1].Trim(" <>\"".ToCharArray());
			}
			else
				address = nameAddressPair[0].Trim(" <>\"".ToCharArray());

			return new EmailAddress(address, name);
		}

		/// <summary>
		/// Create EmailAddressCollection from an array of name/email addresses.
		/// </summary>
		/// <param name="nameAddress">Name address array</param>
		/// <returns>EmailAddressCollection</returns>
		private static EmailAddressCollection CreateEmailAddress(string[] nameAddress) {

			EmailAddressCollection emailAddressList = new EmailAddressCollection();

			// Loop through each name/email address pair
			for (int i=0; i < nameAddress.Length; i++) {

				EmailAddress ea = CreateEmailAddress(nameAddress[i]);
				if (ea != null)
					emailAddressList.Add(ea);
			}
			return emailAddressList;
		}


		/// <summary>
		/// Format the name and email into a valid string ready for mailing.
		/// </summary>
		/// <param name="name">Name</param>
		/// <param name="email">Email address</param>
		/// <returns>A string formatted as "name" &lt;email&gt;</returns>
		public static string FormatAddress(string name, string email)
		{
			return "\"" + name + "\" <" + email + ">";
		}
	}
}
