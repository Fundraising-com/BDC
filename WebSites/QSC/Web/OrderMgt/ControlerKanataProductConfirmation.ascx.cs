namespace QSPFulfillment.OrderMgt
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Data;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.CommonWeb;
	using Business.com.ses.ws.AddressHygiene;

	/// <summary>
	///		Summary description for ControlerProductReplacement.
	/// </summary>
	public partial class ControlerKanataProductConfirmation :  QSPFulfillment.CustomerService.CustomerServiceControl
	{
		protected QSPFulfillment.OrderMgt.ControlerProductSelectForKanata ctrlControlerProductSelectForKanataConf;
		private Batch currentBatch;
		protected ProductItemCollectionFilter productItemCollectionFilter;
		AddressTable fieldManagerAddressTable; 
		AddressTable schoolAddressTable;
		FieldManagerTable fieldManagerTable;
		CustomerTable customerTable;
		CAccountTable cAccountTable;
		protected QSPFulfillment.OrderMgt.UC.OrderQualifier ddlOrderQualifier;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDeliveryDate;
		private string currentCatalog;
      private int currentCatalogType;
      protected QSPFulfillment.CommonWeb.UC.Address AddressControl;

		public event EventHandler ConfirmedClicked;
		public event EventHandler SaveClicked;
		public event EventHandler StepBackClicked;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Init += new System.EventHandler(this.ControlerProductReplacement_Init);
			this.AddressControl.AddressHygienedEvent += new System.EventHandler(AddressHygiened);
		}
		#endregion

		protected void ControlerProductReplacement_Init(object sender, EventArgs e)
		{
			try 
			{
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.lblErrorMessage.Visible=false;
			
			try 
			{
				UpdateBatchInformations();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
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

			if (this.AddressControl.IsAddressHygieneEnabled && !this.AddressControl.IsAddressHygiened)
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
			script += "    document.getElementById(\"" + this.btnSave.ClientID + "\").disabled = true;\n";
			script += "    document.getElementById(\"" + this.btnConfirm.ClientID + "\").disabled = true;\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("DisableSubmitButton", script);
		}

		private void AddAddressAttributes()
		{
			this.AddressControl.Address1Attributes.Add("onchange", "DisableSubmitButton();");
			this.AddressControl.Address2Attributes.Add("onchange", "DisableSubmitButton();");
			this.AddressControl.CityAttributes.Add("onchange", "DisableSubmitButton();");
			this.AddressControl.CountyAttributes.Add("onchange", "DisableSubmitButton();");
			this.AddressControl.StateProvinceAttributes.Add("onchange", "DisableSubmitButton();");
			this.AddressControl.PostalCodeAttributes.Add("onchange", "DisableSubmitButton();");
			this.AddressControl.CountryAttributes.Add("onchange", "DisableSubmitButton();");
		}

		private void RemoveAddressAtrributes()
		{
			this.AddressControl.Address1Attributes.Add("onchange", "");
			this.AddressControl.Address2Attributes.Add("onchange", "");
			this.AddressControl.CityAttributes.Add("onchange", "");
			this.AddressControl.CountyAttributes.Add("onchange", "");
			this.AddressControl.StateProvinceAttributes.Add("onchange", "");
			this.AddressControl.PostalCodeAttributes.Add("onchange", "");
			this.AddressControl.CountryAttributes.Add("onchange", "");
		}
		#endregion

		#region Properties

		private string FirstName 
		{
			get 
			{
				if (ViewState["FirstName"] == null) 
					return String.Empty;
				else
                    return ViewState["FirstName"].ToString();
			}
			set 
			{
				ViewState["FirstName"] = value;
			}
		}

		private string LastName 
		{
			get 
			{
				if (ViewState["LastName"] == null) 
					return String.Empty;
				else
					return ViewState["LastName"].ToString();
			}
			set 
			{
				ViewState["LastName"] = value;
			}
		}

		private string Email 
		{
			get 
			{
				if (ViewState["Email"] == null) 
					return String.Empty;
				else
					return ViewState["Email"].ToString();
			}
			set 
			{
				ViewState["Email"] = value;
			}
		}

		private string Function 
		{
			get 
			{
				if (ViewState["Function"] == null) 
					return String.Empty;
				else
					return ViewState["Function"].ToString();
			}
			set 
			{
				ViewState["Function"] = value;
			}
		}

		private string Address1 
		{
			get 
			{
				if (ViewState["Address1"] == null) 
					return String.Empty;
				else
					return ViewState["Address1"].ToString();
			}
			set 
			{
				ViewState["Address1"] = value;
			}
		}

		private string Address2 
		{
			get 
			{
				if (ViewState["Address2"] == null) 
					return String.Empty;
				else
					return ViewState["Address2"].ToString();
			}
			set 
			{
				ViewState["Address2"] = value;
			}
		}

		private string City 
		{
			get 
			{
				if (ViewState["City"] == null) 
					return String.Empty;
				else
					return ViewState["City"].ToString();
			}
			set 
			{
				ViewState["City"] = value;
			}
		}

		private string County 
		{
			get 
			{
				if (ViewState["County"] == null) 
					return String.Empty;
				else
					return ViewState["County"].ToString();
			}
			set 
			{
				ViewState["County"] = value;
			}
		}

		private string StateProvince 
		{
			get 
			{
				if (ViewState["StateProvince"] == null) 
					return String.Empty;
				else
					return ViewState["StateProvince"].ToString();
			}
			set 
			{
				ViewState["StateProvince"] = value;
			}
		}

		private string PostalCode 
		{
			get 
			{
				if (ViewState["PostalCode"] == null) 
					return String.Empty;
				else
					return ViewState["PostalCode"].ToString();
			}
			set 
			{
				ViewState["PostalCode"] = value;
			}
		}

		private string PostalCode2
		{
			get 
			{
				if (ViewState["PostalCode2"] == null) 
					return String.Empty;
				else
					return ViewState["PostalCode2"].ToString();
			}
			set 
			{
				ViewState["PostalCode2"] = value;
			}
		}

		private string Country 
		{
			get 
			{
				if (ViewState["Country"] == null) 
					return String.Empty;
				else
					return ViewState["Country"].ToString();
			}
			set 
			{
				ViewState["Country"] = value;
			}
		}

		public bool BtnSave 
		{
			get 
			{
				return btnSave.Visible;
			}
			set 
			{
				btnSave.Visible = value;
			}
		}

		public bool BtnConfirm 
		{
			get 
			{
				return btnConfirm.Visible;
			}
			set 
			{
				btnConfirm.Visible = value;
			}
		}

		public bool BtnBackTop
		{
			get 
			{
				return btnBackTop.Visible;
			}
			set 
			{
				btnBackTop.Visible = value;
			}
		}

		public bool BtnBackBottom
		{
			get 
			{
				return btnBackBottom.Visible;
			}
			set 
			{
				btnBackBottom.Visible = value;
			}
		}

		public int DDLOrderQualifier
		{
			get 
			{
				return ddlOrderQualifier.SelectedValue;
			}
			set 
			{
				ddlOrderQualifier.SelectedValue = value;
			}
		}

		public string LblOrderQualifier
		{
			get 
			{
				return lblOrderQualifier.Text;
			}
			set 
			{
				lblOrderQualifier.Text = value;
			}
		}

		public string CurrentCatalog
		{
			get 
			{
				return currentCatalog;
			}
			set 
			{
				currentCatalog = value;
			}
		}
      public int CurrentCatalogType
      {
         get
         {
            return currentCatalogType;
         }
         set
         {
            currentCatalogType = value;
         }
      }
      public bool HypPrintVisible
		{
			get 
			{
				return hypPrint.Visible;
			}
			set 
			{
				hypPrint.Visible = value;
			}
		}

		public DateTime DeliveryDate
		{
			get 
			{
				return this.dteDeliveryDate.Date;
			}
			set
			{
				this.dteDeliveryDate.Date = value;
			}
		}
		
		public QSPFulfillment.CommonWeb.UC.Address AddressCtrl
		{
			get 
			{
				return this.AddressControl;
			}
		}

		public string RblBillTo
		{
			get 
			{
				return this.rblBillTo.SelectedValue;
			}
			set
			{
				this.rblBillTo.SelectedValue = value;
			}
		}

		public string RblShipTo
		{
			get 
			{
				return this.rblShipTo.SelectedValue;
			}
			set
			{
				this.rblShipTo.SelectedValue = value;
			}
		}

		public string FirstNameTbx
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

		public string LastNameTbx
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

		public string EmailTbx
		{
			get 
			{
				return this.tbxEmail.Text;
			}
			set
			{
				this.tbxEmail.Text = value;
			}
		}

		public bool AddressValidateBtnEnabled
		{
			get 
			{
				return this.AddressControl.AddressValidateBtnEnabled;
			}
			set
			{
				this.AddressControl.AddressValidateBtnEnabled = value;
			}
		}

		#endregion

		private void btnNextStudent_Click(object sender, System.EventArgs e)
		{
		}

		private void ctrlControlerOrderHeaderForProductReplacement_EditProductsClicked(object sender, OrderHeader orderHeader)
		{
			try 
			{
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlControlerOrderHeaderForProductReplacement_RemoveOrderClicked(object sender, OrderHeader orderHeader)
		{
			try 
			{
				RemoveOrderHeader(orderHeader);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public void InitField()
		{
			this.rblBillTo.SelectedIndex=0;
			this.rblShipTo.SelectedIndex=0;
			this.tbxFirstName.Text="";
			this.tbxLastName.Text="";
			this.tbxEmail.Text="";
			this.AddressControl.Address1 = "";
			this.AddressControl.Address2 ="";
			this.AddressControl.City = "";
			this.AddressControl.Country = "";
			this.AddressControl.StateProvince = "";
			this.AddressControl.PostalCode = "";
			this.AddressControl.PostalCode2 = "";
		}

		public void SetInitialShipToInfo(bool Editable, bool accountUpdated)
		{
            if (!accountUpdated)
			    this.AddressControl.DataBind();

			fieldManagerTable = new FieldManagerTable();
			FieldManagerData fieldManagerDataAccess = new FieldManagerData();
			fieldManagerDataAccess.SelectOne(fieldManagerTable, currentBatch.Campaign.FMID);

			fieldManagerAddressTable = new AddressTable();
			AddressBusiness addressBusiness = new AddressBusiness();
			addressBusiness.GetFMShipmentAddressByFMID(fieldManagerAddressTable, currentBatch.Campaign.FMID);

			cAccountTable = new CAccountTable();
			CAccountData cAccountDataAccess = new CAccountData();
			cAccountDataAccess.SelectOne(cAccountTable, currentBatch.Campaign.AccountID);

			schoolAddressTable = new AddressTable();
			addressBusiness.GetAccountShipmentAddressByAccountID(schoolAddressTable, currentBatch.Campaign.AccountID);
			
			//only look at an item that is not soft deleted in DB
			customerTable = new CustomerTable();
			this.Page.BusCustomer.SelectCustomerByCOD(customerTable, this.currentBatch.OrderHeaders[0].CustomerOrderHeaderInstance, ProductItemCollectionFiltered[0].TransID);

			int shipTo=0;
			if (customerTable.Rows.Count > 0)
			{
				if (customerTable.Rows[0]["Address1"].ToString() == schoolAddressTable.Rows[0]["Street1"].ToString())
					shipTo = 0;
				else if (customerTable.Rows[0]["Address1"].ToString() == fieldManagerAddressTable.Rows[0]["Street1"].ToString())
					shipTo = 1;
				else
				{
					shipTo = 2;
					FirstName = customerTable.Rows[0]["FirstName"].ToString();
					LastName = customerTable.Rows[0]["LastName"].ToString();
					Email = customerTable.Rows[0]["Email"].ToString();
					Address1 = customerTable.Rows[0]["Address1"].ToString();
					Address2 = customerTable.Rows[0]["Address2"].ToString();
					County = customerTable.Rows[0]["County"].ToString();
					City = customerTable.Rows[0]["City"].ToString();
					StateProvince = customerTable.Rows[0]["State"].ToString();
					PostalCode = customerTable.Rows[0]["Zip"].ToString();
					PostalCode2 = customerTable.Rows[0]["ZipPlusFour"].ToString();

					//Customer table does not have country field, so force country of account
					if (schoolAddressTable.Rows.Count > 0)
						Country = schoolAddressTable.Rows[0]["country"].ToString();
					else
						Country = "CA";
				}
			}
            else if (accountUpdated)
            {
                shipTo = 2;
            }
			else
			{
				shipTo = 0;
			}

			if (shipTo == 0)
			{
				SetShipToSelection(0);
				SetShipToInfoSchool();
				SetShipToInfoEditable(false);
			}
			else if (shipTo == 1)
			{
				SetShipToSelection(1);
				SetShipToInfoFM();
				SetShipToInfoEditable(false);
			}
			else
			{
				SetShipToSelection(2);
                if (!accountUpdated)
    				SetShipToInfoOther();
				SetShipToInfoEditable(true);
			}

			if (!Editable)
			{
				SetShipToInfoEditable(false);
				SetShipToSelectionInactive();
			}

			//this.ucProvinceddl.AutoPostBack = false;
		}

		public void SetBillToSelection(int Selection)
		{
			rblBillTo.SelectedIndex = Selection;
		}

		public void SetShipToSelection(int Selection)
		{
			rblShipTo.SelectedIndex = Selection;
		}

		public void SetBillToSelectionInactive()
		{
			rblBillTo.Enabled = false;
		}

		public void SetShipToSelectionInactive()
		{
			rblShipTo.Enabled = false;
		}

		public void SetDeliveryDateInactive()
		{
			this.dteDeliveryDate.Enabled = false;
		}

		public bool ValidateShippingAddress()
		{
			bool isvalid =true;

		
			if (this.tbxFirstName.Text=="" )
			{
				this.lblErrorMessage.Text="First Name is required";
				this.lblErrorMessage.Visible=true;
				isvalid = false;
				return isvalid;
			}
			
			if (this.rblShipTo.SelectedIndex > 2 && this.tbxLastName.Text=="" )
			{
				this.lblErrorMessage.Text="Last Name is required";
				this.lblErrorMessage.Visible=true;
				isvalid = false;
				return isvalid;
			}

			if (this.AddressControl.Address1 == "")
			{
				this.lblErrorMessage.Text="Address is required";
				this.lblErrorMessage.Visible=true;
				isvalid = false;
				return isvalid;
			}

			if (this.AddressControl.City == "")
			{
				this.lblErrorMessage.Text="City Name is required";
				this.lblErrorMessage.Visible=true;
				isvalid = false;
				return isvalid;
			}

			if (this.AddressControl.StateProvince == "")
			{
				this.lblErrorMessage.Text="Provice is required";
				this.lblErrorMessage.Visible=true;
				isvalid = false;
				return isvalid;
			}

			if (this.AddressControl.PostalCode == "")
			{
				this.lblErrorMessage.Text="Postal Code is required";
				this.lblErrorMessage.Visible=true;
				isvalid = false;
				return isvalid;
			}

			return isvalid;
		}

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			if (ValidateShippingAddress()) 
			{
				this.currentBatch.OrderQualifierID = (OrderQualifier) this.DDLOrderQualifier;
				this.currentBatch.OrderDeliveryDate = this.DeliveryDate;

				try 
				{
					if(SaveClicked != null) 
					{
						SaveClicked(this, EventArgs.Empty);
					}
				}
				catch(Exception ex) 
				{
					this.Page.ManageError(ex);
				}
			}		
		}

		protected void btnConfirm_Click(object sender, System.EventArgs e)
		{
			if (ValidateShippingAddress()) 
			{
				this.currentBatch.OrderQualifierID = (OrderQualifier) this.DDLOrderQualifier;
				this.currentBatch.OrderDeliveryDate = this.DeliveryDate;
				//this.currentBatch.OrderHeaders[0].CustomerBillToInstance = this.rblBillTo.SelectedValue;
				
				//foreach (ProductItem productItem in this.currentBatch.OrderHeaders[0].ProductItems)
				//	productItem.CustomerShipToInstance = this.rblShipTo.SelectedValue;

				try 
				{
					if(ConfirmedClicked != null) 
					{
						ConfirmedClicked(this, EventArgs.Empty);
					}
				}
				catch(Exception ex) 
				{
					this.Page.ManageError(ex);
				}
			}
		}

		private ArrayList OrderHeaderControlIDCollection 
		{
			get 
			{
				if(Session[this.ClientID + "OrderHeaderControlIDCollection"] == null)
					Session[this.ClientID + "OrderHeaderControlIDCollection"] = new ArrayList();

				return (ArrayList) Session[this.ClientID + "OrderHeaderControlIDCollection"];
			}
		}

		public Batch CurrentBatch
		{
			get 
			{
				return currentBatch;
			}
			set 
			{
				currentBatch = value;
			}
		}

		#region Fields

		//private string Comment 
		//{
			/*get 
			{
				//return this.tbxComment.Text;
			}
			set 
			{
				//this.tbxComment.Text = value;
			}*/
		//}

		#endregion

		public override void DataBind()
		{
			if (!QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM
				&& QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == "9999")
			{
				this.ctrlControlerProductSelectForKanataConf.ShowEnterredPriceLbl=true;
			}
			else
			{
				this.ctrlControlerProductSelectForKanataConf.ShowEnterredPriceLbl=false;
			}

			this.ctrlControlerProductSelectForKanataConf.ShowSearch=false;
			this.ctrlControlerProductSelectForKanataConf.ShowCatalogName=true;
			this.ctrlControlerProductSelectForKanataConf.ShowCheckBoxes=false;
			this.ctrlControlerProductSelectForKanataConf.ShowTotalPrice=true;
			this.ctrlControlerProductSelectForKanataConf.ShowQuantityTextBox=false;
			this.ctrlControlerProductSelectForKanataConf.ShowQuantitylabel=true;
			this.ctrlControlerProductSelectForKanataConf.QtyFieldEditable = false;
			this.ctrlControlerProductSelectForKanataConf.ShowEnterredPriceLbl = true;
			this.ctrlControlerProductSelectForKanataConf.AllowPaging = false;

			this.ctrlControlerProductSelectForKanataConf.DataSource = ProductItemCollectionFiltered;
			this.ctrlControlerProductSelectForKanataConf.DataBind();

			this.ddlOrderQualifier.KanataBind(this.CurrentCatalogType);
		}

		public ProductItemCollection ProductItemCollectionFiltered
		{
			get
			{
				productItemCollectionFilter = new ProductItemCollectionFilter();
				return productItemCollectionFilter.Filter(this.currentBatch.OrderHeaders[0].ProductItems, 1);
			}
		}

		private void UpdateBatchInformations() 
		{
		}

		private void RemoveOrderHeader(OrderHeader orderHeader) 
		{
		}

		protected void rblShipTo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Save Other Shipping Info if it was originally set to that
			if (this.tbxFirstName.Enabled)
			{
				FirstName = this.tbxFirstName.Text;
				LastName = this.tbxLastName.Text;
				Email = this.tbxEmail.Text;
				Address1 = this.AddressControl.Address1;
				Address2 = this.AddressControl.Address2;
				City = this.AddressControl.City;
				County = this.AddressControl.County;
				StateProvince = this.AddressControl.StateProvince;
				PostalCode = this.AddressControl.PostalCode;
				PostalCode2 = this.AddressControl.PostalCode2;
				Country = this.AddressControl.Country;
			}

			if (this.rblShipTo.SelectedValue.ToString()=="FM")
			{
				SetShipToInfoFM();
				SetShipToInfoEditable(false);
				SetSubmitButtonEnabled(true);
			}
			else if (this.rblShipTo.SelectedValue.ToString()=="School")
			{
				SetShipToInfoSchool();
				SetShipToInfoEditable(false);
				SetSubmitButtonEnabled(true);
			}
			else
			{
				SetShipToInfoOther();
				SetShipToInfoEditable(true);
				SetSubmitButtonEnabled(!this.AddressControl.IsAddressHygieneEnabled || this.AddressControl.IsAddressHygiened);
			}
		}

		public OrderHeader CurrentOrderHeader 
		{
			get 
			{
				if(this.ViewState["CurrentOrderHeader"] == null) 
				{
					this.ViewState["CurrentOrderHeader"] = new OrderHeader();
				}

				return (OrderHeader) this.ViewState["CurrentOrderHeader"];
			}
			set 
			{
				this.ViewState["CurrentOrderHeader"] = value;
			}
		}

		private void SetShipToInfoFM()
		{	
			FieldManagerTable fieldManagerTable = new FieldManagerTable();
			FieldManagerData fieldManagerDataAccess = new FieldManagerData();
			fieldManagerDataAccess.SelectOne(fieldManagerTable, currentBatch.Campaign.FMID);

			AddressTable addressTable = new AddressTable();
			AddressBusiness addressBusiness = new AddressBusiness();
			addressBusiness.GetFMShipmentAddressByFMID(addressTable, currentBatch.Campaign.FMID);
			
			if (fieldManagerTable.Rows.Count > 0)
			{
				this.tbxFirstName.Text = fieldManagerTable.Rows[0]["firstName"].ToString();
				this.tbxLastName.Text = fieldManagerTable.Rows[0]["lastName"].ToString();
				this.tbxEmail.Text = fieldManagerTable.Rows[0]["eMail"].ToString();
			}
			else
			{
				this.tbxFirstName.Text = "";
				this.tbxLastName.Text = "";
				this.tbxEmail.Text = "";
			}

			if (addressTable.Rows.Count > 0)
			{
				this.AddressControl.Address1 =  addressTable.Rows[0]["street1"].ToString();
				this.AddressControl.Address2 = addressTable.Rows[0]["street2"].ToString();
				this.AddressControl.City = addressTable.Rows[0]["city"].ToString();
				this.AddressControl.County = ""; //addressTable.Rows[0]["county"].ToString();
				this.AddressControl.PostalCode = addressTable.Rows[0]["postal_code"].ToString();
				this.AddressControl.PostalCode2 = addressTable.Rows[0]["zip4"].ToString();
				this.AddressControl.Country = addressTable.Rows[0]["country"].ToString();
				this.AddressControl.StateProvince = addressTable.Rows[0]["stateProvince"].ToString();
			}
			else
			{
				this.AddressControl.Address1 = "";
				this.AddressControl.Address2 = "";
				this.AddressControl.City = "";
				this.AddressControl.County = "";
				this.AddressControl.PostalCode = "";
				this.AddressControl.PostalCode2 = "";
				this.AddressControl.Country = "";
				this.AddressControl.StateProvince = "";
			}
		}

		public void SetShipToInfoSchool()
		{
			CAccountTable cAccountTable = new CAccountTable();
			CAccountData cAccountDataAccess = new CAccountData();
			cAccountDataAccess.SelectOne(cAccountTable, currentBatch.Campaign.AccountID);

			AddressTable addressTable = new AddressTable();
			AddressBusiness addressBusiness = new AddressBusiness();
			addressBusiness.GetAccountShipmentAddressByAccountID(addressTable, currentBatch.Campaign.AccountID);
			
			if (cAccountTable.Rows.Count > 0)
			{
				this.tbxFirstName.Text = cAccountTable.Rows[0]["name"].ToString();
				this.tbxLastName.Text = "";
				this.tbxEmail.Text = cAccountTable.Rows[0]["email"].ToString();
			}
			else
			{
				this.tbxFirstName.Text = "";
				this.tbxLastName.Text = "";
				this.tbxEmail.Text = "";
			}

			if (addressTable.Rows.Count > 0)
			{
				this.AddressControl.Address1 =  addressTable.Rows[0]["street1"].ToString();
				this.AddressControl.Address2 = addressTable.Rows[0]["street2"].ToString();
				this.AddressControl.City = addressTable.Rows[0]["city"].ToString();
				this.AddressControl.County = ""; //addressTable.Rows[0]["county"].ToString();
				this.AddressControl.PostalCode = addressTable.Rows[0]["postal_code"].ToString();
				this.AddressControl.PostalCode2 = addressTable.Rows[0]["zip4"].ToString();
				this.AddressControl.Country = addressTable.Rows[0]["country"].ToString();
				this.AddressControl.StateProvince = addressTable.Rows[0]["stateProvince"].ToString();
			}
			else
			{
				this.AddressControl.Address1 = "";
				this.AddressControl.Address2 = "";
				this.AddressControl.City = "";
				this.AddressControl.County = "";
				this.AddressControl.PostalCode = "";
				this.AddressControl.PostalCode2 = "";
				this.AddressControl.Country = "";
				this.AddressControl.StateProvince = "";
			}
		}

		private void SetShipToInfoOther()
		{
			this.tbxFirstName.Text = FirstName;
			this.tbxLastName.Text = LastName;
			this.tbxEmail.Text = Email;
			this.AddressControl.Address1 = Address1;
			this.AddressControl.Address2 = Address2;
			this.AddressControl.City = City;
			this.AddressControl.County = County;
			this.AddressControl.PostalCode = PostalCode;
			this.AddressControl.PostalCode2 = PostalCode2;

			//Customer table does not have country field, so force country of account
			AddressTable addressTable = new AddressTable();
			AddressBusiness addressBusiness = new AddressBusiness();
			addressBusiness.GetAccountShipmentAddressByAccountID(addressTable, currentBatch.Campaign.AccountID);
			if (addressTable.Rows.Count > 0)
				this.AddressControl.Country = addressTable.Rows[0]["country"].ToString();
			else
				this.AddressControl.Country = "CA";
			
			this.AddressControl.StateProvince = StateProvince;
		}

		private void SetShipToInfoEditable(bool Editable)
		{
			this.tbxFirstName.Enabled = Editable;
			this.tbxLastName.Enabled = Editable;
			this.tbxEmail.Enabled = Editable;
			this.AddressControl.Address1Enabled = Editable;
			this.AddressControl.Address2Enabled = Editable;
			this.AddressControl.CityEnabled = Editable;
			this.AddressControl.CountyEnabled = Editable;
			this.AddressControl.StateProvinceEnabled = Editable;
			this.AddressControl.PostalCodeEnabled = Editable;
			this.AddressControl.CountryEnabled = false; //Customer table has no Country field
			this.AddressControl.SetAddressHygieneEnabled(Editable);
			this.SetSubmitButtonEnabled(!Editable);
		}

		protected void btnBack_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.ctrlControlerProductSelectForKanataConf.Reset();

				if(StepBackClicked != null) 
				{
					StepBackClicked(this, EventArgs.Empty);
				}
			}
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}
		
		public void SetDDLOrderQualifier(bool edit)
		{
			if (this.currentBatch.OrderQualifierID != OrderQualifier.None)
				this.DDLOrderQualifier = Convert.ToInt32(this.currentBatch.OrderQualifierID);

			//lock Order Qualifier if viewing order or if order is WFC
			if (edit && !(this.currentBatch.OrderQualifierID == OrderQualifier.WFCSigningBonus))
			{
				this.ddlOrderQualifier.Visible = true;
				this.LblOrderQualifier = "Order Qualifier:";
			}
			else
			{
				this.ddlOrderQualifier.Visible = false;
				this.LblOrderQualifier = "Order Qualifier: " + this.CurrentBatch.OrderQualifierID.ToString();
			}
		}

		public void SetInitialBillToInfo(bool Editable)
		{
			fieldManagerTable = new FieldManagerTable();
			FieldManagerData fieldManagerDataAccess = new FieldManagerData();
			fieldManagerDataAccess.SelectOne(fieldManagerTable, currentBatch.Campaign.FMID);

			fieldManagerAddressTable = new AddressTable();
			AddressBusiness addressBusiness = new AddressBusiness();
			addressBusiness.GetFMShipmentAddressByFMID(fieldManagerAddressTable, currentBatch.Campaign.FMID);

			cAccountTable = new CAccountTable();
			CAccountData cAccountDataAccess = new CAccountData();
			cAccountDataAccess.SelectOne(cAccountTable, currentBatch.Campaign.AccountID);

			schoolAddressTable = new AddressTable();
			addressBusiness.GetAccountShipmentAddressByAccountID(schoolAddressTable, currentBatch.Campaign.AccountID);
			
			customerTable = new CustomerTable();
			this.Page.BusCustomer.SelectCustomerByCOH(customerTable, this.currentBatch.OrderHeaders[0].CustomerOrderHeaderInstance);

			int billTo=0;
			if (customerTable.Rows.Count > 0)
			{
				if (customerTable.Rows[0]["Address1"].ToString() == schoolAddressTable.Rows[0]["Street1"].ToString())
					billTo = 0;
				else
					billTo = 1;
			}
			else
			{
				billTo = 0;
			}

			if (billTo == 0)
			{
				SetBillToSelection(0);
			}
			else if (billTo == 1)
			{
				SetBillToSelection(1);
			}

			if (!Editable)
			{
				SetBillToSelectionInactive();
			}
		}
		public void SetInitialDeliveryDate(bool Editable)
		{
			this.DeliveryDate = this.currentBatch.OrderDeliveryDate;
			this.dteDeliveryDate.Enabled = Editable;
		}

		public void SetSubmitButtonEnabled(bool Enabled)
		{
			this.btnSave.Enabled = Enabled;
			this.btnConfirm.Enabled = Enabled;
		}

		public void AddressHygiened(object sender, EventArgs e)
		{
			SetSubmitButtonEnabled(true);
		}

        public bool UpdateAccountInfoForPrizeOrder()
        {
            bool accountUpdated = false;
            if ((OrderQualifier)this.DDLOrderQualifier == OrderQualifier.Kanata || (OrderQualifier)this.DDLOrderQualifier == OrderQualifier.KanataPSolver)
            {
                if (currentBatch.Campaign.IsFMAccount == 0 && currentBatch.Campaign.IncentivesBillToID == 51002)
                {
                    DataTable c = new DataTable();
                    CampaignData cData = new CampaignData();
                    cData.SelectFMCommissionCampaign(c, currentBatch.Campaign.FMID);
                    
                    if (c.Rows.Count > 0)
                    {
                        this.AddressControl.DataBind();
                        SetShipToInfoSchool();
                        accountUpdated = true;
                        currentBatch.Campaign.CampaignID = Convert.ToInt32(c.Rows[0][CampaignTable.FLD_ID].ToString());
                        currentBatch.Campaign.AccountID = Convert.ToInt32(c.Rows[0][CampaignTable.FLD_BILLTOACCOUNTID].ToString());
                        currentBatch.Campaign.IsFMAccount = 1;
                    }
                }
            }
            return accountUpdated;
        }
    }
}