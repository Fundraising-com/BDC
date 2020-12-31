namespace QSPFulfillment.CustomerService.action
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Web.Services.Protocols;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSP.WebControl;
	using Business.com.ses.ws.AddressHygiene;

	/// <summary>
	///		Summary description for chaddaddress.
	/// </summary>
	public partial class ChangeNameAddress : CustomerServiceActionControl
	{
		protected const string MSG_HEADER = "Change address information";
		protected const int NO_PROBLEM_CODE = 0;
		protected ControlerSubscriptionForCOHI ctrlControlerSubscriptionForCOHI;
		protected System.Web.UI.WebControls.Button Button1;
		protected QSPFulfillment.CommonWeb.UC.AddressHygiene ctrlAddressHygiene;
		private int iCustomerRemitInstance = 0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadData();
			
				if(DataSource.Rows.Count!=0)
				{
					SetValue();

					ctrlControlerSubscriptionForCOHI.DataBind();
					
					if(ctrlControlerSubscriptionForCOHI.DataSource.Rows.Count == 0) 
					{
						this.ctrlControlerSubscriptionForCOHI.Visible = false;
						lblSubscriptions.Visible = false;
					}
				}

                InitAddressHygiene();
			}
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
			script += "     document.getElementById(\"" + this.Page.ConfirmButton.ClientID + "\").disabled = true;\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("DisableSubmitButton", script);
		}

		private void AddAddressAttributes()
		{
			this.tbxStreet1.Attributes["onchange"] = "DisableConfirmButton();";
			this.tbxStreet2.Attributes["onchange"] = "DisableConfirmButton();";
			this.tbxCity.Attributes["onchange"] = "DisableConfirmButton();";
			this.ddlProvince.Attributes["onchange"] = "DisableConfirmButton();";
			this.tbxPostalCode.Attributes["onchange"] = "DisableConfirmButton();";
			this.ddlCountry.Attributes["onchange"] = "DisableConfirmButton();";
		}

		private void RemoveAddressAtrributes()
		{
			this.tbxStreet1.Attributes["onchange"] = "";
			this.tbxStreet2.Attributes["onchange"] = "";
			this.tbxCity.Attributes["onchange"] = "";
			this.ddlProvince.Attributes["onchange"] = "";
			this.tbxPostalCode.Attributes["onchange"] = "";
			this.ddlCountry.Attributes["onchange"] = "";
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

		#region Fields

		private string FirstName 
		{
			get 
			{
				return this.tbxFirstName.Text;
			}
			set 
			{
				this.tbxFirstName.Text = value;
			}
		}

		private string LastName 
		{
			get 
			{
				return this.tbxLastName.Text;
			}
			set 
			{
				this.tbxLastName.Text = value;
			}
		}

		private string Street1 
		{
			get 
			{
				return this.tbxStreet1.Text;
			}
			set 
			{
				this.tbxStreet1.Text = value;
			}
		}

		private string Street2 
		{
			get 
			{
				return this.tbxStreet2.Text;
			}
			set 
			{
				this.tbxStreet2.Text = value;
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

		#endregion

        private void InitAddressHygiene()
        {
            AddressHygieneStatusLabel.Text = String.Empty;

            //Only enable confirm button after Address has been checked
            this.Page.ConfirmButton.Enabled = !this.ctrlAddressHygiene.IsAddressHygieneEnabled;
            this.btnValidateAddress.Visible = this.ctrlAddressHygiene.IsAddressHygieneEnabled;

        }

		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}
		
		protected override void DoAction()
		{
			System.Web.UI.WebControls.CheckBox[] chkSelect;
			DataTable subsTable;
			DataRow subRow;
			
			ChangeOfAddress cna;
			int newCustomerInstance = 0;


			cna = GetChangeNameAddress();

			if(this.Page.BusCustomerOrderHeader.ValidateChangeNameAddress(cna)) 
			{
				chkSelect = GetSelectedSubs();
				subsTable = GetSubsTable();

				RecordRecipientAddressHistory(chkSelect, subsTable);

				for(int i = 0; i < chkSelect.Length; i++) 
				{
					subRow = ctrlControlerSubscriptionForCOHI.DataSource.Rows[i];

					if(chkSelect[i].Checked)
					{
						if(this.Page.IsMagazine && !this.Page.IsMagazineBeforeRemit) 
						{
							ApplyChangeNameAddress(cna, subRow);
						} 
						else 
						{
							ChangeNameAddressBeforeRemit(cna, subRow);
						}
					}
					else
					{
						newCustomerInstance = AdjustCustomerForUnselected(cna, subRow, newCustomerInstance);
					}
				}
			} 
			else 
			{
				this.Page.MessageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
				this.Page.MessageManager.PrepareErrorMessage();
				throw new ExceptionFulf(this.Page.MessageManager);
			}
		}

		private void RecordRecipientAddressHistory(System.Web.UI.WebControls.CheckBox[] chkSelect, DataTable subsTable) 
		{
			DataRow row;

			for(int i = 0; i < chkSelect.Length; i++) 
			{
				row = subsTable.Rows[i];

				if(chkSelect[i].Checked) 
				{
					this.Page.BusCustomerOrderHeader.RecordRecipientAddressHistory(Convert.ToInt32(row["CustomerOrderHeaderInstance"]), Convert.ToInt32(row["TransID"]));
				}
			}
		}

		private void ApplyChangeNameAddress(ChangeOfAddress cna, DataRow subRow) 
		{
			if(Convert.ToInt32(subRow["CustomerOrderHeaderInstance"]) == this.Page.OrderInfo.CustomerOrderHeaderInstance &&
				Convert.ToInt32(subRow["TransID"]) == this.Page.OrderInfo.TransID) 
			{
				SetCurrentChangeOfAddress(cna);

				this.Page.BusCustomerOrderHeader.ChangeNameAddress(cna, this.Page.CommunicationChannelInstance, this.Page.CommunicationSourceInstance);
				iCustomerRemitInstance = Convert.ToInt32(this.Page.BusCustomerOrderHeader.ResultSetReturned);
			} 
			else 
			{
				SetOtherChangeOfAddress(cna, subRow);

				this.Page.BusCustomerOrderHeader.ChangeNameAddress(cna, this.Page.CommunicationChannelInstance, this.Page.CommunicationSourceInstance);
			}
		}

		private void ChangeNameAddressBeforeRemit(ChangeOfAddress cna, DataRow subRow) 
		{
			if(Convert.ToInt32(subRow["CustomerOrderHeaderInstance"]) == this.Page.OrderInfo.CustomerOrderHeaderInstance && Convert.ToInt32(subRow["TransID"]) == this.Page.OrderInfo.TransID) 
			{
				SetCurrentChangeOfAddress(cna);

				this.Page.BusCustomerOrderHeader.ChangeNameAddressBeforeRemit(cna, this.Page.CommunicationChannelInstance, this.Page.CommunicationSourceInstance);
			} 
			else 
			{
				if(this.Page.BusAction.IsValidAction(4, Convert.ToInt32(subRow["CustomerOrderHeaderInstance"]), Convert.ToInt32(subRow["TransID"]))) 
				{
					SetOtherChangeOfAddress(cna, subRow);

					this.Page.BusCustomerOrderHeader.ChangeNameAddressBeforeRemit(cna, this.Page.CommunicationChannelInstance, this.Page.CommunicationSourceInstance);
				}
			}
		}

		private int AdjustCustomerForUnselected(ChangeOfAddress cna, DataRow subRow, int newCustomerInstance) 
		{
			DataTable CustomerTable = new DataTable("Customer");

			if(newCustomerInstance == 0) 
			{
				this.Page.BusCustomer.SelectCustomerByCOH(CustomerTable, this.Page.OrderInfo.CustomerOrderHeaderInstance);
				if(Enum.IsDefined(typeof(CustomerType), Convert.ToInt32(CustomerTable.Rows[0]["Type"]))) 
				{
					cna.CustomerOldInfo.Type = (CustomerType) Enum.ToObject(typeof(CustomerType), Convert.ToInt32(CustomerTable.Rows[0]["Type"]));
				} 
				else 
				{
					cna.CustomerOldInfo.Type = CustomerType.none;
				}

				newCustomerInstance = this.Page.BusCustomer.InsertForCHADD(cna.CustomerOldInfo, this.Page.UserID);
			}

			this.Page.BusCustomerOrderDetail.UpdateSubscriptionForChadd(Convert.ToInt32(subRow["CustomerOrderHeaderInstance"]), Convert.ToInt32(subRow["TransID"]), newCustomerInstance);
			
			return newCustomerInstance;
		}

		private Customer GetCustomer() 
		{
			DataAccess.Common.ActionObject.Address address = new DataAccess.Common.ActionObject.Address(Street1, Street2, City, Province, PostalCode, Country);
			return new Customer(LastName, FirstName, address);
		}

		private Customer GetCustomerOld() 
		{
			DataAccess.Common.ActionObject.Address address;
			DataTable addressTable = new DataTable("Address");
			DataRow addressRow;

			this.Page.BusCustomerOrderHeader.SelectShipToAddress(addressTable, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);

			addressRow = addressTable.Rows[0];
			address = new DataAccess.Common.ActionObject.Address(addressRow["Address1"].ToString(), addressRow["Address2"].ToString(), addressRow["City"].ToString(), addressRow["State"].ToString(), addressRow["Zip"].ToString(), addressRow["Country"].ToString());
			return new Customer(addressRow["lastname"].ToString(), addressRow["firstname"].ToString(), address, addressRow["email"].ToString(), addressRow["phone"].ToString());
		}

		private ChangeOfAddress GetChangeNameAddress() 
		{
			Customer customer = GetCustomer();
			Customer customerOld = GetCustomerOld();

			return new ChangeOfAddress(customerOld, customer, this.Page.UserID, this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID, NO_PROBLEM_CODE);
		}

		private System.Web.UI.WebControls.CheckBox[] GetSelectedSubs() 
		{
			System.Web.UI.WebControls.CheckBox[] chkSelect = new System.Web.UI.WebControls.CheckBox[ctrlControlerSubscriptionForCOHI.dtgMain.Items.Count + 1]; 
			chkSelect[0] = new System.Web.UI.WebControls.CheckBox();
			chkSelect[0].Checked = true;

			for(int i = 1; i <= ctrlControlerSubscriptionForCOHI.dtgMain.Items.Count; i++) 
			{
				chkSelect[i] = (System.Web.UI.WebControls.CheckBox) ctrlControlerSubscriptionForCOHI.dtgMain.Items[i - 1].FindControl("chkSelect");
			}

			return chkSelect;
		}

		private DataTable GetSubsTable() 
		{
			DataTable subsTable;
			DataRow currentSubRow;

			ctrlControlerSubscriptionForCOHI.DataBind();
			subsTable = ctrlControlerSubscriptionForCOHI.DataSource;

			currentSubRow = subsTable.NewRow();
			currentSubRow["CustomerOrderHeaderInstance"] = this.Page.OrderInfo.CustomerOrderHeaderInstance;
			currentSubRow["TransID"] = this.Page.OrderInfo.TransID;

			subsTable.Rows.InsertAt(currentSubRow, 0);

			return subsTable;
		}

		private void SetCurrentChangeOfAddress(ChangeOfAddress cna) 
		{
			cna.CustomerInfo.FirstName = FirstName;
			cna.CustomerInfo.LastName = LastName;
			cna.CustomerOrderHeaderInstance = this.Page.OrderInfo.CustomerOrderHeaderInstance;
			cna.TransID = this.Page.OrderInfo.TransID;
			cna.ProblemCode = NO_PROBLEM_CODE;
		}

		private void SetOtherChangeOfAddress(ChangeOfAddress cna, DataRow subRow) 
		{
			cna.CustomerInfo.FirstName = subRow["RecipientFirstName"].ToString();
			cna.CustomerInfo.LastName = subRow["RecipientLastName"].ToString();
			cna.CustomerOrderHeaderInstance = Convert.ToInt32(subRow["CustomerOrderHeaderInstance"]);
			cna.TransID = Convert.ToInt32(subRow["TransID"]);
			cna.ProblemCode = this.Page.ProblemCode;
		}

		private void LoadData()
		{
			DataSource = new DataTable("Address");
			this.Page.BusCustomerOrderHeader.SelectShipToAddress(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);
		}

		private void SetValue()
		{
			DataRow row = DataSource.Rows[0];
			FirstName = row["firstname"].ToString();
			LastName = row["lastname"].ToString();
			City = row["city"].ToString();
			PostalCode = row["Zip"].ToString();
			Street1 = row["Address1"].ToString();
			Street2 = row["Address2"].ToString();

			SetValueProvince(row);
		}

		private void SetValueProvince(DataRow row)
		{
			string ss = row["State"].ToString();
			for(int i=0; i < this.ddlProvince.Items.Count; i++)
			{
				if(this.ddlProvince.Items[i].Value ==ss )
				{
					this.ddlProvince.SelectedIndex = i;
					break;
				}
			}
		}

		protected override int CustomerRemitHistoryInstance
		{
			get
			{
				return iCustomerRemitInstance;
			}
		}

		protected void btnValidateAddress_Click(object sender, System.EventArgs e)
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
			this.Page.ConfirmButton.Enabled = true;
		}

		private Business.com.ses.ws.AddressHygiene.Address GetAddressFromFields()
		{
			Business.com.ses.ws.AddressHygiene.Address address = new Business.com.ses.ws.AddressHygiene.Address();

			address.Address1 = Street1;
			address.Address2 = Street2;
			address.City = City;
			address.County = "";
			address.Region = Province;
			if (this.PostalCode.Length == 6) //Address Hygiene requires space within Canadian Postal Code
				address.PostCode = this.PostalCode.Substring(0, 3) + " " + this.PostalCode.Substring(3, 3);
			else
				address.PostCode = this.PostalCode;
			address.PostCode2 = "";
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
				Street1 = OutputAddress.Address1;
				Street2 = OutputAddress.Address2;
				City = OutputAddress.City;
				
				if (OutputAddress.PostCode.Length == 7) //Address Hygiene returns a space between Canadian Postal Code
					PostalCode = OutputAddress.PostCode.Substring(0, 3) + OutputAddress.PostCode.Substring(4, 3);
				else
					PostalCode = OutputAddress.PostCode;

				string province = OutputAddress.Region;
				bool provinceFound = false;
				for(int i = 0; i < this.ddlProvince.Items.Count; i++)
				{
					if(ddlProvince.Items[i].Value == province)
					{
						ddlProvince.SelectedIndex = i;
						provinceFound = true;
						break;
					}
				}
				if (provinceFound == false)
					ddlProvince.SelectedIndex = 0;

				//Display Status 
				if (ChangeStatus)
					AddressHygieneStatusLabel.Text = "Address Updated";
				else
					AddressHygieneStatusLabel.Text = "Address Validated";
			}
		}
	}
}
