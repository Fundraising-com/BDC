using System;
using System.Text;
using System.Web.Mail;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for CatalystDataMailMessage.
	/// </summary>
	public class AddressChangeMailMessage : MailMessage
	{
		private const string MESSAGE_FROM = "QSP_Fulfillment@rd.com";
		private const string ADDRESS_WARNING_EMAIL_CONSTANT = "AddressChangeWarningEmail";
		private const string MESSAGE_SUBJECT = "ACCOUNT ADDRESS INFO CHANGED";

		private StringBuilder stringBuilder = null;

		public AddressChangeMailMessage() : base()
		{
			this.BodyFormat = MailFormat.Html;

			this.From = MESSAGE_FROM;
			this.To = System.Configuration.ConfigurationSettings.AppSettings[ADDRESS_WARNING_EMAIL_CONSTANT];
			this.Bcc = QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ErrorWebTo;

			this.Subject = MESSAGE_SUBJECT;

			StringBuilder.Append("<table>");
		}

		protected StringBuilder StringBuilder 
		{
			get 
			{
				if(stringBuilder == null) 
				{
					stringBuilder = new StringBuilder();
				}

				return stringBuilder;
			}
		}

		public virtual void AddChangedAddress(AddressChangedDetail addressDetail) 
		{
			if(addressDetail.InitialAcc == addressDetail.EnteredAcc) 
			{
				StringBuilder.Append("<tr><td><b>" + addressDetail.EnteredAcc + "</b></td></tr>");
			} 
		
			StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			StringBuilder.Append("<tr><td><b>Vendor Number: </b></td><td>" + addressDetail.VendorNumber + "</td></tr>");
			StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			StringBuilder.Append("<tr><td><b>Address Type: </b></td><td>" + addressDetail.AddressTypeDesc + "</td></tr>");
			StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");

			if(addressDetail.InitialAddress1 != addressDetail.EnteredAddress1) 
			{
				StringBuilder.Append("<tr><td><b>Initial Address Line 1: </b></td><td>" + addressDetail.InitialAddress1 + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Address Line 1: </b></td><td>" + addressDetail.EnteredAddress1 + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(addressDetail.InitialAddress2 != addressDetail.EnteredAddress2) 
			{
				StringBuilder.Append("<tr><td><b>Initial Address Line 2: </b></td><td>" + addressDetail.InitialAddress2 + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Address Line 2: </b></td><td>" + addressDetail.EnteredAddress2 + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(addressDetail.InitialCity != addressDetail.EnteredCity) 
			{
				StringBuilder.Append("<tr><td><b>Initial City: </b></td><td>" + addressDetail.InitialCity + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New City: </b></td><td>" + addressDetail.EnteredCity + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(addressDetail.InitialStateProvince != addressDetail.EnteredStateProvince) 
			{
				StringBuilder.Append("<tr><td><b>Initial State / Province: </b></td><td>" + addressDetail.InitialStateProvince + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New State / Province: </b></td><td>" + addressDetail.EnteredStateProvince + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(addressDetail.InitialPostal != addressDetail.EnteredPostal) 
			{
				StringBuilder.Append("<tr><td><b>Initial Zip / Postal Code: </b></td><td>" + addressDetail.InitialPostal + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Zip / Postal Code: </b></td><td>" + addressDetail.EnteredPostal + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			StringBuilder.Append("<tr><td colspan=\"2\"><hr size=\"2\"></td></tr>");
			StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
		}

		public virtual void Send() 
		{
			StringBuilder.Append("</table>");
			this.Body = StringBuilder.ToString();

			SmtpMail.SmtpServer = QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ErrorWebSmtp;

			System.Web.Mail.SmtpMail.Send(this);
		}
	}
}
