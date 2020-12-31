using System;
using System.Text;
using System.Web.Mail;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for CatalystDataMailMessage.
	/// </summary>
	public class CatalystDataMailMessage : MailMessage
	{
		private const string MESSAGE_FROM = "QSP_Fulfillment@rd.com";
		private const string CATALYST_WARNING_EMAIL_CONSTANT = "CatalystWarningEmail";
		private const string MESSAGE_SUBJECT = "MAGAZINE CATALYST DATA CHANGED";

		private StringBuilder stringBuilder = null;

		public CatalystDataMailMessage() : base()
		{
			this.BodyFormat = MailFormat.Html;

			this.From = MESSAGE_FROM;
			this.To = System.Configuration.ConfigurationSettings.AppSettings[CATALYST_WARNING_EMAIL_CONSTANT];
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

		public virtual void AddFulfillmentHouse(CatalystDataFulfillmentHouse catalystDataFulfillmentHouse) 
		{
			if(catalystDataFulfillmentHouse.InitialName == catalystDataFulfillmentHouse.EnteredName) 
			{
				StringBuilder.Append("<tr><td><b>Fulfillment House Name: </b></td><td>" + catalystDataFulfillmentHouse.EnteredName + "</td></tr>");
			} 
			else 
			{
				StringBuilder.Append("<tr><td><b>Initial Fulfillment House Name: </b></td><td>" + catalystDataFulfillmentHouse.InitialName + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Fulfillment House Name: </b></td><td>" + catalystDataFulfillmentHouse.EnteredName + "</td></tr>");
			}

			StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");

			if(catalystDataFulfillmentHouse.InitialAddress1 != catalystDataFulfillmentHouse.EnteredAddress1) 
			{
				StringBuilder.Append("<tr><td><b>Initial Address Line 1: </b></td><td>" + catalystDataFulfillmentHouse.InitialAddress1 + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Address Line 1: </b></td><td>" + catalystDataFulfillmentHouse.EnteredAddress1 + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(catalystDataFulfillmentHouse.InitialAddress2 != catalystDataFulfillmentHouse.EnteredAddress2) 
			{
				StringBuilder.Append("<tr><td><b>Initial Address Line 2: </b></td><td>" + catalystDataFulfillmentHouse.InitialAddress2 + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Address Line 2: </b></td><td>" + catalystDataFulfillmentHouse.EnteredAddress2 + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(catalystDataFulfillmentHouse.InitialCity != catalystDataFulfillmentHouse.EnteredCity) 
			{
				StringBuilder.Append("<tr><td><b>Initial City: </b></td><td>" + catalystDataFulfillmentHouse.InitialCity + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New City: </b></td><td>" + catalystDataFulfillmentHouse.EnteredCity + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(catalystDataFulfillmentHouse.InitialStateProvince != catalystDataFulfillmentHouse.EnteredStateProvince) 
			{
				StringBuilder.Append("<tr><td><b>Initial State / Province: </b></td><td>" + catalystDataFulfillmentHouse.InitialStateProvince + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New State / Province: </b></td><td>" + catalystDataFulfillmentHouse.EnteredStateProvince + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(catalystDataFulfillmentHouse.InitialZip != catalystDataFulfillmentHouse.EnteredZip) 
			{
				StringBuilder.Append("<tr><td><b>Initial Zip / Postal Code: </b></td><td>" + catalystDataFulfillmentHouse.InitialZip + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Zip / Postal Code: </b></td><td>" + catalystDataFulfillmentHouse.EnteredZip + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			StringBuilder.Append("<tr><td colspan=\"2\"><hr size=\"2\"></td></tr>");
			StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
		}

		public virtual void AddProductCollection(CatalystDataProductCollection catalystDataProductCollection) 
		{
			foreach(CatalystDataProduct catalystDataProduct in catalystDataProductCollection) 
			{
				AddProduct(catalystDataProduct);
			}
		}

		public virtual void AddProduct(CatalystDataProduct catalystDataProduct) 
		{
			StringBuilder.Append("<tr><td width=\"250px\"><b>Title Code: </b></td><td>" + catalystDataProduct.ProductCode + "</td></tr>");
			StringBuilder.Append("<tr><td><b>Year: </b></td><td>" + catalystDataProduct.Year.ToString() + "</td></tr>");
			StringBuilder.Append("<tr><td><b>Season: </b></td><td>" + catalystDataProduct.Season + "</td></tr>");

			if(catalystDataProduct.InitialProductSortName == catalystDataProduct.EnteredProductSortName) 
			{
				StringBuilder.Append("<tr><td><b>Title Name: </b></td><td>" + catalystDataProduct.EnteredProductSortName + "</td></tr>");
			}

			StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");

			if(catalystDataProduct.InitialRemitCode != catalystDataProduct.EnteredRemitCode) 
			{
				StringBuilder.Append("<tr><td><b>Initial UMC Code: </b></td><td>" + catalystDataProduct.InitialRemitCode + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New UMC Code: </b></td><td>" + catalystDataProduct.EnteredRemitCode + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(catalystDataProduct.InitialProductSortName != catalystDataProduct.EnteredProductSortName) 
			{
				StringBuilder.Append("<tr><td><b>Initial Title Name: </b></td><td>" + catalystDataProduct.InitialProductSortName + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Title Name: </b></td><td>" + catalystDataProduct.EnteredProductSortName + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(catalystDataProduct.InitialVendorNumber != catalystDataProduct.EnteredVendorNumber) 
			{
				StringBuilder.Append("<tr><td><b>Initial Vendor Number: </b></td><td>" + catalystDataProduct.InitialVendorNumber + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Vendor Number: </b></td><td>" + catalystDataProduct.EnteredVendorNumber + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(catalystDataProduct.InitialVendorSiteName != catalystDataProduct.EnteredVendorSiteName) 
			{
				StringBuilder.Append("<tr><td><b>Initial Vendor Site Name: </b></td><td>" + catalystDataProduct.InitialVendorSiteName + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Vendor Site Name: </b></td><td>" + catalystDataProduct.EnteredVendorSiteName + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			if(catalystDataProduct.InitialPayGroupLookUpCode != catalystDataProduct.EnteredPayGroupLookUpCode) 
			{
				StringBuilder.Append("<tr><td><b>Initial Pay Group Look Up Code: </b></td><td>" + catalystDataProduct.InitialPayGroupLookUpCode + "</td></tr>");
				StringBuilder.Append("<tr><td><b>New Pay Group Look Up Code: </b></td><td>" + catalystDataProduct.EnteredPayGroupLookUpCode + "</td></tr>");
				StringBuilder.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
			}

			StringBuilder.Append("<tr><td colspan=\"2\"><hr></td></tr>");
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
