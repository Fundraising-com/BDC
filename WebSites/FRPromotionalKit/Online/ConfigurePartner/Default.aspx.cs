using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using eSubs = efundraising.ESubsGlobal;
using efundraising.ESubsGlobal.Touch;
using efundraising.EFundraisingCRMWeb.Components.Server;
using efundraising.EFundraisingCRMWeb.Components.Server.Partner;

namespace efundraising.EFundraisingCRMWeb.Online.ConfigurePartner
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : EFundraisingCrmOnlineBasePage, IPage, INoQuickToolBar
	{

		protected efundraising.EFundraisingCRMWeb.Components.User.Partner.eSubsPartner ESubsPartner1;
		protected efundraising.EFundraisingCRMWeb.Components.User.PaymentInformation.PaymentInfoSummary PaymentInfoSummary1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				BindControls();				
			}
		}

		private void BindControls()
		{
			// Fill the partner drop down list
			eSubs.PartnerCollections partners = eSubs.Partner.GetPartnerCollections();
			partners.SortByType(eSubs.PartnerComparable.Name);
			PartnersDropDownList.DataSource = partners;
			PartnersDropDownList.DataBind();

			// Fill the Email Template lists
			EmailTemplateList[] eList = EmailTemplateList.GetList();

			if (eList !=  null)
			{

				// Build our data table for binding
				DataTable dt = new DataTable();
				dt.Columns.Add("email_template_id");
				dt.Columns.Add("template_name");
			
				for (int i = eList.Length - 1; i >= 0 ; i--)
				{
					DataRow drow = dt.NewRow();
					drow["email_template_id"] = eList[i].TemplateId;
					drow["template_name"] = eList[i].TemplateName;
					dt.Rows.Add(drow);
				}
					
				// Bind
				ESubsPartner1.GetLetterDefaultDropDownList().DataSource = dt;
				ESubsPartner1.GetLetterDefaultDropDownList().DataBind();
				ESubsPartner1.GetLetterFirstDropDownList().DataSource = dt;
				ESubsPartner1.GetLetterFirstDropDownList().DataBind();
			}

			FillControls(int.Parse(PartnersDropDownList.SelectedValue));
		}

		private void FillControls(int partnerId)
		{
			eSubsPartnerConfiguration partner = LoadPartner(partnerId);
			if (partner != null)
			{
				ESubsPartner1.SetNameLabel(partner.Partner.Name);
				ESubsPartner1.SetPartnerTypeLabel(partner.PartnerType);
				ESubsPartner1.SetTypeLabel(partner.Type);
				ESubsPartner1.SetProfitTextBox(partner.PaymentConfig.ProfitPercentage.ToString());
				try
				{
					ESubsPartner1.SetAccountNoTextBox(partner.StoreTemplate.AccountNumber.ToString());
				}
				catch
				{
					ESubsPartner1.SetAccountNoTextBox("No Store");
				}
				ESubsPartner1.SetLetterDefaultDropDownListSelectedValue(partner.PaymentConfig.EmailTemplateId.ToString());
				ESubsPartner1.SetLetterFirstDropDownListSelectedValue(partner.PaymentConfig.FirstEmailTemplateId.ToString());
				ESubsPartner1.SetView1HyperLinkNavigateUrl("~/Online/EmailTemplate/Preview.aspx?" + UrlParam.UrlKeyEmailTemplateID + "=" + ESubsPartner1.GetLetterDefaultDropDownListSelectedValue());
				ESubsPartner1.SetView2HyperLinkNavigateUrl("~/Online/EmailTemplate/Preview.aspx?" + UrlParam.UrlKeyEmailTemplateID + "=" + ESubsPartner1.GetLetterFirstDropDownListSelectedValue());
				ESubsPartner1.SetPaymentToDropDownListSelectedValue(partner.PaymentConfig.PaymentTo.ToString());
				if (partner.PaymentConfig.PaymentTo == 1)
				{
					ESubsPartner1.SetPartnerPaymentInfoIdTextBox(partner.PaymentConfig.PartnerPaymentInfoId.ToString());

					try
					{
						ESubsGlobal.PartnerPaymentInfo ppInfo = ESubsGlobal.PartnerPaymentInfo.GetActivePartnerPaymentInfoByPartnerID(partner.Partner.PartnerID);
						if (ppInfo == null)
							throw new Exception(string.Format("Partner has not partner payment info {0}", partner.Partner.PartnerID));


						ESubsGlobal.Payment.PaymentInfo paymentInfo = ESubsGlobal.Payment.PaymentInfo.GetPaymentInfoByID(ppInfo.PaymentInfoId);
						PaymentInfoSummary1.SetAddress1Label(paymentInfo.PostalAddress.Address1);
						PaymentInfoSummary1.SetAddress2Label(paymentInfo.PostalAddress.Address2);
						PaymentInfoSummary1.SetAttOfLabel(paymentInfo.PaymentName);
						PaymentInfoSummary1.SetCityLabel(paymentInfo.PostalAddress.City);
						PaymentInfoSummary1.SetCountryLabel(paymentInfo.PostalAddress.CountryCode.Code);
						PaymentInfoSummary1.SetStateLabel(paymentInfo.PostalAddress.SubDivisionCode.Substring(paymentInfo.PostalAddress.SubDivisionCode.Length - 2, 2));
						PaymentInfoSummary1.SetZipLabel(paymentInfo.PostalAddress.ZipCode);

						PaymentInfoPanel.Visible = true;
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
				else
				{
					PaymentInfoPanel.Visible = false;
					ESubsPartner1.SetPartnerPaymentInfoIdTextBox("");
				}
			}
		}

		private eSubsPartnerConfiguration LoadPartner(int partnerId) 
		{
			eSubsPartnerConfiguration partner = 
				(eSubsPartnerConfiguration) Components.Server.CurrentWorkingObject.Get(Session, "ESUBS_PARTNER");

			if(partner == null || !IsPostBack) 
			{
				// instanciate the object
				partner = new eSubsPartnerConfiguration(partnerId);				

				// save it for futher usage to the current working object
				Components.Server.CurrentWorkingObject.Save(partner, Session, "ESUBS_PARTNER", null);
			}
			else
			{
				if (partner.Partner.PartnerID != partnerId)
				{
					// instanciate the object
					partner = new eSubsPartnerConfiguration(partnerId);				

					// save it for futher usage to the current working object
					Components.Server.CurrentWorkingObject.Save(partner, Session, "ESUBS_PARTNER", null);
				}
			}

			return partner;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			this.ESubsPartner1.list1Changed += new EventHandler(ESubsPartner1_list1Changed);
			this.ESubsPartner1.list2Changed += new EventHandler(ESubsPartner1_list2Changed);
			this.ESubsPartner1.listPaymentToChanged += new EventHandler(ESubsPartner1_listPaymentToChanged);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void PartnersDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillControls(int.Parse(PartnersDropDownList.SelectedValue));
		}

		private void ESubsPartner1_list1Changed(object sender, EventArgs e)
		{
			ESubsPartner1.SetView1HyperLinkNavigateUrl("~/Online/EmailTemplate/Preview.aspx?" + UrlParam.UrlKeyEmailTemplateID + "=" + ESubsPartner1.GetLetterDefaultDropDownListSelectedValue());
		}

		private void ESubsPartner1_list2Changed(object sender, EventArgs e)
		{
			ESubsPartner1.SetView2HyperLinkNavigateUrl("~/Online/EmailTemplate/Preview.aspx?" + UrlParam.UrlKeyEmailTemplateID + "=" + ESubsPartner1.GetLetterFirstDropDownListSelectedValue());
		}

		private void ESubsPartner1_listPaymentToChanged(object sender, EventArgs e)
		{
			if (int.Parse(ESubsPartner1.GetPaymentToDropDownListSelectedValue()) == (int)ESubsGlobal.Payment.PartnerPaymentConfig.PaymentToList.Group)
				PaymentInfoPanel.Visible = false;
		}

		protected void SaveButton_Click(object sender, System.EventArgs e)
		{
			eSubsPartnerConfiguration partner = LoadPartner(int.Parse(PartnersDropDownList.SelectedValue));
			if (partner != null)
			{
				if (partner.PaymentConfig.PartnerId != partner.Partner.PartnerID)
				{
					ESubsGlobal.Payment.PartnerPaymentConfig newPaymentConfig = new efundraising.ESubsGlobal.Payment.PartnerPaymentConfig();
					
					// create a partner payment config for the current partner
					newPaymentConfig.PartnerId = partner.Partner.PartnerID;
					newPaymentConfig.ProfitPercentage = int.Parse(ESubsPartner1.GetProfitTextBox());
					newPaymentConfig.PaymentTo = int.Parse(ESubsPartner1.GetPaymentToDropDownListSelectedValue());
					if (newPaymentConfig.PaymentTo == (int)ESubsGlobal.Payment.PartnerPaymentConfig.PaymentToList.Partner)
						newPaymentConfig.PartnerPaymentInfoId= int.Parse(ESubsPartner1.GetPartnerPaymentInfoIdTextBox());
					else
						newPaymentConfig.PartnerPaymentInfoId = int.MinValue;
					newPaymentConfig.EmailTemplateId = int.Parse(ESubsPartner1.GetLetterDefaultDropDownListSelectedValue());
					newPaymentConfig.FirstEmailTemplateId = int.Parse(ESubsPartner1.GetLetterFirstDropDownListSelectedValue());
					newPaymentConfig.IsDefault = false;

					newPaymentConfig.Insert();
				}
				else
				{
					// update the current Partner Payment Config
					partner.PaymentConfig.ProfitPercentage = int.Parse(ESubsPartner1.GetProfitTextBox());
					partner.PaymentConfig.PaymentTo = int.Parse(ESubsPartner1.GetPaymentToDropDownListSelectedValue());
					if (partner.PaymentConfig.PaymentTo == (int)ESubsGlobal.Payment.PartnerPaymentConfig.PaymentToList.Partner)
						partner.PaymentConfig.PartnerPaymentInfoId = int.Parse(ESubsPartner1.GetPartnerPaymentInfoIdTextBox());
					else
						partner.PaymentConfig.PartnerPaymentInfoId = int.MinValue;
					partner.PaymentConfig.EmailTemplateId = int.Parse(ESubsPartner1.GetLetterDefaultDropDownListSelectedValue());
					partner.PaymentConfig.FirstEmailTemplateId = int.Parse(ESubsPartner1.GetLetterFirstDropDownListSelectedValue());					

					partner.PaymentConfig.Update();
				}

				Components.Server.CurrentWorkingObject.Remove(Session, "ESUBS_PARTNER");
				FillControls(partner.Partner.PartnerID);

				Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('Changes saved!');</script>");
			}
		}

		protected void CancelButton_Click(object sender, System.EventArgs e)
		{
			FillControls(int.Parse(PartnersDropDownList.SelectedValue));
		}

		#region IPage Members

		public string PageInformation {
			get {
				return "Configure Partner for Online Fundraising";
			}
		}

		public string PageDescription {
			get {
				return "Set profit, group types, etc.";
			}
		}

		#endregion

	}
}
