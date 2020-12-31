namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSP.WebControl;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSP.WebControl.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using Business.com.ses.ws.AddressHygiene;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class FulfillmentHouseMaintenanceControl : MarketingMgtControl
	{
		private const string DEFAULT_EFFORT_KEY_REQUIRED = "N";

		protected QSPFulfillment.MarketingMgt.Control.FulfillmentHouseContactMaintenanceControl ctrlFulfillmentHouseContactMaintenanceControl;
		public System.Web.UI.WebControls.Label AddressHygieneStatusLabel;
		protected QSPFulfillment.MarketingMgt.Control.FulfillmentHouseContactSearchControl ctrlFulfillmentHouseContactSearchControl;
		protected QSPFulfillment.CommonWeb.UC.AddressHygiene ctrlAddressHygiene;

		public event SelectFulfillmentHouseEventHandler FulfillmentHouseSaved;
		public event System.EventHandler FulfillmentHouseCancelled;

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
			this.ctrlFulfillmentHouseContactSearchControl.SelectFulfillmentHouseContactClick += new SelectFulfillmentHouseContactEventHandler(ctrlFulfillmentHouseContactSearchControl_SelectFulfillmentHouseContactClick);
			this.ctrlFulfillmentHouseContactSearchControl.DeleteFulfillmentHouseContactClick += new SelectFulfillmentHouseContactEventHandler(ctrlFulfillmentHouseContactSearchControl_DeleteFulfillmentHouseContactClick);
			this.ctrlFulfillmentHouseContactMaintenanceControl.FulfillmentHouseContactSaved += new SelectFulfillmentHouseContactEventHandler(ctrlFulfillmentHouseContactMaintenanceControl_FulfillmentHouseContactSaved);
			this.ctrlFulfillmentHouseContactMaintenanceControl.FulfillmentHouseContactCancelled += new EventHandler(ctrlFulfillmentHouseContactMaintenanceControl_FulfillmentHouseContactCancelled);
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
				SaveFulfillmentHouseInformation();
				this.DataBind();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			SelectFulfillmentHouseClickedArgs args;

			try 
			{
				if(this.btnCancel.Text == "Done") 
				{
					args = new SelectFulfillmentHouseClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.FulfillmentHouse());

					if(FulfillmentHouseSaved != null)
						FulfillmentHouseSaved(sender, args);
				} 
				else 
				{
					if(FulfillmentHouseCancelled != null)
						FulfillmentHouseCancelled(sender, e);
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
				SaveFulfillmentHouseInformation();

				this.ctrlFulfillmentHouseContactMaintenanceControl.FulfillmentHouseContactInfo = new FulfillmentHouseContact();
				this.ctrlFulfillmentHouseContactMaintenanceControl.FulfillmentHouseContactInfo.FulfillmentHouseName = Name;
				this.ctrlFulfillmentHouseContactMaintenanceControl.MainContactID = this.ctrlFulfillmentHouseContactSearchControl.MainContactID;

				this.FulfillmentHouseContactMode = true;
				this.DataBind();
				this.btnCancel.Text = "Cancel";
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlFulfillmentHouseContactSearchControl_SelectFulfillmentHouseContactClick(object sender, SelectFulfillmentHouseContactClickedArgs e)
		{
			SaveFulfillmentHouseInformation();

			this.ctrlFulfillmentHouseContactMaintenanceControl.FulfillmentHouseContactInfo = e.FulfillmentHouseContactInfo;
			this.ctrlFulfillmentHouseContactMaintenanceControl.MainContactID = this.ctrlFulfillmentHouseContactSearchControl.MainContactID;

			this.FulfillmentHouseContactMode = true;
			this.DataBind();
			this.btnCancel.Text = "Cancel";

		}

		private void ctrlFulfillmentHouseContactSearchControl_DeleteFulfillmentHouseContactClick(object sender, SelectFulfillmentHouseContactClickedArgs e)
		{
			this.DataBind();
		}

		private void ctrlFulfillmentHouseContactMaintenanceControl_FulfillmentHouseContactSaved(object sender, SelectFulfillmentHouseContactClickedArgs e)
		{
			this.FulfillmentHouseContactMode = false;
			this.DataBind();
		}

		private void ctrlFulfillmentHouseContactMaintenanceControl_FulfillmentHouseContactCancelled(object sender, EventArgs e)
		{
			this.FulfillmentHouseContactMode = false;
			this.DataBind();
		}

		public FulfillmentHouse FulfillmentHouseInfo 
		{
			get 
			{
				return (FulfillmentHouse) ViewState["FulfillmentHouseInfo"];
			}
			set 
			{
				ViewState["FulfillmentHouseInfo"] = value;
			}
		}

		private bool FulfillmentHouseContactMode 
		{
			get 
			{
				if(this.ViewState["FulfillmentHouseContactMode"] == null)
					this.ViewState["FulfillmentHouseContactMode"] = false;

				return Convert.ToBoolean(this.ViewState["FulfillmentHouseContactMode"]);
			}
			set 
			{
				this.ViewState["FulfillmentHouseContactMode"] = value;
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

		private string Name
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

		private string StateProvince
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

		private string Zip
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

		private InterfaceMedia InterfaceMedia
		{
			get 
			{
				return (InterfaceMedia) Convert.ToInt32(this.ddlInterfaceMedia.SelectedValue);
			}
			set 
			{
				this.ddlInterfaceMedia.SelectedIndex = this.ddlInterfaceMedia.Items.IndexOf(this.ddlInterfaceMedia.Items.FindByValue(((int) value).ToString()));
			}
		}

		private string InterfaceMediaDescription
		{
			get 
			{
				return this.ddlInterfaceMedia.SelectedItem.Text;
			}
			set 
			{
				this.ddlInterfaceMedia.SelectedIndex = this.ddlInterfaceMedia.Items.IndexOf(this.ddlInterfaceMedia.Items.FindByValue(value));
			}
		}

		private InterfaceLayout InterfaceLayout
		{
			get 
			{
				return (InterfaceLayout) Convert.ToInt32(this.ddlInterfaceLayout.SelectedValue);
			}
			set 
			{
				this.ddlInterfaceLayout.SelectedIndex = this.ddlInterfaceLayout.Items.IndexOf(this.ddlInterfaceLayout.Items.FindByValue(((int) value).ToString()));
			}
		}

		private string InterfaceLayoutDescription
		{
			get 
			{
				return this.ddlInterfaceLayout.SelectedItem.Text;
			}
			set 
			{
				this.ddlInterfaceLayout.SelectedIndex = this.ddlInterfaceLayout.Items.IndexOf(this.ddlInterfaceLayout.Items.FindByText(value));
			}
		}

		private TransmissionMethod TransmissionMethod
		{
			get 
			{
				return (TransmissionMethod) Convert.ToInt32(this.ddlTransmissionMethod.SelectedValue);
			}
			set 
			{
				this.ddlTransmissionMethod.SelectedIndex = this.ddlTransmissionMethod.Items.IndexOf(this.ddlTransmissionMethod.Items.FindByValue(((int) value).ToString()));
			}
		}

		private string TransmissionMethodDescription
		{
			get 
			{
				return this.ddlTransmissionMethod.SelectedItem.Text;
			}
			set 
			{
				this.ddlTransmissionMethod.SelectedIndex = this.ddlTransmissionMethod.Items.IndexOf(this.ddlTransmissionMethod.Items.FindByText(value));
			}
		}

		private bool HardCopy
		{
			get 
			{
				if (Convert.ToInt32(this.rblHardCopy.SelectedIndex) == 0)
					return true;
				else
					return false;
			}
			set 
			{
				if (value == false)
					this.rblHardCopy.SelectedIndex = 1;
				else
					this.rblHardCopy.SelectedIndex = 0;
			}
		}

		private string QSPAgencyCode
		{
			get 
			{
				return this.tbxQSPAgencyCode.Text;
			}
			set 
			{
				this.tbxQSPAgencyCode.Text = value;
			}
		}

		private string IsEffortKeyRequired
		{
			get 
			{
				string isEffortKeyRequired = DEFAULT_EFFORT_KEY_REQUIRED;

				if(ViewState["IsEffortKeyRequired"] != null) 
				{
					isEffortKeyRequired = ViewState["IsEffortKeyRequired"].ToString();
				}

				return isEffortKeyRequired;
			}
			set 
			{
				ViewState["IsEffortKeyRequired"] = value;
			}
		}

		private string PayGroupLookUpCode 
		{
			get 
			{
				string payGroupLookUpCode = String.Empty;

				if(ViewState["PayGroupLookUpCode"] != null) 
				{
					payGroupLookUpCode = ViewState["PayGroupLookUpCode"].ToString();
				}

				return payGroupLookUpCode;
			}
			set 
			{
				ViewState["PayGroupLookUpCode"] = value;
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

		#region Initial Values

		private string InitialName 
		{
			get 
			{
				string initialName = String.Empty;

				if(ViewState["InitialName"] != null) 
				{
					initialName = ViewState["InitialName"].ToString();
				}

				return initialName;
			}
			set 
			{
				ViewState["InitialName"] = value;
			}
		}

		private string InitialAddress1 
		{
			get 
			{
				string initialAddress1 = String.Empty;

				if(ViewState["InitialAddress1"] != null) 
				{
					initialAddress1 = ViewState["InitialAddress1"].ToString();
				}

				return initialAddress1;
			}
			set 
			{
				ViewState["InitialAddress1"] = value;
			}
		}

		private string InitialAddress2 
		{
			get 
			{
				string initialAddress2 = String.Empty;

				if(ViewState["InitialAddress2"] != null) 
				{
					initialAddress2 = ViewState["InitialAddress2"].ToString();
				}

				return initialAddress2;
			}
			set 
			{
				ViewState["InitialAddress2"] = value;
			}
		}

		private string InitialCity 
		{
			get 
			{
				string initialCity = String.Empty;

				if(ViewState["InitialCity"] != null) 
				{
					initialCity = ViewState["InitialCity"].ToString();
				}

				return initialCity;
			}
			set 
			{
				ViewState["InitialCity"] = value;
			}
		}

		private string InitialStateProvince 
		{
			get 
			{
				string initialStateProvince = String.Empty;

				if(ViewState["InitialStateProvince"] != null) 
				{
					initialStateProvince = ViewState["InitialStateProvince"].ToString();
				}

				return initialStateProvince;
			}
			set 
			{
				ViewState["InitialStateProvince"] = value;
			}
		}

		private string InitialZip 
		{
			get 
			{
				string initialZip = String.Empty;

				if(ViewState["InitialZip"] != null) 
				{
					initialZip = ViewState["InitialZip"].ToString();
				}

				return initialZip;
			}
			set 
			{
				ViewState["InitialZip"] = value;
			}
		}

		private string InitialCountry 
		{
			get 
			{
				string initialCountry = String.Empty;

				if(ViewState["InitialCountry"] != null) 
				{
					initialCountry = ViewState["InitialCountry"].ToString();
				}

				return initialCountry;
			}
			set 
			{
				ViewState["InitialCountry"] = value;
			}
		}

		#endregion

		#region Catalyst Data

		private bool IsCatalystDataChanged 
		{
			get 
			{
				return (IsNameChanged || IsAddress1Changed || IsAddress2Changed || IsCityChanged || IsStateProvinceChanged || IsZipChanged || IsCountryChanged);
			}
		}

		private bool IsCatalystProductDataChanged 
		{
			get 
			{
				return (IsCityChanged || IsStateProvinceChanged || IsCountryChanged);
			}
		}

		private bool IsNameChanged 
		{
			get 
			{
				return (InitialName != String.Empty && InitialName != Name);
			}
		}

		private bool IsAddress1Changed 
		{
			get 
			{
				return (InitialAddress1 != String.Empty && InitialAddress1 != Address1);
			}
		}

		private bool IsAddress2Changed 
		{
			get 
			{
				return (InitialAddress2 != String.Empty && InitialAddress2 != Address2);
			}
		}
		
		private bool IsCityChanged 
		{
			get 
			{
				return (InitialCity != String.Empty && InitialCity != City);
			}
		}

		private bool IsStateProvinceChanged 
		{
			get 
			{
				return (InitialStateProvince != String.Empty && InitialStateProvince != StateProvince);
			}
		}

		private bool IsZipChanged 
		{
			get 
			{
				return (InitialZip != String.Empty && InitialZip != Zip);
			}
		}

		private bool IsCountryChanged 
		{
			get 
			{
				return (InitialCountry != String.Empty && InitialCountry != Country);
			}
		}

		#endregion

		public override void DataBind()
		{
			if(!this.FulfillmentHouseContactMode) 
			{
				SetValueDDL();

				if(this.FulfillmentHouseInfo != null && FulfillmentHouseInfo.FulfillmentHouseNumber != 0)
				{
					SetValue();
					this.ctrlFulfillmentHouseContactSearchControl.FulfillmentHouseID = FulfillmentHouseInfo.FulfillmentHouseNumber;
					this.ctrlFulfillmentHouseContactSearchControl.DataBind();
					this.lblInstructions.Visible = true;
					this.ctrlFulfillmentHouseContactSearchControl.Visible = true;
					this.btnCreateNew.Visible = true;
				} 
				else 
				{
					SetValueEmpty();
					this.lblInstructions.Visible = false;
					this.ctrlFulfillmentHouseContactSearchControl.Visible = false;
					this.btnCreateNew.Visible = false;
				}

				this.tblMaintenance.Visible = true;
				this.ctrlFulfillmentHouseContactMaintenanceControl.Visible = false;
			} 
			else 
			{
				this.ctrlFulfillmentHouseContactMaintenanceControl.FulfillmentHouseID = FulfillmentHouseInfo.FulfillmentHouseNumber;
				this.ctrlFulfillmentHouseContactMaintenanceControl.DataBind();
				this.tblMaintenance.Visible = false;
				this.ctrlFulfillmentHouseContactMaintenanceControl.Visible = true;
			}
		}

		private void SetValue() 
		{
			Name = this.FulfillmentHouseInfo.Name;
			Status = this.FulfillmentHouseInfo.Status;
			InterfaceMedia = this.FulfillmentHouseInfo.InterfaceMedia;
			InterfaceLayout = this.FulfillmentHouseInfo.InterfaceLayout;
			TransmissionMethod = this.FulfillmentHouseInfo.TransmissionMethod;
			HardCopy = this.FulfillmentHouseInfo.HardCopy;
			QSPAgencyCode = this.FulfillmentHouseInfo.QSPAgencyCode;
			IsEffortKeyRequired = this.FulfillmentHouseInfo.IsEffortKeyRequired;
			Address1 = this.FulfillmentHouseInfo.Address1;
			Address2 = this.FulfillmentHouseInfo.Address2;
			City = this.FulfillmentHouseInfo.City;
			StateProvince = this.FulfillmentHouseInfo.StateProvince;
			Zip = this.FulfillmentHouseInfo.Zip;
			Country = this.FulfillmentHouseInfo.Country;
			PayGroupLookUpCode = this.FulfillmentHouseInfo.PayGroupLookUpCode;
			ctrlFulfillmentHouseContactSearchControl.FulfillmentHouseID = this.FulfillmentHouseInfo.FulfillmentHouseNumber;
			ctrlFulfillmentHouseContactMaintenanceControl.FulfillmentHouseID = this.FulfillmentHouseInfo.FulfillmentHouseNumber;

			SetInitialValues();
		}

		private void SetValueEmpty() 
		{
			Name = String.Empty;
			Status = String.Empty;
			InterfaceMedia = InterfaceMedia.Excel;
			InterfaceLayout = InterfaceLayout.InterfaceLayout7;
			TransmissionMethod = TransmissionMethod.Email;
			HardCopy = false;
			QSPAgencyCode = String.Empty;
			IsEffortKeyRequired = String.Empty;
			Address1 = String.Empty;
			Address2 = String.Empty;
			City = String.Empty;
			StateProvince = String.Empty;
			Zip = String.Empty;
			Country = String.Empty;
			PayGroupLookUpCode = String.Empty;
			this.ctrlFulfillmentHouseContactSearchControl.FulfillmentHouseID = 0;
		}

		private void SetInitialValues() 
		{
			InitialName = Name;
			InitialAddress1 = Address1;
			InitialAddress2 = Address2;
			InitialCity = City;
			InitialStateProvince = StateProvince;
			InitialZip = Zip;
			InitialCountry = Country;
		}

		private void SetInitialValuesEmpty() 
		{
			InitialName = String.Empty;
			InitialAddress1 = String.Empty;
			InitialAddress2 = String.Empty;
			InitialCity = String.Empty;
			InitialStateProvince = String.Empty;
			InitialZip = String.Empty;
			InitialCountry = String.Empty;
		}

		private void SetValueDDL() 
		{
			SetValueDDLStatus();
			SetValueDDLStateProvince();
			SetValueDDLCountry();
			SetValueDDLInterfaceMedia();
			SetValueDDLInterfaceLayout();
			SetValueDDLTransmissionMethod();
		}

		private void SetValueDDLStatus() 
		{
			if(this.ddlStatus.Items.Count == 0)
			{
				this.ddlStatus.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, ""));
				this.ddlStatus.Items.Add(new ListItem("Active", "ACTIVE"));
				this.ddlStatus.Items.Add(new ListItem("Inactive", "INACTIVE"));
			}
		}

		private void SetValueDDLStateProvince() 
		{
			this.ddlStateProvince.AsTextFirstRow = true;
			this.ddlStateProvince.TextFirstRow = MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY;
			this.ddlStateProvince.DataBind();
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

		private void SetValueDDLInterfaceMedia() 
		{
			if(this.ddlInterfaceMedia.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table, 32000);

				this.ddlInterfaceMedia.DataSource = Table;
				this.ddlInterfaceMedia.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				this.ddlInterfaceMedia.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlInterfaceMedia.DataBind();
			}
		}

		private void SetValueDDLInterfaceLayout() 
		{
			if(this.ddlInterfaceLayout.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table, 33000);

				foreach(DataRow row in Table.Rows) 
				{
					this.ddlInterfaceLayout.Items.Add(new ListItem(row[CodeDetailTable.FLD_DESCRIPTION].ToString(), row[CodeDetailTable.FLD_INSTANCE].ToString()));
				}
			}
		}

		private void SetValueDDLTransmissionMethod() 
		{
			if(this.ddlTransmissionMethod.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table, 60000);

				this.ddlTransmissionMethod.DataSource = Table;
				this.ddlTransmissionMethod.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				this.ddlTransmissionMethod.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlTransmissionMethod.DataBind();
			}
		}

		private void SaveFulfillmentHouseInformation() 
		{
			if(FulfillmentHouseInfo != null && FulfillmentHouseInfo.FulfillmentHouseNumber != 0) 
			{
				UpdateFulfillmentHouseInformation();
			} 
			else 
			{
				InsertFulfillmentHouseInformation();
			}

			FulfillmentHouseInfo.Status = Status;
			FulfillmentHouseInfo.Name = Name;
			FulfillmentHouseInfo.Address1 = Address1;
			FulfillmentHouseInfo.Address2 = Address2;
			FulfillmentHouseInfo.City = City;
			FulfillmentHouseInfo.StateProvince = StateProvince;
			FulfillmentHouseInfo.Zip = Zip;
			FulfillmentHouseInfo.Country = Country;
			FulfillmentHouseInfo.InterfaceMedia = InterfaceMedia;
			FulfillmentHouseInfo.InterfaceLayout = InterfaceLayout;
			FulfillmentHouseInfo.TransmissionMethod = TransmissionMethod;
			FulfillmentHouseInfo.HardCopy = HardCopy;
			FulfillmentHouseInfo.QSPAgencyCode = QSPAgencyCode;
			FulfillmentHouseInfo.IsEffortKeyRequired = IsEffortKeyRequired;

			this.btnCancel.Text = "Done";
		}

		private void InsertFulfillmentHouseInformation() 
		{
			FulfillmentHouseInfo = new FulfillmentHouse();
			FulfillmentHouseInfo.FulfillmentHouseNumber = this.Page.BusFulfillmentHouse.Insert(Status, Name, Address1, Address2, City, StateProvince, Zip, Country, InterfaceMedia, InterfaceLayout, TransmissionMethod, HardCopy, QSPAgencyCode, IsEffortKeyRequired);
		}

		private void UpdateFulfillmentHouseInformation() 
		{
			this.Page.BusFulfillmentHouse.Update(FulfillmentHouseInfo.FulfillmentHouseNumber, Status, Name, Address1, Address2, City, StateProvince, Zip, Country, InterfaceMedia, InterfaceLayout, TransmissionMethod, HardCopy, QSPAgencyCode, IsEffortKeyRequired);

			if(IsCatalystDataChanged) 
			{
				SendMail();
				SetInitialValuesEmpty();
			}
		}

		private void SendMail() 
		{
			CatalystDataMailMessage catalystDataMailMessage = new CatalystDataMailMessage();
			CatalystDataFulfillmentHouse catalystDataFulfillmentHouse = new CatalystDataFulfillmentHouse(InitialName, Name, InitialAddress1, Address1, InitialAddress2, Address2, InitialCity, City, InitialStateProvince, StateProvince, InitialZip, Zip);
			CatalystDataProduct catalystDataProduct = null;
			DataTable productTable = null;
			string vendorSiteName = String.Empty;
			string payGroupLookUpCode = String.Empty;

			catalystDataMailMessage.AddFulfillmentHouse(catalystDataFulfillmentHouse);

			if(IsCatalystProductDataChanged) 
			{
				productTable = new DataTable("Product");
				this.Page.BusProduct.SelectAllByFulfillmentHouseID(productTable, this.FulfillmentHouseInfo.FulfillmentHouseNumber);

				foreach(DataRow row in productTable.Rows) 
				{
					vendorSiteName = this.Page.BusFulfillmentHouse.ProcessVendorSiteName(StateProvince, City);
					payGroupLookUpCode = this.Page.BusFulfillmentHouse.ProcessPayGroupLookUpCode(Convert.ToInt32(row["Currency"]), PayGroupLookUpCode);
					catalystDataProduct = new CatalystDataProduct(row["Product_Code"].ToString(), Convert.ToInt32(row["Product_Year"]), row["Product_Season"].ToString(), row["RemitCode"].ToString(), row["RemitCode"].ToString(), row["Product_Sort_Name"].ToString(), row["Product_Sort_Name"].ToString(), row["VendorNumber"].ToString(), row["VendorNumber"].ToString(), row["VendorSiteName"].ToString(), vendorSiteName, row["PayGroupLookUpCode"].ToString(), payGroupLookUpCode);

					catalystDataMailMessage.AddProduct(catalystDataProduct);

                    this.Page.BusProduct.Update(Convert.ToInt32(row["Product_Instance"]), row["Product_Code"].ToString(), row["Product_Season"].ToString(), Convert.ToInt32(row["Product_Year"]), row["Product_Name"].ToString(), row["Product_Sort_Name"].ToString(), row["Lang"].ToString(), Convert.ToInt32(row["Category_Code"]), Convert.ToInt32(row["Status"]), Convert.ToInt32(row["Type"]), Convert.ToInt32(row["DaysLeadTime"]), Convert.ToInt32(row["Nbr_Of_Issues_Per_Year"]), Convert.ToInt32(row["Pub_Nbr"]), Convert.ToInt32(row["Fulfill_House_Nbr"]), row["Comment"].ToString(), row["VendorNumber"].ToString(), vendorSiteName, payGroupLookUpCode, Convert.ToInt32(row["Currency"]), row["GST_Registration_Nbr"].ToString(), row["HST_Registration_Nbr"].ToString(), row["PST_Registration_Nbr"].ToString(), row["OracleCode"].ToString(), row["Prize_Level"].ToString(), Convert.ToInt32(row["Prize_Level_Qty_Required"]), row["RemitCode"].ToString(), Convert.ToBoolean(row["IsQSPExclusive"]), row["EnglishDescription"].ToString(), row["FrenchDescription"].ToString(), row["VendorProductCode"].ToString());
				}
			}

			catalystDataMailMessage.Send();
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
			catch (Exception f)
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
			address.Region = StateProvince;
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
