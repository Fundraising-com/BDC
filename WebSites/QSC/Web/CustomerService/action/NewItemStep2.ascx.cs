namespace QSPFulfillment.CustomerService.action
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using Business.com.ses.ws.AddressHygiene;

	/// <summary>
	///		Summary description for NewSubStep2.
	/// </summary>
	public partial class NewItemStep2 : CustomerServiceActionControl
	{
		protected const string MSG_HEADER = "New item";

		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected QSPFulfillment.CommonWeb.UC.AddressHygiene ctrlAddressHygiene;
		
		public event EventHandler BackClicked;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			AddressHygieneStatusLabel.Text = String.Empty;

			//Only enable confirm button after Address has been checked
			this.Page.ConfirmButton.Enabled = false;
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
		}

		private void RemoveAddressAtrributes()
		{
			this.tbxStreet1.Attributes["onchange"] = "";
			this.tbxStreet2.Attributes["onchange"] = "";
			this.tbxCity.Attributes["onchange"] = "";
			this.ddlProvince.Attributes["onchange"] = "";
			this.tbxPostalCode.Attributes["onchange"] = "";
		}
		#endregion

		protected void btnBack_Click(object sender, EventArgs e)
		{
			if(BackClicked != null) 
			{
				BackClicked(this, EventArgs.Empty);
			}
		}

		public QSPFulfillment.DataAccess.Common.ActionObject.Magazine ProductInfo 
		{
			get
			{
				if(ViewState["MagazineInfo"] == null) 
				{
					ViewState["MagazineInfo"] = new QSPFulfillment.DataAccess.Common.ActionObject.Magazine();
				}

				return (QSPFulfillment.DataAccess.Common.ActionObject.Magazine) ViewState["MagazineInfo"];
			}
			set
			{
				ViewState["MagazineInfo"] = value;
			}
		}

		public bool IsDataBound 
		{
			get 
			{
				if (ViewState["IsDataBound"] != null)
					return Convert.ToBoolean(ViewState["IsDataBound"]);
				else
					return false;
			}
			set 
			{
				ViewState["IsDataBound"] = value;
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

		public override void DataBind()
		{
			if (!IsDataBound)
			{
				IsDataBound = true;
				LoadData();
				SetValueAddress();
			}

			SetValueItem();
			LoadValueDDL();
		}

		private void LoadData()
		{
			DataSource = new DataTable("Address");
			this.Page.BusCustomerOrderHeader.SelectShipToAddress(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);
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
			ctrlAddressHygiene.OutputAddress += new QSPFulfillment.CommonWeb.UC.OutputAddressEventHandler(AddressSelected);
		}
		#endregion

		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}

		protected override void DoAction()
		{			
			NewSubcription newsub = new NewSubcription(this.Page.OrderInfo.CampaignID, this.ProductInfo.MagInstance, "", this.ProductInfo.Price, GetOverrideCode(), this.Page.CustomerInfo.CustomerInstance, this.ProductInfo.Price, this.Page.UserID, GetFirstName(), GetLastName(), GetAddress1(), GetAddress2(), GetCity(), GetProvince(), GetPostalCode());
		
			this.Page.BusCustomerOrderDetail.NewItem(newsub, this.ProductInfo.ProductType, 39008, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);
		}

		private int GetOverrideCode()
		{
			return Convert.ToInt32(this.ddlPriceOverrideReason.SelectedItem.Value);
		}

		private float GetPrice()
		{
			float price;
			
			if(tbxPrice.Text != String.Empty) 
			{
				price = Convert.ToSingle(tbxPrice.Text);
			}
			else 
			{
				price = this.ProductInfo.Price;
			}

			return price;
		}

		private string GetFirstName() 
		{
			return this.tbxFirstName.Text;
		}

		private string GetLastName() 
		{
			return this.tbxLastName.Text;
		}

		private string GetAddress1() 
		{
			return this.tbxStreet1.Text;
		}

		private string GetAddress2() 
		{
			return this.tbxStreet2.Text;
		}

		private string GetCity() 
		{
			return this.tbxCity.Text;
		}

		private string GetPostalCode() 
		{
			return this.tbxPostalCode.Text;
		}

		private string GetProvince() 
		{
			return this.ddlProvince.SelectedValue;
		}

		private void SetValueAddress()
		{	
			DataRow row= DataSource.Rows[0];
			this.tbxFirstName.Text = row["firstname"].ToString();
			this.tbxLastName.Text = row["lastname"].ToString();
			this.tbxCity.Text = row["city"].ToString();
			this.tbxPostalCode.Text = row["Zip"].ToString();
			this.tbxStreet1.Text = row["Address1"].ToString();
			this.tbxStreet2.Text= row["Address2"].ToString();

			SetValueProvince(row);
		}

		private void SetValueItem()
		{
			this.lblCatalogPrice.Text = String.Format("{0:N2}", this.ProductInfo.Price);
			this.lblProductName.Text = this.ProductInfo.Title;
			this.lblProductCode.Text = this.ProductInfo.ProductCode;
			this.tbxPrice.Text =String.Format("{0:N2}", this.ProductInfo.Price);
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

		private void LoadValueDDL()
		{
			if(ddlPriceOverrideReason.Items.Count == 0)
			{
				DataTable Table = new DataTable("45000");
				
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table,45000);
				DataViewObject dvo = new DataViewObject();
				dvo.Table = Table;
				dvo.Sort = CodeDetailTable.FLD_INSTANCE +" DESC";
				ddlPriceOverrideReason.DataSource = dvo;

				ddlPriceOverrideReason.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				ddlPriceOverrideReason.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlPriceOverrideReason.DataBind();
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

			address.Address1 = this.GetAddress1();
			address.Address2 = this.GetAddress2();
			address.City = this.GetCity();
			address.County = "";
			address.Region = this.GetProvince();
			if (this.GetPostalCode().Length == 6) //Address Hygiene requires space within Canadian Postal Code
				address.PostCode = this.GetPostalCode().Substring(0, 3) + " " + this.GetPostalCode().Substring(3, 3);
			else
				address.PostCode = this.GetPostalCode();
			address.PostCode2 = "";
			address.Country = "CANADA";

			return address;
		}

		public void DisableConfirmButton()
		{
			this.Page.ConfirmButton.Enabled = false;
		}

		public void SetConfirmButtonVisibility()
		{
			this.Page.ConfirmButton.Visible = true;
			this.Page.ConfirmButton.CausesValidation = true;
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
				this.tbxStreet1.Text = OutputAddress.Address1;
				this.tbxStreet2.Text = OutputAddress.Address2;
				this.tbxCity.Text = OutputAddress.City;

				if (OutputAddress.PostCode.Length == 7) //Address Hygiene returns a space between Canadian Postal Code
					this.tbxPostalCode.Text = OutputAddress.PostCode.Substring(0, 3) + OutputAddress.PostCode.Substring(4, 3);
				else
					this.tbxPostalCode.Text = OutputAddress.PostCode;

				string stateProvince = OutputAddress.Region;
				bool stateProvinceFound = false;
				for(int i = 0; i < ddlProvince.Items.Count; i++)
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
	}
}

