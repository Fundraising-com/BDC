namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSP.WebControl;
	using Business.Objects;
	using Common;
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Summary description for AccountListControl.
	/// </summary>
	public partial class AccountListControl : AcctMgtControl
	{
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteToDeliveryDate;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteFromDeliveryDate;
		protected System.Web.UI.WebControls.TextBox lblFromDeliveryDate;
		protected System.Web.UI.WebControls.TextBox lblToDeliveryDate;
		private string sSortExpression = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(AccountListControl));
			AttachEvents();
			if(QSPFulfillment.CommonWeb.QSPPage.aUserProfile.HasRole("HomeOffice") != true)
				GenerateFSBtn.Visible= false;
		
			if(!IsPostBack)
			{
				ddlSupplyStatus.Items.Add("All");
				ddlSupplyStatus.Items.Add("Generated");
				ddlSupplyStatus.Items.Add("Not Generated");
			}
			if(hidDataBind.Value=="1")
			{
				hidDataBind.Value="0";
				DataBindResults();
			}
		}

		[Ajax.AjaxMethod()]
		public string GenerateFieldSupply(int nCampaignID, string zProvince, string zStartDate, string zEndDate, string zFMID)
		{			
			//string x = new string("Field Supply generation started");
			try
			{
				AccountCampaignList acList = new AccountCampaignList();
				acList.GenerateFieldSupplies(1007,nCampaignID, zStartDate, zEndDate, zProvince, zFMID);
			}
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
				
			}
			return "FS Generation completed";
		}
		private void AccountListControl_PreRender(object sender, EventArgs e)
		{
			SetVisible();

			/*if(!IsPostBack && !this.SelectionMode) 
			{
				LoadSearchState();
			}*/
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.dtgMain.TemplateSelection += new DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventHandler(this.dtgMain_TemplateSelection);
			this.dtgMain.ItemDataBound += new DataGridItemEventHandler(dtgMain_ItemDataBound);
			this.dtgMain.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgMain_PageIndexChanged);
			this.dtgMain.SortCommand += new DataGridSortCommandEventHandler(dtgMain_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new EventHandler(AccountListControl_PreRender);
		}
		#endregion

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.dtgMain.CurrentPageIndex = 0;

			DataBindResults();

			/*if(!this.SelectionMode) 
			{
				SaveSearchState();
			}*/
		}

		private void dtgMain_TemplateSelection(object sender, DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventArgs e)
		{
			switch(e.Row.Table.TableName)
			{
				case "Campaign":
					e.TemplateFilename = "Control/CampaignListControl" + ".ascx";
					
					break;
			}
		}

		private void dtgMain_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			HtmlAnchor hypNewCampaign = null;

			if(QSPPage.aUserProfile.IsFM) 
			{
				if(e.Item.ItemType == ListItemType.Item ||
					e.Item.ItemType == ListItemType.AlternatingItem ||
					e.Item.ItemType == ListItemType.EditItem ||
					e.Item.ItemType == ListItemType.SelectedItem) 
				{
					try 
					{
						hypNewCampaign = (HtmlAnchor) e.Item.FindControl("hypNewCampaign");
					} 
					catch { }

					if(hypNewCampaign != null) 
					{
						hypNewCampaign.Visible = false;
					}
				}
			}
		}

      private void dtgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			if(e.NewPageIndex >= 0 && e.NewPageIndex < this.dtgMain.PageCount) 
			{
				this.dtgMain.CurrentPageIndex = e.NewPageIndex;
				DataBindResults();
			}
		}

		private void dtgMain_SortCommand(object source, DataGridSortCommandEventArgs e)
		{
			sSortExpression = e.SortExpression;
			DataBindResults();
		}

		private void CampaignListControl_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Clone") 
			{
				LoadDataResults();
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

		public bool SelectionMode 
		{
			get 
			{
				if(this.ViewState["SelectionMode"] == null)
					return false;

				return Convert.ToBoolean(this.ViewState["SelectionMode"]);
			}
			set 
			{
				this.ViewState["SelectionMode"] = value;
			}
		}

		public string ParentControlName 
		{
			get 
			{
				if(this.ViewState["ParentControlName"] == null) 
					return "";

				return this.ViewState["ParentControlName"].ToString();
			}
			set 
			{
				this.ViewState["ParentControlName"] = value;
			}
		}

		#region Fields
		private string FMIDSearch
		{
			get 
			{
				return this.ddlFieldManager.SelectedValue;
			}
			set 
			{
				this.ddlFieldManager.SelectedIndex = this.ddlFieldManager.Items.IndexOf(this.ddlFieldManager.Items.FindByValue(value));
			}
		}

		private int AccountIDSearch
		{
			get 
			{
				try 
				{
					return Convert.ToInt32(this.tbxGroupID.Text);
				} 
				catch 
				{
					return 0;
				}
			}
			set 
			{
				this.tbxGroupID.Text = value.ToString();
			}
		}

		private int CampaignIDSearch 
		{
			get 
			{
				try 
				{
					return Convert.ToInt32(this.tbxCampaignID.Text);
				} 
				catch 
				{
					return 0;
				}
			}
			set 
			{
				this.tbxCampaignID.Text = value.ToString();
			}
		}

		private string NameSearch
		{
			get 
			{
				return this.tbxGroupName.Text;
			}
			set 
			{
				this.tbxGroupName.Text = value;
			}
		}

		private string CitySearch
		{
			get 
			{
				return this.tbxGroupCity.Text;
			}
			set 
			{
				this.tbxGroupCity.Text = value;
			}
		}

		private string ProvinceSearch
		{
			get 
			{
				return this.ddlGroupProvince.SelectedValue;
			}
			set 
			{
				this.ddlGroupProvince.SelectedIndex = this.ddlGroupProvince.Items.IndexOf(this.ddlGroupProvince.Items.FindByValue(value));
			}
		}

		private string PostalCodeSearch
		{
			get 
			{
				return this.tbxGroupPostalCode.Text;
			}
			set 
			{
				this.tbxGroupPostalCode.Text = value;
			}
		}

		private int FiscalYearSearch
		{
			get 
			{
				return Convert.ToInt32(this.ddlFiscalYear.SelectedValue);
			}
			set 
			{
				this.ddlFiscalYear.SelectedIndex = this.ddlFiscalYear.Items.IndexOf(this.ddlFiscalYear.Items.FindByValue(value.ToString()));
			}
		}
		private string FromDeliveryDateSearch
		{
			get 
			{

				
				return this.dteFromDeliveryDate.Value.ToString();
			}
			set 
			{
				this.lblFromDeliveryDate.Text="";
			}
		}
		private string ToDeliveryDateSearch
		{
			get 
			{
				return this.dteToDeliveryDate.Value.ToString();
			}
			set 
			{
				this.lblToDeliveryDate.Text="";
			}
		}
		#endregion

		public override void DataBind()
		{
			LoadDataSearch();
		}


		private void LoadDataSearch() 
		{
			LoadDataDDL();

			SetValueSearch();
			SetEnabledSearch();

			if(this.AccountID != 0) 
			{
				this.tbxGroupID.Text = this.AccountID.ToString();
				DataBindResults();
			}
		}

		private void SetValueSearch() 
		{
			if(QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999") 
			{
				this.FMIDSearch = QSPPage.aUserProfile.FMID;
			}
		}

		private void SetEnabledSearch() 
		{
			this.ddlFieldManager.Enabled = !QSPPage.aUserProfile.IsFM;
		}

		private void LoadDataDDL() 
		{
			LoadDataDDLProvince();
			LoadDataDDLFieldManager();
			LoadDataDDLFiscalYear();

		}

		private void LoadDataDDLProvince() 
		{
			if(this.ddlGroupProvince.Items.Count == 0) 
			{
				try 
				{
					this.ddlGroupProvince.DataBind();
				} 
				catch(MessageException ex) 
				{
					this.Page.SetPageError(ex);
				}
			}
		}

		private void LoadDataDDLFieldManager() 
		{
			if(this.ddlFieldManager.Items.Count == 0) 
			{
				try 
				{
					FieldManager fm = new FieldManager();
					fm.GetAll();

					this.ddlFieldManager.DataSource = fm.dataSet;
					this.ddlFieldManager.DataMember = fm.dataSet.FieldManager.TableName;
					this.ddlFieldManager.DataTextField = fm.dataSet.FieldManager.ListNameColumn.ColumnName;
					this.ddlFieldManager.DataValueField = fm.dataSet.FieldManager.FMIDColumn.ColumnName;
					this.ddlFieldManager.DataBind();
				} 
				catch (MessageException ex) 
				{
					this.Page.SetPageError(ex);
				}
			}
		}

		private void LoadDataDDLFiscalYear() 
		{
			if(this.ddlFiscalYear.Items.Count == 0) 
			{
				try 
				{
					Season s = new Season();
					s.GetAllFiscalYears();

					this.ddlFiscalYear.DataSource =s.dataSet;
					this.ddlFiscalYear.DataMember = s.dataSet.Season.TableName;
					this.ddlFiscalYear.DataTextField = s.dataSet.Season.FiscalYearColumn.ColumnName;
					this.ddlFiscalYear.DataValueField = s.dataSet.Season.FiscalYearColumn.ColumnName;
					this.ddlFiscalYear.DataBind();
				} 
				catch (MessageException ex) 
				{
					this.Page.SetPageError(ex);
				}
			}
		}

		public void DataBindResults() 
		{
			this.dtgMain.RowExpanded.CollapseAll();

			SetValueSearch();

			LoadDataResults();
		}

		private void LoadDataResults() 
		{
			try 
			{
				
				AccountCampaignList acList = new AccountCampaignList();
				if(!this.SelectionMode) 
				{
					acList.Search(FMIDSearch, AccountIDSearch, CampaignIDSearch, NameSearch, CitySearch, ProvinceSearch, PostalCodeSearch, FiscalYearSearch,FromDeliveryDateSearch,ToDeliveryDateSearch,ddlSupplyStatus.SelectedIndex);
				} 
				else 
				{
					acList.SearchAccountOnly(FMIDSearch, AccountIDSearch, CampaignIDSearch, NameSearch, CitySearch, ProvinceSearch, PostalCodeSearch, FiscalYearSearch,FromDeliveryDateSearch,ToDeliveryDateSearch,ddlSupplyStatus.SelectedIndex);
				}
				acList.dataSet.CAccount.DefaultView.Sort = sSortExpression;

				this.dtgMain.DataSource = acList.dataSet.CAccount.DefaultView;
				//acList.dataSet.CAccount.Count
				this.dtgMain.DataBind();

				CampaignCount.Text = acList.dataSet.Campaign.Count.ToString();
			} 
			catch (MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void SetVisible() 
		{
			// TODO: If a better way to do this exists, we need to replace this code
			// If a column is added to the DataGrid AFTER the button columns, this will crash.
			// Ben - 05/05/10
			this.dtgMain.Columns[this.dtgMain.Columns.Count - 2].Visible = !this.SelectionMode;
			this.dtgMain.Columns[this.dtgMain.Columns.Count - 1].Visible = this.SelectionMode;

			this.btnCancel.Visible = this.SelectionMode;
		}

		private void LoadSearchState() 
		{
			if(Session["FMIDState"] != null) 
			{
				this.ddlFieldManager.SelectedIndex = this.ddlFieldManager.Items.IndexOf(this.ddlFieldManager.Items.FindByValue(Session["FMIDState"].ToString()));
				this.tbxGroupID.Text = Session["AccountIDState"].ToString();
				this.tbxCampaignID.Text = Session["CampaignIDState"].ToString();
				this.tbxGroupName.Text = Session["NameState"].ToString();
				this.tbxGroupCity.Text = Session["CityState"].ToString();
				this.ddlGroupProvince.SelectedIndex = this.ddlGroupProvince.Items.IndexOf(this.ddlGroupProvince.Items.FindByValue(Session["ProvinceState"].ToString()));
				this.tbxGroupPostalCode.Text = Session["PostalCodeState"].ToString();
				this.ddlFiscalYear.SelectedIndex = this.ddlFiscalYear.Items.IndexOf(this.ddlFiscalYear.Items.FindByValue(Session["FiscalYearState"].ToString()));
			}
		}

		private void SaveSearchState() 
		{
			Session["FMIDState"] = this.ddlFieldManager.SelectedValue;
			Session["AccountIDState"] = this.tbxGroupID.Text;
			Session["CampaignIDState"] = this.tbxCampaignID.Text;
			Session["NameState"] = this.tbxGroupName.Text;
			Session["CityState"] = this.tbxGroupCity.Text;
			Session["ProvinceState"] = this.ddlGroupProvince.SelectedValue;
			Session["PostalCodeState"] = this.tbxGroupPostalCode.Text;
			Session["FiscalYearState"] = this.ddlFiscalYear.SelectedValue;
		}

		private void AttachEvents() 
		{
			foreach(DataGridItem item in this.dtgMain.Items) 
			{
				// Tweak to reach controls in nested datagrid - no other solution in this version of
				// HierarGrid yet.
				if(item.Cells[0].FindControl("DCP").FindControl("Panel") != null) 
				{
                    ((DataGrid)item.Cells[0].FindControl("DCP").FindControl("Panel").FindControl("Panel_CAccountCampaign").FindControl("ChildTemplate_CAccountCampaign").FindControl("dtgMain")).ItemCommand += new DataGridCommandEventHandler(CampaignListControl_ItemCommand);
				}
			}
		}
	}
}