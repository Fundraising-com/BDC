namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSP.WebControl;
	using Business.com.ses.ws.AddressHygiene;

	/// <summary>
	///		Summary description for AddressMaintenanceControl.
	/// </summary>
	public partial class Address : System.Web.UI.UserControl
	{
		protected QSPFulfillment.CommonWeb.UC.AddressHygiene AddressHygieneControl;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				this.AddressHygieneStatusLabel.Text = String.Empty;
				this.AddressHygieneVisible = false;

				//this.DataBind();
			}
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
		}

		#region Events
		//Event signifying address has been returned
		public event EventHandler AddressHygienedEvent;
		#endregion

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
			this.AddressHygieneControl.OutputAddress += new QSPFulfillment.CommonWeb.UC.OutputAddressEventHandler(AddressSelected);

		}
		#endregion

		#region ViewState

		public int AddressID 
		{
			get 
			{
				if(this.ViewState["AddressID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["AddressID"]);
			}
			set 
			{
				this.ViewState["AddressID"] = value;
			}
		}

		public int AddressListID 
		{
			get 
			{
				if(this.ViewState["AddressListID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["AddressListID"]);
			}
			set 
			{
				this.ViewState["AddressListID"] = value;
			}		
		}

		public bool Required 
		{
			get
			{
				if(this.ViewState["Required"] == null)
					return true;

				return Convert.ToBoolean(this.ViewState["Required"]);
			}
			set 
			{
				this.ViewState["Required"] = value;
				SetRequired();
			}
		}

		public bool Enabled 
		{
			get 
			{
				if(this.ViewState["Enabled"] == null)
					return true;

				return Convert.ToBoolean(this.ViewState["Enabled"]);
			}
			set 
			{
				this.ViewState["Enabled"] = value;
				SetEnabled();
			}
		}

		private bool AddressHygiened
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

		public bool IsAddressHygiened
		{
			get 
			{
				if(ViewState["AddressHygiened"] != null) 
					return (bool)ViewState["AddressHygiened"];
				else
					return false;
			}
		}		

		#endregion

		#region Controls

		public TextBoxReq Address1Control
		{
			get 
			{
				return this.Address1Tbx;
			}
		}

		public System.Web.UI.WebControls.TextBox Address2Control
		{
			get 
			{
				return this.Address2Tbx;
			}
		}

		public TextBoxReq CityControl
		{
			get 
			{
				return this.CityTbx;
			}
		}

		public System.Web.UI.WebControls.TextBox CountyControl
		{
			get 
			{
				return this.CountyControl;
			}
		}

		public DropDownListProvince StateProvinceControl
		{
			get 
			{
				return this.StateProvinceDDL;
			}
		}

		public PostalCode PostalCodeControl
		{
			get 
			{
				return this.PostalCodeTbx;
			}
		}

		public DropDownListReq CountryControl
		{
			get 
			{
				return this.CountryDDL;
			}
		}

		#endregion

		#region Properties

		public string Address1 
		{
			get 
			{
				return this.Address1Tbx.Text;
			}
			set 
			{
				this.Address1Tbx.Text = value;
			}
		}

		public string Address2
		{
			get 
			{
				return this.Address2Tbx.Text;
			}
			set 
			{
				this.Address2Tbx.Text = value;
			}
		}

		public string City 
		{
			get 
			{
				return this.CityTbx.Text;
			}
			set 
			{
				this.CityTbx.Text = value;
			}
		}

		public string County 
		{
			get 
			{
				return this.CountyTbx.Text;
			}
			set 
			{
				this.CountyTbx.Text = value;
			}
		}

		public string StateProvince
		{
			get 
			{
				return this.StateProvinceDDL.SelectedValue;
			}
			set 
			{
				this.StateProvinceDDL.SelectedIndex = this.StateProvinceDDL.Items.IndexOf(this.StateProvinceDDL.Items.FindByValue(value));
			}
		}

		public string StateProvinceFull
		{
			get 
			{
				return this.StateProvinceDDL.SelectedItem.Text;
			}
			set 
			{
				this.StateProvinceDDL.SelectedIndex = this.StateProvinceDDL.Items.IndexOf(this.StateProvinceDDL.Items.FindByText(value));
			}
		}

		public string PostalCode 
		{
			get 
			{
				return this.PostalCodeTbx.Text.Split(new char[] {'-'}, 2).GetValue(0).ToString();
			}
			set 
			{
				if (PostalCode2.Length > 0)
                    this.PostalCodeTbx.Text = value + "-" + this.PostalCode2;
				else
					this.PostalCodeTbx.Text = value;
			}
		}

		public string PostalCode2 
		{
			get 
			{
				string[] postCodes = this.PostalCodeTbx.Text.Split(new char[] {'-'}, 2);
				if (postCodes.Length == 2)
					return Convert.ToString(postCodes.GetValue(1));
				else
					return String.Empty;
			}
			set 
			{
				if (value.Length > 0)
					this.PostalCodeTbx.Text = PostalCode + "-" + value;
				else
					this.PostalCodeTbx.Text = PostalCode;
			}
		}

		public string Country
		{
			get 
			{
				return this.CountryDDL.SelectedValue;
			}
			set 
			{
				this.CountryDDL.SelectedIndex = this.CountryDDL.Items.IndexOf(this.CountryDDL.Items.FindByValue(value));
				this.SetCountrySpecificInfo();
			}
		}

		public string CountryFull
		{
			get 
			{
				return this.CountryDDL.SelectedItem.Text;
			}
			set 
			{
				this.CountryDDL.SelectedIndex = this.CountryDDL.Items.IndexOf(this.CountryDDL.Items.FindByText(value));
			}
		}

		public System.Web.UI.AttributeCollection Address1Attributes
		{
			get 
			{
				return this.Address1Tbx.Attributes;
			}
		}

		public System.Web.UI.AttributeCollection Address2Attributes
		{
			get 
			{
				return this.Address2Tbx.Attributes;
			}
		}

		public System.Web.UI.AttributeCollection CityAttributes
		{
			get 
			{
				return this.CityTbx.Attributes;
			}
		}

		public System.Web.UI.AttributeCollection CountyAttributes
		{
			get 
			{
				return this.CountyTbx.Attributes;
			}
		}

		public System.Web.UI.AttributeCollection StateProvinceAttributes
		{
			get 
			{
				return this.StateProvinceDDL.Attributes;
			}
		}

		public System.Web.UI.AttributeCollection PostalCodeAttributes
		{
			get 
			{
				return this.PostalCodeTbx.Attributes;
			}
		}

		public System.Web.UI.AttributeCollection CountryAttributes
		{
			get 
			{
				return this.CountryDDL.Attributes;
			}
		}

		public bool Address1Enabled
		{
			get 
			{
				return this.Address1Tbx.Enabled;
			}
			set 
			{
				this.Address1Tbx.Enabled = value;
			}
		}

		public bool Address2Enabled
		{
			get 
			{
				return this.Address2Tbx.Enabled;
			}
			set 
			{
				this.Address2Tbx.Enabled = value;
			}
		}

		public bool CityEnabled
		{
			get 
			{
				return this.CityTbx.Enabled;
			}
			set 
			{
				this.CityTbx.Enabled = value;
			}
		}

		public bool CountyEnabled
		{
			get 
			{
				return this.CountyTbx.Enabled;
			}
			set 
			{
				this.CountyTbx.Enabled = value;
			}
		}

		public bool StateProvinceEnabled
		{
			get 
			{
				return this.StateProvinceDDL.Enabled;
			}
			set 
			{
				this.StateProvinceDDL.Enabled = value;
			}
		}

		public bool PostalCodeEnabled
		{
			get 
			{
				return this.PostalCodeTbx.Enabled;
			}
			set 
			{
				this.PostalCodeTbx.Enabled = value;
			}
		}

		public bool CountryEnabled
		{
			get 
			{
				return this.CountryDDL.Enabled;
			}
			set 
			{
				this.CountryDDL.Enabled = value;
			}
		}

		public bool Address1Visible
		{
			get 
			{
				return this.Address1Tbx.Visible;
			}
			set 
			{
				this.Address1Tbx.Visible = value;
				this.Address1Label.Visible = value;
			}
		}

		public bool Address2Visible
		{
			get 
			{
				return this.Address2Tbx.Visible;
			}
			set 
			{
				this.Address2Tbx.Visible = value;
				this.Address2Label.Visible = value;
			}
		}

		public bool CityVisible
		{
			get 
			{
				return this.CityTbx.Visible;
			}
			set 
			{
				this.CityTbx.Visible = value;
				this.CityLabel.Visible = value;
			}
		}

		public bool CountyVisible
		{
			get 
			{
				return this.CountyTbx.Visible;
			}
			set 
			{
				this.CountyTbx.Visible = value;
				this.CountyLabel.Visible = value;
			}
		}

		public bool StateProvinceVisible
		{
			get 
			{
				return this.StateProvinceDDL.Visible;
			}
			set 
			{
				this.StateProvinceDDL.Visible = value;
				this.StateProvinceLabel.Visible = value;
			}
		}

		public bool PostalCodeVisible
		{
			get 
			{
				return this.PostalCodeTbx.Visible;
			}
			set 
			{
				this.PostalCodeTbx.Visible = value;
				this.PostalCodeLabel.Visible = value;
			}
		}

		public bool CountryVisible
		{
			get 
			{
				return this.CountryDDL.Visible;
			}
			set 
			{
				this.CountryDDL.Visible = value;
				this.CountryLabel.Visible = value;
			}
		}

		public bool AddressValidateBtnVisible
		{
			get
			{
				return this.ValidateAddressButton.Visible;
			}
			set
			{
				this.ValidateAddressButton.Visible = value;
			}
		}

		public bool AddressValidateBtnEnabled
		{
			get
			{
				return this.ValidateAddressButton.Enabled;
			}
			set
			{
				this.ValidateAddressButton.Enabled = value;
			}
		}

		public string AddressHygieneStatusLbl
		{
			get
			{
				return this.AddressHygieneStatusLabel.Text;
			}
			set
			{
				this.AddressHygieneStatusLabel.Text = value;
			}
		}
		
		public bool AddressHygieneVisible
		{
			get
			{
				return this.AddressHygieneControl.Visible;
			}
			set
			{
				this.AddressHygieneControl.Visible = value;
			}
		}
		
		public bool AddressHygieneStatusLblVisible
		{
			get
			{
				return this.AddressHygieneStatusLabel.Visible;
			}
			set
			{
				this.AddressHygieneStatusLabel.Visible = value;
			}
		}

		public bool IsAddressHygieneEnabled
		{
			get
			{
				if (this.AddressHygieneControl.IsAddressHygieneEnabled)
					return true;
				else
					return false;
			}
		}

		#endregion

		public override void DataBind()
		{
			LoadData();
		}

		private void LoadData() 
		{
			LoadDataDDL();
		}

		private void SetValueEmpty() 
		{
			this.Address1 = "";
			this.Address2 = "";
			this.City = "";
			this.County = "";
			this.StateProvince = "";
			this.PostalCode = "";
			this.Country = "";
		}

		private void SetRequired() 
		{
			this.Address1Tbx.Required = this.Required;
			this.CityTbx.Required = this.Required;
			this.PostalCodeTbx.Required = this.Required;
		}

		private void SetEnabled() 
		{
			this.Address1Tbx.ReadOnly = !this.Enabled;
			this.Address2Tbx.ReadOnly = !this.Enabled;
			this.CityTbx.ReadOnly = !this.Enabled;
			this.CountyTbx.ReadOnly = !this.Enabled;
			this.StateProvinceDDL.Enabled = this.Enabled;
			this.PostalCodeTbx.ReadOnly = !this.Enabled;
			this.CountryDDL.Enabled = this.Enabled;
		}

		private void LoadDataDDL() 
		{
			LoadDataDDLCountry();
			SetCountrySpecificInfo();
		}

		private void SetCountrySpecificInfo()
		{
			if (this.Country == "CA")
			{
				this.StateProvinceDDL.Code = QSP.WebControl.DataAccess.Business.CountryCode.CA;
				this.StateProvinceLabel.Text = "Province";
				this.CountyVisible = false;
				this.PostalCodeLabel.Text = "Postal Code";
			}
			else if (this.Country == "US")
			{
				this.StateProvinceDDL.Code = QSP.WebControl.DataAccess.Business.CountryCode.US;
				this.StateProvinceLabel.Text = "State";
				this.CountyVisible = true;
				this.PostalCodeLabel.Text = "Zip Code";
			}
			else
			{
				this.StateProvinceDDL.Code = QSP.WebControl.DataAccess.Business.CountryCode.All;
				this.StateProvinceLabel.Text = "Province / State";
				this.CountyVisible = true;
				this.PostalCodeLabel.Text = "Postal Code / Zip Code";
			}

			this.StateProvinceDDL.DataBind();

		}

		private void LoadDataDDLCountry() 
		{
			if(this.CountryDDL.Items.Count == 0) 
			{
				this.CountryDDL.Items.Add(new ListItem("Canada", "CA"));
				this.CountryDDL.Items.Add(new ListItem("United States", "US"));
			}
		}

		public void SetAddressHygieneEnabled(bool Enabled)
		{
			if (!this.AddressHygieneControl.IsAddressHygieneEnabled)
				Enabled = false;

			this.AddressValidateBtnVisible = Enabled;
			this.AddressHygieneStatusLblVisible = Enabled;
			this.AddressHygieneVisible = Enabled;
		}

		protected void ValidateAddressButton_Click(object sender, System.EventArgs e)
		{
			AddressHygiened = true;
			AddressHygieneStatusLabel.Text = String.Empty;
			AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Blue;
			
			bool enableSuggestionList = true;
			
			try
			{
				if (AddressHygieneControl.DoAddressHygiene(GetAddressFromFields(), enableSuggestionList))
				{
					AddressHygieneControl.Visible = true;
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

			AddressHygienedEvent(this, null);
		}

		private Business.com.ses.ws.AddressHygiene.Address GetAddressFromFields()
		{
			Business.com.ses.ws.AddressHygiene.Address address = new Business.com.ses.ws.AddressHygiene.Address();

			address.Address1 = this.Address1;
			address.Address2 = this.Address2;
			address.City = this.City;
			address.County = this.County;
			address.Region = this.StateProvince.ToUpper(System.Globalization.CultureInfo.InvariantCulture);
			if (this.PostalCode.Length == 6) //Address Hygiene requires space within Canadian Postal Code
				address.PostCode = this.PostalCode.Substring(0, 3) + " " + this.PostalCode.Substring(3, 3);
			else
				address.PostCode = this.PostalCode;
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
				AddressHygieneStatusLabel.Text = "Error: " + AddressHygieneControl.GetErrorFromFault(OutputAddress.Fault.ToString());
			}
				//Check if there was a Suggestion List error
			else if (OutputAddress.SuggestionListInformation.Error != SuggestionListError.None && OutputAddress.SuggestionListInformation.Error != SuggestionListError.MoreInformationRequired)
			{
				AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
				AddressHygieneStatusLabel.Text = "Error: " + AddressHygieneControl.GetErrorFromFault(OutputAddress.SuggestionListInformation.Error.ToString());
			}
			else
			{
				Address1 = OutputAddress.Address1;
				Address2 = OutputAddress.Address2;
				City = OutputAddress.City;
				if (OutputAddress.PostCode.Length == 7) //Address Hygiene returns a space between Canadian Postal Code
					PostalCode = OutputAddress.PostCode.Substring(0, 3) + OutputAddress.PostCode.Substring(4, 3);
				else
                    PostalCode = OutputAddress.PostCode;
				PostalCode2 = OutputAddress.PostCode2;

				string stateProvince = OutputAddress.Region;
				bool stateProvinceFound = false;
				for(int i = 0; i < this.StateProvinceDDL.Items.Count; i++)
				{
					if(StateProvinceDDL.Items[i].Value == stateProvince)
					{
						StateProvinceDDL.SelectedIndex = i;
						stateProvinceFound = true;
						break;
					}
				}
				if (stateProvinceFound == false)
					StateProvinceDDL.SelectedIndex = 0;

				//Display Status 
				if (ChangeStatus)
					AddressHygieneStatusLabel.Text = "Address Updated";
				else
					AddressHygieneStatusLabel.Text = "Address Validated";
			}
		}
	}
}