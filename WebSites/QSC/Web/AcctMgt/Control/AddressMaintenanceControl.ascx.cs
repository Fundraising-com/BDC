namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using Business.Objects;
	using Common;
	using Common.TableDef;
	using QSP.WebControl;
	using Business.com.ses.ws.AddressHygiene;

	/// <summary>
	///		Summary description for AddressMaintenanceControl.
	/// </summary>
	public partial class AddressMaintenanceControl : AcctMgtControl
	{
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;

		private Business.Objects.Address addr;
		private Business.Objects.CAccount ac;
		private CAccountDataSet.CAccountRow rowAcc;
		protected System.Web.UI.WebControls.Label Label3;
		private AddressDataSet.AddressRow row;
		protected QSPFulfillment.CommonWeb.UC.AddressHygiene ctrlAddressHygiene;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				AddressHygieneStatusLabel.Text = String.Empty;
				this.btnValidateAddress.Attributes["onclick"] = "EnableSubmitButton();";

				SetAddressHygieneVisible(this.IsAddressHygieneEnabled);
			}
		}
		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript();

			if (this.IsAddressHygieneEnabled && !AddressHygiened)
			{
				AddAddressAttributes();
			}
			else
				RemoveAddressAtrributes();
		}

		private void AddAddressAttributes()
		{
			this.tbxAddress1.Attributes["onchange"] = "DisableSubmitButton();";
			this.tbxAddress2.Attributes["onchange"] = "DisableSubmitButton();";
			this.tbxCity.Attributes["onchange"] = "DisableSubmitButton();";
			this.ddlProvince.Attributes["onchange"] = "DisableSubmitButton();";
			this.tbxPostalCode.Attributes["onchange"] = "DisableSubmitButton();";
		}

		private void RemoveAddressAtrributes()
		{
			this.tbxAddress1.Attributes["onchange"] = "";
			this.tbxAddress2.Attributes["onchange"] = "";
			this.tbxCity.Attributes["onchange"] = "";
			this.ddlProvince.Attributes["onchange"] = "";
			this.tbxPostalCode.Attributes["onchange"] = "";
		}
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
			ctrlAddressHygiene.OutputAddress += new QSPFulfillment.CommonWeb.UC.OutputAddressEventHandler(AddressSelected);

		}
		#endregion

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

		public int AccountID 
		{
			get 
			{
				if(this.ViewState["AccountID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["AccountID"]);
			}
			set 
			{
				this.ViewState["AccountID"] = value;
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

		public Business.Objects.Address DataSource 
		{
			get 
			{
				return addr;
			}
			set 
			{
				addr = value;
			}
		}

		public int DefaultAddressType 
		{
			get 
			{
				if(this.ViewState["DefaultAddressType"] == null)
					return 54006;

				return Convert.ToInt32(this.ViewState["DefaultAddressType"]);
			}
			set 
			{
				this.ViewState["DefaultAddressType"] = value;
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

		public bool IsAddressHygieneEnabled
		{
			get
			{
				if (this.ctrlAddressHygiene.IsAddressHygieneEnabled)
					return true;
				else
					return false;
			}
		}

		#region Controls

		public TextBoxReq Address1Control
		{
			get 
			{
				return this.tbxAddress1;
			}
		}

		public System.Web.UI.WebControls.TextBox Address2Control
		{
			get 
			{
				return this.tbxAddress2;
			}
		}

		public TextBoxReq CityControl
		{
			get 
			{
				return this.tbxCity;
			}
		}

		public DropDownListProvince ProvinceControl
		{
			get 
			{
				return this.ddlProvince;
			}
		}

		public PostalCode PostalCodeControl
		{
			get 
			{
				return this.tbxPostalCode;
			}
		}


		public DropDownListReq CountryControl
		{
			get 
			{
			return this.ddlCountry;
			}
		}


		#endregion

		#region Fields

		private int AddressType 
		{
			get 
			{
				return Convert.ToInt32(this.ddlType.SelectedValue);
			}
			set 
			{
				this.ddlType.SelectedIndex = this.ddlType.Items.IndexOf(this.ddlType.Items.FindByValue(value.ToString()));
				this.lblType.Text = this.ddlType.SelectedItem.Text;
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
				return this.ddlProvince.SelectedValue;
			}
			set 
			{
				this.ddlProvince.SelectedIndex = this.ddlProvince.Items.IndexOf(this.ddlProvince.Items.FindByValue(value));
			}
		}

		private string PostalCode 
		{
			get 
			{
				return this.tbxPostalCode.Text;
			}
			set 
			{
				this.tbxPostalCode.Text = value;
			}
		}

		public string PostalCodeLabelText
		{
			get 
			{
				return this.PostalCodeLabel.Text;
			}
			set 
			{
				this.PostalCodeLabel.Text = value;
			}
		}

		public string ProvinceLabelText
		{
			get 
			{
				return this.StateProvinceLabel.Text;
			}
			set 
			{
				this.StateProvinceLabel.Text = value;
			}
		}

		public string Country
		{
			get 
			{
				return this.ddlCountry.SelectedValue;
			}
			set 
			{
				this.ddlCountry.SelectedIndex = this.ddlCountry.Items.IndexOf(this.ddlCountry.Items.FindByValue(value));
				this.SetCountrySpecificInfo();
			}
		}
		#endregion

		public CAccount oCAccount 
		{
			get 
			{
				return ac;
			}
		}
		public override void DataBind()
		{
			LoadData();
		}

		private void LoadData() 
		{
			LoadDataDDL();
			LoadDataDDLType();
			if(this.AddressID != 0 && this.DataSource != null) 
			{
				row = this.DataSource.dataSet.Address.FindByaddress_id(this.AddressID);

				if(row != null) 
				{

					SetValue();
					SetInitialValues(); 
				} 
				else 
				{
					SetValueEmpty();
				}
			} 
			else 
			{
				SetValueEmpty();
			}

           
		}

		private void SetValue() 
		{
			try 
			{
				this.Country=row.country;
				this.AddressType = row.address_type;
				this.Address1 = row.street1;
				this.Address2 = row.street2;
				this.City = row.city;
				this.Province = row.stateProvince;
				this.PostalCode = row.postal_code;
			} 
			catch (Exception ex) 
			{
				ApplicationError.ManageError(ex);
			}
		}

		private void SetValueEmpty() 
		{
			this.AddressType = this.DefaultAddressType;
			this.Address1 = "";
			this.Address2 = "";
			this.City = "";
			this.Province = "";
			this.PostalCode = "";
			this.Country="";
		}

		private void SetRequired() 
		{
			this.tbxAddress1.Required = this.Required;
			this.tbxCity.Required = this.Required;
			this.tbxPostalCode.Required = this.Required;
		}

		private void SetEnabled() 
		{
			this.ddlType.Enabled = this.Enabled;
			this.tbxAddress1.ReadOnly = !this.Enabled;
			this.tbxAddress2.ReadOnly = !this.Enabled;
			this.tbxCity.ReadOnly = !this.Enabled;
			this.ddlProvince.Enabled = this.Enabled;
			this.tbxPostalCode.ReadOnly = !this.Enabled;
		}

		private void LoadDataDDL() 
		{
			LoadDataDDLCountry();
			SetCountrySpecificInfo();

			//LoadDataDDLProvince();
			//LoadDataDDLType();
		}

		private void SetCountrySpecificInfo()
		{
			//row = this.DataSource.dataSet.Address.FindByaddress_id(this.AddressID);
			if (this.Country == "CA")
			//if (row.country == "CA")
			{
				this.ddlProvince.Code = QSP.WebControl.DataAccess.Business.CountryCode.CA;
				this.StateProvinceLabel.Text = "Province";
				//this.CountyVisible = false;
				this.PostalCodeLabel.Text = "Postal Code";
				this.PostalCodeControl.TypeDate= QSP.WebControl.CountryCheck.Canada;
			}
			//else if (row.country == "US")
			else if (this.Country == "US")
			{
				this.ddlProvince.Code = QSP.WebControl.DataAccess.Business.CountryCode.US;
				this.StateProvinceLabel.Text = "State";
				//this.CountyVisible = true;
				this.PostalCodeLabel.Text = "Zip Code";
				this.PostalCodeControl.TypeDate= QSP.WebControl.CountryCheck.US;
			}
			else
			{
				this.ddlProvince.Code = QSP.WebControl.DataAccess.Business.CountryCode.All;
				this.StateProvinceLabel.Text = "Province / State";
				//this.CountyVisible = true;
				this.PostalCodeLabel.Text = "Postal Code / Zip Code";
				this.PostalCodeControl.TypeDate= QSP.WebControl.CountryCheck.All;
			}

			this.ddlProvince.DataBind();

		}

		private void LoadDataDDLCountry() 
		{
			if(this.ddlCountry.Items.Count == 0) 
			{
				this.ddlCountry.Items.Add(new ListItem("Canada", "CA"));
				this.ddlCountry.Items.Add(new ListItem("United States", "US"));
			}
		}

		
		private void LoadDataDDLType() 
		{
			if(this.ddlType.Items.Count == 0) 
			{
				try 
				{
					CodeDetail cd = new CodeDetail(CodeHeaderInstance.AddressType);

					this.ddlType.DataSource = cd.dataSet;
					this.ddlType.DataMember = cd.dataSet.CodeDetail.TableName;
					this.ddlType.DataTextField = cd.dataSet.CodeDetail.DescriptionColumn.ColumnName;
					this.ddlType.DataValueField = cd.dataSet.CodeDetail.InstanceColumn.ColumnName;
					this.ddlType.DataBind();
				} 
				catch (MessageException ex) 
				{
					this.Page.SetPageError(ex);
				}
			}
		}

	
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

		private string InitialPostalCode 
		{
			get 
			{
				string initialPostalCode = String.Empty;

				if(ViewState["PostalCode"] != null) 
				{
					initialPostalCode = ViewState["PostalCode"].ToString();
				}

				return initialPostalCode;
			}
			set 
			{
				ViewState["PostalCode"] = value;
			}
		}

		private string InitialCountryCode 
		{
			get 
			{
				string initialCountryCode = String.Empty;

				if(ViewState["CountryCode"] != null) 
				{
					initialCountryCode = ViewState["CountryCode"].ToString();
				}

				return initialCountryCode;
			}
			set 
			{
				ViewState["CountryCode"] = value;
			}
		}


		#endregion
		
		private void SetInitialValues() 
		{
			InitialAddress1 = Address1;
			InitialAddress2 = Address2;
			InitialCity = City;
			InitialStateProvince = Province;
			InitialPostalCode = PostalCode;
			InitialCountryCode = this.Country;

		}

		private void SetInitialValuesEmpty() 
		{
			InitialName = String.Empty;
			InitialAddress1 = String.Empty;
			InitialAddress2 = String.Empty;
			InitialCity = String.Empty;
			InitialStateProvince = String.Empty;
			InitialPostalCode = String.Empty;
			InitialCountryCode = String.Empty;

		}

		private bool IsAddressChanged 
		{
			get 
			{
				return (IsAddress1Changed || IsAddress2Changed || IsCityChanged || IsStateProvinceChanged || IsPostalCodeChanged) || IsCountryChanged ;
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
				return (InitialStateProvince != String.Empty && InitialStateProvince != this.Province);
			}
		}

		private bool IsPostalCodeChanged 
		{
			get 
			{
				return (InitialPostalCode != String.Empty && InitialPostalCode != this.PostalCode);
			}
		}
		private bool IsCountryChanged 
		{
			get 
			{
				return (InitialCountryCode != String.Empty && InitialCountryCode != Country);
			}
		}
	
		public void Save() 
		{
			if(this.Visible) 
			{
				if(this.AddressID != 0) 
				{
					row = this.DataSource.dataSet.Address.FindByaddress_id(this.AddressID);

					if(row != null) 
					{
						/*if (IsAddressChanged) 
						{
							SendFinanceEMail();
							
						}*/
						FillAddressRow(row);
					} 
					else 
					{
						row = this.DataSource.dataSet.Address.NewAddressRow();

						FillAddressRow(row);
						row.AddressListID = this.AddressListID;

						this.DataSource.dataSet.Address.AddAddressRow(row);
					}
				} 
				else 
				{
					row = this.DataSource.dataSet.Address.NewAddressRow();

					FillAddressRow(row);
					row.AddressListID = this.AddressListID;

					this.DataSource.dataSet.Address.AddAddressRow(row);
				}
			} 
			else 
			{
				if(this.AddressID != 0) 
				{
					row = this.DataSource.dataSet.Address.FindByaddress_id(this.AddressID);

					if(row != null) 
					{
						row.Delete();
					}
				}
			}
		}

		private void FillAddressRow(AddressDataSet.AddressRow row) 
		{
			row.address_type = this.AddressType;
			row.street1 = this.Address1;
			row.street2 = this.Address2;
			row.city = this.City;
			row.stateProvince = this.Province;
			row.postal_code = this.PostalCode;
			row.zip4 = String.Empty;
			row.country = this.Country;
		}

		private void SendFinanceEMail() 
		{
			DataSet ds = new DataSet();
			DAL.CodeDetailDataAccess AddTypeData =	new DAL.CodeDetailDataAccess();
			ds = AddTypeData.GetCodeDescSelectone(this.AddressType); 
			string AddrType = ds.Tables[0].Rows[0]["Description"].ToString();

			Label Accountlbl = (Label) this.Parent.NamingContainer.FindControl("lblAccountID");
			string name = Accountlbl.Text;
			int accId=  Convert.ToInt32(name.Substring(9,name.Length-(name.IndexOf(':')+1)));

			ac = new CAccount(accId, this.Page.CurrentTransaction);
			rowAcc = ac.dataSet.CAccount.FindById(accId);

			name = name+ "("+rowAcc.Name+")";
			
			AddressChangeMailMessage addresschangeMailMessage = new AddressChangeMailMessage();
			AddressChangedDetail addressDetail = new AddressChangedDetail(name, name, InitialAddress1, Address1, InitialAddress2, Address2, InitialCity, City, InitialStateProvince, Province, InitialPostalCode, PostalCode,AddrType,rowAcc.VendorNumber);
					
			addresschangeMailMessage.AddChangedAddress(addressDetail);

			addresschangeMailMessage.Send();
		}

		protected void btnValidateAddress_Click(object sender, System.EventArgs e)
		{
			AddressHygiened = true;
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
		}

		private Business.com.ses.ws.AddressHygiene.Address GetAddressFromFields()
		{
			Business.com.ses.ws.AddressHygiene.Address address = new Business.com.ses.ws.AddressHygiene.Address();

			address.Address1 = Address1;
			address.Address2 = Address2;
			address.City = City;
			address.County = "";
			address.Region = Province;
			address.PostCode = PostalCode;
			address.PostCode2 = "";
			address.Country = Country;

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
					PostalCode = OutputAddress.PostCode.Substring(0, 3) + OutputAddress.PostCode.Substring(4, 3);
				else
					PostalCode = OutputAddress.PostCode;

				string stateProvince = OutputAddress.Region;
				bool stateProvinceFound = false;
				for(int i = 0; i < this.ddlProvince.Items.Count; i++)
				{
					if(ddlProvince.Items[i].Value == stateProvince)
					{
						ddlProvince.SelectedIndex = i;
						stateProvinceFound = true;
						break;
					}
				}
				if (stateProvinceFound == false)
					ddlProvince.SelectedIndex = 0;

				//Display Status 
				if (ChangeStatus)
					AddressHygieneStatusLabel.Text = "Address Updated";
				else
					AddressHygieneStatusLabel.Text = "Address Validated";
			}
		}

		protected void ddlCountry_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.PostalCode = "";
			SetCountrySpecificInfo();
		}

		public void SetAddressHygieneVisible(bool Visible)
		{
			if (!this.ctrlAddressHygiene.IsAddressHygieneEnabled)
				Visible = false;

			this.btnValidateAddress.Visible = Visible;
			this.AddressHygieneStatusLabel.Visible = Visible;
			this.ctrlAddressHygiene.Visible = Visible;
		}
	}
}