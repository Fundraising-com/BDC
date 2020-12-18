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

using GA.BDC.Core.Email;
using GA.BDC.Core.efundraisingCore;
using GA.BDC.Core.efundraisingCore.FormObject;
using GA.BDC.Core.Web.UI.UIControls;
using GA.BDC.Core.EnterpriseComponents;

//using Business.com.ses.ws.AddressHygiene;

namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Sections
{	
	using efundCRM= GA.BDC.Core.EFundraisingCRM;

	public class SampleKitShort : System.Web.UI.UserControl
	{

		protected System.Web.UI.WebControls.TextBox txtFirstName;
		protected System.Web.UI.WebControls.TextBox txtLastName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLastName;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.TextBox txtGroupName;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAddress;
		protected System.Web.UI.WebControls.TextBox txtCity;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCity;
		protected System.Web.UI.WebControls.DropDownList ddlCountry;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.WebControls.TextBox txtZipCode;
		protected System.Web.UI.WebControls.TextBox txtDayPhone1;
		protected System.Web.UI.WebControls.TextBox txtDayPhone2;
		protected System.Web.UI.WebControls.TextBox txtDayPhone3;
		protected System.Web.UI.WebControls.TextBox txtEvnPhone1;
		protected System.Web.UI.WebControls.TextBox txtEvnPhone2;
		protected System.Web.UI.WebControls.TextBox txtEvnPhone3;
		protected System.Web.UI.WebControls.DropDownList ddlBestTimeToCall;
		protected System.Web.UI.WebControls.TextBox txtGroupNumber;
		protected System.Web.UI.WebControls.TextBox txtDayPhnExt;
		protected System.Web.UI.WebControls.TextBox txtEvnPhnExt;
		protected System.Web.UI.WebControls.ValidationSummary vdsForm;
		protected System.Web.UI.WebControls.TextBox txtDayPhone;
		protected System.Web.UI.WebControls.TextBox txtEvnPhone;

		private const string __PLEASE_SELECT = "----- Please Select -----";
		protected System.Web.UI.WebControls.Label lblLastName;
		protected System.Web.UI.WebControls.Label lblEmailAddress;
		protected System.Web.UI.WebControls.Label lblGroupName;
		protected System.Web.UI.WebControls.Label lblAddress;
		protected System.Web.UI.WebControls.Label lblCity;
		protected System.Web.UI.WebControls.Label lblCountry;
		protected System.Web.UI.WebControls.Label lblState;
		protected System.Web.UI.WebControls.Label lblDayPhone;
		protected System.Web.UI.WebControls.Label lblEvnPhone;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGroupName;
		protected System.Web.UI.WebControls.CustomValidator rfvState;
		protected System.Web.UI.WebControls.RegularExpressionValidator rfvDayPhone;
		protected System.Web.UI.WebControls.RegularExpressionValidator rfvGroupNumber;
		protected System.Web.UI.WebControls.RegularExpressionValidator rfvEmail;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFirstName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGroupNumber2;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvZipCode;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEmailAddress2;
		protected System.Web.UI.WebControls.Label lblEmail;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEmail2;
		protected System.Web.UI.WebControls.Label lblGroupNumber;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDayPhone2;
		protected System.Web.UI.WebControls.CustomValidator rfvBestTimeToCall;
		protected System.Web.UI.WebControls.Label lblBestTimeToCall;
		protected System.Web.UI.WebControls.TextBox txtMonthList;
		protected System.Web.UI.WebControls.Label lblFirstName;
		protected ButtonPanelControl imgSubmitForm;
		protected System.Web.UI.WebControls.ValidationSummary Validationsummary1;
		
		protected string fullNameContainer = string.Empty;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl imgSendFreeKit;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected System.Web.UI.WebControls.Image Image1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl5;
		protected System.Web.UI.WebControls.Image Image2;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected string _QuestionCssStyle = "NormalText";
	
		// address validation
		public Components.User.AddressHygiene.AddressHygiene AddressHygiene1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList AddressZoneDropDownList;
		public Components.User.AddressHygiene.ExplicitAddressConfirmation ExplicitAddressConfirmation1;

		private void Page_Load(object sender, System.EventArgs e) 
		{					

			// set hander for the address hygiene controls
            //AddressHygiene1.OutputAddress += new efundraising.ScratchcardWeb.Components.User.AddressHygiene.OutputAddressEventHandler(AddressHygiene1_OutputAddress);
            //ExplicitAddressConfirmation1.OnConfirm += new EventHandler(ExplicitAddressConfirmation1_OnConfirm);

			// Hide AddressHygiene controls
            //AddressHygiene1.Visible = false;
            //ExplicitAddressConfirmation1.Visible = false;

			// GLOBALIZATION
			GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage gbp =
				(GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage)this.Page;
			gbp.Globalize(PagePanelControl1, this);
			// END GLOBALIZATION
			
			fullNameContainer = Components.Server.UIFactories.PanelUtilities.FindFullControlName(this);
			GA.BDC.Core.efundraisingCore.eFundEnv oEnv = null;
			
			if(!base.IsPostBack) 
			{
				
				oEnv = eFundEnv.Create();
				if(oEnv.PartnerInfo.PartnerID == -1)
					oEnv = eFundEnv.CreateByPartnerId(0);

				
				oEnv.MailConfigFile = Server.MapPath("EmailTemplate/EmailTemplate.xml");
				oEnv.CultureName = "en-US";

				oEnv.Save();

							
				try 
				{

					
					TimeToCallCollections timeToCall = TimeToCallCollections.Load();
					if(timeToCall[__PLEASE_SELECT] == null)
						timeToCall.Insert(0, new TimeToCall("0",__PLEASE_SELECT));
					this.ddlBestTimeToCall.DataSource = timeToCall; 
					this.ddlBestTimeToCall.DataBind();

                    GA.BDC.Core.efundraisingCore.Geography.CountryCollections countryList =
                        GA.BDC.Core.efundraisingCore.Geography.CountryCollections.Load();

					// select the default country depending on the site
					int oCtnIndex = 0;
					
					// Default is us
					string countryToSelect = "US";	
					// Partner 129 is Scratchcard.ca, so select Canada
					if (oEnv.PartnerInfo.PartnerID == 129)
						countryToSelect = "CA";


                    GA.BDC.Core.efundraisingCore.Geography.StateCollections statesList =
                        GA.BDC.Core.efundraisingCore.Geography.StateCollections.Create(countryToSelect);
					if(statesList[__PLEASE_SELECT] == null)
                        statesList.Insert(0, new GA.BDC.Core.efundraisingCore.Geography.State("0", __PLEASE_SELECT));
					
					this.ddlState.DataSource = statesList;
					this.ddlState.DataBind();
					
			
					for(int i=0;i<countryList.Count;i++) 
					{
						if(countryList[i].CountryCode.ToLower() == countryToSelect.ToLower())
							oCtnIndex = i;
					}
					this.ddlCountry.DataSource = countryList;
					this.ddlCountry.DataBind();

					this.ddlCountry.SelectedIndex = oCtnIndex;
					
					SetAddressZone(AddressZoneDropDownList);
					
				} 
				catch(Exception ex) 
				{
					LoggingSystem.LogError("Unable to load FreeFundraisingKit: " + ex.Message);

				}
			}			

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ddlCountry.SelectedIndexChanged += new System.EventHandler(this.ddlCountry_SelectedIndexChanged);
			this.imgSendFreeKit.Click += new TrackingButtonEventHandler(this.imgSendFreeKit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private DateTime GetFirstDate(int[] pSelectedMonthNumber) 
		{
			int oCurrentMonthNumber = DateTime.Now.Month;
			int oTheMonthNumber = -1;
			bool oNotSet = false, oContainLessThan = false, oContainGreaterThan = false;
			for(int i=0;i<pSelectedMonthNumber.Length;i++) 
			{
				if(pSelectedMonthNumber[i] < oCurrentMonthNumber && !oContainLessThan) 
					oContainLessThan = true;
				if(pSelectedMonthNumber[i] >= oCurrentMonthNumber && !oContainGreaterThan)
					oContainGreaterThan = true;
			}
			for(int i=0;i<pSelectedMonthNumber.Length;i++) 
			{
				if(pSelectedMonthNumber[i] != -1) 
				{					
					if(pSelectedMonthNumber[i] > 0) 
					{
						if(pSelectedMonthNumber[i] >= oCurrentMonthNumber && !oNotSet) 
						{
							oTheMonthNumber = pSelectedMonthNumber[i];
							oNotSet = true;
						}
						if(pSelectedMonthNumber[i] < oCurrentMonthNumber && !oContainGreaterThan && oContainLessThan && !oNotSet) 
						{
							oTheMonthNumber = pSelectedMonthNumber[i];
							oNotSet = true;
						}
					}
				}
			}
			return DateTime.Parse("01-" + oTheMonthNumber.ToString() + "-" + DateTime.Now.Year.ToString());
		}

		private void ddlCountry_SelectedIndexChanged(object sender, System.EventArgs e) 
		{
			try 
			{
				string oCountryCode = this.ddlCountry.SelectedValue;
				if(oCountryCode.ToLower() != "ca" && oCountryCode.ToLower() != "us")
					oCountryCode = "Unknown";

                GA.BDC.Core.efundraisingCore.Geography.StateCollections statesList =
                    GA.BDC.Core.efundraisingCore.Geography.StateCollections.Create(oCountryCode);


				this.ddlState.DataSource = statesList;
				this.ddlState.DataBind();
				GA.BDC.Core.efundraisingCore.eFundEnv oEnv = eFundEnv.Create();
				if(oCountryCode.ToLower() != "us")
					oEnv.CultureName = "en-CA";
				else
					oEnv.CultureName = "en-US";				
				oEnv.Save();
			} 
			catch(Exception ex) 
			{
				LoggingSystem.LogError(this.ToString(), ex.Message);
			}
		}

		private void rfvFirstName_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) 
		{
			if(base.Page.IsValid)
				Response.Write("page is valid");
			else
				Response.Write("page isn`t valid");
		}

		private void imgSendFreeKit_Click(object sender, System.EventArgs e) 
		{
			
			if(Session["ADDRESS_VALIDATED"] != null || this.Page.IsValid) 
			{

				bool postalAddressIsValidated = false;
                if (AddressHygiene1.IsAddressHygieneEnabled)
                {
					if(ddlCountry.SelectedValue == "US") {
						// fill out the address using the profit information
                        //Business.com.ses.ws.AddressHygiene.Address address = 
                        //    new Business.com.ses.ws.AddressHygiene.Address();

                        //address.Address1 = txtAddress.Text;
                        //address.City = txtCity.Text;
                        //address.Region = ddlState.SelectedValue;
                        //address.Country = "US";
                        //string zip1 = "";
                        //string zip2 = "";

                        //// cut out the zip/zip4
                        //string zip = txtZipCode.Text;
                        //if(zip.IndexOf(" ") > -1) {
                        //    string[] zipSlip = zip.Split(' ');
                        //    zip1 = zipSlip[0];
                        //    zip2 = zipSlip[1];
                        //}

                        //address.PostCode = (zip1.Trim() == ""? zip: zip1);
                        //address.PostCode2 = zip2;

						bool enableSuggestionList = true;

                        //if(Session["CONFIRM"] == null) {
                        //    if (AddressHygiene1.DoAddressHygiene(address, enableSuggestionList)) {
                        //        Business.com.ses.ws.AddressHygiene.OutputAddress outputAddress =
                        //            AddressHygiene1.OutAddress;

                        //        //Check if there was an error with the address that came back
                        //        if (outputAddress.Fault != Fault.NoError) {
                        //            ExplicitAddressConfirmation1.SetAddress(outputAddress.Address1 + "<br />" + outputAddress.City + ", " + outputAddress.Region + " " + "<br />" + outputAddress.PostCode + " " + outputAddress.PostCode2);
                        //            ExplicitAddressConfirmation1.Visible = true;
                        //            return;
                        //        }
                        //            //Check if there was a Suggestion List error
                        //        else if (outputAddress.SuggestionListInformation.Error!= SuggestionListError.None) {
                        //            ExplicitAddressConfirmation1.SetAddress(outputAddress.Address1 + "<br />" + outputAddress.City + ", " + outputAddress.Region + " " + "<br />" + outputAddress.PostCode + " " + outputAddress.PostCode2);
                        //            ExplicitAddressConfirmation1.Visible = true;
                        //            return;
                        //        } else {
                        //            postalAddressIsValidated = true;

                        //            string newStreet1 = outputAddress.Address1;
                        //            string newStreet2 = outputAddress.Address2;
                        //            string newCity = outputAddress.City;
                        //            string newCounty = outputAddress.County;
                        //            string newPostalCode = outputAddress.PostCode;
                        //            string newPostalCode2 = outputAddress.PostCode2;
                        //            string newProvince = outputAddress.Region;
                        //            string newCountry = outputAddress.Country;

                        //            txtAddress.Text = newStreet1 + " " + newStreet2;
                        //            txtCity.Text = newCity;
                        //            txtZipCode.Text = newPostalCode + " " + newPostalCode2;
									
                        //            ListItem item = ddlState.Items.FindByValue(newProvince);
                        //            if(item != null) {
                        //                for(int i=0;i<ddlState.Items.Count;i++) {
                        //                    ListItem li = (ListItem)ddlState.Items[i];
                        //                    li.Selected = false;
                        //                }
                        //                item.Selected = true;
                        //            }								
                        //        }
                        //    }
                        //}
					}
				}

				eFundEnv oEnv = eFundEnv.Create();
				
				//Create the lead here
				if(oEnv.PromotionID == -1)
					oEnv.PromotionID = oEnv.DefaultPromotionID;

				string comments = string.Empty;

				string oProgram = "Scratchcards";
		
				// Hack: Make sure the date is converted to en-US format because the stored_proc must convert from a string to a timestamp
				IFormatProvider dateCulture = new System.Globalization.CultureInfo("en-US", true);
				string correctDate = DateTime.Now.Date.ToString("d", dateCulture);
								
				comments += "SHORT FORM - " + oProgram + " " + "[Fundraising Date => " + correctDate + " ]";

				EfundraisingLead oNewLead = new EfundraisingLead(this.txtFirstName.Text, this.txtLastName.Text, this.txtEmail.Text,
					this.txtAddress.Text, this.txtCity.Text, this.ddlState.SelectedValue, this.txtZipCode.Text,
					this.ddlCountry.SelectedValue, this.txtGroupName.Text, correctDate, 
					this.txtDayPhone.Text, this.txtEvnPhone.Text,this.txtDayPhnExt.Text, this.txtEvnPhnExt.Text, 
					99, 99, 
					int.Parse(this.txtGroupNumber.Text), 0, oEnv.PromotionID, "Other", 
					this.ddlBestTimeToCall.SelectedValue, false, oProgram, false, comments, 1, 0);
				
				oNewLead.PartnerID = oEnv.PartnerInfo.PartnerID;
				oNewLead.PartnerName = oEnv.PartnerInfo.PartnerName;
				oNewLead.IsPostalAddressValidated = (postalAddressIsValidated? 1: 0);

				try 
				{
					oNewLead.Integrate();

					if(oNewLead.LeadID != int.MinValue && oNewLead.LeadID != -1) {
						try {
							// after inserting a lead, insert the promotional kit
							// this promotional kit has to be inserted directly 
							// to the efundraising prod database through sql link server
							// the reason why there are no sql transaction is that
							// our link servers does not handle transactions

							// findout which kit type to send depending of the
							// lead arguments
							GA.BDC.Core.EFundraisingCRM.KitType kitType =
                                GA.BDC.Core.EFundraisingCRM.KitType.GetProperKitTypeFromLeadInformation(
                                oNewLead.ConsultantID, GA.BDC.Core.EFundraisingCRM.LeadChannel.Internet.ChannelCode, 
								oNewLead.PromotionID, oNewLead.PartnerID,
								oNewLead.State, oNewLead.Country);
						
							// create a postal address object with lead information
                            GA.BDC.Core.EFundraisingCRM.PostalAddress postalAddress =
                                new GA.BDC.Core.EFundraisingCRM.PostalAddress(
								int.MinValue, oNewLead.StreetAddress, oNewLead.City, 
								oNewLead.ZipCode, oNewLead.Country, 
								oNewLead.Country + "-" + oNewLead.State, DateTime.Now);

							// insert the postal address, if it failed, log 
							// and continue the process (will insert an invalid
							// promotional kit with no postal address id)
							try {
								postalAddress.Insert();
							} catch(System.Exception ex) {
								GA.BDC.Core.Diagnostics.Logger.LogWarn("Sample Kit failed to insert postal address: Lead ID: " + oNewLead.LeadID, ex);
							}

							// create our promotional kit object 
							GA.BDC.Core.EFundraisingCRM.PromotionalKit promotionalKit =
								new GA.BDC.Core.EFundraisingCRM.PromotionalKit(
								int.MinValue, oNewLead.LeadID, oNewLead.LeadVisitID, kitType.KitTypeID,
								GA.BDC.Core.EFundraisingCRM.Carrier.RegularMail.CarrierId, int.MinValue,
								postalAddress.PostalAddressId, (postalAddress.PostalAddressId == int.MinValue? 0: 1), DateTime.Now, DateTime.MinValue);

							// insert the promotional kit
							promotionalKit.Insert();
						} catch(System.Exception ex) {
							// let it go anyway, the promotional kit manager service will insert it
							GA.BDC.Core.Diagnostics.Logger.LogWarn("Unable to insert promotional kit", ex);
						}
					}
				} 
				catch
				{
					// On error, always insert into temporary 
					// table to avoid losing lead
					TempLead tempLead = new TempLead(oNewLead);
					tempLead.Insert();
					
					throw;
				} 
				finally 
				{
					oEnv.LeadObject = oNewLead;
					oEnv.Save();

				}


                GA.BDC.Core.WebTracking.VisitorLog visitorLog = GA.BDC.Core.WebTracking.VisitorLog.Create(Session);
				visitorLog.UpdateVisitorLog(oNewLead.LeadID);

				Components.Server.EmailSender.SendMail oSnd = new Components.Server.EmailSender.SendMail();
				// oSnd.SendLeadConfirmation();
				oSnd.SendConfirmation();

				Response.Redirect("SampleKitConfirmation.aspx", false);			




			}
		}

        //private void AddressHygiene1_OutputAddress(object sender, OutputAddress outputAddress, bool addressChanged, int nAddress)
        //{
        //    Session["ADDRESS_VALIDATED"] = true;
        //    //imgSendFreeKit_Click(sender, null);
        //}

        //private void ExplicitAddressConfirmation1_OnConfirm(object sender, EventArgs e) {
        //    Session["CONFIRM"] = true;
        //    imgSendFreeKit_Click(sender, e);
        //}

        //private void ExplicitAddressConfirmation1_OnSaveWithoutChange(object sender, EventArgs e)
        //{
        //    Session["CONFIRM"] = "NOCHANGE";
        //}

        //private void ExplicitAddressConfirmation1_OnCancel(object sender, EventArgs e)
        //{

        //}

		private void SetAddressZone(DropDownList ddL)
		{
			if (ddL.Items.Count == 0)
			{
				efundCRM.AddressZone[] addZone = efundCRM.AddressZone.GetAddressZones();
				for (int i= 0; i< addZone.Length; i++)
				{
					ddL.Items.Add(new ListItem(addZone[i].Description, addZone[i].AddressZoneId.ToString().Trim()));
				}
			}
		}
	}
}
