namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSP.WebControl;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;
	using QSP.WebControl.DataAccess.Business;
	using Business.com.ses.ws.AddressHygiene;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class PublisherMaintenanceControl : MarketingMgtControl
	{
		protected PublisherContactSearchControl ctrlPublisherContactSearchControl;
		protected PublisherContactMaintenanceControl ctrlPublisherContactMaintenanceControl;

		public event SelectPublisherEventHandler PublisherSaved;
		protected System.Web.UI.HtmlControls.HtmlTable Table3;
		public System.Web.UI.WebControls.Label AddressHygieneStatusLabel;
		public event System.EventHandler PublisherCancelled;
		protected QSPFulfillment.CommonWeb.UC.AddressHygiene ctrlAddressHygiene;

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			AddJavaScript();
		}

		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript();

			if (!AddressHygiened)
			{
				AddJavaScriptDisableSubmitButton();
				AddAddressAttributes();
			}
			else
				RemoveAddressAtrributes();
		}

		private void AddJavaScriptDisableSubmitButton() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function DisableSubmitButton() {\n";
			script += "     document.getElementById(\"" + this.btnSubmit.ClientID + "\").disabled = true;\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("DisableSubmitButton", script);
		}

		private void AddAddressAttributes()
		{
			this.tbxAddress1.Attributes["onchange"] = "DisableSubmitButton();";
			this.tbxAddress2.Attributes["onchange"] = "DisableSubmitButton();";
			this.tbxCity.Attributes["onchange"] = "DisableSubmitButton();";
			this.ddlStateProvince.Attributes["onchange"] = "DisableSubmitButton();";
			this.tbxZip.Attributes["onchange"] = "DisableSubmitButton();";
			this.ddlCountry.Attributes["onchange"] = "DisableSubmitButton();";
		}

		private void RemoveAddressAtrributes()
		{
			this.tbxAddress1.Attributes["onchange"] = "";
			this.tbxAddress2.Attributes["onchange"] = "";
			this.tbxCity.Attributes["onchange"] = "";
			this.ddlStateProvince.Attributes["onchange"] = "";
			this.tbxZip.Attributes["onchange"] = "";
			this.ddlCountry.Attributes["onchange"] = "";
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlPublisherContactSearchControl.SelectPublisherContactClick += new SelectPublisherContactEventHandler(ctrlPublisherContactSearchControl_SelectPublisherContactClick);
			this.ctrlPublisherContactSearchControl.DeletePublisherContactClick += new SelectPublisherContactEventHandler(ctrlPublisherContactSearchControl_DeletePublisherContactClick);
			this.ctrlPublisherContactMaintenanceControl.PublisherContactSaved += new SelectPublisherContactEventHandler(ctrlPublisherContactMaintenanceControl_PublisherContactSaved);
			this.ctrlPublisherContactMaintenanceControl.PublisherContactCancelled += new EventHandler(ctrlPublisherContactMaintenanceControl_PublisherContactCancelled);
			this.btnValidateAddress.Click += new System.EventHandler(this.btnValidateAddress_Click);
			ctrlAddressHygiene.OutputAddress += new QSPFulfillment.CommonWeb.UC.OutputAddressEventHandler(AddressSelected);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			try 
			{
				SavePublisherInformations();
				this.DataBind();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			SelectPublisherClickedArgs args;

			try 
			{
				if(this.btnCancel.Text == "Done") 
				{
					args = new SelectPublisherClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.Publisher());

					if(PublisherSaved != null)
						PublisherSaved(sender, args);
				} 
				else 
				{
					if(PublisherCancelled != null)
						PublisherCancelled(sender, e);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnCreateNew_Click(object sender, System.EventArgs e)
		{
			try 
			{
				SavePublisherInformations();

				this.ctrlPublisherContactMaintenanceControl.PublisherContactInfo = new PublisherContact();
				this.ctrlPublisherContactMaintenanceControl.PublisherContactInfo.PublisherName = PublisherName;
				this.ctrlPublisherContactMaintenanceControl.MainContactID = this.ctrlPublisherContactSearchControl.MainContactID;

				this.PublisherContactMode = true;
				this.DataBind();
				this.btnCancel.Text = "Cancel";
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlPublisherContactSearchControl_SelectPublisherContactClick(object sender, SelectPublisherContactClickedArgs e)
		{
			SavePublisherInformations();

			this.ctrlPublisherContactMaintenanceControl.PublisherContactInfo = e.PublisherContactInfo;
			this.ctrlPublisherContactMaintenanceControl.MainContactID = this.ctrlPublisherContactSearchControl.MainContactID;

			this.PublisherContactMode = true;
			this.DataBind();
			this.btnCancel.Text = "Cancel";
		}

		private void ctrlPublisherContactSearchControl_DeletePublisherContactClick(object sender, SelectPublisherContactClickedArgs e)
		{
			DataBind();
		}

		private void ctrlPublisherContactMaintenanceControl_PublisherContactSaved(object sender, SelectPublisherContactClickedArgs e)
		{
			this.PublisherContactMode = false;
			this.DataBind();
		}

		private void ctrlPublisherContactMaintenanceControl_PublisherContactCancelled(object sender, EventArgs e)
		{
			this.PublisherContactMode = false;
			this.DataBind();
		}

		public Publisher PublisherInfo 
		{
			get 
			{
				return (Publisher) ViewState["PublisherInfo"];
			}
			set 
			{
				ViewState["PublisherInfo"] = value;
			}
		}

		private bool PublisherContactMode 
		{
			get 
			{
				if(this.ViewState["PublisherContactMode"] == null)
					this.ViewState["PublisherContactMode"] = false;

				return Convert.ToBoolean(this.ViewState["PublisherContactMode"]);
			}
			set 
			{
				this.ViewState["PublisherContactMode"] = value;
			}
		}

		#region Fields

		private string Status 
		{
			get 
			{
				return this.ddlStatus.SelectedValue;
			}
			set 
			{
				this.ddlStatus.SelectedIndex = this.ddlStatus.Items.IndexOf(this.ddlStatus.Items.FindByValue(value));
			}
		}

		private string PublisherName 
		{
			get 
			{
				return this.tbxName.Text;
			}
			set 
			{
				this.tbxName.Text = value;
			}
		}
		
		private string Address1 
		{
			get 
			{
				return this.tbxAddress1.Text;
			}
			set 
			{
				this.tbxAddress1.Text = value;
			}
		}
		
		private string Address2 
		{
			get 
			{
				return this.tbxAddress2.Text;
			}
			set 
			{
				this.tbxAddress2.Text = value;
			}
		}
		
		private string City 
		{
			get 
			{
				return this.tbxCity.Text;
			}
			set 
			{
				this.tbxCity.Text = value;
			}
		}
		
		private string Province 
		{
			get 
			{
				return this.ddlStateProvince.SelectedValue;
			}
			set 
			{
				this.ddlStateProvince.SelectedIndex = this.ddlStateProvince.Items.IndexOf(this.ddlStateProvince.Items.FindByValue(value));
			}
		}

		private string PostalCode 
		{
			get 
			{
				return this.tbxZip.Text;
			}
			set 
			{
				this.tbxZip.Text = value;
			}
		}

		public string PostalCode1 
		{
			get 
			{
				return this.tbxZip.Text.Split(new char[] {'-'}, 2).GetValue(0).ToString();
			}
			set 
			{
				if (PostalCode2.Length > 0)
					this.tbxZip.Text = value + "-" + this.PostalCode2;
				else
					this.tbxZip.Text = value;
			}
		}

		public string PostalCode2 
		{
			get 
			{
				string[] postCodes = this.tbxZip.Text.Split(new char[] {'-'}, 2);
				if (postCodes.Length == 2)
					return Convert.ToString(postCodes.GetValue(1));
				else
					return String.Empty;
			}
			set 
			{
				if (value.Length > 0)
					this.tbxZip.Text = PostalCode1 + "-" + value;
				else
					this.tbxZip.Text = PostalCode1;
			}
		}

		private string Country 
		{
			get 
			{
				return this.ddlCountry.SelectedValue;
			}
			set 
			{
				this.ddlCountry.SelectedIndex = this.ddlCountry.Items.IndexOf(this.ddlCountry.Items.FindByValue(value));
			}
		}

		public string CountryFull
		{
			get 
			{
				return this.ddlCountry.SelectedItem.Text;
			}
		}

		public bool AddressHygiened
		{
			get 
			{
				if(ViewState["AddressHygiened"] != null) 
					return (bool)ViewState["AddressHygiened"];
				else
					return false;
			}
			set 
			{
				ViewState["AddressHygiened"] = value;
			}
		}

		#endregion

		public override void DataBind()
		{
			if(!this.PublisherContactMode) 
			{
				SetValueDDL();

				if(this.PublisherInfo != null && PublisherInfo.PublisherNumber != 0)
				{
					SetValue();
					this.ctrlPublisherContactSearchControl.PublisherID = PublisherInfo.PublisherNumber;
					this.ctrlPublisherContactSearchControl.DataBind();
					this.lblInstructions.Visible = true;
					this.ctrlPublisherContactSearchControl.Visible = true;
					this.btnCreateNew.Visible = true;
				} 
				else 
				{
					SetValueEmpty();
					this.lblInstructions.Visible = false;
					this.ctrlPublisherContactSearchControl.Visible = false;
					this.btnCreateNew.Visible = false;
				}

				this.tblMaintenance.Visible = true;
				this.ctrlPublisherContactMaintenanceControl.Visible = false;
			} 
			else 
			{
				this.ctrlPublisherContactMaintenanceControl.PublisherID = PublisherInfo.PublisherNumber;
				this.ctrlPublisherContactMaintenanceControl.DataBind();
				this.tblMaintenance.Visible = false;
				this.ctrlPublisherContactMaintenanceControl.Visible = true;
			}
		}

		private void SetValue() 
		{
			PublisherName = PublisherInfo.Name;
			Status = PublisherInfo.Status;
			Address1 = PublisherInfo.Address1;
			Address2 = PublisherInfo.Address2;
			City = PublisherInfo.City;
			Province = PublisherInfo.StateProvince;
			PostalCode = PublisherInfo.Zip;
			Country = PublisherInfo.CountryCode;
			this.ctrlPublisherContactSearchControl.PublisherID = PublisherInfo.PublisherNumber;
			this.ctrlPublisherContactMaintenanceControl.PublisherID = PublisherInfo.PublisherNumber;
		}

		private void SetValueEmpty() 
		{
			PublisherName = String.Empty;
			Status = String.Empty;
			Address1 = String.Empty;
			Address2 = String.Empty;
			City = String.Empty;
			Province = String.Empty;
			PostalCode = String.Empty;
			Country = String.Empty;
			this.ctrlPublisherContactSearchControl.PublisherID = 0;
		}

		private void SetValueDDL() 
		{
			SetValueDDLStatus();
			SetValueDDLStateProvince();
			SetValueDDLCountry();
		}

		private void SetValueDDLStatus() 
		{
			if(this.ddlStatus.Items.Count == 0)
			{
				this.ddlStatus.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				this.ddlStatus.Items.Add(new ListItem("Active", "ACTIVE"));
				this.ddlStatus.Items.Add(new ListItem("Inactive", "INACTIVE"));
			}
		}

		private void SetValueDDLStateProvince() 
		{
			if(this.ddlStateProvince.Items.Count == 0) 
			{
				this.ddlStateProvince.AsTextFirstRow = true;
				this.ddlStateProvince.TextFirstRow = MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY;
				this.ddlStateProvince.DataBind();
			}
		}

		private void SetValueDDLCountry() 
		{
			if(this.ddlCountry.Items.Count == 0) 
			{
				this.ddlCountry.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, ""));
			
				this.ddlCountry.Items.Add(new ListItem("Canada", "CA"));
				this.ddlCountry.Items.Add(new ListItem("United States", "US"));
			}
		}

		private void SavePublisherInformations() 
		{
			if(PublisherInfo != null && this.PublisherInfo.PublisherNumber != 0) 
			{
				this.Page.BusPublisher.Update(PublisherInfo.PublisherNumber, Status, PublisherName, Address1, Address2, City, Province, PostalCode, Country);
			} 
			else 
			{
				PublisherInfo = new Publisher();
				PublisherInfo.PublisherNumber = this.Page.BusPublisher.Insert(Status, PublisherName, Address1, Address2, City, Province, PostalCode, Country);
			}

			PublisherInfo.Status = Status;
			PublisherInfo.Name = PublisherName;
			PublisherInfo.Address1 = Address1;
			PublisherInfo.Address2 = Address2;
			PublisherInfo.City = City;
			PublisherInfo.StateProvince = Province;
			PublisherInfo.Zip = PostalCode;
			PublisherInfo.CountryCode = Country;

			this.btnCancel.Text = "Done";
		}

		private void btnValidateAddress_Click(object sender, System.EventArgs e)
		{
			AddressHygieneStatusLabel.Text = String.Empty;
			AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Blue;
			
			bool enableSuggestionList = true;
			
			try
			{
				if (ctrlAddressHygiene.DoAddressHygiene(GetAddressFromFields(), enableSuggestionList))
				{
					ctrlAddressHygiene.Visible = true;
				}
				else
				{
					AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
					AddressHygieneStatusLabel.Text = "Error: Unable to perform Address Hygiene";
				}
			}
			catch (Exception)
			{
				AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
				AddressHygieneStatusLabel.Text = "Error: Unable to perform Address Hygiene";
			}

			AddressHygiened = true;
			this.btnSubmit.Enabled = true;
		}

		private Business.com.ses.ws.AddressHygiene.Address GetAddressFromFields()
		{
			Business.com.ses.ws.AddressHygiene.Address address = new Business.com.ses.ws.AddressHygiene.Address();

			address.Address1 = Address1;
			address.Address2 = Address2;
			address.City = City;
			address.County = "";
			address.Region = Province;
			if (this.PostalCode1.Length == 6) //Address Hygiene requires space within Canadian Postal Code
				address.PostCode = this.PostalCode1.Substring(0, 3) + " " + this.PostalCode1.Substring(3, 3);
			else
				address.PostCode = this.PostalCode1;
			address.PostCode2 = this.PostalCode2;
			address.Country = this.CountryFull.ToUpper(System.Globalization.CultureInfo.InvariantCulture);

			return address;
		}

		private void AddressSelected(object Sender, Business.com.ses.ws.AddressHygiene.OutputAddress OutputAddress, bool ChangeStatus)
		{
			//Check if there was an error with the address that came back
			if (OutputAddress.Fault != Fault.NoError)
			{
				AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
				AddressHygieneStatusLabel.Text = "Error: " + ctrlAddressHygiene.GetErrorFromFault(OutputAddress.Fault.ToString());
			}
				//Check if there was a Suggestion List error
			else if (OutputAddress.SuggestionListInformation.Error != SuggestionListError.None && OutputAddress.SuggestionListInformation.Error != SuggestionListError.MoreInformationRequired)
			{
				AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
				AddressHygieneStatusLabel.Text = "Error: " + ctrlAddressHygiene.GetErrorFromFault(OutputAddress.SuggestionListInformation.Error.ToString());
			}
			else
			{
				Address1 = OutputAddress.Address1;
				Address2 = OutputAddress.Address2;
				City = OutputAddress.City;

				if (OutputAddress.PostCode.Length == 7) //Address Hygiene returns a space between Canadian Postal Code
					PostalCode1 = OutputAddress.PostCode.Substring(0, 3) + OutputAddress.PostCode.Substring(4, 3);
				else
					PostalCode1 = OutputAddress.PostCode;

				PostalCode2 = OutputAddress.PostCode2;

				string stateProvince = OutputAddress.Region;
				bool stateProvinceFound = false;
				for(int i = 0; i < this.ddlStateProvince.Items.Count; i++)
				{
					if(ddlStateProvince.Items[i].Value == stateProvince)
					{
						ddlStateProvince.SelectedIndex = i;
						stateProvinceFound = true;
						break;
					}
				}
				if (stateProvinceFound == false)
					ddlStateProvince.SelectedIndex = 0;

				string country;
				switch (OutputAddress.Country)
				{
					case "CANADA":
						country = "CA";
						break;
					case "UNITED STATES":
						country = "US";
						break;
					default:
						country = OutputAddress.Country;
						break;
				}
				bool countryFound = false;
				for(int i = 0; i < this.ddlCountry.Items.Count; i++)
				{
					if(ddlCountry.Items[i].Value == country)
					{
						ddlCountry.SelectedIndex = i;
						countryFound = true;
						break;
					}
				}
				if (countryFound == false)
					ddlCountry.SelectedIndex = 0;

				//Display Status 
				if (ChangeStatus)
					AddressHygieneStatusLabel.Text = "Address Updated";
				else
					AddressHygieneStatusLabel.Text = "Address Validated";
			}
		}
	}
}
