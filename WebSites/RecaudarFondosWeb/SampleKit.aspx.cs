using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.efundraisingCore;
using GA.BDC.Core.Email;
using GA.BDC.Core.AddressHygiene.com.qsp.AddressHygiene;

//using Business.com.ses.ws.AddressHygiene;

namespace efundraising.RecaudarFondosWeb
{
	/// <summary>
	/// Summary description for SampleKit.
	/// </summary>
	public class SampleKit : RecaudarFondosWebPage
	{
		protected System.Web.UI.WebControls.DropDownList ddlTitle;
		protected System.Web.UI.WebControls.DropDownList ddlCountry;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.WebControls.DropDownList ddlBestTime;
		protected System.Web.UI.WebControls.DropDownList ddlOrgType;
		protected System.Web.UI.WebControls.TextBox txtFirstName;
		protected System.Web.UI.WebControls.TextBox txtLastName;
		protected System.Web.UI.WebControls.TextBox txtEMail;
		protected System.Web.UI.WebControls.TextBox txtGroupName;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.TextBox txtCity;
		protected System.Web.UI.WebControls.TextBox txtZipCode;
		protected System.Web.UI.WebControls.TextBox txtDayPhone1;
		protected System.Web.UI.WebControls.TextBox txtDayPhone2;
		protected System.Web.UI.WebControls.TextBox txtEveningPhone2;
		protected System.Web.UI.WebControls.CheckBox chkMonthList0;
		protected System.Web.UI.WebControls.CheckBox chkMonthList1;
		protected System.Web.UI.WebControls.CheckBox chkMonthList2;
		protected System.Web.UI.WebControls.CheckBox chkMonthList4;
		protected System.Web.UI.WebControls.CheckBox chkMonthList5;
		protected System.Web.UI.WebControls.CheckBox chkMonthList6;
		protected System.Web.UI.WebControls.CheckBox chkMonthList7;
		protected System.Web.UI.WebControls.CheckBox chkMonthList8;
		protected System.Web.UI.WebControls.CheckBox chkMonthList9;
		protected System.Web.UI.WebControls.CheckBox chkMonthList10;
		protected System.Web.UI.WebControls.CheckBox chkMonthList11;
		protected System.Web.UI.WebControls.RadioButton rdbDecision0;
		protected System.Web.UI.WebControls.RadioButton rdbDecision1;
		protected System.Web.UI.WebControls.CheckBox chkProgramList0;
		protected System.Web.UI.WebControls.CheckBox chkProgramList1;
		protected System.Web.UI.WebControls.CheckBox chkProgramList2;
		protected System.Web.UI.WebControls.CheckBox chkProgramList3;
		protected System.Web.UI.WebControls.CheckBox chkNewsLetter;
		protected System.Web.UI.WebControls.TextBox txtComments;
		protected System.Web.UI.HtmlControls.HtmlInputImage Image1;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFirstName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLastName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGroupName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAddress;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCity;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvZipCode;
		protected System.Web.UI.WebControls.TextBox txtDayPhoneExt;
		protected System.Web.UI.WebControls.TextBox txtEveningPhone;
		protected System.Web.UI.WebControls.TextBox txtEveningPhoneExt;
		protected System.Web.UI.WebControls.TextBox txtDayPhone;
		protected System.Web.UI.WebControls.TextBox txtDayPhone3;
		protected System.Web.UI.WebControls.TextBox txtEveningPhone1;
		protected System.Web.UI.WebControls.TextBox txtEveningPhone3;
		protected System.Web.UI.WebControls.ImageButton Submit;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDayPhone;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEmail;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEmail;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDayPhone;
		protected System.Web.UI.WebControls.TextBox txtGroupNumber;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGroupNumber;
		protected System.Web.UI.WebControls.TextBox txtMonthList;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMonthList;
		protected System.Web.UI.WebControls.CheckBox chkMonthList3;
		protected System.Web.UI.WebControls.TextBox txtDecision;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDecision;
		protected System.Web.UI.WebControls.DropDownList ddlGroupType;
	
		ListItemCollection lstState;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlTableRow tdForm;
		protected System.Web.UI.HtmlControls.HtmlTable tblConfirm;

		// address validation
		public Components.User.AddressHygiene.AddressHygiene AddressHygiene1;
		public Components.User.AddressHygiene.ExplicitAddressConfirmation ExplicitAddressConfirmation1;

		// For "Partner" Concept
		protected System.Web.UI.WebControls.PlaceHolder MagFunTop = new PlaceHolder();
		protected System.Web.UI.WebControls.PlaceHolder EfunBottom = new PlaceHolder();
		protected System.Web.UI.WebControls.PlaceHolder MagBottom = new PlaceHolder();
		#region Constants
		
		private const int RecaudarFondosPromotion = 4157;
		
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			recaudarFondosOmnitureTracking.SetPageNameAndCategory("Public", "Free Info Kit");
			recaudarFondosOmnitureTracking.AddEVar_Custom(5, "Free Kit Form");
			recaudarFondosOmnitureTracking.AddEvent_Custom(4);

            // set hander for the address hygiene controls
            AddressHygiene1.OutputAddress += new efundraising.RecaudarFondosWeb.Components.User.AddressHygiene.OutputAddressEventHandler(AddressHygiene1_OutputAddress);
            ExplicitAddressConfirmation1.OnConfirm += new EventHandler(ExplicitAddressConfirmation1_OnConfirm);
            ExplicitAddressConfirmation1.OnCancel += new EventHandler(ExplicitAddressConfirmation1_OnCancel);
            ExplicitAddressConfirmation1.OnSaveWithoutChange += new EventHandler(ExplicitAddressConfirmation1_OnSaveWithoutChange);

            // Hide AddressHygiene controls
            AddressHygiene1.Visible = false;
            ExplicitAddressConfirmation1.Visible = false;


			this.Page.FindControl("tblConfirm").Visible=false;

			Components.User.Partner.MagfunTop top = Page.LoadControl("Components/User/Partner/MagfunTop.ascx") as Components.User.Partner.MagfunTop;
			MagFunTop.Controls.Add(top);

			Components.User.Partner.EfunBottom efun = Page.LoadControl("Components/User/Partner/EfunBottom.ascx") as Components.User.Partner.EfunBottom;
			EfunBottom.Controls.Add(efun);

			Components.User.Partner.MagfunBottom magfun = Page.LoadControl("Components/User/Partner/MagfunBottom.ascx") as Components.User.Partner.MagfunBottom;
			MagBottom.Controls.Add(magfun);
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
			this.Submit.Click += new System.Web.UI.ImageClickEventHandler(this.Submit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Submit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if( this.Page.IsValid)
			{
				bool postalAddressIsValidated = false;
                if (AddressHygiene1.IsAddressHygieneEnabled) 
				{
					// fill out the address using the profit information
					Address addr = 	new Address();

					addr.Address1 = txtAddress.Text;
					addr.City = txtCity.Text;
					addr.Region = ddlState.SelectedValue;
                    addr.Country = ddlCountry.SelectedValue;
                    //addr.Country = "US";
					string zip1 = "";
					string zip2 = "";

					// cut out the zip/zip4
					string zipp = txtZipCode.Text;
					if(zipp.IndexOf(" ") > -1) 
					{
						string[] zipSlip = zipp.Split(' ');
						zip1 = zipSlip[0];
						zip2 = zipSlip[1];
					}

					addr.PostCode = (zip1.Trim() == ""? zipp: zip1);
					addr.PostCode2 = zip2;

					bool enableSuggestionList = true;

					if(Session["CONFIRM"] == null) 
					{
						if (AddressHygiene1.DoAddressHygiene(addr, enableSuggestionList)) 
						{
							OutputAddress outputAddress =
								AddressHygiene1.OutAddress;

							//Check if there was an error with the address that came back
							if (outputAddress.Fault != Fault.NoError) 
							{
								ExplicitAddressConfirmation1.SetAddress(outputAddress.Address1 + "<br />" + outputAddress.City + ", " + outputAddress.Region + " " + "<br />" + outputAddress.PostCode + " " + outputAddress.PostCode2);
								ExplicitAddressConfirmation1.Visible = true;
								return;
							}
								//Check if there was a Suggestion List error
                            //else if (outputAddress.SuggestionListInformation.SystemError != SuggestionListSystemError.None) 
                            //{
                            //    ExplicitAddressConfirmation1.SetAddress(outputAddress.Address1 + "<br />" + outputAddress.City + ", " + outputAddress.Region + " " + "<br />" + outputAddress.PostCode + " " + outputAddress.PostCode2);
                            //    ExplicitAddressConfirmation1.Visible = true;
                            //    return;
                            //} 
							else 
							{
								postalAddressIsValidated = true;

								string newStreet1 = outputAddress.Address1;
								string newStreet2 = outputAddress.Address2;
								string newCity = outputAddress.City;
								string newCounty = outputAddress.County;
								string newPostalCode = outputAddress.PostCode;
								string newPostalCode2 = outputAddress.PostCode2;
								string newProvince = outputAddress.Region;
								string newCountry = outputAddress.Country;

								txtAddress.Text = newStreet1 + " " + newStreet2;
								txtCity.Text = newCity;
								txtZipCode.Text = newPostalCode;
								for(int i=0;i<ddlState.Items.Count;i++) 
								{
									ListItem item = ddlState.Items[i];
									item.Selected = false;
								}

								ListItem it = ddlState.Items.FindByValue(newProvince);
								if(it != null) 
								{
									it.Selected = true;
								}
							}
						}
					}
				}

				string firstName, lastName, email, address, city, state, zip, country;
				string dayPhone, eveningPhone, organisationName, title, eveningPhoneExt;
				string dayPhoneExt, bestTimeToCall, productsInterest, comments, fundraisingMonth;
				int groupSize = 0;
				int organizationTypeID;
				byte pGroupTypeID;
				DateTime fundraisingDate;
				bool decisionMaker,onEmailList;

				firstName = txtFirstName.Text ;
				lastName = txtLastName.Text;
				email = txtEMail.Text;
				address = txtAddress.Text;
				city = txtCity.Text;
				state = ddlState.SelectedValue;
				zip = txtZipCode.Text;
				country = ddlCountry.SelectedValue;
				dayPhone = txtDayPhone1.Text + "-" + txtDayPhone2.Text + "-" + txtDayPhone3.Text ;
				eveningPhone = txtEveningPhone1.Text + "-" + txtEveningPhone2.Text + "-" + txtEveningPhone3.Text ;
				if (eveningPhone == "--")
					eveningPhone = "";

				try 
				{
					groupSize = Convert.ToInt32(txtGroupNumber.Text.ToString());
				}
				catch {}

				organisationName = txtGroupName.Text;
				title = ddlTitle.SelectedValue;
				eveningPhoneExt = txtEveningPhoneExt.Text;
				dayPhoneExt = txtDayPhoneExt.Text;
				if (ddlBestTime.SelectedIndex != 0)
					bestTimeToCall = ddlBestTime.SelectedItem.Text;
				else
					bestTimeToCall = "";
				organizationTypeID = Convert.ToInt32(ddlOrgType.SelectedValue.ToString());
				pGroupTypeID = Convert.ToByte(ddlGroupType.SelectedValue.ToString());

				TimeSpan ts = new TimeSpan(0,5,0,0,0);
				DateTime dt = TimeZone.CurrentTimeZone.ToUniversalTime(DateTime.Now).Subtract(ts);
				fundraisingDate = dt;

			
				if (rdbDecision0.Checked)
					decisionMaker=true;
				else
					decisionMaker=false;

				productsInterest = "";
				if (chkProgramList0.Checked)
					productsInterest = productsInterest + chkProgramList0.Text + ",";
				if (chkProgramList1.Checked)
					productsInterest = productsInterest + chkProgramList1.Text + "," ;
				if (chkProgramList2.Checked)
					productsInterest = productsInterest + chkProgramList2.Text + "," ;
				if (chkProgramList3.Checked)
					productsInterest = productsInterest + chkProgramList3.Text;
				if (chkNewsLetter.Checked)
					onEmailList = true;
				else
					onEmailList = false;

				//[Fundraising Date => May- June- July- August- September- ]
				//Appending the fundraising start date to the comments
			
				fundraisingMonth="";
				if (chkMonthList0.Checked)
					fundraisingMonth += chkMonthList0.Text + "-";
				if (chkMonthList1.Checked)
					fundraisingMonth += chkMonthList1.Text + "-";
				if (chkMonthList2.Checked)
					fundraisingMonth += chkMonthList2.Text + "-";
				if (chkMonthList3.Checked)
					fundraisingMonth += chkMonthList3.Text + "-";
				if (chkMonthList4.Checked)
					fundraisingMonth += chkMonthList4.Text + "-";
				if (chkMonthList5.Checked)
					fundraisingMonth += chkMonthList5.Text + "-";
				if (chkMonthList6.Checked)
					fundraisingMonth += chkMonthList6.Text + "-";
				if (chkMonthList7.Checked)
					fundraisingMonth += chkMonthList7.Text + "-";
				if (chkMonthList8.Checked)
					fundraisingMonth += chkMonthList8.Text + "-";
				if (chkMonthList9.Checked)
					fundraisingMonth += chkMonthList9.Text + "-";
				if (chkMonthList10.Checked)
					fundraisingMonth += chkMonthList10.Text + "-";
				if (chkMonthList11.Checked)
					fundraisingMonth += chkMonthList11.Text + "-";
			
				fundraisingMonth = "[Fundraising Date =>" + fundraisingMonth + "]";
				comments = fundraisingMonth + txtComments.Text;

				try
				{
					Label1.Visible = false;

					EfundraisingLead lead = new EfundraisingLead();
					lead.FirstName = firstName;
					lead.LastName = lastName;
					lead.Email = email;
					lead.StreetAddress = address;
					lead.City = city;
					lead.State = state;
					lead.ZipCode = zip;
					lead.Country = country;
					lead.DayPhone = dayPhone;
					lead.EveningPhone = eveningPhone;
					lead.ParticipantCount = groupSize;
					lead.GroupName = organisationName;
					lead.DayPhoneExt = dayPhoneExt;
					lead.EveningPhoneExt = eveningPhoneExt;
					lead.OrganizationTypeID = organizationTypeID;
					lead.GroupTypeID = pGroupTypeID;
					if(this.Page.Request.QueryString["promotion_id"]!= null && this.Page.Request.QueryString["promotion_id"]!= null)
					{
						try
						{
							lead.PromotionID = Convert.ToInt32(Page.Request.QueryString["promotion_id"].ToString());
						}
						catch(Exception)
						{
							if(this.Page.Request.QueryString["partner"] != null && this.Page.Request.QueryString["partner"]!= string.Empty)
							{
								Partner partner = Partner.GetPartnerInfoByFolder(Request.QueryString["partner"]);
								if(partner != null)
								{
									eFundEnv env = eFundEnv.Create();
									env.PartnerInfo = partner;
									env.FindPromotion(Request);
									env.PartnerID = env.PartnerInfo.PartnerID;
									if(env.PromotionID != -1)
									{
										lead.PromotionID = env.PromotionID;
									}
									else
									{
										lead.PromotionID = RecaudarFondosPromotion;
									}
								}
								else
								{
									lead.PromotionID = RecaudarFondosPromotion;
								}
							}
							else
							{
								lead.PromotionID = RecaudarFondosPromotion;
							}
						}
					}
					else if(this.Page.Request.QueryString["partner"] != null && this.Page.Request.QueryString["partner"]!= string.Empty)
					{
						Partner partner = Partner.GetPartnerInfoByFolder(Request.QueryString["partner"]);
					
						if(partner != null)
						{
							eFundEnv env = eFundEnv.Create();
							env.PartnerInfo = partner;
							env.FindPromotion(Request);
							env.PartnerID = env.PartnerInfo.PartnerID;
							if(env.PromotionID != -1)
							{
								lead.PromotionID = env.PromotionID;
							}
							else
							{
								lead.PromotionID = RecaudarFondosPromotion;
							}
						}
						else
						{
							lead.PromotionID = RecaudarFondosPromotion;
						}
					}
					else
					{
						lead.PromotionID = RecaudarFondosPromotion;
					}
					lead.Title = title;
					lead.BestTimeToCall = bestTimeToCall;
					lead.FundraisingDate = fundraisingDate.ToString();
					lead.DecisionMaker = decisionMaker;
					lead.ProductsInterest = productsInterest;
					lead.OnEmailList = onEmailList;
					lead.IsPostalAddressValidated = (postalAddressIsValidated ? 1: 0);
					lead.Integrate();

					if(lead.LeadID != int.MinValue) {
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
								lead.ConsultantID, GA.BDC.Core.EFundraisingCRM.LeadChannel.Internet.ChannelCode, 
								lead.PromotionID, lead.PartnerID,
								lead.State, lead.Country);
						
							// create a postal address object with lead information
							GA.BDC.Core.EFundraisingCRM.PostalAddress postalAddress =
								new GA.BDC.Core.EFundraisingCRM.PostalAddress(
								int.MinValue, lead.StreetAddress, lead.City, 
								lead.ZipCode, lead.Country, 
								lead.Country + "-" + lead.State, DateTime.Now);

							// insert the postal address, if it failed, log 
							// and continue the process (will insert an invalid
							// promotional kit with no postal address id)
							try {
								postalAddress.Insert();
							} catch(System.Exception ex) {
							  GA.BDC.Core.Diagnostics.Logger.LogWarn("Sample Kit failed to insert postal address: Lead ID: " + lead.LeadID, ex);
							}

							// create our promotional kit object 
                            GA.BDC.Core.EFundraisingCRM.PromotionalKit promotionalKit =
                                new GA.BDC.Core.EFundraisingCRM.PromotionalKit(
								int.MinValue, lead.LeadID, lead.LeadVisitID, kitType.KitTypeID,
                                GA.BDC.Core.EFundraisingCRM.Carrier.RegularMail.CarrierId, int.MinValue,
								postalAddress.PostalAddressId, (postalAddress.PostalAddressId == int.MinValue? 0: 1), DateTime.Now, DateTime.MinValue);

							// insert the promotional kit
							promotionalKit.Insert();
						} catch(System.Exception ex) {
							// let it go anyway, the promotional kit manager service will insert it
                            GA.BDC.Core.Diagnostics.Logger.LogWarn("Unable to insert promotional kit", ex);
						}
					}

					// Send confirmation email
					string subject = "A new lead has been added";
					string body = @"
PromotionID         : " + lead.PromotionID + @"
Lead ID             : " + lead.LeadID + @"
First Name          : " + lead.FirstName + @"
Last Name           : " + lead.LastName + @"
Title               : " + lead.Title + @"
E-Mail Address      : " + lead.Email + @"
Group Name          : " + lead.GroupName + @"
Address             : " + lead.StreetAddress + @"
City                : " + lead.City + @"
State / Province    : " + lead.State + @"
Country             : " + lead.Country + @"
Zip / Postal Code   : " + lead.ZipCode + @"
Day Phone           : " + lead.DayPhone + @"
Evening Phone       : " + lead.EveningPhone + @"
Best Time to Call   : " + lead.BestTimeToCall + @"
Organization Type   : " + lead.OrganizationTypeID + @"
Group Name          : " + lead.GroupName + @"
Group Members       : " + lead.GroupName + @"
Decision Maker      : " + lead.DecisionMaker + @"
Newsletter          : " + lead.OnEmailList + @"
Comments            : 

" + lead.Comments;

					// Send email to all people interested to receive reports
					for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("RecaudarFondosWeb.Leads.Report"); i++)
					{
						SendMail.AsyncSend(ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
							"services@efundraising.com", ApplicationSettings.GetConfig()["RecaudarFondosWeb.Leads.Report", i, "email"],
							null, null, null, "",  subject, body, "");	
					}
 

					// Signal Omniture form completion
					recaudarFondosOmnitureTracking.SetPageNameAndCategory("Public", "Free Info Kit Confirmation");
					recaudarFondosOmnitureTracking.AddEvent_Custom(5);
				
					this.Page.FindControl("tblForm").Visible = false;
					this.Page.FindControl("tblConfirm").Visible = true;
							
				}
				catch(Exception ex)
				{
					Label1.Text += ex.Message;
				}
			}

		}

		private void ddlCountry_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lstState=new ListItemCollection();
			switch (ddlCountry.SelectedItem.Value )
			{
				case "CA":
					lstState.Add(new ListItem("----- Please Select -----","0"));
					lstState.Add(new ListItem("Alberta","AB"));
					lstState.Add(new ListItem("British Columbia","BC"));
					lstState.Add(new ListItem("Labrador","LB"));
					lstState.Add(new ListItem("Manitoba","MB"));
					lstState.Add(new ListItem("New Brunswick","NB"));
					lstState.Add(new ListItem("Newfoundland","NL"));
					lstState.Add(new ListItem("Northwest Territories","NT"));
					lstState.Add(new ListItem("Nova Scotia","NS"));
					lstState.Add(new ListItem("Nunavut","NU"));
					lstState.Add(new ListItem("Ontario","ON"));
					lstState.Add(new ListItem("Prince Edward Island","PE"));
					lstState.Add(new ListItem("Qu&#233;bec","QC"));
					lstState.Add(new ListItem("Saskatchewan","SK"));
					lstState.Add(new ListItem("Yukon Territory","YT"));
					
					break;
				case "US":
					lstState.Add(new ListItem("----- Please Select -----","0"));
					lstState.Add(new ListItem("Alabama","AL"));
					lstState.Add(new ListItem("Alaska","AK"));
					lstState.Add(new ListItem("Arizona","AZ"));
					lstState.Add(new ListItem("Arkansas","AR"));
					lstState.Add(new ListItem("Armed Forces Americas","AA"));
					lstState.Add(new ListItem("Armed Forces Europe/Middle East/Canada","AE"));
					lstState.Add(new ListItem("Armed Forces Pacific","AP"));
					lstState.Add(new ListItem("California","CA"));
					lstState.Add(new ListItem("Colorado","CO"));
					lstState.Add(new ListItem("Connecticut","CT"));
					lstState.Add(new ListItem("Delaware","DE"));
					lstState.Add(new ListItem("District of Columbia","DC"));
					lstState.Add(new ListItem("Florida","FL"));
					lstState.Add(new ListItem("Georgia","GA"));
					lstState.Add(new ListItem("Hawaii","HI"));
					lstState.Add(new ListItem("Idaho","ID"));
					lstState.Add(new ListItem("Illinois","IL"));
					lstState.Add(new ListItem("Indiana","IN"));
					lstState.Add(new ListItem("Iowa","IA"));
					lstState.Add(new ListItem("Kansas","KS"));
					lstState.Add(new ListItem("Kentucky","KY"));
					lstState.Add(new ListItem("Louisiana","LA"));
					lstState.Add(new ListItem("Maine","ME"));
					lstState.Add(new ListItem("Maryland","MD"));
					lstState.Add(new ListItem("Massachusetts","MA"));
					lstState.Add(new ListItem("Michigan","MI"));
					lstState.Add(new ListItem("Minnesota","MN"));
					lstState.Add(new ListItem("Mississippi","MS"));
					lstState.Add(new ListItem("Missouri","MO"));
					lstState.Add(new ListItem("Montana","MT"));
					lstState.Add(new ListItem("Nebraska","NE"));
					lstState.Add(new ListItem("Nevada","NV"));
					lstState.Add(new ListItem("New Hampshire","NH"));
					lstState.Add(new ListItem("New Jersey","NJ"));
					lstState.Add(new ListItem("New Mexico","NM"));
					lstState.Add(new ListItem("New York","NY"));
					lstState.Add(new ListItem("North Carolina","NC"));
					lstState.Add(new ListItem("North Dakota","ND"));
					lstState.Add(new ListItem("Ohio","OH"));
					lstState.Add(new ListItem("Oklahoma","OK"));
					lstState.Add(new ListItem("Oregon","OR"));
					lstState.Add(new ListItem("Pennsylvania","PA"));
					lstState.Add(new ListItem("Rhode Island","RI"));
					lstState.Add(new ListItem("South Carolina","SC"));
					lstState.Add(new ListItem("South Dakota","SD"));
					lstState.Add(new ListItem("Tennessee","TN"));
					lstState.Add(new ListItem("Texas","TX"));
					lstState.Add(new ListItem("Utah","UT"));
					lstState.Add(new ListItem("Vermont","VT"));
					lstState.Add(new ListItem("Virginia","VA"));
					lstState.Add(new ListItem("Washington","WA"));
					lstState.Add(new ListItem("West Virginia","WV"));
					lstState.Add(new ListItem("Wisconsin","WI"));
					lstState.Add(new ListItem("Wyoming","WY"));
									
					break;
				//case "AU","BM","BR","CK","FR","GU","ID","MX","NZ","na","PR","SG","ZA","TG","UK","VI";
				default:
					lstState.Add(new ListItem("----- Please Select -----","0"));
					lstState.Add(new ListItem("Not Applicable (unknown)","N/A"));
					break;

			}
			ddlState.Items.Clear();
			ddlState.DataSource=lstState;
			ddlState.DataBind();

		}

		private void AddressHygiene1_OutputAddress(object sender, OutputAddress outputAddress, bool addressChanged, int nAddress) {
            Session["ADDRESS_VALIDATED"] = true;
		}

		private void ExplicitAddressConfirmation1_OnConfirm(object sender, EventArgs e) {
			Session["CONFIRM"] = true;
			Submit_Click(sender, null);
		}

        private void ExplicitAddressConfirmation1_OnSaveWithoutChange(object sender, EventArgs e)
        {
            Session["CONFIRM"] = "NOCHANGE";
        }

        private void ExplicitAddressConfirmation1_OnCancel(object sender, EventArgs e)
        {

        }
	}
}
