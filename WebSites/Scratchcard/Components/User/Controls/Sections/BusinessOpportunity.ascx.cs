using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GA.BDC.Core.WebTracking;
using GA.BDC.Core.efundraisingCore;
using GA.BDC.Core.EnterpriseComponents;
using GA.BDC.Core.Email;
using GA.BDC.Core.Web.UI.UIControls;

//using Business.com.ses.ws.AddressHygiene;

namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Sections
{


	/// <summary>
	///		Summary description for BusinessOpportunity.
	/// </summary>
	public class BusinessOpportunity : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.TextBox FirstNameTextBox;
		protected System.Web.UI.WebControls.TextBox LastNameTextBox;
		protected System.Web.UI.WebControls.TextBox EmailTextBox;
		protected System.Web.UI.WebControls.TextBox AddressTextBox;
		protected System.Web.UI.WebControls.TextBox CityTextBox;
		protected System.Web.UI.WebControls.DropDownList StateDropDownList;
		protected System.Web.UI.WebControls.TextBox ZipTextBox;
		protected System.Web.UI.WebControls.DropDownList CountryDropDownList;
		protected System.Web.UI.WebControls.TextBox DPhone1TextBox;
		protected System.Web.UI.WebControls.TextBox DPhone2TextBox;
		protected System.Web.UI.WebControls.TextBox DPhone3TextBox;
		protected System.Web.UI.WebControls.TextBox DPhoneExtTextBox;
		protected System.Web.UI.WebControls.TextBox EPhone1TextBox;
		protected System.Web.UI.WebControls.TextBox EPhone2TextBox;
		protected System.Web.UI.WebControls.TextBox EPhone3TextBox;
		protected System.Web.UI.WebControls.TextBox EPhoneExtTextBox;
		protected System.Web.UI.WebControls.DropDownList TimeDropDownList;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator4;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator5;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator6;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator7;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator8;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator9;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl5;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl6;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl7;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl8;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl9;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl11;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl12;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl10;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl13;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl16;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl17;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl18;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl20;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl19;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl21;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl22;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl23;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl24;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl25;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl26;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl27;
        protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl ButtonPanelControl1;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl14;
        protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl15;
        protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;

		// address validation
		public Components.User.AddressHygiene.AddressHygiene AddressHygiene1;
		public Components.User.AddressHygiene.ExplicitAddressConfirmation ExplicitAddressConfirmation1;


		private void Page_Load(object sender, System.EventArgs e)
		{
			GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage gbp = 
				(GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage)Page;
			gbp.Globalize(PagePanelControl1, this);

			// set hander for the address hygiene controls
			//AddressHygiene1.OutputAddress += new efundraising.ScratchcardWeb.Components.User.AddressHygiene.OutputAddressEventHandler(AddressHygiene1_OutputAddress);
            //AddressHygiene1.OutputAddress += new efundraising.ScratchcardWeb.Components.User.AddressHygiene.OutputAddressEventHandler(AddressHygiene1_OutputAddress);


            //ExplicitAddressConfirmation1.OnConfirm += new EventHandler(ExplicitAddressConfirmation1_OnConfirm);
           // ExplicitAddressConfirmation1.OnCancel += new EventHandler(ExplicitAddressConfirmation1_OnCancel);
           // ExplicitAddressConfirmation1.OnSaveWithoutChange += new EventHandler(ExplicitAddressConfirmation1_OnSaveWithoutChange);
			
            // Hide AddressHygiene controls
			//AddressHygiene1.Visible = false;
			//ExplicitAddressConfirmation1.Visible = false;

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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ButtonPanelControl1.Click += new TrackingButtonEventHandler(this.ButtonPanelControl1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ButtonPanelControl1_Click(object sender, System.EventArgs e)
		{
			if(Session["ADDRESS_VALIDATED"] != null || this.Page.IsValid) 
			{
				bool postalAddressIsValidated = false;
                if (AddressHygiene1.IsAddressHygieneEnabled)
                {
                    if (CountryDropDownList.SelectedValue == "US" || CountryDropDownList.SelectedValue == "CA")
                    {
						// fill out the address using the profit information
						//Business.com.ses.ws.AddressHygiene.Address address = 
							//new Business.com.ses.ws.AddressHygiene.Address();

						//address.Address1 = AddressTextBox.Text;
						//address.City = CityTextBox.Text;
						//address.Region = StateDropDownList.SelectedValue;
                       // address.Country = CountryDropDownList.SelectedValue;
                        //address.Country = "US";
						string zip1 = "";
						string zip2 = "";

						// cut out the zip/zip4
						string zip = ZipTextBox.Text;
						if(zip.IndexOf(" ") > -1) {
							string[] zipSlip = zip.Split(' ');
							zip1 = zipSlip[0];
							zip2 = zipSlip[1];
						}

                        //address.PostCode = (zip1.Trim() == ""? zip: zip1);
                        //address.PostCode2 = zip2;

						bool enableSuggestionList = true;

						if(Session["CONFIRM"] == null) {
                            //if (AddressHygiene1.DoAddressHygiene(address, enableSuggestionList)) {
                            //    Business.com.ses.ws.AddressHygiene.OutputAddress outputAddress =
                            //        AddressHygiene1.OutAddress;

                            //    //Check if there was an error with the address that came back
                            //    if (outputAddress.Fault != Fault.NoError) {
                            //        ExplicitAddressConfirmation1.SetAddress(outputAddress.Address1 + "<br />" + outputAddress.City + ", " + outputAddress.Region + " " + "<br />" + outputAddress.PostCode + " " + outputAddress.PostCode2);
                            //        ExplicitAddressConfirmation1.Visible = true;
                            //        return;
                            //    }
                            //        //Check if there was a Suggestion List error
                            //    else if (outputAddress.SuggestionListInformation.Error!= SuggestionListError.None && Session["ADDRESS_VALIDATED"] == null) {
                            //        AddressHygiene1.Visible = true;
                            //        return;
                            //    } else {
                            //        postalAddressIsValidated = true;

                            //        string newStreet1 = outputAddress.Address1;
                            //        string newStreet2 = outputAddress.Address2;
                            //        string newCity = outputAddress.City;
                            //        string newCounty = outputAddress.County;
                            //        string newPostalCode = outputAddress.PostCode;
                            //        string newPostalCode2 = outputAddress.PostCode2;
                            //        string newProvince = outputAddress.Region;
                            //        string newCountry = outputAddress.Country;

                            //        AddressTextBox.Text = newStreet1 + " " + newStreet2;
                            //        CityTextBox.Text = newCity;
                            //        ZipTextBox.Text = newPostalCode + " " + newPostalCode2;
									
                            //        ListItem item = StateDropDownList.Items.FindByValue(newProvince);
                            //        if(item != null) {
                            //            for(int i=0;i<StateDropDownList.Items.Count;i++) {
                            //                ListItem li = (ListItem)StateDropDownList.Items[i];
                            //                li.Selected = false;
                            //            }
                            //            item.Selected = true;
                            //        }
                            //    }
                            //}
						}
					}
				}

				eFundEnv oEnv = eFundEnv.Create();

				GA.BDC.Core.efundraisingCore.EfundraisingLead oNewLead = new GA.BDC.Core.efundraisingCore.EfundraisingLead();
				oNewLead.FirstName = this.FirstNameTextBox.Text;
				oNewLead.LastName = this.LastNameTextBox.Text;
				oNewLead.Title = "99";
				oNewLead.Email = this.EmailTextBox.Text;
				oNewLead.GroupName = "";
				oNewLead.StreetAddress = this.AddressTextBox.Text;
				oNewLead.City = this.CityTextBox.Text;
				oNewLead.State = this.StateDropDownList.SelectedValue;
				oNewLead.Country = this.CountryDropDownList.SelectedValue;
				oNewLead.ZipCode = this.ZipTextBox.Text;
				oNewLead.DayPhone = this.DPhone1TextBox.Text + "-" + this.DPhone2TextBox.Text + "-" +this.DPhone3TextBox.Text;
				oNewLead.DayPhoneExt = this.DPhoneExtTextBox.Text;
				oNewLead.EveningPhone = this.EPhone1TextBox.Text + "-" + this.EPhone2TextBox.Text + "-" +this.EPhone3TextBox.Text;
				oNewLead.EveningPhoneExt = this.EPhoneExtTextBox.Text;
				oNewLead.ParticipantCount = 0;
				oNewLead.BestTimeToCall = this.TimeDropDownList.SelectedValue;
				oNewLead.OrganizationTypeID = 99;
				oNewLead.GroupTypeID = 99;
				oNewLead.IsPostalAddressValidated = (postalAddressIsValidated? 1: 0);

				// New free kit lead.
				oNewLead.LeadStatusID = 1; 

				oNewLead.FundraisingDate = "";
				oNewLead.DecisionMaker = false;
				oNewLead.ProductsInterest = "Give N Take Agent";
				oNewLead.OnEmailList = false;
				oNewLead.Comments = "";
				try 
				{
					oNewLead.PromotionID = 4137;
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
								GA.BDC.Core.Diagnostics.Logger.LogWarn("Business Oportunity failed to insert postal address: Lead ID: " + oNewLead.LeadID, ex);
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
				catch(Exception ex) 
				{
					LoggingSystem.LogError("efundraisingWeb",ex.Message);
				} 
				finally 
				{
					oEnv.LeadObject = oNewLead;
					oEnv.Save();
					Components.Server.EmailSender.SendMail oSnd = new Components.Server.EmailSender.SendMail();
					oSnd.SendLeadConfirmation();
				}

				VisitorLog visitorLog = VisitorLog.Create(Session);
				visitorLog.UpdateVisitorLog(oNewLead.LeadID);
				
				Response.Redirect("BusinessOpportunityConfirm.aspx");
			}
		}

        //private void AddressHygiene1_OutputAddress(object sender, OutputAddress outputAddress, bool addressChanged, int nAddress)
        //{
        //    Session["ADDRESS_VALIDATED"] = true;
        //    //ButtonPanelControl1_Click(sender, null);
        //}

        //private void ExplicitAddressConfirmation1_OnConfirm(object sender, EventArgs e)
        //{
        //    Session["CONFIRM"] = true;
        //    ButtonPanelControl1_Click(sender, e);
        //}

        //private void ExplicitAddressConfirmation1_OnSaveWithoutChange(object sender, EventArgs e)
        //{
        //    Session["CONFIRM"] = "NOCHANGE";
        //}

        //private void ExplicitAddressConfirmation1_OnCancel(object sender, EventArgs e)
        //{

        //}

	}
}
